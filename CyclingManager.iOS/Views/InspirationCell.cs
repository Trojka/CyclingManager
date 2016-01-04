using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;
using CoreGraphics;

namespace CyclingManager
{
//	partial class InspirationCell : UICollectionViewCell
//	{
//		public InspirationCell (IntPtr handle) : base (handle)
//		{
//		}
//
//		Inspiration content;
//		public Inspiration Content {
//			get { return content; }
//			set {
//				content = value;
//				if (content != null) {
//					imageView.Image = content.BackgroundImage;
//					titleLabel.Text = content.Title;
//					timeAndRoomLabel.Text = content.RoomAndTime;
//					speakerLabel.Text = content.Speaker;
//				}
//			}
//		}
//
//		public override void ApplyLayoutAttributes (UICollectionViewLayoutAttributes layoutAttributes)
//		{
//			base.ApplyLayoutAttributes (layoutAttributes);
//
//			var standardHeight = UltravisualLayoutConstants.StandardHeight;
//			var featuredHeight = UltravisualLayoutConstants.FeaturedHeight;
//
//			var delta = 1 - ((featuredHeight - this.Frame.Height) / (featuredHeight - standardHeight));
//
//			nfloat minAlpha = 0.3f;
//			nfloat maxAlpha = 0.75f;
//			imageCoverView.Alpha = maxAlpha - (delta * (maxAlpha - minAlpha));
//
//			nfloat scale = (nfloat)Math.Max(delta, 0.5);
//			titleLabel.Transform = CGAffineTransform.MakeScale(scale, scale);
//
//			timeAndRoomLabel.Alpha = delta;
//			speakerLabel.Alpha = delta;
//		}
//	}
}
