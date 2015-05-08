using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;

namespace WhitePaperBible.iOSWatchKitExtension
{
	partial class PaperController : WatchKit.WKInterfaceController
	{
		public PaperController (IntPtr handle) : base (handle)
		{
		}

		public override void Awake (NSObject context)
		{
			base.Awake (context);

			var p = context as Paper;
			if(p != null){
				TitleLabel.SetText(p.Title);
			}else{
				TitleLabel.SetText("NULL BABY");
			}
		}

		public override void WillActivate ()
		{
			base.WillActivate ();
		}
	}
}
