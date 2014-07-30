using System;
using System.Collections.Generic;
using WhitePaperBible.Core.Models;
using MonkeyArms;

namespace WhitePaperBible.Core.Views
{
	public interface IEditProfileView : IMediatorTarget
	{
		Invoker Save { get; }

		void SetUserProfile (AppUser user);

	}
}

