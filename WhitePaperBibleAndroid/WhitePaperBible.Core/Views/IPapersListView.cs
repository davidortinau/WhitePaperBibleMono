using System;
using System.Collections.Generic;
using WhitePaperBibleCore.Models;

namespace WhitePaperBible.Core.Views
{
	public interface IPapersListView
	{
		void SetPapers (List<Paper> papers);
	}
}

