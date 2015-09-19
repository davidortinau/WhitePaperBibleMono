
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

namespace WhitePaperBible.Droid
{
	[Activity (Label = "")]			
	public class PaperDetailActivity : Activity, IPaperDetailView, IInjectingTarget
	{
		#region IPaperDetailView implementation

		public void SetPaper (Paper paper, bool isFavorite, bool isOwned)
		{
			//
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

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			// Activate the action bar and display it in navigation mode.
			RequestWindowFeature(WindowFeatures.ActionBar);

			SetContentView( Resource.Layout.PaperDetail );

			ToggleFavorite = new Invoker();

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

			var papersView = new Intent(this, typeof(PapersView));
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

