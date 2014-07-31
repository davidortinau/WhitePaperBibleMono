using System;
using MonkeyArms;
using WhitePaperBible.Core.Models;
using WhitePaperBible.Core.Invokers;
using WhitePaperBible.Core.Views;
using System.Linq;
using WhitePaperBible.Core.Services;

namespace WhitePaperBible.Core.Mediators
{
	public class EditProfileViewMediator : Mediator
	{
		[Inject]
		public AppModel AM;

		[Inject]
		public SaveUserInvoker SaveUser;

		IEditProfileView Target;

		public EditProfileViewMediator (IEditProfileView view) : base (view)
		{
			this.Target = view;
		}

		public override void Register ()
		{
			InvokerMap.Add (Target.Save, OnSave);

			Target.SetUserProfile (AM.User);
		}

		void OnSave (object sender, EventArgs e)
		{
			// validation?

			// do what with user/pass?

			var args = (SaveUserInvokerArgs)e;
			if(isValid(args.User)){
				SaveUser.Invoke (args);
			}
		}

		bool isValid (AppUser user)
		{
			return true;
//			NSString *usernameCharsRegex = @"^[a-zA-Z0-9_\\w(@)(.)( )(\\-)(_)]{4,100}$";//@"[a-zA-Z0-9]";//@"\\w*\\s*@*-*.*";// @"\\w*\\s*[@]*[-]*[.]*";
//
//			NSString *emailRegEx =
//				@"(?:[a-z0-9!#$%\\&'*+/=?\\^_`{|}~-]+(?:\\.[a-z0-9!#$%\\&'*+/=?\\^_`{|}"
//	@"~-]+)*|\"(?:[\\x01-\\x08\\x0b\\x0c\\x0e-\\x1f\\x21\\x23-\\x5b\\x5d-\\"
//				@"x7f]|\\\\[\\x01-\\x09\\x0b\\x0c\\x0e-\\x7f])*\")@(?:(?:[a-z0-9](?:[a-"
//					@"z0-9-]*[a-z0-9])?\\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?|\\[(?:(?:25[0-5"
//	@"]|2[0-4][0-9]|[01]?[0-9][0-9]?)\\.){3}(?:25[0-5]|2[0-4][0-9]|[01]?[0-"
//	@"9][0-9]?|[a-z0-9-]*[a-z0-9]:(?:[\\x01-\\x08\\x0b\\x0c\\x0e-\\x1f\\x21"
//	@"-\\x5a\\x53-\\x7f]|\\\\[\\x01-\\x09\\x0b\\x0c\\x0e-\\x7f])+)\\])";
//					//thanks http://cocoawithlove.com/2009/06/verifying-that-string-is-email-address.html
//
//
//					NSPredicate *regExPredicate =[NSPredicate predicateWithFormat:@"SELF MATCHES %@", emailRegEx];
//					BOOL looksLikeAnEmail = [regExPredicate evaluateWithObject:theEmail];
//
//					NSPredicate *regExPredicate2 =[NSPredicate predicateWithFormat:@"SELF MATCHES %@", usernameCharsRegex];
//					BOOL looksLikeAnGoodUsername = [regExPredicate2 evaluateWithObject:theUsername];
//
//
//					//NSLog(@"check with: u:%@ p:%@ pc:%@ em:%@ %d %d",theUsername, thePassword, theConfirmation, theEmail, looksLikeAnEmail, looksLikeAnGoodUsername);
//
//					NSMutableString *errorString = [[NSMutableString alloc] initWithCapacity:0];
//					if([theUsername length] <= 2)
//					[errorString appendString:@"Username must be longer than 2 characters\n"];
//					if(!looksLikeAnGoodUsername){ //regex
//						[errorString appendString:@"Username should use only letters, numbers, spaces, and .-_@.\n"];
//					}
//					if([thePassword length] > 0){
//						if([thePassword length] <= 3)
//							[errorString appendString:@"Password must be a longer than 3 characters.\n"];
//
//						if(![thePassword isEqualToString:theConfirmation]){	
//							[errorString appendString:@"Password doesn't match confirmation.\n"];
//
//						}	
//					}
//					if([theEmail length] <= 5)
//					[errorString appendString:@"Email must be longer than 5 characters.\n"];
//					if(!looksLikeAnEmail){ //regex	
//						[errorString appendString:@"Email should look like an email address.\n"];
//					}
//					if([errorString length] > 0){
//						[errorString appendString:@"\nPlease fix these errors and try again."];
//
//						//NSString *message = [NSString stringWithString:[userDictionary valueForKey:@"userBio"]];
//						UIAlertView *alert = [[UIAlertView alloc] initWithTitle:@"Error Creating Account" message:errorString
//							delegate:self cancelButtonTitle:@"OK" otherButtonTitles: nil];
//						[alert show];	
//						[alert release];
//					}
//
		}
	}
}