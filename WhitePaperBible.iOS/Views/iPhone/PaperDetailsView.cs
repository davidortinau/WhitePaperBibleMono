using System;
using System.Drawing;

using MonoTouch.Foundation;
using MonoTouch.UIKit;
using WhitePaperBible.Core.Models;
using WhitePaperBible.Core.Services;
using System.Collections.Generic;
using MonoTouch.ObjCRuntime;
using MonoTouch.MessageUI;
using MonoTouch.Twitter;
using System.Web;

namespace WhitePaperBible.iOS
{
	public partial class PaperDetailsView : UIViewController
	{
		Paper paper;
		
		/**
		 * TODO
		 * if I own the paper, set EDIT button in upper right nav
		 * if logged in enable favorites
		 * 
		 * Toolbar with Favorite and Share
		 * 
		 * 
		 * */
		
		
		public PaperDetailsView (Paper paper) : base ("PaperDetailsView", null)
		{
			this.paper = paper;

			this.HidesBottomBarWhenPushed = true;
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

			MonoTouch.UIKit.UIApplication.SharedApplication.NetworkActivityIndicatorVisible = true;
			var svc = new PaperService ();
			svc.GetPaperReferences (paper.id, onReferencesReceived, onFailure);
			
			this.Title = paper.title;
	
			webView.ScrollView.ScrollEnabled = true;
//			this.NavigationController.SetNavigationBarHidden (true, false);


//			this.TabBarController.
			
			webView.ShouldStartLoad += webViewShouldStartLoad;
			
			var tapRecognizer = new UITapGestureRecognizer (this, new Selector ("HandleTapFrom"));
			tapRecognizer.NumberOfTapsRequired = 1;
			tapRecognizer.Delegate = new GestureDelegate ();
			
			//var swipeRecognizer = new UISwipeGestureRecognizer
						
			webView.AddGestureRecognizer (tapRecognizer);
			
//			UITapGestureRecognizer *tapRecognizer = [[UITapGestureRecognizer alloc] initWithTarget:self action:@selector(HandleTapFrom:)];
//    tapRecognizer.numberOfTapsRequired = 1;
//    tapRecognizer.delegate = self;
//    [self.webView addGestureRecognizer:tapRecognizer];
		}
		
//		float startX;
//		
//		public override void HandleTouchesBegan (NSSet touches, UIEvent evt)
//		{
//			Console.WriteLine("touchesBegan");
//			var touch = (touches.AnyObject as UITouch);
//			
//			if(touch.View == this.View)
//			{
//				var location = touch.LocationInView(this.View);	
//				startX = location.X - this.View.Center.X;
//			}
//		}
//	
//		public override void TouchesMoved (NSSet touches, UIEvent evt)
//		{
//			Console.WriteLine("touchesMoved");
//			var touch = (touches.AnyObject as UITouch);
//			if(touch.View == this.View)
//			{
//				var location = touch.LocationInView(this.View);	
//				location.X = location.X - startX;
//				this.View.Center = location;
//			}
//		}
//		
//		public override void TouchesEnded (NSSet touches, UIEvent evt)
//		{
//			Console.WriteLine("touchesEnded");
//			// do anything?
//		}
		
		[Export("HandleTapFrom")]
		public void HandleTapFrom (UITapGestureRecognizer recognizer)
		{
			Console.WriteLine ("getting taps");
			
			// so the question is how long were we holding it down? Has the user initialized text selection?
			var selection = webView.EvaluateJavascript ("window.getSelection().toString()");
			Console.WriteLine ("selection {0}", selection);
			
			if (selection.Length <= 0) {			
				// TODO animate these
				if (toolbar.Alpha.Equals (1f)) {
					toolbar.Alpha = 0f;
				} else {
					toolbar.Alpha = 1f;
				}
			} else {
				Console.WriteLine ("user is selecting text");
			}
			
//			CGPoint location = [recognizer locationInView:self.navigationController.topViewController.view];
//		    CGRect bounds = self.navigationController.topViewController.view.bounds;
//		
//		    if (location.x < bounds.size.width / 5.0) {
//		        // This is in the left most quadrant
//		
//		        // I implement code here to perform a "previous page" action
//		    } else if (location.x > bounds.size.width * 4.0 / 5.0) {
//		        // This is in the right most quadrant
//		
//		        // I implement code here to perform a "next page" action
//		    } else if ((location.x > bounds.size.width / 3.0) && (location.x < bounds.size.width * 2.0 / 3.0)) {
//		        // This is in the middle third
//		        BOOL hidden = [self.navigationController isNavigationBarHidden];
//		
//		        // resize the height of self.webView1, self.webView2, and self.webView3 based on the toolbar hiding / showing
//		        CGRect webViewControllerFrame = self.webView.frame;
//		        webViewControllerFrame.size.height += (hidden ? -44 : 44);
//		        self.webView.frame = webViewControllerFrame;
//		
//		        // I hide all of the upper status bar and navigation bar, and bottom toolbar for a clean reading screen
//		        [[UIApplication sharedApplication] setStatusBarHidden:!hidden];
//		        [self.navigationController setNavigationBarHidden:!hidden animated:YES];
//		        self.toolbar.hidden = !hidden;
//		
//		        // I perform content relayout here in the now modified webView screen real estate
//		    }
		}
		
