using MonkeyArms;
using WhitePaperBible.Core.Mediators;
using WhitePaperBible.Core.Views;

namespace WhitePaperBible.Core.Commands
{
	public class ConfigureViewsCommand : Command
	{
		public override void Execute (InvokerArgs args)
		{
			DI.MapMediatorToClass<LoadingViewMediator, ILoadingView> ();
			DI.MapMediatorToClass<PapersListMediator, IPapersListView> ();
			DI.MapMediatorToClass<PaperDetailMediator, IPaperDetailView> ();
			DI.MapMediatorToClass<TagsListMediator, ITagsListView> ();
			DI.MapMediatorToClass<PapersListByTagMediator, IPapersByTagListView> ();
			DI.MapMediatorToClass<LoginMediator, ILoginView> ();
			DI.MapMediatorToClass<FavoritesListMediator, IFavoritesView> ();

		}
	}
}

