using System;
using MonkeyArms;

namespace WhitePaperBible.Core.Invokers
{
	public class LogInInvoker:Invoker
	{
		public LogInInvoker ()
		{
		}
	}

	public class LogInInvokerArgs:InvokerArgs{

		string userName;

		public string UserName {
			get {
				return userName;
			}
		}

		string password;

		public string Password {
			get {
				return password;
			}
		}

		public LogInInvokerArgs(string userName, string password)
		{
			this.userName = userName;
			this.password = password;
		}
	}
}

