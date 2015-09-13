
using System.Collections.Generic;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Views;
using Android.Widget;

using WhitePaperBible.Core.Views;
using MonkeyArms;
using System;
using WhitePaperBible.Core.Models;
using WhitePaperBible.Droid.Fragments;
using Android.Support.V7.Widget;

namespace WhitePaperBible.Droid
{
	public class PapersView : BaseFragment, IPapersListView, IInjectingTarget
	{
		public PapersView (int layoutId) : base (layoutId)
		{
			AddPaper = new Invoker ();	
		}

		public void AddPaperEditView ()
		{
			throw new NotImplementedException ();
		}

		public void PromptForLogin ()
		{
			throw new NotImplementedException ();
		}

		public void DismissLoginPrompt ()
		{
			throw new NotImplementedException ();
		}

		public Invoker AddPaper {
			get;
			private set;
		}

		[Inject]
		public AppModel Model;

		List<Paper> Papers;

		public event EventHandler Filter = delegate {};

		public event EventHandler OnPaperSelected = delegate {};

		public string SearchQuery {
			get;
			set;
		}

		public string SearchPlaceHolderText {
			get;
			set;
		}

		public Paper SelectedPaper {
			get;
			set;
		}



		#region IPapersListView implementation

		public void SetPapers (List<Paper> papers)
		{
			this.Papers = papers;
//			RunOnUiThread(()=>{
//				ListAdapter = new PapersAdapter(this, papers);
//			});
		}

		#endregion
		  
	}

}

