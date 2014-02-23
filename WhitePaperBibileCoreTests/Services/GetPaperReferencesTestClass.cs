using NUnit.Framework;
using System;
using WhitePaperBible.Core.Services;
using Should;

namespace WhitePaperBibileCoreTests
{
	[TestFixture ()]
	public class GetPaperReferencesTestClass:BaseServiceTestClass
	{
		[Test, Property ("Intent", "When Execute is called on Service, it should open url with paperID and proper API method")]
		public void VerifyExecute ()
		{
			Service.Execute (23);
			VerifyOpenedURLContains ("23");
			VerifyOpenedURLContains ("papers/");
			VerifyOpenedURLContains ("references.json");
		}

		[Test, Property ("Intent", "When Success raised, service should pass args with parsed References")]
		public void VerifySuccess ()
		{
			bool successRaised = false;
			Service.Success += (sender, e) => successRaised = VerifyPayload (e as GetPaperReferencesServiceEventArgs);
			MockWebClientSuccessResponseText (TestData.ReferenceJSON);
			RaiseRequestComplete ();

		}

		protected GetPaperReferencesService Service;
		protected int PaperID = 23;

		[SetUp]
		public void Init ()
		{
			Service = InitService<GetPaperReferencesService> ();
		}

		bool VerifyPayload (GetPaperReferencesServiceEventArgs args)
		{
			args.References [0].reference.ShouldEqual (TestData.ReferenceNodeList [0].reference);
			return true;
		}
	}
}

