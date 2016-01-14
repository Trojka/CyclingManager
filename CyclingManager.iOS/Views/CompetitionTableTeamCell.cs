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

		public string TeamName{
			get { return TeamNameLabel.Text; }
			set { TeamNameLabel.Text = value; }
		}
	}
}

