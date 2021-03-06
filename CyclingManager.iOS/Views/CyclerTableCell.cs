﻿using System;
using UIKit;
using Foundation;

namespace CyclingManager
{
	[Register ("CyclerTableCell")]
	public partial class CyclerTableCell : UITableViewCell
	{
		public CyclerTableCell (IntPtr handle) : base (handle)
		{
		}

		public string CyclerName {
			get { return CyclerNameLabel.Text; }
			set { CyclerNameLabel.Text = value; }
		}

		public string CyclerScore {
			get { return CyclerScoreLabel.Text; }
			set { CyclerScoreLabel.Text = value; }
		}

		public UIImage CountryFlagImage {
			get { return CountryFlagImageView.Image; }
			set { CountryFlagImageView.Image = value; }
		}

		public void SetVisualsToUndetermined() {
			leadingConstraint.Constant = 0;
			trailingConstraint.Constant = 0;
			topConstraint.Constant = 0;
			bottomConstraint.Constant = 0;
		}

		public void SetVisualsToCommon() {
			leadingConstraint.Constant = 15;
			trailingConstraint.Constant = 15;
			topConstraint.Constant = 10;
			bottomConstraint.Constant = 10;
		}

		public void SetVisualsToTheirs() {
			leadingConstraint.Constant = 5;
			trailingConstraint.Constant = 30;
			topConstraint.Constant = 10;
			bottomConstraint.Constant = 10;
		}

		public void SetVisualsToMine() {
			leadingConstraint.Constant = 30;
			trailingConstraint.Constant = 5;
			topConstraint.Constant = 10;
			bottomConstraint.Constant = 10;
		}

		public void SetVisualsToOutsideRight(nfloat rightLimit) {
			leadingConstraint.Constant = rightLimit;
			trailingConstraint.Constant = 0;
			topConstraint.Constant = 10;
			bottomConstraint.Constant = 10;
		}
	}
}

