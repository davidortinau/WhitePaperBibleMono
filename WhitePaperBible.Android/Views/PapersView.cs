
using System.Collections.Generic;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Views;
using Android.Widget;

using WhitePaperBible.Core.Views;
using MonkeyArms;
using System;
using WhitePaperBible.Core.Models;
using WhitePaperBible.Droid.Fragments;
using Android.Support.V7.Widget;
using Newtonsoft.Json;

namespace WhitePaperBible.Droid
{
	public class PapersView : BaseMediatedFragment, IPapersListView, IInjectingTarget
	{
		private View _view;

		private ListView _listView;

		PapersAdapter ListAdapter;

		public PapersView (int layoutId) : base (layoutId)
		{
			AddPaper = new Invoker ();	
		}

		public override View OnCreateView (LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
		{
			_view = base.OnCreateView (inflater, container, savedInstanceState);

			_listView = _view.FindViewById<ListView>(Resource.Id.PapersList);
			_listView.ItemClick += OnRowSelected;

			return _view;
		}

		public override void OnResume ()
		{
			base.OnResume ();

			if(Papers != null){
				DisplayPapers();
			}
		}

		void OnRowSelected (object sender, AdapterView.ItemClickEventArgs e)
		{
			var item = Papers[e.Position];

			var courseIntent = new Intent(_view.Context, typeof(PaperDetailActivity));
			var json = JsonConvert.SerializeObject(item);
			courseIntent.PutExtra("item_json", json);
			StartActivity(courseIntent);
		}

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
			get;
			private set;
		}

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



		#region IPapersListView implementation

		public void SetPapers (List<Paper> papers)
		{
			this.Papers = papers;

			this.Activity.RunOnUiThread (() =>  {
				DisplayPapers ();
			});
		}

		#endregion

		void DisplayPapers ()
		{
			
			if(Papers != null){
				Console.WriteLine ("How Many Papers? {0}", Papers.Count);
				_listView.Adapter = new PapersAdapter (this.Activity, Papers);
			}

		}
		  
	}

}

