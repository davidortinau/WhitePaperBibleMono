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
		public LoggedInInvoker LoggedIn;

		IProfileView Target;

		public ProfileViewMediator (IProfileView view) : base (view)
		{
			this.Target = view;
		}

		public override void Register ()
		{
			InvokerMap.Add (LoggedIn, OnLoggedIn);


			if (AM.IsLoggedIn) {
				SetUserProfile ();
			}
		}

		void OnLoggedIn (object sender, EventArgs e)
		{
			SetUserProfile ();
		}

		public void SetUserProfile ()
		{
			Target.SetUserProfile (AM.User);
		}
	}
}