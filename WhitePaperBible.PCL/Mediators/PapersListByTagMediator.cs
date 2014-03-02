using System;
using MonkeyArms;
using WhitePaperBible.Core.Models;
using WhitePaperBible.Core.Invokers;
using WhitePaperBible.Core.Views;
using System.Linq;

namespace WhitePaperBible.Core.Mediators
{
	public class PapersListByTagMediator : Mediator
	{
		[Inject]
		public AppModel AppModel;
		[Inject]
		public PapersByTagReceivedInvoker PapersReceived;
		[Inject]
		public GetPapersByTagInvoker GetPapersByTag;
		protected IPapersByTagListView Target;

		public PapersListByTagMediator (IPapersByTagListView view) : base (view)
		{
			this.Target = view;
		}

		public override void Register ()
		{
			InvokerMap.Add (Target.OnPaperSelected, HandlerPaperSelected);
			InvokerMap.Add (PapersReceived, (object sender, EventArgs e) => SetPapers (e as PapersReceivedInvokerArgs));
			GetPapersByTag.Invoke (new GetPapersByTagInvokerArgs (Target.SelectedTag));

		}

		void HandlerPaperSelected (object sender, EventArgs e)
		{
			AppModel.CurrentPaper = Target.SelectedPaper;
		}

		public void SetPapers (PapersReceivedInvokerArgs args)
		{
			Target.SetPapers (args.Papers);
		}
	}
}