using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;

namespace WhitePaperBible.iOSWatchKitExtension
{
	partial class PaperRow : NSObject
	{
		public PaperRow (IntPtr handle) : base (handle)
		{
		}

		public void SetData(string title){
			TitleLabel.SetText(title);
		}
	}
}
