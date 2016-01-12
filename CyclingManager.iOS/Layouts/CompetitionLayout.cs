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
		public const double CompetitionStandardHeight = 100.0f;
		public const double CompetitionFeaturedHeight = 200.0f;

		public CompetitionLayout (IntPtr handle) : base (handle)
		{
			StandardHeight = (nfloat)CompetitionStandardHeight;
			FeaturedHeight = (nfloat)CompetitionFeaturedHeight;
			DragOffset = 100.0f;
		}
	}
}

