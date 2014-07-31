using System;
using WhitePaperBible.Core.Models;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Net;
using MonkeyArms;

namespace WhitePaperBible.Core.Services
{
	public interface IGetMyPapersService:IBaseService
	{
		void Execute ();
	}

	public class GetMyPapersService:BaseService, IGetMyPapersService, IInjectingTarget
	{
		[Inject]
		public AppModel AM;

		public void Execute ()
		{
			var cookieJar = new CookieContainer ();
			cookieJar.Add (new Uri (Constants.BASE_URI), new Cookie (AM.UserSessionCookie.Name, AM.UserSessionCookie.Value));
			Client.OpenURL (Constants.BASE_URI + "papers/?caller=wpb-iPhone", MethodEnum.GET, cookieJar);
		}

		#region implemented abstract members of BaseService

		protected override void HandleSuccess (object sender, EventArgs args)
		{
			DispatchSuccess (new GetMyPapersServiceEventArgs (ParseResponse<List<PaperNode>> ()));
		}

		#endregion
	}

	public class GetMyPapersServiceEventArgs:EventArgs
	{
		public readonly List<PaperNode> Papers;

		public GetMyPapersServiceEventArgs (List<PaperNode> papers)
		{
			Papers = papers;
		}
	}
}

