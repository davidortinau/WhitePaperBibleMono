using System;
using WhitePaperBible.Core.Models;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Net;
using MonkeyArms;

namespace WhitePaperBible.Core.Services
{
	public interface IGetFavoritesService:IBaseService
	{
		void Execute ();
	}

	public class GetFavoritesService:BaseService, IGetFavoritesService, IInjectingTarget
	{
		[Inject]
		public AppModel AM;

		public void Execute ()
		{
			var cookieJar = new CookieContainer ();
			cookieJar.Add (new Uri (Constants.BASE_URI), new Cookie (AM.UserSessionCookie.Name, AM.UserSessionCookie.Value));
			Client.OpenURL (Constants.BASE_URI + "favorite/index/?caller=wpb-iPhone", false, cookieJar);
		}

		#region implemented abstract members of BaseService

		protected override void HandleSuccess (object sender, EventArgs args)
		{
			DispatchSuccess (new GetFavoritesServiceEventArgs (ParseResponse<List<PaperNode>> ()));
		}

		#endregion
	}

	public class GetFavoritesServiceEventArgs:EventArgs
	{
		public readonly List<PaperNode> Papers;

		public GetFavoritesServiceEventArgs (List<PaperNode> papers)
		{
			Papers = papers;
		}
	}
}

