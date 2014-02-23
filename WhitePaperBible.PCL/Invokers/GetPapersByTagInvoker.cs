using System;
using MonkeyArms;
using WhitePaperBible.Core.Models;
using WhitePaperBible.Core.Invokers;

namespace WhitePaperBible.Core.Invokers
{
	public class GetPapersByTagInvoker : Invoker
	{
	
	}

	public class GetPapersByTagInvokerArgs: InvokerArgs
	{
		private Tag tag;

		public Tag Tag {
			get {
				return tag;
			}
		}

		public GetPapersByTagInvokerArgs (Tag tag)
		{
			this.tag = tag;
		}
	}
}

