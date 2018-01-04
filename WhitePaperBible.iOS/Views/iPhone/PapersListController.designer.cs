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
    [Register ("PapersListController")]
    partial class PapersListController
    {
        [Outlet]
        UIKit.UIView ListContainer { get; set; }


        [Outlet]
        UIKit.NSLayoutConstraint TopConstraint { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (ListContainer != null) {
                ListContainer.Dispose ();
                ListContainer = null;
            }

            if (TopConstraint != null) {
                TopConstraint.Dispose ();
                TopConstraint = null;
            }
        }
    }
}