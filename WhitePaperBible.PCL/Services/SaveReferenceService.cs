using System;
using WhitePaperBible.Core.Models;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Net;
using MonkeyArms;

namespace WhitePaperBible.Core.Services
{
	public interface ISaveRefereceService:IBaseService
	{
		void Execute (Paper paper, Reference reference);
	}

	public class SaveReferenceService:BaseService, ISaveReferenceService, IInjectingTarget
	{
		[Inject]
		public AppModel AM;

		public void Execute (Paper paper, Reference reference)
		{
			var cookieJar = new CookieContainer ();
			cookieJar.Add (new Uri (Constants.BASE_URI), new Cookie (AM.UserSessionCookie.Name, AM.UserSessionCookie.Value));

			var url = Constants.BASE_URI;
			url += String.Format("papers/{0}?references?reference[reference]={1}&reference[paper_id]={2}&user_id={3}", paper.id, reference.reference, paper.id, AM.User.ID);
			url += "&caller=wpb-iPhone";
			Client.OpenURL (url, MethodEnum.POST, cookieJar);

//			NSMutableString *completeSearchURL= [[[NSMutableString alloc] initWithString:serverURLString] autorelease];
//			[completeSearchURL appendString: @"/papers/"];
//			[completeSearchURL appendFormat:@"%@",newPaperID];
//			[completeSearchURL appendString: @"/references?reference[reference]="];
//			[completeSearchURL appendString: [currentReferenceString stringByAddingPercentEscapesUsingEncoding:NSASCIIStringEncoding]];
//			[completeSearchURL appendString: @"&reference[paper_id]="];
//			[completeSearchURL appendFormat:@"%@",newPaperID];
//			[completeSearchURL appendString: @"#"];
//			NSLog(@"complete url: %@", completeSearchURL);
//
//			//http://localhost:3000/search/esv_verse_search.json?keyword=Phi%203
//
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
			DispatchSuccess (null);
		}

		#endregion
	}
}

