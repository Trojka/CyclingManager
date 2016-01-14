using System;
using UIKit;
using System.Collections.Generic;
using CyclingManager.Shared.DomainModel;
using CyclingManager.Shared;
using Foundation;

namespace CyclingManager
{
	[Foundation.Register ("CompetitionDetailViewController")]
	public partial class CompetitionDetailViewController : UIViewController
	{
		private enum DetailTableDataSourceEnum {
			Teams,
			Cyclers
		}

		DetailTableDataSourceEnum tableDataSourceType;

		List<Team> teamDataSource;
		List<Cycler> cyclerDataSource;

		public CompetitionDetailViewController (IntPtr handle) : base (handle)
		{
			tableDataSourceType = DetailTableDataSourceEnum.Cyclers;
		}

		partial void CompetitionDetailTableSource (Foundation.NSObject sender)
		{
			if(!(sender is UISegmentedControl))
				return;

			var sortationSelector = sender as UISegmentedControl;

			if(sortationSelector.SelectedSegment == 0)
			{
				tableDataSourceType =  DetailTableDataSourceEnum.Cyclers;
				cyclerDataSource = DataSource.GetCompetitionCyclerResults (0);
			}

			if(sortationSelector.SelectedSegment == 1)
			{
				tableDataSourceType = DetailTableDataSourceEnum.Teams;
				teamDataSource = DataSource.GetCompetitionTeamResults (0);
			}

			CompetitionDetailTable.ReloadData();
		}

		#region implemented abstract members of UITableViewDataSource

		[Export ("tableView:cellForRowAtIndexPath:")]
		public UITableViewCell GetCell (UITableView tableView, NSIndexPath indexPath)
		{
			UITableViewCell cell = null;
			switch (tableDataSourceType) {
			case DetailTableDataSourceEnum.Cyclers:
				CompetitionTableCyclerCell cyclerCell = tableView.DequeueReusableCell ("CompetitionCyclerCell") as CompetitionTableCyclerCell;
				cyclerCell.CyclerName = cyclerDataSource [(int)indexPath.Item].Name;
				cell = cyclerCell;
				break;
			case DetailTableDataSourceEnum.Teams:
				CompetitionTableTeamCell teamCell = tableView.DequeueReusableCell ("CompetitionTeamCell") as CompetitionTableTeamCell;
				teamCell.TeamName = teamDataSource [(int)indexPath.Item].Name;
				cell = teamCell;
				break;
			}

			return cell;
		}

		[Export("tableView:numberOfRowsInSection:")]
		public nint RowsInSection (UITableView tableView, nint section)
		{
			switch (tableDataSourceType) {
			case DetailTableDataSourceEnum.Cyclers:
				return cyclerDataSource.Count;
			case DetailTableDataSourceEnum.Teams:
				return teamDataSource.Count;
			}
			return 0;
		}

		#endregion

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			cyclerDataSource = DataSource.GetCompetitionCyclerResults (0);

			CompetitionDetailTable.WeakDelegate = this;
			CompetitionDetailTable.WeakDataSource = this;
		}
	}
}

