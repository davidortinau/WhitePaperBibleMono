using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;
using System.Collections.Generic;
using WhitePaperBible.Core.Models;
using WhitePaperBible.WatchShared;
using WhitePaperBible.WatchShared.MessageParams;
using WatchKit;

namespace WhitePaperBible.iOSWatchKitExtension
{
	partial class AllPapersController : WatchKit.WKInterfaceController
	{
		public AllPapersController (IntPtr handle) : base (handle)
		{
		}

		List<Paper> papers = new List<Paper>();

		public override void Awake (NSObject context)
		{
			base.Awake (context);

			GetPapers();
		}

		async void GetPapers ()
		{
			WatchMessage<PapersResponseParams> responseMessage = null;
			WatchMessage<ActionRequestParams> requestParams = new WatchMessage	<ActionRequestParams>();

			try {
				Console.WriteLine("Favorites - GET");
				responseMessage = await WatchMessenger.RequestMessage<PapersResponseParams, ActionRequestParams> (WatchAction.Papers, requestParams);
				papers = responseMessage.Params.Papers;
				if(papers == null){
					Console.WriteLine ("Null Papers");
					//					this.SetTitle("Error!");
					PapersTable.SetNumberOfRows ((nint)papers.Count, "default");
					var elementRow = (PaperRow)PapersTable.GetRowController (0);
					elementRow.SetData ("Couldn't find any papers.");

				}else{
					Console.WriteLine ("Got Papers {0}", papers.Count);
					LoadTableRows();
				}
			} catch (Exception ex) {
				Console.WriteLine (ex);
			}
		}

		public override void WillActivate ()
		{
			base.WillActivate ();

			LoadTableRows();
		}

		void LoadTableRows ()
		{
			if(papers == null){
				GetPapers();
				return;
			}

			PapersTable.SetNumberOfRows ((nint)papers.Count, "default");
			//myTable.SetRowTypes (new [] {"default", "type1", "type2", "default", "default"});
			// Create all of the table rows.
			for (var i = 0; i < papers.Count; i++) {
				var elementRow = (PaperRow)PapersTable.GetRowController (i);

				elementRow.SetData (papers [i].title);
			}
		}

		public override void DidSelectRow (WKInterfaceTable table, nint rowIndex)
		{
			var rowData = papers [(int)rowIndex];

			var paper = (Paper)rowData;
			//			var d = new NSDictionary ();
			//			d.SetValueForKey((NSString)rowData, new NSString("paperTitle"));
			PushController ("PaperController", new PaperDTO(){Title = paper.title, ID = paper.id});


			//			PushController ("PaperController", rowData);
			Console.WriteLine ("Row selected:" + paper.title);
		}
	}
}
