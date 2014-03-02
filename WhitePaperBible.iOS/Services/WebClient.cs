using System;
using RestSharp;
using WhitePaperBible.Core.Services;
using MonoTouch.UIKit;
using System.Collections.Generic;

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

		public void OpenURL (string url)
		{
			var client = new RestClient ();

			var request = new RestRequest (url, Method.GET) { RequestFormat = DataFormat.Json };

			ShowNetworkActivity (url);

			client.ExecuteAsync (request, response => {
				responseText = response.Content;
				CheckPendingNetworkActivity (url);
				if (response.ResponseStatus == ResponseStatus.Error) {
					RequestError (this, EventArgs.Empty);
				} else {
					RequestComplete (this, EventArgs.Empty);
				}

			});
		}

		static void ShowNetworkActivity (string url)
		{
			PendingMethods.Add (url);
			UIApplication.SharedApplication.NetworkActivityIndicatorVisible = true;
		}

		static void CheckPendingNetworkActivity (string url)
		{
			PendingMethods.Remove (url);
			UIApplication.SharedApplication.NetworkActivityIndicatorVisible = PendingMethods.Count != 0;
		}

		private string responseText;

		public string ResponseText {
			get {
				return responseText;
			}
		}

		#endregion
	}
}

