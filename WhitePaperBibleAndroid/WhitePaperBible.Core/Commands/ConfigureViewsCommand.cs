
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

			DI.MapMediatorToClass<LoadingViewMediator, ILoadingView> ();
			DI.MapMediatorToClass<PapersListMediator, IPapersListView> ();
			DI.MapMediatorToClass<PaperDetailMediator, IPaperDetailView> ();

		}
	}
}

