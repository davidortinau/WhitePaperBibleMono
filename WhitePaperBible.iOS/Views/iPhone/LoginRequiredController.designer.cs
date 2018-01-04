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
    [Register ("LoginRequiredController")]
    partial class LoginRequiredController
    {
        [Outlet]
        public UIKit.UIButton CancelButton { get; private set; }


        [Outlet]
        public UIKit.UITextView DescriptionLabel { get; private set; }


        [Outlet]
        public UIKit.UIButton LoginRegisterButton { get; private set; }


        [Outlet]
        public UIKit.NSLayoutConstraint TopConstraint { get; private set; }

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