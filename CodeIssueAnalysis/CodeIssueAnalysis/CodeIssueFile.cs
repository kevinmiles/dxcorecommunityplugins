using DevExpress.CodeRush.Core;
using DevExpress.CodeRush.StructuralParser;
using System;

namespace CodeIssueAnalysis
{
    class CodeIssueFile
    {     
        internal CodeIssue codeIssue;
        internal SourceFile file;
        internal string message;

        internal CodeIssueFile(CodeIssue codeIssue, SourceFile file, string message)
        {
            this.codeIssue = codeIssue;
            this.file = file;
            this.message = message;

        }

        //WARNING NOT TRULY UNIQUE ON PURPOSE TO TRY AND MAINATAIN GROUPING THROUGH DATA REFRESH
        //e.g. doesn't account for range/line change
        public override int GetHashCode()
        {
            return codeIssue.Message.GetHashCode() ^ 
                file.FilePath.GetHashCode() ^ 
                message.GetHashCode();
        }
        
    }
}
