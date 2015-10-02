
using System.Collections.Generic;

using Android.App;
using Android.OS;
using WhitePaperBible.Core.Views;
using MonkeyArms;
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
using Android.Support.V7.Widget;
using Android.Widget;
using Rendr.Droid.Components;

namespace WhitePaperBible.Droid
{
	[Activity (Label = "")]		
	[IntentFilter(new string[]{"android.intent.action.SEARCH"})]
	[MetaData(("android.app.searchable"), Resource = "@xml/searchable")]
	public class SearchPapersActivity : BaseActivityView, ISearchPapersView, IInjectingTarget
	{
		private ListView _listView;

		private List<Paper> _papers;

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreateWithLayout(bundle, Resource.Layout.SearchPapersView);

			this.setSupportActionBarTitle ("Search");
			this.addSupportActionBarBackButton ();

			_listView = FindViewById<ListView>(Resource.Id.PapersList);
			_listView.ItemClick += OnRowClicked;

			OnPaperSelected = new Invoker();
			Filter = new Invoker();

		}

		protected override void OnResume ()
		{
			base.OnResume ();
		}

		void OnRowClicked (object sender, AdapterView.ItemClickEventArgs e)
		{
			var item = ListAdapter.FilteredItems[e.Position];

			var courseIntent = new Intent(this, typeof(PaperDetailActivity));
			var json = JsonConvert.SerializeObject(item);
			courseIntent.PutExtra("item_json", json);
			StartActivity(courseIntent);
		}

		class OnQueryTextListener: Java.Lang.Object, Android.Support.V7.Widget.SearchView.IOnQueryTextListener
		{
			private readonly SearchPapersActivity _activity;

			public OnQueryTextListener(SearchPapersActivity activity)
			{
				_activity = activity;
			}

			public bool OnQueryTextChange(string newText)
			{
				_activity.ListAdapter.Filter.InvokeFilter(newText);
				return true;
			}

			public bool OnQueryTextSubmit(string query)
			{
//				if (String.IsNullOrEmpty(query))
//					return false; //let the default happen
//
//				Intent searchIntent = new Intent(_activity, typeof(search.SearchResults));
//				searchIntent.SetAction(Intent.ActionSearch); //currently not necessary to set because SearchResults doesn't care, but let's be as close to the default as possible
//				searchIntent.PutExtra(SearchManager.Query, query);
//				//forward appTask:
//				_activity.ToIn(searchIntent);
//
//				_activity.StartActivityForResult(searchIntent, 0);
//
				return true;
			}
		}

		public override bool OnCreateOptionsMenu(IMenu menu)
		{
            _menu = menu;
			MenuInflater.Inflate(Resource.Menu.ActionItems, menu);
            SetupSearch(menu);
            return base.OnCreateOptionsMenu(menu);
		}

        public override bool OnPrepareOptionsMenu (IMenu menu)
        {
            
            searchView.SetIconifiedByDefault(false);// should start it open
            searchView.RequestFocus();
            return base.OnPrepareOptionsMenu (menu);
        }

        Android.Support.V7.Widget.SearchView searchView;

        void SetupSearch(IMenu menu)
        {
            var key = Resources.GetText (Resource.String.icon_search);
            var icon = new TextDrawable(key, this);
            var searchItem = menu.FindItem(Resource.Id.action_search);
            searchItem.SetIcon(icon);


            var searchManager = (SearchManager) GetSystemService(SearchService);
            var searchActionView = MenuItemCompat.GetActionView(searchItem);//(Android.Support.V7.Widget.SearchView) searchItem.ActionView;

            searchView = searchActionView.JavaCast<Android.Support.V7.Widget.SearchView> ();
            searchView.SetSearchableInfo(searchManager.GetSearchableInfo(ComponentName));
            searchView.SetOnQueryTextListener(new OnQueryTextListener(this));
            searchView.SetIconifiedByDefault(false);// should start it open


            searchItem.ExpandActionView();

//            int searchImgId = Resource.Id.search_button; // I used the explicit layout ID of searchview's ImageView
//            ImageView v = (ImageView) searchView.FindViewById(searchImgId);
//            v.Visibility = ViewStates.Gone;
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

		PapersAdapter ListAdapter {
			get;
			set;
		}


		#region IPapersByTagListView implementation
		public void SetPapers (List<Paper> papers)
		{
			_papers = papers;
			_listView.Adapter = ListAdapter = new PapersAdapter(this, papers);
		}

		public Invoker Filter {
			get;
			private set;
		}

		public Invoker OnPaperSelected {
			get;
			private set;
		}

		public Paper SelectedPaper {
			get;set;
		}
		public Tag SelectedTag {
			get;set;
		}
		#endregion

		public void AddPaperEditView ()
		{
			throw new NotImplementedException ();
		}

		public void PromptForLogin ()
		{
			throw new NotImplementedException ();
		}

		public void DismissLoginPrompt ()
		{
			throw new NotImplementedException ();
		}

		public Invoker AddPaper {
			get {
				throw new NotImplementedException ();
			}
		}

		public string SearchPlaceHolderText {
			get;set;
		}

		public string SearchQuery {
			get {
				return "";
			}
		}
	}

}

