using System;
using System.Collections.Generic;
using System.Linq;
using Foundation;
using UIKit;
using ObjCRuntime;
using MonkeyArms;
using WhitePaperBible.Core.Commands;
using WhitePaperBible.Core.Invokers;
using WhitePaperBible.Core.Models;
using WhitePaperBible.Core.Services;
using SystemConfiguration;
using WhitePaperBible.Core.Utilities;
//using Segment;
using Xamarin;
using IOS.Util;
using Newtonsoft.Json;
using WhitePaperBible.Core.Repositories;

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
		NetworkStatus remoteHostStatus, internetStatus, localWifiStatus;
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
				if (UIKit.UIScreen.MainScreen.RespondsToSelector (new Selector ("scale")))
					return (UIKit.UIScreen.MainScreen.Scale == 2.0);
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

			DI.MapInstanceToSingleton<NetworkUtil> (new Reachability ());

			if (DI.CanResolve<NetworkUtil> ()) {
				var NUtil = DI.Get<NetworkUtil> ();
			}

			Insights.Initialize("7fd2e4a9a3eb23a3d95e395a568abc3b8621065e");
			Insights.Track ("App Launched");

			UINavigationBar.Appearance.TintColor = UIColor.DarkGray;
			UITabBar.Appearance.TintColor = UIColor.Black;
//			UITabBar.Appearance.BarTintColor = UIColor.LightGray;
			UITableViewCell.Appearance.TintColor = UIColor.DarkGray;
			UITableView.Appearance.TintColor = UIColor.DarkGray;
			UIToolbar.Appearance.TintColor = UIColor.White;
			
			// create a new window instance based on the screen size
			window = new UIWindow (UIScreen.MainScreen.Bounds);
			
			tabBarController = new TabBarController ();
			
			window.RootViewController = tabBarController;
			window.MakeKeyAndVisible ();

			AnalyticsUtil.TrackEvent ("App", "Launched", "");

			return true;
		}

		void initMonkeyArms ()
		{
			DI.MapClassToInterface<WebClient, IJSONWebClient> ();
			DI.MapCommandToInvoker <BootstrapCommand, BootstrapInvoker> ().Invoke ();
		}

//		async public override void HandleWatchKitExtensionRequest
//		(UIApplication application, NSDictionary userInfo, Action<NSDictionary> reply)
//		{
//			// need to make sure the app is all ready to go first
//			// is AppModel ready and has a UserSessionCookie
//			// call LoadStorageCommand and listen for StorageLoaded or make it async
//			initMonkeyArms();
//			var appModel = DI.Get<AppModel>();
//			if(appModel.UserSessionCookie == null){
//				Console.WriteLine("NO COOKIE");
//				var loadCmd = DI.Get<LoadStorageCommand>();
//				try{
//					Console.WriteLine("TRY TO LOAD DATA STORE");
//					await loadCmd.LoadStore();
//				}catch(Exception ex){
//					reply (new NSDictionary (
//						"payload", "error " + ex.Message
//					)
//					);
//					return;	
//				}
//			}
//
//			WatchAppManager.ProcessMessage(userInfo, reply);
//
////			var request = userInfo.Values
////
////			List<string> papers = new List<string>();
////			papers.Add("Abiding in Christ");
////			papers.Add("Believer's Authority");
////			papers.Add("Bible Scriptures About Work");
////			papers.Add("Bible Verses about Prospering");
////			papers.Add("Encouragement to do God's work");
////			papers.Add("Encouragement to Receive by Faith");
////			papers.Add("Fear Not");
////			papers.Add("For Daily Encouragement");
////			papers.Add("God Loves Me");
////			papers.Add("God's Word");
////
////			var payload = JsonConvert.SerializeObject(papers);
////
////			reply (new NSDictionary (
////				"payload", payload
////			));
//		}
	}
}

