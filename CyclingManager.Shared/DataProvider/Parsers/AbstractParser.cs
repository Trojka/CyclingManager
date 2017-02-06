using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.IO;

namespace CyclingManager.Shared.DataProvider.Parsers {
    public abstract class AbstractParser<T> {

        public abstract List<T> Parse(List<string> textLines);

        public int GetInteger(string value) {
            return int.Parse(value);
        }

        public string GetString(string value) {
            return value;
        }

        public static byte[] GetResource(string resourceName) {

            var assembly = typeof(AbstractParser<T>).GetTypeInfo().Assembly;

            byte[] buffer;
            using (Stream s = assembly.GetManifestResourceStream(resourceName)) {
                long length = s.Length;
                buffer = new byte[length];
                s.Read(buffer, 0, (int)length);
            }

            return buffer;
        }
    }
}
