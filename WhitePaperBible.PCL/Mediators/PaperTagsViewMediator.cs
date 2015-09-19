using System;
using MonkeyArms;
using WhitePaperBible.Core.Models;
using WhitePaperBible.Core.Invokers;
using WhitePaperBible.Core.Views;
using System.Linq;
using WhitePaperBible.Core.Services;

namespace WhitePaperBible.Core.Mediators
{
	public class PaperTagsViewMediator : Mediator
	{
		[Inject]
		public AppModel AppModel;

		[Inject]
		public SavePaperInvoker SavePaper;

		[Inject]
		public PaperSavedInvoker PaperSaved;

		[Inject]
		public DeletePaperInvoker DeletePaper;

		IPaperTagsView Target;

		public PaperTagsViewMediator (IPaperTagsView view) : base (view)
		{
			this.Target = view;
		}

		public override void Register ()
		{
//			AppModel.CurrentPaper = Target.Paper;
//			InvokerMap.Add (PaperDetailsReceived, (object sender, EventArgs e) => SetDetails ());

			InvokerMap.Add (Target.Save, OnSave);

			Target.SetTags (AppModel.Tags, AppModel.CurrentPaper.tags);
		}

		void OnSave (object sender, EventArgs e)
		{
//			SavePaper.Invoke ((SavePaperInvokerArgs)e);
		}


	}
}