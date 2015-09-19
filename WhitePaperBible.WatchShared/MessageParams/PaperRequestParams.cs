using System;
using WhitePaperBible.WatchShared;
using WhitePaperBible.Core.Models;

namespace WhitePaperBible.WatchShared.MessageParams
{
	public class PaperRequestParams : WatchMessageParams
	{
		public Paper Paper {get;set;}

		public PaperRequestParams ()
		{
		}
	}
}

