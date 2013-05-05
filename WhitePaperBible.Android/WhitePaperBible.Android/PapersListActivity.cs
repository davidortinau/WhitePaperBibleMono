
using System.Collections.Generic;

using Android.App;
using Android.OS;
using WhitePaperBibleCore.Models;
using WhitePaperBible.Core.Views;
using MonkeyArms;

namespace WhitePaperBible.Android
{
	[Activity (Label = "Papers")]			
	public class PapersListActivity : ListActivity, IPapersListView, IMediatorTarget
	{
		[Inject]
		public AppModel Model;

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			SetContentView( Resource.Layout.PapersList );

//			if(Model.Papers != null){
//				SetPapers (Model.Papers);
//			}
		}

		#region IPapersListView implementation

		public void SetPapers (List<Paper> papers)
		{
//			RunOnUiThread(()=>{
				ListAdapter = new PapersAdapter(this, papers);
//			});
		}

		#endregion
	}

}

