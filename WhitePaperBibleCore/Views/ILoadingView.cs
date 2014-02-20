using System;
using System.Collections.Generic;
using WhitePaperBible.Core.Models;
using MonkeyArms;

namespace WhitePaperBible.Core.Views
{
	public interface ILoadingView : IMediatorTarget
	{
		void OnLoadingComplete ();
	}
}

