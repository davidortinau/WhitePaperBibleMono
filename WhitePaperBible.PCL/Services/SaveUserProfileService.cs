using System;
using WhitePaperBible.Core.Models;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Net;
using MonkeyArms;

namespace WhitePaperBible.Core.Services
{
	public interface ISaveUserProfileService:IBaseService
	{
		void Execute (AppUser user);
	}

	public class SaveUserProfileService:BaseService, ISaveUserProfileService, IInjectingTarget
	{
		[Inject]
		public AppModel AM;

		public void Execute (AppUser user)
		{
			var cookieJar = new CookieContainer ();
			cookieJar.Add (new Uri (Constants.BASE_URI), new Cookie (AM.UserSessionCookie.Name, AM.UserSessionCookie.Value));

			var url = Constants.BASE_URI + String.Format ("users/update/{0}?caller=wpb-iPhone", AM.User.username);
			url += String.Format ("&user[name]={0}&user[website]={1}&user[bio]={2}&user[email]={3}&user[username]={4}", user.Name, user.Website, user.Bio, user.Email, user.username);
			if(user.password.Length > 0){
				url += String.Format ("&user[password]={0}&user[password_confirmation]={1}", user.password, user.passwordConfirmation);
			}
			url += String.Format ("&user_id={0}#", AM.User.ID);

			Client.OpenURL (url, MethodEnum.GET, cookieJar);

//			NSMutableString *completeSearchURL= [[NSMutableString alloc] initWithString:serverURLString];
//			[completeSearchURL appendFormat: @"/users/update/%@?caller=wpb-iPhone",[appDelegateAS getUsername]];
//			[completeSearchURL appendString: @"&user[name]="];
//			[completeSearchURL appendString: [theName stringByAddingPercentEscapesUsingEncoding:NSASCIIStringEncoding]];
//			[completeSearchURL appendString: @"&user[website]="];
//			[completeSearchURL appendString: [theWebsite stringByAddingPercentEscapesUsingEncoding:NSASCIIStringEncoding]];
//			[completeSearchURL appendString: @"&user[bio]="];
//			[completeSearchURL appendString: [theBio stringByAddingPercentEscapesUsingEncoding:NSASCIIStringEncoding]];
//			[completeSearchURL appendString: @"&user[email]="];
//			[completeSearchURL appendString: [self urlencode:theEmail]];
//			[completeSearchURL appendString: @"&user[username]="];
//			[completeSearchURL appendString: [theUsername stringByAddingPercentEscapesUsingEncoding:NSASCIIStringEncoding]];
//			if([thePassword length] > 0){
//				[completeSearchURL appendString: @"&user[password]="];
//				[completeSearchURL appendString: [thePassword stringByAddingPercentEscapesUsingEncoding:NSASCIIStringEncoding]];
//				[completeSearchURL appendString: @"&user[password_confirmation]="];
//				[completeSearchURL appendString: [theConfirmation stringByAddingPercentEscapesUsingEncoding:NSASCIIStringEncoding]];
//			}
//			[completeSearchURL appendString: @"&user_id="];
//			[completeSearchURL appendFormat:@"%@",[appDelegateAS getUserID]];
//
//
//			[completeSearchURL appendString: @"#"];
//			NSLog(@"complete url: %@", completeSearchURL);
//
//			NSMutableURLRequest *urlRequest = [NSMutableURLRequest requestWithURL:[NSURL URLWithString:completeSearchURL]
//				cachePolicy:NSURLCacheStorageNotAllowed
//				timeoutInterval:30];
//			[urlRequest setHTTPMethod:@"PUT"];
//			[urlRequest setValue:@"(WhitePaperBible-iPhoneApp/1.0 (iPhone)" forHTTPHeaderField:@"User-Agent"];
//			[urlRequest setValue:@"application/json" forHTTPHeaderField:@"Accept"];
		}

		#region implemented abstract members of BaseService

		protected override void HandleSuccess (object sender, EventArgs args)
		{
//			var user = ParseResponse<UserDTO> ();
			DispatchSuccess (null);
		}

		#endregion
	}

//	public class GetUserProfileEventArgs:EventArgs
//	{
//		public readonly AppUser User;
//
//		public GetUserProfileEventArgs (AppUser user)
//		{
//			User = user;
//		}
//	}
}

