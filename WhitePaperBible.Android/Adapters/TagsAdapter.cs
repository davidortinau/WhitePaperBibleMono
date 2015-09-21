
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

namespace WhitePaperBible.Droid.Adapters
{		
	public class TagsAdapter : BaseAdapter
	{
		List<Tag> items;
		Activity context;

		public TagsAdapter(Activity context, List<Tag> items) : base() {
			this.context = context;
			this.items = items;
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
			get { return items.Count; }
		}

		public override View GetView(int position, View convertView, ViewGroup parent)
		{
			var itemView = convertView; // re-use an existing view, if one is available
			if (itemView == null) {
				var layoutInflater = (LayoutInflater)context.GetSystemService(Context.LayoutInflaterService);
				//view = context.LayoutInflater.Inflate(Android.Resource.Layout.PaperListItem, null);
				itemView = layoutInflater.Inflate(Resource.Layout.TagListItem, parent, false);
			}

			TextView tagTxt = itemView.FindViewById<TextView>(Resource.Id.tagNameTextView);
			TextView countTxt = itemView.FindViewById<TextView>(Resource.Id.countTextView);

			var tag = items.ElementAt(position);
			tagTxt.Text = tag.name;
			countTxt.Text = string.Format("({0})", tag.count);

			return itemView;
		}

	}
}

