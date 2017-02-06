using CyclingManager.Shared.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CyclingManager.Shared.Common;

namespace CyclingManager.Shared.DataProvider.Parsers {
    public class TeamParser : AbstractParser<Team> {

        public override List<Team> Parse(List<string> textLines) {
            var result = new List<Team>();

            foreach (var line in textLines.Skip(2)) {
                var o = ParseLine(line);
                if (o == null)
                    continue;
                
                result.Add(o);
            }

            return result;
        }

        public Team ParseLine(string line) {
            var components = line.Split(';');

            if (components[1] != "16")
                return null;

            var team = new Team();

            team.Id = GetInteger(components[0]);
            team.OwnerId = GetInteger(components[2]);
            team.Name = GetString(components[3]);

            team.TeamImageData = GetResource("CyclingManager.Shared.Resources.bicycles4.png");
            team.OwnerAvatarUrl = "CyclingManager.Shared.Resources.user82.png";
            team.TeamColor = Color.FromRGB(85, 0, 255);

            return team;
        }
    }
}
