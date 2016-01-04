using System;
using UIKit;
using CoreGraphics;
using Foundation;
using System.ComponentModel;
using System.Collections.Generic;
using CoreAnimation;
using ObjCRuntime;
using System.Diagnostics;

namespace CyclingManager
{
	[Register("ScoreView"), DesignTimeVisible(true)]
	public class ScoreView : UIScrollView
	{
		private List<int> graphPointList = new List<int>();

		private nfloat spacing = 20.0f;

		private UIColor color;
		private List<CGPath> pointStartPathList = new List<CGPath> ();
		private List<CGPath> pointEndPathList = new List<CGPath> ();
		private List<CAShapeLayer> pointLayerList = new List<CAShapeLayer> ();

		public ScoreView (IntPtr handle) : base (handle)
		{
			Init (UIColor.Green);
		}

		public ScoreView ()
		{
			Init (UIColor.Green);
		}

		private void Init(UIColor color) 
		{
			this.color = color;

			this.BackgroundColor = UIColor.Clear;
			this.graphPointList.AddRange (new int[]{ 10, 20, 5, 25, 15, 20, 30, 20, 5, 25, 15, 20, 30, 20, 5, 25, 15, 20, 30, 20, 5, 25, 15, 20, 30 });
			//Configure ();
			//ShowData ();
		}

		public void Configure() 
		{
			nfloat startXPos = spacing / 2;
			nfloat offset = spacing;

			CGSize graphSize = new CGSize (spacing * graphPointList.Count, this.Frame.Height);
			this.ContentSize = graphSize;

			foreach (var graphPoint in graphPointList) {
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

				startXPos += offset;
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

