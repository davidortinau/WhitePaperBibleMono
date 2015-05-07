using System;

using WatchKit;
using Foundation;

namespace WhitePaperBible.iOSWatchKitExtension
{
	public partial class GlanceController : WKInterfaceController
	{
		public GlanceController (IntPtr handle) : base (handle)
		{
		}

		public override void Awake (NSObject context)
		{
			base.Awake (context);

			// Configure interface objects here.
			Console.WriteLine ("{0} awake with context", this);

			ReferenceLabel.SetText("Hebrews 11:16");
			VerseLabel.SetText("And without faith it is impossible to please him, for whoever would draw near to God must believe that he exists and that he rewards those who seek him.");
		}

		public override void WillActivate ()
		{
			// This method is called when the watch view controller is about to be visible to the user.
			Console.WriteLine ("{0} will activate", this);
		}

		public override void DidDeactivate ()
		{
			// This method is called when the watch view controller is no longer visible to the user.
			Console.WriteLine ("{0} did deactivate", this);
		}
	}
}

