
using System.Collections.Generic;

using Android.App;
using Android.OS;
using WhitePaperBible.Core.Views;
using MonkeyArms;
using Android.Views;
using Android.Widget;
using System;
using Android.Text;
using Android.Webkit;
using Android.Content;
using WhitePaperBible.Core.Models;
using Newtonsoft.Json;

namespace WhitePaperBible.Droid
{
	[Activity (Label = "")]			
	public class PaperDetailActivity : BaseActivityView, IPaperDetailView, IInjectingTarget
	{
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreateWithLayout(bundle, Resource.Layout.PaperDetail);

			ToggleFavorite = new Invoker();
			var itemStr = this.Intent.GetStringExtra("item_json");
			Paper = JsonConvert.DeserializeObject<Paper>(itemStr);
			this.setSupportActionBarTitle (Paper.title);
			this.addSupportActionBarBackButton ();
		}

		public override bool OnCreateOptionsMenu(IMenu menu)
		{
			MenuInflater.Inflate(Resource.Menu.PaperDetailsActionItems,menu);
			return base.OnCreateOptionsMenu(menu);
		}

		public override bool OnOptionsItemSelected (IMenuItem item)
		{
			switch (item.ItemId) {
			case Android.Resource.Id.Home:
				{
//					Finish();
					base.OnBackPressed();
					break;
				}
			default:
				return base.OnOptionsItemSelected (item);
			}

			return true;
		}


		#region IPaperDetailView implementation
		public void SetPaper (Paper paper, bool isFavorite, bool isOwned)
		{
			RunOnUiThread (() => {
				var paperView = (WebView)FindViewById (Resource.Id.detailsWebView);
				paperView.LoadData (paper.HtmlContent, "text/html", "utf-8");
			});

//			this.Title = paper.title;
		}

		public void SetReferences (string html)
		{
			//
		}

		public void DismissController ()
		{
			//
		}

		public Paper Paper {
			get;set;
		}

		public Invoker ToggleFavorite {
			get;set;
		}

		#endregion
	}

}

