using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Microsoft.Build.BuildEngine;
using DevExpress.CodeRush.StructuralParser;
using CR_StyleCop.Tests.Loaders;

namespace CR_StyleCop.Tests.Helpers
{
    public class VS2005VSLangProjectLoader : Vs2002VSLangProjectLoader
    {
        private const char CHAR_Comma = ',';
        private const char CHAR_SemiColon = ';';

        private const string STR_SolutionFolder = "{2150E333-8FDC-42A3-9474-1A3956D46DE8}";
        private const string STR_HintPath = "HintPath";
        private const string STR_OutputPath = "OutputPath";
        private const string STR_DefineConstants = "DefineConstants";
        private const string STR_ProjectTypeGuids = "ProjectTypeGuids";
        private const string STR_Reference = "Reference";
        private const string STR_Aliases = "Aliases";
        private const string STR_ProjectReference = "ProjectReference";
        private const string STR_Import = "Import";
        private const string STR_Compile = "Compile";
        private const string STR_ApplicationDefinition = "ApplicationDefinition";
        private const string STR_Page = "Page";
        private const string STR_Content = "Content";

        // private methods...
        private bool IsSilverlightProject(ProjectElement project)
        {
            return project != null && project.ContainsProjectTypeGuid(ProjectTypeGuidConstants.Silverlight);
        }

        private string GetAssemblyPath(string assemblyFileName, bool isSilverlight)
        {
            string path = String.Empty;
            if (isSilverlight)
            {
                path = FrameworkHelper.GetSilverlightAssemblyPath(assemblyFileName);

            }            //if (string.IsNullOrEmpty(path))
            //  path = FrameworkHelper.GetAssemblyPath(assemblyFileName);

            return path;
        }

        private string GetReferenceDllName(BuildItem item)
        {
            string spec = item.FinalItemSpec;
            int commaIndex = spec.IndexOf(CHAR_Comma);
            if (commaIndex >= 0)
            {
                return spec.Substring(0, commaIndex);
            }
            return spec;
        }

        private string GetAssemblyPathFromVSInstalDir(BuildItem item)
        {
            string name = GetReferenceDllName(item);
            string[] installDirs = FrameworkHelper.GetVSInstallFoldersPaths();
            string path = FrameworkHelper.GetAssemblyPath(name, installDirs);
            if (File.Exists(path))
            {
                return path;
            }
            return string.Empty;
        }

        private string[] ReversePaths(string[] paths)
        {
            if (paths == null || paths.Length == 0)
            {
                return new string[0];
            }
            int length = paths.Length;
            string[] result = new string[length];
            for (int i = 0; i < length; i++)
            {
                result[i] = paths[length - i - 1];
            }
            return result;
        }

        private string GetAssemblyPathFromAssemblyFolders(MsBuildProjectLoader msBuildLoader, BuildItem item)
        {
            string spec = GetReferenceDllName(item);
            string[] assemblyFoldersPaths = FrameworkHelper.GetFrameworkPaths(msBuildLoader.TargetFramework);
            assemblyFoldersPaths = ReversePaths(assemblyFoldersPaths);
            string path = FrameworkHelper.GetAssemblyPath(spec, assemblyFoldersPaths);
            if (File.Exists(path))
            {
                return path;

            }
            assemblyFoldersPaths = FrameworkHelper.GetAssemblyFoldersPaths(msBuildLoader.TargetFramework);
            path = FrameworkHelper.GetAssemblyPath(spec, assemblyFoldersPaths);
            if (File.Exists(path))
            {
                return path;

            }
            try
            {
                Assembly assembly = Assembly.Load(item.FinalItemSpec);
                if (assembly != null)
                {
                    return assembly.Location;
                }
            }
            catch
            {
            }

            try
            {
                Assembly assembly = Assembly.LoadWithPartialName(item.FinalItemSpec);
                if (assembly != null)
                {
                    return assembly.Location;
                }
            }
            catch
            {
            }

            return string.Empty;
        }

        private string GetAssemblyPathFromHintPath(BuildItem item, string projectDir)
        {
            string path = item.GetMetadata(STR_HintPath);
            path = PathUtilities.GetPath(projectDir, path);
            if (File.Exists(path))
            {
                return path;
            }
            return null;
        }

        private string GetAssemblyPathFromInclude(BuildItem item, string projectDir)
        {
            string path = item.Include;
            path = PathUtilities.GetPath(projectDir, path);
            if (File.Exists(path))
            {
                return path;
            }
            return null;
        }

        private string GetAssemblyPathFromProjectOutput(MsBuildProjectLoader msBuildLoader, BuildItem item, string projectDir)
        {
            string outputPath = msBuildLoader.Project.GetEvaluatedProperty(STR_OutputPath);
            if (string.IsNullOrEmpty(outputPath))
            {
                return null;

            }
            outputPath = PathUtilities.GetPath(projectDir, outputPath);
            string path = Path.Combine(outputPath, GetReferenceDllName(item) + ".dll");
            if (File.Exists(path))
            {
                return path;
            }
            return null;
        }

