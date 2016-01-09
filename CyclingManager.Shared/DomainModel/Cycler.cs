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
		public string CountryFlagUrl { get; set; }

		public int Score { get; set; }

		public TeamOrigin Origin { get; set; } = TeamOrigin.Undetermined;

		public static List<Cycler> Team1() {

			var team1 = new List<Cycler> ();

			for (int i = 0; i < 20; i = i+2) {
				team1.Add (new Cycler (){ Id = i, Name = "Renner " + i, Score = i*100, CountryFlagUrl = "CyclingManager.Shared.Resources.flag_france.png" });
			}

			return team1;
		}

		public static List<Cycler> MyTeam() {

			var myTeam = new List<Cycler> ();

			myTeam.Add (new Cycler (){ Id = 2, Name = "Renner 2", Score = 100, CountryFlagUrl = "CyclingManager.Shared.Resources.flag_france.png" });
			myTeam.Add (new Cycler (){ Id = 3, Name = "Renner 3", Score = 200, CountryFlagUrl = "CyclingManager.Shared.Resources.flag_france.png" });
			myTeam.Add (new Cycler (){ Id = 4, Name = "Renner 4", Score = 300, CountryFlagUrl = "CyclingManager.Shared.Resources.flag_france.png" });

			return myTeam;
		}

	}

}

