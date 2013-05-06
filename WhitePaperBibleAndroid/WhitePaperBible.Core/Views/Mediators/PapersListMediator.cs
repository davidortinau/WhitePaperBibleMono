using System;
using MonkeyArms;
using WhitePaperBibleCore.Models;
using WhitePaperBibleCore.Invokers;
using WhitePaperBible.Core.Views;

namespace WhitePaperBibleCore.Views.Mediators
{
	public class PapersListMediator : Mediator
	{
		[Inject]
		public AppModel AppModel;

		[Inject]
		public PapersReceivedInvoker PapersReceived;

		IPapersListView Target;

		public PapersListMediator(IPapersListView view):base(view){
			this.Target = view;
		}

		public override void Register ()
		{
			base.Register ();

			SetPapers ();

			DI.Get<PapersReceivedInvoker> ().Invoked += (object sender, EventArgs e) => {
				SetPapers();
			};

			PapersReceived.Invoked += (object sender, EventArgs e) => {
				SetPapers();
			};
		}

		public override void Unregister ()
		{
			base.Unregister ();
		}

		public void SetPapers(){
			if (AppModel.Papers != null) {
				Target.SetPapers (AppModel.Papers);
			}
		}
	}
}