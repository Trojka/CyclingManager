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
		//private List<int> graphPointList = new List<int>();

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
			//this.graphPointList.AddRange (new int[]{ 10, 20, 5, 25, 15, 20, 30, 20, 5, 25, 15, 20, 30, 20, 5, 25, 15, 20, 30, 20, 5, 25, 15, 20, 30 });
			//Configure ();
			//ShowData ();
		}

		private List<CompetitionResult> Results {
			get;
			set;
		}

		public void Configure() 
		{
			nfloat offset = (nfloat)Math.Max(minimalSpacing, this.Frame.Width / Results.Count / 2);
			nfloat startXPos = (this.Frame.Width / 2);

			CGSize graphSize = new CGSize (minimalSpacing * Results.Count, this.Frame.Height);
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
				pointLayer.StrokeColor = UIColor.Yellow.CGColor;
				pointLayer.FillColor = UIColor.Yellow.CGColor;

				pointLayerList.Add (pointLayer);

				this.Layer.AddSublayer (pointLayer);

				startXPos -= offset;
			}
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

