using MonkeyArms;
using System.Collections.Generic;
using WhitePaperBible.Core.Models;

namespace WhitePaperBible.Core.Invokers
{
	public class BibleSearchResultsReceivedInvoker : Invoker
	{
	}

	public class BibleSearchResultsReceivedInvokerArgs: InvokerArgs
	{
		private List<ReferenceNode> results;

		public List<ReferenceNode> Results {
			get {
				return results;
			}
		}

		public BibleSearchResultsReceivedInvokerArgs(List<ReferenceNode> results)
		{
			this.results = results;
		}
	}
}

