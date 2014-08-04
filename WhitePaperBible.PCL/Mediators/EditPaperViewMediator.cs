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

		IEditPaperView Target;

		public EditPaperViewMediator (IEditPaperView view) : base (view)
		{
			this.Target = view;
		}

		public override void Register ()
		{
//			AppModel.CurrentPaper = Target.Paper;
//			InvokerMap.Add (PaperDetailsReceived, (object sender, EventArgs e) => SetDetails ());

			InvokerMap.Add (Target.Save, OnSave);

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
	}
}