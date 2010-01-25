using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.CodeRush.Core;
using DevExpress.CodeRush.StructuralParser;
using System.IO;

namespace CR_TestDrivenDevelopmentTools
{
    public class MoveTypeToFileInSpecificProject
    {
        private ProjectServices _projectServices;

        private NamespaceServices _namespaceServices;

        /// <summary>
        /// Initializes a new instance of the MoveTypeToFileInSpecificProject class.
        /// </summary>
        /// <param name="fileServices"></param>
        public MoveTypeToFileInSpecificProject(ProjectServices fileServices, NamespaceServices namespaceServices)
        {
            _namespaceServices = namespaceServices;
            _projectServices = fileServices;
        }

        public void AddProjectsToSubmenu(CheckContentAvailabilityEventArgs ea)
        {
            foreach (Project project in CodeRush.Solution.AllProjects.Where(p => p.Name != ea.Element.Project.Name && p.Type != ProjectType.Miscellaneous).OrderBy(p => p.Name))
            {
                ea.AddSubMenuItem(project.Name, project.Name, String.Format("Move type file in the {0} project", project.Name));
            }
        }

        public string Apply(ApplyContentEventArgs ea, string selectedProjectName)
        {
            Project project = _projectServices.GetProject(selectedProjectName);

            IEnumerable<NamespaceReference> namespaceReferences = _namespaceServices.BuildNamespaceReferences(ea.Element);
            Namespace namespaceScope = new Namespace(_namespaceServices.GetNamespaceForElement(ea.Element));
            TypeDeclaration typeDeclaration = GetTypeDeclarationForElement(ea.Element);

            if (typeDeclaration != null)
                DeleteTypeDeclaration(typeDeclaration);

            string code = BuildCode(namespaceReferences, namespaceScope, typeDeclaration);
            return WriteCodeToNewFileInProject(project, code, ea.Element.Name);
        }

        public string BuildCode(IEnumerable<NamespaceReference> namespaceReferences, Namespace namespaceScope, TypeDeclaration typeDeclaration)
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

        public bool IsAvailable(CheckContentAvailabilityEventArgs ea)
        {
            return IsValidSelection(ea.Element, ea.TextView.Selection, ea.Caret) && IsValidType(ea.Element);
        }

        private void DeleteTypeDeclaration(LanguageElement element)
        {
            element.SelectFullBlock();
            CodeRush.Selection.Delete();
        }

    

        private TypeDeclaration GetTypeDeclarationForElement(LanguageElement element)
        {
            return element.GetDeclaration() as TypeDeclaration;
        }

        private bool IsValidSelection(LanguageElement element, TextViewSelection selection, SourcePoint caret)
        {
            if ((element == null) || (selection == null))
            {
                return false;
            }
            if (selection.Exists)
            {
                return false;
            }

            return (element.NameRange.Contains(caret) || CodeRush.Source.GetStartWordRange(element).Contains(caret));
        }

        private bool IsValidType(LanguageElement member)
        {
            return member.ElementType == LanguageElementType.Class
                || member.ElementType == LanguageElementType.Interface
                || member.ElementType == LanguageElementType.Struct;
        }

        private string WriteCodeToNewFileInProject(Project project, string code, string idealBaseFilename)
        {
            string newFilepath = _projectServices.GetUniqueFilepathForNewFileInProject(project, idealBaseFilename);
            File.WriteAllText(newFilepath, code);
            CodeRush.Solution.AddFileToProject(project.Name, newFilepath);

            return newFilepath;
        }

    }
}