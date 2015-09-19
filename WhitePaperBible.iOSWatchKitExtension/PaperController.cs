using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;
using WhitePaperBible.WatchShared;
using WhitePaperBible.WatchShared.MessageParams;
using WhitePaperBible.Core.Models;

namespace WhitePaperBible.iOSWatchKitExtension
{
	partial class PaperController : WatchKit.WKInterfaceController
	{
		public PaperController (IntPtr handle) : base (handle)
		{
		}

		public override void Awake (NSObject context)
		{
			base.Awake (context);

			var p = context as PaperDTO;
//			if(p != null){
//				TitleLabel.SetText(p.Title);
//			}else{
//				TitleLabel.SetText("NULL BABY");
//			}

			// get paper references
			GetPaper(p.ID);
		}

		async void GetPaper (int paperId)
		{
			WatchMessage<PaperResponseParams> responseMessage = null;

			WatchMessage<PaperRequestParams> requestParams = new WatchMessage	<PaperRequestParams>();
			requestParams.Params.Paper = new Paper(){ id = paperId};

			try {
				responseMessage = await WatchMessenger.RequestMessage<PaperResponseParams, PaperRequestParams> (WatchAction.Paper, requestParams);
				Console.WriteLine ("Got Paper");
				var paper = (Paper)responseMessage.Params.Paper;
				this.ContentLabel.SetText( GetAttributedStringFromHtml(paper.WatchText) );
				this.AuthorLabel.SetText(string.Format("By {0}", paper.AuthorName));
				this.ViewsLabel.SetText(string.Format("{0} Views", paper.view_count));
			} catch (Exception ex) {
				Console.WriteLine (ex);
			}
		}

		protected NSAttributedString GetAttributedStringFromHtml(string html)
		{
			NSError error = null;
			NSAttributedString attributedString = new NSAttributedString (NSData.FromString(html), 
				new NSAttributedStringDocumentAttributes{ DocumentType = NSDocumentType.HTML, StringEncoding = NSStringEncoding.UTF8 }, 
				ref error);
			return attributedString;
		}

		public override void WillActivate ()
		{
			base.WillActivate ();
		}
	}
}
