using System;
using System.Drawing;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using System.Collections.Generic;
using WhitePaperBibleCore.Models;
using WhitePaperBible.iOS.TableSource;

namespace WhitePaperBible.iOS
{
	public partial class TagsListView : UIViewController
	{
		List<TagNode> tags;
		TagsTableSource tableSource;

		public TagsListView () : base ("TagsListView", null)
		{
		}

		public override void DidReceiveMemoryWarning ()
		{
			// Releases the view if it doesn't have a superview.
			base.DidReceiveMemoryWarning ();
			
			// Release any cached data, images, etc that aren't in use.
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			tags = AppDelegate.tags;
			tableSource = new WhitePaperBible.iOS.TableSource.TagsTableSource (tags, new TagsListView ());

			this.table.Source = tableSource;
			this.table.Delegate = new TableDelegate (this);

//			this.searchBar.ShouldBeginEditing += shouldBeginEditing;
//			this.searchBar.ShouldEndEditing += shouldEndEditing;
//			this.searchBar.CancelButtonClicked += HandleSearchBarhandleCancelButtonClicked;
//			this.searchBar.TextChanged += HandleSearchBarhandleTextChanged;

		}

		#region Delegates for the table

		class TableDelegate : UITableViewDelegate
		{
			TagsListView avc;

			public TableDelegate (TagsListView avc)
			{
				this.avc = avc;
			}

			public override float GetHeightForRow (UITableView tableView, NSIndexPath indexPath)
			{
				return 60f; // indexPath.Row == 0 ? 50f : 22f;
			}

			public override void RowSelected (UITableView tableView, NSIndexPath indexPath)
			{
//				avc.resignSearch ();

//				avc.NavigationController.NavigationBarHidden = false;
//
//				Paper paper = ((PaperNode)avc.papersTableSource.papers.ElementAt (tableView.IndexPathForSelectedRow.Row)).paper;
//				PaperDetailsView details = new PaperDetailsView (paper);
//
//				details.HidesBottomBarWhenPushed = true;
//				avc.NavigationController.PushViewController (details, true);
//
//				tableView.DeselectRow (tableView.IndexPathForSelectedRow, true);
			}



		}
		#endregion
	}
}

