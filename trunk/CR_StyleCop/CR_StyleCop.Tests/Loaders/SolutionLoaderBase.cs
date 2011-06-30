using System;
using DevExpress.CodeRush.StructuralParser;

namespace CR_StyleCop.Tests.Helpers
{
    public interface ISolutionLoader
    {
        SolutionElement LoadFrom(string path);
    }

    public abstract class SolutionLoaderBase : ISolutionLoader
    {
        public abstract SolutionElement LoadFrom(string path);
        public abstract SolutionElement LoadFrom(string path, string configuration, string platform);
    }
}