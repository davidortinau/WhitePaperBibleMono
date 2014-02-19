using MonkeyArms;
using WhitePaperBible.Core.Invokers;

namespace WhitePaperBible.Core.Commands
{
	public class BootstrapCommand : Command
	{
		public override void Execute (InvokerArgs args)
		{
//			base.Execute (args);

			DI.MapCommandToInvoker<ConfigureModelsCommand, ConfigureModelsInvoker> ().Invoke();
			DI.MapCommandToInvoker<ConfigureViewsCommand, ConfigureViewsInvoker> ().Invoke();
			DI.MapCommandToInvoker<ConfigureInvokersCommand, ConfigureInvokersInvoker> ().Invoke();
			DI.MapCommandToInvoker<GetPapersCommand, GetPapersInvoker> ().Invoke();

			DI.MapCommandToInvoker<GetPaperDetailsCommand, GetPaperDetailsInvoker> ();
		}
	}
}

