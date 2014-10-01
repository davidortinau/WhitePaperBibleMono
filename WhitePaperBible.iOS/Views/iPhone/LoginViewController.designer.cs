// WARNING
//
// This file has been generated automatically by Xamarin Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using MonoTouch.Foundation;
using System.CodeDom.Compiler;

namespace WhitePaperBible.iOS
{
	[Register ("LoginViewController")]
	partial class LoginViewController
	{
		[Outlet]
		public MonoTouch.UIKit.UIButton CancelButton { get; set; }

		[Outlet]
		public MonoTouch.UIKit.UIButton LoginButton { get; set; }

		[Outlet]
		public MonoTouch.UIKit.UITextField PasswordInput { get; set; }

		[Outlet]
		public MonoTouch.UIKit.UILabel PasswordLabel { get; set; }

		[Outlet]
		public MonoTouch.UIKit.UIButton RegisterButton { get; set; }

		[Outlet]
		public MonoTouch.UIKit.UITextField UsernameInput { get; set; }

		[Outlet]
		public MonoTouch.UIKit.UILabel UsernameLabel { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (UsernameInput != null) {
				UsernameInput.Dispose ();
				UsernameInput = null;
			}

			if (PasswordInput != null) {
				PasswordInput.Dispose ();
				PasswordInput = null;
			}

			if (UsernameLabel != null) {
				UsernameLabel.Dispose ();
				UsernameLabel = null;
			}

			if (PasswordLabel != null) {
				PasswordLabel.Dispose ();
				PasswordLabel = null;
			}

			if (LoginButton != null) {
				LoginButton.Dispose ();
				LoginButton = null;
			}

			if (CancelButton != null) {
				CancelButton.Dispose ();
				CancelButton = null;
			}

			if (RegisterButton != null) {
				RegisterButton.Dispose ();
				RegisterButton = null;
			}
		}
	}
}
