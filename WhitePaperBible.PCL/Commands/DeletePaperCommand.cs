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
	public class DeletePaperCommand : Command
	{
		[Inject]
		public AppModel AM;

		[Inject]
		public IDeletePaperService Service;

		[Inject]
		public RefreshPapersInvoker RefreshPapers;

		Paper paper;

		public override void Execute (InvokerArgs args)
		{
			paper = ((DeletePaperInvokerArgs)args).Paper;
			if (paper.user_id == AM.User.ID) {
				Service.Success += onSuccess;
				Service.Execute (paper);
			}
		}

		void onSuccess (object sender, EventArgs args)
		{
			AM.Papers.Remove (paper);
			RefreshPapers.Invoke ();

		}
	}
}

