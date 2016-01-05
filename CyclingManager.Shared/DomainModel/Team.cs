﻿using System;
using System.Collections.Generic;
using CyclingManager.Shared.Common;

namespace CyclingManager.Shared.DomainModel
{
	public class Team
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public Color TeamColor { get; set; }
		public List<Cycler> Cyclists { get; set; }
		public List<CompetitionResult> Results { get; set; }

		public byte[] ImageData { get; set; }

		public int Score { get; set; }
	}
}
