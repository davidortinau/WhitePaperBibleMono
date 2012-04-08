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
			var svc = new PaperService();
			svc.GetPapers(onPapersReceived, onErrorReceived);
		}
		
		private void onErrorReceived(string error)
        {
            // OOPS
        }

        private void onPapersReceived(List<PaperNode> papers)
        {
			TableView.Source = new PapersTableSource(papers, this);
			TableView.ScrollToRow (NSIndexPath.FromRowSection (0,0), UITableViewScrollPosition.Top, false);
        }
	}
}
