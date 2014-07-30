using System;
using MonkeyArms;
using WhitePaperBible.Core.Models;
using WhitePaperBible.Core.Invokers;
using WhitePaperBible.Core.Views;
using System.Linq;
using WhitePaperBible.Core.Services;

namespace WhitePaperBible.Core.Mediators
{
	public class EditProfileViewMediator : Mediator
	{
		[Inject]
		public AppModel AM;

		IEditProfileView Target;

		public EditProfileViewMediator (IEditProfileView view) : base (view)
		{
			this.Target = view;
		}

		public override void Register ()
		{
			Target.SetUserProfile (AM.User);
		}

	}
}