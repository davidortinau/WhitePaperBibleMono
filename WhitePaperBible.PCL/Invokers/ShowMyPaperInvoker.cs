using System;
using MonkeyArms;
using WhitePaperBible.Core.Models;
using WhitePaperBible.Core.Invokers;

namespace WhitePaperBible.Core.Invokers
{
	public class ShowMyPaperInvoker : Invoker
	{
	}

	public class ShowMyPaperInvokerArgs: InvokerArgs
	{
		public Paper Paper;

		public ShowMyPaperInvokerArgs(Paper paper)
		{
			this.Paper = paper;
		}
	}
}

