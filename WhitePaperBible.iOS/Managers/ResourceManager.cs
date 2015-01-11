using System;
using System.Linq;
using System.Xml.Linq;
using System.Collections.Generic;

namespace WhitePaperBible.iOS.Managers
{
	public static class ResourceManager
	{
		static XDocument stringsDoc;

		static XDocument integersDoc;

		static ResourceManager ()
		{
			stringsDoc = XDocument.Load ("Strings.xml");
			integersDoc = XDocument.Load ("Integers.xml");
		}

		public static string GetString(string key)
		{
			IEnumerable<XElement> strings =
				(from el in stringsDoc.Root.Elements("string")
					where (string) el.Attribute("name") == key
					select el);

			return strings.First ().Value;
		}

		public static string[] GetStringArray(string key)
		{
			IEnumerable<XElement> strings =
				(from el in stringsDoc.Root.Elements("string-array")
					where (string) el.Attribute("name") == key
					select el);
			var values = strings.First ().Descendants ().Select (x => x.Value).ToArray ();
			for (int j = 0; j < values.Length; j++) {
				values [j] = values[j].Replace("\\'", "'");
			}
			return values;
		}

		public static int GetInteger(string key)
		{
			IEnumerable<XElement> integers =
				(from el in integersDoc.Root.Elements("integer")
					where (string) el.Attribute("name") == key
					select el);

			return int.Parse(integers.First ().Value);
		}
	}
}

