
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
using Android.Support.Design.Widget;

namespace WhitePaperBible.Droid
{
	[Activity (MainLauncher=true, NoHistory=true)]			
	public class MainView : BaseActivityView, ILoadingView
	{
		protected BaseMediatedFragment CurrentScreen;

		protected override void OnStart ()
		{
			base.OnStart ();
		}

		protected override void OnCreate (Bundle bundle)
		{
//			RequestWindowFeature(WindowFeatures.NoTitle);

			base.OnCreateWithLayout(bundle, Resource.Layout.MainView);

			var pager = FindViewById<ViewPager>(Resource.Id.pager);
			pager.Adapter = new MainViewPagerAdapter(this.SupportFragmentManager, this.Resources);

			var tabs = FindViewById<TabLayout>(Resource.Id.tab_layout);    
			tabs.SetupWithViewPager (pager);            

			this.setSupportActionBarTitle (Resource.String.tab_papers);
			tabs.TabGravity=TabLayout.GravityFill;

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


