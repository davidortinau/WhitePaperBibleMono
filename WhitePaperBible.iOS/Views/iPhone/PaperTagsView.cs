
using System;
using System.Collections.Generic;
using System.Linq;

using MonoTouch.Foundation;
using MonoTouch.UIKit;
using MonoTouch.Dialog;
using WhitePaperBible.Core.Models;
using MonkeyArms;
using WhitePaperBible.Core.Views;
using WhitePaperBible.Core.Invokers;
using System.Linq;

namespace WhitePaperBible.iOS
{
	public partial class PaperTagsView : DialogViewController, IMediatorTarget, IPaperTagsView
	{
		public EditPaperView Controller {
			get;
			set;
		}

		EntryElement NewTagEl;

		Section TagsSection;

		public List<Tag> tags;

		public Invoker Save {
			get;
			private set;
		}

		public PaperTagsView () : base (UITableViewStyle.Grouped, null)
		{
			Save = new Invoker ();

			Root = new RootElement ("Add Tags") {
				new Section ("") {
					(NewTagEl = new EntryElement ("", "New Tag Name",string.Empty))
				}
			};

		}

		public void SetTags(List<Tag> tags,List<Tag> paperTags)
		{
			TagsSection = new Section ("");
			foreach(var t in tags){
				var el = new CheckboxElement (t.name);
				el.Value = IsTagSelected (t.name, Controller.Tags);
				TagsSection.Add (el);
			}

			Root.Add (TagsSection);
		}

		bool IsTagSelected (string name, List<Tag> tags)
		{
			return (tags != null) && tags.Exists (x => x.name == name);
		}

		public override void ViewDidAppear (bool animated)
		{
			base.ViewDidAppear (animated);

			NavigationItem.SetRightBarButtonItem (
				new UIBarButtonItem ("Save", UIBarButtonItemStyle.Plain, (sender, args)=> {
					ReturnTags();
					this.NavigationController.PopViewControllerAnimated(true);
				})
				, true
			);

			NavigationItem.SetLeftBarButtonItem (
				new UIBarButtonItem ("Back", UIBarButtonItemStyle.Plain, (sender, args)=> {
					ReturnTags();
					this.NavigationController.PopViewControllerAnimated(true);
				})
				, true
			);
		}

		void ReturnTags ()
		{
			tags = new List<Tag> ();

			if(!String.IsNullOrEmpty(NewTagEl.Value)){
				tags.Add(new Tag(){name = NewTagEl.Value});
			}

			foreach(CheckboxElement cb in TagsSection.Elements){
				if(cb.Value){
					tags.Add(new Tag(){name = cb.Caption});
				}
			}

			Controller.Tags = tags;
		}

		public override void ViewWillAppear (bool animated)
		{
			base.ViewWillAppear (animated);
			DI.RequestMediator (this);
		}

		public override void ViewWillDisappear (bool animated)
		{
			base.ViewWillDisappear (animated);
			DI.DestroyMediator (this);
		}
	}
}
