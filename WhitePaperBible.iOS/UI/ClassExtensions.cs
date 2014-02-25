using System;
using MonoTouch.UIKit;
using System.Drawing;
using WhitePaperBible.iOS.UI;
using MonoTouch.Foundation;

namespace WhitePaperBible.iOS.UI
{
	public static class ClassExtensions
	{

		//UIColor
		public static UIColor FromHex(this UIColor color,int hexValue)
		{
			return UIColor.FromRGB(
				(((float)((hexValue & 0xFF0000) >> 16))/255.0f),
				(((float)((hexValue & 0xFF00) >> 8))/255.0f),
				(((float)(hexValue & 0xFF))/255.0f)
			);
		}

		//UIImageView
		public static UIImage Empty(this UIImage image)
		{
			UIGraphics.BeginImageContextWithOptions(new SizeF(36,36), false, 0);
			UIImage blank = UIGraphics.GetImageFromCurrentImageContext();
			UIGraphics.EndImageContext();
			return blank;
		}

		public static UIImage WithTint(this UIImage image, UIColor color,  float alpha = 1f) {

			UIGraphics.BeginImageContext(image.Size);

			RectangleF contextRect = new RectangleF (Point.Empty, image.Size);

			// Retrieve source image and begin image context
			SizeF itemImageSize = image.Size;
			PointF itemImagePosition = new PointF(); 
			itemImagePosition.X = (float)Math.Ceiling((contextRect.Size.Width - itemImageSize.Width) / 2);
			itemImagePosition.Y = (float)Math.Ceiling((contextRect.Size.Height - itemImageSize.Height) );

			UIGraphics.BeginImageContext(contextRect.Size);

			var c = UIGraphics.GetCurrentContext ();
			// Setup shadow
			// Setup transparency layer and clip to mask

			c.BeginTransparencyLayer();
			c.ScaleCTM (1, -1);
			c.ClipToMask (new RectangleF(itemImagePosition.X, -itemImagePosition.Y, itemImageSize.Width, -itemImageSize.Height), image.CGImage);

			// Fill and end the transparency layer
			float red = 0;
			float green = 0;
			float blue = 0;
			float a = 0;
			color.GetRGBA (out red, out green, out blue, out a);
			c.SetRGBFillColor (red, green, blue, a);

			contextRect.Size = new SizeF (contextRect.Size.Width, (-1 * contextRect.Size.Height) - 15);
			c.FillRect(contextRect);
			c.EndTransparencyLayer();

			UIImage img = UIGraphics.GetImageFromCurrentImageContext();
			UIGraphics.EndImageContext();
			return img;
		}

		//UIViewController
		public static void ShowNetworkActivityIndicator(this UIViewController controller)
		{
			controller.InvokeOnMainThread(() => UIApplication.SharedApplication.NetworkActivityIndicatorVisible = true);
		}

		public static void HideNetworkActivityIndicator(this UIViewController controller)
		{
			controller.InvokeOnMainThread(() => UIApplication.SharedApplication.NetworkActivityIndicatorVisible = false);
		}

		public static UIBarButtonItem CreateRightIconHeaderButton(this UIViewController controller, UIImage icon, UINavigationItem item)
		{
			var iconButton = new UIBarButtonItem ();
			iconButton.Image = icon;
			iconButton.SetBackgroundImage (new UIImage().Empty(), UIControlState.Normal, new UIBarMetrics ());
			iconButton.TintColor = UIColor.White;
			item.SetRightBarButtonItem (iconButton, true);
			return iconButton;
		}

		//UIView
		public static void CenterHorizontally(this UIView view)
		{
			view.Center = new PointF (Environment.DeviceCenter.X, view.Center.Y);
		}

		public static void CenterVertically(this UIView view)
		{
			view.Center = new PointF (view.Center.X, view.Superview.Frame.Height/2);
		}

