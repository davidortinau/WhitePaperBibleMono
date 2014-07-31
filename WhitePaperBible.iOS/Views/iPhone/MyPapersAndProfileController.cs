
using System;
using System.Drawing;

using MonoTouch.Foundation;
using MonoTouch.UIKit;
using WhitePaperBible.iOS;
using WhitePaperBible.iOS.Invokers;
using WhitePaperBible.Core.Views;
using MonkeyArms;

namespace WhitePaperBible.iOS
{
	public partial class MyPapersAndProfileController : UIViewController, IMyPapersAndProfileView, IMediatorTarget
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
			loginView.LoginFinished.Invoked += (object sender, EventArgs e) => {
				(e as LoginFinishedInvokerArgs).Controller.DismissViewController (true, null);
			};

			this.PresentViewController (loginView, true, null);
		}

		public void DismissLoginPrompt()
		{
			if (LoginRequiredView != null && !LoginRequiredView.Hidden) {
				LoginRequiredView.Hidden = true;
			}
		}

		protected void CreateLoginRequiredView ()
		{
			LoginRequiredView = new LoginRequiredView (WhitePaperBible.iOS.UI.Environment.DeviceScreenHeight);
			View.AddSubview (LoginRequiredView);
			View.BringSubviewToFront (LoginRequiredView);
			LoginRequiredView.LoginRegister.Invoked += (object sender, EventArgs e) => ShowLoginForm ();
			LoginRequiredView.Hidden = true;
		}

		public MyPapersAndProfileController () : base ("MyPapersAndProfileController", null)
		{
			this.Title = "My Papers";
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
			DI.RequestMediator (this);
			
			// Perform any additional setup after loading the view, typically from a nib.

		}

		public override void ViewDidDisappear (bool animated)
		{
			base.ViewDidDisappear (animated);
			DI.DestroyMediator (this);
		}

		public override void ViewDidLayoutSubviews()
		{
			base.ViewDidLayoutSubviews();

			var profileView = new ProfileView ();
			profileView.Frame = new RectangleF (0, 64, View.Bounds.Width, profileView.Bounds.Height);
			View.AddSubview (profileView);

			profileView.GoToEdit.Invoked += (object sender, EventArgs e) => {
				var editView = new EditProfileView();
				this.NavigationController.PushViewController(editView, true);
			};

			var myPapersView = new MyPapersView ();

			var containerView = new UIView (new RectangleF (0, 164, View.Bounds.Width, View.Bounds.Height - 164));
			containerView.AddSubview (myPapersView.TableView);
			View.AddSubview (containerView);

			if (LoginRequiredView != null) {
				View.BringSubviewToFront (LoginRequiredView);
			}
		}

	}
}

