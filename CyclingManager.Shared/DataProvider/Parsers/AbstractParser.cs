using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CyclingManager.Shared.DataProvider.Parsers {
    public abstract class AbstractParser<T> {

        public abstract List<T> Parse(List<string> textLines);

        public int GetInteger(string value) {
            return int.Parse(value);
        }

        public string GetString(string value) {
            return value;
        }

    }
}
