using System;
using MonoTouch.UIKit;
using System.Drawing;
using WhitePaperBible.iOS.UI;
using WhitePaperBible.iOS.Managers;
using MonkeyArms;

namespace WhitePaperBible.iOS
{
	public class LoginRequiredView:UIView
	{
		public readonly Invoker LoginRegister = new Invoker ();

		public LoginRequiredView (float height) : base (new RectangleF (0, 0, 320, height))
		{
			this.BackgroundColor = AppStyles.OffWhite;
		}

		public override void LayoutSubviews ()
		{
			base.LayoutSubviews ();
			CreateDescription ();
			CreateLoginRegisterButton ();

		}

		void CreateLoginRegisterButton ()
		{
			WPBButton loginRegisterButton = new WPBButton (ResourceManager.GetString ("loginRegister"),
				                                AppStyles.Red,
				                                130);
			loginRegisterButton.TouchUpInside += (object sender, EventArgs e) => {
				LoginRegister.Invoke ();
			};
			AddSubview (loginRegisterButton);
		}

		void CreateDescription ()
		{
			UITextView description = new UITextView (new RectangleF (0, 50, Frame.Width, Frame.Height));
			description.Text = ResourceManager.GetString ("loginRequired");
			description.BackgroundColor = UIColor.Clear;
			description.Font = AppStyles.HelveticaNeue (20);
			description.TextAlignment = UITextAlignment.Center;
			description.Editable = false;
			AddSubview (description);
		}
	}
}

