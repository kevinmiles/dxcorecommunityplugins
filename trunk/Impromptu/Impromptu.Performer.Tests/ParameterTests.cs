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
using Xunit;

namespace Impromptu.Performer.Tests
{
	class ParameterTests
	{
		[Fact]
		public void Parameter_EmptyString_ReturnsNull()
		{
			Parameter result = Parameter.Parse(String.Empty);

			Assert.Null(result);
		}

		[Fact]
		public void Parameter_Null_ReturnsNull()
		{
			Parameter result = Parameter.Parse(null);

			Assert.Null(result);
		}

		[Fact]
		public void Parameter_LeadingSlash_ValidParameter()
		{
			Parameter result = Parameter.Parse("/foo");

			Assert.NotNull(result);
		}

		[Fact]
		public void Parameter_LeadingDash_ValidParameter()
		{
			Parameter result = Parameter.Parse("-foo");

			Assert.NotNull(result);
		}

		[Fact]
		public void Parameter_SlashFoo_ParsesParameterName()
		{
			Parameter result = Parameter.Parse("/foo");

			Assert.Equal("foo", result.Name);
		}

		[Fact]
		public void Parameter_DashFoo_ParsesParameterName()
		{
			Parameter result = Parameter.Parse("-foo");

			Assert.Equal("foo", result.Name);
		}

		[Fact]
		public void Parameter_ParsesNameAndValue()
		{
			Parameter result = Parameter.Parse("/foo:bar");

			Assert.Equal("foo", result.Name);
			Assert.Equal("bar", result.Value);
		}
	}
}
