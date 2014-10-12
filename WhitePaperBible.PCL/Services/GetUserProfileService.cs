using System;
using WhitePaperBible.Core.Models;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Net;
using MonkeyArms;

namespace WhitePaperBible.Core.Services
{
	public interface IGetUserProfileService:IBaseService
	{
		void Execute ();
	}

	public class GetUserProfileService:BaseService, IGetUserProfileService, IInjectingTarget
	{
		[Inject]
		public AppModel AM;

		public void Execute ()
		{
			Client.OpenURL (Constants.BASE_URI + String.Format("users/{0}/", AM.User.username), MethodEnum.GET, true);
		}

		#region implemented abstract members of BaseService

		protected override void HandleSuccess (object sender, EventArgs args)
		{
			var user = ParseResponse<UserDTO> ();
			DispatchSuccess (new GetUserProfileEventArgs (user.User));
		}

		#endregion
	}

	public class GetUserProfileEventArgs:EventArgs
	{
		public readonly AppUser User;

		public GetUserProfileEventArgs (AppUser user)
		{
			User = user;
		}
	}
}

