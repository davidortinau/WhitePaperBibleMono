using NUnit.Framework;
using System;
using WhitePaperBible.Core.Views;
using Moq;
using MonkeyArms;
using WhitePaperBible.Core.Mediators;
using WhitePaperBible.Core.Models;
using WhitePaperBible.Core.Invokers;
using Mono.Security.X509;
using System.Collections.Generic;
using System.Net.Configuration;

namespace WhitePaperBibileCoreTests
{
	[TestFixture ()]
	public class TagListMediatorTests:BaseTest
	{
		[Test, Property ("Intent", "When Register is called on Mediator, SearchPlaceHolderText should be set on View")]
		public void VerifyPlaceholdText ()
		{
			TestMediator.Register ();
			MockView.VerifySet (view => view.SearchPlaceHolderText = "Search Tags", Times.Once (), TestIntent);
		}

		[Test, Property ("Intent", "When Register is called on Mediator and AppModel.Tags NOT Null, SetTags should be called on View.")]
		public void VerifyTagsSetOnView ()
		{
			SetupAppModelToReturnTagList ();
			TestMediator.Register ();
			MockView.Verify (view => view.SetTags (IsTestTagList ()), Times.Once (), TestIntent);
		}

		[Test, Property ("Intent", "When Register is called on Mediator and AppModel.Tags IS Null, SetTags should NOT be called on View.")]
		public void VerifyTagsNotSetOnView ()
		{
			TestMediator.Register ();
			MockView.Verify (view => view.SetTags (IsAnyTagList ()), Times.Never (), TestIntent);
		}

		[Test, Property ("Intent", "When OnTagSelectedInvoker dispatches Mediator should set CurrentTag on AppModel")]
		public void VerifyCurrentTagSet ()
		{
			TestMediator.Register ();
			OnTagSelectedInvoker.Invoke ();
			MockAppModel.VerifySet (model => model.CurrentTag = TestTag, Times.Once (), TestIntent);

		}

		[Test, Property ("Intent", "When FilterInvoker dispatches, Mediator should call FilterTags on AppModel and pass it to view via SetTags")]
		public void VerifySetFilteredTags ()
		{
			SetupAppModelToReturnTagList ();
			TestMediator.Register ();
			FilterInvoker.Invoke ();
			MockView.Verify (view => view.SetTags (IsFilteredTagList ()), Times.Once (), TestIntent);
		}
		//MOCKS
		Mock<ITagsListView> MockView;
		Mock<AppModel> MockAppModel;
		Mock<TagsReceivedInvoker> MockTagsReceived;
		//STUBS
		Invoker FilterInvoker, OnTagSelectedInvoker;
		TagsListMediator TestMediator;
		List<Tag> TestTagList;
		List<Tag> TestFilteredTagList;
		Tag TestTag;
		string TestQuery = "some query";
		/*
		* HELPER METHODS
		*/
		[SetUp]
		public void Init ()
		{
			CreateViewMock ();
			TestMediator = new TagsListMediator (MockView.Object);
			CreateAppModelMock ();
			CreateTagsReceivedMock ();

		}

		void CreateViewMock ()
		{
			MockView = new Mock<ITagsListView> ();
			StubViewFilterInvoker ();
			StubViewOnTagSelectedInvoker ();
			StubViewSelectedTag ();
			MockView.SetupGet (view => view.SearchQuery).Returns (TestQuery);


		}

		void CreateAppModelMock ()
		{
			MockAppModel = new Mock<AppModel> ();
			TestTagList = new List<Tag> ();
			TestMediator.AppModel = MockAppModel.Object;

			TestFilteredTagList = new List<Tag> ();
			MockAppModel.Setup (model => model.FilterTags (It.Is<string> (query => query == TestQuery))).Returns (TestFilteredTagList);
		}

		void SetupAppModelToReturnTagList ()
		{
			MockAppModel.SetupGet (model => model.Tags).Returns (TestTagList);
		}

		void CreateTagsReceivedMock ()
		{
			MockTagsReceived = new Mock<TagsReceivedInvoker> ();
			TestMediator.TagsReceived = MockTagsReceived.Object;
		}

		void StubViewFilterInvoker ()
		{
			FilterInvoker = new Invoker ();
			MockView.SetupGet (view => view.Filter).Returns (FilterInvoker);
		}

		void StubViewOnTagSelectedInvoker ()
		{
			OnTagSelectedInvoker = new Invoker ();
			MockView.SetupGet (view => view.OnTagSelected).Returns (OnTagSelectedInvoker);
		}

		void StubViewSelectedTag ()
		{
			TestTag = new Tag ();
			MockView.SetupGet (view => view.SelectedTag).Returns (TestTag);
		}

		List<Tag> IsTestTagList ()
		{
			return It.Is<List<Tag>> (list => list == TestTagList);
		}

		List<Tag> IsAnyTagList ()
		{
			return It.IsAny<List<Tag>> ();
		}

		List<Tag> IsFilteredTagList ()
		{
			return It.Is<List<Tag>> (list => list == TestFilteredTagList);
		}
	}
}

