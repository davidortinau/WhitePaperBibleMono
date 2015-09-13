
using Android.App;
using Android.Content;
using Android.OS;

using WhitePaperBible.Core.Views;
using MonkeyArms;
using WhitePaperBible.Android.Fragments;

namespace WhitePaperBible.Android
{
	[Activity (MainLauncher=true, NoHistory=true)]			
	public class MainActivity : Activity, ILoadingView
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
//			this.ActionBar.NavigationMode = ActionBarNavigationMode.Tabs;
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


