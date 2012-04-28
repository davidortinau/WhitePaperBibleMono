// WARNING
//
// This file has been generated automatically by MonoDevelop to store outlets and
// actions made in the Xcode designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using MonoTouch.Foundation;

namespace WhitePaperBible.iOS
{
	[Register ("PapersListView")]
	partial class PapersListView
	{
		[Outlet]
		MonoTouch.UIKit.UISearchBar searchBar { get; set; }

		[Outlet]
		MonoTouch.UIKit.UITableView table { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (searchBar != null) {
				searchBar.Dispose ();
				searchBar = null;
			}

			if (table != null) {
				table.Dispose ();
				table = null;
			}
		}
	}
}
