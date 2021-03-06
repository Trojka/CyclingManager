﻿using System;
using CyclingManager.Shared.DomainModel;
using System.Collections.Generic;
using CyclingManager.Shared.Common;
using System.IO;
using System.Reflection;
using System.Linq;
using System.Net;
using System.Text;
using CyclingManager.Shared.DataProvider.Parsers;
using System.Threading.Tasks;

namespace CyclingManager.Shared
{
	public class DataSource
	{	
		static List<Color> competitionColors = new List<Color>(new Color[] {
			Color.FromRGB(85, 0, 255),
			Color.FromRGB(170, 0, 170),
			Color.FromRGB(85, 170, 85),
			Color.FromRGB(0, 85, 0),
			Color.FromRGB(255, 170, 0),
			Color.FromRGB(255, 255, 0),
			Color.FromRGB(255, 85, 0),
			Color.FromRGB(0, 85, 85),
			Color.FromRGB(0, 85, 255),
			Color.FromRGB(170, 170, 255),
			Color.FromRGB(85, 0, 0),
			Color.FromRGB(170, 85, 85),
			Color.FromRGB(170, 255, 0),
			Color.FromRGB(85, 170, 255),
			Color.FromRGB(0, 170, 170)
		});

        public static Task<List<Country>> GetCountriesAsync() {
            Configuration config = new Configuration();
            string countryUrl = config.CountryUrl;

            return GetObjectListAsync<Country>(countryUrl, new CountryParser());

        }

        public static Task<List<Team>> GetTeamsAsync() {
            Configuration config = new Configuration();
            string teamUrl = config.TeamUrl;

            return GetObjectListAsync<Team>(teamUrl, new TeamParser());
        }

