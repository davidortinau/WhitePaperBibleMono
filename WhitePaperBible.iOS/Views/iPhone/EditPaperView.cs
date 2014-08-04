
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

namespace WhitePaperBible.iOS
{
	public partial class EditPaperView : DialogViewController, IEditPaperView, IMediatorTarget
	{
		EntryElement TitleEl;

		EntryElement DescriptionEl;

		public Invoker Save {
			get;
			private set;
		}

		public EditPaperView () : base (UITableViewStyle.Grouped, null)
		{
			Save = new Invoker ();

			NavigationItem.SetRightBarButtonItem (
				new UIBarButtonItem ("Save", UIBarButtonItemStyle.Plain, (sender, args)=> {
					var paper = new Paper(){
						title = TitleEl.Value,
						description = DescriptionEl.Value
					};

					var invokerArgs = new SavePaperInvokerArgs(paper);
					Save.Invoke(invokerArgs);
				})
				, true
			);

		}

		#region IEditPaperView implementation

		LoginRequiredView LoginRequiredView;

		Section VersesSection;

		public void SetPaper (Paper paper)
		{
			VersesSection = new Section ("Verses");
			if (paper.references != null) {
				foreach (var r in paper.references) {
					VersesSection.Add (new StyledMultilineElement (r.reference, r.content, UITableViewCellStyle.Subtitle));
				}
			}
			VersesSection.Add (new StringElement ("Add Verse", () =>{
				// add a new verse flow
			}));

			Root = new RootElement ("EditPaperView") {
				new Section ("") {
					(TitleEl = new EntryElement ("Title", "Verses About...", paper.title)),
					(DescriptionEl = new EntryElement ("Description", "Helps me when...", paper.description)),
				},
				VersesSection
			};
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

		#endregion

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
