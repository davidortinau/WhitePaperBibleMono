using System;
using System.Collections.Generic;
using System.Linq;
using WhitePaperBible.Core.Services;

namespace WhitePaperBible.Core.Models
{
	public class AppModel
	{
		public virtual AppUser User { get; set; }

		public virtual List<Paper> MyPapers { get; set; }

		public virtual List<Paper> Favorites { get; set; }

		public virtual List<Paper> Papers { get; set; }

		public virtual List<Tag> Tags { get; set; }

		public SessionCookie UserSessionCookie {
			get;
			set;
		}

		public virtual Paper CurrentPaper {
			get;
			set;
		}

		public virtual Tag CurrentTag {
			get;
			set;
		}

		public virtual bool IsLoggedIn {
			get;
			set;
		}

		public virtual List<Paper> FilterPapers (string query)
		{
			return Papers.Where (ce => (ce.title.ToLower ().Contains (query))).ToList ();
		}

		public virtual List<Tag> FilterTags (string query)
		{
			return Tags.Where (ce => (ce.name.ToLower ().Contains (query))).ToList ();
		}

		public AppModel ()
		{
			IsLoggedIn = false;
		}

		public void StoreCredentials (string userName, string password)
		{
			var user = User ?? new AppUser ();
			user.username = userName;
			user.password = password;
		}
	}
}

