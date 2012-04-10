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
	public class PaperElement : Element  {
		static NSString cellId = new NSString ("PaperCell");
		PaperNode paperNode;
		
		/// <summary>for iPhone</summary>
		public PaperElement (PaperNode node) : base (node.paper.title)
		{
			paperNode = node;
		}
		
		
		static int count;
		public override UITableViewCell GetCell (UITableView tv)
		{
			var cell = tv.DequeueReusableCell (cellId);
			count++;
			if (cell == null)
				cell = new PaperCell (UITableViewCellStyle.Default, cellId, paperNode);
			else
				((PaperCell)cell).UpdateCell (paperNode);

			return cell;
		}

		/// <summary>Implement MT.D search on name and company properties</summary>
		public override bool Matches (string text)
		{
			return paperNode.paper.title.ToLower ().IndexOf (text.ToLower ()) >= 0;
		}

		/// <summary>
		/// Behaves differently depending on iPhone or iPad
		/// </summary>
		public override void Selected (DialogViewController dvc, UITableView tableView, MonoTouch.Foundation.NSIndexPath path)
		{
			var paperDetails = new WhitePaperBible.iOS.PaperDetailsView(paperNode.paper);
			paperDetails.Title = paperNode.paper.title;
			dvc.ActivateController(paperDetails);		
		}
	}
}

