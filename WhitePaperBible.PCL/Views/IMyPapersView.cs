using System;
using System.Collections.Generic;
using WhitePaperBible.Core.Models;
using MonkeyArms;

namespace WhitePaperBible.Core.Views
{
	public interface IMyPapersView : IMediatorTarget
	{
        void SetUserProfile (AppUser user);

		Invoker Filter{ get; }

		Invoker OnPaperSelected{ get; }

//		Invoker OnLogoutRequested{ get; }

		void SetPapers (List<Paper> papers);

		string SearchPlaceHolderText{ get; set; }

		string SearchQuery{ get; }

		Paper SelectedPaper{ get; set; }
	}
}

