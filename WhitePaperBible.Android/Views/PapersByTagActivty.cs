
using System.Collections.Generic;

using Android.App;
using Android.OS;
using WhitePaperBible.Core.Views;
using MonkeyArms;
using Android.Widget;
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

namespace WhitePaperBible.Droid
{
	[Activity (Label = "")]			
	public class PapersByTagActivity : BaseActivityView, IPapersByTagListView, IInjectingTarget
	{
		private ListView _listView;

		private List<Paper> _papers;

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreateWithLayout(bundle, Resource.Layout.PapersByTagList);

			var itemStr = this.Intent.GetStringExtra("item_json");
			SelectedTag = JsonConvert.DeserializeObject<Tag>(itemStr);

			this.setSupportActionBarTitle (SelectedTag.name);
			this.addSupportActionBarBackButton ();

			_listView = FindViewById<ListView>(Resource.Id.PapersByTagList);
			_listView.ItemClick += OnRowClicked;

			OnPaperSelected = new Invoker();
			Filter = new Invoker();

		}

		void OnRowClicked (object sender, AdapterView.ItemClickEventArgs e)
		{
			var item = _papers[e.Position];

			var courseIntent = new Intent(this, typeof(PaperDetailActivity));
			var json = JsonConvert.SerializeObject(item);
			courseIntent.PutExtra("item_json", json);
			StartActivity(courseIntent);
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



		#region IPapersByTagListView implementation
		public void SetPapers (List<Paper> papers)
		{
			_papers = papers;
			_listView.Adapter = new PapersAdapter(this, papers);
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
	}

}

