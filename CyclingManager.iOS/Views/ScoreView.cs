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

namespace CyclingManager
{
	[Register("ScoreView"), DesignTimeVisible(true)]
	public class ScoreView : UIScrollView
	{
		private CGColor graphColor = UIColor.Black.CGColor;

		private nfloat minimalSpacing = 20.0f;

		private List<CGPath> pointStartPathList = new List<CGPath> ();
		private List<CGPath> pointEndPathList = new List<CGPath> ();
		private List<CAShapeLayer> pointLayerList = new List<CAShapeLayer> ();

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

		private nfloat Offset {
			get;
			set;
		}

		public nfloat GetCompetitionOffsetClosestTo(nfloat targetOffset) {
			int index = (int)((targetOffset - (this.Frame.Width / 2)) / Offset);
			return (this.Frame.Width / 2) + index * Offset;
		}

		public CompetitionResult GetCompetitionAtCurrentOffset (){
			int index = (int)(this.ContentOffset.X / Offset);
			return Results [index];
		}

		public void Configure() 
		{
			Offset = (nfloat)Math.Max(minimalSpacing, this.Frame.Width / Results.Count / 2);
			nfloat startXPos = (this.Frame.Width / 2) + (Offset * (Results.Count-1));

			CGSize graphSize = new CGSize (this.Frame.Width + (Offset * (Results.Count-1)), this.Frame.Height);
			this.ContentSize = graphSize;

			int maxScore = Results.Max (r => r.Score);
			foreach (var result in Results.OrderByDescending(c => c.ExecutionDate)) {
				nfloat graphPoint = this.Frame.Height * 0.9f / maxScore * result.Score;

				var pointStartPath = new CGPath ();
				pointStartPath.MoveToPoint (startXPos, this.Frame.Height);
				pointStartPath.AddLineToPoint (startXPos, this.Frame.Height);

				pointStartPathList.Add (pointStartPath); 

				var pointEndPath = new CGPath ();
				pointEndPath.MoveToPoint (startXPos, this.Frame.Height);
				pointEndPath.AddLineToPoint (startXPos, this.Frame.Height - graphPoint);

				pointEndPathList.Add (pointEndPath); 

				var pointLayer = new CAShapeLayer ();

				pointLayer.Path = pointStartPath;
				pointLayer.StrokeColor = graphColor;
				pointLayer.FillColor = graphColor;

				pointLayerList.Add (pointLayer);

				this.Layer.AddSublayer (pointLayer);

				startXPos -= Offset;
			}

			this.ContentOffset = new CGPoint ((Offset * (Results.Count - 1)), 0);
		}


		public void ShowData() {
			int index = 0;
			foreach (var pointLayer in pointLayerList) {
				
				pointLayer.Path = pointEndPathList[index];

				var animate = CABasicAnimation.FromKeyPath ("path");
				animate.Duration = 0.5;
				animate.From = Runtime.GetNSObject (pointStartPathList[index].Handle);

				pointLayer.AddAnimation (animate, "path");

				index++;
			}
		}

	}
}

