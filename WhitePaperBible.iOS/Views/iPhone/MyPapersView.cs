using System;
using System.Collections.Generic;
using System.Linq;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using MonoTouch.Dialog;
using WhitePaperBible.Core.Views;
using MonkeyArms;
using WhitePaperBible.Core.Models;
using WhitePaperBible.iOS.Invokers;
using WhitePaperBible.Core.Invokers;

namespace WhitePaperBible.iOS
{
	public partial class MyPapersView : DialogViewController, IMyPapersView, IMediatorTarget
	{
		public MyPapersView () : base (UITableViewStyle.Plain, null, true)
		{
			EnableSearch = true; 
			AutoHideSearch = true;
			SearchPlaceholder = @"Find Papers";
			this.Filter = new Invoker ();
			this.OnPaperSelected = new Invoker ();
			this.OnLogoutRequested = new Invoker ();

			// add logout in upper left
			NavigationItem.SetLeftBarButtonItem (
				new UIBarButtonItem ("Logout", UIBarButtonItemStyle.Plain, (sender, args)=> {
					OnLogoutRequested.Invoke();
				})
				, true
			);

//			this.TableView.Frame = new System.Drawing.RectangleF (0, 110, View.Bounds.Width, View.Bounds.Height - 110);

//			AddProfileView ();

		}

//		void AddProfileView ()
//		{
//			var profileView = new ProfileView ();
//			View.AddSubview (profileView);
//		}

		#region IPapersListView implementation

		public Invoker Filter {
			get;
			private set;
		}

		public Invoker OnPaperSelected {
			get;
			private set;
		}

		public Invoker OnLogoutRequested {
			get;
			private set;
		}

		public void SetPapers (List<Paper> papers)
		{
			if(papers == null || papers.Count == 0){
				return;
			}

			InvokeOnMainThread (delegate {

				Root = new RootElement ("Papers") {
					from node in papers
					group node by (node.title [0].ToString ().ToUpper ()) into alpha
					orderby alpha.Key
					select new Section (alpha.Key) {
						from eachNode in alpha
						select (Element)new WhitePaperBible.iOS.UI.CustomElements.PaperElement (eachNode, delegate {
							this.OnPaperSelected.Invoke(new ShowMyPaperInvokerArgs(eachNode));
						})
					}
				};

				TableView.ScrollToRow (NSIndexPath.FromRowSection (0, 0), UITableViewScrollPosition.Top, false);
			});

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

//		public override void ViewDidAppear (bool animated)
//		{
//			base.ViewDidAppear (animated);
//		}
//
//		public override void ViewWillAppear (bool animated)
//		{
//			base.ViewWillAppear (animated);
//			DI.RequestMediator (this);
//			this.Title = "My Papers";
//		}
//
//		public override void ViewDidDisappear (bool animated)
//		{
//			base.ViewDidDisappear (animated);
//			DI.DestroyMediator (this);
//		}
	}
}
