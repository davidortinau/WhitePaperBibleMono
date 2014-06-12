using System;
using MonkeyArms;
using WhitePaperBible.Core.Invokers;
using WhitePaperBible.Core.Models;
using WhitePaperBible.Core.Services;
using System.Collections.Generic;

namespace WhitePaperBible.Core.Commands
{
	public class LoginCommand : Command
	{
		[Inject]
		public AppModel AM;

		[Inject]
		public IAuthenticateUserService Service;

		[Inject]
		public LoggedInInvoker LoggedIn;

		[Inject]
		public LoginFaultInvoker Fault;

		public override void Execute (InvokerArgs args)
		{
			var loginArgs = (args as LogInInvokerArgs);
			Service.Success += onSuccess;
			Service.Fault += onFault;
			Service.Execute (loginArgs.UserName, loginArgs.Password);
		}

		void onSuccess (object sender, EventArgs args)
		{
			if ((args as AuthenticateUserServiceEventArgs).Success) {
				AM.IsLoggedIn = true;
				LoggedIn.Invoke ();
			}else{
				Fault.Invoke ();
			}
		}

		void onFault (object sender, EventArgs e)
		{
			Fault.Invoke ();
		}
	}
}

