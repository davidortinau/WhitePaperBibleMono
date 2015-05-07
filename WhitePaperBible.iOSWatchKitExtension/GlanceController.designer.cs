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
	[Register ("GlanceController")]
	partial class GlanceController
	{
		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		WatchKit.WKInterfaceLabel ReferenceLabel { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		WatchKit.WKInterfaceLabel VerseLabel { get; set; }

		void ReleaseDesignerOutlets ()
		{
			if (ReferenceLabel != null) {
				ReferenceLabel.Dispose ();
				ReferenceLabel = null;
			}
			if (VerseLabel != null) {
				VerseLabel.Dispose ();
				VerseLabel = null;
			}
		}
	}
}
