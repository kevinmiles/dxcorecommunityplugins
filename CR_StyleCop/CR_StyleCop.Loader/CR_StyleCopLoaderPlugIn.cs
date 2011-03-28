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
        private bool styleCopCSharpRulesLoaded;
        private bool styleCopVSPackageLoaded;

        public override void InitializePlugIn()
        {
            base.InitializePlugIn();
            Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();
            foreach (Assembly assembly in assemblies)
            {
                string assemblyName = assembly.GetName().Name;
                if (assemblyName == "StyleCop")
                {
                    this.styleCopLoaded = true;
                    if (this.StyleCopLoaded)
                    {
                        LoadCRStyleCopPlugin();
                        return;
                    }
                }
                if (assemblyName == "StyleCop.CSharp")
                {
                    this.styleCopCSharpLoaded = true;
                    if (this.StyleCopLoaded)
                    {
                        LoadCRStyleCopPlugin();
                        return;
                    }
                }
                if (assemblyName == "StyleCop.CSharp.Rules")
                {
                    this.styleCopCSharpRulesLoaded = true;
                    if (this.StyleCopLoaded)
                    {
                        LoadCRStyleCopPlugin();
                        return;
                    }
                }
                if (assemblyName == "StyleCop.VSPackage")
                {
                    this.styleCopVSPackageLoaded = true;
                    if (this.StyleCopLoaded)
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
            if (loadedAssemblyName == "StyleCop")
            {
                this.styleCopLoaded = true;
                if (this.StyleCopLoaded)
                {
                    LoadCRStyleCopPlugin();
                }
                return;
            }
            if (loadedAssemblyName == "StyleCop.CSharp")
            {
                this.styleCopCSharpLoaded = true;
                if (this.StyleCopLoaded)
                {
                    LoadCRStyleCopPlugin();
                }
                return;
            }
            if (loadedAssemblyName == "StyleCop.CSharp.Rules")
            {
                this.styleCopCSharpRulesLoaded = true;
                if (this.StyleCopLoaded)
                {
                    LoadCRStyleCopPlugin();
                }
                return;
            }
            if (loadedAssemblyName == "StyleCop.VSPackage")
            {
                this.styleCopVSPackageLoaded = true;
                if (this.StyleCopLoaded)
                {
                    LoadCRStyleCopPlugin();
                }
                return;
            }
        }

        private bool StyleCopLoaded
        {
            get
            {
                return this.styleCopLoaded
                    && this.styleCopCSharpLoaded
                    && this.styleCopCSharpRulesLoaded
                    && this.styleCopVSPackageLoaded;
            }
        }
    }
}