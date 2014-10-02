
using System;
using System.Drawing;

using MonoTouch.Foundation;
using MonoTouch.UIKit;
using WhitePaperBible.iOS;
using WhitePaperBible.iOS.Invokers;
using WhitePaperBible.Core.Views;
using MonkeyArms;
using IOS.Util;

namespace WhitePaperBible.iOS
{
	public partial class MyPapersAndProfileController : UIViewController, IMyPapersAndProfileView, IMediatorTarget
	{
		LoginRequiredController LoginRequiredView;

		ProfileView profileView;

		MyPapersView myPapersView;

		UIView containerView;

		public Invoker Logout {get;private set;}

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
//			LoginRequiredView.View.Frame = new RectangleF (0, 48, View.Bounds.Width, View.Bounds.Height);

			View.AddSubview (LoginRequiredView.View);
			View.BringSubviewToFront (LoginRequiredView.View);
			LoginRequiredView.LoginRegister.Invoked += (object sender, EventArgs e) => ShowLoginForm ();
//			LoginRequiredView.CancelRegister.Invoked += (object sender, EventArgs e) => DismissLoginPrompt ();
			LoginRequiredView.View.Hidden = true;
		}

		public void ShowPaper (WhitePaperBible.Core.Models.Paper paper)
		{
			var paperDetails = new WhitePaperBible.iOS.PaperDetailsView(paper);
			NavigationController.PushViewController (paperDetails, true);
		}

		public MyPapersAndProfileController () : base ("MyPapersAndProfileController", null)
		{
			this.Title = "My Papers";

			Logout = new Invoker ();
		

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
						
			// Perform any additional setup after loading the view, typically from a nib.
			NavigationItem.SetRightBarButtonItem (
				new UIBarButtonItem ("Logout", UIBarButtonItemStyle.Plain, (sender, args)=> {
					Logout.Invoke();
				})
				, true
			);

		}

		public override void ViewDidDisappear (bool animated)
		{
			base.ViewDidDisappear (animated);
			DI.DestroyMediator (this);
			if (this.myPapersView != null) {
				DI.DestroyMediator (this.myPapersView);
			}

			if(this.profileView != null){
				DI.DestroyMediator (this.profileView);
			}
		}

		public override void ViewWillAppear (bool animated)
		{
			base.ViewWillAppear (animated);


		}

		public override void ViewDidAppear (bool animated)
		{
			base.ViewDidAppear (animated);
			DI.RequestMediator (this);
			if (this.myPapersView != null) {
				DI.RequestMediator (this.myPapersView);
			}

			if(this.profileView != null){
				DI.RequestMediator (this.profileView);
			}

			AnalyticsUtil.TrackScreen (this.Title);
		}

		public override void ViewDidLayoutSubviews()
		{
			base.ViewDidLayoutSubviews();

			if (profileView == null) {
				profileView = new ProfileView ();
				profileView.Frame = new RectangleF (0, 64, View.Bounds.Width, profileView.Bounds.Height);
				View.AddSubview (profileView);

				profileView.GoToEdit.Invoked += (object sender, EventArgs e) => {
					var editView = new EditProfileView ();
					this.NavigationController.PushViewController (editView, true);
				};
			}

			if (myPapersView == null) {
				myPapersView = new MyPapersView ();

				containerView = new UIView (new RectangleF (0, 164, View.Bounds.Width, View.Bounds.Height - 164));
				containerView.AddSubview (myPapersView.TableView);
				View.AddSubview (containerView);
			}

			if (LoginRequiredView != null) {
				View.BringSubviewToFront (LoginRequiredView.View);
			}


		}

		public override void DidRotate (UIInterfaceOrientation fromInterfaceOrientation)
		{
			Resize (fromInterfaceOrientation);
			base.DidRotate (fromInterfaceOrientation);
		}

		void Resize (UIInterfaceOrientation fromInterfaceOrientation)
		{
			var top = 64;
			if(fromInterfaceOrientation == UIInterfaceOrientation.Portrait || fromInterfaceOrientation == UIInterfaceOrientation.PortraitUpsideDown){
				top = 32;
			}

//			if(LoginRequiredView != null){
//				LoginRequiredView.Frame = new RectangleF (0, top - 16, View.Bounds.Width, View.Bounds.Height);
//			}

			LoginRequiredView.TopConstraint.Constant = UIApplication.SharedApplication.StatusBarFrame.Height + this.NavigationController.NavigationBar.Frame.Height;

			if(containerView != null){
				containerView.Frame = new RectangleF (0, 100 + top, View.Bounds.Width, View.Bounds.Height - (100 + top));
			}

			if(profileView != null){
				profileView.Frame = new RectangleF (0, top, View.Bounds.Width, 100);
			}
		}

	}
}

