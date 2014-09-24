using System;
using MonkeyArms;
using WhitePaperBible.Core.Models;
using WhitePaperBible.Core.Invokers;

namespace WhitePaperBible.Core.Commands
{
	public class ConfigureInvokersCommand : Command
	{
		public override void Execute (InvokerArgs args)
		{
//			base.Execute (args);

			DI.MapSingleton<PapersReceivedInvoker> ();
			DI.MapSingleton<PaperDetailsReceivedInvoker> ();
			DI.MapSingleton<TagsReceivedInvoker> ();
			DI.MapSingleton<PapersByTagReceivedInvoker> ();
			DI.MapSingleton<LogInInvoker> ();
			DI.MapSingleton<LoggedInInvoker> ();
			DI.MapSingleton<LoginFaultInvoker> ();
			DI.MapSingleton<FavoritesReceivedInvoker> ();
			DI.MapSingleton<MyPapersReceivedInvoker> ();
			DI.MapSingleton<StorageLoadedInvoker> ();
			DI.MapSingleton<UserProfileSavedInvoker> ();
			DI.MapSingleton<UserProfileReceivedInvoker> ();
			DI.MapSingleton<ShowMyPaperInvoker> ();
			DI.MapSingleton<BibleSearchResultsReceivedInvoker> ();
			DI.MapSingleton<UnreachableInvoker> ();
			DI.MapSingleton<RefreshPapersInvoker> ();
			DI.MapSingleton<PaperSavedInvoker> ();
			DI.MapSingleton<PaperDeletedInvoker> ();
			DI.MapSingleton<UserRegisteredInvoker> ();
		}
	}
}

