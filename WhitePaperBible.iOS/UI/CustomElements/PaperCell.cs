using System;
using Foundation;
using UIKit;
using MonoTouch.Dialog;
using CoreGraphics;
using WhitePaperBible.Core.Models;

namespace WhitePaperBible.iOS.UI.CustomElements
{
	public class PaperCell : UITableViewCell {
		static UIFont bigFont = UIFont.FromName ("Helvetica", AppDelegate.Font10_5pt);
		static UIFont smallFont = UIFont.FromName ("Helvetica-Light", AppDelegate.Font10pt);
		UILabel titleLabel, descriptionLabel;
		UIButton button;
		UIImageView locationImageView;
		Paper paper;

		const int padding = 13;

		public PaperCell (UITableViewCellStyle style, NSString ident, Paper paperNode) : base (style, ident)
		{
			SelectionStyle = UITableViewCellSelectionStyle.Blue;

			titleLabel = new UILabel () {
				TextAlignment = UITextAlignment.Left,
				BackgroundColor = UIColor.FromWhiteAlpha (0f, 0f)
			};

			descriptionLabel = new UILabel () {
				TextAlignment = UITextAlignment.Left,
				Font = smallFont,
				TextColor = UIColor.DarkGray,
				BackgroundColor = UIColor.FromWhiteAlpha (0f, 0f)
			};

			UpdateCell (paperNode);

			ContentView.Add (titleLabel);
			ContentView.Add (descriptionLabel);
		}

		public void UpdateCell (Paper p)
		{
			paper = p;

			titleLabel.Font = bigFont;
			titleLabel.Text = paper.title;

			descriptionLabel.Text = paper.description;
		}

//		void UpdateImage (bool selected)
//		{
//			if (selected)
//				button.SetImage (favorited, UIControlState.Normal);
//			else
//				button.SetImage (favorite, UIControlState.Normal);
//		}
//
//		bool ToggleFavorite ()
//		{
//			if (FavoritesManager.IsFavorite (paperNode.Key)) {
//				FavoritesManager.RemoveFavoriteSession (paperNode.Key);
//				return false;
//			} else {
//				var fav = new Favorite {SessionID = paperNode.ID, SessionKey = paperNode.Key};
//				FavoritesManager.AddFavoriteSession (fav);
//				return true;
//			}
//		}
//
		/// <summary>
		/// Used in ViewWillAppear (SessionsScreen, SessionDayScheduleScreen)
		/// to sync favorite-stars that have changed in other views
		/// </summary>
//		public void UpdateFavorite() {
//			UpdateImage (FavoritesManager.IsFavorite (paperNode.Key)) ;
//		}

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

			CGSize size = UIKit.UIStringDrawing.StringSize(titleLabel.Text
						, titleLabel.Font
				, new CGSize(titleFrame.Width, 400));
			if (size.Height > 27) {
				titleAdjustment = 27;
				titleFrame.Height = titleFrame.Height + size.Height;
				titleLabel.Lines = 2;
			}
			else titleLabel.Lines = 1;

			titleLabel.Frame = titleFrame;

//			var locationImagePadding = 18 + 5;
//			if (descriptionLabel.Text == "") {
//				locationImagePadding = 0;
//				locationImageView.Alpha = 0f;
//			} else
//				locationImageView.Alpha = 1f;
//
//			var companyFrame = full;
//			companyFrame.X = padding + locationImagePadding;	// image is
//			var bottomRowY = 15 + 23 + titleAdjustment;
//			companyFrame.Y = bottomRowY;
//			companyFrame.Height = 14; // 12 -> 14
//			companyFrame.Width = titleFrame.Width - locationImagePadding;
//			descriptionLabel.Frame = companyFrame;
//
//			locationImageView.Frame = new CGRect (padding, bottomRowY, 18, 16);
//
//			button.Frame = new CGRect (full.Width-buttonSpace //-5
//				, titleAdjustment / 2, buttonSpace, buttonSpace); // 10 +
		}
	}
}
