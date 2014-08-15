
using System;
using System.Collections.Generic;
using System.Linq;

using MonoTouch.Foundation;
using MonoTouch.UIKit;
using MonoTouch.Dialog;
using MonkeyArms;
using WhitePaperBible.Core.Views;
using WhitePaperBible.Core.Models;
using WhitePaperBible.iOS.Invokers;
using WhitePaperBible.Core.Invokers;
using ElementPack;

namespace WhitePaperBible.iOS
{
	public partial class EditPaperView : DialogViewController, IEditPaperView, IMediatorTarget
	{
		EntryElement TitleEl;

		SimpleMultilineEntryElement DescriptionEl;

		public Invoker Save {
			get;
			private set;
		}

		public Invoker Delete {
			get;
			private set;
		}

		public EditPaperView () : base (UITableViewStyle.Grouped, null)
		{
			Save = new Invoker ();
			Delete = new Invoker ();
		}

		public List<Tag> Tags {
			get;
			set;
		}

		List<Reference> GetReferences ()
		{
			var refs = new List<Reference> ();
			foreach(var el in VerseEls){
				refs.Add (new Reference () {
					reference = el.Value
				});
			}

			return refs;
		}

		List<Tag> GetTags ()
		{
			return new List<Tag> ();// need to populate
		}

		#region IEditPaperView implementation

		LoginRequiredView LoginRequiredView;

		Section VersesSection;
		List<EntryElement> VerseEls;

		StyledStringElement TagsEl;

		public void SetPaper (Paper paper)
		{
			VerseEls = new List<EntryElement> ();
			VersesSection = new Section ("Verses");
			VersesSection.Add (new StringElement("Add Verse",()=>{
				var entryElement = new EntryElement ("", "Verse", "");
				VerseEls.Add(entryElement);
				VersesSection.Insert (1, UITableViewRowAnimation.Left, entryElement);
				// or push to a new view where we can see the verse, the content, and delete it
			}));

			if (paper.references != null) {
				foreach (var r in paper.references) {
					var entryElement = new EntryElement ("", "Verse", r.reference);
					VersesSection.Add (entryElement);
					VerseEls.Add(entryElement);
				}
			}

			Root = new RootElement ("Edit Paper") {
				new Section ("") {
					(TitleEl = new EntryElement ("", "Title", paper.title)),
					(DescriptionEl = new SimpleMultilineEntryElement ("", "Description", paper.description)),
					(TagsEl = new StyledStringElement ("Tags","") { 
						Accessory = UITableViewCellAccessory.DisclosureIndicator 
					})
				},
				VersesSection,
				new Section(""){
					new StringElement("Delete", ()=>{
						Delete.Invoke();
					})
				}
			};

			TagsEl.Tapped += () => {
				var tagsView = new PaperTagsView ();
				tagsView.Controller = this;
				NavigationController.PushViewController (tagsView, true);
			};

			if(paper.tags != null && paper.tags.Count > 0){
				Tags = paper.tags;
			}

			// tags element should probably be a StringElement with disclosure, go to another view listing tags to checkbox and an add entry field
		}

		public void PromptForLogin ()
		{
			if (LoginRequiredView == null) {
				CreateLoginRequiredView ();
				LoginRequiredView.Hidden = false;
			}
		}

		public void ShowLoginForm ()
		{
			var loginView = new LoginView ();
			loginView.LoginFinished.Invoked += (object sender, EventArgs e) => {
				(e as LoginFinishedInvokerArgs).Controller.DismissViewController (true, null);
			};

			this.PresentViewController (loginView, true, null);
		}

		public void DismissLoginPrompt()
		{
			if (LoginRequiredView != null && !LoginRequiredView.Hidden) {
				LoginRequiredView.Hidden = true;
			}
		}

		protected void CreateLoginRequiredView ()
		{
			LoginRequiredView = new LoginRequiredView (WhitePaperBible.iOS.UI.Environment.DeviceScreenHeight);
			View.AddSubview (LoginRequiredView);
			View.BringSubviewToFront (LoginRequiredView);
			LoginRequiredView.LoginRegister.Invoked += (object sender, EventArgs e) => ShowLoginForm ();
			LoginRequiredView.Hidden = true;
		}

		public void DismissController ()
		{
//			this.NavigationController.DismissViewController (true, null);
			this.DismissViewController (true, null);
		}

		#endregion

		public override void ViewDidAppear (bool animated)
		{
			base.ViewDidAppear (animated);

			NavigationItem.SetRightBarButtonItem (
				new UIBarButtonItem ("Save", UIBarButtonItemStyle.Plain, (sender, args)=> {
					var paper = new Paper(){
						title = TitleEl.Value,
						description = DescriptionEl.Value,
						references = GetReferences(),
						tags = GetTags()
					};

					var invokerArgs = new SavePaperInvokerArgs(paper);
					Save.Invoke(invokerArgs);
				})
				, true
			);

			NavigationItem.SetLeftBarButtonItem (
				new UIBarButtonItem ("Cancel", UIBarButtonItemStyle.Plain, (sender, args)=> {
					this.DismissViewController(true, null);
				})
				, true
			);
		}

		public override void ViewWillAppear (bool animated)
		{
			base.ViewWillAppear (animated);
			DI.RequestMediator (this);

			if(Tags != null && Tags.Count > 0){
				string[] tags = Tags.Select (x => x.name).ToArray ();
				TagsEl.Value = string.Join (",", tags);
			}
		}

		public override void ViewWillDisappear (bool animated)
		{
			base.ViewWillDisappear (animated);
			DI.DestroyMediator (this);
		}
	}
}
