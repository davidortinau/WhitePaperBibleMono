using System;
using System.Threading.Tasks;
using System.Threading;
using System.Net.Http;
using WhitePaperBible.Core.Models;
using System.Net;

namespace WhitePaperBible.Core.Repositories
{
	public interface IDownloadService
	{
		Task<string> Download(string url, CancellationTokenSource token = null, SessionCookie sessionCookie = null);
		void CancelCurrent();
		void Cancel(CancellationTokenSource token);
	}

	public interface IModernHttpClient
	{
		HttpClient Get();
		HttpClient Get(HttpMessageHandler handler);
		HttpMessageHandler GetNativeHandler(bool throwOnCaptiveNetwork = false, bool useCustomSslCertification = false);
	}

	public class DownloadService : IDownloadService
	{
		private readonly IModernHttpClient _modernModernHttpClient;
		private CancellationTokenSource _currentToken;

		public DownloadService()
		{
//			_modernModernHttpClient = new ModernHttpClient();Xamarin
		}

		public async Task<string> Download(string url, CancellationTokenSource token = null, SessionCookie sessionCookie = null)
		{
			_currentToken = token ?? new CancellationTokenSource();

			var handler = _modernModernHttpClient.GetNativeHandler();
			var outerHandler = new RetryHandler(handler, 3);
			var client = _modernModernHttpClient.Get(outerHandler);
			if(sessionCookie != null){
				var cookieJar = new CookieContainer ();
				cookieJar.Add (new Uri (Constants.BASE_URI), new Cookie (sessionCookie.Name, sessionCookie.Value));

			}

			var msg = await client.GetAsync(url, _currentToken.Token);

			if (!msg.IsSuccessStatusCode) return "Something derped";

			var result = await msg.Content.ReadAsStringAsync();
			return result;
		}

		public void CancelCurrent()
		{
			if (_currentToken != null)
				_currentToken.Cancel();
		}

		public void Cancel(CancellationTokenSource token)
		{
			if (token == null)
				CancelCurrent();
			else
				token.Cancel();
		}
	}

	public class RetryHandler : DelegatingHandler
	{
		private readonly int _maxRetries;

		public RetryHandler(HttpMessageHandler innerHandler, int maxRetries = 5)
			: base(innerHandler) { _maxRetries = maxRetries; }

		protected override async Task<HttpResponseMessage> SendAsync(
			HttpRequestMessage request,
			CancellationToken cancellationToken)
		{

			Exception lastException = null;
			HttpResponseMessage response = null;
			for (var i = 0; i < _maxRetries; i++)
			{
				try
				{
					response = await base.SendAsync(request, cancellationToken);
					if (response.IsSuccessStatusCode)
					{
//						Mvx.Trace(MvxTraceLevel.Diagnostic, "Request was ok after {0} retries...", i);
						return response;
					}
				}
				catch (Exception e)
				{
					lastException = e;
				}
//				Mvx.Trace(MvxTraceLevel.Diagnostic, "Request failed, retrying...\n{0}", lastException);
				await Task.Delay(500 + (i * 500), cancellationToken);
			}

			if (lastException != null)
				throw lastException;

			return response;
		}
	}
}

