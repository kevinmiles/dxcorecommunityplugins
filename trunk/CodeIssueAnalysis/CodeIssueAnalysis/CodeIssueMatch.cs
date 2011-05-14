using DevExpress.CodeRush.StructuralParser;

namespace CodeIssueAnalysis
{
    class CodeIssueMatch
    {
        SourceFile file;
        string filePath;
        EnvDTE.Project project;

        public CodeIssueMatch(SourceFile file)
        {
            this.file = file;
        }

        public CodeIssueMatch(string filePath)
        {
            this.filePath = filePath;
        }

        public CodeIssueMatch(EnvDTE.Project project)
        {
            this.project = project;
        }
        /*
        internal bool FileMatch(CodeIssueFile issue)
        {
            return issue.file == file ? true : false;
        }*/

        internal bool FilePathMatch(CodeIssueFile issue)
        {
            return issue.file.FilePath == filePath ? true : false;
        }

        internal bool ProjectMatch(CodeIssueFile issue)
        {
            return project.FullName == issue.file.Project.FullName ? true : false;
        }
    }
}
