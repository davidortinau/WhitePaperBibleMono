
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
	public class TagsView : BaseMediatedFragment, ITagsListView, IInjectingTarget
	{
		public TagsView (int layoutId) : base (layoutId)
		{
			this.Filter = new Invoker ();
			this.OnTagSelected = new Invoker ();
		}

		#region ITagsListView implementation

		public void SetTags (List<WhitePaperBible.Core.Models.Tag> tags)
		{
			//
		}

		public Invoker Filter {
			get;
			private set;
		}

		public Invoker OnTagSelected {
			get;
			private set;
		}

		public string SearchPlaceHolderText {
			get;set;
		}

		public string SearchQuery {
			get;
		}

		public WhitePaperBible.Core.Models.Tag SelectedTag {
			get;
			set;
		}

		#endregion
	}
}

