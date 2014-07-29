
using System;
using System.Drawing;

using MonoTouch.Foundation;
using MonoTouch.UIKit;

namespace WhitePaperBible.iOS
{
	public partial class CopyrightsView : UIViewController
	{
		public CopyrightsView () : base ("CopyrightsView", null)
		{
			this.Title = "Copyrights";
		}

		public override void DidReceiveMemoryWarning ()
		{
			// Releases the view if it doesn't have a superview.
			base.DidReceiveMemoryWarning ();
			
			// Release any cached data, images, etc that aren't in use.
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			var textView = new UITextView (new RectangleF (0, 0, View.Bounds.Width, View.Bounds.Height));
			textView.Text = "© 2014 Simply Profound, LLC\n\nAll Scripture quotations, unless otherwise indicated, are taken from The Holy Bible, English Standard Version. Copyright ©2001 by Crossway Bibles, a publishing ministry of Good News Publishers. Used by permission. All rights reserved. Text provided by the Crossway Bibles Web Service.";
			this.View.AddSubview (textView);
		}
	}
}

