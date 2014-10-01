using System;
using MonkeyArms;
using WhitePaperBible.Core.Models;
using WhitePaperBible.Core.Invokers;
using WhitePaperBible.Core.Views;
using System.Linq;

namespace WhitePaperBible.Core.Mediators
{
	public class LoginMediator : Mediator
	{
		[Inject]
		public AppModel AppModel;

		[Inject]
		public LogInInvoker LoginInvoker;

		[Inject]
		public LoggedInInvoker LoggedIn;

		[Inject]
		public LoginFaultInvoker LoginFault;

		ILoginView Target;

		public LoginMediator (ILoginView view) : base (view)
		{
			this.Target = view;
		}

		public override void Register ()
		{
			this.Target.LoginSubmitted += HandleLoginSubmitted;
			InvokerMap.Add (LoggedIn, onLoggedIn);
			InvokerMap.Add (LoginFault, onLoginFault);
			InvokerMap.Add (Target.RegistrationClosed, onRegistrationClosed);
		}

		void onRegistrationClosed (object sender, EventArgs e)
		{
			if(AppModel.IsLoggedIn){
				Target.Dismiss ();
			}
		}

		void HandleLoginSubmitted (object sender, EventArgs e)
		{
			LoginInvoker.Invoke (new LogInInvokerArgs (Target.UserName, Target.Password));
			Target.ShowBusyIndicator ();
		}

		void onLoggedIn (object sender, EventArgs e)
		{
			Target.HideBusyIndicator ();
			Target.Dismiss ();
		}

		void onLoginFault (object sender, EventArgs e)
		{
			Target.HideBusyIndicator ();
		}
	}
}