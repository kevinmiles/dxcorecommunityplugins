using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using DevExpress.CodeRush.StructuralParser;


namespace CR_StyleCop.Tests.Helpers
{
    internal class ProjectInfo
    {
        public string Guid;
        public string Name;
        public string FilePath;
        public string Kind;
        public string ProjectLangTag;

        List<string> _ProjectReferences;

        public ProjectInfo()
        {
            _ProjectReferences = new List<string>();
        }

        public void AddProjectReference(string projectGuid)
        {
            _ProjectReferences.Add(projectGuid);
        }

        public IEnumerable<string> ProjectReferences
        {
            get { return _ProjectReferences; }
        }
    }

    internal class VS2002SolutionLoader : SolutionLoaderBase
    {
        #region Consts
        const char CHAR_Comma = ',';
        const string STR_StringQuote = "\"";
        const string STR_Equals = "=";
        const string STR_OpenParen = "(";
        const string STR_CloseParen = ")";
        const string STR_Project = "Project";
        #endregion
        // private methods...
        #region CreateProjectLoader
        protected virtual FileProjectLoaderBase CreateProjectLoader(ProjectInfo info)
        {
            LanguageID id = LanguageHelper.FromProjectLanguageTag(info.ProjectLangTag);
            switch (id)
            {
                case LanguageID.Basic:
                case LanguageID.CSharp:
                    return new Vs2002VSLangProjectLoader();
                case LanguageID.Cpp:
                case LanguageID.CppMe:
                    return new Vs2002VCProjectLoader();
            }
            return null;
        }
        #endregion
        #region GetProjectLanguageTag
        protected string GetProjectLanguageTag(string projectPath)
        {
            LanguageID lID = LanguageHelper.FromFile(projectPath);
            return LanguageHelper.GetProjectLanguageTag(lID);
        }
        #endregion
        #region RemoveStringQuotes
        protected string RemoveStringQuotes(string s)
        {
            if (s == null || s.Length == 0)
                return s;
            int lStart = s.IndexOf(STR_StringQuote);
            int lEnd = s.IndexOf(STR_StringQuote, lStart + 1);
            return s.Substring(lStart + 1, lEnd - lStart - 1);
        }
        #endregion
        #region GetProjectKind
        protected string GetProjectKind(string line)
        {
            if (line == null || line.Length == 0)
                return String.Empty;
            int lOpenParen = line.IndexOf(STR_OpenParen);
            int lCloseParen = line.IndexOf(STR_CloseParen);
            if (lOpenParen < 0 || lCloseParen < 0)
                return String.Empty;
            string lKind = line.Substring(lOpenParen + 1, lCloseParen - lOpenParen - 1);
            return RemoveStringQuotes(lKind);
        }
        #endregion
        #region GetProjectName
        protected void GetProjectName(string solutionDir, string line, out string name, out string filePath, out string guid)
        {
            name = String.Empty;
            filePath = String.Empty;
            guid = String.Empty;
            int lEqualsIdx = line.IndexOf(STR_Equals);
            if (lEqualsIdx >= 0)
            {
                string lPropertiesLine = line.Substring(lEqualsIdx + 1);
                string[] lProperties = lPropertiesLine.Split(CHAR_Comma);
                name = RemoveStringQuotes(lProperties[0]);
                string lFile = RemoveStringQuotes(lProperties[1]);
                guid = RemoveStringQuotes(lProperties[2]);
                filePath = PathUtilities.GetPath(solutionDir, lFile);
            }
        }
        #endregion
        #region GetProjectInfo
        protected ProjectInfo GetProjectInfo(string solutionDir, string line)
        {
            ProjectInfo lInfo = new ProjectInfo();
            GetProjectName(solutionDir, line, out lInfo.Name, out lInfo.FilePath, out lInfo.Guid);
            lInfo.ProjectLangTag = GetProjectLanguageTag(lInfo.FilePath);
            lInfo.Kind = GetProjectKind(line);
            return lInfo;
        }
        #endregion

