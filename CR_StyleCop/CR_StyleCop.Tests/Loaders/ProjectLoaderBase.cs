using System;
using System.Collections;
using System.IO;
using System.Xml;
using DX_CPPLanguage;
using DevExpress.CodeRush.StructuralParser;

namespace CR_StyleCop.Tests.Helpers
{
    public abstract class FileProjectLoaderBase
    {
        public abstract ProjectElement Load(ProjectInfo info, Hashtable projects);
        public abstract ProjectElement Load(ProjectInfo info, Hashtable projects, string configuration, string platform);
    }

    public sealed class PathUtilities
    {
        public static string GetPath(string basePath, string relative)
        {
            if (string.IsNullOrEmpty(basePath) || string.IsNullOrEmpty(relative))
                return string.Empty;

            if (!relative.StartsWith(".\\") && !relative.StartsWith("..\\"))
                relative = ".\\" + relative;

            Uri lBaseUri = new Uri(basePath + "\\.");
            Uri lUri = new Uri(lBaseUri, relative);
            return lUri.LocalPath;
        }
    }

    public class Vs2002VSLangProjectLoader : FileProjectLoaderBase
    {
        #region Consts
        protected const string STR_ImportsQueryFormat = "VisualStudioProject/{0}/Build/Imports/Import";
        protected const string STR_ReferencesQueryFormat = "VisualStudioProject/{0}/Build/References/Reference";
        protected const string STR_FilesQueryFormat = "VisualStudioProject/{0}/Files/Include/File";
        protected const string STR_Project = "Project";
        protected const string STR_Namespace = "Namespace";
        protected const string STR_Name = "Name";
        protected const string STR_AssemblyName = "AssemblyName";
        protected const string STR_RelPath = "RelPath";
        protected const string STR_SettingsQueryFormat = "VisualStudioProject/{0}/Build/Settings";
        protected const string STR_RootNamespace = "RootNamespace";
        #endregion

