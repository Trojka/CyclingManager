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
	partial class CompetitionTableTeamCell
	{
		[Outlet]
		UIKit.UILabel TeamCompetitionScoreLabel { get; set; }

		[Outlet]
		UIKit.UIImageView TeamManagerImageView { get; set; }

		[Outlet]
		UIKit.UILabel TeamNameLabel { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (TeamNameLabel != null) {
				TeamNameLabel.Dispose ();
				TeamNameLabel = null;
			}

			if (TeamCompetitionScoreLabel != null) {
				TeamCompetitionScoreLabel.Dispose ();
				TeamCompetitionScoreLabel = null;
			}

			if (TeamManagerImageView != null) {
				TeamManagerImageView.Dispose ();
				TeamManagerImageView = null;
			}
		}
	}
}
