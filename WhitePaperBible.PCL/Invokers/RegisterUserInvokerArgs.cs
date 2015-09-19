using System;
using MonkeyArms;
using WhitePaperBible.Core.Models;

namespace WhitePaperBible.Core.Invokers
{
	public class RegisterUserInvokerArgs: InvokerArgs
	{
		public AppUser User;

		public RegisterUserInvokerArgs(AppUser user)
		{
			this.User = user;
		}
	}
}

