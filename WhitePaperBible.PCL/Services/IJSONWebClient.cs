using System;

namespace WhitePaperBible.Core.Services
{
	public interface IJSONWebClient
	{
		event EventHandler RequestComplete, RequestError;

		string ResponseText{ get; }
		//http://tnt.freckleinteractive.com/public/api.php?method=login&username=Ben&password=asdf
		void OpenURL (string url);
	}
}

