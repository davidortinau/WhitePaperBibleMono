using System;
using MonkeyArms;
using WhitePaperBible.Core.Models;
using WhitePaperBible.Core.Invokers;

namespace WhitePaperBible.Core.Commands
{
	public class ConfigureModelsCommand : Command
	{
		public override void Execute (InvokerArgs args)
		{
//			base.Execute (args);

			DI.MapSingleton <AppModel>();
		}
	}
}

