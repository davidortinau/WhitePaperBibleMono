using MonkeyArms;
using WhitePaperBible.Core.Mediators;
using WhitePaperBible.Core.Views;

namespace WhitePaperBible.Core.Commands
{
	public class ConfigureViewsCommand : Command
	{
		public override void Execute (InvokerArgs args)
		{
//			base.Execute (args);

			DI.MapMediatorToClass<LoadingViewMediator, ILoadingView> ();
			DI.MapMediatorToClass<PapersListMediator, IPapersListView> ();
			DI.MapMediatorToClass<PaperDetailMediator, IPaperDetailView> ();
			DI.MapMediatorToClass<TagsListMediator, ITagsListView> ();

		}
	}
}

