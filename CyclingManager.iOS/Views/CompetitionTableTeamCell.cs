using System;
using UIKit;
using Foundation;

namespace CyclingManager
{
	[Register ("CompetitionTableTeamCell")]
	public partial class CompetitionTableTeamCell : UITableViewCell
	{
		public CompetitionTableTeamCell (IntPtr handle) : base (handle)
		{
		}

		public UIImage TeamManagerImage {
			get { return TeamManagerImageView.Image; }
			set { TeamManagerImageView.Image = value; }
		}

		public String TeamName {
			get { return TeamNameLabel.Text; }
			set { TeamNameLabel.Text = value; }
		}

		public String TeamCompetitionScore {
			get { return TeamCompetitionScoreLabel.Text; }
			set { TeamCompetitionScoreLabel.Text = value; }
		}

	}
}

