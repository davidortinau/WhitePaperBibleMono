using System;
using WhitePaperBible.Core.Models;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Net;
using MonkeyArms;

namespace WhitePaperBible.Core.Services
{
	public interface IDeletePaperService:IBaseService
	{
		void Execute (Paper paper);
	}

	public class DeletePaperService:BaseService, IDeletePaperService, IInjectingTarget
	{
		[Inject]
		public AppModel AM;

		public void Execute (Paper paper)
		{
			var cookieJar = new CookieContainer ();
			cookieJar.Add (new Uri (Constants.BASE_URI), new Cookie (AM.UserSessionCookie.Name, AM.UserSessionCookie.Value));

			var url = Constants.BASE_URI;
			url += String.Format("papers/delete/{0}", paper.id);
			url += "&caller=wpb-iPhone";
			Client.OpenURL (url, MethodEnum.DELETE, cookieJar);

		}

		#region implemented abstract members of BaseService

		protected override void HandleSuccess (object sender, EventArgs args)
		{
//			var user = ParseResponse<UserDTO> ();
			DispatchSuccess (null);

//			DispatchSuccess (new PaperSavedEventArgs (ParseResponse<PaperNode> ()));
		}

		#endregion

	}
}

