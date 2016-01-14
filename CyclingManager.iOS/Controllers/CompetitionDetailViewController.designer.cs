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
	partial class CompetitionDetailViewController
	{
		[Outlet]
		UIKit.UILabel CompetitionDateLabel { get; set; }

		[Outlet]
		UIKit.UITableView CompetitionDetailTable { get; set; }

		[Outlet]
		UIKit.UILabel CompetitionNameLabel { get; set; }

		[Action ("CompetitionDetailTableSource:")]
		partial void CompetitionDetailTableSource (Foundation.NSObject sender);
		
		void ReleaseDesignerOutlets ()
		{
			if (CompetitionDateLabel != null) {
				CompetitionDateLabel.Dispose ();
				CompetitionDateLabel = null;
			}

			if (CompetitionNameLabel != null) {
				CompetitionNameLabel.Dispose ();
				CompetitionNameLabel = null;
			}

			if (CompetitionDetailTable != null) {
				CompetitionDetailTable.Dispose ();
				CompetitionDetailTable = null;
			}
		}
	}
}
