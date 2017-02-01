using System;
using System.IO;
using System.Reflection;
using System.Linq;
using System.Xml.Linq;

namespace CyclingManager.Shared
{
	public class Configuration
	{
		static bool m_isLoaded = false;
		static XDocument m_configDocument;
		
		public string this[string key]
		{
			get
			{
				return GetValue(key);
			}
//			set
//			{
//				SetValue(key,value);
//			}
		}

		public string GetValue(string key)
		{
			if (!m_isLoaded)
				LoadConfiguration ();

            var urlElem = m_configDocument.Element("DataUrls");
            var urlList = urlElem.Elements();
            var requestedUrl = urlList.Where(u => u.Attribute("id").Value == key).FirstOrDefault();
            var urlAttr = requestedUrl.Value;

            return urlAttr;
		}

//		public void SetValue(string key, string value)
//		{
//		}

		private void LoadConfiguration() 
		{
			var assembly = typeof(Configuration).GetTypeInfo().Assembly; 

			byte[] buffer;
			using (Stream s = assembly.GetManifestResourceStream("CyclingManager.Shared.Resources.DataQueries.xml"))
			{
				using (var reader = new StreamReader(s)) {
					m_configDocument = XDocument.Parse(reader.ReadToEnd());
				}

			}

			m_isLoaded = true;
		}
	}
}