		public override void ViewWillAppear (bool animated)
		{
			base.ViewWillAppear (animated);
			
			toolbar.Alpha = 1f;
			var userInfo = new NSString ("MyUserInfo");
			var timer = NSTimer.CreateScheduledTimer (2, this, new Selector ("HideToolBar"), userInfo, false);
		}
		
		[Export("HideToolBar")]
		void HideToolBar (NSTimer timer)
		{
			toolbar.Alpha = 0f;
			Console.WriteLine ("hiding toolbar");
//			timer.Invalidate();
//			timer = null;
		}

		bool webViewShouldStartLoad (UIWebView webView, NSUrlRequest request, UIWebViewNavigationType navigationType)
		{
			Console.WriteLine (request.Url.AbsoluteString);
			if (request.Url.AbsoluteString.IndexOf ("#back") > -1) {
				NavigationController.PopViewControllerAnimated (true);
				return false;
			}
		
			return true;
		}
		
		private void onFailure (String msg)
		{
			// Ooops
		}
		
		private void onReferencesReceived (List<ReferenceNode> nodes)
		{
			MonoTouch.UIKit.UIApplication.SharedApplication.NetworkActivityIndicatorVisible = false;

			string html = @"<style type='text/css'>body { color: #000000; background-color: #FFFFFF; font-family: 'HelveticaNeue-Light', Helvetica, Arial, sans-serif; padding-bottom: 50px; } h1, h2, h3, h4, h5, h6 { padding: 0px; margin: 0px; font-style: normal; font-weight: normal; } h2 { font-family: 'HelveticaNeue', Helvetica, Arial, sans-serif; font-size: 18px; font-weight: bold; margin-bottom: -10px; padding-bottom: 0px; } h4 { font-size: 16px; } p { font-family: Helvetica, Verdana, Arial, sans-serif; line-height:1.5; font-size: 16px; } .esv-text { padding: 0 0 10px 0; } .description { border-radius: 5px; background-color:#F1F1F1; margin: 10px; padding: 8px; }</style>";
//			html += "<a href='#back'><img src='Images/btn_back.png' alt='back'/></a>";
			html += "<h1>" + paper.title + "</h1>";
			html += "<section class=\"description\">" + paper.description + "</section>";
						
			foreach (ReferenceNode node in nodes) {
				string content = node.reference.content;
				html += content;
			}

			InvokeOnMainThread (delegate {
				webView.LoadHtmlString (html, NSBundle.MainBundle.BundleUrl);//NSBundle.MainBundle.BundleUrl
			});
			
			
		}
		
		public override void ViewDidUnload ()
		{
			base.ViewDidUnload ();
			
			// Clear any references to subviews of the main view in order to
			// allow the Garbage Collector to collect them sooner.
			//
			// e.g. myOutlet.Dispose (); myOutlet = null;
			
			ReleaseDesignerOutlets ();
		}
		
		public override bool ShouldAutorotateToInterfaceOrientation (UIInterfaceOrientation toInterfaceOrientation)
		{
			// Return true for supported orientations
			return (toInterfaceOrientation != UIInterfaceOrientation.PortraitUpsideDown);
		}

