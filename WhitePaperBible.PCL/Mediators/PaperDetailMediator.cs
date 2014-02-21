using System;
using MonkeyArms;
using WhitePaperBible.Core.Models;
using WhitePaperBible.Core.Invokers;
using WhitePaperBible.Core.Views;
using System.Linq;

namespace WhitePaperBible.Core.Mediators
{
	public class PaperDetailMediator : Mediator
	{
		[Inject]
		public AppModel AppModel;
		[Inject]
		public PaperDetailsReceivedInvoker PaperDetailsReceived;
		[Inject]
		public GetPaperDetailsInvoker GetPaperDetails;
		IPaperDetailView Target;

		public PaperDetailMediator (IPaperDetailView view) : base (view)
		{
			this.Target = view;
		}

		public override void Register ()
		{
		
			InvokerMap.Add (PaperDetailsReceived, (object sender, EventArgs e) => SetPaper ());

			GetPaperDetails.Invoke ();
		}

		public void SetPaper ()
		{
			if (AppModel.CurrentPaper != null) {
				Target.SetPaper (AppModel.CurrentPaper);
			}
		}
	}
}