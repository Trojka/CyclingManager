using System;
using UIKit;
using Foundation;
using CoreGraphics;
using System.Collections.Generic;
using System.Linq;
using CyclingManager.Shared.DomainModel;
using CoreAnimation;
using CyclingManager.Shared;

namespace CyclingManager
{
	[Foundation.Register ("TeamDetailViewController")]
	public partial class TeamDetailViewController : UIViewController
	{
		const int HeaderHeight = 125;
		const int HeaderCollapseDelta = 25;
		const int HeaderHeightCollapsed = HeaderHeight - HeaderCollapseDelta;
		const int TeamOwnerHolderHeight = 100;
		const int TeamRankingGraphHolderHeight = 150;

		const double ExpandCollapseAnimationDuration = 1;

		ScoreView scoreView;

		bool RankingDetailIsOpen { get; set;} = false;

		//List<Cycler> team = Cycler.Team1();

		bool m_isInModeCompare = false;

		public TeamDetailViewController (IntPtr handle) : base (handle)
		{
		}

		public Team Team {
			get;
			set;
		}

		private List<Cycler> Cyclers {
			get;
			set;
		}

		private List<CompetitionResult> Results {
			get;
			set;
		}

		partial void ShowRankingDetail (Foundation.NSObject sender)
		{
			if(RankingDetailIsOpen)
			{
				CloseRankingDetail ();
				RankingDetailIsOpen = false;
				return;
			}

			OpenRankingDetail();
			RankingDetailIsOpen = true;
		}

		void OpenRankingDetail()
		{
			this.View.LayoutIfNeeded();

			HeaderHeightConstraint.Constant = HeaderHeightCollapsed;
			TeamOwnerHolderHeightConstraint.Constant = 0;
			TeamRankingGraphHolderHeightConstraint.Constant = TeamRankingGraphHolderHeight;

			var newContentOffset = new CGPoint(0, TeamCyclersTable.ContentOffset.Y - HeaderCollapseDelta);

			UIView.Animate (ExpandCollapseAnimationDuration, () => {
				TeamCyclersTable.ContentInset = new UIEdgeInsets (HeaderHeightCollapsed + TeamRankingGraphHolderHeight, 0, 0, 0);
				TeamCyclersTable.ContentOffset = newContentOffset;
				TeamCyclersTable.ScrollIndicatorInsets = new UIEdgeInsets (HeaderHeightCollapsed + TeamRankingGraphHolderHeight, 0, 0, 0);

				OwnerNameLabel.Alpha = 0;
				OwnerImageView.Alpha = 0;

				this.View.LayoutIfNeeded ();
			},
				() => {
					scoreView = new ScoreView (Results);
					scoreView.Frame = new CGRect (new CGPoint (0, 0), TeamRankingGraphHolder.Frame.Size);
					TeamRankingGraphHolder.AddSubview (scoreView);

					scoreView.Configure ();
					scoreView.ShowData ();
				});
		}

		void CloseRankingDetail ()
		{
			TeamRankingGraphHolder.Subviews.ToList ().ForEach (v => v.RemoveFromSuperview ());

			this.View.LayoutIfNeeded();

			HeaderHeightConstraint.Constant = HeaderHeight;
			TeamOwnerHolderHeightConstraint.Constant = TeamOwnerHolderHeight;
			TeamRankingGraphHolderHeightConstraint.Constant = 0;

			var newContentOffset = new CGPoint(0, TeamCyclersTable.ContentOffset.Y + HeaderCollapseDelta);

			UIView.Animate (ExpandCollapseAnimationDuration, () => {
				TeamCyclersTable.ContentInset = new UIEdgeInsets (HeaderHeight + TeamOwnerHolderHeight, 0, 0, 0);
				TeamCyclersTable.ContentOffset = newContentOffset;
				TeamCyclersTable.ScrollIndicatorInsets = new UIEdgeInsets (HeaderHeight + TeamOwnerHolderHeight, 0, 0, 0);

				OwnerNameLabel.Alpha = 1;
				OwnerImageView.Alpha = 1;

				this.View.LayoutIfNeeded ();
			},
				() => {
					scoreView.RemoveFromSuperview ();
					scoreView = null;
				});
		}

