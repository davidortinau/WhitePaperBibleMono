using System;
using System.Collections.Generic;
using System.Linq;
using WhitePaperBible.Core.Models;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using MonoTouch.Dialog;
using WhitePaperBible.Core.Views;
using MonkeyArms;
using WhitePaperBible.iOS.Invokers;
using WhitePaperBible.iOS.UI.CustomElements;
using IOS.Util;

namespace WhitePaperBible.iOS
{
	public partial class PapersView : DialogViewController
	{
		UINavigationController NavController;

		public Invoker RequestAddPaper {
			get;
			private set;
		}

		public PapersView (UINavigationController controller) : base (UITableViewStyle.Plain, null, true)
		{
			NavController = controller;
			EnableSearch = true; 
			AutoHideSearch = true;
			SearchPlaceholder = @"Find Papers";
//			this.Filter = new Invoker ();
//			this.OnPaperSelected = new Invoker ();
			this.AddPaper = new Invoker ();

		}



		#region IPapersListView implementation

//		public Invoker Filter {
//			get;
//			private set;
//		}
//
//		public Invoker OnPaperSelected {
//			get;
//			private set;
//		}

		public Invoker AddPaper {
			get;
			private set;
		}

		public void SetPapers (List<Paper> papers)
		{
			Console.WriteLine ("SetPapers. {0}", papers.Count);

			if(papers == null || papers.Count == 0){
				return;
			}

			InvokeOnMainThread (()=> {

				Root = new RootElement ("Papers") {
					new Section(""){
						AddPaperElements(papers)
					}
//					from node in papers
//					group node by (node.title [0].ToString ().ToUpper ()) into alpha
//					orderby alpha.Key
//					select new Section (alpha.Key) {
//						from eachNode in alpha
//						select (Element)new WhitePaperBible.iOS.UI.CustomElements.PaperElement (eachNode, delegate {
//							var paperDetails = new WhitePaperBible.iOS.PaperDetailsView(eachNode);
//							paperDetails.Title = eachNode.title;
//							NavigationController.PushViewController(paperDetails, true);
//						})
//					}
				};

				TableView.ScrollToRow (NSIndexPath.FromRowSection (0, 0), UITableViewScrollPosition.Top, false);

			});

		}

		List<PaperElement> AddPaperElements (List<Paper> papers)
		{
			var elements = new List<PaperElement> ();
			foreach(var p in papers){
				elements.Add (new PaperElement (p, () => {
					var paperDetails = new PaperDetailsView(p);
					paperDetails.Title = p.title;
					NavController.PushViewController(paperDetails, true);
				}));
			}
			return elements;
		}

		public string SearchPlaceHolderText {
			get;
			set;
		}

		public string SearchQuery {
			get;
			set;
		}

		public Paper SelectedPaper {
			get;
			set;
		}

		#endregion

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			SearchTextChanged += (sender, args) => {
				Console.WriteLine ("search text changed");	
			};			
		}

		public override void ViewDidDisappear (bool animated)
		{
			base.ViewDidDisappear (animated);


		}

		public override void ViewWillAppear (bool animated)
		{
			base.ViewWillAppear (animated);
//			DI.RequestMediator (this);
		}

		public override void ViewWillDisappear (bool animated)
		{
			base.ViewWillDisappear (animated);
//			DI.DestroyMediator (this);
		}
	}
}