        private string GetReferencePath(MsBuildProjectLoader msBuildLoader, BuildItem item, string projectDir)
        {
            string path = GetAssemblyPathFromHintPath(item, projectDir);

            if (string.IsNullOrEmpty(path))
            {
                path = GetAssemblyPathFromProjectOutput(msBuildLoader, item, projectDir);

            }
            if (string.IsNullOrEmpty(path))
            {
                path = GetAssemblyPathFromAssemblyFolders(msBuildLoader, item);

            }
            if (string.IsNullOrEmpty(path))
            {
                path = GetAssemblyPathFromVSInstalDir(item);

            }
            if (string.IsNullOrEmpty(path))
            {
                path = GetAssemblyPathFromInclude(item, projectDir);

            }
            return path;
        }

        private AssemblyReference AddReference(ProjectElement project, MsBuildProjectLoader msBuildLoader, BuildItem item, string projectDir, string aliases)
        {
            AssemblyReference reference = null;
            string dllName = GetReferenceDllName(item);
            bool isSilverlight = IsSilverlightProject(project);
            string path = GetAssemblyPath(dllName, isSilverlight);
            if (String.IsNullOrEmpty(path))
            {
                path = GetReferencePath(msBuildLoader, item, projectDir);

            }
            if (!string.IsNullOrEmpty(path))
            {
                reference = new AssemblyReference(path);
                reference.SetAliasesString(aliases);
                project.AddReference(reference);
            }
            else
            {
                reference = project.AddReferenceByName(item.FinalItemSpec, aliases);

            }
            return reference;
        }

        private void LoadBuildItems(ProjectElement project, string projectDir, IEnumerable<BuildItem> items)
        {
            if (project == null || string.IsNullOrEmpty(projectDir) || items == null)
            {
                return;
            }
            foreach (BuildItem item in items)
            {
                string spec = item.FinalItemSpec;
                string filePath = Path.Combine(projectDir, spec);
                SourceFile proxy = project.AddDiskFile(filePath);
                proxy.BuildProjectSymbols();
            }
        }

        private void LoadProjectDefines(ProjectElement result, MsBuildProjectLoader msBuildLoader, string configuration, string platform)
        {
            string definesStr = string.Empty;
            if (string.IsNullOrEmpty(configuration) || string.IsNullOrEmpty(platform))
            {
                definesStr = msBuildLoader.GetProperty(STR_DefineConstants);
            }
            else
            {
                definesStr = msBuildLoader.GetPropertyWithCondition(string.Format(" '$(Configuration)|$(Platform)' == '{0}|{1}' ", configuration, platform), STR_DefineConstants);
            }
            if (string.IsNullOrEmpty(definesStr))
            {
                return;

            }
            result.SetDefines(definesStr.Split(CHAR_SemiColon));
        }

        private void LoadProjectTypeGuids(ProjectElement result, MsBuildProjectLoader msBuildLoader)
        {
            string projectTypeGuids = msBuildLoader.GetProperty(STR_ProjectTypeGuids);
            if (string.IsNullOrEmpty(projectTypeGuids))
            {
                return;

            }
            result.SetProjectTypeGuids(projectTypeGuids);
        }

        private void LoadNotImportStdLibOption(ProjectElement result, MsBuildProjectLoader msBuildLoader)
        {
            if (result == null)
            {
                return;

            }
            if (!result.ContainsProjectTypeGuid(ProjectTypeGuidConstants.WindowsCSharp))
            {
                string extension = Path.GetExtension(result.FilePath);
                if (extension != ".csproj")
                {
                    return;
                }
            }

            result.NotImportStdLib = msBuildLoader.NotImportStdLib;
            if (msBuildLoader.NotImportStdLib)
            {
                result.NeedLoadCoreAssembly = false;
            }
        }

        private void LoadReferences(ProjectElement result, MsBuildProjectLoader msBuildLoader, string projectDir)
        {
            IEnumerable<BuildItem> referenceItems = msBuildLoader.GetProjectItems(STR_Reference);
            foreach (BuildItem item in referenceItems)
            {
                string aliases = item.GetEvaluatedMetadata(STR_Aliases);
                AssemblyReference reference = AddReference(result, msBuildLoader, item, projectDir, aliases);
                if (reference == null)
                {
                    continue;
                }
            }
        }

        private void LoadProjectReferences(ProjectElement result, MsBuildProjectLoader msBuildLoader, Hashtable projects)
        {
            IEnumerable<BuildItem> projectReferenceItems = msBuildLoader.GetProjectItems(STR_ProjectReference);
            foreach (BuildItem item in projectReferenceItems)
            {
                string prj = item.GetMetadata(STR_Project);
                ProjectInfo projectInfo = projects[prj] as ProjectInfo;
                if (projectInfo == null)
                {
                    continue;

                }
                AssemblyReference projectRef = new AssemblyReference(String.Empty);
                projectRef.SetSourceProjectFullName(projectInfo.FilePath);
                result.AddReference(projectRef);
            }
        }

