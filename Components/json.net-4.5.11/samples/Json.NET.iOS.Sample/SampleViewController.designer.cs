// WARNING
//
// This file has been generated automatically by Xamarin Studio to store outlets and
// actions made in the Xcode designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;

namespace Sample
{
	[Register ("SampleViewController")]
	partial class SampleViewController
	{
		[Outlet]
		UIKit.UITextView Json { get; set; }

		[Outlet]
		UIKit.UITextView Output { get; set; }

		[Action ("Parse:")]
		partial void Parse (Foundation.NSObject sender);
		
		void ReleaseDesignerOutlets ()
		{
			if (Json != null) {
				Json.Dispose ();
				Json = null;
			}

			if (Output != null) {
				Output.Dispose ();
				Output = null;
			}
		}
	}
}
