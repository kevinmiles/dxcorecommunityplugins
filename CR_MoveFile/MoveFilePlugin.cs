using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using DevExpress.CodeRush.Core;
using DevExpress.CodeRush.PlugInCore;
using DevExpress.CodeRush.StructuralParser;
using System.IO;
using VSLangProj;
using System.Collections.Generic;

namespace CR_MoveFile
{
    public partial class MoveFilePlugin : StandardPlugIn
    {
        // DXCore-generated code...
        #region InitializePlugIn
        public override void InitializePlugIn()
        {
            base.InitializePlugIn();

            //
            // TODO: Add your initialization code here.
            //
        }
        #endregion
        #region FinalizePlugIn
        public override void FinalizePlugIn()
        {
            //
            // TODO: Add your finalization code here.
            //

            base.FinalizePlugIn();
        }
        #endregion

        private void codeProvider1_CheckAvailability(object sender, CheckContentAvailabilityEventArgs ea)
        {
            ea.Available = ea.Element is INamespaceReference 
                        || ea.Element is INamespaceElement
                        || ea.Element.IsClassInterfaceOrStruct();
        }

        private void codeProvider1_Apply(object sender, ApplyContentEventArgs ea)
        {
            var currentFile = CodeRush.Documents.ActiveFileName;
            var currentProject = CodeRush.Project.Active;
            using (var form = new SolutionBrowser(currentFile))
            {
                if (form.ShowAt(CodeRush.IDE, GetCaretPositionScreenPoint(true)) == DialogResult.OK)
                {
                    MoveFile(currentFile, currentProject, form.SelectedPath, form.SelectedProject);
                }
            }
        }

        private void MoveFile(string currentFile, Project currentProject, string selectedPath, string selectedProject)
        {
            if (string.IsNullOrEmpty(selectedPath) || !Directory.Exists(selectedPath))
                return;
            
            var referencingProjects = GetReferencingProjects();
            CodeRush.Documents.Active.Close(EnvDTE.vsSaveChanges.vsSaveChangesPrompt);
            var mainFile = Path.Combine(selectedPath, Path.GetFileName(currentFile));
            List<string> files = GetDependentFiles(Path.GetDirectoryName(currentFile), CodeRush.ProjectItems.Active);
            if (FileAlreadyExists(selectedPath, mainFile, files))
            {
                MessageBox.Show("A file already exists in the destination directory with the same name");
                return;
            }
            var mainProjectItem = MoveFiles(currentFile, currentProject, selectedProject, mainFile);
            MoveDependentFiles(currentProject, files, mainProjectItem);
            File.Delete(currentFile);
            DeleteDependentFiles(files);
            var targetProject = CodeRush.Solution.FindEnvDTEProject(selectedProject);
            if (targetProject.Name != currentProject.Name)
                CodeRush.Solution.FindEnvDTEProject(currentProject.Name).Save(currentProject.FileName);
            targetProject.Save(targetProject.FullName);
            foreach (var projectName in referencingProjects)
            {
                if (projectName == targetProject.Name)
                    continue;
                VSLangProj.VSProject vsProject = CodeRush.Solution.FindEnvDTEProject(projectName).Object as VSLangProj.VSProject;
                vsProject.References.AddProject(targetProject);
            }
            CodeRush.File.Activate(mainFile);
        }
        private IEnumerable<string> GetReferencingProjects()
        {
            var sourceFile = CodeRush.Source.ActiveSourceFile;
            var types = sourceFile.AllTypes;
            return types.Cast<IElement>().SelectMany(e => e.FindAllReferences().Select(r => r.Project)).Distinct(new ProjectComparer()).Select(p => p.Name).ToList();
        }
        private bool FileAlreadyExists(string selectedPath, string mainFile, List<string> files)
        {
            return File.Exists(mainFile) || files.Select(f => Path.Combine(selectedPath, Path.GetFileName(f))).Any(f => File.Exists(f));
        }

        private List<string> GetDependentFiles(string currentDir, EnvDTE.ProjectItem item)
        {
            List<string> files = new List<string>();
            foreach (EnvDTE.ProjectItem file in item.ProjectItems)
            {
                files.Add(Path.Combine(currentDir, file.Name));
            }
            return files;
        }

        private void DeleteDependentFiles(List<string> files)
        {
            if (files.Count > 0)
            {
                files.ForEach(f => File.Delete(f));
            }
        }

        private void MoveDependentFiles(Project currentProject, List<string> files, EnvDTE.ProjectItem mainProjectItem)
        {
            if (files.Count > 0)
            {
                foreach (var file in files)
                {
                    mainProjectItem.ProjectItems.AddFromFileCopy(file);
                    CodeRush.Solution.ExcludeFileFromProject(currentProject.Name, file);
                }
            }
        }

        private EnvDTE.ProjectItem MoveFiles(string currentFile,Project currentProject, string newProjectName, string destPath)
        {
            CodeRush.Solution.ExcludeFileFromProject(currentProject.Name, currentFile);
            File.Move(currentFile, destPath);
            var proj = CodeRush.Solution.FindEnvDTEProject(newProjectName);
            return proj.ProjectItems.AddFromFile(destPath);
        }

        private Point GetCaretPositionScreenPoint(bool newLine)
        {
            SourcePoint point2;
            SourcePoint active = CodeRush.Caret.SourcePoint; ;
            if (active == null)
            {
                return Point.Empty;
            }
            if (newLine)
            {
                point2 = new SourcePoint(active.Line + 1, active.Offset);
            }
            else
            {
                point2 = new SourcePoint(active.Line, active.Offset);
            }
            return CodeRush.TextViews.Active.ToScreenPoint(CodeRush.TextViews.Active.GetPoint(point2));
        }
    }

    public static class ExtensionMethods
    {
        public static bool IsClassInterfaceOrStruct(this IElement element)
        {
            return element is IClassElement || element is IInterfaceElement || element is IStructElement;   
        }
    }

    public class ProjectComparer : IEqualityComparer<IProjectElement>
    {
        public bool Equals(IProjectElement x, IProjectElement y)
        {
            return x.Name.Equals(y.Name);
        }

        public int GetHashCode(IProjectElement obj)
        {
            return obj.Name.GetHashCode();
        }

    }

}