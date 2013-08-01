using System;

using System.Collections.Generic;

namespace WhitePaperBibleCore.Models
{
	public class AppModel
	{
		public List<Paper> Papers { get; set; } 

		public Paper CurrentPaper {
			get;
			set;
		}

		public AppModel ()
		{
		}
	}
}

