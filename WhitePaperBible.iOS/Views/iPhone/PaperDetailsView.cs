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
			
			// Perform any additional setup after loading the view, typically from a nib.
			var svc = new PaperService();
			svc.GetPaperReferences(paper.id, onReferencesReceived, onFailure);
		}
		
		private void onFailure(String msg){
			// Ooops
		}
		
		private void onReferencesReceived(List<ReferenceNode> nodes){
			// do what?	
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

