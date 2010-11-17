namespace CR_StyleCop.Loader
{
    using System;
    using System.Reflection;
    using DevExpress.CodeRush.Common;
    using DevExpress.CodeRush.Core;
    using DevExpress.CodeRush.PlugInCore;

    public partial class CR_StyleCopLoaderPlugIn : StandardPlugIn
    {
        public override void InitializePlugIn()
        {
            base.InitializePlugIn();
            Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();
            foreach (Assembly assembly in assemblies)
            {
                if (assembly.GetName().Name == "Microsoft.StyleCop")
                {
                    LoadCRStyleCopPlugin();
                    return;
                }
            }

            AppDomain.CurrentDomain.AssemblyLoad += this.CurrentDomainAssemblyLoad;
        }

        private static void LoadCRStyleCopPlugin()
        {
            ILoaderEngine loader = CodeRush.LoaderEngine;

            foreach (IPlugInAssembly plugin in loader.CommunityPlugIns)
            {
                if (plugin.AssemblyName == "CR_StyleCop")
                {
                    loader.ForcedAssemblyLoad(plugin);
                }
            }
        }

        private void CurrentDomainAssemblyLoad(object sender, AssemblyLoadEventArgs e)
        {
            if (e.LoadedAssembly.GetName().Name == "Microsoft.StyleCop")
            {
                LoadCRStyleCopPlugin();
            }
        }
    }
}