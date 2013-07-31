using System;
using MonkeyArms;
using WhitePaperBibleCore.Models;
using WhitePaperBibleCore.Invokers;
using WhitePaperBible.Core.Views;
using System.Linq;

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

			Target.Filter += HandleFilter;

			Target.SearchPlaceHolderText = "Search Papers";

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

		void HandleFilter (object sender, EventArgs e)
		{
			if (AppModel.Papers != null) {
				var query = Target.SearchQuery;
				var filteredPapers = AppModel.Papers.Where(ce =>(ce.title.ToLower().Contains(query))).ToList();

				Target.SetPapers ( filteredPapers );
			}
		}

		public void SetPapers(){
			if (AppModel.Papers != null) {
				Target.SetPapers (AppModel.Papers);
			}
		}
	}
}