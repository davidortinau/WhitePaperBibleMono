using System;
using RestSharp;
using WhitePaperBible.Core.Services;
using UIKit;
using System.Collections.Generic;
using System.Net;
using WhitePaperBible.Core.Models;

using MonkeyArms;
using WhitePaperBible.Core.Invokers;
using WhitePaperBible.Core.Utilities;
using WhitePaperBible.Core;
using System.Threading.Tasks;
using System.Threading;


namespace WhitePaperBible.iOS
{
	public class WebClient:IJSONWebClient
	{
		public WebClient ()
		{
			ResolveNetworkUtil ();
		}

		private static List<string> PendingMethods = new List<string> ();

		#region IJSONWebClient implementation

		public event EventHandler RequestComplete = delegate {};
		public event EventHandler RequestError;

		NetworkUtil NUtil;

		void ResolveNetworkUtil ()
		{
			if (NUtil == null) {
				if (DI.CanResolve<NetworkUtil> ()) {
					NUtil = DI.Get<NetworkUtil> ();
				}
			}
		}

		public async Task OpenURL (string url, MethodEnum method=MethodEnum.GET, bool includeSessionCookie=false)
		{
			ResolveNetworkUtil ();

			if (NUtil != null && (NUtil.RemoteHostStatus () == NetworkStatus.NotReachable)) {
				DI.Get<UnreachableInvoker> ().Invoke ();
			} else {
				var client = new RestClient ();
				if(includeSessionCookie){
					AppModel AM = DI.Get<AppModel> ();
					var cookieJar = new CookieContainer ();
					cookieJar.Add (new Uri (Constants.BASE_URI), new Cookie (AM.UserSessionCookie.Name, AM.UserSessionCookie.Value));
					client.CookieContainer = cookieJar;
				}

				var request = new RestRequest (url, (Method)method) { RequestFormat = DataFormat.Json };

				AddNetworkActivity (url);

				var cancellationTokenSource = new CancellationTokenSource();
				var response = await client.ExecuteTaskAsync (request, cancellationTokenSource.Token);
				if (response.Cookies.Count > 0) {
					foreach(var cookie in response.Cookies){
						if(cookie.Name == "_whitepaperbible_session"){
							UserSessionCookie = new SessionCookie { 
								Name = cookie.Name,
								Value = cookie.Value
							};
						}
					}
				}

				ResponseText = response.Content;
				RemoveNetworkActivity (url);
				if (response.ResponseStatus == ResponseStatus.Error) {
					DispatchError ();
				} else {
					DispatchComplete ();
				}

//				client.ExecuteAsync (request, response => {
//					if (response.Cookies.Count > 0) {
//						foreach(var cookie in response.Cookies){
//							if(cookie.Name == "_whitepaperbible_session"){
//								UserSessionCookie = new SessionCookie { 
//									Name = cookie.Name,
//									Value = cookie.Value
//								};
//							}
//						}
//					}
//
//					ResponseText = response.Content;
//					RemoveNetworkActivity (url);
//					if (response.ResponseStatus == ResponseStatus.Error) {
//						DispatchError ();
//					} else {
//						DispatchComplete ();
//					}
//				});
			}
		}

		private void DispatchComplete ()
		{
			RequestComplete (this, EventArgs.Empty);
		}

		private void DispatchError ()
		{
//			if (!Reachability.IsHostReachable ("http://google.com")) {
//				DI.Get<UnreachableInvoker> ().Invoke ();
////				Unreachable.Invoke ();
//			} else {
			RequestError (this, EventArgs.Empty);
//			}
		}

		private static void AddNetworkActivity (string url)
		{
			PendingMethods.Add (url);
			UIApplication.SharedApplication.NetworkActivityIndicatorVisible = true;
		}

		private static void RemoveNetworkActivity (string url)
		{
			PendingMethods.Remove (url);
			UIApplication.SharedApplication.NetworkActivityIndicatorVisible = PendingMethods.Count != 0;
		}

		public string ResponseText {
			get;
			private set;
		}

		public SessionCookie UserSessionCookie {
			get;
			private set;
		}

		#endregion
	}
}

