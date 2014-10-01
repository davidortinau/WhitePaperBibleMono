
using System;
using System.Collections.Generic;
using System.Linq;

using MonoTouch.Foundation;
using MonoTouch.UIKit;
using MonoTouch.Dialog;
using MonkeyArms;
using WhitePaperBible.Core.Models;
using WhitePaperBible.Core.Views;
using ElementPack;
using WhitePaperBible.Core.Invokers;
using IOS.Util;

namespace WhitePaperBible.iOS
{
	public partial class EditProfileView : DialogViewController, IMediatorTarget, IEditProfileView
	{
		EntryElement nameEl;

		EntryElement websiteEl;

		SimpleMultilineEntryElement bioEl;

		EntryElement emailEl;

		EntryElement usernameEl;

		EntryElement passwordEl;

		EntryElement passwordConfirmEl;

		public Invoker Save {
			get;
			private set;
		}

		public EditProfileView () : base (UITableViewStyle.Grouped, null)
		{
			Save = new Invoker ();

			// add logout in upper left
			NavigationItem.SetRightBarButtonItem (
				new UIBarButtonItem ("Save", UIBarButtonItemStyle.Plain, (sender, args)=> {
					var user = new AppUser(){
						Name = nameEl.Value,
						Website = websiteEl.Value, 
						Bio = bioEl.Value,
						username = usernameEl.Value,
						password = passwordEl.Value,
						passwordConfirmation = passwordConfirmEl.Value,
						Email = emailEl.Value
					};

					var invokerArgs = new SaveUserInvokerArgs(user);
					Save.Invoke(invokerArgs);
				})
				, true
			);
		}

		public void SetUserProfile(AppUser user){
			Root = new RootElement ("Edit Profile") {
				new Section ("Profile") {
					(nameEl = new EntryElement ("Name", "Enter your name", user.Name)),
					(websiteEl = new EntryElement ("Website", "http://yoursite.com", user.Website){KeyboardType=UIKeyboardType.Url}),
					(bioEl = new SimpleMultilineEntryElement ("", "Bio", user.Bio){Editable=true}),
					(emailEl = new EntryElement ("Email", "you@somewhere.com", user.Email){KeyboardType=UIKeyboardType.EmailAddress}),
					(usernameEl = new EntryElement ("Username", "Enter your username", user.username)),
					(passwordEl = new EntryElement ("Password", "Super Secret", user.password, true)),
					(passwordConfirmEl = new EntryElement ("Confirm Password", "Super Secret", user.password, true))
				},
			};
		}

		public override void ViewDidAppear (bool animated)
		{
			base.ViewDidAppear (animated);

			DI.RequestMediator (this);

			AnalyticsUtil.TrackScreen (this.Title);


		}

		public override void ViewDidDisappear (bool animated)
		{
			base.ViewDidDisappear (animated);

			DI.DestroyMediator (this);
		}
	}
}
