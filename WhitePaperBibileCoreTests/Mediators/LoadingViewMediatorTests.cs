using NUnit.Framework;
using System;
using WhitePaperBible.Core.Mediators;
using Moq;
using WhitePaperBible.Core.Views;
using WhitePaperBible.Core.Invokers;
using MonkeyArms;

namespace WhitePaperBibileCoreTests
{
	[TestFixture ()]
	public class LoadingViewMediatorTests:BaseTest
	{
		[Test, Property ("Intent", "When PapersReceivedInvoker dispatches, Mediator should call OnLoadingComplete on View")]
		public void Verify_View_OnLoadingComplete ()
		{
			MockPapersReceivedInvoker.Raise (invoker => invoker.Invoked += null, InvokerArgs.Empty);
			MockView.Verify (view => view.OnLoadingComplete (), Times.Once (), TestIntent);
		}

		Mock<ILoadingView> MockView;
		LoadingViewMediator TestMediator;
		Mock<PapersReceivedInvoker> MockPapersReceivedInvoker;

		[SetUp]
		public void InitTest ()
		{
			MockView = new Mock<ILoadingView> ();
			MockPapersReceivedInvoker = new Mock<PapersReceivedInvoker> ();
			TestMediator = new LoadingViewMediator (MockView.Object);
			TestMediator.PapersReceived = MockPapersReceivedInvoker.Object;
			TestMediator.Register ();
		}
	}
}

