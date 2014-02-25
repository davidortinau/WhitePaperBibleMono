using System;
using System.Collections.Generic;
using System.Linq;

using MonoTouch.Foundation;
using MonoTouch.UIKit;
using MonoTouch.Dialog;
using WhitePaperBible.Core.Views;
using MonkeyArms;
using WhitePaperBible.iOS.UI;
using System.Drawing;
using WhitePaperBible.iOS.Managers;
using WhitePaperBible.iOS.Invokers;

namespace WhitePaperBible.iOS
{
	public partial class LoginView : UIViewController, ILoginView
	{
		public Invoker LoginFinished {
			get;
			private set;
		}

		public event EventHandler LoginSubmitted;

		public event EventHandler MoreInfoClicked;

		UITextField UsernameInput, PasswordInput;
		UIView loginForm;
		TNTButton SubmitButton;
		UIButton RegisterButton;

		static bool UserInterfaceIdiomIsPhone {
			get { return UIDevice.CurrentDevice.UserInterfaceIdiom == UIUserInterfaceIdiom.Phone; }
		}

		public LoginView () : base ()
		{
			this.Title = "Login";
			this.LoginFinished = new Invoker ();
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			this.View.BackgroundColor = UIColor.White;

			InitUI ();
			AddEventHandlers ();

		}

		public override void ViewDidAppear (bool animated)
		{
			base.ViewDidAppear (animated);
			DI.RequestMediator(this);

		}

		public override void ViewDidDisappear (bool animated)
		{
			base.ViewDidDisappear (animated);
			DI.DestroyMediator (this);
		}

		public void ShowInvalidPrompt (string message)
		{
			InvokeOnMainThread (() => {
				var alert = new UIAlertView (ResourceManager.GetString("authenticationError"), ResourceManager.GetString("invalidLogin"), null, ResourceManager.GetString("ok"), null);
				alert.Show ();
			});
		}

		public void GoToNextScreen ()
		{
//			InvokeOnMainThread (() => {
//				var navigationController = new CustomNavigationController ();
//				var tabViewController = new MainTabViewController (athleteID,teamID);
//				navigationController.PushViewController (tabViewController, false);
//				PresentViewController(navigationController, true, null);
//
//			});
			LoginFinished.Invoke (new LoginFinishedInvokerArgs (this));
		}

		void InitUI ()
		{
			CreateLoginForm ();

			//Submit Button
			SubmitButton = new TNTButton (ResourceManager.GetString("login"), 
				AppStyles.Red,
				loginForm.Frame.Bottom + 20);

			SubmitButton.CenterHorizontally ();
			View.AddSubview (SubmitButton);

			//Register Button
			RegisterButton = new UIButton ();
			var buttonY = WhitePaperBible.iOS.UI.Environment.DeviceScreenHeight - 60;
			RegisterButton.Frame = new RectangleF (10, buttonY, 280, 43);
			RegisterButton.CenterHorizontally ();
			RegisterButton.SetTitle (ResourceManager.GetString("register"), UIControlState.Normal);
			RegisterButton.BackgroundColor = UIColor.Clear;
			RegisterButton.SetTitleColor (UIColor.White, UIControlState.Normal); 
			RegisterButton.Font = UIFont.FromName ("Helvetica", 12);
			RegisterButton.SetTitleColor (AppStyles.DarkGray, UIControlState.Highlighted); 
			View.AddSubview (RegisterButton);
		}

		void AddEventHandlers ()
		{
			SubmitButton.TouchUpInside += (object sender, EventArgs e) => {
//				LoginSubmitted(this, EventArgs.Empty);
				LoginFinished.Invoke (new LoginFinishedInvokerArgs (this));
			};

			RegisterButton.TouchUpInside += (object sender, EventArgs e) => {
				MoreInfoClicked(this, EventArgs.Empty);
			};


			UsernameInput.ShouldReturn = delegate {
				PasswordInput.BecomeFirstResponder();
				return true;
			};

			PasswordInput.ShouldReturn = delegate {
				PasswordInput.ResignFirstResponder();
				return true;
			};
		}

