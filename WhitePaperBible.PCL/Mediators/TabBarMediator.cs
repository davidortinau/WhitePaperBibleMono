using System;
using MonkeyArms;
using WhitePaperBible.Core.Models;
using WhitePaperBible.Core.Invokers;
using WhitePaperBible.Core.Views;
using System.Linq;

namespace WhitePaperBible.Core.Mediators
{
	public class TabBarMediator : Mediator
	{
		[Inject]
		public AppModel AppModel;

		[Inject]
		public UnreachableInvoker Unreachable;

		[Inject]
		public LoginRequiredInvoker LoginRequired;

		[Inject]
		public LoggedInInvoker LoggedIn;

		ITabBarView Target;

		public TabBarMediator (ITabBarView view) : base (view)
		{
			this.Target = view;
		}

		public override void Register ()
		{
			InvokerMap.Add (Unreachable, OnUnreachable);
		}

		void OnUnreachable (object sender, EventArgs e)
		{
			Target.ShowUnreachable ();
		}
	}
}