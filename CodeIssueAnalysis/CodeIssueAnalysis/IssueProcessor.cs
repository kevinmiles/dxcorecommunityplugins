using System;
using System.Data;
using DevExpress.CodeRush.Core;
using DevExpress.CodeRush.PlugInCore;
using DevExpress.CodeRush.StructuralParser;
using System.Diagnostics;
using System.Threading;
using System.Collections.Generic;
using System.Windows.Forms;
using System.IO;

namespace CodeIssueAnalysis
{
    internal class IssueProcessor
    {
        internal volatile bool shutdown;
        private IssueServices issueService = CodeRush.Issues;
        internal List<CodeIssueFile> codeIssues = new List<CodeIssueFile>();

        internal event EventHandler<EventArgs> Results;
        internal event EventHandler<ProcessingArgs> ProcessingFile;
        internal event EventHandler<ErrorArgs> Error;

        private int processedFiles;
        private int totalFiles;

        RunType runType = RunType.NotRun;
        ProjectElement activeProject = null;

        internal enum RunType
        {
            Solution,
            Project,
            NotRun
        }

        internal void AddCodeIssue(CodeIssue issue, SourceFile file, string message)
        {
            lock (this)
            {
               codeIssues.Add(new CodeIssueFile(issue, file, message));
            }
        }

        private void OnResults(EventArgs e)
        {
            if (Results != null)
            {
                Results(this, e);
            }
        }

        private void OnProcessingFile(ProcessingArgs e)
        {
            if (ProcessingFile != null)
            {
                ProcessingFile(this, e);
            }
        }

        private void OnError(ErrorArgs e)
        {
            if (Error != null)
            {
                Error(this, e);
            }
        }

        internal void AddProcessedCount()
        {
            lock (this)
            {
                processedFiles++;
                OnProcessingFile(new ProcessingArgs(processedFiles, totalFiles));

                if (processedFiles >= totalFiles)
                    OnResults(null);
            }
        }       

        internal class ProcessingArgs : EventArgs
        {
            public int totalFiles { get; private set; }
            public int processedFiles { get; private set; }

