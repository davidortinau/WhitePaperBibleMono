
using System;
using System.Collections.Generic;
using System.Linq;

using MonoTouch.Foundation;
using MonoTouch.UIKit;
using MonoTouch.Dialog;
using MonkeyArms;
using WhitePaperBible.Core.Views;

namespace WhitePaperBible.iOS
{
	public partial class BibleSearchResults : DialogViewController, IMediatorTarget, IBibleSearchResultsView
	{
		Section ResultsSection;
		public BibleSearchResults () : base (UITableViewStyle.Grouped, null)
		{

		}

		#region IBibleSearchResultsView implementation

		public void SetResults (List<WhitePaperBible.Core.Models.ReferenceNode> references)
		{
			InvokeOnMainThread(()=>{

				ResultsSection = new Section ();

				foreach(var r in references){
					var el = new StyledStringElement (r.reference.reference, r.reference.content, UITableViewCellStyle.Subtitle);
					el.Tapped += delegate {
						new UIAlertView ("Hola", "Thanks for tapping!", null, "Continue").Show (); 
					};
					ResultsSection.Add (el);
				}

				Root = new RootElement ("BibleSearchResults") {
					ResultsSection
				};

			});
		}

		#endregion
	}
}
