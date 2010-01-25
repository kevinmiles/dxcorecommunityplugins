using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.CodeRush.Core;
using DevExpress.CodeRush.StructuralParser;
using System.IO;

namespace CR_TestDrivenDevelopmentTools
{
    public class ProjectServices
    {
        public Project GetProject(string projectName)
        {
            return CodeRush.Solution.AllProjects.Where(p => p.Name == projectName).First();
        }

        public string GetUniqueFilepathForNewFileInProject(Project project, string idealBaseFilename)
        {
            var rootPath = Path.GetDirectoryName(project.FileName);
            string fileExtension = CodeRush.Language.SupportedFileExtensions;
            var newFilepath = Path.Combine(rootPath, Path.ChangeExtension(idealBaseFilename, fileExtension));
            int suffixIncrement = 1;

            while (File.Exists(newFilepath))
            {
                newFilepath = Path.Combine(rootPath, Path.ChangeExtension(idealBaseFilename + suffixIncrement.ToString(), fileExtension));
                suffixIncrement++;
            }

            return newFilepath;
        }
    }
}
