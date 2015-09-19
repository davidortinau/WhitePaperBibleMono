
using System;
using CoreGraphics;

using Foundation;
using UIKit;
using WhitePaperBible.iOS.Utils;
using MonkeyArms;

namespace WhitePaperBible.iOS
{
	public partial class LoginRequiredController : UIViewController
	{
		public readonly Invoker LoginRegister = new Invoker ();

		public readonly Invoker CancelRegister = new Invoker ();

		bool ShowCancel;

		public LoginRequiredController (bool showCancel=true) : base ("LoginRequiredController", null)
		{
			this.ShowCancel = showCancel;

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

			CancelButton.Hidden = !ShowCancel;

			ApplyStyle.LoginRequired (this);
			
			AddEventListeners ();

//			UpdateTopConstraint ();
		}

//		public override void DidRotate (UIInterfaceOrientation fromInterfaceOrientation)
//		{
////			UpdateTopConstraint ();
//			base.DidRotate (fromInterfaceOrientation);
//		}

		void AddEventListeners ()
		{
			LoginRegisterButton.TouchUpInside += (object sender, EventArgs e) => {
				LoginRegister.Invoke ();
			};

			CancelButton.TouchUpInside += (object sender, EventArgs e) => {
				CancelRegister.Invoke ();
			};
		}

//		void UpdateTopConstraint(){
//			if (this.TopConstraint != null) {
//				this.TopConstraint.Constant = UIApplication.SharedApplication.StatusBarFrame.Height + this.NavigationController.NavigationBar.Frame.Height;
//			}
//		}
	}
}

