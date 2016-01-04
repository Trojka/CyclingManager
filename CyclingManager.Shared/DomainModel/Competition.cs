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
	}
}

