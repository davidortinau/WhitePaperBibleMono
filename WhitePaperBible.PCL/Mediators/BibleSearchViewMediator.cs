using System;
using MonkeyArms;
using WhitePaperBible.Core.Models;
using WhitePaperBible.Core.Invokers;
using WhitePaperBible.Core.Views;
using System.Linq;

namespace WhitePaperBible.Core.Mediators
{
	public class BibleSearchViewMediator : Mediator
	{
		[Inject]
		public AppModel AppModel;

		protected IBibleSearchView Target;

		[Inject]
		public GetBibleSearchResultsInvoker Search;

		public BibleSearchViewMediator (IBibleSearchView view) : base (view)
		{
			this.Target = view;
		}

		public override void Register ()
		{
			InvokerMap.Add (Target.DoSearch, DoSearch);
//			InvokerMap.Add (PapersReceived, (object sender, EventArgs e) => SetPapers (e as PapersReceivedInvokerArgs));
//			GetPapersByTag.Invoke (new GetPapersByTagInvokerArgs (Target.SelectedTag));

		}

		void DoSearch (object sender, EventArgs e)
		{
			Search.Invoke (e as GetBibleSearchResultsInvokerArgs);
		}
	}
}