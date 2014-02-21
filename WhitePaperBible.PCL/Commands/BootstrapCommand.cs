using WhitePaperBible.Core.Invokers;
using MonkeyArms;
using WhitePaperBible.Core.Services;

namespace WhitePaperBible.Core.Commands
{
	public class BootstrapCommand : MonkeyArms.Command
	{
		public override void Execute (InvokerArgs args)
		{

			DI.MapClassToInterface<GetPapersService, IGetPapersService> ();

			DI.MapCommandToInvoker<ConfigureModelsCommand, ConfigureModelsInvoker> ().Invoke ();
			DI.MapCommandToInvoker<ConfigureViewsCommand, ConfigureViewsInvoker> ().Invoke ();
			DI.MapCommandToInvoker<ConfigureInvokersCommand, ConfigureInvokersInvoker> ().Invoke ();
			DI.MapCommandToInvoker<GetPapersCommand, GetPapersInvoker> ().Invoke ();

			DI.MapCommandToInvoker<GetPaperDetailsCommand, GetPaperDetailsInvoker> ();
		}
	}
}

