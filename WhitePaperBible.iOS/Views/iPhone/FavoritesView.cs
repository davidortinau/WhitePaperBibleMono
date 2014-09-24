using System;
using System.Collections.Generic;
using System.Linq;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using MonoTouch.Dialog;
using WhitePaperBible.Core.Views;
using MonkeyArms;
using WhitePaperBible.Core.Models;
using WhitePaperBible.iOS.Invokers;

namespace WhitePaperBible.iOS
{
	public partial class FavoritesView : DialogViewController, IFavoritesView, IMediatorTarget
	{
		LoginRequiredView LoginRequiredView;

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

		public FavoritesView () : base (UITableViewStyle.Plain, null, true)
		{
			EnableSearch = true; 
			AutoHideSearch = true;
			SearchPlaceholder = @"Find Papers";
			this.Filter = new Invoker ();
			this.OnPaperSelected = new Invoker ();
			this.Title = @"Favorites";


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

		public void SetPapers (List<Paper> papers)
		{
			if(LoginRequiredView != null && !LoginRequiredView.Hidden){
				LoginRequiredView.Hidden = true;
			}

			InvokeOnMainThread (delegate {

				Root = new RootElement ("Papers") {
					from node in papers
					group node by (node.title [0].ToString ().ToUpper ()) into alpha
					orderby alpha.Key
					select new Section (alpha.Key) {
						from eachNode in alpha
						select (Element)new WhitePaperBible.iOS.UI.CustomElements.PaperElement (eachNode, delegate {
							var paperDetails = new WhitePaperBible.iOS.PaperDetailsView(eachNode);
							paperDetails.Title = eachNode.title;
							NavigationController.PushViewController(paperDetails, true);
						})
					}
				};

				TableView.ScrollToRow (NSIndexPath.FromRowSection (0, 0), UITableViewScrollPosition.Top, false);
			});

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



		}

		public override void ViewDidAppear (bool animated)
		{
			base.ViewDidAppear (animated);
			DI.RequestMediator (this);
			this.Title = "Favorites";
//			View.BringSubviewToFront (LoginRequiredView);
		}

		public override void ViewDidDisappear (bool animated)
		{
			base.ViewDidDisappear (animated);

			DI.DestroyMediator (this);
		}

		protected void CreateLoginRequiredView ()
		{
			LoginRequiredView = new LoginRequiredView (WhitePaperBible.iOS.UI.Environment.DeviceScreenHeight, false);
			LoginRequiredView.Frame = new System.Drawing.RectangleF (0, 48, View.Bounds.Width, View.Bounds.Height);

			View.AddSubview (LoginRequiredView);
			View.BringSubviewToFront (LoginRequiredView);
			LoginRequiredView.LoginRegister.Invoked += (object sender, EventArgs e) => ShowLoginForm ();

			LoginRequiredView.Hidden = true;
		}
	}
}
