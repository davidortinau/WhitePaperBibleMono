
using System;
using System.Drawing;

using MonoTouch.Foundation;
using MonoTouch.UIKit;

namespace WhitePaperBible.iOS
{
	public partial class TermsAndConditionsView : UIViewController
	{
		public TermsAndConditionsView () : base ("TermsAndConditionsView", null)
		{
			this.Title = "Terms & Conditions";
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

			var text  = System.IO.File.ReadAllText("WPB Terms and Conditions.txt", System.Text.Encoding.UTF8);
			var textView = new UITextView (new RectangleF (0, 0, View.Bounds.Width, View.Bounds.Height));
			textView.Text = text;
			this.View.AddSubview (textView);
		}
	}
}

