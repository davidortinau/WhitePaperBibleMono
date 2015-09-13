// WARNING
//
// This file has been generated automatically by Xamarin Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace CustomElements
{
	[Register ("PaperDetailedCell")]
	partial class PaperDetailedCell
	{
		[Outlet]
		public UIKit.UILabel AuthorLabel { get; set; }

		[Outlet]
		public UIKit.UILabel TitleLabel { get; set; }

		[Outlet]
		public UIKit.UILabel ViewCountLabel { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (TitleLabel != null) {
				TitleLabel.Dispose ();
				TitleLabel = null;
			}

			if (AuthorLabel != null) {
				AuthorLabel.Dispose ();
				AuthorLabel = null;
			}

			if (ViewCountLabel != null) {
				ViewCountLabel.Dispose ();
				ViewCountLabel = null;
			}
		}
	}
}
