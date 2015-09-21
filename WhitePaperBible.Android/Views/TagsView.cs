
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using WhitePaperBible.Droid.Fragments;
using WhitePaperBible.Core.Views;
using MonkeyArms;
using WhitePaperBible.Droid.Adapters;
using WhitePaperBible.Core.Models;
using Newtonsoft.Json;

namespace WhitePaperBible.Droid.Views
{
	public class TagsView : BaseMediatedFragment, ITagsListView, IInjectingTarget
	{
		private View _view;

		private ListView _listView;

		private List<Tag> _tags;

		public TagsView (int layoutId) : base (layoutId)
		{
			this.Filter = new Invoker ();
			this.OnTagSelected = new Invoker ();
		}

		public override View OnCreateView (LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
		{
			_view = base.OnCreateView (inflater, container, savedInstanceState);

			_listView = _view.FindViewById<ListView>(Resource.Id.TagsList);
			_listView.ItemClick += OnRowSelected;

			return _view;
		}

		void OnRowSelected (object sender, AdapterView.ItemClickEventArgs e)
		{
			var item = _tags[e.Position];

			var courseIntent = new Intent(_view.Context, typeof(PapersByTagActivity));
			var json = JsonConvert.SerializeObject(item);
			courseIntent.PutExtra("item_json", json);
			StartActivity(courseIntent);
		}

		#region ITagsListView implementation

		public void SetTags (List<WhitePaperBible.Core.Models.Tag> tags)
		{
			_tags = tags;
			_listView.Adapter = new TagsAdapter (this.Activity, tags);
		}

		public Invoker Filter {
			get;
			private set;
		}

		public Invoker OnTagSelected {
			get;
			private set;
		}

		public string SearchPlaceHolderText {
			get;set;
		}

		public string SearchQuery {
			get;
		}

		public WhitePaperBible.Core.Models.Tag SelectedTag {
			get;
			set;
		}

		#endregion
	}
}

