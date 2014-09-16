using System;
using System.Collections.Generic;
using System.Linq;
using WhitePaperBible.Core.Services;

namespace WhitePaperBible.Core.Models
{
	public class AppModel
	{
		bool firstLoad = true;
		public bool FirstLoad {
			get {
				return firstLoad;
			}
			set {
				firstLoad = value;
			}
		}

		public virtual AppUser User { get; set; }

		public virtual List<Paper> MyPapers { get; set; }

		public virtual List<Paper> Favorites { get; set; }

		public virtual List<Paper> Papers { get; set; }

		public virtual List<Tag> Tags { get; set; }

		public List<Paper> Popular {
			get;
			set;
		}

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
			User = User ?? new AppUser ();
			User.username = userName;
			User.password = password;

		}

		public void ClearCredentials ()
		{
			User = null;
			IsLoggedIn = false;
			UserSessionCookie = null;
			MyPapers = null;
			Favorites = null;
		}
	}
}

