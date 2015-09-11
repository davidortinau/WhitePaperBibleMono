using System;
using System.Collections.Generic;
using System.Linq;
using Foundation;
using UIKit;
using MonoTouch.Dialog;
using WhitePaperBible.Core.Views;
using MonkeyArms;
using WhitePaperBible.Core.Models;
using WhitePaperBible.iOS.Invokers;
using IOS.Util;

namespace WhitePaperBible.iOS
{
	public partial class FavoritesView : DialogViewController, IFavoritesView, IMediatorTarget
	{
		LoginRequiredController LoginRequiredView;

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
			if(papers.Count == 0){
				return;
			}

			InvokeOnMainThread (delegate {

				Root = new RootElement ("Favorites") {
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

		public void ShowLoginButton()
		{
			NavigationItem.SetRightBarButtonItem (
				new UIBarButtonItem ("Login", UIBarButtonItemStyle.Plain, (sender, args)=> {
					ShowLoginForm();
				})
				, true
			);
		}

		public void HideLoginButton()
		{
			NavigationItem.SetRightBarButtonItem (null,false);
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


			AnalyticsUtil.TrackScreen (this.Title);
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

		public void PromptForLogin ()
		{
			if (LoginRequiredView == null) {
				CreateLoginRequiredView ();
			}
			LoginRequiredView.View.Hidden = false;
		}

		public void ShowLoginForm ()
		{
			var loginView = new LoginViewController ();
			loginView.LoginFinished.Invoked += (object sender, EventArgs e) => {
				(e as LoginFinishedInvokerArgs).Controller.DismissViewController (true, null);
				DismissLoginPrompt();
				HideLoginButton();
			};

			this.PresentViewController (loginView, true, null);
		}

		public void DismissLoginPrompt()
		{
			InvokeOnMainThread (() => {
				if (LoginRequiredView != null && !LoginRequiredView.View.Hidden) {
					LoginRequiredView.View.Hidden = true;
				}
			});

		}

		protected void CreateLoginRequiredView ()
		{
			LoginRequiredView = new LoginRequiredController (false);
			LoginRequiredView.View.Frame = this.View.Frame;
			//			LoginRequiredView.View.Frame = new CGRect (0, 48, View.Bounds.Width, View.Bounds.Height);

			View.AddSubview (LoginRequiredView.View);
			View.BringSubviewToFront (LoginRequiredView.View);
			LoginRequiredView.LoginRegister.Invoked += (object sender, EventArgs e) => ShowLoginForm ();
			//			LoginRequiredView.CancelRegister.Invoked += (object sender, EventArgs e) => DismissLoginPrompt ();
			LoginRequiredView.View.Hidden = true;
		}
	}
}
