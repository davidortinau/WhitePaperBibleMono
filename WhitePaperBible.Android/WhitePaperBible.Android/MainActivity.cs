using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using WhitePaperBibleCore.Services;
using WhitePaperBibleCore.Models;
using WhitePaperBibleCore.Invokers;
using WhitePaperBibleCore.Commands;
using System.Collections.Generic;

using MonkeyArms;

namespace WhitePaperBible.Android
{
	[Activity (Label = "Welcome", MainLauncher = true)]
	public class Activity1 : Activity
	{
		protected override void OnStart ()
		{
			base.OnStart ();
		}

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			// Set our view from the "main" layout resource
			SetContentView (Resource.Layout.Main);

			initUI ();
		}

		void initUI() {
			Button button = FindViewById<Button> (Resource.Id.myButton);

			button.Click += delegate {
				var papersView = new Intent(this, typeof(PapersListActivity));
				StartActivity( papersView );
			};
		}


	}
}


