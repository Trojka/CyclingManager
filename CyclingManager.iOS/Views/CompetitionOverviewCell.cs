using System;
using UIKit;
using Foundation;

namespace CyclingManager
{
	[Register ("CompetitionOverviewCell")]
	public partial class CompetitionOverviewCell : UICollectionViewCell
	{
		public CompetitionOverviewCell (IntPtr handle) : base (handle)
		{
		}

		public string CompetitionName {
			get { return CompetitionNameLabel.Text; }
			set { CompetitionNameLabel.Text = value; }
		}

		public override void ApplyLayoutAttributes (UICollectionViewLayoutAttributes layoutAttributes)
		{
			base.ApplyLayoutAttributes (layoutAttributes);

			nfloat featuredHeight = (nfloat)CompetitionLayout.CompetitionFeaturedHeight;
			nfloat standardHeight = (nfloat)CompetitionLayout.CompetitionStandardHeight;

			nfloat delta = 1 - ((featuredHeight - this.Frame.Height) / (featuredHeight - standardHeight));

			CompetitionFirstCyclerLabel.Alpha = delta;
			CompetitionSecondCyclerLabel.Alpha = delta;
			CompetitionThirdCyclerLabel.Alpha = delta;
		}
	}
}

