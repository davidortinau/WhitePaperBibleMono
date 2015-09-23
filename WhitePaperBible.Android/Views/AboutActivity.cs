
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
	public class AboutActivity : BaseActivityView, IInjectingTarget
	{
		private ListView _listView;

		ListView _appInfoList;

		ListView _supportList;

		private List<string> _supportItems = new List<string>(){
			"Get Help",
			"Send Feedback"
		};

		private List<string> _appInfoItems = new List<string>(){
			"App Version",
			"Visit whitepaperbible.org",
			"Preface to the ESV Bible",
			"Licenses",
			"Terms and Conditions"
		};

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreateWithLayout(bundle, Resource.Layout.AboutView);

			this.setSupportActionBarTitle (Resource.String.tab_about);
			this.addSupportActionBarBackButton ();

			BindLists();
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

		void BindLists ()
		{
			_supportList = FindViewById<ListView>(Resource.Id.SupportList);
			_supportList.Adapter = new AboutListAdapter(this, _supportItems);
			_supportList.ItemClick += OnSupportRowClicked;

			_appInfoList = FindViewById<ListView>(Resource.Id.AppInfoList);
			_appInfoList.Adapter = new AboutListAdapter(this, _appInfoItems);
			_appInfoList.ItemClick += OnAppRowClicked;
		}

		void OnSupportRowClicked (object sender, AdapterView.ItemClickEventArgs e)
		{
			string subject = string.Empty;
			string message = string.Empty;

			switch(e.Position){
			case 0:
				{
					subject = "White Paper Bible: Help Needed";
					message = "How can we help?";
					break;
				}
			case 1:
				{
					subject = "White Paper Bible: Feedback";
					message = "We'd love to hear from you. What's on your mind?";
					break;
				}
			}

			var email = new Intent (Android.Content.Intent.ActionSend);
			email.PutExtra(Android.Content.Intent.ExtraEmail, new string[]{"dave@whitepaperbible.org"});
			email.PutExtra(Android.Content.Intent.ExtraSubject, subject);
			email.PutExtra(Android.Content.Intent.ExtraText, message);
			email.SetType("message/rfc822");
			StartActivity(email);
		}

		void OnAppRowClicked (object sender, AdapterView.ItemClickEventArgs e)
		{
			switch(e.Position){
			case 0:
				{
					break;
				}
			case 1:
				{
					var uri = Android.Net.Uri.Parse ("http://www.whitepaperbible.org");
					var intent = new Intent (Intent.ActionView, uri);
					StartActivity (intent);
					break;
				}
			case 2:
				{
					var uri = Android.Net.Uri.Parse ("http://www.gnpcb.org/esv/preface/");
					var intent = new Intent (Intent.ActionView, uri);
					StartActivity (intent);
					break;
				}
			case 3:
				{
					var intent = new Intent(this, typeof(CopyRightsActivity));
					StartActivity(intent);
					break;
				}
			case 4:
				{
					var intent = new Intent(this, typeof(TermsAndConditionsActivity));
					StartActivity(intent);
					break;
				}
			}

//			"App Version",
//			"Visit whitepaperbible.org",
//			"Preface to the ESV Bible",
//			"Licenses",
//			"Terms and Conditions"
		}
	}

}

