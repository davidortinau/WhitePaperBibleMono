using System;
using MonkeyArms;
using WhitePaperBible.Core.Views;
using WhitePaperBible.Core.Invokers;

namespace WhitePaperBible.Core.Mediators
{
	public class LoadingViewMediator : Mediator
	{
		[Inject]
		public PapersReceivedInvoker PapersReceived;
		ILoadingView Target;

		public LoadingViewMediator (ILoadingView view) : base (view)
		{
			this.Target = view;
		}

		public override void Register ()
		{
			PapersReceived.Invoked += (object sender, EventArgs e) => {
				Target.OnLoadingComplete ();
			};
		}

		public override void Unregister ()
		{
			base.Unregister ();
		}
	}
}

