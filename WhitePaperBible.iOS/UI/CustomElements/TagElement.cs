using System;
using WhitePaperBibleCore.Models;
using MonoTouch.Dialog;
using MonoTouch.Foundation;
using MonoTouch.UIKit;

namespace WhitePaperBible.iOS.UI.CustomElements
{
	/// <summary>
	/// Speaker element.
	/// on iPhone, pushes via MT.D
	/// on iPad, sends view to SplitViewController
	/// </summary>
	public class TagElement : Element  {
		static NSString cellId = new NSString ("TagCell");
		TagNode tagNode;
		
		/// <summary>for iPhone</summary>
		public TagElement (TagNode node) : base (node.tag.permalink)
		{
			tagNode = node;
		}
		
		
		static int count;
		public override UITableViewCell GetCell (UITableView tv)
		{
			var cell = tv.DequeueReusableCell (cellId);
			count++;
			if (cell == null)
				cell = new TagCell (UITableViewCellStyle.Default, cellId, tagNode);
			else
				((TagCell)cell).UpdateCell (tagNode);

			return cell;
		}

		/// <summary>Implement MT.D search on name and company properties</summary>
		public override bool Matches (string text)
		{
			return tagNode.tag.permalink.ToLower ().IndexOf (text.ToLower ()) >= 0;
		}

		/// <summary>
		/// Behaves differently depending on iPhone or iPad
		/// </summary>
		public override void Selected (DialogViewController dvc, UITableView tableView, MonoTouch.Foundation.NSIndexPath path)
		{
			//TODO on selected go to papers list for this tag

//			var paperDetails = new WhitePaperBible.iOS.PaperDetailsView(tagNode.tag);
			//paperDetails.Title = tagNode.tag.permalink;
//			dvc.ActivateController(paperDetails);		
		}
	}
}

