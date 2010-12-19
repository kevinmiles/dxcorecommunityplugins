namespace CR_StyleCop.Loader
{
    using System;
    using System.Reflection;
    using DevExpress.CodeRush.Common;
    using DevExpress.CodeRush.Core;
    using DevExpress.CodeRush.PlugInCore;

    public partial class CR_StyleCopLoaderPlugIn : StandardPlugIn
    {
        private bool styleCopLoaded;
        private bool styleCopCSharpLoaded;

        public override void InitializePlugIn()
        {
            base.InitializePlugIn();
            Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();
            foreach (Assembly assembly in assemblies)
            {
                string assemblyName = assembly.GetName().Name;
                if (assemblyName == "Microsoft.StyleCop")
                {
                    this.styleCopLoaded = true;
                    if (this.styleCopCSharpLoaded)
                    {
                        LoadCRStyleCopPlugin();
                        return;
                    }
                }
                if (assemblyName == "Microsoft.StyleCop.CSharp")
                {
                    this.styleCopCSharpLoaded = true;
                    if (this.styleCopLoaded)
                    {
                        LoadCRStyleCopPlugin();
                        return;
                    }
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
            string loadedAssemblyName = e.LoadedAssembly.GetName().Name;
            if (loadedAssemblyName == "Microsoft.StyleCop")
            {
                this.styleCopLoaded = true;
                if (this.styleCopCSharpLoaded)
                {
                    LoadCRStyleCopPlugin();
                    return;
                }
            }
            if (loadedAssemblyName == "Microsoft.StyleCop.CSharp")
            {
                this.styleCopCSharpLoaded = true;
                if (this.styleCopLoaded)
                {
                    LoadCRStyleCopPlugin();
                    return;
                }
            }
        }
    }
}