
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using WhitePaperBible.Droid.Fragments;
using WhitePaperBible.Core.Views;
using MonkeyArms;

namespace Views
{
	public class TagsView : BaseFragment, ITagsListView, IInjectingTarget
	{
		public TagsView (int layoutId) : base (layoutId)
		{
			
		}

		#region ITagsListView implementation

		public void SetTags (List<WhitePaperBible.Core.Models.Tag> tags)
		{
			throw new NotImplementedException ();
		}

		public Invoker Filter {
			get {
				throw new NotImplementedException ();
			}
		}

		public Invoker OnTagSelected {
			get {
				throw new NotImplementedException ();
			}
		}

		public string SearchPlaceHolderText {
			get {
				throw new NotImplementedException ();
			}
			set {
				throw new NotImplementedException ();
			}
		}

		public string SearchQuery {
			get {
				throw new NotImplementedException ();
			}
		}

		public WhitePaperBible.Core.Models.Tag SelectedTag {
			get {
				throw new NotImplementedException ();
			}
			set {
				throw new NotImplementedException ();
			}
		}

		#endregion
	}
}

