
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

namespace WhitePaperBible.Android
{
	[Activity (Label = "")]			
	public class PaperDetailActivity : Activity, IPaperDetailView, IInjectingTarget
	{
		#region IPaperDetailView implementation

		public void SetPaper (Paper paper, bool isFavorite, bool isOwned)
		{
			throw new NotImplementedException ();
		}

		public void SetReferences (string html)
		{
			throw new NotImplementedException ();
		}

		public void DismissController ()
		{
			throw new NotImplementedException ();
		}

		public Paper Paper {
			get {
				throw new NotImplementedException ();
			}
			set {
				throw new NotImplementedException ();
			}
		}

		public Invoker ToggleFavorite {
			get {
				throw new NotImplementedException ();
			}
		}

		#endregion

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			// Activate the action bar and display it in navigation mode.
			RequestWindowFeature(WindowFeatures.ActionBar);

			SetContentView( Resource.Layout.PaperDetail );

			DI.RequestMediator(this);

		}

		public override bool OnCreateOptionsMenu(IMenu menu)
		{
			MenuInflater.Inflate(Resource.Menu.PaperDetailsActionItems,menu);
//			menu.SetDisplayHomeAsUpEnabled(true);
			ActionBar.SetDisplayHomeAsUpEnabled (true);
//			ActionBar.DisplayOptions = ActionBarDisplayOptions.HomeAsUp | ActionBarDisplayOptions.UseLogo | ActionBarDisplayOptions.ShowHome;

			return true;
		}

		public override bool OnOptionsItemSelected(IMenuItem item)
		{

			var papersView = new Intent(this, typeof(PapersListActivity));
			StartActivity( papersView );
			return false;
		}


		#region IPaperDetailView implementation
		public void SetPaper (Paper paper)
		{
			RunOnUiThread (() => {
				var paperView = (WebView)FindViewById (Resource.Id.detailsWebView);
				paperView.LoadData (paper.HtmlContent, "text/html", "utf-8");
			});

//			this.Title = paper.title;
		}
		#endregion
	}

}

