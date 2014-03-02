using System;
using System.Collections.Generic;
using WhitePaperBible.Core.Models;
using MonkeyArms;

namespace WhitePaperBible.Core.Views
{
	public interface ILoginView : IMediatorTarget
	{
		event EventHandler LoginSubmitted, MoreInfoClicked;

		Invoker LoginFinished{ get; }

		Invoker LoginCancelled{ get; }

		void ShowInvalidPrompt (string message);

		void GoToNextScreen ();
	}
}