		partial void sharePressed (MonoTouch.UIKit.UIBarButtonItem sender1)
		{
//			UIActionSheet *actionSheet =[[UIActionSheet alloc]
//										 initWithTitle:@"Share this paper"
//										 delegate:self 
//										 cancelButtonTitle:@"Cancel" 
//										 destructiveButtonTitle:nil
//										 otherButtonTitles:@"E-mail",@"Twitter",@"Facebook", nil];
//			[actionSheet showInView:self.view];

			var actionSheet = new UIActionSheet ("Sharing", null, "Cancel", null, null){
				Style = UIActionSheetStyle.BlackTranslucent
			};
			actionSheet.AddButton("Email");
			actionSheet.AddButton("Twitter");
			actionSheet.AddButton("Facebook");
			actionSheet.Clicked += HandleShareOptionClicked;
				
			actionSheet.ShowInView (View);
		}

		void HandleShareOptionClicked (object sender, UIButtonEventArgs e)
		{
			UIActionSheet myActionSheet = sender as UIActionSheet;
			Console.WriteLine ("Clicked on item {0}", e.ButtonIndex);
			if( e.ButtonIndex != myActionSheet.CancelButtonIndex) {
				
				string paperTitle = paper.title;
				string urlTitle = paper.url_title;
				string subject = "White Paper Bible: " + paperTitle;
				string paperFullURL = "http://whitepaperbible.org/" + urlTitle;

				string messageCombined = subject + Environment.NewLine + paperFullURL;

				if(e.ButtonIndex == 1){
					//email
					if(MFMailComposeViewController.CanSendMail){
						MFMailComposeViewController _mail = new MFMailComposeViewController ();
						_mail.SetSubject( subject );
			            _mail.SetMessageBody ( messageCombined, false);
			            _mail.Finished += HandleMailFinished;
			            this.PresentModalViewController (_mail, true);
					}			
				}
				else if(e.ButtonIndex == 2){
					TWTweetComposeViewController tweetComposer = new TWTweetComposeViewController();
					tweetComposer.SetInitialText(paperTitle + " | " + paperFullURL);
					this.PresentModalViewController( tweetComposer, true );
				}
				else if(e.ButtonIndex == 3){
					//facebook
					string encodedURLString = HttpUtility.UrlEncode(paperFullURL);
					string URLString = @"http://www.facebook.com/sharer.php?u=" + encodedURLString;
					UIApplication.SharedApplication.OpenUrl(NSUrl.FromString(URLString));

				}
		    }
		
		}

		void HandleMailFinished (object sender, MFComposeResultEventArgs e){

		    if (e.Result == MFMailComposeResult.Sent) {
		        UIAlertView alert = new UIAlertView ("Mail Alert", "Mail Sent",
		            null, "Yippie", null);
		        alert.Show ();
		        
		        // you should handle other values that could be returned 
		        // in e.Result and also in e.Error 
		    }
		    e.Controller.DismissModalViewControllerAnimated (true);
		}

		partial void favoritePressed (MonoTouch.UIKit.UIBarButtonItem sender)
		{

//			WhitePaperBibleAppDelegate *appDelegate = (WhitePaperBibleAppDelegate *)[[UIApplication sharedApplication] delegate];
//	
//			if([appDelegate isUserLoggedIn]){
//				NSString *paperIDToFavorite = [[NSString alloc] initWithFormat:@"%@",[paper valueForKey:@"id"]];
//				if(!isFavorite){
//					[favoriteButton setImage:[UIImage imageNamed:@"favorited.png"]];
//					
//					NSString *postPath = [NSString stringWithFormat:@"/favorite/create/%@",paperIDToFavorite];
//					[HRRestModel postPath:postPath withOptions:nil object:nil];
//					isFavorite = YES;
//					//send a call out to favorite the paper
//				}
//				else{
//					//[favoriteButton setTitle:@"FAV"];
//					[favoriteButton setImage:[UIImage imageNamed:@"favorites.png"]];
//					isFavorite = NO;
//					NSString *postPath = [NSString stringWithFormat:@"/favorite/destroy/%@",paperIDToFavorite];
//					[HRRestModel deletePath:postPath withOptions:nil object:nil];
//					//send a call out to UNfavorite the paper
//				}
//			}
//			else{
//		
//				NotLoggedInViewController *notLoggedInViewController = [[NotLoggedInViewController alloc] initWithNibName:@"NotLoggedInViewController"  bundle:[NSBundle mainBundle]];
//				notLoggedInViewController.title = [paper valueForKey:@"title"];
//			
//				
//				[self presentModalViewController:notLoggedInViewController animated:YES];
//				
//				[notLoggedInViewController release];
//				
//				
//			}
//		
		}

