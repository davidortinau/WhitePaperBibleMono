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
	public class GetTagsServiceTests:BaseTest
	{
		[Test, Property ("Intent", "When Execute is called on Service, OpenURL should be called on WebClient")]
		public void VerifyWebClientOpenURL ()
		{
			Service.Execute ();
			MockWebClient.Verify (client => client.OpenURL (UrlIsValid ()));
		}

		[Test, Property ("Intent", "When WebClient raises Complete, Service should raise Success")]
		public void VerifySuccessRaised ()
		{
			bool successRaisedAndValid = false;
			Service.Success += (object sender, EventArgs e) => successRaisedAndValid = VerifyResultIsValid (e);
			MockWebClientSuccessResponseText ();
			MockWebClient.Raise (client => client.RequestComplete += null, EventArgs.Empty);
			successRaisedAndValid.ShouldBeTrue (TestIntent);
		}

		Mock<IJSONWebClient> MockWebClient;
		GetTagsService Service;

		[SetUp]
		public void Init ()
		{
			MockWebClient = new Mock<IJSONWebClient> ();
			Service = new GetTagsService ();
			Service.Client = MockWebClient.Object;
		}
		/*
		 * HELPER METHODS
		 */
		static string UrlIsValid ()
		{
			return It.Is<string> (url => url.Contains ("tag.json"));
		}

		bool VerifyResultIsValid (EventArgs e)
		{
			var args = ((GetTagsServiceEventArgs)e);
			args.Tags.Count.ShouldEqual (TestData.TagNodeList.Count, TestIntent);
//			args.Tags [0].Index.ShouldEqual (TestData.TagNodeList [0].Index, TestIntent);
			return true;
		}

		void MockWebClientSuccessResponseText ()
		{
			MockWebClient.SetupGet (client => client.ResponseText).Returns (TestData.TagsJSON);
		}
	}
}

