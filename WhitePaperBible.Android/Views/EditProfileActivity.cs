
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
    public class EditProfileActivity : BaseActivityView, IEditProfileView, IInjectingTarget
	{
		protected override void OnCreate (Bundle bundle)
		{
            base.OnCreateWithLayout(bundle, Resource.Layout.EditProfileView);

			this.setSupportActionBarTitle (Resource.String.tab_edit_profile);
			this.addSupportActionBarBackButton ();

            Save = new Invoker();
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

		
        #region IEditProfileView implementation

        EditText _nameText, _websiteText, _emailText, _usernameText, _passwordText, _confirmPasswordText;
        public void SetUserProfile (AppUser user)
        {
            _nameText = FindViewById<EditText>(Resource.Id.NameText);
            _websiteText = FindViewById<EditText>(Resource.Id.WebsiteText);
            _emailText = FindViewById<EditText>(Resource.Id.EmailText);
            _usernameText = FindViewById<EditText>(Resource.Id.UsernameText);
            _passwordText = FindViewById<EditText>(Resource.Id.PasswordText);
            _confirmPasswordText = FindViewById<EditText>(Resource.Id.ConfirmPasswordText);

            _nameText.Text = user.Name;
            _websiteText.Text = user.Website;
            _emailText.Text = user.Email;
            _usernameText.Text = user.username;
        }

        public Invoker Save {
            get;
            private set;
        }
        #endregion
	}

}

