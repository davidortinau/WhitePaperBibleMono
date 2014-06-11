using MonkeyArms;
using WhitePaperBible.Core.Invokers;

namespace WhitePaperBible.Core.Commands
{
	public class ConfigureCommandsCommand : Command
	{
		public override void Execute (InvokerArgs args)
		{
			DI.MapCommandToInvoker<GetPaperDetailsCommand, GetPaperDetailsInvoker> ();
			DI.MapCommandToInvoker<GetTagsCommand, GetTagsInvoker> ();
			DI.MapCommandToInvoker<GetPapersByTagCommand, GetPapersByTagInvoker> ();
			DI.MapCommandToInvoker<LoginCommand, LogInInvoker> ();
		}
	}
}

