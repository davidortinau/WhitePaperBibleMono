using System;
using System.Linq;

using WhitePaperBibleCore.Models;
using System.Collections.Generic;
using MonoTouch.Dialog;
using MonoTouch.Foundation;
using MonoTouch.UIKit;

namespace WhitePaperBible.iOS.TableSource
{
	public class PapersTableSource: UITableViewSource {
		public IList<PaperNode> papers;
		PapersView view;
		
		static NSString cellId = new NSString("PaperElement");

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
		
//		public override string[] SectionIndexTitles (UITableView tableView)
//		{
//			var sit = from node in papers
//                    group node by (node.paper.title[0].ToString().ToUpper()) into alpha
//						orderby alpha.Key
//						select alpha.Key;
//			return sit.ToArray();
//		}

		public override float GetHeightForRow (UITableView tableView, NSIndexPath indexPath)
		{
			return 60f;
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

