using System;
using MonkeyArms;
using WhitePaperBibleCore.Models;
using WhitePaperBibleCore.Invokers;
using WhitePaperBible.Core.Views;
using System.Linq;

namespace WhitePaperBibleCore.Views.Mediators
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
			base.Register ();

//			Target.Filter += HandleFilter;
//			Target.OnPaperSelected += HandlerPaperSelected;
//
//			Target.SearchPlaceHolderText = "Search Papers";

//			SetPapers ();

			DI.Get<GetPaperDetailsInvoker> ().Invoke ();

			DI.Get<PaperDetailsReceivedInvoker> ().Invoked += (object sender, EventArgs e) => {
				SetPaper();
			};

			PaperDetailsReceived.Invoked += (object sender, EventArgs e) => {
				SetPaper();
			};

			if(AppModel.CurrentPaper != null){
				Target.SetPaper (AppModel.CurrentPaper);
			}
		}

		public override void Unregister ()
		{
			base.Unregister ();
		}

//		void HandleFilter (object sender, EventArgs e)
//		{
//			if (AppModel.Papers != null) {
//				var query = Target.SearchQuery;
//				var filteredPapers = AppModel.Papers.Where(ce =>(ce.title.ToLower().Contains(query))).ToList();
//
//				Target.SetPapers ( filteredPapers );
//			}
//		}
//
//		void HandlerPaperSelected (object sender, EventArgs e)
//		{
//			throw new NotImplementedException ();
//		}
//
		public void SetPaper(){
			if (AppModel.CurrentPaper != null) {
				Target.SetPaper (AppModel.CurrentPaper);
			}
		}
	}
}