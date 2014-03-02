using MonoTouch.Dialog;
using MonoTouch.UIKit;
using WhitePaperBible.iOS.Managers;
using System.ComponentModel;
using System;

namespace WhitePaperBible.iOS
{
	public class TabBarController : UITabBarController
	{
		UINavigationController papersNav, tagsNav, favoritesNav, searchNav, aboutNav, myPapersNav;
		//UISplitViewController speakersSplitView, sessionsSplitView, exhibitorsSplitView, twitterSplitView, newsSplitView;
		public TabBarController ()
		{
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			

			papersNav = CreateTabView<PapersView> (ResourceManager.GetString ("papers"), "papers");

			tagsNav = CreateTabView<TagsView> (ResourceManager.GetString ("tags"), "tag");

			favoritesNav = CreateTabView<FavoritesView> (ResourceManager.GetString ("favorites"), "favorites");
			
			searchNav = CreateTabView<SearchView> (ResourceManager.GetString ("search"), "search");
			
			myPapersNav = CreateTabView<MyPapersView> (ResourceManager.GetString ("myPapers"), "search");

			aboutNav = CreateTabView<AboutView> (ResourceManager.GetString ("about"), "myDots");

			
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
			CustomizableViewControllers = new UIViewController[] { };
			
			// set our selected item
			SelectedViewController = papersNav;
		}

		protected UINavigationController CreateTabView<TScreen> (string title, string iconName) where TScreen:class
		{
			UIViewController view = Activator.CreateInstance<TScreen> () as UIViewController;
			UINavigationController navController = new UINavigationController ();
			navController.PushViewController (view, false);			
			navController.Title = title;
			navController.TabBarItem = new UITabBarItem (title, UIImage.FromBundle ("Images/Tabs/" + iconName), 0);
			return navController;
		}

		public override bool ShouldAutorotateToInterfaceOrientation (UIInterfaceOrientation toInterfaceOrientation)
		{
			if (AppDelegate.IsPad)
				return true;
			else
				return toInterfaceOrientation == UIInterfaceOrientation.Portrait;
		}
	}
}

