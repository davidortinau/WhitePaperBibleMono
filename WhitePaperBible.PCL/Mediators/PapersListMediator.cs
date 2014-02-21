using System;
using MonkeyArms;
using WhitePaperBible.Core.Models;
using WhitePaperBible.Core.Invokers;
using WhitePaperBible.Core.Views;
using System.Linq;

namespace WhitePaperBible.Core.Mediators
{
	public class PapersListMediator : Mediator
	{
		[Inject]
		public AppModel AppModel;
		[Inject]
		public PapersReceivedInvoker PapersReceived;
		IPapersListView Target;

		public PapersListMediator (IPapersListView view) : base (view)
		{
			this.Target = view;
		}

		public override void Register ()
		{
			InvokerMap.Add (Target.Filter, HandleFilter);
			InvokerMap.Add (Target.OnPaperSelected, HandlerPaperSelected);
			InvokerMap.Add (PapersReceived, (object sender, EventArgs e) => SetPapers ());

			Target.SearchPlaceHolderText = "Search Papers";

			SetPapers ();

		}

		void HandleFilter (object sender, EventArgs e)
		{
			if (AppModel.Papers != null) {
				Target.SetPapers (AppModel.FilterPapers (Target.SearchQuery));
			}
		}

		void HandlerPaperSelected (object sender, EventArgs e)
		{
			AppModel.CurrentPaper = Target.SelectedPaper;
		}

		public void SetPapers ()
		{
			if (AppModel.Papers != null) {
				Target.SetPapers (AppModel.Papers);
			}
		}
	}
}