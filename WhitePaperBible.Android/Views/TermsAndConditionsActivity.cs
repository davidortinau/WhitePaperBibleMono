
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
using System.IO;

namespace WhitePaperBible.Droid
{
	[Activity (Label = "")]			
	public class TermsAndConditionsActivity : BaseActivityView, IInjectingTarget
	{
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreateWithLayout(bundle, Resource.Layout.TermsAndConditionsView);

			this.setSupportActionBarTitle ("Terms & Conditions");
			this.addSupportActionBarBackButton ();

			var txt = FindViewById<TextView>(Resource.Id.TermsTextView);
			string content;
			using (StreamReader sr = new StreamReader (Assets.Open ("WPB Terms and Conditions.txt")))
			{
				content = sr.ReadToEnd ();
			}
			txt.Text = content;
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
	}

}

