using System;
using System.Drawing;

using MonoTouch.Foundation;
using MonoTouch.UIKit;
using WhitePaperBibleCore.Models;
using WhitePaperBibleCore.Services;
using System.Collections.Generic;
using MonoTouch.ObjCRuntime;

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
		 * 
		 * */
		
		
		public PaperDetailsView (Paper paper) : base ("PaperDetailsView", null)
		{
			this.paper = paper;
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
			this.NavigationController.SetNavigationBarHidden (true, false);
			
			webView.ShouldStartLoad += webViewShouldStartLoad;
			
			var tapRecognizer = new UITapGestureRecognizer (this, new Selector ("HandleTapFrom"));
			tapRecognizer.NumberOfTapsRequired = 1;
			tapRecognizer.Delegate = new GestureDelegate ();
			
			webView.AddGestureRecognizer (tapRecognizer);
			
//			UITapGestureRecognizer *tapRecognizer = [[UITapGestureRecognizer alloc] initWithTarget:self action:@selector(HandleTapFrom:)];
//    tapRecognizer.numberOfTapsRequired = 1;
//    tapRecognizer.delegate = self;
//    [self.webView addGestureRecognizer:tapRecognizer];
			
		}
		
		[Export("HandleTapFrom")]
		public void HandleTapFrom (UITapGestureRecognizer recognizer)
		{
			Console.WriteLine ("getting taps");
			
			// so the question is how long were we holding it down? Has the user initialized text selection?
			var selection = webView.EvaluateJavascript ("window.getSelection().toString()");
			Console.WriteLine("selection {0}", selection);
			
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
			
			
			string html = @"<style type='text/css'>body { color: #000000; background-color: white; font-family: 'HelveticaNeue-Light', Helvetica, Arial, sans-serif; padding-bottom: 50px; } h1, h2, h3, h4, h5, h6 { padding: 0px; margin: 0px; font-style: normal; font-weight: normal; } h2 { font-family: 'HelveticaNeue', Helvetica, Arial, sans-serif; font-size: 18px; font-weight: bold; margin-bottom: -10px; padding-bottom: 0px; } h4 { font-size: 16px; } p { font-family: Helvetica, Verdana, Arial, sans-serif; line-height:1.5; font-size: 16px; } .esv-text { padding: 0 0 10px 0; }</style>";
			html += "<a href='#back'>back</a><h1>" + paper.title + "</h1>";
			
			foreach (ReferenceNode node in nodes) {
				string content = node.reference.content;
				html += content;
			}
			
			webView.LoadHtmlString (html, NSUrl.FromString ("http://whitepaperbible.org"));
			
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

