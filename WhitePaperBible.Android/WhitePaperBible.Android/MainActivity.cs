
using Android.App;
using Android.Content;
using Android.OS;

using WhitePaperBible.Core.Views;
using MonkeyArms;
using WhitePaperBible.Android.Fragments;

namespace WhitePaperBible.Android
{
	[Activity (Label = "Loading", NoHistory=true, MainLauncher = true)]
	public class Activity1 : Activity, ILoadingView
	{
		protected BaseFragment CurrentScreen;

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