        #region LoadImportedNamespaces
        protected virtual void LoadImportedNamespaces(ProjectElement project, XmlDocument doc, string projectDir, string projectLangTag)
        {
            string lImportsQuery = String.Format(STR_ImportsQueryFormat, projectLangTag);
            XmlNodeList lImports = doc.SelectNodes(lImportsQuery);
            int lCount = lImports.Count;
            for (int i = 0; i < lCount; i++)
            {
                System.Xml.XmlNode lImportNode = lImports[i];
                System.Xml.XmlNode lNamespace = lImportNode.Attributes.GetNamedItem(STR_Namespace);
                string lNamespaceStr = lNamespace.Value;
                project.AddImportedNamespace(lNamespaceStr);
            }
        }
        #endregion
        #region LoadReferences
        protected virtual void LoadReferences(ProjectElement project, Hashtable projects, XmlDocument doc, string projectDir, string projectLangTag)
        {
            string lReferencesQuery = String.Format(STR_ReferencesQueryFormat, projectLangTag);
            XmlNodeList lReferences = doc.SelectNodes(lReferencesQuery);
            int lCount = lReferences.Count;
            for (int i = 0; i < lCount; i++)
            {
                System.Xml.XmlNode lReferenceNode = lReferences[i];
                System.Xml.XmlNode lName = lReferenceNode.Attributes.GetNamedItem(STR_Name);
                System.Xml.XmlNode lAssemblyName = lReferenceNode.Attributes.GetNamedItem(STR_AssemblyName);
                if (lAssemblyName != null)
                {
                    string lAssemblyNameStr = lAssemblyName.Value;
                    string lPath = FrameworkHelper.GetAssemblyPath(lAssemblyNameStr);
                    if (lPath == null || lPath.Length == 0)
                    {
                        System.Xml.XmlNode lHintPath = lReferenceNode.Attributes.GetNamedItem("HintPath");
                        lPath = lHintPath.Value;
                        lPath = PathUtilities.GetPath(projectDir, lPath);
                        if (File.Exists(lPath))
                        {
                            AssemblyReference lRef = new AssemblyReference(lPath);
                            project.AddReference(lRef);
                        }
                    }
                    else
                        project.AddReferenceByName(lAssemblyNameStr);
                }
                else
                {
                    System.Xml.XmlNode lProjectRef = lReferenceNode.Attributes.GetNamedItem(STR_Project);
                    string lGuid = lProjectRef.Value;
                    ProjectInfo lInfo = projects[lGuid] as ProjectInfo;
                    if (lInfo != null)
                    {
                        AssemblyReference lRef = new AssemblyReference(String.Empty);
                        lRef.SetSourceProjectFullName(lInfo.FilePath);
                        project.AddReference(lRef);
                    }
                }
            }
        }
        #endregion
        #region GetFilesQuery
        protected virtual string GetFilesQuery(string projectLang)
        {
            return String.Format(STR_FilesQueryFormat, projectLang);
        }
        #endregion
        #region LoadFiles
        protected virtual void LoadFiles(ProjectElement project, XmlDocument lDoc, string lProjectDir, string lProjectLangTag)
        {
            string lFilesQuery = GetFilesQuery(lProjectLangTag);
            XmlNodeList lFiles = lDoc.SelectNodes(lFilesQuery);
            int lCount = lFiles.Count;
            for (int i = 0; i < lCount; i++)
            {
                System.Xml.XmlNode lFileNode = lFiles[i];
                System.Xml.XmlNode lRelPath = lFileNode.Attributes.GetNamedItem(STR_RelPath);
                string lFilePath = Path.Combine(lProjectDir, lRelPath.Value);
                SourceFile proxy = project.AddDiskFile(lFilePath);
                proxy.BuildProjectSymbols();
            }
        }
        #endregion
        #region LoadRootNamespace
        protected virtual void LoadRootNamespace(ProjectElement project, XmlDocument lDoc, string lProjectDir, string lProjectLangTag)
        {
            string lSettingsQuery = String.Format(STR_SettingsQueryFormat, lProjectLangTag);
            XmlNodeList lSettings = lDoc.SelectNodes(lSettingsQuery);
            int lCount = lSettings.Count;
            if (lCount > 0)
            {
                System.Xml.XmlNode lSettingsNode = lSettings[0];
                System.Xml.XmlNode lRootNamespace = lSettingsNode.Attributes.GetNamedItem(STR_RootNamespace);
                if (lRootNamespace != null)
                    project.SetRootNamespace(lRootNamespace.Value);
            }
        }
        #endregion

        protected virtual ProjectElement CreateProjectElement(ProjectInfo info)
        {
            if (info.ProjectLangTag != "VisualBasic")
                return new ProjectElement(info.Name, info.FilePath, info.Kind);

            ProjectElement projectElement = new DX_VBLanguage.VBProjectElement(info.Name);
            projectElement.SetFullName(info.FilePath);
            projectElement.SetKind(info.Kind);
            projectElement.SetLanguage(LanguageID.Basic.ToString());
            return projectElement;
        }

        #region LoadProject
        protected virtual ProjectElement LoadProject(ProjectInfo info, Hashtable projects, string configuration, string platform)
        {
            ProjectElement projectElement = CreateProjectElement(info);

            XmlDocument lDoc = new XmlDocument();
            lDoc.Load(info.FilePath);

            string lProjectDir = Path.GetDirectoryName(info.FilePath);
            LoadRootNamespace(projectElement, lDoc, lProjectDir, info.ProjectLangTag);
            LoadReferences(projectElement, projects, lDoc, lProjectDir, info.ProjectLangTag);
            LoadImportedNamespaces(projectElement, lDoc, lProjectDir, info.ProjectLangTag);
            LoadFiles(projectElement, lDoc, lProjectDir, info.ProjectLangTag);
            return projectElement;
        }
        #endregion
        #region Load
        public override ProjectElement Load(ProjectInfo info, Hashtable projects)
        {
            return LoadProject(info, projects, string.Empty, string.Empty);
        }
        #endregion
        #region Load
        public override ProjectElement Load(ProjectInfo info, Hashtable projects, string configuration, string platform)
        {
            return LoadProject(info, projects, configuration, platform);
        }
        #endregion
    }

