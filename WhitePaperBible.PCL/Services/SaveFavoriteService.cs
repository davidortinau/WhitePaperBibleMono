using System;
using WhitePaperBible.Core.Models;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Net;
using MonkeyArms;

namespace WhitePaperBible.Core.Services
{
	public interface ISaveFavoriteService:IBaseService
	{
		void Execute (Paper paper, bool isFavorite);
	}

	public class SaveFavoriteService:BaseService, ISaveFavoriteService, IInjectingTarget
	{
		[Inject]
		public AppModel AM;

		public void Execute (Paper paper, bool isFavorite)
		{
			var cookieJar = new CookieContainer ();
			cookieJar.Add (new Uri (Constants.BASE_URI), new Cookie (AM.UserSessionCookie.Name, AM.UserSessionCookie.Value));

			var url = Constants.BASE_URI + "favorite/";
			if(isFavorite){
				url += "create";
			}else{
				url += "destroy";
			}
			url += String.Format ("/{0}?caller=wpb-iPhone", paper.id);
			Client.OpenURL (url, MethodEnum.GET, cookieJar);

		}

		#region implemented abstract members of BaseService

		protected override void HandleSuccess (object sender, EventArgs args)
		{
//			var user = ParseResponse<UserDTO> ();
			DispatchSuccess (null);
		}

		#endregion
	}
}

