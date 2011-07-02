using System;
using System.IO;

namespace CR_StyleCop.Tests.Helpers
{
    internal enum LanguageID
    {
        None,
        CSharp,
        Basic,
        Basic_90,
        Cpp,
        CppMe,
        Html,
        JavaScript,
        FSharp,
        FSharpLight,
        Xaml,
        Xml
    }

    internal class LanguageHelper
    {
        #region Constants...
        const string STR_Cs = "cs";
        const string STR_DotCs = "." + STR_Cs;

        const string STR_Vb = "vb";
        const string STR_DotVb = "." + STR_Vb;
        const string STR_CSHARP = "CSHARP";
        const string STR_VisualBasic = "VisualBasic";
        const string STR_Csproj = "csproj";
        const string STR_Vbproj = "vbproj";
        const string STR_DotCSProj = ".csproj";
        const string STR_DotVBProj = ".vbproj";

        const string STR_Cpp = "cpp";
        const string STR_DotCpp = "." + STR_Cpp;
        const string STR_VisualCpp = "VisualCpp";
        const string STR_CPPproj = "vcproj";
        const string STR_DotCppProj = ".vcproj";

        const string STR_Xaml = "xaml";
        const string STR_DotXaml = "." + STR_Xaml;
        #endregion

        private LanguageHelper() { }

        // public static methods...
        #region FromFile
        public static LanguageID FromFile(string path)
        {
            string lExtention = Path.GetExtension(path);
            switch (lExtention.ToLower())
            {
                case STR_Cs:
                case STR_DotCs:
                case STR_Csproj:
                case STR_DotCSProj:
                    return LanguageID.CSharp;
                case STR_Vb:
                case STR_DotVb:
                case STR_Vbproj:
                case STR_DotVBProj:
                    return LanguageID.Basic;
                case STR_Cpp:
                case STR_DotCpp:
                case STR_CPPproj:
                case STR_DotCppProj:
                    return LanguageID.Cpp;
                case STR_Xaml:
                case STR_DotXaml:
                    return LanguageID.Xaml;
            }
            return LanguageID.CSharp;
        }
        #endregion
        #region GetProjectLanguageTag
        public static string GetProjectLanguageTag(LanguageID language)
        {
            switch (language)
            {
                case LanguageID.CSharp:
                    return STR_CSHARP;
                case LanguageID.Basic:
                    return STR_VisualBasic;
                case LanguageID.Cpp:
                    return STR_VisualCpp;
            }
            return String.Empty;
        }
        #endregion
        #region FromProjectLanguageTag
        public static LanguageID FromProjectLanguageTag(string tag)
        {
            switch (tag)
            {
                case STR_CSHARP:
                    return LanguageID.CSharp;
                case STR_VisualBasic:
                    return LanguageID.Basic;
                case STR_VisualCpp:
                    return LanguageID.Cpp;
            }
            return LanguageID.None;
        }
        #endregion
    }
}