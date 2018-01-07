using System;
using System.Collections.Generic;

using Xamarin.Forms;
using WhitePaperBible.Core.Models;
using System.ComponentModel;
using System.Linq;
using System.Collections.ObjectModel;

namespace Forms
{
    public partial class TagsListView : ContentPage, INotifyPropertyChanged
    {
        List<Tag> tags;

        string searchQuery;

        public event EventHandler ItemSelected = delegate { };

        private ObservableCollection<ObservableGroupCollection<string, Tag>> groupedTags;

        public List<Tag> Tags {
            get {
                if (string.IsNullOrEmpty (searchQuery)) {
                    return tags;
                } else {
                    return tags.Where (x => x.name.IndexOf (searchQuery, StringComparison.OrdinalIgnoreCase) != -1).ToList ();
                }
            }

            set {
                tags = value;
                UpdateGroupedTags (tags);
            }
        }

        private void UpdateGroupedTags (List<Tag> tags)
        {

            var g = tags.OrderBy (p => p.name)
                                  .GroupBy (p => p.name [0].ToString ().ToUpper ())
                .Select (p => new ObservableGroupCollection<string, Tag> (p))
                .ToList ();

            GroupedTags = new ObservableCollection<ObservableGroupCollection<string, Tag>> (g);
        }

        public string SearchQuery { get; set; }

        public Tag SelectedTag { get; set; }
        public ObservableCollection<ObservableGroupCollection<string, Tag>> GroupedTags {
            get {
                return groupedTags;
            }
            set { 
                groupedTags = value; 
                OnPropertyChanged (nameof (GroupedTags));
            }
        }

        public TagsListView ()
        {
            InitializeComponent ();

            BindingContext = this;
        }

        void Handle_TextChanged (object sender, Xamarin.Forms.TextChangedEventArgs e)
        {
            searchQuery = e.NewTextValue;
            //var filtered = tags.Where (x => x.name.IndexOf (searchQuery, StringComparison.OrdinalIgnoreCase) > -1).ToList ();
            UpdateGroupedTags (Tags);
        }

        void Handle_ItemSelected (object sender, Xamarin.Forms.SelectedItemChangedEventArgs e)
        {
            ItemSelected.Invoke (this, e);
            //((ListView)sender).SelectedItem = null;
        }
    }

    public class ObservableGroupCollection<S, T> : ObservableCollection<T>
    {
        private readonly S _key;

        public ObservableGroupCollection (IGrouping<S, T> group)
            : base (group)
        {
            _key = group.Key;
        }

        public S Key {
            get { return _key; }
        }

    }
}
