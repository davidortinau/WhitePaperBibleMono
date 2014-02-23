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
	public class GetTagsServiceTests:BaseServiceTestClass
	{
		[Test, Property ("Intent", "When Execute is called on Service, OpenURL should be called on WebClient")]
		public void VerifyWebClientOpenURL ()
		{
			Service.Execute ();
			VerifyOpenedURLContains ("tag.json");
		}

		[Test, Property ("Intent", "When WebClient raises Complete, Service should raise Success")]
		public void VerifySuccessRaised ()
		{
			bool successRaisedAndValid = false;
			Service.Success += (object sender, EventArgs e) => successRaisedAndValid = VerifyResultIsValid (e);
			MockWebClientSuccessResponseText (TestData.TagsJSON);
			RaiseRequestComplete ();
			successRaisedAndValid.ShouldBeTrue (TestIntent);
		}

		GetTagsService Service;

		[SetUp]
		public void Init ()
		{
			Service = InitService<GetTagsService> ();
		}
		/*
		 * HELPER METHODS
		 */
		bool VerifyResultIsValid (EventArgs e)
		{
			var args = ((GetTagsServiceEventArgs)e);
			args.Tags.Count.ShouldEqual (TestData.TagNodeList.Count, TestIntent);
			args.Tags [0].tag.name.ShouldEqual (TestData.TagNodeList [0].tag.name, TestIntent);
			return true;
		}
	}
}

