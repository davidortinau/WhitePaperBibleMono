// WARNING
//
// This file has been generated automatically by Xamarin Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;

namespace WhitePaperBible.iOSWatchKitExtension
{
	[Register ("AllPapersController")]
	partial class AllPapersController
	{
		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		WatchKit.WKInterfaceTable PapersTable { get; set; }

		void ReleaseDesignerOutlets ()
		{
			if (PapersTable != null) {
				PapersTable.Dispose ();
				PapersTable = null;
			}
		}
	}
}
