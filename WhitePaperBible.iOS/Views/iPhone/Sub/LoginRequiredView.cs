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

		public readonly Invoker CancelRegister = new Invoker ();

		bool ShowCancel;

		public LoginRequiredView (float height, bool showCancel=true) : base (new RectangleF (0, 0, 320, height))
		{
			this.BackgroundColor = AppStyles.OffWhite;
			this.ShowCancel = showCancel;
		}

		public override void LayoutSubviews ()
		{
			base.LayoutSubviews ();
			CreateDescription ();
			CreateLoginRegisterButton ();
			if (ShowCancel) {
				CreateCancelButton ();
			}
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

		void CreateCancelButton ()
		{
			WPBButton cancelRegisterButton = new WPBButton (ResourceManager.GetString ("cancel"),
				AppStyles.DarkGray,
				190);
			cancelRegisterButton.TouchUpInside += (object sender, EventArgs e) => {
				CancelRegister.Invoke ();
			};
			AddSubview (cancelRegisterButton);
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