    public class Vs2002VCProjectLoader : Vs2002VSLangProjectLoader
    {
        #region LoadReferences
        protected override void LoadReferences(ProjectElement project, Hashtable projects, XmlDocument doc, string projectDir, string projectLangTag)
        {
            // Vs2002 VC doesn't stores assembly references, instead #using <dll> directive is used.
        }
        #endregion
        #region LoadFilterFiles
        protected virtual void LoadFilterFiles(ProjectElement project, XmlDocument lDoc, string lProjectDir, string lProjectLangTag)
        {
            string query = "VisualStudioProject/Files/Filter";
            XmlNodeList filters = lDoc.SelectNodes(query);
            int count = filters.Count;
            for (int i = 0; i < count; i++)
            {
                System.Xml.XmlNode filterNode = filters[i];
                XmlNodeList files = filterNode.SelectNodes("File");
                int filesCount = files.Count;
                for (int j = 0; j < filesCount; j++)
                {
                    System.Xml.XmlNode fileNode = files[j];
                    System.Xml.XmlNode relPath = fileNode.Attributes.GetNamedItem("RelativePath");
                    string lFilePath = Path.Combine(lProjectDir, relPath.Value);
                    project.AddDiskFile(lFilePath);
                }
            }
        }
        #endregion
        #region LoadRootFiles
        protected virtual void LoadRootFiles(ProjectElement project, XmlDocument lDoc, string lProjectDir, string lProjectLangTag)
        {
            string query = "VisualStudioProject/Files/File";
            XmlNodeList files = lDoc.SelectNodes(query);
            int count = files.Count;
            for (int i = 0; i < count; i++)
            {
                System.Xml.XmlNode fileNode = files[i];
                System.Xml.XmlNode relPath = fileNode.Attributes.GetNamedItem("RelativePath");
                string lFilePath = Path.Combine(lProjectDir, relPath.Value);
                project.AddDiskFile(lFilePath);
            }
        }
        #endregion
        #region LoadFiles
        protected override void LoadFiles(ProjectElement project, XmlDocument lDoc, string lProjectDir, string lProjectLangTag)
        {
            LoadRootFiles(project, lDoc, lProjectDir, lProjectLangTag);
            LoadFilterFiles(project, lDoc, lProjectDir, lProjectLangTag);
        }
        #endregion
        #region LoadImportedNamespaces
        protected override void LoadImportedNamespaces(ProjectElement project, XmlDocument doc, string projectDir, string projectLangTag)
        {
            base.LoadImportedNamespaces(project, doc, projectDir, projectLangTag);
        }
        #endregion
        #region LoadRootNamespace
        protected override void LoadRootNamespace(ProjectElement project, XmlDocument lDoc, string lProjectDir, string lProjectLangTag)
        {
            base.LoadRootNamespace(project, lDoc, lProjectDir, lProjectLangTag);
        }
        #endregion
        #region LoadCompileUnits
        protected virtual void LoadCompileUnits(ProjectElement project, XmlDocument lDoc, string lProjectDir, string lProjectLangTag)
        {
        }
        #endregion
        #region LoadProject
        protected override ProjectElement LoadProject(ProjectInfo info, Hashtable projects, string configuration, string platform)
        {
            XmlDocument lDoc = new XmlDocument();
            lDoc.Load(info.FilePath);
            string lProjectDir = Path.GetDirectoryName(info.FilePath);

            CppProjectElement lProject = new CppProjectElement(info.Name);
            lProject.SetFullName(info.FilePath);
            LoadReferences(lProject, projects, lDoc, lProjectDir, info.ProjectLangTag);
            LoadImportedNamespaces(lProject, lDoc, lProjectDir, info.ProjectLangTag);
            LoadFiles(lProject, lDoc, lProjectDir, info.ProjectLangTag);
            LoadRootNamespace(lProject, lDoc, lProjectDir, info.ProjectLangTag);
            LoadCompileUnits(lProject, lDoc, lProjectDir, info.ProjectLangTag);
            return lProject;
        }
        #endregion
    }
}