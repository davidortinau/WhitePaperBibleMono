
using System;
using System.Drawing;

using MonoTouch.Foundation;
using MonoTouch.UIKit;
using WhitePaperBible.Core.Views;
using MonkeyArms;
using System.Collections.Generic;
using WhitePaperBible.Core.Models;
using WhitePaperBible.iOS.Invokers;
using IOS.Util;

namespace WhitePaperBible.iOS
{
	public partial class PapersListController : UIViewController, IPapersListView
	{
		PapersView papersList;

		LoginRequiredController LoginRequiredView;

		public PapersListController () : base ("PapersListController_iPhone", null)
		{
			this.Title = "Papers";
			this.AddPaper = new Invoker ();
		}

		public override void DidReceiveMemoryWarning ()
		{
			// Releases the view if it doesn't have a superview.
			base.DidReceiveMemoryWarning ();
			
			// Release any cached data, images, etc that aren't in use.
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			AnalyticsUtil.TrackScreen (this.Title);

			NavigationItem.SetRightBarButtonItem (
				new UIBarButtonItem (UIBarButtonSystemItem.Compose, (sender, args)=> {
					AddPaper.Invoke();
				})
				, true
			);

			UpdateTopConstraint ();
		}

		public override void ViewDidLayoutSubviews()
		{
			base.ViewDidLayoutSubviews ();

			if (papersList == null) {
				AddDialog ();
			}
		}

		void UpdateTopConstraint ()
		{
			if(this.TopConstraint != null){
				this.TopConstraint.Constant = UIApplication.SharedApplication.StatusBarFrame.Height + this.NavigationController.NavigationBar.Frame.Height;

				if(LoginRequiredView != null){
					LoginRequiredView.TopConstraint.Constant = this.TopConstraint.Constant;
				}
			}
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

		void AddDialog ()
		{
			papersList = new PapersView (this.NavigationController);
			ListContainer.AddSubview (papersList.TableView);
		}

		public void SetPapers (List<Paper> papers)
		{
			papersList.SetPapers (papers);
		}

		public Invoker AddPaper {
			get;
			private set;
		}

		public void AddPaperEditView()
		{
			var addPaperView = new EditPaperView();
			var editNav = new UINavigationController (addPaperView);
			this.PresentViewController (editNav, true, null);
		}

		public string SearchPlaceHolderText {
			get{
				return papersList.SearchPlaceHolderText;
			}
			set {
				if (papersList != null) {
					papersList.SearchPlaceHolderText = value;
				}
			}
		}

		public string SearchQuery {
			get{
				return papersList.SearchQuery;
			}
			set {
				papersList.SearchQuery = value;
			}
		}

		public Paper SelectedPaper {
			get{
				return papersList.SelectedPaper;
			}
			set {
				papersList.SelectedPaper = value;
			}
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
			};

			this.PresentViewController (loginView, true, null);
		}

		protected void CreateLoginRequiredView ()
		{
			LoginRequiredView = new LoginRequiredController ();
			LoginRequiredView.View.Frame = this.View.Frame;
			View.AddSubview (LoginRequiredView.View);
			View.BringSubviewToFront (LoginRequiredView.View);
			LoginRequiredView.LoginRegister.Invoked += (object sender, EventArgs e) => ShowLoginForm ();
			LoginRequiredView.CancelRegister.Invoked += (object sender, EventArgs e) => DismissLoginPrompt();
			LoginRequiredView.View.Hidden = true;
		}

		public void DismissLoginPrompt()
		{
			if (LoginRequiredView != null && !LoginRequiredView.View.Hidden) {
				LoginRequiredView.View.Hidden = true;
				LoginRequiredView = null;
			}
		}

		public override void DidRotate (UIInterfaceOrientation fromInterfaceOrientation)
		{
			UpdateTopConstraint ();
			base.DidRotate (fromInterfaceOrientation);
		}
	}
}

