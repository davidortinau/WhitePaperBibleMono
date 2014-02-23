using MonkeyArms;
using System.Collections.Generic;
using WhitePaperBible.Core.Models;

namespace WhitePaperBible.Core.Invokers
{
	public class PapersByTagReceivedInvoker : Invoker
	{

	}

	public class PapersReceivedInvokerArgs: InvokerArgs
	{
		private List<Paper> papers;

		public List<Paper> Papers {
			get {
				return papers;
			}
		}

		public PapersReceivedInvokerArgs(List<Paper> papers)
		{
			this.papers = papers;
		}
	}
}

