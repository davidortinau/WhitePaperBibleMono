using System;
using UIKit;
using CoreGraphics;
using CoreGraphics;
using WhitePaperBible.iOS.UI;
using WhitePaperBible.iOS.Managers;
using Foundation;

namespace WhitePaperBible.iOS.Utils
{
	public static class ApplyStyle
	{
		public const int OrangeHex = 0xf15a29;

		public const int DarkGreyHex = 0x50575d;

		public static void NavigationBar (UINavigationBar navigationBar)
		{
			navigationBar.BarStyle = UIBarStyle.Black;
			navigationBar.BarTintColor = ColorFromHex (OrangeHex);
			navigationBar.Translucent = false;
			navigationBar.TintColor = UIColor.White;
		}

		public static void TabBar (UITabBar tabBar)
		{
			tabBar.Translucent = false;
			tabBar.BarTintColor = ColorFromHex (OrangeHex);
			tabBar.TintColor = UIColor.White;
			UITabBarItem.Appearance.SetTitleTextAttributes (new UITextAttributes (){ TextColor = UIColor.White }, UIControlState.Normal);
		}

		public static void SegementedControl (UISegmentedControl control)
		{
			control.TintColor = UIColor.White;
			control.SetTitleTextAttributes (new UITextAttributes (){ Font = UIFont.SystemFontOfSize (14) }, UIControlState.Normal);
		}

		public static void Switch (UISwitch control)
		{
			control.OnTintColor = ColorFromHex (OrangeHex);
		}

		public static void OrangeContainer (UIView view)
		{
			view.BackgroundColor = ColorFromHex (OrangeHex);
		}

		public static void TableView (UITableView table)
		{
			table.BackgroundColor = ColorFromHex (0xf9f9f9);
		}

		public static void TableCell (UITableViewCell cell)
		{
			cell.BackgroundColor = UIColor.White;
		}

		public static void TableCellTextLabel (UILabel label)
		{
			label.TextColor = ColorFromHex (0x53555a);
			label.Font = UIFont.SystemFontOfSize (16);

		}

		public static void LoginView (LoginViewController view)
		{
			view.RegisterButton.SetTitle (ResourceManager.GetString ("register"), UIControlState.Normal);
			view.RegisterButton.BackgroundColor = UIColor.Clear;
			view.RegisterButton.SetTitleColor (UIColor.White, UIControlState.Normal);
			view.RegisterButton.Font = UIFont.FromName ("Helvetica", 12);
			view.RegisterButton.SetTitleColor (AppStyles.DarkGray, UIControlState.Highlighted); 

			view.LoginButton.BackgroundColor = AppStyles.Red;
			view.LoginButton.Font = UIFont.FromName ("Helvetica", 21);
			view.LoginButton.SetTitleColor (UIColor.White, UIControlState.Normal);

			view.CancelButton.BackgroundColor = AppStyles.Gray;
			view.CancelButton.Font = UIFont.FromName ("Helvetica", 21);
			view.CancelButton.SetTitleColor (UIColor.White, UIControlState.Normal);

			view.UsernameInput.BackgroundColor = UIColor.Clear;
			view.UsernameInput.BorderStyle = UITextBorderStyle.None;
			view.UsernameInput.VerticalAlignment = UIControlContentVerticalAlignment.Center;
			view.UsernameInput.TextColor = UIColor.White;
			view.UsernameInput.TextAlignment = UITextAlignment.Right;
			view.UsernameInput.Placeholder = ResourceManager.GetString ("emailPlaceholder");
			view.UsernameInput.ReturnKeyType = UIReturnKeyType.Next;
			view.UsernameInput.AttributedPlaceholder = new NSAttributedString (ResourceManager.GetString ("emailPlaceholder"), null, UIColor.White);

			view.PasswordInput.SecureTextEntry = true;
			view.PasswordInput.BackgroundColor = UIColor.Clear;
			view.PasswordInput.BorderStyle = UITextBorderStyle.None;
			view.PasswordInput.VerticalAlignment = UIControlContentVerticalAlignment.Center;
			view.PasswordInput.TextColor = UIColor.White;
			view.PasswordInput.TextAlignment = UITextAlignment.Right;
			view.PasswordInput.Placeholder = ResourceManager.GetString ("password");
			view.PasswordInput.ReturnKeyType = UIReturnKeyType.Done;
			view.PasswordInput.AttributedPlaceholder = new NSAttributedString (ResourceManager.GetString ("password"), null, UIColor.White);

		}

