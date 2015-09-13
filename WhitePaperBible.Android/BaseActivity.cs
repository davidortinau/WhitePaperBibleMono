
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

namespace WhitePaperBible.Android
{
	[Activity]			
	public class BaseActivity : AppCompatActivity,IMediatorTarget
	{
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			// Create your application here
		}

		private bool _hideOverflow;

		protected void OnCreateWithLayout(Bundle bundle, int layoutId, bool hideOverflowMenu = false)
		{
			base.OnCreate(bundle);

			_hideOverflow = hideOverflowMenu;

			SetContentView(layoutId);

			var toolbar = FindViewById<Toolbar>(Resource.Id.toolbar);

			SetSupportActionBar(toolbar);

			SupportActionBar.SetDisplayHomeAsUpEnabled(true);
			SupportActionBar.SetHomeButtonEnabled(true);
			//			SupportActionBar.SetHomeAsUpIndicator(Resource.Drawable.icon_add);
		}

		public override bool OnCreateOptionsMenu(IMenu menu)
		{
			if (_hideOverflow)
			{
				MenuInflater.Inflate(Resource.Menu.empty, menu);
			}
			else
			{
				MenuInflater.Inflate(Resource.Menu.sub, menu);
			}
			return base.OnCreateOptionsMenu(menu);
		}

		public override bool OnOptionsItemSelected(IMenuItem item)
		{
//			if (item.ItemId == Android.Resource.Id.Home)
//			{
//				Finish();
//			}

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
	}
}

