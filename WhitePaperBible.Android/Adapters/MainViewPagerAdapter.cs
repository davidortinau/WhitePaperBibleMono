using System;
using Android.Support.V4.App;
using System.Collections.Generic;
using Android.Content.Res;
using WhitePaperBible.Droid;
using Views;

namespace Adapters
{
	public class MainViewPagerAdapter: FragmentStatePagerAdapter
	{

		List< Tuple<string, Func<Fragment>>> fragmentsStore;

		public MainViewPagerAdapter( FragmentManager fragmentManager, Resources resources)
			: base (fragmentManager)
		{
			fragmentsStore = new List< Tuple<string, Func<Fragment>>>();
			addFragment(resources.GetString(Resource.String.tab_papers),()=>new PapersView(Resource.Layout.PapersList));
			addFragment(resources.GetString(Resource.String.tab_tags),() => new TagsView(Resource.Layout.TagsView));
			addFragment(resources.GetString(Resource.String.tab_favorites),() => new FavoritesView(Resource.Layout.FavoritesView));
			addFragment(resources.GetString(Resource.String.tab_search),() => new SearchView(Resource.Layout.SearchView));
		}

		public override Fragment GetItem(int position)
		{
			if (fragmentsStore.Count > position)
				return fragmentsStore [position].Item2();
			else
				return null;
		}

		public override Java.Lang. ICharSequence GetPageTitleFormatted (int position)
		{
			if (fragmentsStore.Count > position)
				return new Java.Lang.String (fragmentsStore[position].Item1);
			else
				return null;
		}

		private void addFragment(string tabTitle,Func<Fragment> fragment)
		{
			//fragmentsStore is a tuple with fragments. First parameter is the tab title in the tap strip. Second parameter is a Func<Fragment> that returns the fragment. 
			//BaseFragment has a constructor with the viewid and the vm 
			fragmentsStore.Add (new Tuple<string, Func<Fragment >>(tabTitle, fragment));
		}

		public override int Count
		{
			get
			{
				return fragmentsStore .Count;
			}
		}


	}
}

