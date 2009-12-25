using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace UnitTestErrorVisualizer
{
	public class TestResultParser
	{
		private const string kNUnitActualStartDelimiter = "\n  But was:  ";
		private const string kNUnitExpectedStartDelimiter = "Expected: ";
		private const string kXunitActualStartDelimiter = "\nActual:   ";
		private const string kXunitExpectedStartDelimiter = "\nExpected: ";
		private const string kMbUnitActualStartDelimiter = "\nActual Value   : ";
		private const string kMbUnitExpectedStartDelimiter = "\nExpected Value : ";
		private const string kMbUnitAltActualStartDelimiter = ".\n\nActual Value : ";
		private const string kMbUnitAltExpectedStartDelimiter = "Expected value to be ";
		private const string kMbUnitActualEndDelimiter = "\n\n   at ";
		private const string kNUnitDifferAtStartDelimiter = "Strings differ at index ";
		private const string kXunitDifferAtStartDelimiter = "First difference is at position ";
		public static int LineNumber(string stack)
		{
			stack = stack.Trim();
			Console.WriteLine(stack);
			stack = stack.Substring(stack.LastIndexOf('\n'));
			Console.WriteLine(stack);
			string location = Regex.Match(stack, @"\(\d+\, \d+\)").Value;
			Console.WriteLine(location);
			return int.Parse(Regex.Match(location, @"\d+").Value);
		}
		public static string Expected(string message)
		{
			if (message.Contains(kNUnitActualStartDelimiter))
            {
            	return GetExpectedNUnit(message);
            }
			if (message.Contains(kXunitActualStartDelimiter))
            {
				return GetExpectedXunit(message);
            }
			if (message.Contains(kMbUnitActualStartDelimiter))
            {
				return GetExpectedMbUnit(message);
            }
			if (message.Contains(kMbUnitAltActualStartDelimiter))
			{
				return GetExpectedMbUnitAlt(message);
			}
			return String.Empty;
		}
		private static string GetExpectedXunit(string message)
		{
			string expected = message.Substring(0, message.IndexOf(kXunitActualStartDelimiter));
			return expected.Substring(expected.IndexOf(kXunitExpectedStartDelimiter) + kXunitExpectedStartDelimiter.Length);
		}
		private static string GetExpectedNUnit(string message)
		{
			string expected = message.Substring(0, message.IndexOf(kNUnitActualStartDelimiter));
			return expected.Substring(expected.IndexOf(kNUnitExpectedStartDelimiter) + kNUnitExpectedStartDelimiter.Length);
		}
		private static string GetExpectedMbUnit(string message)
		{
			string expected = message.Substring(0, message.IndexOf(kMbUnitActualStartDelimiter));
			return expected.Substring(expected.IndexOf(kMbUnitExpectedStartDelimiter) + kMbUnitExpectedStartDelimiter.Length);
		}
		private static string GetExpectedMbUnitAlt(string message)
		{// Here because MbUnit is not consistent between message. In this case Null/NotNull values
			string expected = message.Substring(0, message.IndexOf(kMbUnitAltActualStartDelimiter));
			return expected.Substring(expected.IndexOf(kMbUnitAltExpectedStartDelimiter) + kMbUnitAltExpectedStartDelimiter.Length);
		}
		public static string Actual(string message)
		{
			if (message.Contains(kNUnitActualStartDelimiter))
			{
				return GetActualNUnit(message);
			}
			if (message.Contains(kXunitActualStartDelimiter))
			{
				return GetActualXunit(message);
			}
			if (message.Contains(kMbUnitActualStartDelimiter))
			{
				return GetActualMbUnit(message);
			}
			if (message.Contains(kMbUnitAltActualStartDelimiter))
			{
				return GetActualMbUnitAlt(message);
			}
			return String.Empty;
		}
		private static string GetActualMbUnit(string message)
		{
			string actual = message.Substring(message.IndexOf(kMbUnitActualStartDelimiter) + kMbUnitActualStartDelimiter.Length);
			return actual.Substring(0, actual.IndexOf(kMbUnitActualEndDelimiter));
		}
		private static string GetActualXunit(string message)
		{
			return message.Substring(message.IndexOf(kXunitActualStartDelimiter) + kXunitActualStartDelimiter.Length);
		}
		private static string GetActualNUnit(string message)
		{
			string actual = message.Substring(message.IndexOf(kNUnitActualStartDelimiter) + kNUnitActualStartDelimiter.Length);
			return actual.Substring(0, actual.IndexOf("\n"));
		}
		private static string GetActualMbUnitAlt(string message)
		{// Here because MbUnit is not consistent between message. In this case Null/NotNull values
			string actual = message.Substring(message.IndexOf(kMbUnitAltActualStartDelimiter) + kMbUnitAltActualStartDelimiter.Length);
			return actual.Substring(0, actual.IndexOf(kMbUnitActualEndDelimiter));
		}
		public static int DifferAt(string message, string expected, string actual)
		{
			if (message.Contains(kNUnitDifferAtStartDelimiter))
			{
				return GetDifferAtNUnit(message);
			}
			if (message.Contains(kXunitDifferAtStartDelimiter))
			{
				return GetDifferAtXunit(message);
			}
			return GetDifferAtManual(expected, actual);
		}
		private static int GetDifferAtXunit(string message)
		{
			string header = message.Substring(0, message.IndexOf(kXunitExpectedStartDelimiter));
			string index = header.Substring(header.IndexOf(kXunitDifferAtStartDelimiter));
			return int.Parse(Regex.Match(index, @"\d+").Value);
		}
		public static int GetDifferAtManual(string expected, string actual)
		{
			int i = 0;
			if (string.IsNullOrEmpty(expected) == false && string.IsNullOrEmpty(actual) == false)
			{
				for (; i < Math.Min(expected.Length, actual.Length); i++)
					if (expected[i] != actual[i])
						break;
			}
			return i;
		}
		private static int GetDifferAtNUnit(string message)
		{
			string header = message.Substring(0, message.IndexOf(kNUnitActualStartDelimiter));
			string index = header.Substring(header.IndexOf(kNUnitDifferAtStartDelimiter));
			return int.Parse(Regex.Match(index, @"\d+").Value);
		}
	}
}
