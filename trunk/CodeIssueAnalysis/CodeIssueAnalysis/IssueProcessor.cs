using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Text.RegularExpressions;
using DevExpress.CodeRush.Core;
using DevExpress.CodeRush.PlugInCore;
using DevExpress.CodeRush.StructuralParser;
using System.Text;
using System.IO;

namespace CodeIssueAnalysis
{
    internal class IssueProcessor
    {
        bool fullSolution;
        public volatile bool shutdown;

        public event EventHandler<ResultsArgs> Results;
        public event EventHandler<ProcessingArgs> ProcessingFile;
        public event EventHandler<ErrorArgs> Error;

        internal void OnResults(ResultsArgs e)
        {
            if (Results != null)
            {
                Results(this, e);
            }
        }

        internal void OnProcessingFile(ProcessingArgs e)
        {
            if (ProcessingFile != null)
            {
                ProcessingFile(this, e);
            }
        }

        internal void OnError(ErrorArgs e)
        {
            if (Error != null)
            {
                Error(this, e);
            }
        }

        internal class ResultsArgs : EventArgs
        {
            public DataTable dt { get; private set; }

            public ResultsArgs(DataTable dt)
            {
                this.dt = dt;
            }
        }

        internal class ProcessingArgs : EventArgs
        {
            public int FileCount { get; private set; }
            public int CurrentFile { get; private set; }

            public ProcessingArgs(int fileCount, int currentFile)
            {
                this.FileCount = fileCount;
                this.CurrentFile = currentFile;
            }
        }

        internal class ErrorArgs : EventArgs
        {
            public Exception Error { get; private set; }

            public ErrorArgs(Exception error)
            {
                this.Error = error;
            }
        }

        internal IssueProcessor(bool fullSolution)
        {
            this.fullSolution = fullSolution;
        }        

        internal void Run()
        {
            DataTable dt = CreateTable();
            IssueServices issueService = CodeRush.Issues;            

            try
            {
                int fileCount = GetFileCount();
                int currentFile = 0;

                foreach (ProjectElement project in CodeRush.Source.SolutionNode.AllProjects)
                {                    
                    if (CodeRush.Source.ActiveProject == project || fullSolution == true)
                    {
                        foreach (SourceFile file in project.AllFiles)
                        {
                            //early abort still ends with OnResults
                            if (shutdown)
                                break;
                            OnProcessingFile(new ProcessingArgs(fileCount, currentFile));
                            currentFile++;
                            CheckFile(issueService, dt, file);
                        }
                    }
                }
            }
            catch (Exception err)
            {
                OnError(new ErrorArgs(err));
            }

            OnResults(new ResultsArgs(dt));
        }

        private int GetFileCount()
        {
            int fileCount = 0;
            foreach (ProjectElement project in CodeRush.Source.SolutionNode.AllProjects)
            {
                if (CodeRush.Source.ActiveProject == project || fullSolution == true)
                {
                    foreach (SourceFile file in project.AllFiles)
                    {
                        fileCount++;
                    }
                }
            }
            return fileCount;
        }

        private static void CheckFile(IssueServices issueService, DataTable dt, SourceFile file)
        {
            //test included by content or file name
            if (FileNamePatternMatch(file, CodeIssueOptions.GetInclusions()) ||
               ContentPatternMatch(file, CodeIssueOptions.GetContentInclusions()))
            {
                //exlusions only need to be tested if file is included
                if (!FileNamePatternMatch(file, CodeIssueOptions.GetExclusions()) &&
                    !ContentPatternMatch(file, CodeIssueOptions.GetContentExclusions()))
                {
                    try
                    {
                        AddToDataTable(issueService.CheckCodeIssues(file), dt, file);
                    }
                    catch (Exception err)
                    {
                        Debug.Assert(false, "Failed To Check Issues", err.Message);
                    }
                }
            }
        }

        private static bool FileNamePatternMatch(SourceFile file, List<string> patterns)
        {
            foreach (string pattern in patterns)
            {
                if ((new Regex(pattern)).Match(file.Name).Success)
                    return true;
            }
            return false;
        }

        private static bool ContentPatternMatch(SourceFile file, List<string> patterns)
        {
            //test file content avoiding multiple file reads
            if (patterns.Count > 0)
            {               
                string fileText = GetFileText(file);
                Debug.Assert(fileText.Length != 0, "Empty FileText");

                foreach (string pattern in patterns)
                {
                    if ((new Regex(pattern)).Match(fileText).Success)
                        return true;
                }
            }
            return false;
        }

        private static string GetFileText(SourceFile file)
        {
            //read file without locking
            string content = "";

            using (FileStream fs = new FileStream(file.Name, FileMode.Open, FileAccess.Read))
            using (StreamReader sr = new StreamReader(fs))
            {
                content = sr.ReadToEnd();
                if (sr != null)
                    sr.Close();
                if (fs != null)
                    fs.Close();
            }

            return content;            
        }

        private static DataTable CreateTable()
        {
            DataTable dt = new DataTable(typeof(CodeIssue).Name);

            dt.Columns.Add("Type", typeof(String));
            dt.Columns.Add("Message", typeof(String));
            dt.Columns.Add("Line", typeof(Int32));
            dt.Columns.Add("Solution", typeof(String));
            dt.Columns.Add("Project", typeof(String));
            dt.Columns.Add("File", typeof(String));
            dt.Columns.Add("Source File", typeof(SourceFile));
            dt.Columns.Add("Range", typeof(SourceRange));
            return dt;
        }

        private static DataTable AddToDataTable(IEnumerable<CodeIssue> codeIssues, DataTable dt, SourceFile file)
        {
            foreach (var issue in codeIssues)
            {
                var values = new object[dt.Columns.Count];
                values[0] = issue.Type.ToString();
                values[1] = issue.Message;
                values[2] = issue.Range.Start.Line;
                values[3] = System.IO.Path.GetFileName(file.Solution.Name);
                values[4] = file.Project.Name;
                values[5] = System.IO.Path.GetFileName(file.Name);
                values[6] = file;
                values[7] = issue.Range;
                dt.Rows.Add(values);
            }

            return dt;
        }

        internal static void GotoCode(SourceFile file, SourceRange range, LocatorBeacon beacon)
        {
            CodeRush.File.Activate(file.Name);           
            TextView view = (TextView)CodeRush.Source.Active.View;
            view.MakeVisible(range.Start);
            CodeRush.Caret.MoveTo(range.Start);
            beacon.Start(view, range.Start.Line, range.Start.Offset);
        }
    }
}
