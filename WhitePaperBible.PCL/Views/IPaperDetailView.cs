using System;
using System.Collections.Generic;
using WhitePaperBible.Core.Models;
using MonkeyArms;

namespace WhitePaperBible.Core.Views
{
	public interface IPaperDetailView : IMediatorTarget
	{
		void SetPaper (Paper paper, bool isFavorite);
		void SetReferences (string html);
		Paper Paper{ get; set;}
		Invoker ToggleFavorite{get;}
	}
}

