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
			
			return m_configDocument.Element("data_url").Element(key).Value;;
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
					//return doc.Element("config").Element("google-api-key").Value;
				}

			}

//			var type = this.GetType();
//			var resource = type.Namespace + "." +
//				Device.OnPlatform("iOS", "Droid", "WinPhone") + ".config.xml";
//			using (var stream = type.Assembly.GetManifestResourceStream(resource))
//			using (var reader = new StreamReader(stream)) {
//				var doc = XDocument.Parse(reader.ReadToEnd());
//				return doc.Element("config").Element("google-api-key").Value;
//			}

			m_isLoaded = true;
		}
	}
}

