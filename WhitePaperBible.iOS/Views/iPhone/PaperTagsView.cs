
using System;
using System.Collections.Generic;
using System.Linq;

using MonoTouch.Foundation;
using MonoTouch.UIKit;
using MonoTouch.Dialog;
using WhitePaperBible.Core.Models;
using MonkeyArms;
using WhitePaperBible.Core.Views;

namespace WhitePaperBible.iOS
{
	public partial class PaperTagsView : DialogViewController, IMediatorTarget, IPaperTagsView
	{
		EntryElement NewTagEl;

		Section TagsSection;

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
				TagsSection.Add (el);
			}

			Root.Add (TagsSection);
		}

		public override void ViewDidAppear (bool animated)
		{
			base.ViewDidAppear (animated);

			NavigationItem.SetRightBarButtonItem (
				new UIBarButtonItem ("Save", UIBarButtonItemStyle.Plain, (sender, args)=> {

					//					var paper = new Paper(){
					//						title = TitleEl.Value,
					//						description = DescriptionEl.Value,
					//						references = GetReferences(),
					//						tags = GetTags()
					//					};
					//
					//					var invokerArgs = new SavePaperInvokerArgs(paper);
					//					Save.Invoke(invokerArgs);
					this.NavigationController.PopViewControllerAnimated(true);
				})
				, true
			);

			NavigationItem.SetLeftBarButtonItem (
				new UIBarButtonItem ("Back", UIBarButtonItemStyle.Plain, (sender, args)=> {
					this.NavigationController.PopViewControllerAnimated(true);
				})
				, true
			);
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
