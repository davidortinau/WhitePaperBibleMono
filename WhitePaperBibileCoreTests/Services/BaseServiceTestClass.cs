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
		[Test, Property ("Intent", "When WebClient raises RequestComplete, BaseService should call HandleSuccess")]
		public void VerifyHandleSuccess ()
		{
			MockWebClient.Raise (client => client.RequestComplete += null, EventArgs.Empty);
			Service.HandleSuccessInvoked.ShouldBeTrue (TestIntent);
		}

		[Test, Property ("Intent", "When WebClient raises RequestError, BaseService should raise Fault")]
		public void VerifyFaultRaied ()
		{
			bool faultRaised = false;
			Service.Fault += (object sender, EventArgs e) => faultRaised = true;
			MockWebClient.Raise (client => client.RequestError += null, EventArgs.Empty);
			faultRaised.ShouldBeTrue (TestIntent);
		}

		Mock<IJSONWebClient> MockWebClient;
		TestBaseService Service;

		[SetUp]
		public void Init ()
		{
			MockWebClient = new Mock<IJSONWebClient> ();
			Service = new TestBaseService (MockWebClient.Object);
		}
	}

	public class TestBaseService:BaseService
	{
		public bool HandleSuccessInvoked = false;

		public TestBaseService (IJSONWebClient client) : base (client)
		{
	
		}

		#region implemented abstract members of BaseService

		protected override void HandleSuccess (object sender, EventArgs args)
		{
			HandleSuccessInvoked = true;
		}

		#endregion
	}
}

