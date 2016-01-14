using System;
using System.Collections.Generic;
using CyclingManager.Shared.Common;

namespace CyclingManager.Shared.DomainModel
{
	public class Team
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string OwnerName { get; set;}
		public string OwnerAvatarUrl { get; set; }
		public Color TeamColor { get; set; }

		public byte[] TeamImageData { get; set; }
		public byte[] OwnerImageData { get; set; }

		public int Score { get; set; }
	}
}