		partial void CompareTeam (Foundation.NSObject sender)
		{
			if(m_isInModeCompare) {
				m_isInModeCompare = false;

				foreach(var indexPath in TeamCyclersTable.IndexPathsForVisibleRows)
				{
					var cell = TeamCyclersTable.CellAt(indexPath) as CyclerTableCell;
					if(Cyclers[(int)indexPath.Item].Origin == TeamOrigin.Mine)
					{
						cell.SetVisualsToOutsideRight (TeamCyclersTable.Bounds.Width);
					}
					else
					{
						cell.SetVisualsToUndetermined();
					}
				}

				var mine = Cyclers.Where(c => c.Origin == TeamOrigin.Mine).ToList();
				var removeIndexPaths = new List<NSIndexPath>();
				foreach(var c in mine)
				{
					int index = Cyclers.FindIndex(tm => tm.Id == c.Id);
					Cyclers.Remove(c);
					removeIndexPaths.Add(NSIndexPath.FromItemSection(index, 0));

				}

				Cyclers.ForEach(t => t.Origin = TeamOrigin.Undetermined);


				UIView.Animate(1, () => {
					this.View.LayoutIfNeeded();
				}, () => {
					TeamCyclersTable.DeleteRows(removeIndexPaths.ToArray(), UITableViewRowAnimation.None);

				});

			}
			else {
				m_isInModeCompare = true;

				var myTeam = Cycler.MyTeam();
				Cyclers.ForEach(t => t.Origin = TeamOrigin.Theirs);
				Cyclers.Where(t => myTeam.Any(mt => mt.Id == t.Id)).ToList().ForEach(t => t.Origin = TeamOrigin.Common);

				foreach(var indexPath in TeamCyclersTable.IndexPathsForVisibleRows)
				{
					var cell = TeamCyclersTable.CellAt(indexPath) as CyclerTableCell;
					if(Cyclers[(int)indexPath.Item].Origin == TeamOrigin.Common)
					{
						cell.SetVisualsToCommon();
					}
					else if(Cyclers[(int)indexPath.Item].Origin == TeamOrigin.Theirs)
					{
						cell.SetVisualsToTheirs();
					}
					else if(Cyclers[(int)indexPath.Item].Origin == TeamOrigin.Undetermined)
					{
						cell.SetVisualsToUndetermined();
					}
				}

				UIView.Animate(1, () => {
					this.View.LayoutIfNeeded();
				});

				var myUniqueCyclers = myTeam.Where(mt => !Cyclers.Any(t => t.Id == mt.Id)).ToList();
				var addIndexPaths = new List<NSIndexPath>();
				foreach(var cycler in myUniqueCyclers) {
					cycler.Origin = TeamOrigin.Mine;
					int theirsRankedHeigherCount = Cyclers.Where(t => t.Id < cycler.Id).Count();
					Cyclers.Insert(theirsRankedHeigherCount, cycler);
					addIndexPaths.Add(NSIndexPath.FromItemSection(theirsRankedHeigherCount, 0));
				}

				TeamCyclersTable.InsertRows(addIndexPaths.ToArray(), UITableViewRowAnimation.None);
			}
		}

		#region implemented abstract members of UITableViewDataSource

		[Export ("tableView:cellForRowAtIndexPath:")]
		public UITableViewCell GetCell (UITableView tableView, NSIndexPath indexPath)
		{
			var cell = tableView.DequeueReusableCell("CyclerCell") as CyclerTableCell;
			cell.CyclerName = Cyclers [(int)indexPath.Item].Name;
			cell.CyclerScore = string.Format("Score: {0}", Cyclers [(int)indexPath.Item].Score);
			NSData flagImageData = NSData.FromArray (DataSource.GetResource(Cyclers [(int)indexPath.Item].CountryFlagUrl));
			cell.CountryFlagImage = UIImage.LoadFromData (flagImageData, 1);

			if(Cyclers[(int)indexPath.Item].Origin == TeamOrigin.Common)
			{
				cell.SetVisualsToCommon();
			}
			else if(Cyclers[(int)indexPath.Item].Origin == TeamOrigin.Theirs)
			{
				cell.SetVisualsToTheirs();
			}
			else if(Cyclers[(int)indexPath.Item].Origin == TeamOrigin.Mine)
			{
				cell.SetVisualsToMine();
			}
			else if(Cyclers[(int)indexPath.Item].Origin == TeamOrigin.Undetermined)
			{
				cell.SetVisualsToUndetermined();
			}

			return cell;
		}

