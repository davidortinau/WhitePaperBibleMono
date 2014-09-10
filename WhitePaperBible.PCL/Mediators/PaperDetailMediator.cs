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

		[Inject]
		public PaperDeletedInvoker PaperDeleted;

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
			InvokerMap.Add (PaperDeleted, (object sender, EventArgs e) => Target.DismissController ());
			GetPaperDetails.Invoke ();

		}

		private void SetDetails()
		{
			if(AppModel.Papers.IndexOf(AppModel.CurrentPaper) < 0){
				Target.DismissController ();// it has been deleted
				return;
			}
			isFavorite = AppModel.Favorites.Any (paper => paper.id == AppModel.CurrentPaper.id);
			bool isOwned = (AppModel.IsLoggedIn) && (AppModel.CurrentPaper.user_id == AppModel.User.ID);
			Target.SetPaper (AppModel.CurrentPaper, isFavorite, isOwned);
		}

		void OnToggleFavorite (object sender, EventArgs e)
		{
			isFavorite = !isFavorite;
			var args = new ToggleFavoriteInvokerArgs (AppModel.CurrentPaper, isFavorite);
			ToggleFavorite.Invoke (args);
		}
	}
}