using MonoTouch.Dialog;
using UIKit;
using WhitePaperBible.iOS.Managers;
using System.ComponentModel;
using System;

using MonkeyArms;
using WhitePaperBible.Core.Invokers;
using WhitePaperBible.Core.Views;
using WhitePaperBible.iOS.Invokers;
using BigTed;

namespace WhitePaperBible.iOS
{
	public class TabBarController : UITabBarController, ITabBarView, IMediatorTarget
	{
		UINavigationController papersNav, tagsNav, favoritesNav, searchNav, aboutNav, myPapersNav;
		//UISplitViewController speakersSplitView, sessionsSplitView, exhibitorsSplitView, twitterSplitView, newsSplitView;

		LoginRequiredController LoginRequiredView;

		public TabBarController ()
		{
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			papersNav = CreateTabView<PapersListController> (ResourceManager.GetString ("papers"), "papers");

			tagsNav = CreateTabView<TagsView> (ResourceManager.GetString ("tags"), "tag");

			favoritesNav = CreateTabView<FavoritesView> (ResourceManager.GetString ("favorites"), "favorites");
			
			searchNav = CreateTabView<BibleSearchView> (ResourceManager.GetString ("search"), "search");
			
			myPapersNav = CreateTabView<MyPapersAndProfileController> (ResourceManager.GetString ("myPapers"), "my_papers");

			aboutNav = CreateTabView<AboutView> (ResourceManager.GetString ("about"), "myDots");

			
			UIViewController[] viewControllers;
			// create our array of controllers
			if (AppDelegate.IsPhone) {
				viewControllers = new UIViewController[] {
					papersNav,
					tagsNav,
					favoritesNav,
					searchNav,
					myPapersNav,
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
			return true;
//			if (AppDelegate.IsPad)
//				return true;
//			else
//				return toInterfaceOrientation == UIInterfaceOrientation.Portrait;
		}

		public void ShowUnreachable ()
		{
			InvokeOnMainThread (() => {
				BTProgressHUD.ShowErrorWithStatus("Some features and content may not load without an internet connect. To get the very latest content, please enable WiFi or Data.", 4000);
			});

		}

		public override void ViewWillAppear (bool animated)
		{
			base.ViewWillAppear (animated);
			DI.RequestMediator (this);
		}

		public override void ViewWillDisappear (bool animated)
		{
			base.ViewWillDisappear (animated);
			DI.DestroyMediator (this);

		}



	}
}

