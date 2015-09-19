using System;
using MonkeyArms;
using WhitePaperBible.Core.Models;
using WhitePaperBible.Core.Invokers;
using WhitePaperBible.Core.Views;
using System.Linq;
using WhitePaperBible.Core.Services;

namespace WhitePaperBible.Core.Mediators
{
	public class ProfileViewMediator : Mediator
	{
		[Inject]
		public AppModel AM;

		[Inject]
		public UserProfileReceivedInvoker Received;

		IProfileView Target;

		public ProfileViewMediator (IProfileView view) : base (view)
		{
			this.Target = view;
		}

		public override void Register ()
		{
			InvokerMap.Add (Received, OnProfileReceived);

			if (AM.IsLoggedIn && AM.User != null && AM.User.Name != null) {
				SetUserProfile ();
			}
		}

		void OnProfileReceived (object sender, EventArgs e)
		{
			SetUserProfile ();
		}

		public void SetUserProfile ()
		{
			Target.SetUserProfile (AM.User);
		}
	}
}