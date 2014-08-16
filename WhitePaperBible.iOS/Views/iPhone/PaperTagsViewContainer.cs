
using System;
using System.Drawing;

using MonoTouch.Foundation;
using MonoTouch.UIKit;

namespace WhitePaperBible.iOS
{
	public partial class PaperTagsViewContainer : UIViewController
	{
		public PaperTagsView tagsView;

		public EditPaperView Controller {
			get;
			set;
		}

		public PaperTagsViewContainer () : base ("PaperTagsViewContainer", null)
		{
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
			
			// Perform any additional setup after loading the view, typically from a nib.
			var bar = new UIToolbar (new RectangleF (0, 0, View.Bounds.Width, 64));
			View.AddSubview (bar);

			var btn = new UIBarButtonItem (UIBarButtonSystemItem.Done);
			UIBarButtonItem[] btns = new UIBarButtonItem[]{btn};
			bar.SetItems (btns,false);

			btn.Clicked += (object sender, EventArgs e) => {
				tagsView.ReturnTags();
				this.ParentViewController.NavigationController.PopViewControllerAnimated(true);
			};
		}

		public override void ViewDidLayoutSubviews()
		{
			base.ViewDidLayoutSubviews();

			if (tagsView == null) {
				tagsView = new PaperTagsView ();
				tagsView.Controller = Controller;

				var containerView = new UIView (new RectangleF (0, 64, View.Bounds.Width, View.Bounds.Height - 64));
				containerView.AddSubview (tagsView.TableView);
				View.AddSubview (containerView);
			}
		}
	}
}

