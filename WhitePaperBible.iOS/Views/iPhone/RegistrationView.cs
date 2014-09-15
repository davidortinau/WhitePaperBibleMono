
using System;
using System.Collections.Generic;
using System.Linq;

using MonoTouch.Foundation;
using MonoTouch.UIKit;
using MonoTouch.Dialog;
using MonkeyArms;
using WhitePaperBible.Core.Views;
using WhitePaperBible.Core.Models;
using WhitePaperBible.Core.Invokers;

namespace WhitePaperBible.iOS
{
	public partial class RegistrationView : DialogViewController, IRegistrationView, IMediatorTarget
	{
		EntryElement FullNameEl;

		EntryElement EmailEl;

		EntryElement UsernameEl;

		EntryElement PasswordEl;

		EntryElement PasswordConfirmEl;

		public RegistrationView () : base (UITableViewStyle.Grouped, null)
		{
			Root = new RootElement ("Create Account") {
				new Section ("") {
					(FullNameEl = new EntryElement ("Full Name", "Jane Doe", String.Empty)),
					(EmailEl = new EntryElement ("Email", "jane@doe.com", String.Empty)),
					(UsernameEl = new EntryElement ("Username", "janedoe", String.Empty)),
					(PasswordEl = new EntryElement ("Password", "****", String.Empty, true)),
					(PasswordConfirmEl = new EntryElement ("Password Confirm", "****", String.Empty, true)),
					new StringElement("By registering you agree to the terms and conditions",()=>{
						var termsView = new TermsAndConditionsView();
						this.PresentViewController((UIViewController)termsView,true, null);
					})

				},
				new Section ("") {
					new StyledStringElement("Register",()=>{
						if(IsValid()){
							var user = new AppUser(){
								Name = FullNameEl.Value,
								Email = EmailEl.Value,
								username = UsernameEl.Value,
								password = PasswordEl.Value,
								passwordConfirmation = PasswordConfirmEl.Value
							};
							//TODO move validation to AppUser class and then push message back here for alert
							var args = new RegisterUserInvokerArgs(user);
							Register.Invoke(args);
						}
					}),
					new StyledStringElement("Cancel",()=>{
						this.DismissViewController(true, null);
					})
				},
			};
		}

		bool IsValid ()
		{
			string results = string.Empty;
			if(String.IsNullOrEmpty(FullNameEl.Value)){
				results += "Full Name is required." + Environment.NewLine;
			}

			if(String.IsNullOrEmpty(EmailEl.Value)){
				results += "Email is required." + Environment.NewLine;
			}else{
				//validate email
				if(!RegexUtilities.IsValidEmail(EmailEl.Value)){
					results += "Email format isn't recognized." + Environment.NewLine;
				}
			}

			if(String.IsNullOrEmpty(UsernameEl.Value)){
				results += "Username is required." + Environment.NewLine;
			}else{
				if(UsernameEl.Value.Length < 4){
					results += "Username should be longer than 4 characters." + Environment.NewLine;
				}

				if(UsernameEl.Value.IndexOf(" ") < 4){
					results += "Username should not have empty spaces." + Environment.NewLine;
				}
			}

			if(String.IsNullOrEmpty(PasswordEl.Value)){
				results += "Password is required." + Environment.NewLine;
			}

			if(String.IsNullOrEmpty(PasswordConfirmEl.Value)){
				results += "Password Confirmation is required." + Environment.NewLine;
			}

			if(PasswordEl.Value != PasswordConfirmEl.Value){
				results += "Passwords do not match." + Environment.NewLine;
			}

			if(!String.IsNullOrEmpty(results)){
				var alert = new UIAlertView ("Please Fix", results, null, "Okay");
				alert.Show ();
				return false;
			}else{
				return true;
			}
		}

		#region IRegistrationView implementation

		public void DisplayError (string msg)
		{
			throw new NotImplementedException ();
		}

		public Invoker Register {
			get;
			private set;
		}

		#endregion

		public override void ViewWillAppear (bool animated)
		{
			base.ViewWillAppear (animated);
			DI.RequestMediator (this);
		}

		public override void ViewWillDisappear (bool animated)
		{
			base.ViewWillDisappear (animated);
			DI.DestroyMediator (this);
		}
	}
}
