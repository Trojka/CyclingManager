using System;

namespace CyclingManager.Shared.Common
{
	public class Color
	{
		public int Red { get; set; }
		public int Green { get; set; }
		public int Blue { get; set; }

		public static Color FromRGB(int red, int green, int blue) {
			return new Color (){ Red = red, Green = green, Blue = blue };
		}
	}
}

