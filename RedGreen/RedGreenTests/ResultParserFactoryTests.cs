using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using RedGreen;

namespace RedGreenTests
{
    public class ResultParserFactoryTests
    {
        [Fact]
        public void UnknownFramework()
        {
            ResultParserFactory factory = new ResultParserFactory();

            Assert.IsType(typeof(NullGallioParser), factory.GetParser("boo"));
        }

        [Fact]
        public void MbUnit()
        {
            ResultParserFactory factory = new ResultParserFactory();

            Assert.IsType(typeof(MbUnitGallioParser), factory.GetParser(new MbUnitGallioParser().Framwork));
        }

        [Fact]
        public void NUnit()
        {
            ResultParserFactory factory = new ResultParserFactory();

            Assert.IsType(typeof(NUnitGallioParser), factory.GetParser(new NUnitGallioParser().Framwork));
        }

        [Fact]
        public void Xunit()
        {
            ResultParserFactory factory = new ResultParserFactory();

            Assert.IsType(typeof(XunitGallioParser), factory.GetParser(new XunitGallioParser().Framwork));
        }
    }
}
