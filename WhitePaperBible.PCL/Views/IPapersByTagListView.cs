using System;
using System.Collections.Generic;
using WhitePaperBible.Core.Models;
using MonkeyArms;

namespace WhitePaperBible.Core.Views
{
	public interface IPapersByTagListView : IMediatorTarget
	{
//		Invoker Filter{ get; }

		Invoker OnPaperSelected{ get; }

		void SetPapers (List<Paper> papers);

//		string SearchPlaceHolderText{ get; set; }

//		string SearchQuery{ get; }

		Paper SelectedPaper{ get; set; }

		Tag SelectedTag{get;set;}
	}
}

