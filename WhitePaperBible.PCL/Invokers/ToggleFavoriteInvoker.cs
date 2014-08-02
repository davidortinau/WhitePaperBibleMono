using System;
using MonkeyArms;
using WhitePaperBible.Core.Models;
using WhitePaperBible.Core.Invokers;

namespace WhitePaperBible.Core.Invokers
{
	public class ToggleFavoriteInvoker : Invoker
	{

	}

	public class ToggleFavoriteInvokerArgs: InvokerArgs
	{
		public Paper Paper;
		public bool IsFavorite;

		public ToggleFavoriteInvokerArgs(Paper paper, bool isFavorite)
		{
			this.Paper = paper;
			this.IsFavorite = isFavorite;
		}
	}
}