		[Export("tableView:numberOfRowsInSection:")]
		public nint RowsInSection (UITableView tableView, nint section)
		{
			return Cyclers.Count;
		}

		[Export("tableView:willDisplayCell:forRowAtIndexPath:")]
		public void WillDisplayCell (UITableView tableView, UITableViewCell cell, NSIndexPath indexPath)
		{
			if (m_isInModeCompare && Cyclers[(int)indexPath.Item].Origin == TeamOrigin.Mine) {

				(cell as CyclerTableCell).SetVisualsToOutsideRight (TeamCyclersTable.Bounds.Width);
				cell.LayoutIfNeeded();

				(cell as CyclerTableCell).SetVisualsToMine ();

				UIView.Animate(1, () => {
					cell.LayoutIfNeeded();
				});
			}
		}

		#endregion

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			// Perform any additional setup after loading the view, typically from a nib.

			this.NavigationController.NavigationBar.Translucent = true;
			this.NavigationController.NavigationBar.SetBackgroundImage (new UIImage (), UIBarMetrics.Default);
			this.NavigationController.NavigationBar.ShadowImage = new UIImage();
			this.NavigationController.NavigationBar.BackgroundColor = new UIColor(0.0f, 0.0f, 0.0f, 0.0f);

			//this.EdgesForExtendedLayout = UIRectEdge.All;

			HeaderHeightConstraint.Constant = HeaderHeight;
			TeamOwnerHolderHeightConstraint.Constant = TeamOwnerHolderHeight;
			TeamRankingGraphHolderHeightConstraint.Constant = 0;

			TeamCyclersTable.ContentInset = new UIEdgeInsets (HeaderHeight + TeamOwnerHolderHeight, 0, 0, 0);
			TeamCyclersTable.ContentOffset = new CGPoint (0, -(HeaderHeight + TeamOwnerHolderHeight));
			TeamCyclersTable.ScrollIndicatorInsets = new UIEdgeInsets (HeaderHeight + TeamOwnerHolderHeight, 0, 0, 0);

			this.View.LayoutIfNeeded();

			Cyclers = DataSource.GetTeamCyclers (Team.Id);
			Results = DataSource.GetTeamResults (Team.Id);

			NSData teamImageData = NSData.FromArray(this.Team.TeamImageData);
			TeamImageView.Image = UIImage.LoadFromData (teamImageData, 1);

			CAGradientLayer transparencyLayer = new CAGradientLayer ();
			transparencyLayer.Frame = TeamImageView.Frame;
			transparencyLayer.Colors = new CGColor[] { UIColor.Black.CGColor, UIColor.Clear.CGColor }; //[NSArray arrayWithObjects:(id)[UIColor whiteColor].CGColor, (id)[UIColor clearColor].CGColor, nil];
			transparencyLayer.StartPoint = new CGPoint(1.0f, 2.0f);
			transparencyLayer.EndPoint = new CGPoint(1.0f, -1.0f);
			TeamImageView.Layer.AddSublayer (transparencyLayer);

			RankingButton.Layer.CornerRadius = RankingButton.Frame.Height/2;
			RankingButton.Layer.BorderWidth = 1;
			RankingButton.Layer.BorderColor = RankingButton.TitleColor(UIControlState.Normal).CGColor;

			FollowButton.Layer.CornerRadius = FollowButton.Frame.Height/2;
			FollowButton.Layer.BorderWidth = 1;
			FollowButton.Layer.BorderColor = FollowButton.TitleColor(UIControlState.Normal).CGColor;

			OwnerNameLabel.Text = Team.OwnerName;
			OwnerImageView.Layer.CornerRadius = 5;
			NSData ownerImageData = NSData.FromArray (DataSource.GetResource(this.Team.OwnerAvatarUrl));
			OwnerImageView.Image = UIImage.LoadFromData (ownerImageData, 1);

			TeamCyclersTable.WeakDelegate = this;
			TeamCyclersTable.WeakDataSource = this;

		}
	}
}

