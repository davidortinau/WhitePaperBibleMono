using System;
using WhitePaperBible.Core.Models;
using MonoTouch.Dialog;
using Foundation;
using UIKit;
using CustomElements;

namespace WhitePaperBible.iOS.UI.CustomElements
{
	/// <summary>
	/// Speaker element.
	/// on iPhone, pushes via MT.D
	/// on iPad, sends view to SplitViewController
	/// </summary>
	public class PaperElement : Element  {
		static NSString cellId = new NSString ("PaperDetailedCell");
		Paper paper;

		public event Action Tapped;
		
		/// <summary>for iPhone</summary>
		public PaperElement (Paper p, Action tapped) : base (p.title)
		{
			paper = p;
			Tapped = tapped;
		}
		
		
		static int count;
		public override UITableViewCell GetCell (UITableView tv)
		{
			var cell = tv.DequeueReusableCell (cellId) as PaperDetailedCell;
			count++;
			if (cell == null)
				cell = PaperDetailedCell.Create();
//			else
//				((PaperCell)cell).UpdateCell (paper);

			cell.TitleLabel.Text = paper.title;
			cell.AuthorLabel.Text = string.Format("by: {0}", paper.Author.Name);
			cell.ViewCountLabel.Text = string.Empty;// string.Format("{0} views", paper.view_count);
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
		public override void Selected (DialogViewController dvc, UITableView tableView, Foundation.NSIndexPath path)
		{
//			var paperDetails = new WhitePaperBible.iOS.PaperDetailsView(paper);
//			paperDetails.Title = paper.title;
//			dvc.ActivateController (paperDetails);

			if (Tapped != null)
				Tapped ();
			tableView.DeselectRow (path, true);
		}
	}
}

