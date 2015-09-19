using System;
using WhitePaperBible.Core.Models;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Net;
using MonkeyArms;

namespace WhitePaperBible.Core.Services
{
	public interface IRegisterUserService:IBaseService
	{
		void Execute (AppUser user);
	}

	public class RegisterUserService:BaseService, IRegisterUserService, IInjectingTarget
	{
		[Inject]
		public AppModel AM;

		public void Execute (AppUser user)
		{
			var url = Constants.BASE_URI + String.Format ("users/create/{0}?caller=wpb-iPhone", Uri.EscapeDataString(user.username));
			url += String.Format ("&user[name]={0}&user[email]={1}&user[username]={2}&eula[accepted]=YES", user.Name, Uri.EscapeDataString(user.Email), Uri.EscapeDataString(user.username));
			url += String.Format ("&user[password]={0}&user[password_confirmation]={1}", user.password, user.passwordConfirmation);

			Client.OpenURL (url, MethodEnum.POST, false);
//
		}

		#region implemented abstract members of BaseService

		protected override void HandleSuccess (object sender, EventArgs args)
		{
			var payload = ParseResponse<RegistrationPayload> ();
			AM.UserSessionCookie = Client.UserSessionCookie;

			DispatchSuccess (new UserProfileCreatedEventArgs (payload));
			
		}

		#endregion
	}

	public class UserProfileCreatedEventArgs:EventArgs
	{
		public bool Success {
			get;
			set;
		}

		public readonly AppUser User;

		public readonly string[] Messages;

		public UserProfileCreatedEventArgs (RegistrationPayload payload)
		{
			Success = payload.Success;
			Messages = payload.Messages;
			if (Success && payload.User != null) {
				User = payload.User.User;
			}

		}
	}

	public class RegistrationPayload
	{
		public Boolean Success;

		public string[] Messages;

		public UserDTO User;
	}
}

