using NUnit.Framework;
using System;
using WhitePaperBible.Core.Services;
using Moq;
using Should;

namespace WhitePaperBibileCoreTests
{
	[TestFixture ()]
	public class BaseServiceTestClass:BaseTest
	{
		protected Mock<IJSONWebClient> MockWebClient;
		//		[Test, Property ("Intent", "When WebClient raises RequestError, BaseService should raise Fault")]
		//		public void VerifyFaultRaied ()
		//		{
		//			bool faultRaised = false;
		//			Service.Fault += (object sender, EventArgs e) => faultRaised = true;
		//			MockWebClient.Raise (client => client.RequestError += null, EventArgs.Empty);
		//			faultRaised.ShouldBeTrue (TestIntent);
		//		}
		protected TService InitService<TService> () where TService:class
		{
			MockWebClient = new Mock<IJSONWebClient> ();
			TService Service = Activator.CreateInstance<TService> ();
			(Service as BaseService).Client = MockWebClient.Object;
			return Service;
		}

		protected void VerifyOpenedURLContains (string methodName)
		{
			MockWebClient.Verify (client => client.OpenURL (UrlContains (methodName), IsGetMethod ()), Times.Once (), TestIntent);
		}

		protected string UrlContains (string methodName)
		{
			return It.Is<string> (url => url.Contains (methodName));
		}

		protected bool IsGetMethod ()
		{
			return It.Is<bool> (b => b == false);
		}

		protected bool IsPostMethod ()
		{
			return It.Is<bool> (b => b == true);
		}

		protected void MockWebClientSuccessResponseText (string jsonResponse)
		{
			MockWebClient.SetupGet (client => client.ResponseText).Returns (jsonResponse);
		}

		protected void RaiseRequestComplete ()
		{
			MockWebClient.Raise (client => client.RequestComplete += null, EventArgs.Empty);
		}
	}
}

