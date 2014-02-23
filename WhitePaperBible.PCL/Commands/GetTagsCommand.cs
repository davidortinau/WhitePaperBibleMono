using System;
using MonkeyArms;
using WhitePaperBible.Core.Invokers;
using WhitePaperBible.Core.Models;
using WhitePaperBible.Core.Services;
using System.Collections.Generic;

namespace WhitePaperBible.Core.Commands
{
	public class GetTagsCommand : Command
	{
		[Inject]
		public AppModel AM;

		[Inject]
		public IGetTagsService Service;

		[Inject]
		public TagsReceivedInvoker TagsReceived;

		public override void Execute (InvokerArgs args)
		{
			Service.Success += onSuccess;
			Service.Execute ();
		}

		void onSuccess (object sender, EventArgs args)
		{
			AM.Tags = new List<Tag> ();
			foreach (var node in ((GetTagsServiceEventArgs)args).Tags) {
				AM.Tags.Add (node.tag);
			}

			TagsReceived.Invoke ();
		}
	}
}

