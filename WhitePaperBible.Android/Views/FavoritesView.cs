
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
			Filter = new Invoker();
			OnPaperSelected = new Invoker();
		}

		#region IFavoritesView implementation

		public void SetPapers (List<WhitePaperBible.Core.Models.Paper> papers)
		{
			// add to table
		}

		public void ShowLoginButton ()
		{
			// 
		}

		public void HideLoginButton ()
		{
			// 
		}

		public Invoker Filter {
			get;
			private set;
		}

		public Invoker OnPaperSelected {
			get;
			private set;
		}

		public string SearchPlaceHolderText {
			get;set;
		}

		public string SearchQuery {
			get;
		}

		public WhitePaperBible.Core.Models.Paper SelectedPaper {
			get;set;
		}

		#endregion
	}
}

