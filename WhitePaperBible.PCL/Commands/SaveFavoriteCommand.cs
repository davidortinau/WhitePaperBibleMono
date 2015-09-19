using MonkeyArms;
using WhitePaperBible.Core.Invokers;
using WhitePaperBible.Core.Models;
using System.Threading.Tasks;
using PCLStorage;
using System.Xml.Serialization;
using System.IO;
using WhitePaperBible.Core.Services;
using System;
using System.Collections.Generic;

namespace WhitePaperBible.Core.Commands
{
	public class SaveFavoriteCommand : Command
	{
		[Inject]
		public AppModel AM;

		[Inject]
		public ISaveFavoriteService Service;

		[Inject]
		public SaveStorageInvoker SaveStorage;

		AppUser user;

		public override void Execute (InvokerArgs args)
		{
			var a = (ToggleFavoriteInvokerArgs)args;

			if(AM.Favorites == null){
				AM.Favorites = new List<Paper> ();
			}

			// add or remove from Favorites
			if(a.IsFavorite){
				AM.Favorites.Add (a.Paper);
			}else{
				AM.Favorites.Remove (a.Paper);
			}

			if(AM.IsLoggedIn && AM.UserSessionCookie != null){
				Service.Success += onSuccess;
				Service.Execute (a.Paper, a.IsFavorite);
			}

			SaveStorage.Invoke ();
		}

		void onSuccess (object sender, EventArgs args)
		{
			// anything?
		}
	}
}

