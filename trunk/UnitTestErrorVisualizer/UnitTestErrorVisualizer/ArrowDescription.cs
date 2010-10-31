/*
 * Software License Agreement for RedGreen
 * 
 * Copyright (c) 2010 Renaissance Learning, Inc. and James Argeropoulos
 * 
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to deal
 * in the Software without restriction, including without limitation the rights
 * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:
 * 
 * The above copyright notice and this permission notice shall be included in
 * all copies or substantial portions of the Software.
 * 
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
 * THE SOFTWARE.
 */
using System;
using DevExpress.CodeRush.StructuralParser;
using System.Text.RegularExpressions;
using DevExpress.CodeRush.Core.Testing;

using PlatformAttribute = DevExpress.CodeRush.StructuralParser.Attribute;

namespace UnitTestErrorVisualizer
{
	public class ArrowDescription
	{
		public ArrowDescription(PlatformAttribute attribute,
			Method method,
			TestResult result,
			MessageLimiter textConditioner)
		{
			ParseRange(attribute, result.StackTrace);
			ParseMessage(result.Message, textConditioner);
			Test = method;
		}
		public SourceRange Start { get; set; }
		public SourceRange End { get; set; }
		public string Expected { get; private set; }
		public string Correct {get;private set;}
		public string Incorrect {get; private set;}
		public Method Test { get; set; }

		private void ParseRange(PlatformAttribute attribute, string stackTrace)
		{
			Start = new SourceRange(attribute.StartLine, attribute.StartOffset);
			string assertLocation = ExtractLineAndColumnData(stackTrace);
			End = new SourceRange(ParseLineNumber(assertLocation), ParseColumnNumber(assertLocation));
		}
		private void ParseMessage(string message, MessageLimiter limiter)
		{
			string expected = TestResultParser.Expected(message);
			string actual = TestResultParser.Actual(message);
			int differAt = TestResultParser.DifferAt(message, expected, actual);
			string correct;
			string incorrect;

			limiter.AdjustExpectedActualLengths(ref expected, ref actual, differAt, out correct, out incorrect);
			Correct = correct;
			Incorrect = incorrect;
		}
		private static int ParseColumnNumber(string location)
		{
			int startColData = location.IndexOf(',') + 2;
			int endColData = location.LastIndexOf(')');
			return int.Parse(location.Substring(startColData, endColData - startColData));
		}

		private static int ParseLineNumber(string location)
		{
			return int.Parse(location.Substring(1, location.IndexOf(',') - 1));
		}

		private static string ExtractLineAndColumnData(string stackTrace)
		{
			//Xunit implementation
			return Regex.Match(stackTrace, @"\(\d+, \d+\)").Value;
		}
	}
}
