using System;
using GoogleAnalytics.iOS;
using WhitePaperBible.Core.Enum;

namespace IOS.Util
{
	public static class AnalyticsUtil
	{
		// Shared GA tracker
		private static IGAITracker Tracker;

		// Learn how to get your own Tracking Id from:
		// https://support.google.com/analytics/answer/2614741?hl=en
		private static readonly string TrackingId = "UA-6849331-5";

		static AnalyticsUtil ()
		{
			#if !DEBUG
			// Optional: set Google Analytics dispatch interval to e.g. 20 seconds.
			GAI.SharedInstance.DispatchInterval = 20;

			// Optional: automatically send uncaught exceptions to Google Analytics.
			GAI.SharedInstance.TrackUncaughtExceptions = true;

			// Initialize tracker.
			Tracker = GAI.SharedInstance.GetTracker (TrackingId);
			#endif
		}

		public static void TrackScreen (string screenName)
		{
			#if !DEBUG
			GAI.SharedInstance.DefaultTracker.Set (GAIConstants.ScreenName, screenName);
			GAI.SharedInstance.DefaultTracker.Send (GAIDictionaryBuilder.CreateAppView ().Build ());
			#endif
		}

		public static void TrackEvent (string category, string action, string label)
		{
			#if !DEBUG
			GAI.SharedInstance.DefaultTracker.Send (GAIDictionaryBuilder.CreateEvent (category, action, label, 1).Build ());
			#endif
		}

		public static void TrackSocial (MessageSourceEnum network, string action, string target)
		{
			#if !DEBUG
//			GAI.SharedInstance.DefaultTracker.Send (GAIDictionaryBuilder.CreateSocial (network.ToLabelString (), action, target).Build ());
			#endif
		}
	}
}

