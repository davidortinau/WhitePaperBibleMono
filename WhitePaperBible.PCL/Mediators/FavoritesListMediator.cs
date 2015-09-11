using System;
using MonkeyArms;
using WhitePaperBible.Core.Models;
using WhitePaperBible.Core.Invokers;
using WhitePaperBible.Core.Views;
using System.Linq;
using WhitePaperBible.Core.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WhitePaperBible.Core.Mediators
{
	public class FavoritesListMediator : Mediator
	{
		[Inject]
		public AppModel AM;

		[Inject]
		public FavoritesReceivedInvoker FavoritesReceived;

		[Inject]
		public GetFavoritesInvoker GetFavorites;

		[Inject]
		public LoggedInInvoker LoggedIn;

		IFavoritesView Target;

		public FavoritesListMediator (IFavoritesView view) : base (view)
		{
			this.Target = view;
		}

		public override void Register ()
		{
			InvokerMap.Add (Target.OnPaperSelected, HandlerPaperSelected);
			InvokerMap.Add (FavoritesReceived, (object sender, EventArgs e) => SetPapers ());
			InvokerMap.Add (LoggedIn, (object sender, EventArgs e) => Target.HideLoginButton ());

			if (!AM.IsLoggedIn) {
				Target.ShowLoginButton ();
			}

			if (AM.IsLoggedIn) {
				GetFavorites.Invoke ();
			}
			SetPapers ();
		}

		void HandlerPaperSelected (object sender, EventArgs e)
		{
			AM.CurrentPaper = Target.SelectedPaper;
		}

		public void SetPapers ()
		{
			if (AM.Favorites != null && AM.Favorites != null) {
				Target.SetPapers (AM.Favorites);
			}else{
				if (AM.IsLoggedIn) {
					GetFavorites.Invoke ();
				}
			}
		}

//		public async Task<List<Paper>> Papers()
//		{
//			//
//		}
	}
}