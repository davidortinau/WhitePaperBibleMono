using System;
using MonkeyArms;
using WhitePaperBibleCore.Invokers;
using WhitePaperBibleCore.Models;
using WhitePaperBibleCore.Services;
using WhitePaperBibleCore.Views.Mediators;
using System.Collections.Generic;

namespace WhitePaperBibleCore.Commands
{
	public class GetPapersCommand : Command
	{
		[Inject]
		public AppModel AM;

//		[Inject]
//		public PapersReceivedInvoker PapersReceived;

		// This injection throws an error
//		[Inject]
//		public PapersListMediator PM;

		public override void Execute (InvokerArgs args)
		{
			base.Execute (args);

			var svc = new PaperService();
			svc.GetPapers(onSuccess, onFail);
		}

		void onSuccess (System.Collections.Generic.List<PaperNode> paperNodes)
		{
			AM.Papers = new List<Paper> ();
			foreach(var node in paperNodes){
				AM.Papers.Add (node.paper);
			}

//			PM.SetPapers ();
			DI.Get<PapersReceivedInvoker> ().Invoke ();
		}

		void onFail (string message)
		{
			Console.WriteLine("ERROR {0}", message);
		}
	}
}

