using System;
using Android.App;
using Android.Runtime;
using MonkeyArms;
using WhitePaperBible.Core.Commands;
using WhitePaperBible.Core.Invokers;
using WhitePaperBible.Android.Services;
using WhitePaperBible.Core.Services;

namespace WhitePaperBible.Android
{
	[Application]
	public class MainApplication : Application
	{
		public MainApplication (IntPtr handle, JniHandleOwnership transfer)
			: base (handle, transfer)
		{
		}

		public override void OnCreate ()
		{
			base.OnCreate ();
			initMonkeyArms ();
		}

		void initMonkeyArms ()
		{
			DI.MapClassToInterface<WebClient, IJSONWebClient> ();
			DI.MapCommandToInvoker <BootstrapCommand, BootstrapInvoker> ().Invoke ();
		}
	}
}

