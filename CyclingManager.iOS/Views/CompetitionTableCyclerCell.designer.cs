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
	partial class CompetitionTableCyclerCell
	{
		[Outlet]
		UIKit.UIImageView CountryFlagImageView { get; set; }

		[Outlet]
		UIKit.UILabel CyclerNameLabel { get; set; }

		[Outlet]
		UIKit.UILabel CyclerScoreLabel { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (CountryFlagImageView != null) {
				CountryFlagImageView.Dispose ();
				CountryFlagImageView = null;
			}

			if (CyclerNameLabel != null) {
				CyclerNameLabel.Dispose ();
				CyclerNameLabel = null;
			}

			if (CyclerScoreLabel != null) {
				CyclerScoreLabel.Dispose ();
				CyclerScoreLabel = null;
			}
		}
	}
}
