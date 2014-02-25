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
	public class LoginMediatorTests:BaseTest
	{
//		[Test, Property ("Intent", "When Register is called on Mediator, SearchPlaceHolderText should be set on View")]
//		public void VerifyPlaceholdText ()
//		{
//			TestMediator.Register ();
//			MockView.VerifySet (view => view.SearchPlaceHolderText = "Search Tags", Times.Once (), TestIntent);
//		}

//		[Test, Property ("Intent", "When Register is called on Mediator and AppModel.Tags NOT Null, SetTags should be called on View.")]
//		public void VerifyTagsSetOnView ()
//		{
//			SetupAppModelToReturnTagList ();
//			TestMediator.Register ();
//			MockView.Verify (view => view.SetTags (IsTestTagList ()), Times.Once (), TestIntent);
//		}
//
//		[Test, Property ("Intent", "When Register is called on Mediator and AppModel.Tags IS Null, SetTags should NOT be called on View.")]
//		public void VerifyTagsNotSetOnView ()
//		{
//			TestMediator.Register ();
//			MockView.Verify (view => view.SetTags (IsAnyTagList ()), Times.Never (), TestIntent);
//		}
//
//		[Test, Property ("Intent", "When OnTagSelectedInvoker dispatches Mediator should set CurrentTag on AppModel")]
//		public void VerifyCurrentTagSet ()
//		{
//			TestMediator.Register ();
//			OnTagSelectedInvoker.Invoke ();
//			MockAppModel.VerifySet (model => model.CurrentTag = TestTag, Times.Once (), TestIntent);
//
//		}

		//MOCKS
		Mock<ILoginView> MockView;
		Mock<AppModel> MockAppModel;
//		Mock<LoginCompleteInvoker> MockLoginCompleteReceived;

		//STUBS
		Invoker FilterInvoker, OnTagSelectedInvoker;
		LoginMediator TestMediator;
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
			TestMediator = new LoginMediator (MockView.Object);
			CreateAppModelMock ();

		}

		void CreateViewMock ()
		{
			MockView = new Mock<ILoginView> ();
//			MockView.SetupGet (view => view.SearchQuery).Returns (TestQuery);


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

