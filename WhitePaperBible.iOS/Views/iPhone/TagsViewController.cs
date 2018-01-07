using System;
using System.Collections.Generic;
using Forms;
using IOS.Util;
using MonkeyArms;
using UIKit;
using WhitePaperBible.Core.Models;
using WhitePaperBible.Core.Views;
using WhitePaperBible.iOS;
using Xamarin.Forms;

namespace iPhone
{
    public partial class TagsViewController : UIViewController, ITagsListView
    {
        public TagsViewController () : base ("TagsViewController", null)
        {
              this.Filter = new Invoker ();
              this.OnTagSelected = new Invoker ();
        }

        public override void ViewDidLoad ()
        {
            base.ViewDidLoad ();
            AnalyticsUtil.TrackScreen (this.Title);
        }

        public override void ViewDidLayoutSubviews ()
        {
            base.ViewDidLayoutSubviews ();

            if(tagsListView == null){
                Xamarin.Forms.Forms.Init ();
                tagsListView = new TagsListView ();
                tagsListView.Tags = tags;
                tagsListView.ItemSelected += TagsListView_ItemSelected;
                ListContainer.AddSubview (tagsListView.CreateViewController ().View);
            }
        }

        public override void DidReceiveMemoryWarning ()
        {
            base.DidReceiveMemoryWarning ();
            // Release any cached data, images, etc that aren't in use.
        }

        public override void ViewWillAppear (bool animated)
        {
            base.ViewWillAppear (animated);
            DI.RequestMediator (this);
        }

        public override void ViewDidDisappear (bool animated)
        {
            base.ViewDidDisappear (animated);

            DI.DestroyMediator (this);
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

        List<Tag> tags;

        public void SetTags (List<Tag> tags)
        {
            InvokeOnMainThread (delegate {
                if (tagsListView != null) {
                    tagsListView.Tags = tags;
                }else{
                    this.tags = tags;
                }
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

        void TagsListView_ItemSelected (object sender, EventArgs e)
        {
            Tag tag = (WhitePaperBible.Core.Models.Tag)(e as SelectedItemChangedEventArgs).SelectedItem;
            var papersByTagList = new PapersByTagView ();
            papersByTagList.SelectedTag = tag;
            papersByTagList.Title = tag.name;
            this.NavigationController.PushViewController (papersByTagList, true);
        }
    }
}

