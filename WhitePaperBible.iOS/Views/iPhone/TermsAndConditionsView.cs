
using System;
using CoreGraphics;

using Foundation;
using UIKit;
using IOS.Util;

namespace WhitePaperBible.iOS
{
	public partial class TermsAndConditionsView : UIViewController
	{
		bool ShowDone;

		public TermsAndConditionsView (bool showDone=false) : base ("TermsAndConditionsView", null)
		{
			this.Title = "Terms & Conditions";
			ShowDone = showDone;
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

			var textTop = 0;

			if(ShowDone){
				var bar = new UIToolbar (new CGRect (0, 0, View.Bounds.Width, 48));
//				bar.BarTintColor = UIColor.DarkGray;
				bar.TintColor = UIColor.DarkGray;
				var done = new UIBarButtonItem (UIBarButtonSystemItem.Done);

				UIBarButtonItem[] btns = new UIBarButtonItem[]{done};
				bar.SetItems (btns,false);

				done.Clicked += (object sender, EventArgs e) => {
//					this.ParentViewController.NavigationController.PopViewControllerAnimated(true);
					this.DismissViewController(true, null);
				};
				this.Add (bar);

				textTop = 48;
			}

			var text  = System.IO.File.ReadAllText("WPB Terms and Conditions.txt", System.Text.Encoding.UTF8);
			var textView = new UITextView (new CGRect (0, textTop, View.Bounds.Width, View.Bounds.Height));
			textView.Text = text;
			this.View.AddSubview (textView);

			AnalyticsUtil.TrackScreen (this.Title);
		}
	}
}

