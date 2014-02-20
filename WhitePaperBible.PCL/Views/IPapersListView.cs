using System;
using System.Collections.Generic;
using WhitePaperBible.Core.Models;
using MonkeyArms;

namespace WhitePaperBible.Core.Views
{
	public interface IPapersListView : IMediatorTarget
	{
		event EventHandler Filter;

		event EventHandler OnPaperSelected;

		void SetPapers (List<Paper> papers);

		string SearchPlaceHolderText{get;set;}

		string SearchQuery{get;}

		Paper SelectedPaper{get;set;}
	}
}

