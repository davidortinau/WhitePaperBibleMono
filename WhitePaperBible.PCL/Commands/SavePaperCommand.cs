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
		public ISaveReferenceService ReferenceService;

		[Inject]
		public RefreshPapersInvoker RefreshPapers;

		[Inject]
		public PaperSavedInvoker Saved;

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

			foreach(var r in a.Paper.references){
				ReferenceService.Success+= (object s, EventArgs e) => {
					var refArg = (WhitePaperBible.Core.Services.SaveReferenceService.ReferenceSavedEventArgs)e;
					a.Paper.UpdateReference(refArg.Reference);
				};
				ReferenceService.Execute (a.Paper, r);
			}

			if (this.paper.id > 0) {
				var pIndex = -1;
				foreach (var paper in AM.Papers) {
					pIndex++;
					if (paper.id == a.Paper.id) {
						AM.Papers.Remove (paper);
						AM.Papers [pIndex] = a.Paper;
						break;
					}

				}
			}else{
				AM.Papers.Add (a.Paper);
				AM.Papers.Sort ();// make sure we sort alpha
			}



			RefreshPapers.Invoke ();
			Saved.Invoke ();
		}
	}
}

