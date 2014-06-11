using System;
using System.ServiceModel.Security;

namespace WhitePaperBible.Core.Services
{
	public interface IAuthenticateUserService:IBaseService
	{
		void Execute (string username, string password);
	}

	public class AuthenticateUserService:BaseService, IAuthenticateUserService
	{
		public void Execute (string username, string password)
		{
			this.Client.OpenURL (Constants.BASE_URI + String.Format ("user_sessions/?user_session[username]={0}&user_session[password]={1}", username, password), true);
		}

		#region implemented abstract members of BaseService

		protected override void HandleSuccess (object sender, EventArgs args)
		{

			DispatchSuccess (new AuthenticateUserServiceEventArgs (Client.ResponseText.Contains ("Successfully logged in.")));
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

