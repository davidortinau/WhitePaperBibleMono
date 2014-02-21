using System;
using System.Collections.Generic;

namespace WhitePaperBible.Core.Models
{
	public class AppModel
	{
		public List<Paper> Papers { get; set; }

		public virtual Paper CurrentPaper {
			get;
			set;
		}

		public AppModel ()
		{
		}
	}
}

