using System;
using System.IO;

using DevExpress.CodeRush.StructuralParser;

namespace CR_StyleCop.Tests.Helpers
{
    public class SolutionParser
    {
        string _Path;
        SolutionElement _SolutionElement;
        bool _SkipMetadataLoading;
        string _Configuration;
        string _Platform;

        // constructors...
        public SolutionParser(string path)
            : this(path, false)
        {
        }
        public SolutionParser(string path, string configuration, string platform)
            : this(path, false, configuration, platform)
        {
        }
        public SolutionParser(string path, bool skipMetadataLoading)
            : this(path, skipMetadataLoading, string.Empty, string.Empty)
        {
        }
        public SolutionParser(string path, bool skipMetadataLoading, string configuration, string platform)
        {
            _Path = path;
            _SkipMetadataLoading = skipMetadataLoading;
            _Configuration = configuration;
            _Platform = platform;
        }

        // private methods...
        SolutionElement OpenSolution(string path)
        {
            SolutionLoaderBase loader = GetSolutionLoader(path);
            SolutionElement solution = null;
            if (string.IsNullOrEmpty(_Configuration) || string.IsNullOrEmpty(_Platform))
                solution = loader.LoadFrom(path);
            else
                solution = loader.LoadFrom(path, _Configuration, _Platform);
            if (!_SkipMetadataLoading)
                AssemblyReferenceManager.Instance.LoadAllAssemblies();
            SolutionParseController controller = new SolutionParseController();
            controller.ParseSolution(solution);
            SyncProjectSymbols(solution);
            return solution;
        }

        SolutionLoaderBase GetSolutionLoader(string path)
        {
            using (StreamReader reader = new StreamReader(path))
            {
                string line = FindSolutionFormatLine(reader);
                if (line == null)
                    return null;
                return ParserHelper.GetSolutionLoader(line);
            }
        }

        string FindSolutionFormatLine(StreamReader reader)
        {
            int lineCount = 10;
            bool formatLine = false;
            string line = reader.ReadLine();
            while (line != null && lineCount > 0)
            {
                if (line.IndexOf("Microsoft Visual Studio Solution File") >= 0)
                {
                    formatLine = true;
                    break;
                }
                lineCount--;
                line = reader.ReadLine();
            }
            if (formatLine)
                return line;
            return null;
        }

        void SyncProjectSymbols(SolutionElement solution)
        {
            if (solution == null)
                return;
            NodeList projects = solution.ProjectElements;
            int count = projects.Count;
            if (count == 0)
                return;
            for (int i = 0; i < count; i++)
            {
                ProjectElement project = (ProjectElement)projects[i];
                project.SyncProjectSymbolsAfterWait();
            }
        }

        // public methods...
        public void CloseSolution()
        {
            if (_SolutionElement != null)
            {
                _SolutionElement.CloseSolution();
                _SolutionElement = null;
            }
        }

        public SolutionElement GetParsedSolution()
        {
            if (_SolutionElement == null)
                _SolutionElement = OpenSolution(Path);
            return _SolutionElement;
        }

        // public properties...
        public string Path
        {
            get { return _Path; }
        }

        public string Configuration
        {
            get { return _Configuration; }
            set { _Configuration = value; }
        }
        public string Platform
        {
            get { return _Platform; }
            set { _Platform = value; }
        }
    }
}