
using System.Collections.Generic;

using Android.App;
using Android.OS;
using WhitePaperBible.Core.Views;
using MonkeyArms;
using Android.Widget;
using System;
using Android.Text;
using Android.Webkit;
using Android.Content;
using WhitePaperBible.Core.Models;
using Newtonsoft.Json;
using Android.Support.V4.View;
using Android.Support.V7.View;
using Java.Interop;
using Android.Views;
using WhitePaperBible.Droid.Adapters;

namespace WhitePaperBible.Droid
{
	[Activity (Label = "")]			
    public class LoginActivity : BaseActivityView, IInjectingTarget, ILoginView
	{
		protected override void OnCreate (Bundle bundle)
		{
            base.OnCreateWithLayout(bundle, Resource.Layout.LoginView);

            this.setSupportActionBarTitle (Resource.String.Login);
			this.addSupportActionBarBackButton ();

            this.LoginFinished = new Invoker ();
            this.LoginCancelled = new Invoker ();
            this.RegistrationClosed = new Invoker ();

            InitForm();
		}

		public override bool OnOptionsItemSelected (IMenuItem item)
		{
			switch (item.ItemId) {
			case Android.Resource.Id.Home:
				{
					Finish();
					break;
				}
			default:
				return base.OnOptionsItemSelected (item);
			}

			return true;
		}

        #region ILoginView implementation

        public event EventHandler LoginSubmitted;

        public event EventHandler MoreInfoClicked;

        public void ShowInvalidPrompt (string message)
        {
            throw new NotImplementedException ();
        }

        public void ShowBusyIndicator ()
        {
//            throw new NotImplementedException ();
        }

        public void HideBusyIndicator ()
        {
//            throw new NotImplementedException ();
        }

        public void Dismiss ()
        {
            throw new NotImplementedException ();
        }

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

        public string UserName {
            get {
                return usernameTxt.Text;
            }
        }

        public string Password {
            get {
                return passwordTxt.Text;
            }
        }

        #endregion

        TextView usernameTxt;

        TextView passwordTxt;

        Button loginBtn;

        Button registerButton;

        void InitForm ()
        {
            usernameTxt = FindViewById<TextView>(Resource.Id.emailText);
            passwordTxt = FindViewById<TextView>(Resource.Id.passwordText);
            loginBtn = FindViewById<Button>(Resource.Id.loginButton);
            registerButton = FindViewById<Button>(Resource.Id.registerButton);

            loginBtn.Click += (object sender, EventArgs e) => {
                if(IsValid()){
                    LoginSubmitted(this, EventArgs.Empty);
                }
            };

            registerButton.Click += (object sender, EventArgs e) => {
                // start new activity
            };
        }

        bool IsValid ()
        {
            if(string.IsNullOrEmpty(usernameTxt.Text)){
                Toast.MakeText(this, "Please enter a username.", ToastLength.Short);
                usernameTxt.RequestFocus();
                return false;
            }

            if(string.IsNullOrEmpty(passwordTxt.Text)){
                Toast.MakeText(this, "Please enter a password.", ToastLength.Short);
                passwordTxt.RequestFocus();
                return false;
            }

            return true;
        }
	}

}

