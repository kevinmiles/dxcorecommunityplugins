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
using Xunit.Extensions;

namespace Impromptu.Performer.Tests
{
	public class RunnerTests
	{
		[Theory,
		InlineData(""),
		InlineData(null),
		InlineData("/AssemblyPath:")
		]
		public void Runner_InvalidAssemblyArgument_AssemblyMissing(string arguments)
		{
			CapturedOutput messages = new CapturedOutput();
			Runner underTest = new Runner(messages);

			underTest.ExecuteMethod(new string[] {arguments});

			Assert.Equal("The /AssemblyPath argument is missing or incomplete", messages.Output[0]);
		}

		[Fact]
		public void Runner_GarbagePath_FileNotFoundMessage()
		{
			CapturedOutput messages = new CapturedOutput();
			Runner underTest = new Runner(messages);

			underTest.ExecuteMethod(new string[] { "/AssemblyPath:c:/1sfaserdf.dll" }); //If anyone has this file in the root of their C drive they need help

			Assert.Equal("Requested assembly not found", messages.Output[0]);
		}

		[Theory,
		InlineData(""),
		InlineData(null),
		InlineData("/TypeName:")
		]
		public void Runner_InvalidTypeArgument_AssemblyMissing(string arguments)
		{
			CapturedOutput messages = new CapturedOutput();
			Runner underTest = new Runner(messages);

			underTest.ExecuteMethod(new string[] { "/AssemblyPath:C:\\windows\\twain.dll", arguments});

			Assert.Equal("The /TypeName argument is missing or incomplete", messages.Output[0]);
		}

		[Theory,
		InlineData(""),
		InlineData(null),
		InlineData("/MethodName:")
		]
		public void Runner_InvalidMethodArgument_AssemblyMissing(string arguments)
		{
			CapturedOutput messages = new CapturedOutput();
			Runner underTest = new Runner(messages);

			underTest.ExecuteMethod(new string[] { "/AssemblyPath:C:\\windows\\twain.dll", "/TypeName:foo", arguments });

			Assert.Equal("The /MethodName argument is missing or incomplete", messages.Output[0]);
		}

		private int IntReturn()
		{
			return 42;
		}

		private string StringReturn()
		{
			return "The Lord, he is God";
		}

		[Theory,
		 InlineData("IntReturn", "42"),
		 InlineData("StringReturn", "The Lord, he is God")
		]
		public void Runner_ValidateOutput(string methodName, string returnValue)
		{
			CapturedOutput messages = new CapturedOutput();
			Runner underTest = new Runner(messages);

			underTest.ExecuteMethod(new string[] { 
				// Yeah, yeah. the path is specific to my hard drive. But based upon my RedGreen experience. Nary a soul besides me will ever run the tests
				"/AssemblyPath:C:\\Users\\JAARGERO.WRPWI\\Documents\\Visual Studio 2008\\Projects\\Impromptu\\Impromptu.Performer.Tests\\bin\\Debug\\Impromptu.Performer.Tests.dll",
				"/TypeName:Impromptu.Performer.Tests.RunnerTests",
				string.Format("/MethodName:{0}", methodName)
			});

			Assert.Equal(returnValue, messages.Output[0]);
		}
	}
}
