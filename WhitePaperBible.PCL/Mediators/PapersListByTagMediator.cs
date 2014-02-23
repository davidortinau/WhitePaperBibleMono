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

		IPapersByTagListView Target;

		public PapersListByTagMediator (IPapersByTagListView view) : base (view)
		{
			this.Target = view;
		}

		public override void Register ()
		{
//			InvokerMap.Add (Target.Filter, HandleFilter);
			InvokerMap.Add (Target.OnPaperSelected, HandlerPaperSelected);
			InvokerMap.Add (PapersReceived, (object sender, EventArgs e) => SetPapers (e as PapersReceivedInvokerArgs));

//			Target.SearchPlaceHolderText = "Search Papers";

			SetPapers (null);

		}

		void HandlerPaperSelected (object sender, EventArgs e)
		{
			AppModel.CurrentPaper = Target.SelectedPaper;
		}

		public void SetPapers (PapersReceivedInvokerArgs args)
		{
			if (args != null && args.Papers != null) {
				Target.SetPapers (args.Papers);
			}else{
				GetPapersByTag.Invoke (new GetPapersByTagInvokerArgs (Target.SelectedTag));
			}
		}
	}
}