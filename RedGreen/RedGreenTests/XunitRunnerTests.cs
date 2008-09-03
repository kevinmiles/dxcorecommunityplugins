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
using RedGreen;

namespace RedGreenTests
{
    public class XunitRunnerTests
    {
        [Fact]
        public void FailureLocationOneWord()
        {
            string source = "Assert.Equal() Failure\r\nPosition: First difference is at position 1\r\nExpected: foo\r\nActual:   f0o   at RedGreenPlayground.Class1.ATest() in C:\\Users\\jaargero.WRPWI\\Documents\\Visual Studio 2005\\Projects\\RedGreenPlayground\\RedGreenPlayground\\Class1.cs:line 32";
            string final = "Assert.Equal() Failure\r\nPosition: First difference is at position 1\r\nExpected: foo\r\nActual:   f0o   \r\nC:\\Users\\jaargero.WRPWI\\Documents\\Visual Studio 2005\\Projects\\RedGreenPlayground\\RedGreenPlayground\\Class1.cs(32,0): at RedGreenPlayground.Class1.ATest()";

            string newMessage = XunitRunner.FormatFailure(source);
            
            Assert.Equal(final, newMessage);
        }

        [Fact]
        public void FailureLocationTwoWord()
        {
            string source = "Assert.Equal() Failure\r\nPosition: First difference is at position 7\r\nExpected: hello hill\r\nActual:   hello hull   at RedGreenPlayground.Class1.ATest() in C:\\Users\\jaargero.WRPWI\\Documents\\Visual Studio 2005\\Projects\\RedGreenPlayground\\RedGreenPlayground\\Class1.cs:line 32";
            string final = "Assert.Equal() Failure\r\nPosition: First difference is at position 7\r\nExpected: hello hill\r\nActual:   hello hull   \r\nC:\\Users\\jaargero.WRPWI\\Documents\\Visual Studio 2005\\Projects\\RedGreenPlayground\\RedGreenPlayground\\Class1.cs(32,0): at RedGreenPlayground.Class1.ATest()";
            Assert.Equal(final, XunitRunner.FormatFailure(source));
        }

        [Fact]
        public void ExtractLineNumber()
        {
            string source = "Assert.Equal() Failure\r\nPosition: First difference is at position 7\r\nExpected: hello hill\r\nActual:   hello hull   at RedGreenPlayground.Class1.ATest() in C:\\Users\\jaargero.WRPWI\\Documents\\Visual Studio 2005\\Projects\\RedGreenPlayground\\RedGreenPlayground\\Class1.cs:line 32";
            Assert.Equal(32, XunitRunner.GetLineNumber(source));
        }

        [Fact]
        public void ExtractErrorPosition()
        {
            string source = "Assert.Equal() Failure\r\nPosition: First difference is at position 7\r\nExpected: hello hill\r\nActual:   hello hull   at RedGreenPlayground.Class1.ATest() in C:\\Users\\jaargero.WRPWI\\Documents\\Visual Studio 2005\\Projects\\RedGreenPlayground\\RedGreenPlayground\\Class1.cs:line 32";
            Assert.Equal(7, XunitRunner.GetPosition(source));
        }

        [Fact]
        public void ExtractErrorExpected()
        {
            string source = "Assert.Equal() Failure\r\nPosition: First difference is at position 7\r\nExpected: hello hill\r\nActual:   hello hull   at RedGreenPlayground.Class1.ATest() in C:\\Users\\jaargero.WRPWI\\Documents\\Visual Studio 2005\\Projects\\RedGreenPlayground\\RedGreenPlayground\\Class1.cs:line 32";
            Assert.Equal("hello hill", XunitRunner.GetExpected(source));
        }

        [Fact]
        public void ExtractErrorActual()
        {
            string source = "Assert.Equal() Failure\r\nPosition: First difference is at position 7\r\nExpected: hello hill\r\nActual:   hello hull   at RedGreenPlayground.Class1.ATest() in C:\\Users\\jaargero.WRPWI\\Documents\\Visual Studio 2005\\Projects\\RedGreenPlayground\\RedGreenPlayground\\Class1.cs:line 32";
            Assert.Equal("hello hull", XunitRunner.GetActual(source));
        }
    }
}
