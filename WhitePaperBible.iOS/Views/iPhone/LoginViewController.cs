
using System;
using CoreGraphics;

using Foundation;
using UIKit;
using WhitePaperBible.Core.Views;
using MonkeyArms;
using WhitePaperBible.iOS.UI;
using WhitePaperBible.iOS.Managers;
using WhitePaperBible.iOS.Utils;
using IOS.Util;
using WhitePaperBible.iOS.Invokers;
using BigTed;

namespace WhitePaperBible.iOS
{
	public partial class LoginViewController : UIViewController, ILoginView
	{
		public Invoker LoginFinished {
			get;
			private set;
		}

		public Invoker LoginCancelled {
			get;
			private set;
		}

		public Invoker RegistrationClosed {
			get;
			private set;
		}

		public event EventHandler LoginSubmitted;
		public event EventHandler MoreInfoClicked;

//		UITextField UsernameInput, PasswordInput;
//		UIView loginForm;
//		WPBButton SubmitButton;
//		UIButton RegisterButton;

		static bool UserInterfaceIdiomIsPhone {
			get { return UIDevice.CurrentDevice.UserInterfaceIdiom == UIUserInterfaceIdiom.Phone; }
		}

		public LoginViewController () : base ("LoginViewController", null)
		{
			this.Title = "Login";
			this.LoginFinished = new Invoker ();
			this.LoginCancelled = new Invoker ();
			this.RegistrationClosed = new Invoker ();
		}

		public string UserName {
			get {
				return UsernameInput.Text;
			}
		}

		public string Password {
			get {
				return PasswordInput.Text;
			}
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

			this.View.BackgroundColor = AppStyles.OffBlack;

			AddEventHandlers ();

			AnalyticsUtil.TrackScreen (this.Title);

			ApplyStyle.LoginView (this);
		}

		public override void ViewDidAppear (bool animated)
		{
			base.ViewDidAppear (animated);
			DI.RequestMediator (this);

		}

		public override void ViewDidDisappear (bool animated)
		{
			base.ViewDidDisappear (animated);
			DI.DestroyMediator (this);
		}

		public void ShowInvalidPrompt (string message)
		{
			InvokeOnMainThread (() => {
				BTProgressHUD.ShowErrorWithStatus(message, 3000);
			});
		}

		void AddEventHandlers ()
		{
			LoginButton.TouchUpInside += (object sender, EventArgs e) => {
				LoginSubmitted(this, EventArgs.Empty);
			};

			CancelButton.TouchUpInside += (object sender, EventArgs e) => {
				DismissViewController (true, null);
				LoginCancelled.Invoke ();
			};

			RegisterButton.TouchUpInside += (object sender, EventArgs e) => {
				var registrationView = new RegistrationView();
				this.PresentViewController(registrationView, true, null);
				registrationView.Dismissed += OnRegistrationDismissed;
			};


			UsernameInput.ShouldReturn = delegate {
				PasswordInput.BecomeFirstResponder ();
				return true;
			};

			PasswordInput.ShouldReturn = delegate {
				PasswordInput.ResignFirstResponder ();
				return true;
			};
		}

		void OnRegistrationDismissed(object sender, EventArgs e) {
			RegistrationClosed.Invoke();

		}

		public void ShowBusyIndicator ()
		{
			InvokeOnMainThread (() => {
				this.ShowNetworkActivityIndicator ();
				LoginButton.Enabled = false;
				LoginButton.Alpha = .25f;
			});

		}

		public void HideBusyIndicator ()
		{
			InvokeOnMainThread (() => {
				this.HideNetworkActivityIndicator ();
				LoginButton.Enabled = true;
				LoginButton.Alpha = 1;
			});
		}

		public void Dismiss()
		{
			InvokeOnMainThread (() => {
				LoginFinished.Invoke(new LoginFinishedInvokerArgs (this));
			});
		}


	}
}

