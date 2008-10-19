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
    public class MbUnitParseTests
    {
        [Fact]
        public void StringExpected()
        {
            string source = "Expected values to be equal.\n\nExpected Value : \"who\\'s there\"\nActual Value   : \"who\\'s where\"\n\n   at RedGreenPlayground.MbUnitTests.AlwaysFails() in C:\\Users\\jaargero.WRPWI\\Documents\\Visual Studio 2005\\Projects\\RedGreenPlayground\\RedGreenPlayground\\MbUnitTests.cs:line 21\n";
            Assert.Equal("Who's there", new MbUnitGallioParser().GetExpected(source));
        }

        [Fact]
        public void IntExpected()
        {
            string source = "Expected values to be equal.\n\nExpected Value : 0\nActual Value   : 1\n\n   at RedGreenPlayground.MbUnitTests.IntFail() in C:\\Users\\jaargero.WRPWI\\Documents\\Visual Studio 2005\\Projects\\RedGreenPlayground\\RedGreenPlayground\\MbUnitTests.cs:line 28\n";
            Assert.Equal("0", new MbUnitGallioParser().GetExpected(source));
        }

        [Fact]
        public void StringActual()
        {
            string source = "Expected values to be equal.\n\nExpected Value : \"who\\'s there\"\nActual Value   : \"who\\'s where\"\n\n   at RedGreenPlayground.MbUnitTests.AlwaysFails() in C:\\Users\\jaargero.WRPWI\\Documents\\Visual Studio 2005\\Projects\\RedGreenPlayground\\RedGreenPlayground\\MbUnitTests.cs:line 21\n";
            Assert.Equal("Who's where", new MbUnitGallioParser().GetActual(source));
        }

        [Fact]
        public void IntActual()
        {
            string source = "Expected values to be equal.\n\nExpected Value : 0\nActual Value   : 1\n\n   at RedGreenPlayground.MbUnitTests.IntFail() in C:\\Users\\jaargero.WRPWI\\Documents\\Visual Studio 2005\\Projects\\RedGreenPlayground\\RedGreenPlayground\\MbUnitTests.cs:line 28\n";
            Assert.Equal("1", new MbUnitGallioParser().GetActual(source));
        }

        [Fact]
        public void StringPosition()
        {
            string source = "Expected values to be equal.\n\nExpected Value : \"who\\'s there\"\nActual Value   : \"who\\'s where\"\n\n   at RedGreenPlayground.MbUnitTests.AlwaysFails() in C:\\Users\\jaargero.WRPWI\\Documents\\Visual Studio 2005\\Projects\\RedGreenPlayground\\RedGreenPlayground\\MbUnitTests.cs:line 21\n";
            Assert.Equal(7, new MbUnitGallioParser().GetPosition(source, "hello hill", "hello hull"));
        }

        [Fact]
        public void IntPosition()
        {
            string source = "MbUnit.Framework.AssertionException: . Equal assertion failed: [[0]]!=[[1]]\r\n   at MbUnit.Framework.Assert.FailNotEquals(Object expected, Object actual, String format, Object[] args) in c:\\RelEng\\Projects\\MbUnit v3\\Work\\src\\MbUnit\\MbUnit\\Framework\\Assert.cs:line 3502\r\n   at MbUnit.Framework.Assert.AreEqual(Int32 expected, Int32 actual, String message) in c:\\RelEng\\Projects\\MbUnit v3\\Work\\src\\MbUnit\\MbUnit\\Framework\\Assert.cs:line 382\r\n   at MbUnit.Framework.Assert.AreEqual(Int32 expected, Int32 actual) in c:\\RelEng\\Projects\\MbUnit v3\\Work\\src\\MbUnit\\MbUnit\\Framework\\Assert.cs:line 421\r\n   at RedGreenPlayground.MbUnitTests.IntFail() in C:\\Users\\jaargero.WRPWI\\Documents\\Visual Studio 2005\\Projects\\RedGreenPlayground\\RedGreenPlayground\\MbUnitTests.cs:line 28\r\n   at Gallio.Utilities.ExceptionUtils.RethrowWithNoStackTraceLoss(Exception ex)\r\n   at Gallio.Utilities.ExceptionUtils.InvokeMethodWithoutTargetInvocationException(MethodBase method, Object obj, Object[] args)\r\n   at Gallio.Framework.Pattern.PatternTestInstanceState.InvokeTestMethod()\r\n   at Gallio.Framework.Pattern.TestMethodPatternAttribute.<SetTestSemantics>b__1(PatternTestInstanceState testInstanceState)\r\n   at MbUnit.Framework.TestAttribute.<SetTestSemantics>b__0(PatternTestInstanceState state, Action`1 action) in c:\\RelEng\\Projects\\MbUnit v3\\Work\\src\\MbUnit\\MbUnit\\Framework\\TestAttribute.cs:line 99\r\n   at Gallio.ActionChain`1.<>c__DisplayClass1.<Around>b__0(T obj)\r\n   at Gallio.Framework.Pattern.PatternTestInstanceActions.ExecuteTestInstance(PatternTestInstanceState testInstanceState)\r\n   at Gallio.Framework.Pattern.PatternTestInstanceActions.ExecuteTestInstance(PatternTestInstanceState testInstanceState)\r\n   at Gallio.Framework.Pattern.PatternTestInstanceActions.ExecuteTestInstance(PatternTestInstanceState testInstanceState)\r\n   at Gallio.Framework.Pattern.PatternTestExecutor.<>c__DisplayClass26.<DoExecuteTestInstance>b__24()\r\n   at Gallio.Concurrency.ThreadAbortScope.RunWithThreadAbort(Action action)\r\n   at Gallio.Concurrency.ThreadAbortScope.Run(Action action)\r\n   at Gallio.Framework.Sandbox.RunWithScope(ThreadAbortScope scope, Action action, String description)\n";
            Assert.Equal(0, new MbUnitGallioParser().GetPosition(source, "0", "1"));
        }

        [Fact]
        public void GetMethodLocation()
        {
            Assert.Equal("RedGreenPlayground.NUnitTests.AlwaysPass", new MbUnitGallioParser().ReformatLocation("RedGreenPlayground/NUnitTests/AlwaysPass"));
        }
    }
}
