using System;
using MonkeyArms;
using WhitePaperBible.Core.Invokers;
using WhitePaperBible.Core.Models;
using WhitePaperBible.Core.Services;
using System.Collections.Generic;

namespace WhitePaperBible.Core.Commands
{
	public class GetMyPapersCommand : Command
	{
		[Inject]
		public AppModel AM;

		[Inject]
		public IGetMyPapersService Service;

		[Inject]
		public MyPapersReceivedInvoker PapersReceived;

		public override void Execute (InvokerArgs args)
		{
			Service.Success += onSuccess;
			Service.Execute ();
		}

		void onSuccess (object sender, EventArgs args)
		{
			AM.MyPapers = new List<Paper> ();
			foreach (var node in ((GetMyPapersServiceEventArgs)args).Papers) {
				AM.MyPapers.Add (node.paper);
			}

			PapersReceived.Invoke ();
		}
	}
}

