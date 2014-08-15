using System;
using System.Collections.Generic;
using WhitePaperBible.Core.Models;
using MonkeyArms;

namespace WhitePaperBible.Core.Views
{
	public interface IPaperTagsView : IMediatorTarget
	{
		void SetTags (List<Tag> tags, List<Tag> paperTags);

		Invoker Save {get;}
	}
}

