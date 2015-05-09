using System;
using WhitePaperBible.WatchShared;
using System.Collections.Generic;
using WhitePaperBible.Core.Models;

namespace WhitePaperBible.WatchShared.MessageParams
{
	public class PapersResponseParams : WatchMessageParams
	{
		public List<Paper> Papers;

		public PapersResponseParams ()
		{
		}
	}
}

