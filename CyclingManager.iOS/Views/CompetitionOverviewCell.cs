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

		public string CompetitionDate {
			get { return CompetitionDateLabel.Text; }
			set { CompetitionDateLabel.Text = value; }
		}

		public UIView CompetitionStageHolderView {
			get { return CompetitionStageHolder; }
		}

		public string CompetitionStage1CyclerName {
			get { return CompetitionFirstCyclerLabel.Text; }
			set { CompetitionFirstCyclerLabel.Text = value; }
		}

		public UIImage CompetitionStage1CyclerFlagImage {
			get { return CompetitionFirstCyclerFlag.Image; }
			set { CompetitionFirstCyclerFlag.Image = value; }
		}

		public string CompetitionStage2CyclerName {
			get { return CompetitionSecondCyclerLabel.Text; }
			set { CompetitionSecondCyclerLabel.Text = value; }
		}

		public UIImage CompetitionStage2CyclerFlagImage {
			get { return CompetitionSecondCyclerFlag.Image; }
			set { CompetitionSecondCyclerFlag.Image = value; }
		}

		public string CompetitionStage3CyclerName {
			get { return CompetitionFirstCyclerLabel.Text; }
			set { CompetitionThirdCyclerLabel.Text = value; }
		}

		public UIImage CompetitionStage3CyclerFlagImage {
			get { return CompetitionFirstCyclerFlag.Image; }
			set { CompetitionThirdCyclerFlag.Image = value; }
		}

		public override void ApplyLayoutAttributes (UICollectionViewLayoutAttributes layoutAttributes)
		{
			base.ApplyLayoutAttributes (layoutAttributes);

			nfloat featuredHeight = (nfloat)CompetitionLayout.CompetitionFeaturedHeight;
			nfloat standardHeight = (nfloat)CompetitionLayout.CompetitionStandardHeight;

			nfloat delta = 1 - ((featuredHeight - this.Frame.Height) / (featuredHeight - standardHeight));

			CompetitionStageHolder.Alpha = delta;
		}
	}
}

