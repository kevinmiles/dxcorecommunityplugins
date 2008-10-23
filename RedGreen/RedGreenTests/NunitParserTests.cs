/*
 * Software License Agreement for RedGreen
 * 
 * Copyright (c) 2008 Renaissance Learning, Inc. and James Argeropoulos
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
using System.Text;
using Xunit;
using System.Collections;
using RedGreen;

namespace RedGreenTests
{
    public class NunitParserTests
    {
        [Fact]
        public void ExpectedNotPresent()
        {
            Assert.Equal(String.Empty, new NUnitGallioParser().GetExpected(String.Empty));
            Assert.Equal(String.Empty, new NUnitGallioParser().GetExpected(null));
        }

        [Fact]
        public void StringExpected()
        {
            string source = "Message\n  String lengths are both 11. Strings differ at index 6.\n  Expected: \"who's there\"\n  But was:  \"who's where\"\n  -----------------^\n\nStack Trace\n   at NUnit.Framework.Assert.That(Object actual, Constraint constraint, String message, Object[] args)\n   at NUnit.Framework.Assert.AreEqual(Object expected, Object actual, String message, Object[] args)\n   at NUnit.Framework.Assert.AreEqual(Object expected, Object actual)\n   at RedGreenPlayground.NUnitTests.AlwaysFails() in C:\\Users\\jaargero.WRPWI\\Documents\\Visual Studio 2005\\Projects\\RedGreenPlayground\\RedGreenPlayground\\NUnitTests.cs:line 21";
            string expected = "\"who's there\"";
            Assert.Equal(expected, new NUnitGallioParser().GetExpected(source));
        }

        [Fact]
        public void NumberExpected()
        {
            string source = "Message\n  Expected: 0\n  But was:  1\n\nStack Trace\n   at NUnit.Framework.Assert.That(Object actual, Constraint constraint, String message, Object[] args)\n   at NUnit.Framework.Assert.AreEqual(Int32 expected, Int32 actual, String message, Object[] args)\n   at NUnit.Framework.Assert.AreEqual(Int32 expected, Int32 actual)\n   at RedGreenPlayground.NUnitTests.IntFail() in C:\\Users\\jaargero.WRPWI\\Documents\\Visual Studio 2005\\Projects\\RedGreenPlayground\\RedGreenPlayground\\NUnitTests.cs:line 28";
            string expected = "0";
            Assert.Equal(expected, new NUnitGallioParser().GetExpected(source));
        }

        [Fact]
        public void ActualNotPresent()
        {
            Assert.Equal(String.Empty, new NUnitGallioParser().GetActual(String.Empty));
            Assert.Equal(String.Empty, new NUnitGallioParser().GetActual(null));
        }

        [Fact]
        public void StringActual()
        {
            string source = "Message\n  String lengths are both 11. Strings differ at index 6.\n  Expected: \"who's there\"\n  But was:  \"who's where\"\n  -----------------^\n\nStack Trace\n   at NUnit.Framework.Assert.That(Object actual, Constraint constraint, String message, Object[] args)\n   at NUnit.Framework.Assert.AreEqual(Object expected, Object actual, String message, Object[] args)\n   at NUnit.Framework.Assert.AreEqual(Object expected, Object actual)\n   at RedGreenPlayground.NUnitTests.AlwaysFails() in C:\\Users\\jaargero.WRPWI\\Documents\\Visual Studio 2005\\Projects\\RedGreenPlayground\\RedGreenPlayground\\NUnitTests.cs:line 21";
            string expected = "\"who's where\"";
            Assert.Equal(expected, new NUnitGallioParser().GetActual(source));
        }

        [Fact]
        public void NumericActual()
        {
            string source = "Message\n  Expected: 0\n  But was:  1\n\nStack Trace\n   at NUnit.Framework.Assert.That(Object actual, Constraint constraint, String message, Object[] args)\n   at NUnit.Framework.Assert.AreEqual(Int32 expected, Int32 actual, String message, Object[] args)\n   at NUnit.Framework.Assert.AreEqual(Int32 expected, Int32 actual)\n   at RedGreenPlayground.NUnitTests.IntFail() in C:\\Users\\jaargero.WRPWI\\Documents\\Visual Studio 2005\\Projects\\RedGreenPlayground\\RedGreenPlayground\\NUnitTests.cs:line 28";
            string expected = "1";
            Assert.Equal(expected, new NUnitGallioParser().GetActual(source));
        }

        [Fact]
        public void GetPositionString()
        {
            string source = "Message\n  String lengths are both 11. Strings differ at index 6.\n  Expected: \"who's there\"\n  But was:  \"who's where\"\n  -----------------^\n\nStack Trace\n   at NUnit.Framework.Assert.That(Object actual, Constraint constraint, String message, Object[] args)\n   at NUnit.Framework.Assert.AreEqual(Object expected, Object actual, String message, Object[] args)\n   at NUnit.Framework.Assert.AreEqual(Object expected, Object actual)\n   at RedGreenPlayground.NUnitTests.AlwaysFails() in C:\\Users\\jaargero.WRPWI\\Documents\\Visual Studio 2005\\Projects\\RedGreenPlayground\\RedGreenPlayground\\NUnitTests.cs:line 21";
            Assert.Equal(6, new NUnitGallioParser().GetPosition(source, "who's where", "who's there"));
        }

        [Fact]
        public void GetMethodLocation()
        {
            Assert.Equal("RedGreenPlayground.NUnitTests.AlwaysPass", new NUnitGallioParser().ReformatLocation("RedGreenPlayground/NUnitTests/AlwaysPass"));
        }

        [Fact]
        public void GetLineNumber()
        {
            string source = "Message\n  String lengths are both 11. Strings differ at index 6.\n  Expected: \"who's there\"\n  But was:  \"who's where\"\n  -----------------^\n\nStack Trace\n   at NUnit.Framework.Assert.That(Object actual, Constraint constraint, String message, Object[] args)\n   at NUnit.Framework.Assert.AreEqual(Object expected, Object actual, String message, Object[] args)\n   at NUnit.Framework.Assert.AreEqual(Object expected, Object actual)\n   at RedGreenPlayground.NUnitTests.AlwaysFails() in C:\\Users\\jaargero.WRPWI\\Documents\\Visual Studio 2005\\Projects\\RedGreenPlayground\\RedGreenPlayground\\NUnitTests.cs:line 21";
            Assert.Equal(21, new NUnitGallioParser().GetLineNumber(source));
        }
    }
}
