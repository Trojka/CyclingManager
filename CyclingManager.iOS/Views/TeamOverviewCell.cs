using System;
using UIKit;
using Foundation;

namespace CyclingManager
{
	[Register ("TeamOverviewCell")]
	public partial class TeamOverviewCell : UICollectionViewCell
	{
		public TeamOverviewCell (IntPtr handle) : base (handle)
		{
		}

		public UIImageView TeamImageHolder {
			get { return teamImageView; }
		}

		public UIImage TeamImage {
			get { return teamImageView.Image; }
			set { teamImageView.Image = value; }
		}

		public String TeamName {
			get { return teamNameLabel.Text; }
			set { teamNameLabel.Text = value; }
		}

		public String TeamScore {
			get { return teamScoreLabel.Text; }
			set { teamScoreLabel.Text = value; }
		}
	}
}

