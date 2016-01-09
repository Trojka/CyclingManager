// WARNING
//
// This file has been generated automatically by Xamarin Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace CyclingManager
{
	partial class TeamDetailViewController
	{
		[Outlet]
		UIKit.UIButton FollowButton { get; set; }

		[Outlet]
		UIKit.NSLayoutConstraint HeaderHeightConstraint { get; set; }

		[Outlet]
		UIKit.UIImageView OwnerImageView { get; set; }

		[Outlet]
		UIKit.UILabel OwnerNameLabel { get; set; }

		[Outlet]
		UIKit.UIButton RankingButton { get; set; }

		[Outlet]
		UIKit.UITableView TeamCyclersTable { get; set; }

		[Outlet]
		UIKit.UIView TeamHeaderHolder { get; set; }

		[Outlet]
		UIKit.UIImageView TeamImageView { get; set; }

		[Outlet]
		UIKit.UIView TeamOwnerDetailHolder { get; set; }

		[Outlet]
		UIKit.NSLayoutConstraint TeamOwnerHolderHeightConstraint { get; set; }

		[Outlet]
		UIKit.UILabel TeamRankingGraphCurrentCompetitionNameLabel { get; set; }

		[Outlet]
		UIKit.UIView TeamRankingGraphHolder { get; set; }

		[Outlet]
		UIKit.NSLayoutConstraint TeamRankingGraphHolderHeightConstraint { get; set; }

		[Action ("CompareTeam:")]
		partial void CompareTeam (Foundation.NSObject sender);

		[Action ("ShowRankingDetail:")]
		partial void ShowRankingDetail (Foundation.NSObject sender);
		
		void ReleaseDesignerOutlets ()
		{
			if (FollowButton != null) {
				FollowButton.Dispose ();
				FollowButton = null;
			}

			if (HeaderHeightConstraint != null) {
				HeaderHeightConstraint.Dispose ();
				HeaderHeightConstraint = null;
			}

			if (OwnerImageView != null) {
				OwnerImageView.Dispose ();
				OwnerImageView = null;
			}

			if (OwnerNameLabel != null) {
				OwnerNameLabel.Dispose ();
				OwnerNameLabel = null;
			}

			if (RankingButton != null) {
				RankingButton.Dispose ();
				RankingButton = null;
			}

			if (TeamCyclersTable != null) {
				TeamCyclersTable.Dispose ();
				TeamCyclersTable = null;
			}

			if (TeamHeaderHolder != null) {
				TeamHeaderHolder.Dispose ();
				TeamHeaderHolder = null;
			}

			if (TeamImageView != null) {
				TeamImageView.Dispose ();
				TeamImageView = null;
			}

			if (TeamOwnerDetailHolder != null) {
				TeamOwnerDetailHolder.Dispose ();
				TeamOwnerDetailHolder = null;
			}

			if (TeamOwnerHolderHeightConstraint != null) {
				TeamOwnerHolderHeightConstraint.Dispose ();
				TeamOwnerHolderHeightConstraint = null;
			}

			if (TeamRankingGraphHolder != null) {
				TeamRankingGraphHolder.Dispose ();
				TeamRankingGraphHolder = null;
			}

			if (TeamRankingGraphCurrentCompetitionNameLabel != null) {
				TeamRankingGraphCurrentCompetitionNameLabel.Dispose ();
				TeamRankingGraphCurrentCompetitionNameLabel = null;
			}

			if (TeamRankingGraphHolderHeightConstraint != null) {
				TeamRankingGraphHolderHeightConstraint.Dispose ();
				TeamRankingGraphHolderHeightConstraint = null;
			}
		}
	}
}