		public static void Move(this UIView view, Point newLocation)
		{
			view.Frame = new RectangleF (newLocation, view.Frame.Size);
		}

		public static void MoveToYPos(this UIView view, int newY)
		{
			view.Move (new Point ((int)view.Frame.X, newY));
		}

		public static void MoveToXPos(this UIView view, int newX)
		{
			view.Move (new Point (newX, (int)view.Frame.Y));
		}

		public static void Resize(this UIView view, Size size)
		{
			view.Frame = new RectangleF (view.Frame.Location, size);
		}

		public static void ResizeHeight(this UIView view, int newHeight)
		{
			view.Resize (new Size ((int)view.Frame.Size.Width, newHeight));
		}

		//UITableViewCell
		public static void Style(this UITableViewCell cell)
		{
			cell.TextLabel.Font = AppStyles.HelveticaNeueMedium (16f);
			cell.TextLabel.TextColor = AppStyles.OffBlack;

			cell.DetailTextLabel.Font = AppStyles.HelveticaNeueLight (15f);
			cell.DetailTextLabel.TextColor = UIColor.FromRGB (115, 115, 115);

			cell.DetailTextLabel.BackgroundColor = cell.TextLabel.BackgroundColor = UIColor.Clear;
			cell.BackgroundView = new UIView (cell.Bounds){ BackgroundColor = UIColor.White };
		}



		//UILabel
		public static void SetTextRangeFont(this UILabel view, UIFont font, NSRange range)
		{
			InitTextAttributes (view);
			(view.AttributedText as NSMutableAttributedString).AddAttribute (UIStringAttributeKey.Font, font, range);

		}

		public static void SetTextFont(this UILabel view, UIFont font, string text)
		{
			var start = view.Text.IndexOf (text);
			var range = new NSRange (start, text.Length);
			SetTextRangeFont(view, font, range);
		}

		public static void SetTextRangeColor(this UILabel view, UIColor color, NSRange range)
		{
			InitTextAttributes (view);
			(view.AttributedText as NSMutableAttributedString).AddAttribute (UIStringAttributeKey.ForegroundColor, color, range);

		}

		public static void SetTextRangeBackgroundColor(this UILabel view, UIColor color, NSRange range)
		{
			InitTextAttributes (view);
			(view.AttributedText as NSMutableAttributedString).AddAttribute (UIStringAttributeKey.BackgroundColor, color, range);

		}
		static void InitTextAttributes (UILabel view)
		{
			if (view.AttributedText is NSMutableAttributedString) {
				return;
			}

			var attrs = new NSMutableDictionary () {
				{ UIStringAttributeKey.ForegroundColor, AppStyles.OffBlack }
			};

			view.AttributedText = new NSMutableAttributedString (view.Text, attrs);
			//			a.AddAttribute (UIStringAttributeKey.Font, UIFont.FromName ("Helvetica", 12), new NSRange (0,view.Text.Length));


		}

		//UITextView
		public static void SetTextFont(this UITextView view, UIFont font, string text)
		{
			var start = view.Text.IndexOf (text);
			var range = new NSRange (start, text.Length);
			SetTextRangeFont(view, font, range);
		}

		public static void SetTextRangeFont(this UITextView view, UIFont font, NSRange range)
		{
			InitTextAttributes (view);
			(view.AttributedText as NSMutableAttributedString).AddAttribute (UIStringAttributeKey.Font, font, range);

		}



		static void InitTextAttributes (UITextView view)
		{
			if (view.AttributedText is NSMutableAttributedString) {
				return;
			}

			var attrs = new NSMutableDictionary () {
				{ UIStringAttributeKey.ForegroundColor, AppStyles.OffBlack }
			};

			view.AttributedText = new NSMutableAttributedString (view.Text, attrs);
			//			a.AddAttribute (UIStringAttributeKey.Font, UIFont.FromName ("Helvetica", 12), new NSRange (0,view.Text.Length));


		}


	} 
}

