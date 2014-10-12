using System;
using System.Net;
using WhitePaperBible.Core.Models;

namespace WhitePaperBible.Core.Services
{
	public interface IJSONWebClient
	{
		event EventHandler RequestComplete, RequestError;

		string ResponseText{ get; }

		SessionCookie UserSessionCookie {get; }

		//http://tnt.freckleinteractive.com/public/api.php?method=login&username=Ben&password=asdf
		void OpenURL (string url, MethodEnum method=MethodEnum.GET, bool inlcudeSessionCookie=false);
	}
}

