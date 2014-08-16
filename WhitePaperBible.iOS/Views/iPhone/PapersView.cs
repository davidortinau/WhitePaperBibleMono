using System;
using System.Collections.Generic;
using System.Linq;
using WhitePaperBible.Core.Models;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using MonoTouch.Dialog;
using WhitePaperBible.Core.Views;
using MonkeyArms;
using WhitePaperBible.iOS.Invokers;
using WhitePaperBible.iOS.UI.CustomElements;

namespace WhitePaperBible.iOS
{
	public partial class PapersView : DialogViewController, IPapersListView
	{
		LoginRequiredView LoginRequiredView;

		public Invoker RequestAddPaper {
			get;
			private set;
		}

		public void PromptForLogin ()
		{
			if (LoginRequiredView == null) {
				CreateLoginRequiredView ();
				LoginRequiredView.Hidden = false;
			}
		}

		public void ShowLoginForm ()
		{
			var loginView = new LoginView ();
			//			loginView.LoginFinished.Invoked += HandleLoginFinished;
			loginView.LoginFinished.Invoked += (object sender, EventArgs e) => {
				(e as LoginFinishedInvokerArgs).Controller.DismissViewController (true, null);
			};

			this.PresentViewController (loginView, true, null);
		}

		protected void CreateLoginRequiredView ()
		{
			LoginRequiredView = new LoginRequiredView (WhitePaperBible.iOS.UI.Environment.DeviceScreenHeight);
			View.AddSubview (LoginRequiredView);
			View.BringSubviewToFront (LoginRequiredView);
			LoginRequiredView.LoginRegister.Invoked += (object sender, EventArgs e) => ShowLoginForm ();
			LoginRequiredView.Hidden = true;
		}

		public void DismissLoginPrompt()
		{
			if (LoginRequiredView != null && !LoginRequiredView.Hidden) {
				LoginRequiredView.Hidden = true;
			}
		}

		public PapersView () : base (UITableViewStyle.Plain, null, true)
		{
			EnableSearch = true; 
			AutoHideSearch = true;
			SearchPlaceholder = @"Find Papers";
			this.Filter = new Invoker ();
			this.OnPaperSelected = new Invoker ();
			this.AddPaper = new Invoker ();

		}

		public void AddPaperEditView()
		{
			var addPaperView = new EditPaperView();
			var editNav = new UINavigationController (addPaperView);
			this.PresentViewController (editNav, true, null);
		}

		#region IPapersListView implementation

		public Invoker Filter {
			get;
			private set;
		}

		public Invoker OnPaperSelected {
			get;
			private set;
		}

		public Invoker AddPaper {
			get;
			private set;
		}

		public void SetPapers (List<Paper> papers)
		{
			if(papers == null){
				return;
			}

			InvokeOnMainThread (()=> {

				Root = new RootElement ("Papers") {
					new Section(""){
						AddPaperElements(papers)
					}
//					from node in papers
//					group node by (node.title [0].ToString ().ToUpper ()) into alpha
//					orderby alpha.Key
//					select new Section (alpha.Key) {
//						from eachNode in alpha
//						select (Element)new WhitePaperBible.iOS.UI.CustomElements.PaperElement (eachNode, delegate {
//							var paperDetails = new WhitePaperBible.iOS.PaperDetailsView(eachNode);
//							paperDetails.Title = eachNode.title;
//							NavigationController.PushViewController(paperDetails, true);
//						})
//					}
				};

				TableView.ScrollToRow (NSIndexPath.FromRowSection (0, 0), UITableViewScrollPosition.Top, false);

			});

		}

		List<PaperElement> AddPaperElements (List<Paper> papers)
		{
			var elements = new List<PaperElement> ();
			foreach(var p in papers){
				elements.Add (new PaperElement (p, () => {
					var paperDetails = new PaperDetailsView(p);
					paperDetails.Title = p.title;
					NavigationController.PushViewController(paperDetails, true);
				}));
			}
			return elements;
		}

		public string SearchPlaceHolderText {
			get;
			set;
		}

		public string SearchQuery {
			get;
			set;
		}

		public Paper SelectedPaper {
			get;
			set;
		}

		#endregion

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();



			SearchTextChanged += (sender, args) => {
				Console.WriteLine ("search text changed");	
			};

			NavigationItem.SetRightBarButtonItem (
				new UIBarButtonItem ("Add Paper", UIBarButtonItemStyle.Plain, (sender, args)=> {
					AddPaper.Invoke();
				})
				, true
			);
			
		}

		public override void ViewDidDisappear (bool animated)
		{
			base.ViewDidDisappear (animated);


		}

		public override void ViewWillAppear (bool animated)
		{
			base.ViewWillAppear (animated);
			DI.RequestMediator (this);
		}

		public override void ViewWillDisappear (bool animated)
		{
			base.ViewWillDisappear (animated);
			DI.DestroyMediator (this);
		}
	}
}
