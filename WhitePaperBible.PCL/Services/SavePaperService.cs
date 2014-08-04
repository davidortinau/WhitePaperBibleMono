using System;
using WhitePaperBible.Core.Models;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Net;
using MonkeyArms;

namespace WhitePaperBible.Core.Services
{
	public interface ISavePaperService:IBaseService
	{
		void Execute (Paper paper);
	}

	public class SavePaperService:BaseService, ISavePaperService, IInjectingTarget
	{
		[Inject]
		public AppModel AM;

		public void Execute (Paper paper)
		{
			var cookieJar = new CookieContainer ();
			cookieJar.Add (new Uri (Constants.BASE_URI), new Cookie (AM.UserSessionCookie.Name, AM.UserSessionCookie.Value));

			var url = Constants.BASE_URI;
			url += String.Format("papers/?paper[title]={0}&paper[description]={1}&user_id={2}", paper.title, paper.description, AM.User.ID);
			url += "&caller=wpb-iPhone";
			Client.OpenURL (url, MethodEnum.POST, cookieJar);

			// and what about references?


//			NSMutableString *completeSearchURL= [[[NSMutableString alloc] initWithString:serverURLString] autorelease];
//			[completeSearchURL appendString: @"/papers?caller=wpb-iPhone&paper[title]="];
//			[completeSearchURL appendString: [realTitleString stringByAddingPercentEscapesUsingEncoding:NSASCIIStringEncoding]];
//			[completeSearchURL appendString: @"&paper[description]="];
//			[completeSearchURL appendString: [realDescriptionString stringByAddingPercentEscapesUsingEncoding:NSASCIIStringEncoding]];
//			[completeSearchURL appendString: @"&user_id="];
//			[completeSearchURL appendFormat:@"%@",[appDelegate getUserID]];
//			[completeSearchURL appendString: @"#"];
//			NSLog(@"complete url: %@", completeSearchURL);
//
//			NSMutableURLRequest *urlRequest = [NSMutableURLRequest requestWithURL:[NSURL URLWithString:completeSearchURL]
//				cachePolicy:NSURLCacheStorageNotAllowed
//				timeoutInterval:30];
//			[urlRequest setHTTPMethod:@"POST"];


		}

		#region implemented abstract members of BaseService

		protected override void HandleSuccess (object sender, EventArgs args)
		{
//			var user = ParseResponse<UserDTO> ();
//			DispatchSuccess (null);

			DispatchSuccess (new PaperSavedEventArgs (ParseResponse<Paper> ()));
		}

		#endregion

		public class PaperSavedEventArgs:EventArgs
		{
			public readonly Paper Paper;

			public PaperSavedEventArgs (Paper paper)
			{
				Paper = paper;
			}
		}
	}
}

