using System;
using System.Collections.Generic;
using WhitePaperBible.Core.Models;
using MonkeyArms;

namespace WhitePaperBible.Core.Views
{
	public interface IEditPaperView : IMediatorTarget
	{
		void DismissController (bool deleted);

		void SetPaper (Paper paper);

		void DismissLoginPrompt ();

		void PromptForLogin ();

		Invoker Save {get;}

		Invoker Delete {get;}

	}
}

