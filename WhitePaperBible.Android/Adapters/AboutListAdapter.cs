
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
	public class AboutListAdapter : BaseAdapter
	{
		List<string> items;
		Activity context;

		public AboutListAdapter(Activity context, List<string> items) : base() {
			this.context = context;
			this.items = items;
		}

		public override long GetItemId(int position)
		{
			return position;
		}

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
				itemView = layoutInflater.Inflate(Resource.Layout.AboutListItem, parent, false);
			}

			TextView titleTxt = itemView.FindViewById<TextView>(Resource.Id.titleTextView);

			var value = items.ElementAt(position);
			if(value.Contains("App Version")){
				value = string.Format("{0} {1}", value, context.PackageManager.GetPackageInfo(context.PackageName, 0).VersionName);
			}
			titleTxt.Text = value;

			return itemView;
		}

	}
}

