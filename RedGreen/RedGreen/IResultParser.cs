using System;
using System.IO;
using System.Text;

namespace RedGreen
{
	public interface IResultParser
	{
		void ReadNextTextResult(TextReader reader, ref string line, StringBuilder result);

		RedGreen.TestResult ParseTest(string rawResult);

		bool IsTestResult(string rawResult);

		SummaryResult ParseSummary(string rawResult);
	}
}
