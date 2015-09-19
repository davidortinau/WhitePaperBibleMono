using System;
using MonkeyArms;
using WhitePaperBible.Core.Models;

namespace WhitePaperBible.Core.Invokers
{
	public class SaveUserInvokerArgs: InvokerArgs
	{
		public AppUser User;

		public SaveUserInvokerArgs(AppUser user)
		{
			this.User = user;
		}
	}
}

