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
	}
}

