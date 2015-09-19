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
		public IRegisterUserService Service;

		[Inject]
		public UserRegisteredInvoker Registered;

		[Inject]
		public RegistrationFaultInvoker FaultInvoker;

		[Inject]
		public LoggedInInvoker LoggedIn;

		[Inject]
		public SaveStorageInvoker SaveStorage;

		[Inject]
		public GetUserProfileInvoker GetProfile;

		AppUser user;

		public override void Execute (InvokerArgs args)
		{
			user = ((RegisterUserInvokerArgs)args).User;
			Service.Success += onSuccess;
			Service.Execute (user);
		}

		void onSuccess (object sender, EventArgs args)
		{
			var a = (UserProfileCreatedEventArgs)args;
			if (a.Success) {
				AppUser newUser = a.User;
				AM.User = newUser;

				// IF user has favorites, but is NOW registered, save those favorites
				if (AM.Favorites != null && AM.Favorites.Count > 0) {
					foreach (var f in AM.Favorites) {
						var toggleFavorite = DI.Get<ToggleFavoriteInvoker> ();
						var toggleArgs = new ToggleFavoriteInvokerArgs (f, true);
						toggleFavorite.Invoke (toggleArgs);
					}
				}

				Registered.Invoke ();
				AM.IsLoggedIn = true;
				LoggedIn.Invoke ();
//				SaveStorage.Invoke ();
				GetProfile.Invoke ();
			}else{
				var fa = new FaultInvokerArgs (a.Messages);
				FaultInvoker.Invoke (fa);
			}
		}
	}
}