        private void LoadImportedNamespaces(ProjectElement result, MsBuildProjectLoader msBuildLoader)
        {
            IEnumerable<BuildItem> importItems = msBuildLoader.GetProjectItems(STR_Import);
            foreach (BuildItem item in importItems)
            {
                result.AddImportedNamespace(item.FinalItemSpec);
            }
        }

        private void LoadFiles(ProjectElement result, MsBuildProjectLoader msBuildLoader, string projectDir)
        {
            IEnumerable<BuildItem> compileItems = msBuildLoader.GetProjectItems(STR_Compile);
            LoadBuildItems(result, projectDir, compileItems);

            IEnumerable<BuildItem> appDefinitionItems = msBuildLoader.GetProjectItems(STR_ApplicationDefinition);
            LoadBuildItems(result, projectDir, appDefinitionItems);

            IEnumerable<BuildItem> pageItems = msBuildLoader.GetProjectItems(STR_Page);
            LoadBuildItems(result, projectDir, pageItems);

            IEnumerable<BuildItem> contentItems = msBuildLoader.GetProjectItems(STR_Content);
            LoadBuildItems(result, projectDir, contentItems);
        }

        private ProjectElement LoadWebSiteProject(ProjectInfo info, Hashtable projects)
        {
            ProjectElement result = CreateProjectElement(info);
            result.SetIsWebSite(true);

            //result.SetTargetFramework(msBuildLoader.TargetFramework);

            LoadWebSiteProjectReferences(result, info, projects);
            LoadFilesFromPath(result, info.FilePath);

            LoadWebSiteReferences(result, info);

            return result;
        }

        private string GetAssemblyPath(string name)
        {
            if (name == null || name.Length == 0)
            {
                return null;
            }
            string path = null;
            try
            {
                path = DevExpress.DXCore.AssemblyResolver.Resolver.Instance.GetAssemblyPath(name);
            }
            catch (Exception)
            {
                path = null;
            }
            return path;
        }

        private void LoadWebSiteReferences(ProjectElement result, ProjectInfo info)
        {
            WebConfigInfo webConfigInfo = WebConfigInfo.Create(result);
            if (webConfigInfo.Assemblies == null)
            {
                return;
            }
            foreach (string assembly in webConfigInfo.Assemblies)
            {
                string path = assembly;
                if (!File.Exists(path))
                {
                    path = GetAssemblyPath(assembly);
                }
                if (!String.IsNullOrEmpty(path))
                {
                    AssemblyReference resultReference = new AssemblyReference(path);
                    result.AddReference(resultReference);
                }
            }
        }

        private ProjectInfo FindProjectByGuid(Hashtable projects, string guid)
        {
            foreach (DictionaryEntry entry in projects)
            {
                ProjectInfo info = entry.Value as ProjectInfo;
                if (info.Guid == guid)
                {
                    return info;
                }
            }
            return null;
        }

        private void LoadWebSiteProjectReferences(ProjectElement result, ProjectInfo info, Hashtable projects)
        {
            foreach (string prjRefGuid in info.ProjectReferences)
            {
                ProjectInfo prfRefInfo = FindProjectByGuid(projects, prjRefGuid);
                if (prfRefInfo == null)
                {
                    continue;

                }
                AssemblyReference projectRef = new AssemblyReference(String.Empty);
                projectRef.SetSourceProjectFullName(prfRefInfo.FilePath);
                result.AddReference(projectRef);
            }
        }

        private void LoadFilesFromPath(ProjectElement project, string path)
        {
            string[] files = Directory.GetFiles(path);
            foreach (string filePath in files)
            {
                SourceFile proxy = project.AddDiskFile(filePath);
                proxy.BuildProjectSymbols();
            }

            string[] directories = Directory.GetDirectories(path);
            foreach (string directory in directories)
            {
                LoadFilesFromPath(project, directory);
            }
        }

        // protected methods...
        protected override ProjectElement LoadProject(ProjectInfo info, Hashtable projects, string configuration, string platform)
        {
            if (info.Kind == STR_SolutionFolder)
            {
                return null;

            }
            if (info.Kind == ProjectTypeGuidConstants.WebSite)
            {
                return LoadWebSiteProject(info, projects);

            }
            string projectDir = Path.GetDirectoryName(info.FilePath);
            ProjectElement result = CreateProjectElement(info);

            MsBuildProjectLoader msBuildLoader = new MsBuildProjectLoader(info.FilePath);

            result.OptionStrict = msBuildLoader.OptionStrict;
            result.SetRootNamespace(msBuildLoader.RootNamespace);
            result.SetAssemblyName(msBuildLoader.AssemblyName);
            result.SetTargetFramework(msBuildLoader.TargetFramework);
            LoadProjectTypeGuids(result, msBuildLoader);
            LoadNotImportStdLibOption(result, msBuildLoader);
            LoadProjectDefines(result, msBuildLoader, configuration, platform);
            LoadReferences(result, msBuildLoader, projectDir);
            LoadProjectReferences(result, msBuildLoader, projects);
            LoadImportedNamespaces(result, msBuildLoader);
            LoadFiles(result, msBuildLoader, projectDir);

            return result;
        }
    }
}
