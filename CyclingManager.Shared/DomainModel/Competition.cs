using System;
using System.Collections.Generic;
using CyclingManager.Shared.Common;

namespace CyclingManager.Shared.DomainModel
{
	public class Competition
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public Color CompetitionColor { get; set; }
		public Cycler Stage1 { get; set; }
		public Cycler Stage2 { get; set; }
		public Cycler Stage3 { get; set; }
	}
}

