using System;
using UIKit;
using System.Collections.Generic;
using CoreGraphics;
using Foundation;

namespace CyclingManager
{
	public class ExpandingItemLayout : UICollectionViewLayout
	{
//		public static readonly nfloat StandardHeight = 100;
//		public static readonly nfloat FeaturedHeight = 200;

		List<UICollectionViewLayoutAttributes> cache = new List<UICollectionViewLayoutAttributes> ();

		public ExpandingItemLayout (IntPtr handle) : base (handle)
		{
		}

		public nfloat StandardHeight { get; set; }

		public nfloat FeaturedHeight { get; set; }

		public nfloat DragOffset { get; set; } = 100.0f;

		public nint FeaturedItemIndex {
			get {
				return Math.Max (0, (int)(CollectionView.ContentOffset.Y / DragOffset));
			}
		}

		public nfloat NextItemPercentageOffset {
			get {
				return (CollectionView.ContentOffset.Y / DragOffset) - (nfloat)(FeaturedItemIndex);
			}
		}

		public nfloat Width {
			get {
				return CollectionView.Bounds.Width;
			}
		}


		public nfloat Height {
			get {
				return CollectionView.Bounds.Height;
			}
		}

		public nint NumberOfItems {
			get {
				return CollectionView.NumberOfItemsInSection (0);
			}
		}

		public override CoreGraphics.CGSize CollectionViewContentSize {
			get {
				var contentHeight = ((nfloat)(NumberOfItems) * DragOffset) + (Height - DragOffset);
				return new CoreGraphics.CGSize (Width, contentHeight);
			}
		}

		public override void PrepareLayout ()
		{
			cache.Clear();

			var standardHeight = StandardHeight;
			var featuredHeight = FeaturedHeight;

			var frame = CGRect.Empty;
			nfloat y = 0;

			for (nint item = 0; item < NumberOfItems; item++) {
				var indexPath = NSIndexPath.FromItemSection (item, 0);
				var attributes = UICollectionViewLayoutAttributes.CreateForCell (indexPath);

				attributes.ZIndex = item;
				var height = standardHeight;

				if (indexPath.Item == FeaturedItemIndex) {
					var yOffSet = standardHeight * NextItemPercentageOffset;
					y = CollectionView.ContentOffset.Y - yOffSet;
					height = featuredHeight;
				}
				else if (indexPath.Item == (FeaturedItemIndex + 1) && (indexPath.Item != NumberOfItems)) {
					var maxY = y + standardHeight;
					height = standardHeight + (nfloat)Math.Max((featuredHeight - standardHeight) * NextItemPercentageOffset, 0);
					y = maxY - height;
				}

				frame = new CGRect(0, y, Width, height);
				attributes.Frame = frame;
				cache.Add (attributes);
				y = frame.GetMaxY();
			}
		}

		public override UICollectionViewLayoutAttributes[] LayoutAttributesForElementsInRect (CoreGraphics.CGRect rect)
		{
			var layoutAttributes = new List<UICollectionViewLayoutAttributes> ();
			foreach(var attributes in cache) 
			{
				if (attributes.Frame.IntersectsWith(rect))
				{
					layoutAttributes.Add(attributes);
				}
			}
			return layoutAttributes.ToArray();	
		}

		public override bool ShouldInvalidateLayoutForBoundsChange (CoreGraphics.CGRect newBounds)
		{
			return true;
		}

		public override CGPoint TargetContentOffset (CGPoint proposedContentOffset, CGPoint scrollingVelocity)
		{
			var itemIndex = Math.Round (proposedContentOffset.Y / DragOffset);
			var yOffset = itemIndex * DragOffset;
			return new CGPoint (0, yOffset);
		}
	}
}

