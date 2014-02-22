using MonoTouch.Dialog;
using MonoTouch.UIKit;

namespace WhitePaperBible.iOS
{
	public class TabBarController : UITabBarController {
		UIViewController papersScreen = null, tagsScreen;
		UINavigationController papersNav, tagsNav, favoritesNav, searchNav, aboutNav, myPapersNav;
		DialogViewController favoritesView, searchView, myPapersView, aboutView;
		//UISplitViewController speakersSplitView, sessionsSplitView, exhibitorsSplitView, twitterSplitView, newsSplitView;
		
		public TabBarController ()
		{
		}
		
		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			
			// papers tab
			if (AppDelegate.IsPhone) {
				papersScreen = new PapersView();
				papersScreen.Title = "Papers";
			} else {
//				papersScreen = new PapersView();
			}
			papersNav = new UINavigationController();
			papersNav.PushViewController ( papersScreen, false );			
			papersNav.Title = "Papers";
			papersNav.TabBarItem = new UITabBarItem("Papers"
										, UIImage.FromBundle("Images/Tabs/papers.png"), 0);
			
//			// tags tab
//			if (AppDelegate.IsPhone) {
//				tagsView = new TagsView();			
//				tagsNav = new UINavigationController();
//				tagsNav.TabBarItem = new UITabBarItem("Tags"
//											, UIImage.FromBundle("Images/Tabs/tag.png"), 1);
//				tagsNav.PushViewController ( tagsView, false );
//			} else {
////				speakersSplitView = new MWC.iOS.Screens.iPad.Speakers.SpeakerSplitView();
////				speakersSplitView.TabBarItem = new UITabBarItem("Speakers"
////											, UIImage.FromBundle("Images/Tabs/tag.png"), 1);
//			}
			
			tagsScreen = new TagsView();
			tagsScreen.Title = "Tags";
			tagsNav = new UINavigationController();
			tagsNav.TabBarItem = new UITabBarItem("Tags"
						, UIImage.FromBundle("Images/Tabs/tag.png"), 1);
			tagsNav.PushViewController ( tagsScreen, false );

			
			
			

			// favorites
			if (AppDelegate.IsPhone) {
				favoritesView = new FavoritesView();
				favoritesNav = new UINavigationController();
				favoritesNav.TabBarItem = new UITabBarItem("Favorites"
											, UIImage.FromBundle("Images/Tabs/favorites.png"), 2);
				favoritesNav.PushViewController ( favoritesView, false );
			} else {
//				sessionsSplitView = new MWC.iOS.Screens.iPad.Sessions.SessionSplitView();
//				sessionsSplitView.TabBarItem = new UITabBarItem("Sessions"
//											, UIImage.FromBundle("Images/Tabs/sessions.png"), 2);		
			}
			
			// search tab
			searchView = new SearchView();
			searchNav = new UINavigationController();
			searchNav.TabBarItem = new UITabBarItem("Search"
											, UIImage.FromBundle("Images/Tabs/search.png"), 3);
			searchNav.PushViewController ( searchView, false );
			
			// my papers tab
			myPapersView = new MyPapersView();
			myPapersNav = new UINavigationController();
			myPapersNav.TabBarItem = new UITabBarItem("My Papers"
											, UIImage.FromBundle("Images/Tabs/search.png"), 4);
			myPapersNav.PushViewController ( myPapersView, false );
			
			
			// about tab
			aboutView = new AboutView();
			aboutNav = new UINavigationController();
			aboutNav.TabBarItem = new UITabBarItem("About"
										, UIImage.FromBundle("Images/Tabs/myDots.png"), 5);
			aboutNav.PushViewController( aboutView, false );
			
			UIViewController[] viewControllers;
			// create our array of controllers
			if (AppDelegate.IsPhone) {
				viewControllers = new UIViewController[] {
					papersNav,
					tagsNav,
					favoritesNav,
					myPapersNav,
					searchNav,
					aboutNav
				};
			} else {	// IsPad
				viewControllers = new UIViewController[] {
					papersNav
				};
			}
			
			// attach the view controllers
			ViewControllers = viewControllers;
			
			// tell the tab bar which controllers are allowed to customize. 
			// if we don't set  it assumes all controllers are customizable. 
			// if we set to empty array, NO controllers are customizable.
			CustomizableViewControllers = new UIViewController[] {};
			
			// set our selected item
			SelectedViewController = papersNav;
		}
		
//		public void ShowSessionDay(int day)
//		{
//			// WARNING: ORDER IS IMPORTANT, call ShowDay() before setting index (which causes ViewWillAppear)
//			var sv = sessionsSplitView as MWC.iOS.Screens.iPad.Sessions.SessionSplitView;
//			sv.ShowDay (day);
//			SelectedIndex = 2; // Sessions
//		}

		/// <summary>
		/// Only allow iPad application to rotate, iPhone is always portrait
		/// </summary>
		public override bool ShouldAutorotateToInterfaceOrientation (UIInterfaceOrientation toInterfaceOrientation)
        {
			if (AppDelegate.IsPad)
	            return true;
			else
				return toInterfaceOrientation == UIInterfaceOrientation.Portrait;
        }
	}
}

