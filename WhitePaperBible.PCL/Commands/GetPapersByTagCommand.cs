using System;
using MonkeyArms;
using WhitePaperBible.Core.Invokers;
using WhitePaperBible.Core.Models;
using WhitePaperBible.Core.Services;
using System.Collections.Generic;

namespace WhitePaperBible.Core.Commands
{
	public class GetPapersByTagCommand : Command
	{
		[Inject]
		public AppModel AM;
		[Inject]
		public IGetPapersByTagService Service;
		[Inject]
		public PapersByTagReceivedInvoker PapersReceived;

		public override void Execute (InvokerArgs args)
		{
			Service.Success += onSuccess;
			Service.Execute ((args as GetPapersByTagInvokerArgs).Tag.name);
		}

		void onSuccess (object sender, EventArgs args)
		{
			var papers = new List<Paper> ();
			foreach (var node in ((GetPapersByTagServiceEventArgs)args).Papers) {
				papers.Add (node.paper);
			}

			PapersReceived.Invoke (new PapersReceivedInvokerArgs(papers));
		}
	}
}

