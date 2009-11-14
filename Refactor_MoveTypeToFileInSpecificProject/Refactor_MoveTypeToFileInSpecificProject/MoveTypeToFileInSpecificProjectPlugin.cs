using System;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using DevExpress.CodeRush.Core;
using DevExpress.CodeRush.PlugInCore;
using DevExpress.CodeRush.StructuralParser;
using System.Collections.Generic;
using System.IO;
using DevExpress.CodeRush.Diagnostics;
using System.Text;

namespace Refactor_MoveTypeToFileInSpecificProject
{
    public partial class MoveTypeToFileInSpecificProjectPlugin : StandardPlugIn
    {
        public override void FinalizePlugIn()
        {
            base.FinalizePlugIn();
        }

        public override void InitializePlugIn()
        {
            base.InitializePlugIn();
        }

        private void AddSubmenuItems(CheckContentAvailabilityEventArgs ea, IList<Project> projectsInSolution)
        {
            foreach (Project project in CodeRush.Solution.AllProjects.Where(p => p.Name != ea.Element.Project.Name && p.Type != ProjectType.Miscellaneous).OrderBy(p => p.Name))
            {
                projectsInSolution.Add(project);
                ea.AddSubMenuItem(project.Name, project.Name, String.Format("Move type to Project {0}", project.Name));
            }
        }

        private string BuildCode(IEnumerable<NamespaceReference> namespaceReferences, Namespace namespaceScope, TypeDeclaration typeDeclaration)
        {
            ElementBuilder builder = new ElementBuilder();

            foreach (NamespaceReference namespaceReference in namespaceReferences)
            {
                builder.AddNode(null, namespaceReference);
            }

            builder.AddNode(null, new EmptyStatement());
            builder.AddNode(null, namespaceScope);
            builder.AddNode(namespaceScope, typeDeclaration);

            return builder.GenerateCode();
        }

        private IEnumerable<NamespaceReference> BuildNamespaceReferences(LanguageElement element)
        {
            IList<NamespaceReference> namespaceReferences = new List<NamespaceReference>();
            ElementBuilder builder = new ElementBuilder();

            foreach (var nsr in CodeRush.Source.NamespaceReferences.Keys)
            {
                namespaceReferences.Add(builder.AddNamespaceReference(element.FileNode, CodeRush.Source.NamespaceReferences[nsr].ToString()));
            }

            return namespaceReferences;
        }

        private void DeleteTypeDeclaration(LanguageElement element)
        {
            element.SelectFullBlock();
            CodeRush.Selection.Delete();
        }

        private string GetNamespaceForElement(LanguageElement element)
        {
            IElement parent = element.Parent;

            while (parent != null && parent.ElementType != LanguageElementType.Namespace)
            {
                parent = parent.Parent;
            }

            return parent == null ? string.Empty : parent.Name;
        }

        private TypeDeclaration GetTypeDeclarationForElement(LanguageElement element)
        {
            return element.GetDeclaration() as TypeDeclaration;
        }

        private string GetUniqueNewFilepathForProject(Project project, string idealBaseFilename)
        {
            var rootPath = Path.GetDirectoryName(project.FileName);
            string fileExtension = CodeRush.Language.SupportedFileExtensions;
            var newFilepath = Path.Combine(rootPath, Path.ChangeExtension(idealBaseFilename, fileExtension));
            int suffixIncrement = 1;

            while (File.Exists(newFilepath))
            {
                newFilepath = Path.Combine(rootPath, Path.ChangeExtension(idealBaseFilename + suffixIncrement.ToString(), fileExtension));
                suffixIncrement += 1;
            }

            return newFilepath;
        }

        private bool IsValidSelection(LanguageElement member, TextViewSelection selection, SourcePoint caret)
        {
            if ((member == null) || (selection == null))
            {
                return false;
            }
            if (selection.Exists)
            {
                return false;
            }

            return (member.NameRange.Contains(caret) || CodeRush.Source.GetStartWordRange(member).Contains(caret));
        }

        private bool IsValidType(LanguageElement member)
        {
            return member.ElementType == LanguageElementType.Class
                || member.ElementType == LanguageElementType.Interface
                || member.ElementType == LanguageElementType.Struct;
        }

        private void MoveTypeToFileInSpecificProjectProvider_Apply(object sender, ApplyContentEventArgs ea)
        {
            string selectedSubmenuName = ea.SelectedSubMenuItem.Name;
            Project project = CodeRush.Solution.AllProjects.Where(p => p.Name == selectedSubmenuName).First();

            IEnumerable<NamespaceReference> namespaceReferences = BuildNamespaceReferences(ea.Element);
            Namespace namespaceScope = new Namespace(GetNamespaceForElement(ea.Element));
            TypeDeclaration typeDeclaration = GetTypeDeclarationForElement(ea.Element);

            CodeRush.UndoStack.BeginUpdate("Move Type To File in Project");

            CodeRush.Markers.Drop();

            DeleteTypeDeclaration(typeDeclaration);

            string code = BuildCode(namespaceReferences, namespaceScope, typeDeclaration);
            var filename = WriteCodeToNewFileInProject(project, code, ea.Element.Name);

            CodeRush.File.Activate(filename);
            CodeRush.Documents.Format();
        }

        private void MoveTypeToFileInSpecificProjectProvider_CheckAvailability(object sender, CheckContentAvailabilityEventArgs ea)
        {
            ea.Available = ShouldBeAvailable(ea);

            if (ea.Available == false)
                return;

            IList<Project> projectsInSolution = new List<Project>();

            ea.MenuCaption = "Move Type To File In Project...";
            AddSubmenuItems(ea, projectsInSolution);

        }

        private bool ShouldBeAvailable(CheckContentAvailabilityEventArgs ea)
        {
            return IsValidSelection(ea.Element, ea.TextView.Selection, ea.Caret) && IsValidType(ea.Element);
        }

        private string WriteCodeToNewFileInProject(Project project, string code, string idealBaseFilename)
        {
            string newFilepath = GetUniqueNewFilepathForProject(project, idealBaseFilename);
            File.WriteAllText(newFilepath, code);
            CodeRush.Solution.AddFileToProject(project.Name, newFilepath);

            return newFilepath;
        }

    }
}