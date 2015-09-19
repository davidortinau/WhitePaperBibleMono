using NUnit.Framework;
using System;
using Moq;
using WhitePaperBible.Core.Services;
using Newtonsoft.Json;
using Should;
using WhitePaperBible.Core.Models;
using System.Collections.Generic;

namespace WhitePaperBibileCoreTests
{
	[TestFixture ()]
	public class GetFavoritesServiceTests:BaseServiceTestClass
	{
		protected GetFavoritesService Service;

		[Test, Property ("Intent", "When Execute is called on Service, OpenURL should be called on WebClient")]
		public void VerifyWebClientOpenURL ()
		{
			Service.Execute ();
			VerifyOpenedURLContains ("favorite/index/");

		}

		[Test, Property ("Intent", "When WebClient raises Complete, Service should raise Success")]
		public void VerifySuccessRaised ()
		{
			bool successRaisedAndValid = false;
			Service.Success += (object sender, EventArgs e) => successRaisedAndValid = VerifyResultIsValid (e);
			MockWebClientSuccessResponseText (TestData.PapersJSON);
			RaiseRequestComplete ();
			successRaisedAndValid.ShouldBeTrue (TestIntent);
		}

		[SetUp]
		public void Init ()
		{
			Service = InitService<GetFavoritesService> ();
		}
		/*
		 * HELPER METHODS
		 */
		bool VerifyResultIsValid (EventArgs e)
		{
			var args = ((GetPapersServiceEventArgs)e);
			args.Papers.Count.ShouldEqual (TestData.PaperNodeList.Count, TestIntent);
			args.Papers [0].Index.ShouldEqual (TestData.PaperNodeList [0].Index, TestIntent);
			return true;
		}
	}
}

