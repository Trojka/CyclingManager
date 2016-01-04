using System;
using Android.Widget;
using Android.Content;
using System.Collections.Generic;
using Android.Views;

namespace CyclingManager.Android
{
	public class AnimatableCellAdapter : ArrayAdapter<CyclerViewModel>
	{
		public AnimatableCellAdapter (Context context, int textViewResourceId, IList<CyclerViewModel> objects)
			: base(context,textViewResourceId, objects)
		{
		}

		public override long GetItemId (int position)
		{
			return base.GetItem (position).Id;
		}

		public override bool HasStableIds {
			get {
				return true;
			}
		}

		public override View GetView (int position, View convertView, ViewGroup parent)
		{
			//View view = base.GetView (position, convertView, parent);
			if (convertView == null) {
				convertView = LayoutInflater.From (this.Context).Inflate (Resource.Layout.cycler_layout, parent, false);
			}

			CyclerViewModel cycler = GetItem (position) as CyclerViewModel;

			TextView textView = convertView.FindViewById (Resource.Id.cyclerName) as TextView ;
			textView.Text = cycler.ToString();

//			textView.SetX (parent.Width);
//			textView.SetWidth (parent.Width);

			return convertView;
		}
	}
}

