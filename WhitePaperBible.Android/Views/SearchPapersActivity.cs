
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
			MenuInflater.Inflate(Resource.Menu.ActionItems, menu);

			_menu = menu;

//			SearchManager searchManager = (SearchManager)
//				GetSystemService(Android.Content.Context.SearchService);
//			var searchMenuItem = _menu.FindItem(Resource.Id.search);
//			var searchView = (SearchView) searchMenuItem.ActionView;
//
//			searchView.SetSearchableInfo(searchManager.
//				GetSearchableInfo(ComponentName));
//			searchView.SubmitButtonEnabled = true;
//			searchView.SetOnQueryTextListener(this);

//			var share = _menu.FindItem(Resource.Id.menu_share);
//
//			var actionProvider = MenuItemCompat.GetActionProvider (share);
//			_shareProvider = actionProvider.JavaCast<Android.Support.V7.Widget.ShareActionProvider>();
//			var intent = CreateIntent ();
//			_shareProvider.SetShareIntent (intent);


			var searchManager = (SearchManager) GetSystemService(SearchService);
			IMenuItem item = menu.FindItem(Resource.Id.action_search);
			var searchItem = MenuItemCompat.GetActionView(item);//(Android.Support.V7.Widget.SearchView) searchItem.ActionView;
			var searchView = searchItem.JavaCast<Android.Support.V7.Widget.SearchView>();
			searchView.SetSearchableInfo(searchManager.GetSearchableInfo(ComponentName));


//			searchView.SetOnSuggestionListener(new SuggestionListener(searchView.SuggestionsAdapter, this, searchItem));
			searchView.SetOnQueryTextListener(new OnQueryTextListener(this));
			searchView.SetIconifiedByDefault(false);// should start it open
			searchView.RequestFocus();

			return base.OnCreateOptionsMenu(menu);
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

