using System;
using System.Runtime.Serialization;

namespace WhitePaperBible.Core.Models
{
	public class Reference
	{
		public bool delete {
			get;
			set;
		}

		public int id { get; set; }

		public string reference { get; set; }

		public string content { get; set; }

		public string audio_tag { get; set; }

		public override bool Equals (object obj)
		{
			Reference ref2 = (Reference)obj;

			return ref2.reference == reference && ref2.content == content && ref2.audio_tag == audio_tag;
		}
	}
}
