using System;
using MonoTouch.Dialog;

namespace CustomElements
{
	public class VerseEntryElement : EntryElement
	{
		public VerseEntryElement (string caption, string placeholder, string value) : base (caption, placeholder, value)
		{
		}

		public void Delete()
		{
			Console.WriteLine ("DELETE THIS ONE"); // do I even need to do anything here? Save happens on clicking save anyway.
		}
	}
}