        void AddProjectRefernces(ProjectInfo info, string line)
        {
            int equalsIdx = line.IndexOf(STR_Equals);
            if (equalsIdx >= 0)
            {
                string propertiesLine = line.Substring(equalsIdx + 1);
                propertiesLine = RemoveStringQuotes(propertiesLine);
                string[] refs = propertiesLine.Split(';');
                foreach (string rf in refs)
                {
                    string[] refValue = rf.Split('|');
                    info.AddProjectReference(refValue[0]);
                }
            }
        }

        #region LoadProjects(SolutionElement solution, string solutionDir, Hashtable projects)
        protected void LoadProjects(SolutionElement solution, string solutionDir, Hashtable projects)
        {
            LoadProjects(solution, solutionDir, projects, string.Empty, string.Empty);
        }
        #endregion
        #region LoadProjects(SolutionElement solution, string solutionDir, Hashtable projects, string confguration, string platform)
        protected void LoadProjects(SolutionElement solution, string solutionDir, Hashtable projects, string confguration, string platform)
        {
            IDictionaryEnumerator lEnum = projects.GetEnumerator();
            while (lEnum.MoveNext())
            {
                string lGuid = (string)lEnum.Key;
                ProjectInfo lInfo = (ProjectInfo)lEnum.Value;
                FileProjectLoaderBase projectLoader = CreateProjectLoader(lInfo);
                ProjectElement lProject = projectLoader.Load(lInfo, projects, confguration, platform);
                if (lProject != null)
                    solution.AddProject(lProject);
            }
        }
        #endregion
        #region LoadProjects(StreamReader reader, SolutionElement solution, string solutionDir)
        protected void LoadProjects(StreamReader reader, SolutionElement solution, string solutionDir)
        {
            LoadProjects(reader, solution, solutionDir, string.Empty, string.Empty);
        }
        #endregion
        #region LoadProjects(StreamReader reader, SolutionElement solution, string solutionDir, string configuration, string platform)
        protected void LoadProjects(StreamReader reader, SolutionElement solution, string solutionDir, string configuration, string platform)
        {
            ProjectInfo lInfo = null;
            Hashtable lProjects = new Hashtable();
            string lLine = reader.ReadLine();
            while (lLine != null)
            {
                if (lLine.StartsWith(STR_Project))
                {
                    lInfo = GetProjectInfo(solutionDir, lLine);
                    if (lInfo.Name != "Solution Items")
                        lProjects.Add(lInfo.Guid, lInfo);
                }

                string trimmedLine = lLine.Trim();
                if (trimmedLine.StartsWith("ProjectReferences"))
                    AddProjectRefernces(lInfo, trimmedLine);

                lLine = reader.ReadLine();
            }
            LoadProjects(solution, solutionDir, lProjects, configuration, platform);
        }
        #endregion
        #region LoadProjects(SolutionElement solution, string path)
        protected void LoadProjects(SolutionElement solution, string path)
        {
            LoadProjects(solution, path, string.Empty, string.Empty);
        }
        #endregion
        #region LoadProjects(SolutionElement solution, string path, string configuration, string platform)
        protected void LoadProjects(SolutionElement solution, string path, string configuration, string platform)
        {
            string lSolutionDir = Path.GetDirectoryName(path);
            using (StreamReader lReader = new StreamReader(path))
                LoadProjects(lReader, solution, lSolutionDir, configuration, platform);
        }
        #endregion

        // public methods...
        #region LoadFrom(string path)
        public override SolutionElement LoadFrom(string path)
        {
            return LoadFrom(path, string.Empty, string.Empty);
        }
        #endregion
        #region LoadFrom(string path, string configuration, string path)
        public override SolutionElement LoadFrom(string path, string configuration, string plargorm)
        {
            SolutionElement solution = new SolutionElement(path);
            LoadProjects(solution, path, configuration, plargorm);
            return solution;
        }
        #endregion
    }
}