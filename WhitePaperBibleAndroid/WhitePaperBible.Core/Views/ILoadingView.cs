using System;
using System.Collections.Generic;
using WhitePaperBibleCore.Models;
using MonkeyArms;

namespace WhitePaperBible.Core.Views
{
	public interface ILoadingView : IMediatorTarget
	{
		void OnLoadingComplete ();
	}
}

