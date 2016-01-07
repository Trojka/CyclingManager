using System;
using CyclingManager.Shared.DomainModel;
using System.Collections.Generic;
using CyclingManager.Shared.Common;
using System.IO;
using System.Reflection;
using System.Linq;

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

		public static List<Competition> Competitions()
		{
			var competitions = new List<Competition> ();

			for (int i = 0; i < competitionColors.Count; i++) {
				competitions.Add (new Competition (){ Id = i, Name = "Wedstrijd " + i, CompetitionColor = competitionColors[i]});
			}

			return competitions;
		}

		static List<Team> teamList = new Team[] {
			new Team (){ Id = 0, Name = "QuickStep", OwnerName = "Meneer Laminaat", Score = 2000, Results = GetTeamResults(0), TeamColor = Color.FromRGB(85, 0, 255), ImageData = GetResource("CyclingManager.Shared.Resources.etixxquickstep.png")},
			new Team (){ Id = 1, Name = "Lotto Soudal", OwnerName = "The Gambler", Score = 1000, TeamColor = Color.FromRGB(170, 0, 170), ImageData = GetResource("CyclingManager.Shared.Resources.lottosoudal.png")},
			new Team (){ Id = 2, Name = "Team Sky", OwnerName = "Froomey", Score = 500, TeamColor = Color.FromRGB(85, 170, 85), ImageData = GetResource("CyclingManager.Shared.Resources.skyprocycling.png")},
			new Team (){ Id = 3, Name = "Europcar", OwnerName = "Jacky Ickx", Score = 250, TeamColor = Color.FromRGB(0, 85, 0), ImageData = GetResource("CyclingManager.Shared.Resources.europcar.png")},
			new Team (){ Id = 4, Name = "Katusha", OwnerName = "Vladimir Poetin", Score = 125, TeamColor = Color.FromRGB(255, 170, 0), ImageData = GetResource("CyclingManager.Shared.Resources.katusha.png")},
			new Team (){ Id = 5, Name = "Trek Factory Racing", OwnerName = "Tja, wie eigenljk?", Score = 62, TeamColor = Color.FromRGB(255, 255, 0), ImageData = GetResource("CyclingManager.Shared.Resources.treckfactoryracing.png")}
		}.ToList();

		public static List<Team> Teams()
		{
			return teamList;
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

		public static List<CompetitionResult> GetTeamResults(int teamId)
		{
			List<CompetitionResult> results = new CompetitionResult[]{
				new CompetitionResult(){ Name = "Omloop Het Nieuwblad", Score = 100 },
				new CompetitionResult(){ Name = "Gent-Wevelgem", Score = 130 },
				new CompetitionResult(){ Name = "Ronde van Vlaanderen", Score = 150 }
			}.ToList();

			return results;
		}
	}
}

