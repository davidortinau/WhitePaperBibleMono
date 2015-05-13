using System;
using WhitePaperBible.WatchShared;
using System.Collections.Generic;
using WhitePaperBible.Core.Models;

namespace WhitePaperBible.WatchShared.MessageParams
{
	public class PaperResponseParams : WatchMessageParams
	{
		public Paper Paper;

		public PaperResponseParams ()
		{
		}
	}
}

