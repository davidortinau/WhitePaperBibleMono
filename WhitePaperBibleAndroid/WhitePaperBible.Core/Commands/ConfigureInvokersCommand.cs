using System;
using MonkeyArms;
using WhitePaperBibleCore.Models;
using WhitePaperBibleCore.Invokers;

namespace WhitePaperBibleCore.Commands
{
	public class ConfigureInvokersCommand : Command
	{
		public override void Execute (InvokerArgs args)
		{
			base.Execute (args);

			DI.MapSingleton<PapersReceivedInvoker> ();
		}
	}
}

