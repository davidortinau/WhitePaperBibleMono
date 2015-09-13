
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
	public class SearchView : BaseFragment, IBibleSearchView, IInjectingTarget
	{
		public SearchView (int layoutId) : base (layoutId)
		{
			
		}

		#region IBibleSearchView implementation

		public MonkeyArms.Invoker DoSearch {
			get {
				throw new NotImplementedException ();
			}
		}

		#endregion
	}
}

