using System;
using System.Net;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using MonoTouch.SystemConfiguration;
using MonoTouch.CoreFoundation;
using WhitePaperBible.Core.Utilities;

namespace WhitePaperBible.iOS
{
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
				adHocWiFiNetworkReachability.SetCallback (OnChange);
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
				defaultRouteReachability.SetCallback (OnChange);
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

				remoteHostReachability.SetCallback (OnChange);
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

