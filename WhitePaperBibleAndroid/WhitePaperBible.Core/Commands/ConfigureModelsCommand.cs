using System;
using MonkeyArms;
using WhitePaperBibleCore.Models;
using WhitePaperBibleCore.Invokers;

namespace WhitePaperBibleCore.Commands
{
	public class ConfigureModelsCommand : Command
	{
		public override void Execute (InvokerArgs args)
		{
			base.Execute (args);

			DI.MapSingleton <AppModel>();
		}
	}
}

