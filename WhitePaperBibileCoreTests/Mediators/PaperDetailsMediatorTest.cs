using NUnit.Framework;
using System;
using WhitePaperBible.Core.Views;
using Moq;
using MonkeyArms;
using WhitePaperBible.Core.Mediators;
using WhitePaperBible.Core.Invokers;
using WhitePaperBible.Core.Models;
using Should;

namespace WhitePaperBibileCoreTests
{
	[TestFixture ()]
	public class PaperDetailsMediatorTest:BaseTest
	{
		[Test, Property ("Intent", "On Register, Mediator should invoke GetPaperDetails.")]
		public void VerifyGetPaperDetailsInvoked ()
		{
			MockGetPaperDetailsInvoker.Verify (invoker => invoker.Invoke (It.IsAny<InvokerArgs> ()), Times.Once (), TestIntent);
		}

		[Test, Property ("Intent", "When PaperDetailsReceivedInvoker dispatches and AppModel.CurrentPaper NOT Null, Mediator should call SetPaper on View")]
		public void VerifySetPaper ()
		{
			MockAppModel.SetupGet (AppModel => AppModel.CurrentPaper).Returns (new Paper ());
			MockPaperDetailsReceivedInvoker.Raise (invoker => invoker.Invoked += null, InvokerArgs.Empty);
			MockView.Verify (view => view.SetPaper (It.IsAny<Paper> ()), Times.Once, TestIntent);
		}

		[Test, Property ("Intent", "When PaperDetailsReceivedInvoker dispatches and AppModel.CurrentPaper is Null, Mediator should NOT call SetPaper on View")]
		public void VerifyNoSetPaper ()
		{

			MockPaperDetailsReceivedInvoker.Raise (invoker => invoker.Invoked += null, InvokerArgs.Empty);
			MockView.Verify (view => view.SetPaper (It.IsAny<Paper> ()), Times.Never, TestIntent);
		}

		[Test, Property ("Intent", "When UnRegister is called on Mediator, Mediator should remove listener from GetPaperDetailsInvoker.")]
		public void VerifyInvokerMapEmpty ()
		{
			TestMediator.GetInvokerMapHandlerCount (MockPaperDetailsReceivedInvoker.Object).ShouldEqual (1, TestIntent);
			TestMediator.Unregister ();
			TestMediator.GetInvokerMapHandlerCount (MockPaperDetailsReceivedInvoker.Object).ShouldEqual (0, TestIntent);
		}

		Mock<IPaperDetailView> MockView;
		PaperDetailMediator TestMediator;
		Mock<GetPaperDetailsInvoker> MockGetPaperDetailsInvoker;
		Mock<PaperDetailsReceivedInvoker> MockPaperDetailsReceivedInvoker;
		Mock<AppModel> MockAppModel;

		[SetUp]
		public void Init ()
		{
			MockView = new Mock<IPaperDetailView> ();
			TestMediator = new PaperDetailMediator (MockView.Object);

			CreatePaperDetailsReceivedMock ();
			CreateGetPaperDetailsMock ();
			CreateAppModelMock ();

			TestMediator.Register ();
		}

		void CreatePaperDetailsReceivedMock ()
		{
			MockPaperDetailsReceivedInvoker = new Mock<PaperDetailsReceivedInvoker> ();
			TestMediator.PaperDetailsReceived = MockPaperDetailsReceivedInvoker.Object;
		}

		void CreateGetPaperDetailsMock ()
		{
			MockGetPaperDetailsInvoker = new Mock<GetPaperDetailsInvoker> ();
			TestMediator.GetPaperDetails = MockGetPaperDetailsInvoker.Object;
		}

		void CreateAppModelMock ()
		{
			MockAppModel = new Mock<AppModel> ();
			TestMediator.AppModel = MockAppModel.Object;
		}
	}
}

