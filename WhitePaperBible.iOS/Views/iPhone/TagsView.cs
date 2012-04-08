using System;
using System.Collections.Generic;
using System.Linq;

using MonoTouch.Foundation;
using MonoTouch.UIKit;
using MonoTouch.Dialog;

namespace WhitePaperBible.iOS
{
	public partial class TagsView : DialogViewController
	{
		public TagsView () : base (UITableViewStyle.Grouped, null)
		{
			Root = new RootElement ("Tags") {
				new Section ("First Section"){
					new StringElement ("Hello", () => {
				new UIAlertView ("Hola", "Thanks for tapping!", null, "Continue").Show (); 
			}),
					new EntryElement ("Name", "Enter your name", String.Empty)
				},
				new Section ("Second Section"){
				},
			};
		}
		
	}
}
