using System;
using MonkeyArms;
using WhitePaperBibleCore.Invokers;
using WhitePaperBibleCore.Models;
using WhitePaperBibleCore.Services;
using WhitePaperBibleCore.Views.Mediators;
using System.Collections.Generic;

namespace WhitePaperBibleCore.Commands
{
	public class GetPaperDetailsCommand : Command
	{
		[Inject]
		public AppModel AM;

		public override void Execute (InvokerArgs args)
		{
			base.Execute (args);

			var svc = new PaperService();
			svc.GetPaperReferences(AM.CurrentPaper.id, onSuccess, onFail);
		}

		void onSuccess (System.Collections.Generic.List<ReferenceNode> referenceNodes)
		{
			AM.CurrentPaper.references = new List<Reference> ();
			foreach(var node in referenceNodes){
				AM.CurrentPaper.references.Add (node.reference);
			}

			DI.Get<PaperDetailsReceivedInvoker> ().Invoke ();
		}

		void onFail (string message)
		{
			Console.WriteLine("ERROR {0}", message);
		}
	}
}

