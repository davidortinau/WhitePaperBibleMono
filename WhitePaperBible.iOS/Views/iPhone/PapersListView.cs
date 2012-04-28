using System;
using System.Drawing;
using System.Linq;

using MonoTouch.Foundation;
using MonoTouch.UIKit;
using System.Collections.Generic;
using WhitePaperBibleCore.Models;
using WhitePaperBible.iOS.TableSource;

namespace WhitePaperBible.iOS
{
	public partial class PapersListView : UIViewController
	{
		List<PaperNode> papers;
		PapersTableSource papersTableSource;
		
		public PapersListView () : base ("PapersListView", null)
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
			
			papers = AppDelegate.papers;
			papersTableSource = new WhitePaperBible.iOS.TableSource.PapersTableSource (papers, new PapersView ());
			
			this.table.Source = papersTableSource;
			this.table.Delegate = new TableDelegate (this);
			
			this.searchBar.ShouldBeginEditing += shouldBeginEditing;
			this.searchBar.ShouldEndEditing += shouldEndEditing;
			this.searchBar.CancelButtonClicked += HandleSearchBarhandleCancelButtonClicked;
			this.searchBar.TextChanged += HandleSearchBarhandleTextChanged;
			
		}
		
		public override void ViewWillAppear(bool animated){
			base.ViewWillAppear(animated);
			
			NavigationController.SetNavigationBarHidden(false, false);
		}
		

		void HandleSearchBarhandleTextChanged (object sender, UISearchBarTextChangedEventArgs e)
		{		
			papersTableSource.papers = (from node in AppDelegate.papers
				where node.paper.title.ToLower ().Contains (searchBar.Text.ToLower ())
			          select node).ToList<PaperNode> ();
			
			table.ReloadData ();
		}

		void HandleSearchBarhandleCancelButtonClicked (object sender, EventArgs e)
		{
			resignSearch ();
			                               
		}

		public void resignSearch ()
		{
			searchBar.ResignFirstResponder ();
			this.NavigationController.SetNavigationBarHidden (false, true);
			searchBar.SetShowsCancelButton (false, true);
		}
		
		public override void ViewDidUnload ()
		{
			base.ViewDidUnload ();
			
			// Clear any references to subviews of the main view in order to
			// allow the Garbage Collector to collect them sooner.
			//
			// e.g. myOutlet.Dispose (); myOutlet = null;
			
			ReleaseDesignerOutlets ();
		}
		
		public override bool ShouldAutorotateToInterfaceOrientation (UIInterfaceOrientation toInterfaceOrientation)
		{
			// Return true for supported orientations
			return (toInterfaceOrientation != UIInterfaceOrientation.PortraitUpsideDown);
		}
		
		bool shouldBeginEditing (UISearchBar searchBar)
		{
			Console.WriteLine ("should begin editing");
			this.NavigationController.SetNavigationBarHidden (true, true);
			searchBar.SetShowsCancelButton (true, true);
			
			return true;
		}

		bool shouldEndEditing (UISearchBar searchBar)
		{
			Console.WriteLine ("should end editing");
			this.NavigationController.SetNavigationBarHidden (false, true);
			searchBar.SetShowsCancelButton (false, true);
			return true;
		}
		
		#region Delegates for the table
		
		class TableDelegate : UITableViewDelegate
		{
			PapersListView avc;
			
			public TableDelegate (PapersListView avc)
			{
				this.avc = avc;
			}
				
			public override float GetHeightForRow (UITableView tableView, NSIndexPath indexPath)
			{
				return 60f; // indexPath.Row == 0 ? 50f : 22f;
			}
	
			public override void RowSelected (UITableView tableView, NSIndexPath indexPath)
			{
				avc.resignSearch ();
				
				avc.NavigationController.NavigationBarHidden = false;
				
				Paper paper = ((PaperNode)avc.papersTableSource.papers.ElementAt (tableView.IndexPathForSelectedRow.Row)).paper;
				PaperDetailsView details = new PaperDetailsView (paper);
				
				details.HidesBottomBarWhenPushed = true;
				avc.NavigationController.PushViewController (details, true);
				
				tableView.DeselectRow (tableView.IndexPathForSelectedRow, true);
			}
			
			
			
		}
		#endregion
		
	}
}

