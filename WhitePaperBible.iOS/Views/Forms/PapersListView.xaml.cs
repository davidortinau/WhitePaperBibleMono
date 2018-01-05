using System;
using System.Collections.Generic;

using Xamarin.Forms;
using WhitePaperBible.Core.Models;
using System.ComponentModel;

namespace Forms
{
    public partial class PapersListView : ContentPage, INotifyPropertyChanged
    {
        List<Paper> papers;

        public event EventHandler ItemSelected = delegate { };

        public List<Paper> Papers {
            get {
                return papers;
            }

            set {
                papers = value;

                // raise change event?
                OnPropertyChanged (nameof (Papers));
            }
        }

        public string SearchQuery { get; set; }

        public Paper SelectedPaper { get; set; }

        public PapersListView ()
        {
            InitializeComponent ();

            BindingContext = this;
        }

        void Handle_TextChanged (object sender, Xamarin.Forms.TextChangedEventArgs e)
        {
            throw new NotImplementedException ();
        }

        void Handle_ItemSelected (object sender, Xamarin.Forms.SelectedItemChangedEventArgs e)
        {
            ItemSelected.Invoke (this, e);
            //((ListView)sender).SelectedItem = null;
        }
    }
}
