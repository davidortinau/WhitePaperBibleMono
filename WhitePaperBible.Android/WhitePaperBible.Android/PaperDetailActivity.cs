
using System.Collections.Generic;

using Android.App;
using Android.OS;
using WhitePaperBibleCore.Models;
using WhitePaperBible.Core.Views;
using MonkeyArms;
using Android.Views;
using Android.Widget;
using System;
using Android.Text;
using Android.Webkit;

namespace WhitePaperBible.Android
{
	[Activity (Label = "Paper")]			
	public class PaperDetailActivity : Activity, IPaperDetailView, IInjectingTarget
	{
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
			// TODO how to navigate back? Or is it just start the list activity?

			return false;
		}


		#region IPaperDetailView implementation
		public void SetPaper (Paper paper)
		{
			var paperView = (WebView)FindViewById (Resource.Id.detailsWebView);
			paperView.LoadData(paper.HtmlContent, "text/html", "utf-8");
		}
		#endregion
	}

}

