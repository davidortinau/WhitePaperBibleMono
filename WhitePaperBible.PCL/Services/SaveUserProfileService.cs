using System;
using WhitePaperBible.Core.Models;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Net;
using MonkeyArms;

namespace WhitePaperBible.Core.Services
{
	public interface ISaveUserProfileService:IBaseService
	{
		void Execute (AppUser user);
	}

	public class SaveUserProfileService:BaseService, ISaveUserProfileService, IInjectingTarget
	{
		[Inject]
		public AppModel AM;

		public void Execute (AppUser user)
		{
			var url = Constants.BASE_URI + String.Format ("users/update/{0}?caller=wpb-iPhone", Uri.EscapeDataString(AM.User.username));
			url += String.Format ("&user[name]={0}&user[website]={1}&user[bio]={2}&user[email]={3}&user[username]={4}", user.Name, user.Website, user.Bio, Uri.EscapeDataString(user.Email), Uri.EscapeDataString(user.username));
			if(user.password.Length > 0){
				url += String.Format ("&user[password]={0}&user[password_confirmation]={1}", user.password, user.passwordConfirmation);
			}
			url += String.Format ("&user_id={0}#", AM.User.ID);

			Client.OpenURL (url, MethodEnum.GET, true);
		}

		#region implemented abstract members of BaseService

		protected override void HandleSuccess (object sender, EventArgs args)
		{
			DispatchSuccess (null);
		}

		#endregion
	}
}

