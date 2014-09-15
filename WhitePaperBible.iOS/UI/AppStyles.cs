using System;
using MonoTouch.UIKit;

namespace WhitePaperBible.iOS.UI
{
	public static class AppStyles
	{
		public static UIColor Purple = UIColor.FromRGB (153, 109, 172);

		public static UIColor DarkPurple = UIColor.FromRGB (125,100,147);

		public static UIColor LightPurple = UIColor.FromRGB(187,165,196);

		public static UIColor Gray = UIColor.FromRGB (107, 97, 95);

		public static UIColor DarkGray = UIColor.Clear.FromHex(0x444444);

		public static UIColor Red = UIColor.Clear.FromHex(0xAC1F2D);

		public static UIColor DarkGrayRule = UIColor.FromRGB(92, 83, 82);

		public static UIColor LightGrayRule = UIColor.FromRGB(166, 161, 161);

		public static UIColor OffWhite = UIColor.FromRGB(247, 245, 244);

		public static UIColor OffBlack = UIColor.FromRGB(76,76,76);

		public static UIColor FacebookBlue = UIColor.FromRGB(59, 88, 156);

		public static UIColor TwitterBlue = UIColor.FromRGB(0, 172, 237);



		public static UIFont HelveticaNeue(float size)
		{
			return UIFont.FromName ("HelveticaNeue", size);
		}

		public static UIFont HelveticaNeueLight(float size)
		{
			return UIFont.FromName ("HelveticaNeue-Light", size);
		}
		public static UIFont HelveticaNeueMedium(float size)
		{
			return UIFont.FromName ("HelveticaNeue-Medium", size);
		}

		public static UIFont HelveticaNeueUltraLight(float size)
		{
			return UIFont.FromName ("HelveticaNeue-UltraLight", size);
		}

		public static UIFont HelveticaNeueThin(float size)
		{
			return UIFont.FromName ("HelveticaNeueLTCom-Th", size);
		}

		public static UIFont HelveticaNeueThinCondensed(float size)
		{
			return UIFont.FromName ("HelveticaNeueLTCom-ThCn", size);
		}
	}
}

