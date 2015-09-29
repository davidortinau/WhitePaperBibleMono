
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using WhitePaperBible.Core.Models;

namespace WhitePaperBible.Droid
{		
	public class PapersAdapter : BaseAdapter, IFilterable
	{
		public List<Paper> Items;
		public List<Paper> FilteredItems;
		Activity context;
		private readonly Lazy<PapersFilter> _filter;

		public PapersAdapter(Activity context, List<Paper> items) : base() {
			this.context = context;
			this.Items = items;
			FilteredItems = items;
			_filter = new Lazy<PapersFilter>(() => new PapersFilter(this), true);
		}


		public Filter Filter {
			get {
				return _filter.Value;
			}
		}

		public override long GetItemId(int position)
		{
			return position;
		}


//		public override string this[int position] {  
//			get { return items[position].paper.title; }
//		}

		public override Java.Lang.Object GetItem (int position)
		{
			return null;
		}

		public override int Count {
			get { return FilteredItems.Count; }
		}

		public override View GetView(int position, View convertView, ViewGroup parent)
		{
			Console.WriteLine("{0} of {1}", position, FilteredItems.Count);
			var itemView = convertView; // re-use an existing view, if one is available
			if (itemView == null) {
				var layoutInflater = (LayoutInflater)context.GetSystemService(Context.LayoutInflaterService);
				//view = context.LayoutInflater.Inflate(Android.Resource.Layout.PaperListItem, null);
				itemView = layoutInflater.Inflate(Resource.Layout.PaperListItem, parent, false);
			}

			TextView titleTxt = itemView.FindViewById<TextView>(Resource.Id.titleTextView);
			TextView authorTxt = itemView.FindViewById<TextView>(Resource.Id.authorTextView);

			var paper = FilteredItems.ElementAt(position);
			titleTxt.Text = paper.title;
			authorTxt.Text = string.Format("by: {0}", paper.Author.Name);

			return itemView;
		}

		public void SetFilter(IEnumerable<string> values)
		{
			FilteredItems = Items.Where(i => values.Contains(i.title)).ToList();
			NotifyDataSetChanged();
		}

		private class PapersFilter : Filter
		{
			private PapersAdapter _adapter;

			public PapersFilter (PapersAdapter adapter)
			{
				_adapter = adapter;
			}
			#region implemented abstract members of Filter
			protected override FilterResults PerformFiltering (Java.Lang.ICharSequence constraint)
			{
				string strConstraint = constraint == null ? null : constraint.ToString();
				List<Paper> items = strConstraint == null ? _adapter.Items.ToList() : _adapter.Items.Where(i => i.title.ToLowerInvariant().Contains(strConstraint.ToLowerInvariant())).ToList();
				FilterResults results = new FilterResults()
				{
					Values = items.Select(i => i.title).ToArray(),
					Count = items.Count
				};

				return results;
			}

			protected override void PublishResults (Java.Lang.ICharSequence constraint, FilterResults results)
			{
				_adapter.SetFilter(results.Values.ToArray<string>());
			}
			#endregion
		}
	}
}

