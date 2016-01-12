using System;
using UIKit;
using CoreGraphics;
using Foundation;
using System.ComponentModel;
using System.Collections.Generic;
using CoreAnimation;
using ObjCRuntime;
using System.Diagnostics;
using CyclingManager.Shared;
using System.Linq;
using System.Drawing;

namespace CyclingManager
{
	[Register("ScoreView"), DesignTimeVisible(true)]
	public class ScoreView : UIScrollView
	{
		private CGColor graphColor = UIColor.Black.CGColor;

		private nfloat minimalSpacing = 20.0f;
		private double scoreAnimationDuration = 0.5;

		private List<CGPath> pointStartPathList = new List<CGPath> ();
		private List<CGPath> pointEndPathList = new List<CGPath> ();
		private List<CAShapeLayer> pointLayerList = new List<CAShapeLayer> ();
		private List<CAAnimation> animationList = new List<CAAnimation> ();
		private int finishedAnimationCount = 0;

		public ScoreView (IntPtr handle) : base (handle)
		{
			Init (UIColor.Green, null);
		}

		public ScoreView (List<CompetitionResult> results)
		{
			Init (UIColor.Green, results);
		}

		private void Init(UIColor color, List<CompetitionResult> results) 
		{
			Results = results;

			this.BackgroundColor = UIColor.Clear;
		}

		private List<CompetitionResult> Results {
			get;
			set;
		}

		private nfloat TopPadding {
			get;
			set;
		}

		private nfloat Offset {
			get;
			set;
		}

		private nfloat StartXPos {
			get;
			set;
		}

		private CGSize ScoreBubleSize {
			get;
			set;
		}

		private CGSize ScoreSize {
			get;
			set;
		}

		private nfloat EndYPos(int score) {
			int maxScore = Results.Max (r => r.Score);
			return ScoreBubleSize.Height + (this.Frame.Height - TopPadding - ScoreBubleSize.Height) / maxScore * score;
		}

		public nfloat GetCompetitionOffsetClosestTo(nfloat targetOffset) {
			int index = (int)((targetOffset - (this.Frame.Width / 2)) / Offset);
			return (this.Frame.Width / 2) + index * Offset;
		}

		public CompetitionResult GetCompetitionAtCurrentOffset (){
			int index = (int)Math.Round(this.ContentOffset.X / Offset);
			return Results [index];
		}

		public void Configure() 
		{
			TopPadding = (nfloat)Math.Min (10, this.Frame.Height * 0.1);
			
			var font = UIFont.FromName ("System", 10);
			NSString nsString = new NSString (Results.Max (r => r.Score).ToString());
			UIStringAttributes attribs = new UIStringAttributes { Font = font };
			var textSize = nsString.GetSizeUsingAttributes (attribs);		
			ScoreSize = textSize;
			nfloat squareSide = (nfloat)(Math.Max (ScoreSize.Width, ScoreSize.Height) / Math.Cos (Math.PI / 4));
			ScoreBubleSize = new CGSize(squareSide, squareSide);
			
			Offset = (nfloat)Math.Max(minimalSpacing, this.Frame.Width / Results.Count / 2);
			StartXPos = (this.Frame.Width / 2) + (Offset * (Results.Count-1));

			CGSize graphSize = new CGSize (this.Frame.Width + (Offset * (Results.Count-1)), this.Frame.Height);
			this.ContentSize = graphSize;


			nfloat nextPos = StartXPos;
			foreach (var result in Results.OrderByDescending(c => c.ExecutionDate)) {
				nfloat graphPoint = this.Frame.Height - EndYPos(result.Score);

				var pointStartPath = new CGPath ();
				pointStartPath.MoveToPoint (nextPos, this.Frame.Height);
				pointStartPath.AddLineToPoint (nextPos, this.Frame.Height);

				pointStartPathList.Add (pointStartPath); 

				var pointEndPath = new CGPath ();
				pointEndPath.MoveToPoint (nextPos, this.Frame.Height);
				pointEndPath.AddLineToPoint (nextPos, graphPoint + ScoreBubleSize.Height);

				pointEndPathList.Add (pointEndPath); 

				var pointLayer = new CAShapeLayer ();

				pointLayer.Path = pointStartPath;
				pointLayer.StrokeColor = graphColor;
				pointLayer.FillColor = graphColor;

				pointLayerList.Add (pointLayer);

				this.Layer.AddSublayer (pointLayer);

				nextPos -= Offset;
			}

			this.ContentOffset = new CGPoint ((Offset * (Results.Count - 1)), 0);
		}


		public void ShowData() {
			int index = 0;
			foreach (var pointLayer in pointLayerList) {
				
				pointLayer.Path = pointEndPathList[index];

				var animate = CABasicAnimation.FromKeyPath ("path");
				animate.Duration = scoreAnimationDuration;
				animate.From = Runtime.GetNSObject (pointStartPathList[index].Handle);

				animate.WeakDelegate = this;

				animationList.Add (animate);

				index++;
			}

			finishedAnimationCount = 0;
			index = 0;
			foreach (var pointLayer in pointLayerList) {
				pointLayer.AddAnimation (animationList[index], "path");

				index++;
			}

		}

		#region implementation of UIScrollViewDelegate

		[Export ("animationDidStop:finished:")]
		public void DidStop(CAAnimation theAnimation, bool finished)
		{
			finishedAnimationCount++;
			if (finishedAnimationCount != animationList.Count)
				return;
			
			nfloat nextPos = StartXPos;
			foreach (var result in Results.OrderByDescending(c => c.ExecutionDate)) {
				nfloat graphPoint = this.Frame.Height - EndYPos(result.Score);
				string scoreText = result.Score.ToString ();

				var scorePath = new CGPath ();
				scorePath.AddEllipseInRect ( new CGRect(new CGPoint(nextPos - ScoreBubleSize.Width/2, graphPoint), ScoreBubleSize));

				var pointLayer = new CAShapeLayer ();
				pointLayer.Path = scorePath;
				pointLayer.StrokeColor = graphColor;
				pointLayer.FillColor = UIColor.Clear.CGColor;

				this.Layer.AddSublayer (pointLayer);

				var scoreLayer = new CATextLayer ();
				scoreLayer.SetFont ("System");
				scoreLayer.FontSize = 10;
				scoreLayer.Frame = new CGRect (nextPos - ScoreSize.Width/2, 
					graphPoint + ScoreBubleSize.Height/2 - ScoreSize.Height/2, 
					ScoreSize.Width, 
					ScoreSize.Height);
				scoreLayer.AlignmentMode = CATextLayer.AlignmentCenter;
				scoreLayer.String = scoreText;
				scoreLayer.ForegroundColor = graphColor;

				this.Layer.AddSublayer (scoreLayer);

				nextPos -= Offset;
			}
		}

		#endregion

	}
}

