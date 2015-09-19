using System;
using System.Collections.Generic;
using WhitePaperBible.Core.Models;
using MonkeyArms;

namespace WhitePaperBible.Core.Views
{
	public interface IMyPapersAndProfileView : IMediatorTarget
	{
		void ShowPaper (Paper paper);

		void DismissLoginPrompt ();

		void PromptForLogin ();

		Invoker Logout { get; }
	}
}

