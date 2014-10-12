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
			var url = Constants.BASE_URI;
			url += String.Format("papers/{0}/destroy", paper.id);
			url += "?caller=wpb-iPhone";
			url += String.Format("&user_id={0}",AM.User.ID);
			Client.OpenURL (url, MethodEnum.DELETE, true);

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

