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

		Invoker RegistrationClosed{ get; }

		void ShowInvalidPrompt (string message);

		string UserName { get; }

		string Password { get; }

		void ShowBusyIndicator();

		void HideBusyIndicator();

		void Dismiss ();
	}
}

