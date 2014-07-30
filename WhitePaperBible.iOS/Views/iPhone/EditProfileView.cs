
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

namespace WhitePaperBible.iOS
{
	public partial class EditProfileView : DialogViewController, IMediatorTarget, IEditProfileView
	{
		public Invoker Save {
			get;
			private set;
		}

		public EditProfileView () : base (UITableViewStyle.Grouped, null)
		{

		}

		public void SetUserProfile(AppUser user){
			Root = new RootElement ("Edit Profile") {
				new Section ("Profile") {
					new EntryElement ("Name", "Enter your name", user.Name),
					new EntryElement ("Website", "http://yoursite.com", user.Website),
					new SimpleMultilineEntryElement ("Bio", user.Bio),
					new EntryElement ("Email", "you@somewhere.com", user.Email),
					new EntryElement ("Username", "Enter your username", user.username),
					new EntryElement ("Password", "Super Secret", user.password, true)
				},
			};
		}

		public override void ViewDidAppear (bool animated)
		{
			base.ViewDidAppear (animated);

			DI.RequestMediator (this);
		}

		public override void ViewDidDisappear (bool animated)
		{
			base.ViewDidDisappear (animated);

			DI.DestroyMediator (this);
		}
	}
}
