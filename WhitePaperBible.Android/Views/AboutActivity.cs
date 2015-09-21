
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

namespace WhitePaperBible.Droid
{
	[Activity (Label = "")]			
	public class AboutActivity : BaseActivityView, IInjectingTarget
	{
		private ListView _listView;

		private List<Paper> _papers;

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreateWithLayout(bundle, Resource.Layout.AboutView);

			this.setSupportActionBarTitle (Resource.String.tab_about);
			this.addSupportActionBarBackButton ();
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

