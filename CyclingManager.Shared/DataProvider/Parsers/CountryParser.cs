using CyclingManager.Shared.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CyclingManager.Shared.DataProvider.Parsers {
    public class CountryParser : AbstractParser<Country> {

        public override List<Country> Parse(List<string> textLines) {
            var result = new List<Country>();

            foreach(var line in textLines.Skip(2)) {
                result.Add(ParseLine(line));
            }

            return result;
        }

        public Country ParseLine(string line) {
            var components = line.Split(';');

            var country = new Country();

            country.Id = GetInteger(components[0]);
            country.Name = GetString(components[1]);

            return country;
        }
    }
}
