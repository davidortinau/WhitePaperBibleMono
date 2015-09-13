
using System;

using Foundation;
using UIKit;

namespace CustomElements
{
	public partial class PaperDetailedCell : UITableViewCell
	{
		public static readonly UINib Nib = UINib.FromName ("PaperDetailedCell", NSBundle.MainBundle);
		public static readonly NSString Key = new NSString ("PaperDetailedCell");

		public PaperDetailedCell (IntPtr handle) : base (handle)
		{
		}

		public static PaperDetailedCell Create ()
		{
			return (PaperDetailedCell)Nib.Instantiate (null, null) [0];
		}
	}
}

