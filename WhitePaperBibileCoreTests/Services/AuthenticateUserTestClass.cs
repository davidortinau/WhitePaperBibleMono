using NUnit.Framework;
using System;
using WhitePaperBible.Core.Services;
using Should;

namespace WhitePaperBibileCoreTests
{
	[TestFixture ()]
	public class AuthenticateUserTestClass:BaseServiceTestClass
	{
		protected AuthenticateUserService ServiceUT;

		[Test, Property ("Intent", "When Execute is called on Service, OpenURL should be called on WebClient")]
		public void VerifyOpenedURL ()
		{
			ServiceUT.Execute ("user@gmail.com", "letmein");
			VerifyOpenedURLContains ("/user_sessions/?user_session[username]=user@gmail.com&user_session[password]=letmein", true);

		}

		[Test, Property ("Intent", "When WebClient raises Complete, Service should raise Success with successful event args")]
		public void VerifySuccessfulAuthentication ()
		{
			bool successRaisedAndValid = false;
			ServiceUT.Success += (object sender, EventArgs e) => successRaisedAndValid = VerifySuccessfulAuthentication (e);
			MockWebClientSuccessResponseText (TestData.AuthenticationSuccess);
			RaiseRequestComplete ();
			successRaisedAndValid.ShouldBeTrue (TestIntent);
		}

		[Test, Property ("Intent", "When WebClient raises Complete, Service should raise Success with failed event args")]
		public void VerifyFailedAuthentication ()
		{
			bool successRaisedAndValid = false;
			ServiceUT.Success += (object sender, EventArgs e) => successRaisedAndValid = VerifyFailedAuthentication (e);
			MockWebClientSuccessResponseText (TestData.AuthenticationFauilure);
			RaiseRequestComplete ();
			successRaisedAndValid.ShouldBeTrue (TestIntent);
		}

		[SetUp]
		public void Init ()
		{
			ServiceUT = InitService<AuthenticateUserService> ();
		}

		bool VerifySuccessfulAuthentication (EventArgs e)
		{
			AuthenticateUserServiceEventArgs args = (AuthenticateUserServiceEventArgs)e;
			return args.Success;
		}

		bool VerifyFailedAuthentication (EventArgs e)
		{
			AuthenticateUserServiceEventArgs args = (AuthenticateUserServiceEventArgs)e;
			return !args.Success;
		}
	}
}

