using System;
using UIKit;
using System.Collections.Generic;
using Foundation;
using CyclingManager.Shared.DomainModel;
using CyclingManager.Shared;

namespace CyclingManager
{
	[Register ("CompetitionListViewController")]
	public partial class CompetitionListViewController : UIViewController
	{
		List<Competition> competitions = DataSource.Competitions();

		public CompetitionListViewController (IntPtr handle) : base (handle)
		{
		}

		[Export("collectionView:numberOfItemsInSection:")]
		public nint GetItemsCount (UICollectionView collectionView, nint section)
		{
			return competitions.Count;
		}

		[Export ("collectionView:cellForItemAtIndexPath:")]
		public UICollectionViewCell GetCell (UICollectionView collectionView, NSIndexPath indexPath)
		{
			var cell = competitionCollectionView.DequeueReusableCell("CompetitionOverviewCell", indexPath) as CompetitionOverviewCell;
			cell.ContentView.BackgroundColor = competitions[(int)indexPath.Item].CompetitionColor.ToUIColor();

			cell.CompetitionName = competitions [(int)indexPath.Item].Name;

			return cell;
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			// Perform any additional setup after loading the view, typically from a nib.

			this.NavigationController.NavigationBar.Translucent = true;
			this.NavigationController.NavigationBar.SetBackgroundImage (new UIImage (), UIBarMetrics.Default);
			this.NavigationController.NavigationBar.ShadowImage = new UIImage();
			this.NavigationController.NavigationBar.BackgroundColor = new UIColor(0.0f, 0.0f, 0.0f, 0.0f);

			this.EdgesForExtendedLayout = UIRectEdge.All;

			competitionCollectionView.DecelerationRate = UIScrollView.DecelerationRateFast;
			competitionCollectionView.WeakDataSource = this;

		}
	}
}

