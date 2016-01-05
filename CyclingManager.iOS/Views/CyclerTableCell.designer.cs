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
	partial class CyclerTableCell
	{
		[Outlet]
		UIKit.NSLayoutConstraint bottomConstraint { get; set; }

		[Outlet]
		UIKit.UILabel CyclerNameLabel { get; set; }

		[Outlet]
		UIKit.NSLayoutConstraint leadingConstraint { get; set; }

		[Outlet]
		UIKit.NSLayoutConstraint topConstraint { get; set; }

		[Outlet]
		UIKit.NSLayoutConstraint trailingConstraint { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (bottomConstraint != null) {
				bottomConstraint.Dispose ();
				bottomConstraint = null;
			}

			if (leadingConstraint != null) {
				leadingConstraint.Dispose ();
				leadingConstraint = null;
			}

			if (CyclerNameLabel != null) {
				CyclerNameLabel.Dispose ();
				CyclerNameLabel = null;
			}

			if (topConstraint != null) {
				topConstraint.Dispose ();
				topConstraint = null;
			}

			if (trailingConstraint != null) {
				trailingConstraint.Dispose ();
				trailingConstraint = null;
			}
		}
	}
}