            public ProcessingArgs(int processedFiles, int totalFiles)
            {                
                this.processedFiles = processedFiles;
                this.totalFiles = totalFiles;
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

        internal IssueProcessor()
        {
            EventNexus.ProjectItemRemoved += ProjectItemRemoved;
            EventNexus.ProjectItemAdded += ProjectItemAdded;
            EventNexus.ProjectItemRenamed += ProjectItemRenamed;
            EventNexus.FileChecked += FileChecked;
            EventNexus.ProjectAdded += ProjectAdded;
            EventNexus.ProjectRemoved += ProjectRemoved;
        }

        private void ProjectAdded(EnvDTE.Project project)
        {
            if (runType == RunType.Solution)
            {              
                try
                {
                    processedFiles = 0;
                    ProjectElement addedProject = CodeRush.Source.ActiveSolution.GetProjectByFullName(project.FullName);
                    totalFiles = addedProject.AllFiles.Count;

                    foreach (SourceFile file in addedProject.AllFiles)
                        CheckIssues(file);
                }
                catch (Exception err)
                {
                    Debug.Assert(false, "Error Scanning Project", err.Message);
                }
            }
        }

        private void ProjectRemoved(EnvDTE.Project project)
        {
            try
            {
                codeIssues.RemoveAll(new CodeIssueMatch(project).ProjectMatch);
            }
            catch (Exception err)
            {
                Debug.Assert(false, "Error removing code issues", err.Message);
            }

            OnResults(null);
        }

        internal void RescanFileIssues(SourceFile file)
        {
            try
            {
                processedFiles = 0;
                totalFiles = 1;
                
                codeIssues.RemoveAll(new CodeIssueMatch(file.Document.FullName).FilePathMatch);
                CheckIssues(file);
            }
            catch (Exception err)
            {
                Debug.Assert(false, "Error Scanning File", err.Message);
            }
        }

        internal void AddAllProjectIssues()
        {
            if (CodeRush.Source.ActiveProject == null)
            {
                Error(null, new ErrorArgs(new Exception("You must have a file open in the project you wish to scan.")));
            }
            else
            {
                try
                {
                    runType = RunType.Project;
                    processedFiles = 0;
                    codeIssues.Clear();
                    activeProject = CodeRush.Source.ActiveProject;
                    totalFiles = CodeRush.Source.ActiveProject.AllFiles.Count;

                    foreach (SourceFile file in CodeRush.Source.ActiveProject.AllFiles)
                        CheckIssues(file);
                }
                catch (Exception err)
                {
                    Debug.Assert(false, "Error Scanning Project", err.Message);
                }
            }
        }

        internal void AddAllSolutionIssues()
        {
            try
            {
                runType = RunType.Solution;
                processedFiles = 0;
                totalFiles = 0;
                codeIssues.Clear();

                foreach (ProjectElement project in CodeRush.Source.ActiveSolution.AllProjects)
                    totalFiles = totalFiles + project.AllFiles.Count;

                foreach (SourceFile file in CodeRush.Source.ActiveSolution.AllFiles)
                        CheckIssues(file);
            }
            catch (Exception err)
            {
                Debug.Assert(false, "Error Scanning Project", err.Message);
            }
        }

        private void CheckIssues(SourceFile file)
        {
            try
            {
                //non threaded way
                //new CheckFile(issueService, file, this).Check(null);
                ThreadPool.SetMaxThreads(8, 8);
                ThreadPool.QueueUserWorkItem(new CheckFile(issueService, file, this).Check);
            }
            catch (Exception err)
            {
                Debug.Assert(false, "Error queing issue service", err.Message);
            } 
        }

        internal static void GotoCode(SourceFile file, SourceRange range, LocatorBeacon beacon)
        {
            try
            {
                CodeRush.File.Activate(file.Name);
                TextView view = (TextView)CodeRush.Source.Active.View;
                view.MakeVisible(range.Start);
                CodeRush.Caret.MoveTo(range.Start);
                beacon.Start(view, range.Start.Line, range.Start.Offset);
            }
            catch
            {
                MessageBox.Show("Couldn't find file or line", "Can't goto issue");
            }
        }

        private void FileChecked(object sender, CodeIssuesCheckedEventArgs e)
        {                 
            try
            {
                switch (runType)
                {
                    case RunType.NotRun:
                    case RunType.Solution:
                        processedFiles = 0;
                        totalFiles = 1;                        
                        CheckIssues(e.FileNode);
                        break;
                    case RunType.Project:
                        processedFiles = 0;
                        totalFiles = 1;
                        if (e.FileNode.Project == activeProject)
                        {
                            CheckIssues(e.FileNode);
                        }
                        break;
                    default:
                        break;
                }
            }
            catch (Exception err)
            {
                Debug.Assert(false, "Error adding code issues", err.Message);
            } 
        }

        private void ProjectItemRemoved(EnvDTE.ProjectItem projectItem)
        {
            try
            {
                codeIssues.RemoveAll(new CodeIssueMatch(projectItem.get_FileNames(0)).FilePathMatch);
            }
            catch (Exception err)
            {
                Debug.Assert(false, "Error removing code issues", err.Message);
            }

            OnResults(null);
        }

        private void ProjectItemAdded(EnvDTE.ProjectItem projectItem)
        {
            try
            {
                switch (runType)
                {
                    case RunType.Solution:
                        processedFiles = 0;
                        totalFiles = 1;
                        CheckIssues(GetSourceFile(projectItem));
                        break;
                    case RunType.Project:
                        processedFiles = 0;
                        totalFiles = 1;
                        SourceFile file = GetSourceFile(projectItem);
                        if (file.Project == activeProject)
                        {
                            CheckIssues(file);
                        }                        
                        break;
                    default:
                        break;
                }
            }
            catch (Exception err)
            {
                Debug.Assert(false, "Error adding code issues", err.Message);
            }       
        }

        private void ProjectItemRenamed(EnvDTE.ProjectItem projectItem, string oldName)
        {
            try
            {
                string originalPath = Path.GetDirectoryName(projectItem.get_FileNames(0)) + Path.DirectorySeparatorChar + oldName;
                foreach (CodeIssueFile codeIssue in codeIssues)
                {
                    if (codeIssue.file.Name == originalPath)
                        codeIssue.file = GetSourceFile(projectItem);
                }
            }
            catch (Exception err)
            {
                Debug.Assert(false, "Error renaming code issues", err.Message);
            }

            OnResults(null);
        }

        private static SourceFile GetSourceFile(EnvDTE.ProjectItem projectItem)
        {
            return CodeRush.Source.ActiveSolution.FindSourceFile(projectItem.get_FileNames(0));
        }
    }
}
