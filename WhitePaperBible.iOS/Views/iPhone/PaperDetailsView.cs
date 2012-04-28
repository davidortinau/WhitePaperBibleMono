using System;
using System.Drawing;

using MonoTouch.Foundation;
using MonoTouch.UIKit;
using WhitePaperBibleCore.Models;
using WhitePaperBibleCore.Services;
using System.Collections.Generic;

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
			var svc = new PaperService();
			svc.GetPaperReferences(paper.id, onReferencesReceived, onFailure);
			
			this.Title = paper.title;
	
			webView.ScrollView.ScrollEnabled = true;
			this.NavigationController.SetNavigationBarHidden(true, false);
			
			webView.ShouldStartLoad += webViewShouldStartLoad;
			
		}

		bool webViewShouldStartLoad (UIWebView webView, NSUrlRequest request, UIWebViewNavigationType navigationType)
		{
			Console.WriteLine(request.Url.AbsoluteString);
			if(request.Url.AbsoluteString.IndexOf("#back") > -1){
				NavigationController.PopViewControllerAnimated(true);
				return false;
			}
		
			return true;
		}
		
		private void onFailure(String msg){
			// Ooops
		}
		
		private void onReferencesReceived(List<ReferenceNode> nodes){
			MonoTouch.UIKit.UIApplication.SharedApplication.NetworkActivityIndicatorVisible = false;
			
			
			string html = @"<style type='text/css'>body { color: #000000; background-color: white; font-family: 'HelveticaNeue-Light', Helvetica, Arial, sans-serif; padding-bottom: 50px; } h1, h2, h3, h4, h5, h6 { padding: 0px; margin: 0px; font-style: normal; font-weight: normal; } h2 { font-family: 'HelveticaNeue', Helvetica, Arial, sans-serif; font-size: 18px; font-weight: bold; margin-bottom: -10px; padding-bottom: 0px; } h4 { font-size: 16px; } p { font-family: Helvetica, Verdana, Arial, sans-serif; line-height:1.5; font-size: 16px; } .esv-text { padding: 0 0 10px 0; }</style>";
			html += "<a href='#back'>back</a><h1>" + paper.title + "</h1>";
			
			foreach(ReferenceNode node in nodes){
				string content = node.reference.content;
				html += content;
			}
			
			webView.LoadHtmlString(html, NSUrl.FromString("http://whitepaperbible.org"));
			
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
}

