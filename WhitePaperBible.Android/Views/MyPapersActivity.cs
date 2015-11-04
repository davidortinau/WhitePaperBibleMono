
using System.Collections.Generic;

using Android.App;
using Android.OS;
using WhitePaperBible.Core.Views;
using MonkeyArms;
using Android.Widget;
using System;
using Android.Text;
using Android.Webkit;
using Android.Content;
using WhitePaperBible.Core.Models;
using Newtonsoft.Json;
using Android.Support.V4.View;
using Android.Support.V7.View;
using Java.Interop;
using Android.Views;
using Cocosw.BottomSheetActions;

namespace WhitePaperBible.Droid
{
	[Activity (Label = "")]			
    public class MyPapersActivity : BaseActivityView, IMyPapersView, IInjectingTarget
	{
        private ListView _listView;

        private List<Paper> _papers;

        private TextView _nameText;

        private TextView _editButton;

		protected override void OnCreate (Bundle bundle)
		{
            base.OnCreateWithLayout(bundle, Resource.Layout.MyPapersView);

			this.setSupportActionBarTitle ("My Papers");
			this.addSupportActionBarBackButton ();

            _listView = FindViewById<ListView>(Resource.Id.PapersByTagList);
            _listView.ItemClick += OnRowClicked;

            _nameText = FindViewById<TextView>(Resource.Id.NameText);

            _editButton = FindViewById<TextView>(Resource.Id.EditButton);
            _editButton.Click += (object sender, EventArgs e) => {
                var editIntent = new Intent(this.BaseContext, typeof(EditProfileActivity));
                StartActivity(editIntent);
            };

            OnPaperSelected = new Invoker();
            Filter = new Invoker();

		}

        void OnRowClicked (object sender, AdapterView.ItemClickEventArgs e)
        {
            var item = _papers[e.Position];

            var courseIntent = new Intent(this, typeof(PaperDetailActivity));
            var json = JsonConvert.SerializeObject(item);
            courseIntent.PutExtra("item_json", json);
            StartActivity(courseIntent);
        }

		public override bool OnCreateOptionsMenu(IMenu menu)
		{
            MenuInflater.Inflate(Resource.Menu.MyPapers, menu);
			_menu = menu;

			return base.OnCreateOptionsMenu(menu);
		}

		public override bool OnOptionsItemSelected (IMenuItem item)
		{
			switch (item.ItemId) {
            case Android.Resource.Id.Home:
				{
                    
					Finish();
					break;
				}
            case Resource.Id.menu_logout:
				{
                    // TODO 
					break;
				}
			
			default:
				return base.OnOptionsItemSelected (item);
			}

			return true;
		}

		
        #region IMyPapersView implementation
        public void SetPapers (List<Paper> papers)
        {
            _papers = papers;
            _listView.Adapter = new PapersAdapter(this, papers);
        }

        public Invoker Filter {
            get;
            private set;
        }

        public Invoker OnPaperSelected {
            get;
            private set;
        }

        public Paper SelectedPaper {
            get;set;
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
            get;
            private set;
        }


        public void SetUserProfile (AppUser user)
        {
            _nameText.Text = user.Name;
        }
        #endregion
	}

}

