using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Linq;

namespace CR_NCover
{
	public class ResultParser
	{
		public static IList<CoverageResult> Parse(string file, string sourceFile)
		{
			var results = new List<CoverageResult>();

			if (!File.Exists(file))
				return results;

			string tempFile = CopyResults(file);
			try
			{
				XDocument document = XDocument.Load(tempFile);
				switch (GetVersion(document).Major)
				{
					case 3:
						results.AddRange(NCover3Result.GetStatistics(document, sourceFile));
						break;
					default:
						results.AddRange(NCover1Results.GetStatistics(document, sourceFile));
						break;
				}
			}
			catch (XmlException)
			{
			}
			File.Delete(tempFile);

			return results;
		}

		private static string CopyResults(string file)
		{
			string t = Path.GetTempFileName();
			File.Copy(file, t, true);
			return t;
		}

		private static Version GetVersion(XContainer document)
		{
			XAttribute driverVersion = document.Elements("coverage").Select(e => e.Attribute("driverVersion")).FirstOrDefault();
			return new Version(driverVersion.Value);
		}
	}
}
