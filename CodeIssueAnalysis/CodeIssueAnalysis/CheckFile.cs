using System;
using System.Collections.Generic;
using System.Data;
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

        IssueServices issueService;
        SourceFile file;        
        IssueProcessor issueProcessor;

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
                try
                {
                    if (IsIncluded())
                    {                
                        //exlusions only need to be tested if file is included
                        if (!IsExcluded())
                        {
                            foreach (CodeIssue issue in issueService.CheckCodeIssues(file))
                            {
                                issueProcessor.AddCodeIssue(issue, file, GetFileTextRange(issue.Range));
                            }
                        }
                    }
                }
                catch (Exception err)
                {
                    Debug.Assert(false, "Failed To Check Issues", err.Message);
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
            foreach (string pattern in patterns)
            {
                if ((new Regex(pattern)).Match(file.Name).Success)
                {                    
                    return true;
                }
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
            catch
            {
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
                    Debug.Assert(false, "Failed To Get File Text", err.Message);
                    FileText = String.Empty;
                } 
            }            
        }

        internal string GetFileTextRange(SourceRange range)
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

        /* regular expression way to get groups was slower than looping code currently used
        private string TextRangeTest1(SourceRange range)
        {
            string content = "";
            Match m = Regex.Match(FileText, "((" + Environment.NewLine + ").*?){" + (range.End.Line - 1) + "}");

            if (m.Success)
            {
                int start = m.Groups[2].Captures[range.Start.Line - 2].Index + range.Start.Offset + 1;
                int end = m.Groups[2].Captures[range.End.Line - 2].Index + range.End.Offset + 1;
                content = FileText.Substring(start, end - start);
            }
            return content;
        }*/

        /* methods to read file text within environment 
         * seem to get object refrence errors sometimes
         * so done outside VS for now
        private static string GetFileText(SourceFile fileNode)
        {
            try
            {
                string fileName = fileNode.Name; // File path            

                DevExpress.DXCore.TextBuffers.ITextBuffer buffer = CodeRush.TextBuffers[fileName];
                if (buffer == null)
                    buffer = CodeRush.TextBuffers.Open(fileName);
                if (buffer == null)
                    return String.Empty;
                
                SourceRange rng = buffer.Range;
                return buffer.GetText(rng);
            } 
            catch(Exception err)
            {
                Debug.Assert(false, "Failed Get File Text", err.Message);
                return String.Empty;
            }
        }

        private static string GetFileText(SourceFile fileNode, SourceRange range)
        {
            try
            {
                string fileName = fileNode.Name; // File path            

                DevExpress.DXCore.TextBuffers.ITextBuffer buffer = CodeRush.TextBuffers[fileName];
                if (buffer == null)
                    buffer = CodeRush.TextBuffers.Open(fileName);
                if (buffer == null)
                    return String.Empty;
                
                string tmp = buffer.GetText(range);
                return tmp.Length > 20 ? tmp.Substring(0, 20) : tmp;
            }
            catch(Exception err)
            {
                Debug.Assert(false, "Failed To Get File Text Range", err.Message);
                return String.Empty;
            }
        }        
        */
    }
}
