
using System;
using System.Drawing;

using MonoTouch.Foundation;
using MonoTouch.UIKit;
using MonkeyArms;

using WhitePaperBible.Core.Views;
using WhitePaperBible.Core.Invokers;
using WhitePaperBible.Core.Enums;
using IOS.Util;

namespace WhitePaperBible.iOS
{
	public partial class BibleSearchView : UIViewController, IMediatorTarget, IBibleSearchView
	{
		public UISearchBar SearchBar;

		BibleSearchResults ResultsTable;

		public Invoker DoSearch {
			get;
			private set;
		}

		public BibleSearchView () : base ("BibleSearchView", null)
		{
			DoSearch = new Invoker ();
		}

		public override void DidReceiveMemoryWarning ()
		{
			// Releases the view if it doesn't have a superview.
			base.DidReceiveMemoryWarning ();
			
			// Release any cached data, images, etc that aren't in use.
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			Title = "Bible Search";

			AnalyticsUtil.TrackScreen (this.Title);
			
			// Perform any additional setup after loading the view, typically from a nib.
			CreateView ();
		}

		void CreateView ()
		{
			SearchBar = new UISearchBar (new RectangleF (0, 64, View.Bounds.Width, 90));
			SearchBar.ShowsScopeBar = true;
			SearchBar.Placeholder = @"John 3:16";
			SearchBar.ScopeButtonTitles = new string[]{"Reference", "Keyword", "Phrase"};
			View.AddSubview (SearchBar);

			SearchBar.SelectedScopeButtonIndexChanged += (object sender, UISearchBarButtonIndexEventArgs e) => {
				if(e.SelectedScope == 0){
					SearchBar.Placeholder = @"John 3:16";
				}else if(e.SelectedScope == 1){
					SearchBar.Placeholder = @"Compassion";
				}else{
					SearchBar.Placeholder = @"the just shall live by faith";
				}
			};

			SearchBar.SearchButtonClicked += PerformSearch;
		}

		void PerformSearch (object sender, EventArgs e)
		{
			var args = new GetBibleSearchResultsInvokerArgs (SearchBar.Text, (SearchScopeEnum)SearchBar.SelectedScopeButtonIndex);
			DoSearch.Invoke (args);

			SearchBar.ResignFirstResponder ();
		}

		public override void ViewDidAppear (bool animated)
		{
			base.ViewDidAppear (animated);
			DI.RequestMediator (this);

			if(ResultsTable != null){
				DI.RequestMediator (ResultsTable);
			}

//			if(!isComingFromAddingAPaper){
//				[HRRestModel setDelegate:self];
//				[HRRestModel getPath:@"/papers/?caller=wpb-iPhone" withOptions:nil object:self];
//
//
//
//				isOnAddOrCreateActionSheet = NO;
//				isOnSelectingAPaperActionSheet = NO;
//			}
//
//			[self.table reloadData];


			// add button on element
			// either add to the existing paper in edit
			// add to an existing paper from list
			// or pop an actionscheet to start a new paper
		}

		public override void ViewDidDisappear (bool animated)
		{
			base.ViewDidDisappear (animated);
			DI.DestroyMediator (this);

			if(ResultsTable != null){
				DI.DestroyMediator (ResultsTable);
			}
		}

		public override void ViewDidLayoutSubviews()
		{
			base.ViewDidLayoutSubviews ();

			if (ResultsTable == null) {
				ResultsTable = new BibleSearchResults ();

				var containerView = new UIView (new RectangleF (0, 90+64, View.Bounds.Width, View.Bounds.Height - 90));
				containerView.AddSubview (ResultsTable.TableView);
				View.AddSubview (containerView);
			}
		}
	}
}

