using System;
using MonkeyArms;
using WhitePaperBibleCore.Models;
using WhitePaperBibleCore.Invokers;
using WhitePaperBible.Android.Commands;
using WhitePaperBibleCore.Commands;
using WhitePaperBible.Android.Invokers;
using WhitePaperBible.Core.Invokers;

namespace WhitePaperBible.Android.Commands
{
	public class BootstrapCommand : Command
	{
		public override void Execute (InvokerArgs args)
		{
			base.Execute (args);

			DI.MapCommandToInvoker<ConfigureModelsCommand, ConfigureModelsInvoker> ().Invoke();
			DI.MapCommandToInvoker<ConfigureViewsCommand, ConfigureViewsInvoker> ().Invoke();
			DI.MapCommandToInvoker<ConfigureInvokersCommand, ConfigureInvokersInvoker> ().Invoke();
			DI.MapCommandToInvoker<GetPapersCommand, GetPapersInvoker> ().Invoke();


//			DI.Get<ConfigureModelsInvoker>().Invoke ();
//			DI.Get<ConfigureViewsInvoker>().Invoke ();
//			DI.Get<ConfigureInvokersInvoker>().Invoke ();
//
//			DI.Get<GetPapersInvoker>().Invoke ();

		}
	}
}

