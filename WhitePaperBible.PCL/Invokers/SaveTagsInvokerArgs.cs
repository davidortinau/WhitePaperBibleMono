using System;
using MonkeyArms;
using WhitePaperBible.Core.Models;
using System.Collections.Generic;

namespace WhitePaperBible.Core.Invokers
{
	public class SaveTagsInvokerArgs: InvokerArgs
	{
		public List<Tag> Tags;

		public SaveTagsInvokerArgs(List<Tag> tags)
		{
			this.Tags = tags;
		}
	}
}

