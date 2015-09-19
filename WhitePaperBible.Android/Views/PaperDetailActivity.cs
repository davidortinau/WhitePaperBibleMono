
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
	public class PaperDetailActivity : BaseActivityView, IPaperDetailView, IInjectingTarget
	{
		private Android.Support.V7.Widget.ShareActionProvider _shareProvider;

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
			_menu = menu;

			var share = _menu.FindItem(Resource.Id.menu_share);

			var actionProvider = MenuItemCompat.GetActionProvider (share);
			_shareProvider = actionProvider.JavaCast<Android.Support.V7.Widget.ShareActionProvider>();
			var intent = CreateIntent ();
			_shareProvider.SetShareIntent (intent);

//			_shareProvider = (ShareActionProvider)share.ActionProvider;
//			_shareProvider.SetShareIntent(CreateIntent());

			return base.OnCreateOptionsMenu(menu);
		}

		Intent CreateIntent () {  
			// prep content
			string paperTitle = Paper.title;
			string urlTitle = Paper.url_title;
			string subject = "White Paper Bible: " + paperTitle;
			string paperFullURL = "http://www.whitepaperbible.org/" + urlTitle;

			string messageCombined = subject + System.Environment.NewLine + paperFullURL + System.Environment.NewLine + Paper.ToPlainText();

			var intent = new Intent (Intent.ActionSend);
			intent.SetType ("text/plain");
			intent.PutExtra (Intent.ExtraStream, messageCombined);
			return intent;
		}

		public override bool OnOptionsItemSelected (IMenuItem item)
		{
			switch (item.ItemId) {
			case Android.Resource.Id.Home:
				{
					Finish();
					break;
				}
			case Resource.Id.menu_favorite:
				{
					ToggleFavorite.Invoke();
					showToast(Resource.String.paper_updated);
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

				var _favMenu = _menu.FindItem(Resource.Id.menu_favorite);
				if(isFavorite){
					_favMenu.SetTitle("Unfavorite");
				}else{
					_favMenu.SetTitle("Favorite");
				}
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

