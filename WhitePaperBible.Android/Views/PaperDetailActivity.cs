
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
using Cocosw.BottomSheetActions;

namespace WhitePaperBible.Droid
{
	[Activity (Label = "")]			
	public class PaperDetailActivity : BaseActivityView, IPaperDetailView, IInjectingTarget, IDialogInterfaceOnClickListener
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
			case Resource.Id.menu_favorite:
				{
					ToggleFavorite.Invoke();
					showToast(Resource.String.paper_updated);
					break;
				}
			case Resource.Id.menu_share:
				{
					ShowBottomSheet();
					break;
				}
			default:
				return base.OnOptionsItemSelected (item);
			}

			return true;
		}

		void ShowBottomSheet ()
		{
			BottomSheet sheet = new BottomSheet.Builder(this)
				.Title("Share")
				.Listener((IDialogInterfaceOnClickListener)this)
				.Sheet(Resource.Menu.share)
				.Build();

			sheet.Show();


		}

		public void OnClick (IDialogInterface dialog, int which)
		{
			Console.WriteLine("clicked {0}", which);

			string paperTitle = Paper.title;
			string urlTitle = Paper.url_title;
			string subject = "White Paper Bible: " + paperTitle;
			string paperFullURL = "http://www.whitepaperbible.org/" + urlTitle;

			string messageCombined = subject + System.Environment.NewLine + paperFullURL + System.Environment.NewLine + Paper.ToPlainText();

			switch(which){
			case Resource.Id.menu_email:
				{
					var email = new Intent (Android.Content.Intent.ActionSend);
//					email.PutExtra(Android.Content.Intent.ExtraEmail, new string[]{"person1@xamarin.com", "person2@xamrin.com"});
//					email.PutExtra(Android.Content.Intent.ExtraCc, new string[]{"person3@xamarin.com"});
					email.PutExtra(Android.Content.Intent.ExtraSubject, paperTitle);
					email.PutExtra(Android.Content.Intent.ExtraText, messageCombined);
					email.SetType("message/rfc822");
					StartActivity(email);
					break;
				}
			case Resource.Id.menu_facebook:
				{

					break;
				}
			case Resource.Id.menu_twitter:
				{

					break;
				}
			case Resource.Id.menu_sms:
				{
					var smsUri = Android.Net.Uri.Parse("smsto:");
					var smsIntent = new Intent (Intent.ActionSendto, smsUri);
					smsIntent.PutExtra ("sms_body", string.Format("{0} {1}", paperTitle, paperFullURL));  
					StartActivity (smsIntent);
					break;
				}
			}
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