		void CreateLoginForm ()
		{
			//form container
			loginForm = new UIView (new RectangleF (0, WhitePaperBible.iOS.UI.Environment.DeviceScreenHeight, WhitePaperBible.iOS.UI.Environment.ScreenWidth, 125));
			loginForm.BackgroundColor = AppStyles.OffWhite;
			View.AddSubview (loginForm);

			//Username Input
			var userNameRect = new RectangleF (0, 20, WhitePaperBible.iOS.UI.Environment.ScreenWidth - 40, 40);
			loginForm.AddSubview (CreateLabel("email", userNameRect));
			UsernameInput = CreateInput ("athlete8@gmail.com", ResourceManager.GetString("emailPlaceholder"), userNameRect, UIReturnKeyType.Next, false);
			loginForm.AddSubview (UsernameInput);

			//horizontal rul
//			HorizontalRule rule = new HorizontalRule(userNameRect.Bottom, userNameRect.Width, TNTStyles.DarkGrayRule);
//			rule.CenterHorizontally ();
//			loginForm.AddSubview (rule);

			//Password Input
			var passwordRect = new RectangleF (10, userNameRect.Bottom, WhitePaperBible.iOS.UI.Environment.ScreenWidth - 40, 40);
			loginForm.AddSubview (CreateLabel("password", passwordRect));
			PasswordInput = CreateInput ("password8", "", passwordRect, UIReturnKeyType.Done, true);
			loginForm.AddSubview (PasswordInput);




		}
		protected UILabel CreateLabel(string labelID, RectangleF rowRect)
		{
			var label = new UILabel (rowRect){
				TextColor = UIColor.White,
				Text = ResourceManager.GetString(labelID),
				Font = UIFont.FromName("Helvetica-Bold", 16f),
				BackgroundColor = UIColor.Clear

			};
			label.CenterHorizontally ();
			return label;
		}

		protected UITextField CreateInput(string value,
			string placeholder,
			RectangleF rowRect,
			UIReturnKeyType returnKeyType,
			bool isPassword)
		{
			var tf = new UITextField (rowRect) {
				BorderStyle = UITextBorderStyle.None,
				Placeholder = placeholder,
				SecureTextEntry = isPassword,
				VerticalAlignment = UIControlContentVerticalAlignment.Center,
				ReturnKeyType = returnKeyType,
				TextColor = UIColor.White,
				TextAlignment = UITextAlignment.Right,
				BackgroundColor = UIColor.Clear,
				Text = value
			};

			tf.AttributedPlaceholder = new NSAttributedString (placeholder, null, UIColor.White);
			tf.CenterHorizontally ();
			return tf;
		}
	}

	public class TNTButton:UIButton
	{
		public TNTButton (string title, UIColor backgroundColor, float yPos, UIColor borderColor = null, float borderWidth = 0, UIColor titleColor = null):base(new RectangleF (10, yPos, WhitePaperBible.iOS.UI.Environment.ScreenWidth - 20, 40))
		{
			BackgroundColor = backgroundColor;
			Layer.CornerRadius = 3f;
			Layer.BorderWidth = borderWidth;
			if (borderColor != null) {
				Layer.BorderColor = borderColor.CGColor;
			}
			Font = UIFont.FromName ("Helvetica", 21);

			SetTitleColor (titleColor ?? UIColor.White, UIControlState.Normal);
			SetTitle (title, UIControlState.Normal);
			this.TouchDown += (object sender, EventArgs e) => {
				this.Alpha = .8f;
			};
			this.TouchUpInside += (object sender, EventArgs e) => {
				this.Alpha = 1f;
			};
			this.TouchUpOutside += (object sender, EventArgs e) => {
				this.Alpha = 1f;
			};
		}

		public override void LayoutSubviews ()
		{
			base.LayoutSubviews ();

		}
	}
}
