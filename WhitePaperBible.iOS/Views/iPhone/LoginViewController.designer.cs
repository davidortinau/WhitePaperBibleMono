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
    [Register ("LoginViewController")]
    partial class LoginViewController
    {
        [Outlet]
        public UIKit.UIButton CancelButton { get; set; }


        [Outlet]
        public UIKit.UIButton LoginButton { get; set; }


        [Outlet]
        public UIKit.UITextField PasswordInput { get; set; }


        [Outlet]
        public UIKit.UILabel PasswordLabel { get; set; }


        [Outlet]
        public UIKit.UIButton RegisterButton { get; set; }


        [Outlet]
        public UIKit.UITextField UsernameInput { get; set; }


        [Outlet]
        public UIKit.UILabel UsernameLabel { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (CancelButton != null) {
                CancelButton.Dispose ();
                CancelButton = null;
            }

            if (LoginButton != null) {
                LoginButton.Dispose ();
                LoginButton = null;
            }

            if (PasswordInput != null) {
                PasswordInput.Dispose ();
                PasswordInput = null;
            }

            if (PasswordLabel != null) {
                PasswordLabel.Dispose ();
                PasswordLabel = null;
            }

            if (RegisterButton != null) {
                RegisterButton.Dispose ();
                RegisterButton = null;
            }

            if (UsernameInput != null) {
                UsernameInput.Dispose ();
                UsernameInput = null;
            }

            if (UsernameLabel != null) {
                UsernameLabel.Dispose ();
                UsernameLabel = null;
            }
        }
    }
}