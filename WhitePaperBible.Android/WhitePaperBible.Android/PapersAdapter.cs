
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
using WhitePaperBibleCore.Models;

namespace WhitePaperBible.Android
{		
	public class PapersAdapter : BaseAdapter
	{
		List<Paper> items;
		Activity context;

		public PapersAdapter(Activity context, List<Paper> items) : base() {
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
				itemView = layoutInflater.Inflate(Android.Resource.Layout.PaperListItem, parent, false);
			}

			TextView titleTxtView = itemView.FindViewById<TextView>(Android.Resource.Id.titleTextView);

			var paper = items.ElementAt(position);
			titleTxtView.Text = paper.title;


			return itemView;
		}

	}
}

