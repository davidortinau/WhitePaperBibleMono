using MonkeyArms;
using WhitePaperBible.Core.Invokers;
using WhitePaperBible.Core.Models;
using System.Threading.Tasks;
using PCLStorage;
using System.Xml.Serialization;
using System.IO;
using WhitePaperBible.Core.Services;
using System;
using System.Collections.Generic;

namespace WhitePaperBible.Core.Commands
{
	public class SavePaperCommand : Command
	{
		[Inject]
		public AppModel AM;

		[Inject]
		public ISavePaperService Service;

		[Inject]
		public ISaveRefereceService ReferenceService;

//		[Inject]
//		public PaperSavedInvoker Saved;

		Paper paper;

		public override void Execute (InvokerArgs args)
		{
			paper = ((SavePaperInvokerArgs)args).Paper;
			Service.Success += onSuccess;
			Service.Execute (paper);
		}

		void onSuccess (object sender, EventArgs args)
		{
			var a = (WhitePaperBible.Core.Services.SavePaperService.PaperSavedEventArgs)args;
			AM.Papers.Add (a.Paper);
//			AM.User.Update (paper);
//			Saved.Invoke ();
			foreach(var r in paper.references){
				ReferenceService.Execute (a.Paper, r);
			}
		}
	}
}

