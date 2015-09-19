using System;
using WhitePaperBible.Core.Models;
using MonoTouch.Dialog;
using Foundation;
using UIKit;

namespace WhitePaperBible.iOS.UI.CustomElements
{
	/// <summary>
	/// Speaker element.
	/// on iPhone, pushes via MT.D
	/// on iPad, sends view to SplitViewController
	/// </summary>
	public class TagElement : Element  {
		static NSString cellId = new NSString ("TagCell");
		Tag tag;
		
		/// <summary>for iPhone</summary>
		public TagElement (Tag tag) : base (tag.name)
		{
			this.tag = tag;
		}
		
		
		static int count;
		public override UITableViewCell GetCell (UITableView tv)
		{
			var cell = tv.DequeueReusableCell (cellId);
			count++;
			if (cell == null)
				cell = new TagCell (UITableViewCellStyle.Default, cellId, tag);
			else
				((TagCell)cell).UpdateCell (tag);

			return cell;
		}

		/// <summary>Implement MT.D search on name and company properties</summary>
		public override bool Matches (string text)
		{
			return tag.name.ToLower ().IndexOf (text.ToLower ()) >= 0;
		}

		/// <summary>
		/// Behaves differently depending on iPhone or iPad
		/// </summary>
		public override void Selected (DialogViewController dvc, UITableView tableView, Foundation.NSIndexPath path)
		{
			//TODO on selected go to papers list for this tag
			var papersByTagList = new PapersByTagView ();
			papersByTagList.SelectedTag = tag;
			papersByTagList.Title = tag.name;
			dvc.ActivateController (papersByTagList);

//			var paperDetails = new WhitePaperBible.iOS.PaperDetailsView(tagNode.tag);
			//paperDetails.Title = tagNode.tag.permalink;
//			dvc.ActivateController(paperDetails);		
		}
	}
}

