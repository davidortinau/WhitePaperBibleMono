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
	[Register ("LoginRequiredController")]
	partial class LoginRequiredController
	{
		[Outlet]
		public MonoTouch.UIKit.UIButton CancelButton { get; private set; }

		[Outlet]
		public MonoTouch.UIKit.UITextView DescriptionLabel { get; private set; }

		[Outlet]
		public MonoTouch.UIKit.UIButton LoginRegisterButton { get; private set; }

		[Outlet]
		public MonoTouch.UIKit.NSLayoutConstraint TopConstraint { get; private set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (CancelButton != null) {
				CancelButton.Dispose ();
				CancelButton = null;
			}

			if (DescriptionLabel != null) {
				DescriptionLabel.Dispose ();
				DescriptionLabel = null;
			}

			if (LoginRegisterButton != null) {
				LoginRegisterButton.Dispose ();
				LoginRegisterButton = null;
			}

			if (TopConstraint != null) {
				TopConstraint.Dispose ();
				TopConstraint = null;
			}
		}
	}
}
