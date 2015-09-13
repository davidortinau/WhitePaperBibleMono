
using Android.App;
using Android.Content;
using Android.OS;

using WhitePaperBible.Core.Views;
using MonkeyArms;
using WhitePaperBible.Droid.Fragments;
using Android.Support.V4.App;
using Android.Views;
using Android.Support.V4.View;
using Adapters;
using com.refractored;

namespace WhitePaperBible.Droid
{
	[Activity (MainLauncher=true, NoHistory=true)]			
	public class MainActivity : FragmentActivity, ILoadingView
	{
		protected BaseFragment CurrentScreen;

		protected override void OnStart ()
		{
			base.OnStart ();
		}

		protected override void OnCreate (Bundle bundle)
		{
			RequestWindowFeature(WindowFeatures.NoTitle);
			base.OnCreate(bundle);

			SetContentView(Resource.Layout.MainView);

			var pager = FindViewById<ViewPager>(Resource.Id.pager);

			var PagerAdapter = new MainViewPagerAdapter(this.SupportFragmentManager, this.Resources);
			pager.Adapter = PagerAdapter;

			var tabs = FindViewById<PagerSlidingTabStrip>(Resource.Id.tabs);          
			tabs.SetViewPager(pager);   


//			DI.RequestMediator(this);
		}

		#region ILoadingView implementation
		public void OnLoadingComplete ()
		{
			var papersView = new Intent(this, typeof(PapersView));
			StartActivity( papersView );
		}
		#endregion

	}
}


