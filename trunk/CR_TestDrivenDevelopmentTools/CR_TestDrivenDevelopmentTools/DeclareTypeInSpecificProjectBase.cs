using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.CodeRush.Core;
using DevExpress.CodeRush.StructuralParser;
using DevExpress.Refactor.Core;

namespace CR_TestDrivenDevelopmentTools
{
    public abstract class DeclareTypeInSpecificProjectBase
    {
        public bool IsAvailable(CheckContentAvailabilityEventArgs ea)
        {
            return IsValidSelection(ea.Element, ea.TextView.Selection) && IsValidType(ea.Element);
        }

        protected void AddProjectsToSubmenu(CheckContentAvailabilityEventArgs ea, string typeToDeclareString)
        {
            foreach (Project project in CodeRush.Solution.AllProjects.Where(p => p.Name != ea.Element.Project.Name && p.Type != ProjectType.Miscellaneous).OrderBy(p => p.Name))
                ea.AddSubMenuItem(project.Name, project.Name, String.Format("Declare {0} in the {1} project", typeToDeclareString, project.Name));
        }

        protected abstract bool IsValidSelection(LanguageElement element, TextViewSelection selection);

        protected abstract bool IsValidType(LanguageElement member);

    }
}
