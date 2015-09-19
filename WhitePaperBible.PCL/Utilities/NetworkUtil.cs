using System;

namespace WhitePaperBible.Core.Utilities
{
	public enum NetworkStatus
	{
		NotReachable,
		ReachableViaCarrierDataNetwork,
		ReachableViaWiFiNetwork
	}

	public abstract class NetworkUtil
	{
		public abstract NetworkStatus RemoteHostStatus ();
	}
}

