
using System;
using System.Drawing;

using MonoTouch.Foundation;
using MonoTouch.UIKit;
using MonkeyArms;

using WhitePaperBible.Core.Views;
using WhitePaperBible.Core.Invokers;
using WhitePaperBible.Core.Enums;

namespace WhitePaperBible.iOS
{
	public partial class BibleSearchView : UIViewController, IMediatorTarget, IBibleSearchView
	{
		public UISearchBar SearchBar;

		UITableView ResultsTable;

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

			ResultsTable = new UITableView (new RectangleF (0, 90+64, View.Bounds.Width, View.Bounds.Height - 90));
			View.AddSubview (ResultsTable);
		}

		void PerformSearch (object sender, EventArgs e)
		{
			var args = new GetBibleSearchResultsInvokerArgs (SearchBar.Text, (SearchScopeEnum)SearchBar.SelectedScopeButtonIndex);
			DoSearch.Invoke (args);

			// release input focus
			// dismiss keyboard
		}

		public override void ViewWillAppear (bool animated)
		{
			base.ViewWillAppear (animated);
			DI.RequestMediator (this);

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

		public override void ViewWillDisappear (bool animated)
		{
			base.ViewWillDisappear (animated);
			DI.DestroyMediator (this);
		}
	}
}

