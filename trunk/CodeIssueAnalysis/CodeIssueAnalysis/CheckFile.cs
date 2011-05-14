using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text.RegularExpressions;
using DevExpress.CodeRush.Core;
using DevExpress.CodeRush.StructuralParser;

namespace CodeIssueAnalysis
{
    class CheckFile
    {
        const int MaxRangeTextLength = 30;
        const int MinRangeTextLength = 20;

        readonly IssueServices issueService;
        readonly SourceFile file;
        readonly IssueProcessor issueProcessor;

        private string _FileText = String.Empty;
        public string FileText
        {
            get 
            {
                if (_FileText.Equals(String.Empty))
                {
                    UpdateFileText();
                    return _FileText;
                }
                else
                {
                    return _FileText;
                }
            }
            set
            {
            	_FileText = value;
            }
        }

        private void UpdateFileText()
        {
            //first attempt to get text through editor on failing resort to read from disk
            try
            {
                string fileName = file.Name; // File path            

                DevExpress.DXCore.TextBuffers.ITextBuffer buffer = CodeRush.TextBuffers[fileName];
                if (buffer == null)
                    buffer = CodeRush.TextBuffers.Open(fileName);

                SourceRange rng = buffer.Range;
                FileText = buffer.GetText(rng);
            }
            catch (Exception ex)
            {
                Debug.Assert(false, "Failed To Get File Text Attempt 1", ex.Message);
                try
                {
                    //read file without locking
                    using (FileStream fs = new FileStream(file.Name, FileMode.Open, FileAccess.Read))
                    using (StreamReader sr = new StreamReader(fs))
                    {
                        FileText = sr.ReadToEnd();
                        fs.Flush();

                        if (sr != null)
                            sr.Close();
                        if (fs != null)
                            fs.Close();
                    }
                }
                catch (Exception err)
                {
                    Debug.Assert(false, "Failed To Get File Text Attempt 2", err.Message);
                    FileText = String.Empty;
                }
            }
        }

        internal CheckFile(IssueServices issueService, 
                            SourceFile file, 
                            IssueProcessor issueProcessor)
        {
            this.issueService = issueService;
            this.file = file;
            this.issueProcessor = issueProcessor;
        }

        internal void Check(object state)
        {
            //abort threads quickly if cancel pressed
            if (!issueProcessor.shutdown)
            {
                if (IsIncluded())
                {                
                    //exlusions only need to be tested if file is included
                    if (!IsExcluded())
                    {
                        try
                        {                             
                            // On large project solution analysis take several minutes and you are processing collection which is constantly modified 
                            foreach (CodeIssue issue in issueService.CheckCodeIssues(file))
                            {
                                try
                                {
                                    issueProcessor.AddCodeIssue(issue, file, GetMessageText(issue.Range));
                                }
                                catch (Exception err)
                                {
                                    Debug.Assert(false, "Failure During Checking Issue", 
                                        String.Format("File: {0}{1}{2}{3}Issue: {4}", file, Environment.NewLine, err, Environment.NewLine, issue));
                                }
                            }
                        }
                        catch (Exception err)
                        {
                            Debug.Assert(false, "Failure During issueService.CheckCodeIssues(file)", 
                                String.Format("File: {0}{1}{2}", file, Environment.NewLine, err));
                        }
                    }
                }
            }

            issueProcessor.AddProcessedCount();
        }

        private bool IsIncluded()
        {
            if (FileNamePatternMatch(CodeIssueOptions.fileInclusions))
            {
                return true;
            }

            if (ContentPatternMatch(CodeIssueOptions.fileContentInclusions))
            {
                return true;
            }

            return false;
        }

        private bool IsExcluded()
        {
            if (FileNamePatternMatch(CodeIssueOptions.fileExclusions))
            {
                return true;
            }

            if (ContentPatternMatch(CodeIssueOptions.fileContentExclusions))
            {
                return true;
            }

            return false;
        }

        private bool FileNamePatternMatch(List<string> patterns)
        {
            try
            {
                foreach (string pattern in patterns)
                {
                    if ((new Regex(pattern)).Match(file.Name).Success)
                    {                    
                        return true;
                    }
                }
            }
            catch (Exception err)
            {
                Debug.Assert(false, "Error checking File Patterns",
                    String.Format("File: {0}{1}{2}", file, Environment.NewLine, err));
            }
            return false;
        }

        private bool ContentPatternMatch(List<string> patterns)
        {
            foreach (string pattern in patterns)
            {
                if ((new Regex(pattern)).Match(FileText).Success)
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Returns some text to show in the grid for the issue shorter than the full message
        /// </summary>
        /// <param name="range"></param>
        /// <returns></returns>
        internal string GetMessageText(SourceRange range)
        {
            try
            {
                string content = "";
                int st = -1;
                int count = 1;
                while (count < range.Start.Line)
                {
                    int tmp = FileText.IndexOf(Environment.NewLine, st + 1);
                    st = tmp;
                    count++;
                }
                st = st + Environment.NewLine.Length;
                int en = st;
                while (count < range.End.Line)
                {
                    en = FileText.IndexOf(Environment.NewLine, en + 1);
                    count++;
                }
                st = st + range.Start.Offset - 1;
                en = en + range.End.Offset - 1;

                content = FileText.Substring(st, en - st);

                if (content.Length > MaxRangeTextLength)
                {
                    //try to finish at end of a word between minLength and maxLength
                    int indexEnd = content.LastIndexOfAny(new char[] { ' ', '\t', '\n', '\r' }, MinRangeTextLength);

                    if (indexEnd == -1 || indexEnd > MaxRangeTextLength)
                        return content.Substring(0, MaxRangeTextLength);
                    else
                        return content.Substring(0, indexEnd);
                }
                else
                {
                    return content;
                }
            }
            catch (Exception err)
            {
                Debug.Assert(false, "Failed Get File Text", err.Message);
                return String.Empty;
            }
        }
    }
}
