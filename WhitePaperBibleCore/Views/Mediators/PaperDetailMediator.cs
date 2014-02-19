using System;
using MonkeyArms;
using WhitePaperBible.Core.Models;
using WhitePaperBible.Core.Invokers;
using WhitePaperBible.Core.Views;
using System.Linq;

namespace WhitePaperBible.Core.Views.Mediators
{
	public class PaperDetailMediator : Mediator
	{
		[Inject]
		public AppModel AppModel;

		[Inject]
		public PaperDetailsReceivedInvoker PaperDetailsReceived;

		IPaperDetailView Target;

		public PaperDetailMediator(IPaperDetailView view):base(view){
			this.Target = view;
		}

		public override void Register ()
		{
//			base.Register ();

			DI.Get<PaperDetailsReceivedInvoker> ().Invoked += (object sender, EventArgs e) => {
				SetPaper();
			};

			PaperDetailsReceived.Invoked += (object sender, EventArgs e) => {
				SetPaper();
			};

//			if(AppModel.CurrentPaper != null){
//				Target.SetPaper (AppModel.CurrentPaper);
//			}else{
				DI.Get<GetPaperDetailsInvoker> ().Invoke ();
//			}
		}

		public override void Unregister ()
		{
			base.Unregister ();
		}

		public void SetPaper(){
			if (AppModel.CurrentPaper != null) {
				Target.SetPaper (AppModel.CurrentPaper);
			}
		}
	}
}