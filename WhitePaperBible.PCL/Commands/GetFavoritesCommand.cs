using System;
using MonkeyArms;
using WhitePaperBible.Core.Invokers;
using WhitePaperBible.Core.Models;
using WhitePaperBible.Core.Services;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace WhitePaperBible.Core.Commands
{
	public class GetFavoritesCommand : Command
	{
		[Inject]
		public AppModel AM;

		[Inject]
		public IGetFavoritesService Service;

		[Inject]
		public FavoritesReceivedInvoker PapersReceived;

		public override void Execute (InvokerArgs args)
		{
			Service.Success += onSuccess;
			Service.Execute ();
		}

		void onSuccess (object sender, EventArgs args)
		{
			AM.Favorites = new List<Paper> ();
			foreach (var node in ((GetFavoritesServiceEventArgs)args).Papers) {
				AM.Favorites.Add (node.paper);
			}

			PapersReceived.Invoke ();
		}
	}
}

