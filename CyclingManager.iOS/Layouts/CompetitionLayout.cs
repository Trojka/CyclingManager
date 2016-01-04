using System;
using UIKit;
using System.Collections.Generic;
using CoreGraphics;
using Foundation;

namespace CyclingManager
{
	[Register ("CompetitionLayout")]
	public partial class CompetitionLayout : ExpandingItemLayout
	{
		public CompetitionLayout (IntPtr handle) : base (handle)
		{
			StandardHeight = 100.0f;
			FeaturedHeight = 200.0f;
			DragOffset = 100.0f;
		}
	}
}

