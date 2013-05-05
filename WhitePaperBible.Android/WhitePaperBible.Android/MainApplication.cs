using System;
using Android.App;
using Android.Runtime;
using MonkeyArms;
using WhitePaperBibleCore.Models;
using WhitePaperBibleCore.Commands;
using WhitePaperBibleCore.Invokers;
using WhitePaperBible.Android.Commands;

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

		void initMonkeyArms() {
			DI.MapCommandToInvoker <BootstrapCommand, BootstrapInvoker>();
			DI.Get<BootstrapInvoker> ().Invoke ();

			DI.Get<PapersReceivedInvoker>().Invoked += (object sender, EventArgs e) => {
				Console.WriteLine("GOT PAPERS");
			};
		}
	}
}

