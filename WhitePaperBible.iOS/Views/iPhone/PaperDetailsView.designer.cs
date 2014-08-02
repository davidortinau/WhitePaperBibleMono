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
	[Register ("PaperDetailsView")]
	partial class PaperDetailsView
	{
		[Outlet]
		MonoTouch.UIKit.UIBarButtonItem favoriteButton { get; set; }

		[Outlet]
		MonoTouch.UIKit.UIToolbar toolbar { get; set; }

		[Outlet]
		MonoTouch.UIKit.UIWebView webView { get; set; }

		[Action ("favoritePressed:")]
		partial void favoritePressed (MonoTouch.UIKit.UIBarButtonItem sender);

		[Action ("sharePressed:")]
		partial void sharePressed (MonoTouch.UIKit.UIBarButtonItem sender1);
		
		void ReleaseDesignerOutlets ()
		{
			if (webView != null) {
				webView.Dispose ();
				webView = null;
			}

			if (toolbar != null) {
				toolbar.Dispose ();
				toolbar = null;
			}

			if (favoriteButton != null) {
				favoriteButton.Dispose ();
				favoriteButton = null;
			}
		}
	}
}
