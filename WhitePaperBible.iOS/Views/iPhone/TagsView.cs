using System;
using System.Collections.Generic;
using System.Linq;
using WhitePaperBible.Core.Models;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using MonoTouch.Dialog;
using WhitePaperBible.Core.Views;
using MonkeyArms;

namespace WhitePaperBible.iOS
{
	public partial class TagsView : DialogViewController, ITagsListView
	{
		public TagsView () : base (UITableViewStyle.Plain, null, true)
		{
			EnableSearch = true; 
			AutoHideSearch = true;
			SearchPlaceholder = @"Find Tags";
			this.Filter = new Invoker ();
			this.OnTagSelected = new Invoker ();
		}

		#region ITagsListView implementation

		public Invoker Filter {
			get;
			private set;
		}

		public Invoker OnTagSelected {
			get;
			private set;
		}

		public void SetTags (List<Tag> tags)
		{
			InvokeOnMainThread (delegate {

				Root = new RootElement ("Tags") {
					from node in tags
					group node by (node.name [0].ToString ().ToUpper ()) into alpha
					orderby alpha.Key
					select new Section (alpha.Key) {
						from eachNode in alpha
						select (Element)new WhitePaperBible.iOS.UI.CustomElements.TagElement (eachNode)
					}
				};

//				TableView.ScrollToRow (NSIndexPath.FromRowSection (0, 0), UITableViewScrollPosition.Top, false);
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

		public Tag SelectedTag {
			get;
			set;
		}

		#endregion

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			DI.RequestMediator (this);

			SearchTextChanged += (sender, args) => {
				Console.WriteLine ("search text changed");	
			};
			
		}

		public override void ViewDidDisappear (bool animated)
		{
			base.ViewDidDisappear (animated);

			DI.DestroyMediator (this);
		}
	}
}
