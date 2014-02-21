using System;
using MonkeyArms;
using WhitePaperBible.Core.Invokers;
using WhitePaperBible.Core.Models;
using WhitePaperBible.Core.Services;
using System.Collections.Generic;

namespace WhitePaperBible.Core.Commands
{
	public class GetPapersCommand : Command
	{
		[Inject]
		public AppModel AM;
		[Inject]
		public IGetPapersService Service;
		[Inject]
		public PapersReceivedInvoker PapersReceived;

		public override void Execute (InvokerArgs args)
		{
			Service.Success += onSuccess;
			Service.Execute ();
		}

		void onSuccess (object sender, EventArgs args)
		{
			AM.Papers = new List<Paper> ();
			foreach (var node in ((GetPapersServiceEventArgs)args).Papers) {
				AM.Papers.Add (node.paper);
			}

			PapersReceived.Invoke ();
		}
	}
}

