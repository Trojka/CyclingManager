using System;
using UIKit;
using Foundation;

namespace CyclingManager
{
	[Register ("CompetitionTableCyclerCell")]
	public partial class CompetitionTableCyclerCell : UITableViewCell
	{
		public CompetitionTableCyclerCell (IntPtr handle) : base (handle)
		{
		}

		public string CyclerName {
			get { return CyclerNameLabel.Text; }
			set { CyclerNameLabel.Text = value; }
		}

		public string CyclerScore {
			get { return CyclerScoreLabel.Text; }
			set { CyclerScoreLabel.Text = value; }
		}

		public UIImage CountryFlagImage {
			get { return CountryFlagImageView.Image; }
			set { CountryFlagImageView.Image = value; }
		}
	}
}

