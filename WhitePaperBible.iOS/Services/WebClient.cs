using System;
using RestSharp;
using WhitePaperBible.Core.Services;

namespace WhitePaperBible.iOS
{
	public class WebClient:IJSONWebClient
	{
		public WebClient ()
		{
		}

		#region IJSONWebClient implementation

		public event EventHandler RequestComplete = delegate {};
		public event EventHandler RequestError;

		public void OpenURL (string url)
		{
			var client = new RestClient ();

			var request = new RestRequest (url, Method.GET) { RequestFormat = DataFormat.Json };
			client.ExecuteAsync (request, response => {
				responseText = response.Content;
				if (response.ResponseStatus == ResponseStatus.Error) {
					RequestError (this, EventArgs.Empty);
				} else {
					RequestComplete (this, EventArgs.Empty);
				}

			});
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

