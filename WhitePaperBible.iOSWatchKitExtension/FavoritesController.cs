using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;
using System.Collections.Generic;
using WatchKit;
using Newtonsoft.Json;

namespace WhitePaperBible.iOSWatchKitExtension
{
	partial class FavoritesController : WatchKit.WKInterfaceController
	{
		List<string> papers = new List<string>();

		public FavoritesController (IntPtr handle) : base (handle)
		{
		}

		public override void Awake (NSObject context)
		{
			base.Awake (context);

			GetFavorites();
		}

		void GetFavorites ()
		{
			WKInterfaceController.OpenParentApplication (new NSDictionary (), (replyInfo, error) => {
				if(error != null) {
					Console.WriteLine (error);
					return;
				}

				NSString json = replyInfo.ValueForKey(new NSString("payload")) as NSString;
				papers = JsonConvert.DeserializeObject<List<string>>(json);
				LoadTableRows();
			});
		}

		public override void WillActivate ()
		{
			base.WillActivate ();

			LoadTableRows();
		}

		void LoadTableRows ()
		{
			PapersTable.SetNumberOfRows ((nint)papers.Count, "default");
			//myTable.SetRowTypes (new [] {"default", "type1", "type2", "default", "default"});
			// Create all of the table rows.
			for (var i = 0; i < papers.Count; i++) {
				var elementRow = (PaperRow)PapersTable.GetRowController (i);

				elementRow.SetData (papers [i]);
			}
		}

		public override void DidSelectRow (WKInterfaceTable table, nint rowIndex)
		{
			var rowData = papers [(int)rowIndex];

			var paper = new Paper(){
				Title = rowData
			};
//			var d = new NSDictionary ();
//			d.SetValueForKey((NSString)rowData, new NSString("paperTitle"));
			PushController ("PaperController", paper);


//			PushController ("PaperController", rowData);
			Console.WriteLine ("Row selected:" + rowData);
		}
	}
}
