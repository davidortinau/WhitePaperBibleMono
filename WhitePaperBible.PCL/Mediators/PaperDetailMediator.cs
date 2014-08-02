using System;
using MonkeyArms;
using WhitePaperBible.Core.Models;
using WhitePaperBible.Core.Invokers;
using WhitePaperBible.Core.Views;
using System.Linq;
using WhitePaperBible.Core.Services;

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

		[Inject]
		public GetPaperReferencesService PaperReferencesService;

		[Inject]
		public ToggleFavoriteInvoker ToggleFavorite;

		IPaperDetailView Target;

		bool isFavorite;

		public PaperDetailMediator (IPaperDetailView view) : base (view)
		{
			this.Target = view;
		}

		public override void Register ()
		{
			AppModel.CurrentPaper = Target.Paper;
			InvokerMap.Add (PaperDetailsReceived, (object sender, EventArgs e) => SetDetails ());
			InvokerMap.Add (Target.ToggleFavorite, OnToggleFavorite);
			GetPaperDetails.Invoke ();

		}

		private void SetDetails()
		{
			isFavorite = AppModel.Favorites.Any (paper => paper.id == AppModel.CurrentPaper.id);
			Target.SetPaper (AppModel.CurrentPaper, isFavorite);
		}

		void OnToggleFavorite (object sender, EventArgs e)
		{
			isFavorite = !isFavorite;
			var args = new ToggleFavoriteInvokerArgs (AppModel.CurrentPaper, isFavorite);
			ToggleFavorite.Invoke (args);
		}
	}
}