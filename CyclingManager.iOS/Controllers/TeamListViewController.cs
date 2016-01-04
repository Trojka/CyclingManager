using System;
using UIKit;
using Foundation;
using CyclingManager.Shared;
using System.Collections.Generic;
using CyclingManager.Shared.DomainModel;
using System.Linq;

namespace CyclingManager
{
	[Register ("TeamListViewController")]
	public partial class TeamListViewController : UIViewController
	{
		List<Team> teams = DataSource.Teams();

		public TeamListViewController  (IntPtr handle) : base (handle)
		{
		}

		[Export("collectionView:numberOfItemsInSection:")]
		public nint GetItemsCount (UICollectionView collectionView, nint section)
		{
			return teams.Count;
		}

		[Export ("collectionView:cellForItemAtIndexPath:")]
		public UICollectionViewCell GetCell (UICollectionView collectionView, NSIndexPath indexPath)
		{
			var cell = collectionView.DequeueReusableCell("TeamOverviewCell", indexPath) as TeamOverviewCell;

			NSData data = NSData.FromArray (teams[(int)indexPath.Item].ImageData);
			cell.TeamImage = UIImage.LoadFromData (data, 1);

			cell.TeamName = teams [(int)indexPath.Item].Name;
			cell.TeamScore = teams [(int)indexPath.Item].Score + " punten";

			return cell;
		}

		public override void PrepareForSegue (UIStoryboardSegue segue, NSObject sender)
		{
			if (segue.Identifier == "TeamDetailSegue") {
				var targetController = segue.DestinationViewController as TeamDetailViewController;
				NSIndexPath selectedIndexPath = teamCollectionView.GetIndexPathsForSelectedItems().FirstOrDefault();
				if (selectedIndexPath == null)
					return;
				
				NSData data = NSData.FromArray (teams[(int)selectedIndexPath.Item].ImageData);
				targetController.TeamImage = UIImage.LoadFromData (data, 1);
			}
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

			teamCollectionView.DecelerationRate = UIScrollView.DecelerationRateFast;
			teamCollectionView.WeakDataSource = this;
		}
	}
}

