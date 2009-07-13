using System;
using System.Reflection;
using System.Text;
using ApprovalTests;
using ApprovalTests.Writers;
using NUnit.Framework;

namespace CR_NCover.Tests.ResultParserSpecs
{
	[TestFixture]
	public class ResultParserTests
	{
		private static EnumerableWriter.CustomFormatter<CoverageResult> CoverageResultsFormatter
		{
			get
			{
				return result =>
				       	{
				       		Type type = result.GetType();
				       		var sb = new StringBuilder();
				       		foreach (PropertyInfo property in type.GetProperties(BindingFlags.Public))
				       		{
				       			sb.AppendLine(property.Name + ": " + property.GetValue(result, null).ToString());
				       		}
				       		return sb.ToString();
				       	};
			}
		}

		[Test]
		public void InvalidNCover1ResultFileMissing()
		{
			Approvals.Approve(ResultParser.Parse("Invalid-1.xml", @"c:\foo.cs"), CoverageResultsFormatter);
		}

		[Test]
		public void InvalidNCover3ResultFileMissing()
		{
			Approvals.Approve(ResultParser.Parse("Invalid-3.xml", @"c:\foo.cs"), CoverageResultsFormatter);
		}

		[Test]
		public void InvalidXmlResultFile()
		{
			Approvals.Approve(ResultParser.Parse(@"notvalidxml.xml", @"c:\foo.cs"), CoverageResultsFormatter);
		}

		[Test]
		public void MissingResultFile()
		{
			Approvals.Approve(ResultParser.Parse("Missing.xml", @"c:\foo.cs"), CoverageResultsFormatter);
		}

		[Test]
		public void ParseNCover1()
		{
			Approvals.Approve(ResultParser.Parse("NCover-1.xml", @"c:\foo-1.cs"), CoverageResultsFormatter);
		}

		[Test]
		public void ParseNCover3()
		{
			Approvals.Approve(ResultParser.Parse("NCover-3.xml", @"c:\foo-3.cs"), CoverageResultsFormatter);
		}
	}
}