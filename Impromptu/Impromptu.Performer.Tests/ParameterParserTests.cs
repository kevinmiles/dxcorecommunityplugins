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
using Xunit;

namespace Impromptu.Performer.Tests
{
	class ParameterParserTests
	{
		[Fact]
		public void NoParametersReturnsEmptySet()
		{
			var result = ParameterParser.Parse(new string[] {string.Empty});

			Assert.Equal(0, result.Count());
		}

		[Fact]
		public void NullReturnsEmptySet()
		{
			var result = ParameterParser.Parse(null);

			Assert.Equal(0, result.Count());
		}

		[Fact]
		public void ReturnsIEnumerableOfParameter()
		{
			object result = ParameterParser.Parse(null);

			IEnumerable<Parameter> implements = result as IEnumerable<Parameter>;

			Assert.NotNull(implements);
		}

		[Fact]
		public void SlashIsParameterIndicator()
		{
			var result = ParameterParser.Parse(new string[] { "/some:one" });

			Assert.Equal(1, result.Count());
		}

		[Fact]
		public void ParameterParser_MultipleParameters_CountCorrect()
		{
			var result = ParameterParser.Parse(new string[] { "/foo:bar", "-Some:one" });

			Assert.Equal(2, result.Count());
		}

		[Fact]
		public void ParameterParser_MultipleParameters_ParsedCorrect()
		{
			var result = ParameterParser.Parse(new string[] { "/foo:bar", "-Some:one" });

			List<Parameter> l = result as List<Parameter>;
			Assert.Equal("foo", l[0].Name);
			Assert.Equal("bar", l[0].Value);

			Assert.Equal("Some", l[1].Name);
			Assert.Equal("one", l[1].Value);
		}
	}
}
