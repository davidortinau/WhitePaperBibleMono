using System;
using RestSharp;
using WhitePaperBible.Core.Services;
using MonoTouch.UIKit;
using System.Collections.Generic;
using System.Net;
using WhitePaperBible.Core.Models;

using MonkeyArms;
using WhitePaperBible.Core.Invokers;


namespace WhitePaperBible.iOS
{
	public class WebClient:IJSONWebClient
	{
		public WebClient ()
		{
		}

		private static List<string> PendingMethods = new List<string> ();

		#region IJSONWebClient implementation

		public event EventHandler RequestComplete = delegate {};
		public event EventHandler RequestError;

		public void OpenURL (string url, MethodEnum method=MethodEnum.GET, CookieContainer cookieJar=null)
		{
			if (!Reachability.IsHostReachable ("http://www.google.com")) {
				Console.WriteLine ("UNREACHABLE");
				DI.Get<UnreachableInvoker> ().Invoke ();
			} else {
				var client = new RestClient ();
				client.CookieContainer = cookieJar;

				var request = new RestRequest (url, (Method)method) { RequestFormat = DataFormat.Json };

				AddNetworkActivity (url);

				client.ExecuteAsync (request, response => {
					if (response.Cookies.Count > 0) {
						if (response.Cookies [0].Name == "_whitepaperbible_session") {
							UserSessionCookie = new SessionCookie { 
								Name = response.Cookies [0].Name,
								Value = response.Cookies [0].Value
							};
						}
					}

					ResponseText = response.Content;
					RemoveNetworkActivity (url);
					if (response.ResponseStatus == ResponseStatus.Error) {
						DispatchError ();
					} else {
						DispatchComplete ();
					}
				});
			}
		}

		private void DispatchComplete ()
		{
			RequestComplete (this, EventArgs.Empty);
		}

		private void DispatchError ()
		{
			if (!Reachability.IsHostReachable ("http://google.com")) {
				DI.Get<UnreachableInvoker> ().Invoke ();
//				Unreachable.Invoke ();
			} else {
				RequestError (this, EventArgs.Empty);
			}
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

