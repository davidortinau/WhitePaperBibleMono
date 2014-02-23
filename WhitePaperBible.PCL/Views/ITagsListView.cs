using System;
using System.Collections.Generic;
using WhitePaperBible.Core.Models;
using MonkeyArms;

namespace WhitePaperBible.Core.Views
{
	public interface ITagsListView : IMediatorTarget
	{
		Invoker Filter{ get; }

		Invoker OnTagSelected{ get; }

		void SetTags (List<Tag> tags);

		string SearchPlaceHolderText{ get; set; }

		string SearchQuery{ get; }

		Tag SelectedTag{ get; set; }
	}
}

