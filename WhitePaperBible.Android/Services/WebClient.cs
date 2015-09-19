using System;
using RestSharp;
using WhitePaperBible.Core.Services;
using WhitePaperBible.Core.Models;
using System.Net;
using WhitePaperBible.Core;
using MonkeyArms;
using System.Threading.Tasks;

namespace WhitePaperBible.Android.Services
{
	public class WebClient : IJSONWebClient
	{
		[Inject]
		public AppModel AM;

		public async Task OpenURL (string url, MethodEnum method=MethodEnum.GET, bool includeSessionCookie=false)
		{

			AppModel AM = DI.Get<AppModel> ();
			var client = new RestClient ();
			if(includeSessionCookie){
				var cookieJar = new CookieContainer ();
				cookieJar.Add (new Uri (Constants.BASE_URI), new Cookie (AM.UserSessionCookie.Name, AM.UserSessionCookie.Value));
				client.CookieContainer = cookieJar;
			}

			var request = new RestRequest (url, (Method)method) { RequestFormat = DataFormat.Json };

			client.ExecuteAsync (request, response => {
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

				if (response.ResponseStatus == ResponseStatus.Error) {
					DispatchError ();
				} else {
					DispatchComplete ();
				}
			});

		}

		public event EventHandler RequestComplete = delegate{};
		public event EventHandler RequestError = delegate{};

		private string responseText;

		public string ResponseText {
			get {
				return responseText;
			}
		}

		public SessionCookie UserSessionCookie {
			get;
			private set;
		}

		public WebClient ()
		{
		}

		#region IJSONWebClient implementation
		private void DispatchComplete ()
		{
			RequestComplete (this, EventArgs.Empty);
		}

		private void DispatchError ()
		{
			RequestError (this, EventArgs.Empty);
		}


//		private static void AddNetworkActivity (string url)
//		{
//			PendingMethods.Add (url);
//			UIApplication.SharedApplication.NetworkActivityIndicatorVisible = true;
//		}
//
//		private static void RemoveNetworkActivity (string url)
//		{
//			PendingMethods.Remove (url);
//			UIApplication.SharedApplication.NetworkActivityIndicatorVisible = PendingMethods.Count != 0;
//		}

		#endregion
	}
}

