using System;
using MonkeyArms;
using WhitePaperBible.Core.Invokers;
using WhitePaperBible.Core.Models;
using WhitePaperBible.Core.Services;
using System.Collections.Generic;

namespace WhitePaperBible.Core.Commands
{
	public class LogoutCommand : Command
	{
		[Inject]
		public AppModel AM;

		[Inject]
		public SaveStorageInvoker SaveStorage;

		public override void Execute (InvokerArgs args)
		{
			AM.ClearCredentials ();
			SaveStorage.Invoke ();
		}
	}
}

