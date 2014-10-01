using System;
using MonkeyArms;
using WhitePaperBible.Core.Models;

namespace WhitePaperBible.Core.Invokers
{
	public class FaultInvokerArgs: InvokerArgs
	{
		public string[] Messages;

		public FaultInvokerArgs (string[] msgs)
		{
			Messages = msgs;
		}
	}
}

