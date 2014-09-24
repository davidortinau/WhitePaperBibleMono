using MonkeyArms;
using WhitePaperBible.Core.Invokers;
using WhitePaperBible.Core.Models;
using System.Threading.Tasks;
using PCLStorage;
using System.Xml.Serialization;
using System.IO;
using WhitePaperBible.Core.Services;
using System;
using System.Collections.Generic;

namespace WhitePaperBible.Core.Commands
{
	public class RegisterUserCommand : Command
	{
		[Inject]
		public AppModel AM;

		[Inject]
		public ISaveUserProfileService Service;

		[Inject]
		public UserProfileSavedInvoker Saved;

		AppUser user;

		public override void Execute (InvokerArgs args)
		{
			user = ((SaveUserInvokerArgs)args).User;
			Service.Success += onSuccess;
			Service.Execute (user);
		}

		void onSuccess (object sender, EventArgs args)
		{
			AM.User.Update (user);
			Saved.Invoke ();
		}
	}
}

