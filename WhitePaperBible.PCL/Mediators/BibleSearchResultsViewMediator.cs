using System;
using MonkeyArms;
using WhitePaperBible.Core.Models;
using WhitePaperBible.Core.Invokers;
using WhitePaperBible.Core.Views;
using System.Linq;

namespace WhitePaperBible.Core.Mediators
{
	public class BibleSearchResultsViewMediator : Mediator
	{
		[Inject]
		public AppModel AppModel;

		protected IBibleSearchResultsView Target;

		[Inject]
		public BibleSearchResultsReceivedInvoker Received;

		public BibleSearchResultsViewMediator (IBibleSearchResultsView view) : base (view)
		{
			this.Target = view;
		}

		public override void Register ()
		{
			InvokerMap.Add (Received, SetResults);
		}

		void SetResults (object sender, EventArgs e)
		{
			Target.SetResults ((e as BibleSearchResultsReceivedInvokerArgs).Results);
		}
	}
}