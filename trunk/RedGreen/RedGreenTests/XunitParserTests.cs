using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using RedGreen;

namespace RedGreenTests
{
    public class XunitParserTests
    {
        [Fact]
        public void StringExpected()
        {
            string source = "Assert.Equal() Failure\nPosition: First difference is at position 7\nExpected: hello hill\nActual:   hello hull\n   at RedGreenPlayground.Class1.AlwaysFails() in C:\\Users\\jaargero.WRPWI\\Documents\\Visual Studio 2005\\Projects\\RedGreenPlayground\\RedGreenPlayground\\xUnitTests.cs:line 39\n";
            Assert.Equal("hello hill", new XunitGallioParser().GetExpected(source));
        }

        [Fact]
        public void IntExpected()
        {
            string source = "Assert.Equal() Failure\nExpected: 0\nActual:   1\n   at RedGreenPlayground.Class1.IntFail() in C:\\Users\\jaargero.WRPWI\\Documents\\Visual Studio 2005\\Projects\\RedGreenPlayground\\RedGreenPlayground\\xUnitTests.cs:line 46\n";
            Assert.Equal("0", new XunitGallioParser().GetExpected(source));
        }

        [Fact]
        public void StringActual()
        {
            string source = "Assert.Equal() Failure\nPosition: First difference is at position 7\nExpected: hello hill\nActual:   hello hull\n   at RedGreenPlayground.Class1.AlwaysFails() in C:\\Users\\jaargero.WRPWI\\Documents\\Visual Studio 2005\\Projects\\RedGreenPlayground\\RedGreenPlayground\\xUnitTests.cs:line 39\n";
            Assert.Equal("hello hull", new XunitGallioParser().GetActual(source));
        }

        [Fact]
        public void IntActual()
        {
            string source = "Assert.Equal() Failure\nExpected: 0\nActual:   1\n   at RedGreenPlayground.Class1.IntFail() in C:\\Users\\jaargero.WRPWI\\Documents\\Visual Studio 2005\\Projects\\RedGreenPlayground\\RedGreenPlayground\\xUnitTests.cs:line 46\n";
            Assert.Equal("1", new XunitGallioParser().GetActual(source));
        }

        [Fact]
        public void StringPosition()
        {
            string source = "Assert.Equal() Failure\nPosition: First difference is at position 7\nExpected: hello hill\nActual:   hello hull\n   at RedGreenPlayground.Class1.AlwaysFails() in C:\\Users\\jaargero.WRPWI\\Documents\\Visual Studio 2005\\Projects\\RedGreenPlayground\\RedGreenPlayground\\xUnitTests.cs:line 39\n";
            Assert.Equal(7, new XunitGallioParser().GetPosition(source, "hello hill", "hello hull"));
        }

        [Fact]
        public static void IntPosition()
        {
            string source = "Assert.Equal() Failure\nExpected: 0\nActual:   1\n   at RedGreenPlayground.Class1.IntFail() in C:\\Users\\jaargero.WRPWI\\Documents\\Visual Studio 2005\\Projects\\RedGreenPlayground\\RedGreenPlayground\\xUnitTests.cs:line 46\n";
            Assert.Equal(0, new XunitGallioParser().GetPosition(source, "0", "1"));
        }

        [Fact]
        public void GetMethodLocation()
        {
            Assert.Equal("RedGreenPlayground.NUnitTests.AlwaysPass", new XunitGallioParser().ReformatLocation("RedGreenPlayground/NUnitTests/AlwaysPass"));
        }

        [Fact]
        public void LineNumber()
        {
            string source = "Assert.Equal() Failure\r\nPosition: First difference is at position 6\r\nExpected: who's there\r\nActual:   who's where   at RedGreenPlayground.Class1.AlwaysFails() in C:\\Users\\JAARGERO.WRPWI\\Documents\\Visual Studio 2005\\Projects\\RedGreenPlayground\\RedGreenPlayground\\xUnitTests.cs:line 39\n";
            Assert.Equal(39, new XunitGallioParser().GetLineNumber(source));
        }

		[Fact]
		private void AltLineNumber()
		{
			string source = "System.NullReferenceException : Object reference not set to an instance of an object.   C:\\Users\\JAARGERO.WRPWI\\Documents\\dxCorePlugIns\\RedGreen\\RedGreen\\NUnitParser.cs(38,0): at RedGreen.NUnitParser.ParseSummary(String[] source)\r\n   C:\\Users\\JAARGERO.WRPWI\\Documents\\dxCorePlugIns\\RedGreen\\RedGreenTests\\NunitParserTests.cs(105,0): at RedGreenTests.NunitParserTests.ParseSummary()";

			Assert.Equal(105, XunitRunner.GetLineNumber(source));
		}
    }
}
