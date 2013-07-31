using System;
using System.Collections.Generic;
using WhitePaperBibleCore.Models;
using MonkeyArms;

namespace WhitePaperBible.Core.Views
{
	public interface IPapersListView : IMediatorTarget
	{
		event EventHandler Filter;

		void SetPapers (List<Paper> papers);

		string SearchPlaceHolderText{get;set;}

		string SearchQuery{get;}
	}
}

