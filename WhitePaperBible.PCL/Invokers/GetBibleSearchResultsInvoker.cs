using System;
using MonkeyArms;
using WhitePaperBible.Core.Enums;
using WhitePaperBible.Core.Models;
using WhitePaperBible.Core.Invokers;

namespace WhitePaperBible.Core.Invokers
{
	public class GetBibleSearchResultsInvoker : Invoker
	{
	}

	public class GetBibleSearchResultsInvokerArgs: InvokerArgs
	{
		public string Keywords;

		public int Scope;

		public GetBibleSearchResultsInvokerArgs(string keywords, int scope)
		{
			this.Keywords = keywords;
			this.Scope = scope;
		}
	}
}

