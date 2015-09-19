using System;
using MonkeyArms;
using WhitePaperBible.Core.Models;
using WhitePaperBible.Core.Invokers;

namespace WhitePaperBible.Core.Invokers
{
	public class DeletePaperInvoker : Invoker
	{

	}

	public class DeletePaperInvokerArgs: InvokerArgs
	{
		public Paper Paper;

		public DeletePaperInvokerArgs(Paper paper)
		{
			this.Paper = paper;
		}
	}
}

