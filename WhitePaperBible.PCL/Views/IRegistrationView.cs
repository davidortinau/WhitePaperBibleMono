using System;
using System.Collections.Generic;
using WhitePaperBible.Core.Models;
using MonkeyArms;

namespace WhitePaperBible.Core.Views
{
	public interface IRegistrationView : IMediatorTarget
	{
		void DismissView ();

		Invoker Register { get; }

		void DisplayError (string msg);
	}
}

