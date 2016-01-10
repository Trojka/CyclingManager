using System;
using UIKit;
using CyclingManager.Shared.Common;

namespace CyclingManager
{
	public static class ColorExtensions
	{
		public static UIColor ToUIColor(this Color color) {
			return UIColor.FromRGB(color.Red, color.Green, color.Blue);
		}
	}
}

