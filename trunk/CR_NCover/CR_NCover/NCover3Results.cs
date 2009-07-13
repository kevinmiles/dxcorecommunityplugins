using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace CR_NCover
{
	public class NCover3Result
	{
		public static IEnumerable<CoverageResult> GetStatistics(XContainer document, string source)
		{
			XElement doc =
				document.Descendants("doc").Where(
					d => Path.GetFullPath(d.Attributes("url").FirstOrDefault().Value).ToLower() == Path.GetFullPath(source).ToLower()).
					FirstOrDefault();
			if(doc == null)
				return new List<CoverageResult>();

			string docid = doc.Attributes("id").FirstOrDefault().Value;

			IEnumerable<XElement> statistics = document.Descendants("seqpnt").Where(s =>
			                                                                        	{
			                                                                        		string docAttributeId =
			                                                                        			s.Attributes("doc").FirstOrDefault().Value;
			                                                                        		return docAttributeId == docid;
			                                                                        	});
			return TransformToCoverageResults(statistics);
		}

		private static IEnumerable<CoverageResult> TransformToCoverageResults(IEnumerable<XElement> statistics)
		{
			return
				statistics.Select(
					s =>
					new CoverageResult
						{
							Line = s.AttributeAsInt("l"),
							LineEnd = s.AttributeAsInt("el"),
							Column = s.AttributeAsInt("c"),
							ColumnEnd = s.AttributeAsInt("ec"),
							VisitCount = s.AttributeAsInt("vc"),
						});
		}
	}
}