using System;
using WhitePaperBibleCore.Models;
using System.Collections.Generic;
using MonoTouch.UIKit;
using MonoTouch.Foundation;

namespace WhitePaperBible.iOS.TableSource
{
	public class PapersTableSource: UITableViewSource {
		IList<PaperNode> papers;
		PapersView view;
		
		static NSString cellId = new NSString("PaperCell");

		public PapersTableSource (List<PaperNode> papers, PapersView view)
		{
			this.papers = papers;
			this.view = view;
		}

		public override UITableViewCell GetCell (UITableView tableView, MonoTouch.Foundation.NSIndexPath indexPath)
		{
			var paperNode = papers[indexPath.Row] as PaperNode;
			var cell = tableView.DequeueReusableCell(cellId);
			if(cell == null) 
				cell = new UITableViewCell (UITableViewCellStyle.Default, cellId);
			
			cell.TextLabel.Text = paperNode.paper.title;
			return cell;
		}

		public override float GetHeightForRow (UITableView tableView, NSIndexPath indexPath)
		{
			return 40f;
		}

		public override int RowsInSection (UITableView tableview, int section)
		{
			return papers.Count;
		}
		
//		public override string TitleForHeader (UITableView tableView, int section)
//		{
//			return "Papers";
//		}	

		public override void RowSelected (UITableView tableView, MonoTouch.Foundation.NSIndexPath indexPath)
		{
			var paperNode = papers[indexPath.Row] as PaperNode;
			//ConsoleD.WriteLine("PapersTableSource.RowSelected");			
			//view.SelectSession(session); // TODO implement select method
			if (AppDelegate.IsPhone) tableView.DeselectRow (indexPath, true);
		}
	}
}

