using System;
using UIKit;
using System.Collections.Generic;
using CoreGraphics;
using Foundation;

namespace CyclingManager
{
	[Register ("TeamLayout")]
	public partial class TeamLayout : ExpandingItemLayout
	{
		public TeamLayout (IntPtr handle) : base (handle)
		{
			StandardHeight = 80.0f;
			FeaturedHeight = 80.0f;
			DragOffset = 80.0f;
		}
	}
}

