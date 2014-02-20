using System;
using System.Linq;
using WhitePaperBible.Core.Models;
using System.Collections.Generic;
using MonoTouch.Dialog;
using MonoTouch.Foundation;
using MonoTouch.UIKit;

namespace WhitePaperBible.iOS.TableSource
{
	public class TagsTableSource: UITableViewSource
	{
		public IList<TagNode> tags;
		TagsListView view;
		static NSString cellId = new NSString ("TagElement");

		public TagsTableSource (List<TagNode> tags, TagsListView view)
		{
			this.tags = tags;
			this.view = view;
		}

		public override UITableViewCell GetCell (UITableView tableView, MonoTouch.Foundation.NSIndexPath indexPath)
		{
			var tagNode = tags [indexPath.Row] as TagNode;
			var cell = tableView.DequeueReusableCell (cellId);
			if (cell == null)
				cell = new UITableViewCell (UITableViewCellStyle.Default, cellId);
			
			cell.TextLabel.Text = tagNode.tag.ToString ();
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
			return (tags != null) ? tags.Count : 0;
		}
		//		public override string TitleForHeader (UITableView tableView, int section)
		//		{
		//			return "Papers";
		//		}
		public override void RowSelected (UITableView tableView, MonoTouch.Foundation.NSIndexPath indexPath)
		{
			var tagNode = tags [indexPath.Row] as TagNode;
			if (AppDelegate.IsPhone)
				tableView.DeselectRow (indexPath, true);
		}
	}
}

