using System;
using System.Collections.Generic;
using System.Linq;

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
			EnableSearch = true; // requires Element to implement Matches()
			AutoHideSearch = true;
			SearchPlaceholder = @"Find Papers";
		}
		
		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			
			MonoTouch.UIKit.UIApplication.SharedApplication.NetworkActivityIndicatorVisible = true;
			var svc = new PaperService ();
			svc.GetPapers (onPapersReceived, onErrorReceived);
		}
		
		private void onErrorReceived (string error)
		{
			// OOPS
			MonoTouch.UIKit.UIApplication.SharedApplication.NetworkActivityIndicatorVisible = false;
		}

		private void onPapersReceived (List<PaperNode> papers)
		{
			MonoTouch.UIKit.UIApplication.SharedApplication.NetworkActivityIndicatorVisible = false;
			
			Root = new RootElement ("Papers") {
					from node in papers
				group node by (node.paper.title[0].ToString().ToUpper()) into alpha
					orderby alpha.Key
				select new Section (alpha.Key){
					from eachNode in alpha
					select (Element)new WhitePaperBible.iOS.UI.CustomElements.PaperElement (eachNode)
				}};

			
			TableView.Source = new PapersTableSource (papers, this);
			TableView.ScrollToRow (NSIndexPath.FromRowSection (0, 0), UITableViewScrollPosition.Top, false);
		}
	}
}
