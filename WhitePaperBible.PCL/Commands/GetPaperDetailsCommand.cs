using System;
using MonkeyArms;
using WhitePaperBible.Core.Invokers;
using WhitePaperBible.Core.Models;
using WhitePaperBible.Core.Services;
using WhitePaperBible.Core.Mediators;
using System.Collections.Generic;
using System.Diagnostics;

namespace WhitePaperBible.Core.Commands
{
	public class GetPaperDetailsCommand : Command
	{
		[Inject]
		public AppModel AM;

		[Inject]
		public PaperDetailsReceivedInvoker PaperDetailsReceived;

		[Inject]
		public GetPaperReferencesService GetPaperReferences;

		public override void Execute (InvokerArgs args)
		{
			GetPaperReferences.Success += onSuccess;
			GetPaperReferences.Execute (AM.CurrentPaper.id);
		}

		void onSuccess (object sender, EventArgs args)
		{
			var references = new List<Reference> ();
			foreach (var node in (args as GetPaperReferencesServiceEventArgs).References) {
				references.Add (node.reference);
			}
			AM.CurrentPaper.references = references;

			PaperDetailsReceived.Invoke ();
		}
	}
}

