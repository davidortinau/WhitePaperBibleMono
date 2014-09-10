using System;
using MonkeyArms;
using WhitePaperBible.Core.Models;
using WhitePaperBible.Core.Invokers;
using WhitePaperBible.Core.Views;
using System.Linq;
using WhitePaperBible.Core.Services;

namespace WhitePaperBible.Core.Mediators
{
	public class EditPaperViewMediator : Mediator
	{
		[Inject]
		public AppModel AppModel;

		[Inject]
		public SavePaperInvoker SavePaper;

		[Inject]
		public PaperSavedInvoker PaperSaved;

		[Inject]
		public DeletePaperInvoker DeletePaper;

		IEditPaperView Target;

		public EditPaperViewMediator (IEditPaperView view) : base (view)
		{
			this.Target = view;
		}

		void OnPaperSaved (object sender, EventArgs e)
		{
			Target.DismissController (false);
		}

		public override void Register ()
		{
//			AppModel.CurrentPaper = Target.Paper;
//			InvokerMap.Add (PaperDetailsReceived, (object sender, EventArgs e) => SetDetails ());

			InvokerMap.Add (Target.Save, OnSave);
			InvokerMap.Add (Target.Delete, OnDelete);
			InvokerMap.Add (PaperSaved, OnPaperSaved);

			if(AppModel.IsLoggedIn){
				SetPaper ();
			}else{
				Target.PromptForLogin ();
			}

		}

		private void SetPaper()
		{
			Target.SetPaper (AppModel.CurrentPaper);
		}

		void OnSave (object sender, EventArgs e)
		{
			SavePaper.Invoke ((SavePaperInvokerArgs)e);
		}

		void OnDelete (object sender, EventArgs e)
		{
			var args = new DeletePaperInvokerArgs (AppModel.CurrentPaper);
			DeletePaper.Invoke (args);
			Target.DismissController (true);
		}
	}
}