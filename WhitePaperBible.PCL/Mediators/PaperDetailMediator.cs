using System;
using MonkeyArms;
using WhitePaperBible.Core.Models;
using WhitePaperBible.Core.Invokers;
using WhitePaperBible.Core.Views;
using System.Linq;
using WhitePaperBible.Core.Services;

namespace WhitePaperBible.Core.Mediators
{
	public class PaperDetailMediator : Mediator
	{
		[Inject]
		public AppModel AppModel;

		[Inject]
		public PaperDetailsReceivedInvoker PaperDetailsReceived;

		[Inject]
		public GetPaperDetailsInvoker GetPaperDetails;

		[Inject]
		public GetPaperReferencesService PaperReferencesService;

		IPaperDetailView Target;

		public PaperDetailMediator (IPaperDetailView view) : base (view)
		{
			this.Target = view;
		}

		public override void Register ()
		{
		
			InvokerMap.Add (PaperDetailsReceived, (object sender, EventArgs e) => SetPaper ());

			GetPaperDetails.Invoke ();
		}

		public void SetPaper ()
		{
			if (AppModel.CurrentPaper != null) {
				Target.SetPaper (AppModel.CurrentPaper);
			}
		}

		private void GetDetails()
		{
			PaperReferencesService.Success += (object sender, EventArgs e) => {
				GetPaperReferencesServiceEventArgs args = e as GetPaperReferencesServiceEventArgs;
				string html = @"<style type='text/css'>body { color: #000000; background-color: #FFFFFF; font-family: 'HelveticaNeue-Light', Helvetica, Arial, sans-serif; padding-bottom: 50px; } h1, h2, h3, h4, h5, h6 { padding: 0px; margin: 0px; font-style: normal; font-weight: normal; } h2 { font-family: 'HelveticaNeue', Helvetica, Arial, sans-serif; font-size: 18px; font-weight: bold; margin-bottom: -10px; padding-bottom: 0px; } h4 { font-size: 16px; } p { font-family: Helvetica, Verdana, Arial, sans-serif; line-height:1.5; font-size: 16px; } .esv-text { padding: 0 0 10px 0; } .description { border-radius: 5px; background-color:#F1F1F1; margin: 10px; padding: 8px; }</style>";
				//			html += "<a href='#back'><img src='Images/btn_back.png' alt='back'/></a>";
				html += "<h1>" + AppModel.CurrentPaper.title + "</h1>";
				html += "<section class=\"description\">" + AppModel.CurrentPaper.description + "</section>";

				foreach (ReferenceNode node in args.References) {
					string content = node.reference.content;
					html += content;
				}

				Target.SetReferences(html);
			};
			PaperReferencesService.Execute (AppModel.CurrentPaper.id);


		}
	}
}