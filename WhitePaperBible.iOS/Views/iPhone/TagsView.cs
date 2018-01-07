using System;
using System.Collections.Generic;
using System.Linq;
using WhitePaperBible.Core.Models;
using Foundation;
using UIKit;
using MonoTouch.Dialog;
using WhitePaperBible.Core.Views;
using MonkeyArms;
using IOS.Util;
using Forms;
using Xamarin.Forms;

namespace WhitePaperBible.iOS
{
    public partial class TagsView : UIViewController, ITagsListView
	{
        public TagsView () : base()
        {

        }

  //      public TagsView () : base (UITableViewStyle.Plain, null, true)
		//{
		//	EnableSearch = true; 
		//	AutoHideSearch = true;
		//	SearchPlaceholder = @"Find Tags";
		//	this.Filter = new Invoker ();
		//	this.OnTagSelected = new Invoker ();
		//}

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
                tagsListView.Tags = tags;

			//	Root = new RootElement ("Tags") {
			//		from node in tags
			//		group node by (node.name [0].ToString ().ToUpper ()) into alpha
			//		orderby alpha.Key
			//		select new Section (alpha.Key) {
			//			from eachNode in alpha
			//			select (Element)new WhitePaperBible.iOS.UI.CustomElements.TagElement (eachNode)
			//		}
			//	};

			//	TableView.ScrollToRow (NSIndexPath.FromRowSection (0, 0), UITableViewScrollPosition.Top, false);
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

        TagsListView tagsListView;

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			DI.RequestMediator (this);

            tagsListView = new TagsListView ();
            this.View = tagsListView.CreateViewController ().View;

			//SearchTextChanged += (sender, args) => {
			//	Console.WriteLine ("search text changed");	
			//};



			AnalyticsUtil.TrackScreen (this.Title);
		}

		public override void ViewDidDisappear (bool animated)
		{
			base.ViewDidDisappear (animated);

			DI.DestroyMediator (this);
		}
	}
}
