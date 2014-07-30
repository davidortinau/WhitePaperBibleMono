
using System;
using System.Drawing;

using MonoTouch.Foundation;
using MonoTouch.UIKit;
using WhitePaperBible.iOS;

namespace WhitePaperBible.iOS
{
	public partial class MyPapersAndProfileController : UIViewController
	{
		public MyPapersAndProfileController () : base ("MyPapersAndProfileController", null)
		{
			this.Title = "My Papers";
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

		}

		public override void ViewDidLayoutSubviews()
		{
			base.ViewDidLayoutSubviews();

			var profileView = new ProfileView ();
			profileView.Frame = new RectangleF (0, 64, View.Bounds.Width, profileView.Bounds.Height);
			View.AddSubview (profileView);

			profileView.GoToEdit.Invoked += (object sender, EventArgs e) => {
				var editView = new EditProfileView();
				this.NavigationController.PushViewController(editView, true);
			};

			var myPapersView = new MyPapersView ();

			var containerView = new UIView (new RectangleF (0, 164, View.Bounds.Width, View.Bounds.Height - 164));
			containerView.AddSubview (myPapersView.TableView);
			View.AddSubview (containerView);
		}

	}
}

