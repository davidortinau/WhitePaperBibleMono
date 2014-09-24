using System;
using WhitePaperBible.Core.Models;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Net;
using MonkeyArms;

namespace WhitePaperBible.Core.Services
{
	public interface IRegisterUserService:IBaseService
	{
		void Execute (AppUser user);
	}

	public class RegisterUserService:BaseService, IRegisterUserService, IInjectingTarget
	{
		[Inject]
		public AppModel AM;

		public void Execute (AppUser user)
		{
			var cookieJar = new CookieContainer ();
			cookieJar.Add (new Uri (Constants.BASE_URI), new Cookie (AM.UserSessionCookie.Name, AM.UserSessionCookie.Value));

			var url = Constants.BASE_URI + String.Format ("users/create/?caller=wpb-iPhone");
			url += String.Format ("&user[name]={0}&user[email]={1}&user[username]={2}&eula[accepted]=YES", user.Name, user.Email, user.username);
			url += String.Format ("&user[password]={0}&user[password_confirmation]={1}", user.password, user.passwordConfirmation);

			Client.OpenURL (url, MethodEnum.GET, cookieJar);

//			[completeSearchURL appendFormat: @"/users/create/"];
//			[completeSearchURL appendString: [self urlencode:theName]];
//			[completeSearchURL appendString: @"?user[name]="];
//			[completeSearchURL appendString: [self urlencode:theName]];
//			[completeSearchURL appendString: @"&user[email]="];
//			[completeSearchURL appendString: [self urlencode:theEmail]];
//			[completeSearchURL appendString: @"&user[username]="];
//			[completeSearchURL appendString: [self urlencode:theUsername]];
//			[completeSearchURL appendString: @"&user[password]="];
//			[completeSearchURL appendString: [self urlencode:thePassword]];
//			[completeSearchURL appendString: @"&caller=wpb-iPhone"];
//			[completeSearchURL appendString: @"&eula[accepted]="];
//			[completeSearchURL appendString: [self urlencode:@"YES"]];
//			[completeSearchURL appendString: @"&user[password_confirmation]="];
//			[completeSearchURL appendString: [self urlencode:theConfirmation]];
//			[completeSearchURL appendString: @"#"];
//
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

