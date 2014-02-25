using System;
using MonoTouch.UIKit;
using System.Drawing;

namespace WhitePaperBible.iOS.UI
{
	public static class Environment
	{
		public static bool IsIOS7{
			get{

				var systemVersion = UIDevice.CurrentDevice.SystemVersion;
				var versionnNumber = float.Parse (systemVersion.ToCharArray()[0].ToString());

				return (versionnNumber >= 7);

			}
		}

		public static PointF DeviceCenter
		{
			get{
				return new PointF (ScreenWidth/2, DeviceScreenHeight/2);
			}
		}

		public static float ScreenWidth = 320;

		public static float DeviceScreenHeight{
			get{
				return (IsIOS7)?UIScreen.MainScreen.Bounds.Height:UIScreen.MainScreen.Bounds.Height - 20;
			}
		}




	}
}

