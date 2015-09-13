
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
	public class FavoritesView : BaseFragment, IFavoritesView, IInjectingTarget
	{
		public FavoritesView (int layoutId) : base (layoutId)
		{
			
		}

		#region IFavoritesView implementation

		public void SetPapers (List<WhitePaperBible.Core.Models.Paper> papers)
		{
			throw new NotImplementedException ();
		}

		public void ShowLoginButton ()
		{
			throw new NotImplementedException ();
		}

		public void HideLoginButton ()
		{
			throw new NotImplementedException ();
		}

		public Invoker Filter {
			get {
				throw new NotImplementedException ();
			}
		}

		public Invoker OnPaperSelected {
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

		public WhitePaperBible.Core.Models.Paper SelectedPaper {
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