        private static async Task<List<T>> GetObjectListAsync<T>(string dataUrl, AbstractParser<T> parser) {
            var request = (HttpWebRequest)WebRequest.Create(dataUrl);
            request.Method = "GET";

            var response = await Task.Factory.FromAsync((callback, stateObject) => request.BeginGetResponse(callback, request), request.EndGetResponse, null);

            using (var reader = new System.IO.StreamReader(response.GetResponseStream())) {
                string responseText = reader.ReadToEnd();

                var lines = responseText.Split(new string[] { System.Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries).ToList();

                var objectList = parser.Parse(lines);
                return objectList;
            }
        }

        public static List<Competition> Competitions()
		{
			var competitions = new List<Competition> ();

			for (int i = 0; i < competitionColors.Count; i++) {
				competitions.Add (new Competition (){ 
					Id = i, 
					Name = "Wedstrijd " + i, 
					Date = (new DateTime(2016 + (i/12), (i%12)+1, 1)).AddMonths(-2), 
					CompetitionColor = competitionColors[i],
					Stage1 = new Cycler (){ Id = 2, Name = "Renner 20", Score = 100, CountryFlagUrl = "CyclingManager.Shared.Resources.flag_france.png" },
					Stage2 = new Cycler (){ Id = 2, Name = "Renner 30", Score = 200, CountryFlagUrl = "CyclingManager.Shared.Resources.flag_france.png" },
					Stage3 = new Cycler (){ Id = 2, Name = "Renner 40", Score = 300, CountryFlagUrl = "CyclingManager.Shared.Resources.flag_france.png" }
				});
			}

			return competitions;
		}

		static List<Team> teamList = new Team[] {
			new Team (){ Id = 0, Name = "QuickStep", OwnerName = "Meneer Laminaat", OwnerAvatarUrl="CyclingManager.Shared.Resources.user82.png", Score = 2000, TeamColor = Color.FromRGB(85, 0, 255), TeamImageData = GetResource("CyclingManager.Shared.Resources.etixxquickstep.png")},
			new Team (){ Id = 1, Name = "Lotto Soudal", OwnerName = "The Gambler", OwnerAvatarUrl="CyclingManager.Shared.Resources.user82.png", Score = 1000, TeamColor = Color.FromRGB(170, 0, 170), TeamImageData = GetResource("CyclingManager.Shared.Resources.lottosoudal.png")},
			new Team (){ Id = 2, Name = "Team Sky", OwnerName = "Froomey", OwnerAvatarUrl="CyclingManager.Shared.Resources.user82.png", Score = 500, TeamColor = Color.FromRGB(85, 170, 85), TeamImageData = GetResource("CyclingManager.Shared.Resources.skyprocycling.png")},
			new Team (){ Id = 3, Name = "Europcar", OwnerName = "Jacky Ickx", OwnerAvatarUrl="CyclingManager.Shared.Resources.user82.png", Score = 250, TeamColor = Color.FromRGB(0, 85, 0), TeamImageData = GetResource("CyclingManager.Shared.Resources.bicycles4.png")},
			new Team (){ Id = 4, Name = "Katusha", OwnerName = "Vladimir Poetin", OwnerAvatarUrl="CyclingManager.Shared.Resources.user82.png", Score = 125, TeamColor = Color.FromRGB(255, 170, 0), TeamImageData = GetResource("CyclingManager.Shared.Resources.katusha.png")},
			new Team (){ Id = 5, Name = "Trek Factory Racing", OwnerName = "Tja, wie eigenljk?", OwnerAvatarUrl="CyclingManager.Shared.Resources.user82.png", Score = 62, TeamColor = Color.FromRGB(255, 255, 0), TeamImageData = GetResource("CyclingManager.Shared.Resources.treckfactoryracing.png")}
		}.ToList();

		public static List<Cycler> MyTeam() {

			var myTeam = new List<Cycler> ();

			myTeam.Add (new Cycler (){ Id = 2, Name = "Renner 2", Score = 100, CountryFlagUrl = "CyclingManager.Shared.Resources.flag_france.png" });
			myTeam.Add (new Cycler (){ Id = 3, Name = "Renner 3", Score = 200, CountryFlagUrl = "CyclingManager.Shared.Resources.flag_france.png" });
			myTeam.Add (new Cycler (){ Id = 4, Name = "Renner 4", Score = 300, CountryFlagUrl = "CyclingManager.Shared.Resources.flag_france.png" });

			return myTeam;
		}

		public static byte[] GetResource(string resourceName) {

			var assembly = typeof(DataSource).GetTypeInfo().Assembly; 

			byte[] buffer;
			using (Stream s = assembly.GetManifestResourceStream(resourceName))
			{
				long length = s.Length;
				buffer = new byte[length];
				s.Read(buffer, 0, (int)length);
			}

			return buffer;
		}

		public static List<Cycler> GetTeamCyclers(int teamId) {

			var team1 = new List<Cycler> ();

			for (int i = 0; i < 20; i = i+2) {
				team1.Add (new Cycler (){ Id = i, Name = "Renner " + i, Score = i*100, CountryFlagUrl = "CyclingManager.Shared.Resources.flag_france.png" });
			}

			return team1;
		}

		public static List<CompetitionResult> GetTeamResults(int teamId)
		{
			List<CompetitionResult> results = new CompetitionResult[]{
				new CompetitionResult(){ Name = "Omloop Het Nieuwblad", ExecutionDate = new DateTime(2016, 1, 1), Score = 110 },
				new CompetitionResult(){ Name = "Gent-Wevelgem", ExecutionDate = new DateTime(2016, 1, 2), Score = 300 },
				new CompetitionResult(){ Name = "Ronde van Vlaanderen", ExecutionDate = new DateTime(2016, 1, 3), Score = 9999 }
			}.ToList();

			return results;
		}

		public static List<Team> GetCompetitionTeamResults(int competitionId)
		{
			List<Team> results = new Team[] {
				new Team (){ Id = 0, Name = "QuickStep", OwnerName = "Meneer Laminaat", OwnerAvatarUrl="CyclingManager.Shared.Resources.user82.png", Score = 2000, TeamColor = Color.FromRGB(85, 0, 255), TeamImageData = GetResource("CyclingManager.Shared.Resources.etixxquickstep.png")},
				new Team (){ Id = 1, Name = "Lotto Soudal", OwnerName = "The Gambler", OwnerAvatarUrl="CyclingManager.Shared.Resources.user82.png", Score = 1000, TeamColor = Color.FromRGB(170, 0, 170), TeamImageData = GetResource("CyclingManager.Shared.Resources.lottosoudal.png")},
				new Team (){ Id = 2, Name = "Team Sky", OwnerName = "Froomey", OwnerAvatarUrl="CyclingManager.Shared.Resources.user82.png", Score = 500, TeamColor = Color.FromRGB(85, 170, 85), TeamImageData = GetResource("CyclingManager.Shared.Resources.skyprocycling.png")},
				new Team (){ Id = 3, Name = "Europcar", OwnerName = "Jacky Ickx", OwnerAvatarUrl="CyclingManager.Shared.Resources.user82.png", Score = 250, TeamColor = Color.FromRGB(0, 85, 0), TeamImageData = GetResource("CyclingManager.Shared.Resources.bicycles4.png")},
				new Team (){ Id = 4, Name = "Katusha", OwnerName = "Vladimir Poetin", OwnerAvatarUrl="CyclingManager.Shared.Resources.user82.png", Score = 125, TeamColor = Color.FromRGB(255, 170, 0), TeamImageData = GetResource("CyclingManager.Shared.Resources.katusha.png")},
				new Team (){ Id = 5, Name = "Trek Factory Racing", OwnerName = "Tja, wie eigenljk?", OwnerAvatarUrl="CyclingManager.Shared.Resources.user82.png", Score = 62, TeamColor = Color.FromRGB(255, 255, 0), TeamImageData = GetResource("CyclingManager.Shared.Resources.treckfactoryracing.png")}
			}.ToList ();

			return results;
		}

		public static List<Cycler> GetCompetitionCyclerResults(int competitionId)
		{
			var team1 = new List<Cycler> ();

			for (int i = 0; i < 20; i++) {
				team1.Add (new Cycler (){ Id = i, Name = "Renner " + i, Score = i*100, CountryFlagUrl = "CyclingManager.Shared.Resources.flag_france.png" });
			}

			return team1;
		}
	}
}

