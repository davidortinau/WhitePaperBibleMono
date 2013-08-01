
using System.Collections.Generic;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Views;
using Android.Widget;

using WhitePaperBibleCore.Models;
using WhitePaperBible.Core.Views;
using MonkeyArms;
using System;

namespace WhitePaperBible.Android
{
	[Activity (Label = "Papers")]			
	public class PapersListActivity : ListActivity, IPapersListView, IInjectingTarget
	{
		[Inject]
		public AppModel Model;

		List<Paper> Papers;

		public event EventHandler Filter = delegate {};

		public event EventHandler OnPaperSelected = delegate {};

		public string SearchQuery {
			get;
			set;
		}

		public string SearchPlaceHolderText {
			get;
			set;
		}

		public Paper SelectedPaper {
			get;
			set;
		}

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			// Activate the action bar and display it in navigation mode.
			RequestWindowFeature(WindowFeatures.ActionBar);

			SetContentView( Resource.Layout.PapersList );

			DI.RequestMediator(this);
		}

		protected override void OnListItemClick (ListView l, View v, int position, long id)
		{
			base.OnListItemClick (l, v, position, id);

			SelectedPaper = Papers [position];

			OnPaperSelected (this, new EventArgs ());

			if(Model != null){
				Model.CurrentPaper = SelectedPaper;
			}

			var detailsView = new Intent(this, typeof(PaperDetailActivity));
			StartActivity ( detailsView );
		}

		public override bool OnCreateOptionsMenu(IMenu menu)
		{
			MenuInflater.Inflate(Resource.Menu.ActionItems,menu);

			var searchView = (SearchView)menu.FindItem(Resource.Id.menu_search).ActionView;
			searchView.QueryTextChange += OnSearchTextChanged;
			searchView.QueryTextSubmit += OnSearchTextSubmit;

			return true;
		}

		public override bool OnOptionsItemSelected(IMenuItem item)
		{
			//TODO: Handle the selection event here.
			return false;
		}

		void OnSearchTextSubmit (object sender, SearchView.QueryTextSubmitEventArgs e)
		{
			SearchQuery = e.Query;
			Filter (this, new EventArgs());
			Console.WriteLine ("OnQueryTextSubmit {0}", SearchQuery);
		}

		void OnSearchTextChanged (object sender, SearchView.QueryTextChangeEventArgs e)
		{
			SearchQuery = e.NewText;
			Filter (this, new EventArgs());
			Console.WriteLine ("OnSearchTextChanged {0}", SearchQuery);
		}


		#region IPapersListView implementation

		public void SetPapers (List<Paper> papers)
		{
			this.Papers = papers;
//			RunOnUiThread(()=>{
				ListAdapter = new PapersAdapter(this, papers);
//			});
		}

		#endregion
	}

}

