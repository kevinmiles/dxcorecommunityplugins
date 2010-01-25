using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.CodeRush.Core;
using DevExpress.CodeRush.StructuralParser;

namespace CR_TestDrivenDevelopmentTools
{
    public class DeclareClassInSpecificProject : DeclareTypeInSpecificProjectBase
    {
        private ProjectServices _projectServices;

        /// <summary>
        /// Initializes a new instance of the DeclareClassInSpecificProject class.
        /// </summary>
        /// <param name="projectServices"></param>
        public DeclareClassInSpecificProject(ProjectServices projectServices)
        {
            _projectServices = projectServices;
        }

        public void AddProjectsToSubmenu(CheckContentAvailabilityEventArgs ea)
        {
            AddProjectsToSubmenu(ea, "Class");
        }

        public void Apply(ApplyContentEventArgs ea, string selectedProjectName)
        {
            Project selectedProject = _projectServices.GetProject(selectedProjectName);

            string filename = _projectServices.GetUniqueFilepathForNewFileInProject(selectedProject, ea.Element.Name);

            //selectedProject.CodeModel.DTE.ItemOperations.AddNewItem(@"Visual C# Items\Code\Class", filename);
           
            var currentProject = DevExpress.CodeRush.Core.SolutionHelper.GetProjectByName(CodeRush.Project.Active.Name);
            var projectToAddAsReference = DevExpress.CodeRush.Core.SolutionHelper.GetProjectByName(selectedProjectName);

            currentProject.CodeModel.DTE.ItemOperations.AddNewItem(@"Visual C# Items\Code\Class", filename);
            
            var vsProj = currentProject.Object as VSLangProj.VSProject;
            if (vsProj != null)
                vsProj.References.AddProject(projectToAddAsReference);

        }

        protected override bool IsValidSelection(LanguageElement element, TextViewSelection selection)
        {
            if ((element == null) || (selection == null))
                return false;
            if (selection.Exists)
                return false;
            var creationExpression = element.Parent as ObjectCreationExpression;

            return creationExpression != null && element.GetDeclaration() == null;
        }

        protected override bool IsValidType(LanguageElement member)
        {
            return member.ElementType == LanguageElementType.TypeReferenceExpression;
        }

    }
}
