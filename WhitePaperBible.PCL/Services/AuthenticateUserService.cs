using System;
using System.ServiceModel.Security;
using MonkeyArms;
using WhitePaperBible.Core.Models;

namespace WhitePaperBible.Core.Services
{
	public interface IAuthenticateUserService:IBaseService
	{
		void Execute (string username, string password);
	}

	public class AuthenticateUserService:BaseService, IAuthenticateUserService, IInjectingTarget
	{
		[Inject]
		public AppModel model;


		public void Execute (string username, string password)
		{
			this.Client.OpenURL (Constants.BASE_URI + String.Format ("user_sessions/?user_session[username]={0}&user_session[password]={1}", username, password), MethodEnum.POST, false);
		}

		#region implemented abstract members of BaseService

		protected override void HandleSuccess (object sender, EventArgs args)
		{
			// http://whitepaperbible.org/user_sessions/?user_session%5Busername%5D=davidortinau&user_session%5Bpassword%5D=simple
			// session cookie https://gist.github.com/davidortinau/109966433bac7e4c12b1
			model.UserSessionCookie = Client.UserSessionCookie;
			DispatchSuccess (new AuthenticateUserServiceEventArgs (model.UserSessionCookie != null));
		}

		#endregion
	}

	public class AuthenticateUserServiceEventArgs:EventArgs
	{
		public readonly bool Success;

		public AuthenticateUserServiceEventArgs (bool success)
		{
			this.Success = success;
		}
	}
}

