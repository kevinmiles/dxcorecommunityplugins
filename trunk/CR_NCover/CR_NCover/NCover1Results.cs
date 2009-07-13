using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace CR_NCover
{
	public class NCover1Results
	{
		public static IEnumerable<CoverageResult> GetStatistics(XContainer xdoc, string source)
		{
			var allPoints = xdoc.Descendants("seqpnt");
			var statistics =
				allPoints.Where(s => Path.GetFullPath(s.Attributes("document").FirstOrDefault().Value).ToLower() == source.ToLower());
			return
				TransformToCoverageResults(statistics);
		}
		private static IEnumerable<CoverageResult> TransformToCoverageResults(IEnumerable<System.Xml.Linq.XElement> statistics)
		{
			return statistics.Select(
								s =>
								new CoverageResult
									{
										Line = s.AttributeAsInt("line"),
										LineEnd = s.AttributeAsInt("endline"),
										Column = s.AttributeAsInt("column"),
										ColumnEnd = s.AttributeAsInt("endcolumn"),
										VisitCount = s.AttributeAsInt("visitcount"),
									});
		}
	}
}