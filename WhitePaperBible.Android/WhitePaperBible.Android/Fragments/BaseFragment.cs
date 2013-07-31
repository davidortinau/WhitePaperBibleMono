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

namespace WhitePaperBible.Android.Fragments
{
	public class BaseFragment : Fragment
	{

		public event EventHandler ToggleSettingsView = delegate{};

		protected string ScreenTitle;

		protected SearchView SearchInput;

		protected ViewStates SearchVisibility = ViewStates.Gone;

		protected ViewStates FilterVisibility = ViewStates.Gone;

		protected ViewStates RightHeaderButtonVisiblity = ViewStates.Gone;

		protected string RightHeaderButtonTitle = "";


		#region IBaseCalendarView implementation

		public void GoCalendarMonthView ()
		{
			//do nothing
		}

		public void GoCalendarWeekView ()
		{
			//do nothing
		}

		public void GoCalendarDayView ()
		{
			//do nothing
		}

		public void GoAboutView ()
		{
			//do nothing
		}

		public void GoParentDirectory ()
		{
			//do nothing
		}

		#endregion

		protected int LayoutID;

		public override void OnCreate (Bundle savedInstanceState)
		{
			base.OnCreate (savedInstanceState);

			// Create your fragment here
		}

		public override View OnCreateView (LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
		{
			if (container == null) {
				return null;
			}

			var frame = new LinearLayout (Activity);
			frame.Orientation = Orientation.Vertical;

			InitHeader (inflater, frame);


			var view = inflater.Inflate (LayoutID, null);
			frame.AddView (view);

			return frame;
		}

		void InitHeader (LayoutInflater inflater, LinearLayout frame)
		{
			var headerView = inflater.Inflate (Resource.Layout.ScreenHeader, null);
			frame.AddView (headerView);

			var titleText = headerView.FindViewById (Resource.Id.screenTitle) as TextView;
			titleText.Text = ScreenTitle;

			InitSearchInput (headerView, titleText);
		}

		virtual protected void SubmitSearch(string query)
		{
			throw(new NotImplementedException("OnSearchSubmitted needs to be overridden if header search field is going to be used"));
		}

		virtual protected void ResetSearch()
		{
			throw(new NotImplementedException("OnSearchReset needs to be overridden if header search field is going to be used"));
		}

		virtual protected void RightHeaderButtonClicked()
		{
			throw(new NotImplementedException("RightHeaderButtonClicked needs to be overridden if right header button is going to be used"));
		}

		void InitSearchInput (View headerView, TextView titleText)
		{
			SearchInput = headerView.FindViewById (Resource.Id.searchInput) as SearchView;
			SearchInput.QueryTextFocusChange += (object sender, View.FocusChangeEventArgs e) =>  {
				if (!e.HasFocus) {
					ResetSearch ();
				}
			};
			SearchInput.SearchClick += (object sender, EventArgs e) =>  {
				titleText.Visibility = ViewStates.Gone;
			};
			SearchInput.QueryTextSubmit += (object sender, SearchView.QueryTextSubmitEventArgs e) =>  {
				SubmitSearch (SearchInput.Query);
			};
			SearchInput.Visibility = SearchVisibility;
		}
	}
}

