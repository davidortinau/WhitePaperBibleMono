using System;
using System.Collections.Generic;
using WhitePaperBible.Core.Models;
using MonkeyArms;

namespace WhitePaperBible.Core.Views
{
	public interface IPapersListView : IMediatorTarget
	{
		void AddPaperEditView();

//		Invoker Filter{ get; }
//
//		Invoker OnPaperSelected{ get; }

		Invoker AddPaper{ get; }

		void SetPapers (List<Paper> papers);

		string SearchPlaceHolderText{ get; set; }

		string SearchQuery{ get; }

		Paper SelectedPaper{ get; set; }

		void PromptForLogin ();

		void DismissLoginPrompt();

	}
}

