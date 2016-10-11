using System;
using System.Collections.Generic;
using System.Linq;
using Foundation;
using UIKit;
using MonoTouch.Dialog;
using WhitePaperBible.Core.Views;
using MonkeyArms;
using WhitePaperBible.Core.Models;
using WhitePaperBible.iOS.Invokers;
using WhitePaperBible.Core.Invokers;

namespace WhitePaperBible.iOS
{
	public partial class MyPapersView : DialogViewController, IMyPapersView, IMediatorTarget
	{
		public MyPapersView () : base (UITableViewStyle.Plain, null, true)
		{
			EnableSearch = true; 
			AutoHideSearch = true;
			SearchPlaceholder = @"Find Papers";
			this.Filter = new Invoker ();
			this.OnPaperSelected = new Invoker ();
//			this.OnLogoutRequested = new Invoker ();

//			// add logout in upper left
//			NavigationItem.SetLeftBarButtonItem (
//				new UIBarButtonItem ("Logout", UIBarButtonItemStyle.Plain, (sender, args)=> {
//					OnLogoutRequested.Invoke();
//				})
//				, true
//			);
		}

		#region IPapersListView implementation

		public Invoker Filter {
			get;
			private set;
		}

		public Invoker OnPaperSelected {
			get;
			private set;
		}

//		public Invoker OnLogoutRequested {
//			get;
//			private set;
//		}

		public void SetPapers (List<Paper> papers)
		{
			if(papers == null || papers.Count == 0){
				return;
			}

			InvokeOnMainThread (delegate {

				Root = new RootElement ("Papers") {
					from node in papers
					group node by (node.title [0].ToString ().ToUpper ()) into alpha
					orderby alpha.Key
					select new Section (alpha.Key) {
						from eachNode in alpha
						select (Element)new WhitePaperBible.iOS.UI.CustomElements.PaperElement (eachNode, delegate {
							this.OnPaperSelected.Invoke(new ShowMyPaperInvokerArgs(eachNode));
						})
					}
				};

				TableView.ScrollToRow (NSIndexPath.FromRowSection (0, 0), UITableViewScrollPosition.Top, false);
			});

		}

		public string SearchPlaceHolderText {
			get;
			set;
		}

		public string SearchQuery {
			get;
			set;
		}

		public Paper SelectedPaper {
			get;
			set;
		}

		#endregion

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			SearchTextChanged += (sender, args) => {
				Console.WriteLine ("search text changed");	
			};
		}

        public void PromptForLogin ()
        {
            throw new NotImplementedException ();
        }

        public void SetUserProfile (AppUser user)
        {
            throw new NotImplementedException ();
        }
    }
}
