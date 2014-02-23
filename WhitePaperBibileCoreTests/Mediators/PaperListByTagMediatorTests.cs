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
	public class PaperListByTagMediatorTests:BaseTest
	{
		[Test, Property ("Intent", "When Register is called on Mediator and Papers NOT Null, SetPapers should be called on View.")]
		public void VerifyPapersSetOnView ()
		{
			SetupAppModelToReturnPaperList ();
			TestMediator.Register ();
			MockView.Verify (view => view.SetPapers (IsTestPaperList ()), Times.Once (), TestIntent);
		}

//		[Test, Property ("Intent", "When Register is called on Mediator and AppModel.Papers IS Null, SetPapers should NOT be called on View.")]
//		public void VerifyPapersNotSetOnView ()
//		{
//			TestMediator.Register ();
//			MockView.Verify (view => view.SetPapers (IsAnyPaperList ()), Times.Never (), TestIntent);
//		}
//
//		[Test, Property ("Intent", "When OnPaperSelectedInvoker dispatches Mediator should set CurrentPaper on AppModel")]
//		public void VerifyCurrentPaperSet ()
//		{
//			TestMediator.Register ();
//			OnPaperSelectedInvoker.Invoke ();
//			MockAppModel.VerifySet (model => model.CurrentPaper = TestPaper, Times.Once (), TestIntent);
//
//		}

		//MOCKS
		Mock<IPapersByTagListView> MockView;
		Mock<AppModel> MockAppModel;
		Mock<PapersByTagReceivedInvoker> MockPapersReceived;
		//STUBS
		Invoker FilterInvoker, OnPaperSelectedInvoker;
		PapersListByTagMediator TestMediator;
		List<Paper> TestPaperList;
		List<Paper> TestFilteredPaperList;
		Paper TestPaper;
		string TestQuery = "some query";
		/*
		* HELPER METHODS
		*/
		[SetUp]
		public void Init ()
		{
			CreateViewMock ();
			TestMediator = new PapersListByTagMediator (MockView.Object);
			CreateAppModelMock ();
			CreatePapersReceivedMock ();

		}

		void CreateViewMock ()
		{
			MockView = new Mock<IPapersByTagListView> ();
			StubViewOnPaperSelectedInvoker ();
			StubViewSelectedPaper ();
//			MockView.SetupGet (view => view.SearchQuery).Returns (TestQuery);


		}

		void CreateAppModelMock ()
		{
			MockAppModel = new Mock<AppModel> ();
			TestPaperList = new List<Paper> ();
			TestMediator.AppModel = MockAppModel.Object;

			TestFilteredPaperList = new List<Paper> ();
			MockAppModel.Setup (model => model.FilterPapers (It.Is<string> (query => query == TestQuery))).Returns (TestFilteredPaperList);
		}

		void SetupAppModelToReturnPaperList ()
		{
			MockAppModel.SetupGet (model => model.Papers).Returns (TestPaperList);
		}

		void CreatePapersReceivedMock ()
		{
			MockPapersReceived = new Mock<PapersByTagReceivedInvoker> ();
			TestMediator.PapersReceived = MockPapersReceived.Object;
		}

		void StubViewOnPaperSelectedInvoker ()
		{
			OnPaperSelectedInvoker = new Invoker ();
			MockView.SetupGet (view => view.OnPaperSelected).Returns (OnPaperSelectedInvoker);
		}

		void StubViewSelectedPaper ()
		{
			TestPaper = new Paper ();
			MockView.SetupGet (view => view.SelectedPaper).Returns (TestPaper);
		}

		List<Paper> IsTestPaperList ()
		{
			return It.Is<List<Paper>> (list => list == TestPaperList);
		}

		List<Paper> IsAnyPaperList ()
		{
			return It.IsAny<List<Paper>> ();
		}

		List<Paper> IsFilteredPaperList ()
		{
			return It.Is<List<Paper>> (list => list == TestFilteredPaperList);
		}
	}
}

