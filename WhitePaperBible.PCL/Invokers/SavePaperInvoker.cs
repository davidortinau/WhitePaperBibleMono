using System;
using MonkeyArms;
using WhitePaperBible.Core.Models;
using WhitePaperBible.Core.Invokers;

namespace WhitePaperBible.Core.Invokers
{
	public class SavePaperInvoker : Invoker
	{

	}

	public class SavePaperInvokerArgs: InvokerArgs
	{
		public Paper Paper;

		public SavePaperInvokerArgs(Paper paper)
		{
			this.Paper = paper;
		}
	}
}

