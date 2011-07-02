using System;

namespace CR_StyleCop.Tests.Helpers
{
    internal class VS2005SolutionLoader : VS2002SolutionLoader
    {
        protected override FileProjectLoaderBase CreateProjectLoader(ProjectInfo info)
        {
            LanguageID id = LanguageHelper.FromProjectLanguageTag(info.ProjectLangTag);
            switch (id)
            {
                case LanguageID.Basic:
                case LanguageID.CSharp:
                    return new VS2005VSLangProjectLoader();
            }
            return base.CreateProjectLoader(info);
        }
    }
}