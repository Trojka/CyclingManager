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
	partial class TeamOverviewCell
	{
		[Outlet]
		UIKit.UIImageView teamImageView { get; set; }

		[Outlet]
		UIKit.UILabel teamNameLabel { get; set; }

		[Outlet]
		UIKit.UILabel teamScoreLabel { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (teamImageView != null) {
				teamImageView.Dispose ();
				teamImageView = null;
			}

			if (teamNameLabel != null) {
				teamNameLabel.Dispose ();
				teamNameLabel = null;
			}

			if (teamScoreLabel != null) {
				teamScoreLabel.Dispose ();
				teamScoreLabel = null;
			}
		}
	}
}
