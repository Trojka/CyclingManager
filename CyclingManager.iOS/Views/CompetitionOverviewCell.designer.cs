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
	partial class CompetitionOverviewCell
	{
		[Outlet]
		UIKit.UILabel CompetitionFirstCyclerLabel { get; set; }

		[Outlet]
		UIKit.UILabel CompetitionNameLabel { get; set; }

		[Outlet]
		UIKit.UILabel CompetitionSecondCyclerLabel { get; set; }

		[Outlet]
		UIKit.UILabel CompetitionThirdCyclerLabel { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (CompetitionFirstCyclerLabel != null) {
				CompetitionFirstCyclerLabel.Dispose ();
				CompetitionFirstCyclerLabel = null;
			}

			if (CompetitionSecondCyclerLabel != null) {
				CompetitionSecondCyclerLabel.Dispose ();
				CompetitionSecondCyclerLabel = null;
			}

			if (CompetitionThirdCyclerLabel != null) {
				CompetitionThirdCyclerLabel.Dispose ();
				CompetitionThirdCyclerLabel = null;
			}

			if (CompetitionNameLabel != null) {
				CompetitionNameLabel.Dispose ();
				CompetitionNameLabel = null;
			}
		}
	}
}