		public static void LoginRequired (LoginRequiredController view)
		{
			view.DescriptionLabel.Text = ResourceManager.GetString ("loginRequired");
			view.DescriptionLabel.BackgroundColor = UIColor.Clear;
			view.DescriptionLabel.Font = AppStyles.HelveticaNeue (20);
			view.DescriptionLabel.TextAlignment = UITextAlignment.Center;

			view.LoginRegisterButton.BackgroundColor = AppStyles.Red;
			view.LoginRegisterButton.Font = UIFont.FromName ("Helvetica", 21);
			view.LoginRegisterButton.SetTitleColor (UIColor.White, UIControlState.Normal);

			view.CancelButton.BackgroundColor = AppStyles.Gray;
			view.CancelButton.Font = UIFont.FromName ("Helvetica", 21);
			view.CancelButton.SetTitleColor (UIColor.White, UIControlState.Normal);


		}

//		public static void ItinerarySourceHeader (SourceHeaderCell cell)
//		{
//			cell.BackgroundColor = ColorFromHex (0x50575d);
//			cell.TitleLabel.TextColor = cell.CountLabel.TextColor = UIColor.White;
//
//			cell.CountLabel.BackgroundColor = ColorFromHex (0xf1592a);
//			cell.CountLabel.Layer.CornerRadius = 15;
//		}
//
//		public static void ItineraryItemCell (ItineraryItemCell cell)
//		{
//			cell.BackgroundColor = UIColor.White;
//
//			cell.TitleLabel.TextColor = ColorFromHex (0xf15a2c);
//			cell.TitleLabel.Font = UIFont.BoldSystemFontOfSize (16);
//
//			cell.SubTitleLabel.TextColor = ColorFromHex (0x50575d);
//			cell.SubTitleLabel.Font = UIFont.SystemFontOfSize (16);
//
//			cell.TimeLabel.TextColor = ColorFromHex (0x50575d);
//			cell.TimeLabel.Font = UIFont.SystemFontOfSize (14);
//
//			cell.DueLabel.TextColor = ColorFromHex (0xf15a2c);
//			cell.DueLabel.Font = UIFont.SystemFontOfSize (14);
//		}
//
//		public static void DateHeaderCell (DateHeaderCell cell)
//		{
//			cell.BackgroundColor = ColorFromHex (0x50575d);
//			cell.TitleLabel.TextColor = UIColor.White;
//		}
//
//		public static void ItineraryItemDateCell (ItineraryItemDateCell cell)
//		{
//			cell.TitleLabel.TextColor = ColorFromHex (0xf15a2c);
//			cell.TitleLabel.Font = UIFont.BoldSystemFontOfSize (16);
//
//			cell.CourseLabel.TextColor = ColorFromHex (0x50575d);
//			cell.CourseLabel.Font = UIFont.SystemFontOfSize (12);
//
//			cell.SubTitleLabel.TextColor = ColorFromHex (0x50575d);
//			cell.SubTitleLabel.Font = UIFont.SystemFontOfSize (12);
//		}
//
//		public static void HourCell (ItineraryHourCell cell)
//		{
//			cell.BackgroundColor = cell.HourLabel.BackgroundColor = cell.NowLabel.BackgroundColor = UIColor.Clear;
//			cell.HourRule.Alpha = .1f;
//			cell.NowRule.BackgroundColor = UIColor.Red;
//			cell.NowLabel.TextColor = UIColor.Red;
//			cell.NowLabel.Font = UIFont.SystemFontOfSize (12);
//		}
//
//		public static void AppointmentView (AppointmentView cell)
//		{
//			cell.BackgroundColor = ColorFromHex (0x20aab7).ColorWithAlpha (.5f);
////			cell.Frame = new CGRect (140, 0, 400, 100);
//
//			cell.TitleLabel.TextColor = ColorFromHex (0x20aab7);
//			cell.TitleLabel.Font = UIFont.SystemFontOfSize (16);
//
//			cell.SubTitle.TextColor = ColorFromHex (0x20aab7);
//			cell.SubTitle.Font = UIFont.SystemFontOfSize (12);
//		}
//
//		public static void IntineraryItemDetailsView (ItineraryItemDetailsView view)
//		{
//			//title
//			view.TitleLabel.TextColor = ColorFromHex (OrangeHex);
//			view.TitleLabel.Font = UIFont.BoldSystemFontOfSize (20);
//			//subtitle & due
//			view.DueLabel.TextColor = ColorFromHex (DarkGreyHex);
//			view.DueLabel.Font = view.SubTitleLabel.Font = UIFont.SystemFontOfSize (12);
//			view.SubTitleLabel.TextColor = ColorFromHex (OrangeHex);
//
//
//			//description text
//			view.DescriptionText.TextColor = ColorFromHex (0x50575d);
//			view.DescriptionText.Font = UIFont.SystemFontOfSize (12);
//			//twitter button
//			view.TwitterButton.SetTitleColor (UIColor.White, UIControlState.Normal);
//			view.TwitterButton.BackgroundColor = ColorFromHex (0x3f9dd9);
//			view.TwitterButton.Layer.CornerRadius = 3;
//			view.TwitterButton.ImageEdgeInsets = new UIEdgeInsets (0, 0, 0, 15);
//			//facebook button
//			view.FacebookButton.SetTitleColor (UIColor.White, UIControlState.Normal);
//			view.FacebookButton.BackgroundColor = ColorFromHex (0x3468af);
//			view.FacebookButton.Layer.CornerRadius = 3;
//			view.FacebookButton.ImageEdgeInsets = new UIEdgeInsets (0, 0, 0, 15);
//
//
//		}
//
//		public static void CourseDetailsView (CourseDetailsController view)
//		{
//			view.NameLabel.TextColor = ColorFromHex (OrangeHex);
//			view.NameLabel.Font = UIFont.BoldSystemFontOfSize (20);
//			view.NameLabel.AdjustsFontSizeToFitWidth = true;
//
//			view.InstitutionLabel.TextColor = ColorFromHex (OrangeHex);
//			view.InstitutionLabel.Font = UIFont.BoldSystemFontOfSize (16);
//
//			view.SummaryLabel.TextColor = ColorFromHex (DarkGreyHex);
//			view.SummaryLabel.Font = UIFont.SystemFontOfSize (12);
//
//			view.TeacherLabel.TextColor = view.TimeLabel.TextColor = view.LocationLabel.TextColor = view.TimeLabel.TextColor = view.UrlLabel.TextColor = ColorFromHex (0x50575d);
//			view.TeacherLabel.Font = view.TimeLabel.Font = view.LocationLabel.Font = view.TimeLabel.Font = view.UrlLabel.Font = UIFont.SystemFontOfSize (12);
//
//			view.TeacherTitleLabel.TextColor = view.TimeTitleLabel.TextColor = view.LocationTitleLabel.TextColor = ColorFromHex (OrangeHex);
//			view.TeacherTitleLabel.Font = view.TimeTitleLabel.Font = view.LocationTitleLabel.Font = UIFont.BoldSystemFontOfSize (12);
//
//			//twitter button
//			view.TwitterButton.SetTitleColor (UIColor.White, UIControlState.Normal);
//			view.TwitterButton.BackgroundColor = ColorFromHex (0x3f9dd9);
//			view.TwitterButton.Layer.CornerRadius = 3;
//			view.TwitterButton.ImageEdgeInsets = new UIEdgeInsets (0, 0, 0, 15);
//			//facebook button
//			view.FacebookButton.SetTitleColor (UIColor.White, UIControlState.Normal);
//			view.FacebookButton.BackgroundColor = ColorFromHex (0x3468af);
//			view.FacebookButton.Layer.CornerRadius = 3;
//			view.FacebookButton.ImageEdgeInsets = new UIEdgeInsets (0, 0, 0, 15);
//
//			view.SyllabusButton.SetTitleColor (ColorFromHex (OrangeHex), UIControlState.Normal);
//			view.SyllabusButton.Font = UIFont.BoldSystemFontOfSize (14);
//		}
//
//
//		public static void ConnectTableViewCell (ConnectTableViewCell cell)
//		{
//			cell.AuthorNameLabel.TextColor = ColorFromHex (OrangeHex);
//			cell.AuthorNameLabel.Font = UIFont.SystemFontOfSize (12);
//
//			cell.BodyText.Font = UIFont.SystemFontOfSize (12);
//			cell.CourseTitleLabel.TextColor = cell.BodyText.TextColor = ColorFromHex (0x50575d);
//
//			cell.CourseTitleLabel.Font = UIFont.SystemFontOfSize (14);
//
//			cell.DateLabel.Font = UIFont.SystemFontOfSize (10);
//			cell.DateLabel.TextColor = ColorFromHex (OrangeHex);
//		}
//
//		public static void AddTaskController (AddTaskController view)
//		{
//			ApplyStyle.NavigationBar (view.NavigationController.NavigationBar);
//			// title
////			view.TitleLabel.TextColor = ColorFromHex (OrangeHex);
////			view.TitleLabel.Font = UIFont.BoldSystemFontOfSize (20);
//		}
//
//		public static void AddTaskSuccessController (AddTaskSuccessController view)
//		{
//			// title
//			view.TitleLabel.TextColor = ColorFromHex (OrangeHex);
//			view.TitleLabel.Font = UIFont.BoldSystemFontOfSize (20);
//
//			// twitter button
//			view.TwitterButton.SetTitleColor (UIColor.White, UIControlState.Normal);
//			view.TwitterButton.BackgroundColor = ColorFromHex (0x3f9dd9);
//			view.TwitterButton.Layer.CornerRadius = 3;
//			view.TwitterButton.ImageEdgeInsets = new UIEdgeInsets (0, 0, 0, 15);
//
//			// facebook button
//			view.FacebookButton.SetTitleColor (UIColor.White, UIControlState.Normal);
//			view.FacebookButton.BackgroundColor = ColorFromHex (0x3468af);
//			view.FacebookButton.Layer.CornerRadius = 3;
//			view.FacebookButton.ImageEdgeInsets = new UIEdgeInsets (0, 0, 0, 15);
//
//			// done button
//			view.DoneButton.SetTitleColor (ColorFromHex (OrangeHex), UIControlState.Normal);
//			view.DoneButton.BackgroundColor = UIColor.White;
//			view.DoneButton.Layer.CornerRadius = 3;
//			view.DoneButton.Layer.BorderWidth = 1;
//			view.DoneButton.Layer.BorderColor = ColorFromHex (OrangeHex).CGColor;
//			view.DoneButton.ImageEdgeInsets = new UIEdgeInsets (0, 0, 0, 15);
//
//			// add button
//			view.AddButton.SetTitleColor (ColorFromHex (OrangeHex), UIControlState.Normal);
//			view.AddButton.BackgroundColor = UIColor.White;
//			view.AddButton.Layer.CornerRadius = 3;
//			view.AddButton.Layer.BorderWidth = 1;
//			view.AddButton.Layer.BorderColor = ColorFromHex (OrangeHex).CGColor;
//			view.AddButton.ImageEdgeInsets = new UIEdgeInsets (0, 0, 0, 15);
//		}




		public static UIColor ColorFromHex (int hex)
		{

			return UIColor.Clear.FromHex (hex);
		}
	}
}
