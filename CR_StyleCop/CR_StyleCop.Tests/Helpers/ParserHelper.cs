using System;
using DevExpress.CodeRush.StructuralParser;
using DevExpress.CodeRush.StructuralParser.Xml;
using DevExpress.CodeRush.StructuralParser.CSharp;
using DevExpress.CodeRush.StructuralParser.VB;
using DX_CPPLanguage.CppParser;

using VisualStudioVersion = DevExpress.CodeRush.Core.VisualStudioVersion;
using System.Reflection;
using DevExpress.CodeRush.Core;

namespace CR_StyleCop.Tests.Helpers
{
    internal class ParserHelper
    {
        static ParserHelper()
        {
            RegisterXmlDocParser();
        }

        protected ParserHelper()
        {
        }

        // private methods...
        static void RegisterXmlDocParser()
        {
            ParserServices.RegisterXmlDocParser(new NewXmlParser());
        }

        // public static methods...
        public static Service CreateParserService(Type type)
        {
            return (Service)type.GetConstructor(BindingFlags.NonPublic | BindingFlags.CreateInstance | BindingFlags.Instance, null, new Type[] { }, new ParameterModifier[] { }).Invoke(new object[] { });
        }
        public static void RegisterParserServices()
        {
            ILanguageService lLanguage = (ILanguageService)CreateParserService(typeof(LanguageServices));
            ISourceModelService lSource = (ISourceModelService)CreateParserService(typeof(SourceModelServices));
            IssueServices lIssues = (IssueServices)CreateParserService(typeof(IssueServices));
            PlugInExtensionServices lPluginServices = (PlugInExtensionServices)CreateParserService(typeof(PlugInExtensionServices));
            ParserServices.RegisterLanguageService(lLanguage);
            ParserServices.RegisterSourceModelService(lSource);
            ParserServices.RegisterIssueService(lIssues);

            CodeRush.SetupServiceManager(new ServiceManager());
            //CodeRush.ChangeState(CodeRushState.Initialized, CodeRushStateCause.User);
            //  CodeRush.PlugInExtensions.
            //  CR_StyleCop.CR_StyleCopPlugIn plugin = new CR_StyleCop.CR_StyleCopPlugIn();
            //  lPluginServices.Register;

            lLanguage.RegisterParsers(ParserHelper.GetParsers());
            RegisterSourceTreeResolver();

            ParseWorkerThread.Start();
        }
        public static void UnRegisterParserServices()
        {
            if (ParseWorkerThread.Instance != null)
                ParseWorkerThread.Stop();

            try
            {
                MethodInfo finalizeService = typeof(StringServices).GetMethod("FinalizeService", BindingFlags.NonPublic | BindingFlags.Instance);
                finalizeService.Invoke(ParserServices.StringService, new object[] { FinalizeCause.HostShutdown });
            }
            catch { }
            ParserServices.UnregisterIssueService();
            ParserServices.UnregisterLanguageService();
            ParserServices.UnregisterSourceModelService();
            ParserServices.UnregisterSourceTreeResolver();
        }
        public static void RegisterSourceTreeResolver()
        {
            SourceTreeResolverOptions lOptions = new SourceTreeResolverOptions();
            lOptions.SearchInAssemblies = true;
            lOptions.CacheElementsFromAssemblies = true;
            ParserServices.RegisterSourceTreeResolver(new SourceTreeResolver(lOptions, ParserServices.LanguageService));
        }
        public static SolutionLoaderBase GetSolutionLoader(string format)
        {
            if (format.IndexOf("7.00") >= 0)
                return new VS2002SolutionLoader();

            else if (format.IndexOf("8.00") >= 0)
                return new VS2003SolutionLoader();

            else if (format.IndexOf("9.00") >= 0)
                return new VS2005SolutionLoader();

            else if (format.IndexOf("10.00") >= 0)
                return new VS2005SolutionLoader();

            else if (format.IndexOf("11.00") >= 0)
                return new VS2005SolutionLoader();

            return null;
        }

        public static ParserBase[] GetParsers()
        {
            return GetParsers(VisualStudioVersion.VS2005);
        }

        public static ParserBase[] GetParsers(VisualStudioVersion vsVersion)
        {
            ParserBase[] lParsers = new ParserBase[3];
            switch (vsVersion)
            {
                case VisualStudioVersion.VS2002:
                case VisualStudioVersion.VS2003:
                    lParsers[0] = new CSharp30Parser();
                    lParsers[1] = new VB10Parser();
                    lParsers[2] = new ParserCpp();
                    break;
                case VisualStudioVersion.VS2005:
                    lParsers[0] = new CSharp30Parser();
                    lParsers[1] = new VB10Parser();
                    lParsers[2] = new ParserCpp();
                    break;
            }
            return lParsers;
        }
    }
}