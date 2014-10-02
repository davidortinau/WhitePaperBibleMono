using NUnit.Framework;
using System;
using MonkeyArms;
using WhitePaperBible.Core.Commands;
using Moq;
using WhitePaperBible.Core.Models;
using WhitePaperBible.Core.Invokers;
using WhitePaperBible.Core.Services;
using System.Collections.Generic;

namespace WhitePaperBibileCoreTests
{
	[TestFixture ()]
	public class GetPapersCommandTest:BaseTest
	{
		[Test, Property ("Intent", "When Command Executes, Command should Invoke PapersReceivedInvoker")]
		public void TestCase ()
		{
			GPCommand.Execute (InvokerArgs.Empty);
			MockPapersReceived.Verify (invoker => invoker.Invoke (It.IsAny<InvokerArgs> ()), Times.Once (), TestIntent);
		}

		GetPapersCommand GPCommand;
		Mock<AppModel> MockAppModel;
		Mock<PapersReceivedInvoker> MockPapersReceived;
		Mock<IGetPapersService> MockGetPapersService;

		[SetUp]
		public void Init ()
		{
			GPCommand = new GetPapersCommand ();
			CreateAppModelMock ();
			CreateInvokerMock ();
			CreateServiceMock ();
		}

		void CreateAppModelMock ()
		{
			MockAppModel = new Mock<AppModel> ();
			MockAppModel.SetupGet (model => model.Papers).Returns (new List<Paper> ());
			GPCommand.AM = MockAppModel.Object;
		}

		void CreateInvokerMock ()
		{
			MockPapersReceived = new Mock<PapersReceivedInvoker> ();
			GPCommand.PapersReceived = MockPapersReceived.Object;
		}

		void CreateServiceMock ()
		{
			MockGetPapersService = new Mock<IGetPapersService> ();
			GPCommand.Service = MockGetPapersService.Object;

			MockGetPapersService
				.Setup (service => service.Execute ())
				.Raises (service => service.Success += null, new GetPapersServiceEventArgs (TestData.PaperNodeList, null));
		}
	}
}

