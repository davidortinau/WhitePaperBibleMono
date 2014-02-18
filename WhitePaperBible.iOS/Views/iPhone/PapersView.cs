using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

using WhitePaperBibleCore.Models;
using WhitePaperBibleCore.Services;
using WhitePaperBible.iOS.TableSource;

using MonoTouch.Foundation;
using MonoTouch.UIKit;
using MonoTouch.Dialog;

namespace WhitePaperBible.iOS
{
	public partial class PapersView : DialogViewController
	{
		public PapersView () : base (UITableViewStyle.Plain, null, true)
		{
			EnableSearch = true; 
			AutoHideSearch = true;
			SearchPlaceholder = @"Find Papers";
		}
		
		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			
			MonoTouch.UIKit.UIApplication.SharedApplication.NetworkActivityIndicatorVisible = true;
			
			// rather than do this here, have the AppDelegate load up the initial data and call it off the model here
			var svc = new PaperService ();
			svc.GetPapers (onPapersReceived, onErrorReceived);
			
			SearchTextChanged += (sender, args) => {
				Console.WriteLine("search text changed");	
			};
			
		}
		
		private void onErrorReceived (string error)
		{
			MonoTouch.UIKit.UIApplication.SharedApplication.NetworkActivityIndicatorVisible = false;
		}

		private void onPapersReceived (List<PaperNode> papers)
		{
			MonoTouch.UIKit.UIApplication.SharedApplication.NetworkActivityIndicatorVisible = false;
			
			InvokeOnMainThread (delegate {
				AppDelegate.papers = papers;
				
				Root = new RootElement("Papers") {
						from node in papers
							group node by (node.paper.title [0].ToString ().ToUpper ()) into alpha
							orderby alpha.Key
						select new Section (alpha.Key){
							from eachNode in alpha
						select (Element)new WhitePaperBible.iOS.UI.CustomElements.PaperElement (eachNode)
				}};
	
				TableView.ScrollToRow (NSIndexPath.FromRowSection (0, 0), UITableViewScrollPosition.Top, false);
			});
		}
	}
}
