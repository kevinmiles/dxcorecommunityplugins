using System;
using System.IO;
using System.Text;

namespace RedGreen
{
	public class BaseResultParser
	{
		protected internal sealed class ResultKind
		{
			public static readonly string Test = "Test";
			public static readonly string Fixture = "Fixture";
			public static readonly string Assembly = "Assembly";
			public static readonly string Framework = "Framework";
			public static readonly string Root = "Root";
		}
		protected internal sealed class Status
		{
			public const string Failed = "failed";
			public const string Error = "error";
			public const string Passed = "passed";
			public const string Skipped = "skipped";
			public const string Ignored = "ignored";
		}

		/// <remarks>Assumes that on entry "line" contains the start of a test result.</remarks>
		public void ReadNextTextResult(TextReader reader, ref string line, StringBuilder result)
		{
			do
			{
				result.AppendFormat("{0}\n", line);
				line = reader.ReadLine();
			} while (line != null && line.StartsWith("[") == false);
		}

		/// <remarks>Kind is the first word that follows the status, which is the start of the result and is encased in brackets.</remarks>
		protected static string GetResultKind(string rawResult)
		{
			int statusEnd = rawResult.IndexOf(' ');
			int kindEnd = rawResult.IndexOf(' ', statusEnd + 1);
			return rawResult.Substring(statusEnd + 1, kindEnd - statusEnd - 1);
		}

		protected bool IsTestResultImpl(string rawResult)
		{
			return GetResultKind(rawResult) == ResultKind.Test;
		}

	}
}
