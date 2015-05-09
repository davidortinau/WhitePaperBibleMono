using System;

namespace WhitePaperBible.Core.Models
{
	public class Paper
	{
		public string permalink { get; set; }

		public string url_title { get; set; }

		public DateTime updated_at { get; set; }

		public string title { get; set; }
		//public bool public { get; set; }

		public bool? featured { get; set; }

		public int id { get; set; }

		public string description { get; set; }

		public int user_id { get; set; }

//		public AppUser Author { get; set; }

		public int view_count { get; set; }

		public DateTime created_at { get; set; }
	}

}