
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

using Android.Support.Design.Widget;
using Android.Support.V7.Widget;
using Android.Graphics;
using Android.Text.Style;
using Android.Text;
using Android.Graphics.Drawables;
using Java.Lang;
using Rendr.Droid.Components;

namespace WhitePaperBible.Droid
{
	[Activity (MainLauncher=true, NoHistory=false)]			
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
			pager.OffscreenPageLimit = 4;

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

		public override bool OnCreateOptionsMenu(IMenu menu)
		{
			MenuInflater.Inflate(Resource.Menu.home, menu);

			_menu = menu;

            var key = Resources.GetText (Resource.String.icon_search);
            var icon = new TextDrawable(key, this);
            var searchItem = _menu.FindItem(Resource.Id.menu_search);
            searchItem.SetIcon(icon);

			return base.OnCreateOptionsMenu(menu);
		}

		public override bool OnOptionsItemSelected (IMenuItem item)
		{
			switch (item.ItemId) {
			case Resource.Id.menu_search:
				{
					var courseIntent = new Intent(this.BaseContext, typeof(SearchPapersActivity));
					StartActivity(courseIntent);
					break;
				}
			case Resource.Id.menu_about:
				{
					var courseIntent = new Intent(this.BaseContext, typeof(AboutActivity));
					StartActivity(courseIntent);
					break;
				}
			case Resource.Id.menu_profile:
				{
//					showToast("Not Implemented");
                    var loginIntent = new Intent(this.BaseContext, typeof(LoginActivity));
                    StartActivity(loginIntent);
					break;
				}
			default:
				return base.OnOptionsItemSelected (item);
			}

			return true;
		}

	}




}


