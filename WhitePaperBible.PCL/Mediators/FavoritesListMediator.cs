using System;
using MonkeyArms;
using WhitePaperBible.Core.Models;
using WhitePaperBible.Core.Invokers;
using WhitePaperBible.Core.Views;
using System.Linq;

namespace WhitePaperBible.Core.Mediators
{
	public class FavoritesListMediator : Mediator
	{
		[Inject]
		public AppModel AppModel;

		[Inject]
		public PapersByTagReceivedInvoker PapersReceived;

		[Inject]
		public GetPapersByTagInvoker GetPapersByTag;

		IFavoritesView Target;

		public FavoritesListMediator (IFavoritesView view) : base (view)
		{
			this.Target = view;
		}

		public override void Register ()
		{
//			InvokerMap.Add (Target.Filter, HandleFilter);
			InvokerMap.Add (Target.OnPaperSelected, HandlerPaperSelected);
			InvokerMap.Add (PapersReceived, (object sender, EventArgs e) => SetPapers (e as PapersReceivedInvokerArgs));
//			Target.SearchPlaceHolderText = "Search Papers";

//			if([appDelegate isUserLoggedIn]){
//				self.navigationItem.rightBarButtonItem = self.editButtonItem;
//				[HRRestModel setDelegate:self];
//				[HRRestModel getPath:@"/favorite/index/?caller=wpb-iPhone" withOptions:nil object:self];
//				[UIApplication sharedApplication].networkActivityIndicatorVisible = YES;
//			}
//			else{
//				NotLoggedInViewController *notLoggedInViewController = [[NotLoggedInViewController alloc] initWithNibName:@"NotLoggedInViewController"  bundle:[NSBundle mainBundle]];
//				[self presentModalViewController:notLoggedInViewController animated:YES];
//				[notLoggedInViewController release];
//			}

			if (AppModel.IsLoggedIn) {
				SetPapers (null);
			}else{
				Target.PromptForLogin ();
			}

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
//				GetPapersByTag.Invoke (new GetPapers (Target.SelectedTag));
				// GetFavorites
			}
		}
	}
}