using System;
using MonkeyArms;
using WhitePaperBible.Core.Models;
using WhitePaperBible.Core.Invokers;
using WhitePaperBible.Core.Views;
using System.Linq;
using WhitePaperBible.Core.Services;

namespace WhitePaperBible.Core.Mediators
{
	public class MyPapersAndProfileMediator : Mediator
	{
		[Inject]
		public AppModel AM;

		[Inject]
		public LogoutInvoker Logout;

		[Inject]
		public LoggedInInvoker LoggedIn;

		[Inject]
		public ShowMyPaperInvoker ShowMyPaper;

		[Inject]
		public UserProfileReceivedInvoker ProfileReceived;

		IMyPapersAndProfileView Target;

		public MyPapersAndProfileMediator (IMyPapersAndProfileView view) : base (view)
		{
			this.Target = view;
		}

		public override void Register ()
		{
			InvokerMap.Add (Logout, (object sender, EventArgs e)=> Target.PromptForLogin());
			InvokerMap.Add (LoggedIn, (object sender, EventArgs e) => Target.DismissLoginPrompt ());
			InvokerMap.Add (ShowMyPaper, (object sender, EventArgs e) => Target.ShowPaper (((ShowMyPaperInvokerArgs)e).Paper));
			InvokerMap.Add (Target.Logout, OnLogout);
			InvokerMap.Add(ProfileReceived, (object sender, EventArgs e) => Target.DismissLoginPrompt ());

			if (!AM.IsLoggedIn) {
				Target.PromptForLogin ();
			}

		}

		void OnLogout (object sender, EventArgs e)
		{
			Logout.Invoke ();
		}
	}
}