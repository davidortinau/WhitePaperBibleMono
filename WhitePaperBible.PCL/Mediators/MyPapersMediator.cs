using System;
using MonkeyArms;
using WhitePaperBible.Core.Models;
using WhitePaperBible.Core.Invokers;
using WhitePaperBible.Core.Views;
using System.Linq;
using WhitePaperBible.Core.Services;

namespace WhitePaperBible.Core.Mediators
{
	public class MyPapersMediator : Mediator
	{
		[Inject]
		public AppModel AM;

		[Inject]
		public MyPapersReceivedInvoker PapersReceived;

		[Inject]
		public GetMyPapersInvoker GetMyPapers;

		[Inject]
		public LogoutInvoker Logout;

		IMyPapersView Target;

		public MyPapersMediator (IMyPapersView view) : base (view)
		{
			this.Target = view;
		}

		public override void Register ()
		{
			InvokerMap.Add (Target.OnPaperSelected, HandlerPaperSelected);
			InvokerMap.Add (PapersReceived, (object sender, EventArgs e) => SetPapers ());
			InvokerMap.Add (Logout, (object sender, EventArgs e)=> Target.PromptForLogin());
			InvokerMap.Add (Target.OnLogoutRequested, (object sender, EventArgs e) => Logout.Invoke ());


			if (AM.IsLoggedIn) {
				SetPapers ();
			}else{
				Target.PromptForLogin ();
			}

		}

		void HandlerPaperSelected (object sender, EventArgs e)
		{
			AM.CurrentPaper = Target.SelectedPaper;
		}

		public void SetPapers ()
		{
			// need to filter paper list by user id
			var myPapers = (from paper in AM.Papers
				where paper.user_id == AM.User.ID
				select paper).ToList<Paper>();

			Target.SetPapers (myPapers);

		}
	}
}