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
    [Register ("BibleSearchView")]
    partial class BibleSearchView
    {
        [Outlet]
        UIKit.UIView ResultsContainer { get; set; }


        [Outlet]
        UIKit.UISearchBar SearchBar { get; set; }


        [Outlet]
        UIKit.NSLayoutConstraint TopConstraint { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (ResultsContainer != null) {
                ResultsContainer.Dispose ();
                ResultsContainer = null;
            }

            if (SearchBar != null) {
                SearchBar.Dispose ();
                SearchBar = null;
            }

            if (TopConstraint != null) {
                TopConstraint.Dispose ();
                TopConstraint = null;
            }
        }
    }
}