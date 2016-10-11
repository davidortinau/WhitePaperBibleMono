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

using IOS.Util;
using Newtonsoft.Json;
using WhitePaperBible.Core.Repositories;
using System.Net;
using CoreFoundation;

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

			//Insights.Initialize("7fd2e4a9a3eb23a3d95e395a568abc3b8621065e");
			//Insights.Track ("App Launched");

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

    public class Reachability:NetworkUtil
    {
        public string HostName = "www.google.com";

        public bool IsReachableWithoutRequiringConnection (NetworkReachabilityFlags flags)
        {
            // Is it reachable with the current network configuration?
            bool isReachable = (flags & NetworkReachabilityFlags.Reachable) != 0;

            // Do we need a connection to reach it?
            bool noConnectionRequired = (flags & NetworkReachabilityFlags.ConnectionRequired) == 0;

            // Since the network stack will automatically try to get the WAN up,
            // probe that
            if ((flags & NetworkReachabilityFlags.IsWWAN) != 0)
                noConnectionRequired = true;

            return isReachable && noConnectionRequired;
        }
        // Is the host reachable with the current network configuration
        public bool IsHostReachable (string host)
        {
            if (host == null || host.Length == 0)
                return false;

            using (var r = new NetworkReachability (host)) {
                NetworkReachabilityFlags flags;

                if (r.TryGetFlags (out flags)) {
                    return IsReachableWithoutRequiringConnection (flags);
                }
            }
            return false;
        }
        //
        // Raised every time there is an interesting reachable event,
        // we do not even pass the info as to what changed, and
        // we lump all three status we probe into one
        //
        public event EventHandler ReachabilityChanged = delegate{};

        private void OnChange (NetworkReachabilityFlags flags)
        {
            ReachabilityChanged (this, EventArgs.Empty);

        }
        //
        // Returns true if it is possible to reach the AdHoc WiFi network
        // and optionally provides extra network reachability flags as the
        // out parameter
        //
        static NetworkReachability adHocWiFiNetworkReachability;

        public bool IsAdHocWiFiNetworkAvailable (out NetworkReachabilityFlags flags)
        {
            if (adHocWiFiNetworkReachability == null) {
                adHocWiFiNetworkReachability = new NetworkReachability (new IPAddress (new byte [] { 169, 254, 0, 0 }));

                adHocWiFiNetworkReachability.SetNotification (OnChange);
                adHocWiFiNetworkReachability.Schedule (CFRunLoop.Current, CFRunLoop.ModeDefault);
            }

            if (!adHocWiFiNetworkReachability.TryGetFlags (out flags))
                return false;

            return IsReachableWithoutRequiringConnection (flags);
        }

        static NetworkReachability defaultRouteReachability;

        public bool IsNetworkAvailable (out NetworkReachabilityFlags flags)
        {
            if (defaultRouteReachability == null) {
                defaultRouteReachability = new NetworkReachability (new IPAddress (0));
                defaultRouteReachability.SetNotification (OnChange);
                defaultRouteReachability.Schedule (CFRunLoop.Current, CFRunLoop.ModeDefault);
            }
            if (!defaultRouteReachability.TryGetFlags (out flags))
                return false;
            return IsReachableWithoutRequiringConnection (flags);
        }

        private NetworkReachability remoteHostReachability;

        override public NetworkStatus RemoteHostStatus ()
        {
            NetworkReachabilityFlags flags;
            bool reachable;

            if (remoteHostReachability == null) {
                remoteHostReachability = new NetworkReachability (HostName);

                // Need to probe before we queue, or we wont get any meaningful values
                // this only happens when you create NetworkReachability from a hostname
                reachable = remoteHostReachability.TryGetFlags (out flags);

                remoteHostReachability.SetNotification (OnChange);
                remoteHostReachability.Schedule (CFRunLoop.Current, CFRunLoop.ModeDefault);
            } else
                reachable = remoteHostReachability.TryGetFlags (out flags);         

            if (!reachable)
                return NetworkStatus.NotReachable;

            if (!IsReachableWithoutRequiringConnection (flags))
                return NetworkStatus.NotReachable;

            if ((flags & NetworkReachabilityFlags.IsWWAN) != 0)
                return NetworkStatus.ReachableViaCarrierDataNetwork;

            return NetworkStatus.ReachableViaWiFiNetwork;
        }

        public NetworkStatus InternetConnectionStatus ()
        {
            NetworkReachabilityFlags flags;
            bool defaultNetworkAvailable = IsNetworkAvailable (out flags);
            if (defaultNetworkAvailable && ((flags & NetworkReachabilityFlags.IsDirect) != 0)) {
                return NetworkStatus.NotReachable;
            } else if ((flags & NetworkReachabilityFlags.IsWWAN) != 0)
                return NetworkStatus.ReachableViaCarrierDataNetwork;
            else if (flags == 0)
                return NetworkStatus.NotReachable;
            return NetworkStatus.ReachableViaWiFiNetwork;
        }

        public NetworkStatus LocalWifiConnectionStatus ()
        {
            NetworkReachabilityFlags flags;
            if (IsAdHocWiFiNetworkAvailable (out flags)) {
                if ((flags & NetworkReachabilityFlags.IsDirect) != 0)
                    return NetworkStatus.ReachableViaWiFiNetwork;
            }
            return NetworkStatus.NotReachable;
        }
    }
}

