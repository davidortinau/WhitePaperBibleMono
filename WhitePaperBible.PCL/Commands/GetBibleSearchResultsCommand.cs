using System;
using MonkeyArms;
using WhitePaperBible.Core.Invokers;
using WhitePaperBible.Core.Models;
using WhitePaperBible.Core.Services;
using System.Collections.Generic;

namespace WhitePaperBible.Core.Commands
{
	public class GetBibleSearchResultsCommand : Command
	{
		[Inject]
		public AppModel AM;

		[Inject]
		public IBibleSearchService Service;

		[Inject]
		public BibleSearchResultsReceivedInvoker Received;

		public override void Execute (InvokerArgs args)
		{
			Service.Success += onSuccess;
			var bibleArgs = (args as GetBibleSearchResultsInvokerArgs);
			Service.Execute (bibleArgs.Keywords, bibleArgs.Scope);
		}

		void onSuccess (object sender, EventArgs args)
		{
			var a = ((BibleSearchServiceEventArgs)args).Results;
			Received.Invoke ( new BibleSearchResultsReceivedInvokerArgs ( a ) );
		}
	}
}

