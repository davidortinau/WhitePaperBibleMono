using System;
using UIKit;
using CoreGraphics;

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

		public static CGPoint DeviceCenter
		{
			get{
				return new CGPoint (ScreenWidth/2, DeviceScreenHeight/2);
			}
		}

		public static nfloat ScreenWidth = 320;

		public static nfloat DeviceScreenHeight{
			get{
				return (IsIOS7)?UIScreen.MainScreen.Bounds.Height:UIScreen.MainScreen.Bounds.Height - 20;
			}
		}




	}
}

