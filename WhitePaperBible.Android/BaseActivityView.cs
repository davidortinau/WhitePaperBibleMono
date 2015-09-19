
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Support.V7.App;
using Android.Support.V7.Widget;
using MonkeyArms;
using Android.Graphics.Drawables;

namespace WhitePaperBible.Droid
{
	[Activity]			
	public class BaseActivityView : AppCompatActivity, IMediatorTarget
	{
//		private int _layoutId;
//
//		public BaseActivityView (int layoutId)
//		{
//			_layoutId = layoutId;
//		}
//
//		protected override void OnCreate(Bundle bundle)
//		{			
//			RequestWindowFeature(WindowFeatures.NoTitle);
//			base.OnCreate(bundle);
//			SetContentView(_layoutId);  
//			configureActionBar (Resource.Id.toolbar);
//		}

		protected IMenu _menu;

		private void configureActionBar(int idResource)
		{
			var toolbar = FindViewById<Android.Support.V7.Widget.Toolbar> (idResource);
			if (toolbar != null) {
				SetSupportActionBar (toolbar);
				setSupportActionBarTitle ("");
			}
		}

		protected void OnCreateWithLayout(Bundle bundle, int layoutId)
		{
			RequestWindowFeature(WindowFeatures.NoTitle);

			base.OnCreate(bundle);
			SetContentView(layoutId);
			configureActionBar (Resource.Id.toolbar);
		}

//		public override bool OnCreateOptionsMenu(IMenu menu)
//		{
//			if (_hideOverflow)
//			{
//				MenuInflater.Inflate(Resource.Menu.empty, menu);
//			}
//			else
//			{
//				MenuInflater.Inflate(Resource.Menu.home, menu);
//			}
//			return base.OnCreateOptionsMenu(menu);
//		}

		public override bool OnOptionsItemSelected(IMenuItem item)
		{
			return base.OnOptionsItemSelected(item);
		}

		protected override void OnResume()
		{
			base.OnResume();
			try
			{
				DI.RequestMediator(this);
			}
			catch
			{
				Console.WriteLine("Unable to request mediator for: " + this.Class.Name);
			}
		}

		protected override void OnPause()
		{
			base.OnPause();
			try
			{
				DI.DestroyMediator(this);
			}
			catch
			{
				Console.WriteLine("Unable to destroy mediator for: " + this.Class.Name);
			}
		}

		protected void Logout()
		{
//			Intent intent = new Intent(this, typeof(LoginActivity));
//			intent.AddFlags(ActivityFlags.ClearTask);
//			intent.AddFlags(ActivityFlags.NewTask);
//			StartActivity(intent);
		}

		protected void showToast(int resourceId)
		{
			var toast = Toast.MakeText(this, resourceId, ToastLength.Short);
			toast.Show();
		}

		protected void setSupportActionBarTitle(string title)
		{
			this.SupportActionBar.Title = title; 
		}
		protected void setSupportActionBarTitle(int resourceId)
		{
			this.setSupportActionBarTitle (Resources.GetString (resourceId));
		}
		protected void setSupportActionBarBackgroundColor(int idResourceColor)
		{
			this.SupportActionBar.SetBackgroundDrawable (new ColorDrawable (Resources.GetColor(idResourceColor)));
		}
		protected void addSupportActionBarBackButton()
		{			
			this.SupportActionBar.SetDisplayHomeAsUpEnabled (true);
		}
	}
}

