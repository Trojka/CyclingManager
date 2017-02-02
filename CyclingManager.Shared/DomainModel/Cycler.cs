using System;
using System.Collections.Generic;

namespace CyclingManager.Shared.DomainModel
{
	public enum TeamOrigin
	{
		Common,
		Mine,
		Theirs,
		Undetermined
	}

	public class Cycler
	{
		public int Id { get; set; }
		public string Name { get; set; }
        public int CountryId { get; set; }

		public string CountryFlagUrl { get; set; }

		public int Score { get; set; }

		public TeamOrigin Origin { get; set; } = TeamOrigin.Undetermined;

	}

}

