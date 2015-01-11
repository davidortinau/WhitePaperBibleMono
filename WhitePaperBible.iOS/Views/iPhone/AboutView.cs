using System;
using System.Collections.Generic;
using System.Linq;

using Foundation;
using UIKit;
using MonoTouch.Dialog;
using MessageUI;
using IOS.Util;

namespace WhitePaperBible.iOS
{
	public partial class AboutView : DialogViewController
	{
		public AboutView () : base (UITableViewStyle.Grouped, null)
		{
			this.Title = "About";

			AnalyticsUtil.TrackScreen (this.Title);
//			aboutTableDataArray = [[NSArray alloc] initWithObjects:@"whitepaperbible.org",@"simplyprofound.com",@"Follow WPB on Twitter",@"Join WPB on Facebook",@"Preface to the ESV® Bible",nil];
//			legalTableDataArray = [[NSArray alloc] initWithObjects:@"Copyrights",@"Terms and Conditions",nil];

			Root = new RootElement ("About") {
				new Section ("Support"){
					new StringElement ("Get Help", () => {
						var _mailController = new MFMailComposeViewController();
						_mailController.SetToRecipients(new []{"dave@whitepaperbible.org"});
						_mailController.SetSubject("Help with WhitePaperBible");
						_mailController.SetMessageBody("What can I help you with?", false);

						_mailController.Finished += ( object s, MFComposeResultEventArgs args) => {
							System.Console.WriteLine (args.Result.ToString ());
							args.Controller.DismissViewController (true, null);
						};

						PresentViewController (_mailController, true, null);
			}), 
					new StringElement ("Send Feedback", () => {
						var _mailController = new MFMailComposeViewController();
						_mailController.SetToRecipients(new []{"dave@whitepaperbible.org"});
						_mailController.SetSubject("Feedback and Ideas");
						_mailController.SetMessageBody("What's on your mind?", false);

						_mailController.Finished += ( object s, MFComposeResultEventArgs args) => {
							System.Console.WriteLine (args.Result.ToString ());
							args.Controller.DismissViewController (true, null);
						};

						PresentViewController (_mailController, true, null);
					})},

				new Section ("App Info"){
					new StringElement (String.Format("Version: {0}", NSBundle.MainBundle.InfoDictionary["CFBundleShortVersionString"])),
					new StringElement("Visit whitepaperbible.org", ()=>{
						UIApplication.SharedApplication.OpenUrl(NSUrl.FromString("http://www.whitepaperbible.org"));
					}),
					new StringElement("Preface to the ESV Bible", ()=>{
						UIApplication.SharedApplication.OpenUrl(NSUrl.FromString("http://www.gnpcb.org/esv/preface/"));
					}),
					new StringElement("Licenses", ()=>{
						// show legal stuff
						var copyrightsView = new CopyrightsView();
						this.NavigationController.PushViewController(copyrightsView, true);
					}),
					new StringElement("Terms and Conditions", ()=>{
						var termsView = new TermsAndConditionsView();
						this.NavigationController.PushViewController(termsView, true);
					})
				},
			};
		}
	}
}
