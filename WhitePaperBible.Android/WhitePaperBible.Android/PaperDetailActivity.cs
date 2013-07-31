
using System.Collections.Generic;

using Android.App;
using Android.OS;
using WhitePaperBibleCore.Models;
using WhitePaperBible.Core.Views;
using MonkeyArms;
using Android.Views;
using Android.Widget;
using System;

namespace WhitePaperBible.Android
{
	[Activity (Label = "Paper")]			
	public class PaperDetailActivity : ListActivity, IPaperDetailView
	{
		[Inject]
		public AppModel Model;

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			// Activate the action bar and display it in navigation mode.
			RequestWindowFeature(WindowFeatures.ActionBar);

			SetContentView( Resource.Layout.PaperDetail );

			DI.RequestMediator(this);

//			if(Model.Papers != null){
//				SetPapers (Model.Papers);
//			}
		}

		public override bool OnCreateOptionsMenu(IMenu menu)
		{
			MenuInflater.Inflate(Resource.Menu.ActionItems,menu);

			return true;
		}

		public override bool OnOptionsItemSelected(IMenuItem item)
		{
			//TODO: Handle the selection event here.
			return false;
		}


		#region IPaperDetailView implementation
		public void SetPaper (Paper paper)
		{
			throw new NotImplementedException ();
		}
		#endregion
	}

}