		protected UIWebView webViewTest;
		protected void AddWebView ()
		{
//			Font = UIFont.FromName ("OpenSans", 14),
//				TextColor = UIColor.FromRGB (67, 67, 67),
//				TextAlignment = UITextAlignment.Left,
//				BackgroundColor = UIColor.Clear,
//				Text = '<a href="http://mydiem.com/index.cfm?event=main.faq">Help</a><br/><a href="mailto:support@mydiem.com">Email Support</a>',
//				LineBreakMode = UILineBreakMode.WordWrap,
//				Lines = 0
			var style = "<style type='text/css'>body { color: #000000; background-color: transparent; font-family: 'HelveticaNeue-Light', Helvetica, Arial, sans-serif; padding: 20px; } h1, h2, h3, h4, h5, h6 { padding: 0px; margin: 0px; font-style: normal; font-weight: normal; } h2 { font-family: 'HelveticaNeue-Light', Helvetica, Arial, sans-serif; font-size: 24px; font-weight: normal; } h4 { font-size: 16px; } p { font-family: Helvetica, Verdana, Arial, sans-serif; line-height:1.5; font-size: 16px; } .esv-text { padding: 0 0 10px 0; }</style>";
			var html = style + "<body><h2>Contact</h2><a href='http://mydiem.com/index.cfm?event=main.faq'>Help</a><br/><a href='mailto:support@mydiem.com'>Email Support</a><br/><br/>";
			html += "<h2>Our Story</h2><p>If you've ever sent your child empty-handed to a class party, attended parent/teacher conferences on the wrong day or arrived an hour early for a game, you're not alone. It's not bad parenting, it's bad scheduling and trust us, we've been there too.</p><p>Year after year we would receive paper calendars from our children's schools, get several emails a week, or would be encouraged to visit websites to view upcoming events (sports teams, Boy Scouts/ Girl Scouts, ballet, band and track club all had different sites!). Sometimes we would enter the information into a paper agenda, which couldn't be shared. Or we would type everything into our computer's calendar hoping the information wouldn't change. And, as the school year progressed, updates would take the form of crumpled notes, skimmed over emails and hurried messages from coaches and teachers that didn't always make it to the master calendar. We'd often ask ourselves, \"Why can't school events appear on our smart phones or computer calendar programs?\" or \"If there is a change, couldn't someone update the calendar for us so we don't have to keep track of emails, notes, etc? Then, one day, we stopped wishing and got to work.</p><p>We are parents to three school-age (and very busy) children and now we\'re also the proud parents of MyDiem.com. We're so happy to share this much-needed tool with other busy parents. Hopefully you will find this tool helpful in keeping track of your child's activities.</p>";
			html += "</body>";

			webViewTest = new UIWebView (new RectangleF (0, 0, 320, 435));
			webViewTest.SizeToFit ();
			View.AddSubview (webViewTest);

			webViewTest.Opaque = false;
			webViewTest.BackgroundColor = UIColor.Clear;
			webViewTest.LoadHtmlString(html, NSUrl.FromString("http://localhost"));

//			DescriptionLabel.ShouldStartLoad = myHandler;
		}	

	}
	
	public class GestureDelegate : UIGestureRecognizerDelegate
	{
		public override bool ShouldReceiveTouch (UIGestureRecognizer recognizer, UITouch touch)
		{
			return true;
		}
		
		public override bool ShouldBegin (UIGestureRecognizer recognizer)
		{
			return true;
		}
		
		public override bool ShouldRecognizeSimultaneously (UIGestureRecognizer gestureRecognizer, UIGestureRecognizer otherGestureRecognizer)
		{
			return true;
		}
//		
//		[Export("HandleTapFrom")]
//		void HandleTapFrom(UITapGestureRecognizer recognizer){
//			Console.WriteLine("selector from within delegate");	
//		}
		
		
	}
}

