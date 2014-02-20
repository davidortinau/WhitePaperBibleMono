using System;
using System.Collections.Generic;
using System.Linq;

using MonoTouch.Foundation;
using MonoTouch.UIKit;
using MonoTouch.ObjCRuntime;
using MonkeyArms;
using WhitePaperBible.Core.Commands;
using WhitePaperBible.Core.Invokers;
using WhitePaperBible.Core.Models;

namespace WhitePaperBible.iOS
{
	// The UIApplicationDelegate for the application. This class is responsible for launching the 
	// User Interface of the application, as well as listening (and optionally responding) to 
	// application events from iOS.
	[Register ("AppDelegate")]
	public partial class AppDelegate : UIApplicationDelegate
	{
		
		public const float Font16pt = 22f;
		public const float Font10_5pt = 14f;
		public const float Font10pt = 13f;
		public const float Font9pt = 12f;
		public const float Font7_5pt = 10f;
		public static readonly UIColor ColorNavBarTint = UIColor.FromRGB (55, 87, 118);
		public static readonly UIColor ColorTextHome = UIColor.FromRGB (192, 205, 223);
		public static readonly UIColor ColorHeadingHome = UIColor.FromRGB (150, 210, 254);
		public static readonly UIColor ColorCellBackgroundHome = UIColor.FromRGB (36, 54, 72);
		public static readonly UIColor ColorTextLink = UIColor.FromRGB (9, 9, 238);	
		
		
		// class-level declarations
		UIWindow window;
		UITabBarController tabBarController;
		
//		public static List<PaperNode> papers;
//
		public static List<TagNode> tags {
			get;
			set;
		}
		
		public static bool IsPhone {
			get {
				return UIDevice.CurrentDevice.UserInterfaceIdiom == UIUserInterfaceIdiom.Phone;
			}
		}

		public static bool IsPad {
			get {
				return UIDevice.CurrentDevice.UserInterfaceIdiom == UIUserInterfaceIdiom.Pad;
			}
		}

		public static bool HasRetina {
			get {
				if (MonoTouch.UIKit.UIScreen.MainScreen.RespondsToSelector (new Selector ("scale")))
					return (MonoTouch.UIKit.UIScreen.MainScreen.Scale == 2.0);
				else
					return false;
			}
		}
		
		//
		// This method is invoked when the application has loaded and is ready to run. In this 
		// method you should instantiate the window, load the UI into it and then make the window
		// visible.
		//
		// You have 17 seconds to return from this method, or iOS will terminate your application.
		//
		public override bool FinishedLaunching (UIApplication app, NSDictionary options)
		{
			initMonkeyArms ();

			UINavigationBar.Appearance.TintColor = UIColor.Black;
			
			// create a new window instance based on the screen size
			window = new UIWindow (UIScreen.MainScreen.Bounds);
			
			tabBarController = new TabBarController();
			
			window.RootViewController = tabBarController;
			window.MakeKeyAndVisible ();


			
			return true;
		}

		void initMonkeyArms() {
			DI.MapCommandToInvoker <BootstrapCommand, BootstrapInvoker>().Invoke();

			DI.Get<PapersReceivedInvoker>().Invoked += (object sender, EventArgs e) => {
				Console.WriteLine("GOT PAPERS");
			};
		}
	}
}

