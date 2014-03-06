using System;
using System.ServiceModel.Security;

namespace WhitePaperBible.Core.Services
{
	public class AuthenticateUserService:BaseService
	{
		public void Execute (string username, string password)
		{

		}

		#region implemented abstract members of BaseService

		protected override void HandleSuccess (object sender, EventArgs args)
		{

		}

		#endregion
	}
}

