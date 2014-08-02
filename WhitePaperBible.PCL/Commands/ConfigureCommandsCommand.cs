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
			DI.MapCommandToInvoker<GetFavoritesCommand, GetFavoritesInvoker> ();
			DI.MapCommandToInvoker<SaveStorageCommand, SaveStorageInvoker> ();
			DI.MapCommandToInvoker<GetMyPapersCommand, GetMyPapersInvoker> ();
			DI.MapCommandToInvoker<GetUserProfileCommand, GetUserProfileInvoker> ();
			DI.MapCommandToInvoker<LogoutCommand, LogoutInvoker> ();
			DI.MapCommandToInvoker<SaveUserCommand, SaveUserInvoker> ();
			DI.MapCommandToInvoker<GetBibleSearchResultsCommand, GetBibleSearchResultsInvoker> ();
			DI.MapCommandToInvoker<SaveFavoriteCommand, ToggleFavoriteInvoker> ();

		}
	}
}

