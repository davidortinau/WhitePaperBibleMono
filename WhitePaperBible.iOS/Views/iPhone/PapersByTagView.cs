using System;
using System.Collections.Generic;
using System.Linq;
using WhitePaperBible.Core.Models;
using Foundation;
using UIKit;
using MonoTouch.Dialog;
using WhitePaperBible.Core.Views;
using MonkeyArms;
using IOS.Util;

namespace WhitePaperBible.iOS
{
	public partial class PapersByTagView : DialogViewController, IPapersByTagListView
	{
		public Tag SelectedTag {
			get;
			set;
		}

		public PapersByTagView () : base (UITableViewStyle.Plain, null, true)
		{
			EnableSearch = false; 
//			AutoHideSearch = true;
//			SearchPlaceholder = @"Find Papers";
//			this.Filter = new Invoker ();
			this.OnPaperSelected = new Invoker ();
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

		public void SetPapers (List<Paper> papers)
		{
			InvokeOnMainThread (delegate {

				Root = new RootElement ("Papers") {
					from node in papers
					group node by (node.title [0].ToString ().ToUpper ()) into alpha
					orderby alpha.Key
					select new Section (alpha.Key) {
						from eachNode in alpha
						select (Element)new WhitePaperBible.iOS.UI.CustomElements.PaperElement (eachNode, delegate {
							var paperDetails = new WhitePaperBible.iOS.PaperDetailsView(eachNode);
							paperDetails.Title = eachNode.title;
							NavigationController.PushViewController(paperDetails, true);
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

			DI.RequestMediator (this);

			AnalyticsUtil.TrackScreen (this.Title);

		}

		public override void ViewDidDisappear (bool animated)
		{
			base.ViewDidDisappear (animated);

			DI.DestroyMediator (this);
		}
	}
}
