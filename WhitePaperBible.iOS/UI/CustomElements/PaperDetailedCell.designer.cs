// WARNING
//
// This file has been generated automatically by Visual Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;

namespace CustomElements
{
    [Register ("PaperDetailedCell")]
    partial class PaperDetailedCell
    {
        [Outlet]
        public UIKit.UILabel AuthorLabel { get; set; }


        [Outlet]
        public UIKit.UILabel TitleLabel { get; set; }


        [Outlet]
        public UIKit.UILabel ViewCountLabel { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (AuthorLabel != null) {
                AuthorLabel.Dispose ();
                AuthorLabel = null;
            }

            if (TitleLabel != null) {
                TitleLabel.Dispose ();
                TitleLabel = null;
            }

            if (ViewCountLabel != null) {
                ViewCountLabel.Dispose ();
                ViewCountLabel = null;
            }
        }
    }
}