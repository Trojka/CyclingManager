using System;
using System.Linq;
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
			//cell.ContentView.BackgroundColor = competitions[(int)indexPath.Item].CompetitionColor.ToUIColor();
			cell.CompetitionStageHolderView.Layer.BorderWidth = 2;
			cell.CompetitionStageHolderView.Layer.CornerRadius = 5;
			cell.CompetitionStageHolderView.Layer.BorderColor = UIColor.Black.CGColor;

			cell.CompetitionName = competitions [(int)indexPath.Item].Name;
			cell.CompetitionDate = competitions [(int)indexPath.Item].Date.ToString("dd MMM yyyy");

			cell.CompetitionStage1CyclerName = competitions [(int)indexPath.Item].Stage1.Name;
			NSData flag1ImageData = NSData.FromArray (DataSource.GetResource(competitions [(int)indexPath.Item].Stage1.CountryFlagUrl));
			cell.CompetitionStage1CyclerFlagImage = UIImage.LoadFromData (flag1ImageData, 1);
			cell.CompetitionStage2CyclerName = competitions [(int)indexPath.Item].Stage2.Name;
			NSData flag2ImageData = NSData.FromArray (DataSource.GetResource(competitions [(int)indexPath.Item].Stage2.CountryFlagUrl));
			cell.CompetitionStage2CyclerFlagImage = UIImage.LoadFromData (flag2ImageData, 1);
			cell.CompetitionStage3CyclerName = competitions [(int)indexPath.Item].Stage3.Name;
			NSData flag3ImageData = NSData.FromArray (DataSource.GetResource(competitions [(int)indexPath.Item].Stage3.CountryFlagUrl));
			cell.CompetitionStage3CyclerFlagImage = UIImage.LoadFromData (flag3ImageData, 1);

			return cell;
		}

		public override void PrepareForSegue (UIStoryboardSegue segue, NSObject sender)
		{
			if (segue.Identifier == "CompetitionDetailSegue") {
				var targetController = segue.DestinationViewController as CompetitionDetailViewController;
				NSIndexPath selectedIndexPath = competitionCollectionView.GetIndexPathsForSelectedItems().FirstOrDefault();
				if (selectedIndexPath == null)
					return;

				//				NSData data = NSData.FromArray (sortedTeams[(int)selectedIndexPath.Item].ImageData);
				//				targetController.TeamImage = UIImage.LoadFromData (data, 1);

				targetController.Competition = competitions [(int)selectedIndexPath.Item];
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

			competitionCollectionView.DecelerationRate = UIScrollView.DecelerationRateFast;
			competitionCollectionView.WeakDataSource = this;

		}

		public override void ViewDidLayoutSubviews ()
		{
			base.ViewDidLayoutSubviews ();

			this.View.LayoutIfNeeded ();

			var mostRecentCompetition = competitions.Where (c => c.Date <= DateTime.Now).LastOrDefault ();
			int mostRecentCompetationIndex = competitions.IndexOf (mostRecentCompetition);
			competitionCollectionView.ScrollToItem (NSIndexPath.FromItemSection (mostRecentCompetationIndex, 0), UICollectionViewScrollPosition.Top, false);
		}
	}
}

