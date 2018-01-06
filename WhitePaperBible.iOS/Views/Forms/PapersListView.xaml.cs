using System;
using System.Collections.Generic;

using Xamarin.Forms;
using WhitePaperBible.Core.Models;
using System.ComponentModel;
using System.Linq;

namespace Forms
{
    public partial class PapersListView : ContentPage, INotifyPropertyChanged
    {
        List<Paper> papers;

        string searchQuery;

        public event EventHandler ItemSelected = delegate { };

        public List<Paper> Papers {
            get {
                if (string.IsNullOrEmpty (searchQuery)) {
                    return papers;
                } else {
                    return papers.Where (x => x.title.IndexOf (searchQuery, StringComparison.OrdinalIgnoreCase) != -1).ToList ();
                }
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
            searchQuery = e.NewTextValue;
            OnPropertyChanged (nameof (Papers));
        }

        void Handle_ItemSelected (object sender, Xamarin.Forms.SelectedItemChangedEventArgs e)
        {
            ItemSelected.Invoke (this, e);
            //((ListView)sender).SelectedItem = null;
        }
    }
}
