// WARNING
//
// This file has been generated automatically by Visual Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;

namespace WhitePaperBible.iOS
{
    [Register ("PaperDetailsView")]
    partial class PaperDetailsView
    {
        [Outlet]
        UIKit.UIBarButtonItem favoriteButton { get; set; }


        [Outlet]
        UIKit.UIToolbar toolbar { get; set; }


        [Outlet]
        UIKit.UIWebView webView { get; set; }


        [Action ("favoritePressed:")]
        partial void favoritePressed (UIKit.UIBarButtonItem sender);


        [Action ("sharePressed:")]
        partial void sharePressed (UIKit.UIBarButtonItem sender1);

        void ReleaseDesignerOutlets ()
        {
            if (favoriteButton != null) {
                favoriteButton.Dispose ();
                favoriteButton = null;
            }

            if (toolbar != null) {
                toolbar.Dispose ();
                toolbar = null;
            }

            if (webView != null) {
                webView.Dispose ();
                webView = null;
            }
        }
    }
}