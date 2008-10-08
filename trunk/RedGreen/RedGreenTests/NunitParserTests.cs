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
            string source = "[failed] NUnit v2.4.7.0/RedGreenPlayground/NUnitTests/AlwaysFails\r\n  String lengths are both 10. Strings differ at index 7.\r\n  Expected: \"hello hill\"\r\n  But was:  \"hello hull\"\r\n  ------------------^\r\n   at NUnit.Framework.Assert.That(Object actual, Constraint constraint, String message, Object[] args)\r\n   at NUnit.Framework.Assert.AreEqual(Object expected, Object actual, String message, Object[] args)\r\n   at NUnit.Framework.Assert.AreEqual(Object expected, Object actual)\r\n   at RedGreen.Playground.NUnitTests.AlwaysFails()\n\r\n";
            string expected = "\"hello hill\"";
            Assert.Equal(expected, new NUnitGallioParser().GetExpected(source));
        }

        [Fact]
        public void StringExpected2()
        {
            string source = "[failed] NUnit v2.4.7.0/RedGreenPlayground/NUnitTests/AlwaysFails\r\n  String lengths are both 10. Strings differ at index 7.\r\n  Expected: \"hello hill\"\r\n  But was:  \"hello hull\"\r\n  ------------------^\r\n   at NUnit.Framework.Assert.That(Object actual, Constraint constraint, String message, Object[] args)\r\n   at NUnit.Framework.Assert.AreEqual(Object expected, Object actual, String message, Object[] args)\r\n   at NUnit.Framework.Assert.AreEqual(Object expected, Object actual)\r\n   at RedGreen.Playground.NUnitTests.AlwaysFails()\n\r\n";
            string expected = "\"hello hill\"";
            NUnitGallioParser parser = new NUnitGallioParser();
            Assert.Equal(expected, parser.GetExpected(source));
        }

        [Fact]
        public void NumberExpected()
        {
            string source = "  Expected: 0\r\n  But was:  1\r\n   at NUnit.Framework.Assert.That(Object actual, Constraint constraint, String message, Object[] args)\r\n   at NUnit.Framework.Assert.AreEqual(Int32 expected, Int32 actual, String message, Object[] args)\r\n   at NUnit.Framework.Assert.AreEqual(Int32 expected, Int32 actual)\r\n   at RedGreenPlayground.NUnitTests.IntFail() in C:\\Users\\jaargero.WRPWI\\Documents\\Visual Studio 2005\\Projects\\RedGreenPlayground\\RedGreenPlayground\\NUnitTests.cs:line 28\n";
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
            string source = "[failed] NUnit v2.4.7.0/RedGreenPlayground/NUnitTests/AlwaysFails\r\n  String lengths are both 10. Strings differ at index 7.\r\n  Expected: \"hello hill\"\r\n  But was:  \"hello hull\"\r\n  ------------------^\r\n   at NUnit.Framework.Assert.That(Object actual, Constraint constraint, String message, Object[] args)\r\n   at NUnit.Framework.Assert.AreEqual(Object expected, Object actual, String message, Object[] args)\r\n   at NUnit.Framework.Assert.AreEqual(Object expected, Object actual)\r\n   at RedGreen.Playground.NUnitTests.AlwaysFails()\n\r\n";
            string expected = "\"hello hull\"";
            Assert.Equal(expected, new NUnitGallioParser().GetActual(source));
        }

        [Fact]
        public void NumericActual()
        {
            string source = "[failed] NUnit v2.4.7.0/RedGreenPlayground/NUnitTests/IntFails\r\n  Expected: 0\r\n  But was:  1\r\n   at NUnit.Framework.Assert.That(Object actual, Constraint constraint, String message, Object[] args)\r\n   at NUnit.Framework.Assert.AreEqual(Object expected, Object actual, String message, Object[] args)\r\n   at NUnit.Framework.Assert.AreEqual(Object expected, Object actual)\r\n   at RedGreen.Playground.NUnitTests.AlwaysFails()\n\r\n";
            string expected = "1";
            Assert.Equal(expected, new NUnitGallioParser().GetActual(source));
        }

        [Fact]
        public void GetPositionString()
        {
            string source = "[failed] NUnit v2.4.7.0/RedGreenPlayground/NUnitTests/AlwaysFails\r\n  String lengths are both 10. Strings differ at index 7.\r\n  Expected: \"hello hill\"\r\n  But was:  \"hello hull\"\r\n  ------------------^\r\n   at NUnit.Framework.Assert.That(Object actual, Constraint constraint, String message, Object[] args)\r\n   at NUnit.Framework.Assert.AreEqual(Object expected, Object actual, String message, Object[] args)\r\n   at NUnit.Framework.Assert.AreEqual(Object expected, Object actual)\r\n   at RedGreen.Playground.NUnitTests.AlwaysFails()\n\r\n";
            Assert.Equal(7, new NUnitGallioParser().GetPosition(source, "hello hill", "hello hull"));
        }

        [Fact]
        public void GetMethodLocation()
        {
            Assert.Equal("RedGreenPlayground.NUnitTests.AlwaysPass", new NUnitGallioParser().ReformatLocation("RedGreenPlayground/NUnitTests/AlwaysPass"));
        }

        [Fact]
        public void GetLineNumber()
        {
            Assert.Equal(21, new NUnitGallioParser().GetLineNumber("  String lengths are both 10. Strings differ at index 7.\r\n  Expected: \"hello hill\"\r\n  But was:  \"hello hull\"\r\n  ------------------^\r\n   at NUnit.Framework.Assert.That(Object actual, Constraint constraint, String message, Object[] args)\r\n   at NUnit.Framework.Assert.AreEqual(Object expected, Object actual, String message, Object[] args)\r\n   at NUnit.Framework.Assert.AreEqual(Object expected, Object actual)\r\n   at RedGreenPlayground.NUnitTests.AlwaysFails() in C:\\Users\\jaargero.WRPWI\\Documents\\Visual Studio 2005\\Projects\\RedGreenPlayground\\RedGreenPlayground\\NUnitTests.cs:line 21\n", string.Empty));
        }
    }
}
