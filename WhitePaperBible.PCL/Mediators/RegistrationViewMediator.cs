using System;
using MonkeyArms;
using WhitePaperBible.Core.Models;
using WhitePaperBible.Core.Invokers;
using WhitePaperBible.Core.Views;
using System.Linq;
using WhitePaperBible.Core.Services;

namespace WhitePaperBible.Core.Mediators
{
	public class RegistrationViewMediator : Mediator
	{
		[Inject]
		public AppModel AM;

		IRegistrationView Target;

		public RegistrationViewMediator (IRegistrationView view) : base (view)
		{
			this.Target = view;
		}

		public override void Register ()
		{
//			InvokerMap.Add (Received, OnProfileReceived);

//			if (AM.IsLoggedIn && AM.User != null && AM.User.Name != null) {
//				SetUserProfile ();
//			}
		}
	}
}