using System;
using MonkeyArms;
using WhitePaperBible.Core.Invokers;
using WhitePaperBible.Core.Models;
using WhitePaperBible.Core.Services;
using WhitePaperBible.Core.Views.Mediators;
using System.Collections.Generic;

namespace WhitePaperBible.Core.Commands
{
	public class GetPapersCommand : Command
	{
		[Inject]
		public AppModel AM;

		public override void Execute (InvokerArgs args)
		{
			var svc = new PaperService();
			svc.GetPapers(onSuccess, onFail);
		}

		void onSuccess (System.Collections.Generic.List<PaperNode> paperNodes)
		{
			AM.Papers = new List<Paper> ();
			foreach(var node in paperNodes){
				AM.Papers.Add (node.paper);
			}

			DI.Get<PapersReceivedInvoker> ().Invoke ();
		}

		void onFail (string message)
		{
			Console.WriteLine("ERROR {0}", message);
		}
	}
}

