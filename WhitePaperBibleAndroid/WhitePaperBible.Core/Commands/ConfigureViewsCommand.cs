
using MonkeyArms;
using WhitePaperBibleCore.Views.Mediators;
using WhitePaperBible.Core.Views;

namespace WhitePaperBibleCore.Commands
{
	public class ConfigureViewsCommand : Command
	{
		public override void Execute (InvokerArgs args)
		{
			base.Execute (args);

			// Don't think we can get here b/c the bootstrapping is happening in Core. Maybe that first command should be in the app
			DI.MapMediatorToClass<PapersListMediator, IPapersListView> ();

		}
	}
}

