using System;
using DevExpress.CodeRush.StructuralParser;

namespace CR_StyleCop.Tests.Helpers
{
    internal interface ISolutionLoader
    {
        SolutionElement LoadFrom(string path);
    }

    internal abstract class SolutionLoaderBase : ISolutionLoader
    {
        public abstract SolutionElement LoadFrom(string path);
        public abstract SolutionElement LoadFrom(string path, string configuration, string platform);
    }
}