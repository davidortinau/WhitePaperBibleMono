using NUnit.Framework;
using System;
using WhitePaperBible.Core.Services;
using Should;

namespace WhitePaperBibileCoreTests
{
	[TestFixture ()]
	public class GetPapersByTagServiceTests:BaseServiceTestClass
	{
		[Test, Property ("Intent", "When Execute is called, service should open url with correct API mehtod and params.")]
		public void VerifyURLOpened ()
		{
			Service.Execute (32);
			VerifyOpenedURLContains ("papers/tagged/32");
		}

		[Test, Property ("Intent", "When WebClient raises success, verify service dispatches EventArgs with List of PaperNodes")]
		public void VerifyEventArgs ()
		{
			bool eventRaised = false;
			Service.Success += (object sender, EventArgs e) => eventRaised = ValidateEventArgs (e as GetPapersByTagServiceEventArgs);
			MockWebClientSuccessResponseText (TestData.PapersJSON);
			RaiseRequestComplete ();
			eventRaised.ShouldBeTrue (TestIntent);
		}

		bool ValidateEventArgs (GetPapersByTagServiceEventArgs args)
		{
			args.Papers.Count.ShouldEqual (TestData.PaperNodeList.Count);
			return true;
		}

		GetPapersByTagService Service;

		[SetUp]
		public void Init ()
		{
			Service = InitService<GetPapersByTagService> ();
		}
	}
}

