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
	[Register ("PapersListController")]
	partial class PapersListController
	{
		[Outlet]
		MonoTouch.UIKit.UIView ListContainer { get; set; }

		[Outlet]
		MonoTouch.UIKit.NSLayoutConstraint ListTopConstraint { get; set; }

		[Outlet]
		MonoTouch.UIKit.UIView LoginRequired { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (ListContainer != null) {
				ListContainer.Dispose ();
				ListContainer = null;
			}

			if (LoginRequired != null) {
				LoginRequired.Dispose ();
				LoginRequired = null;
			}

			if (ListTopConstraint != null) {
				ListTopConstraint.Dispose ();
				ListTopConstraint = null;
			}
		}
	}
}
