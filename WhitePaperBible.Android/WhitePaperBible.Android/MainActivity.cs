
using Android.App;
using Android.Content;
using Android.OS;

using WhitePaperBible.Core.Views;
using MonkeyArms;

namespace WhitePaperBible.Android
{
	[Activity (Label = "Loading", MainLauncher = true)]
	public class Activity1 : Activity, ILoadingView
	{
		protected override void OnStart ()
		{
			base.OnStart ();
		}

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			DI.RequestMediator(this);

			// Set our view from the "main" layout resource
			SetContentView (Resource.Layout.Main);

		}

		#region ILoadingView implementation
		public void OnLoadingComplete ()
		{
			var papersView = new Intent(this, typeof(PapersListActivity));
			StartActivity( papersView );

		}
		#endregion
	}
}


