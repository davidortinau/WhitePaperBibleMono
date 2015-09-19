using System;
using MonkeyArms;
using WhitePaperBible.Core.Models;
using WhitePaperBible.Core.Invokers;
using WhitePaperBible.Core.Views;
using System.Linq;
using WhitePaperBible.Core.Services;

namespace WhitePaperBible.Core.Mediators
{
	public class RegistrationViewMediator : Mediator
	{
		[Inject]
		public AppModel AM;

		[Inject]
		public RegisterUserInvoker RegisterUser;

		[Inject]
		public UserRegisteredInvoker UserRegistered;

		[Inject]
		public RegistrationFaultInvoker FaultInvoker;


		IRegistrationView Target;

		public RegistrationViewMediator (IRegistrationView view) : base (view)
		{
			this.Target = view;
		}

		public override void Register ()
		{
			InvokerMap.Add (Target.Register, OnRegistration);
			InvokerMap.Add (UserRegistered, OnRegistered);
			InvokerMap.Add (FaultInvoker, OnFault);
		}

		void OnFault (object sender, EventArgs e)
		{
			var args = (FaultInvokerArgs)e;
			Target.DisplayError (string.Join(", ",args.Messages));
		}

		void OnRegistration (object sender, EventArgs e)
		{
			var args = (RegisterUserInvokerArgs)e;
			RegisterUser.Invoke (args);
		}

		void OnRegistered (object sender, EventArgs e)
		{
			Target.DismissView ();
		}
	}
}