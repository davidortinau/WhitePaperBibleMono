using System;
using MonkeyArms;
using WhitePaperBible.Core.Invokers;
using WhitePaperBible.Core.Models;
using WhitePaperBible.Core.Services;
using System.Collections.Generic;

namespace WhitePaperBible.Core.Commands
{
	public class GetUserProfileCommand : Command
	{
		[Inject]
		public AppModel AM;

		[Inject]
		public IGetUserProfileService Service;

		public override void Execute (InvokerArgs args)
		{
			Service.Success += onSuccess;
			Service.Execute ();
		}

		void onSuccess (object sender, EventArgs args)
		{
			AM.User = ((GetUserProfileEventArgs)args).User;
		}
	}
}

