/*
 * Software License Agreement for RedGreen
 * 
 * Copyright (c) 2009 Renaissance Learning, Inc. and James Argeropoulos
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
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Impromptu.Performer
{
	public class Parameter
	{
		private const char kDelimiter = ':';
		private const string kSlashSwitch = "/";
		private const string kDashSwitch = "-";
		public string Name { get; set; }
		public string Value { get; set; }

		public Parameter()
		{

		}

		private Parameter(string source)
		{
			int delimiterAt = source.IndexOf(kDelimiter);
			if (delimiterAt > 0)
			{
				Name = source.Substring(1, delimiterAt - 1);
				Value = source.Substring(delimiterAt + 1);
			}
			else
			{
				Name = source.Substring(1);
			}
		}
		public static Parameter Parse(string potential)
		{
			if (!string.IsNullOrEmpty(potential))
			{
				if (StartsWithSwitch(potential))
						return new Parameter(potential);
			}
			return null;
		}
		private static bool ContainsDelimiter(string potential)
		{
			return potential.Contains(kDelimiter);
		}
		private static bool StartsWithSwitch(string potential)
		{
			return potential.StartsWith(kSlashSwitch) || potential.StartsWith(kDashSwitch);
		}
	}
}
