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
	public class PaperListMediatorTests:BaseTest
	{
		[Test, Property ("Intent", "When Register is called on Mediator, SearchPlaceHolderText should be set on View")]
		public void VerifyPlaceholdText ()
		{
			TestMediator.Register ();
			MockView.VerifySet (view => view.SearchPlaceHolderText = "Search Papers", Times.Once (), TestIntent);
		}

		[Test, Property ("Intent", "When Register is called on Mediator and AppModel.Papers NOT Null, SetPapers should be called on View.")]
		public void VerifyPapersSetOnView ()
		{
			SetupAppModelToReturnPaperList ();
			TestMediator.Register ();
			MockView.Verify (view => view.SetPapers (IsTestPaperList ()), Times.Once (), TestIntent);
		}

		[Test, Property ("Intent", "When Register is called on Mediator and AppModel.Papers IS Null, SetPapers should NOT be called on View.")]
		public void VerifyPapersNotSetOnView ()
		{
			TestMediator.Register ();
			MockView.Verify (view => view.SetPapers (IsAnyPaperList ()), Times.Never (), TestIntent);
		}

		[Test, Property ("Intent", "When OnPaperSelectedInvoker dispatches Mediator should set CurrentPaper on AppModel")]
		public void VerifyCurrentPaperSet ()
		{
			TestMediator.Register ();
			OnPaperSelectedInvoker.Invoke ();
			MockAppModel.VerifySet (model => model.CurrentPaper = TestPaper, Times.Once (), TestIntent);

		}

		[Test, Property ("Intent", "When FilterInvoker dispatches, Mediator should call FilterPapers on AppModel and pass it to view via SetPapers")]
		public void VerifySetFilteredPapers ()
		{
			SetupAppModelToReturnPaperList ();
			TestMediator.Register ();
			FilterInvoker.Invoke ();
			MockView.Verify (view => view.SetPapers (IsFilteredPaperList ()), Times.Once (), TestIntent);
		}
		//MOCKS
		Mock<IPapersListView> MockView;
		Mock<AppModel> MockAppModel;
		Mock<PapersReceivedInvoker> MockPapersReceived;
		//STUBS
		Invoker FilterInvoker, OnPaperSelectedInvoker;
		PapersListMediator TestMediator;
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
			TestMediator = new PapersListMediator (MockView.Object);
			CreateAppModelMock ();
			CreatePapersReceivedMock ();

		}

		void CreateViewMock ()
		{
			MockView = new Mock<IPapersListView> ();
			StubViewFilterInvoker ();
			StubViewOnPaperSelectedInvoker ();
			StubViewSelectedPaper ();
			MockView.SetupGet (view => view.SearchQuery).Returns (TestQuery);


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
			MockPapersReceived = new Mock<PapersReceivedInvoker> ();
			TestMediator.PapersReceived = MockPapersReceived.Object;
		}

		void StubViewFilterInvoker ()
		{
			FilterInvoker = new Invoker ();
			MockView.SetupGet (view => view.Filter).Returns (FilterInvoker);
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

