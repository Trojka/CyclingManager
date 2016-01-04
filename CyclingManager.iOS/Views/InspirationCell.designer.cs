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
	[Register ("InspirationCell")]
	partial class InspirationCell
	{
		[Outlet]
		UIKit.UIView imageCoverView { get; set; }

		[Outlet]
		UIKit.UIImageView imageView { get; set; }

		[Outlet]
		UIKit.UILabel speakerLabel { get; set; }

		[Outlet]
		UIKit.UILabel timeAndRoomLabel { get; set; }

		[Outlet]
		UIKit.UILabel titleLabel { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (imageCoverView != null) {
				imageCoverView.Dispose ();
				imageCoverView = null;
			}

			if (imageView != null) {
				imageView.Dispose ();
				imageView = null;
			}

			if (titleLabel != null) {
				titleLabel.Dispose ();
				titleLabel = null;
			}

			if (timeAndRoomLabel != null) {
				timeAndRoomLabel.Dispose ();
				timeAndRoomLabel = null;
			}

			if (speakerLabel != null) {
				speakerLabel.Dispose ();
				speakerLabel = null;
			}
		}
	}
}
