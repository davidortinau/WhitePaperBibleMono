using System;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using MonoTouch.Dialog;
using System.Drawing;
using WhitePaperBible.Core.Models;

namespace WhitePaperBible.iOS.UI.CustomElements
{
	public class TagCell : UITableViewCell {
		static UIFont bigFont = UIFont.FromName ("Helvetica", AppDelegate.Font10_5pt);
		static UIFont smallFont = UIFont.FromName ("Helvetica-Light", AppDelegate.Font10pt);
		UILabel titleLabel, descriptionLabel;
		UIButton button;
		UIImageView locationImageView;
		TagNode tagNode;
		
		const int padding = 13;
		
		public TagCell (UITableViewCellStyle style, NSString ident, TagNode tagNode) : base (style, ident)
		{
			SelectionStyle = UITableViewCellSelectionStyle.Blue;
			
			titleLabel = new UILabel () {
				TextAlignment = UITextAlignment.Left,
				BackgroundColor = UIColor.FromWhiteAlpha (0f, 0f)
			};
			
//			descriptionLabel = new UILabel () {
//				TextAlignment = UITextAlignment.Left,
//				Font = smallFont,
//				TextColor = UIColor.DarkGray,
//				BackgroundColor = UIColor.FromWhiteAlpha (0f, 0f)
//			};

			UpdateCell (tagNode);
			
			ContentView.Add (titleLabel);
//			ContentView.Add (descriptionLabel);
		}
		
		public void UpdateCell (TagNode tagNode)
		{
			tagNode = tagNode;
			
			titleLabel.Font = bigFont;
			titleLabel.Text = tagNode.tag.ToString();
			
//			descriptionLabel.Text = tagNode.paper.description;
		}

		public override void LayoutSubviews ()
		{
			base.LayoutSubviews ();
			var full = ContentView.Bounds;
			var titleAdjustment = 0;			
			var titleFrame = full;

			titleFrame.X = padding;
			titleFrame.Y = 12; //15 ?
			titleFrame.Height = 25;
			titleFrame.Width -= (padding); // +10

			SizeF size = titleLabel.StringSize (titleLabel.Text
						, titleLabel.Font
						, new SizeF(titleFrame.Width, 400));
			if (size.Height > 27) {
				titleAdjustment = 27;
				titleFrame.Height = titleFrame.Height + titleAdjustment; //size.Height;
				titleLabel.Lines = 2;
			}
			else titleLabel.Lines = 1;

			titleLabel.Frame = titleFrame;
		}
	}
}

