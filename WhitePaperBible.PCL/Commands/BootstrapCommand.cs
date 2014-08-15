using WhitePaperBible.Core.Invokers;
using MonkeyArms;

namespace WhitePaperBible.Core.Commands
{
	public class BootstrapCommand : MonkeyArms.Command
	{
		public override void Execute (InvokerArgs args)
		{
			DI.MapCommandToInvoker<ConfigureServicesCommand, ConfigureServicesInvoker> ().Invoke ();
			DI.MapCommandToInvoker<ConfigureModelsCommand, ConfigureModelsInvoker> ().Invoke ();
			DI.MapCommandToInvoker<ConfigureViewsCommand, ConfigureViewsInvoker> ().Invoke ();
			DI.MapCommandToInvoker<ConfigureInvokersCommand, ConfigureInvokersInvoker> ().Invoke ();
			DI.MapCommandToInvoker<ConfigureCommandsCommand, ConfigureCommandsInvoker> ().Invoke ();
			DI.MapCommandToInvoker<GetPapersCommand, GetPapersInvoker> ().Invoke ();

			DI.MapCommandToInvoker<LoadStorageCommand, LoadStorageInvoker> ().Invoke ();

			DI.Get<GetTagsInvoker> ().Invoke ();

		}
	}
}

