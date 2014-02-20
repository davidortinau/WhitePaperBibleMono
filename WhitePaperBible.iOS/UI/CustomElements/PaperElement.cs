using System;
using WhitePaperBible.Core.Models;
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
		Paper paper;
		
		/// <summary>for iPhone</summary>
		public PaperElement (Paper p) : base (p.title)
		{
			paper = p;
		}
		
		
		static int count;
		public override UITableViewCell GetCell (UITableView tv)
		{
			var cell = tv.DequeueReusableCell (cellId);
			count++;
			if (cell == null)
				cell = new PaperCell (UITableViewCellStyle.Default, cellId, paper);
			else
				((PaperCell)cell).UpdateCell (paper);

			return cell;
		}

		/// <summary>Implement MT.D search on name and company properties</summary>
		public override bool Matches (string text)
		{
			return paper.title.ToLower ().IndexOf (text.ToLower ()) >= 0;
		}

		/// <summary>
		/// Behaves differently depending on iPhone or iPad
		/// </summary>
		public override void Selected (DialogViewController dvc, UITableView tableView, MonoTouch.Foundation.NSIndexPath path)
		{
			var paperDetails = new WhitePaperBible.iOS.PaperDetailsView(paper);
			paperDetails.Title = paper.title;
			dvc.ActivateController(paperDetails);		
		}
	}
}

