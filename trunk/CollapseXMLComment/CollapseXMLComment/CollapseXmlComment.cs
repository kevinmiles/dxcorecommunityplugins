using System;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using DevExpress.CodeRush.Core;
using DevExpress.CodeRush.PlugInCore;
using DevExpress.CodeRush.StructuralParser;

namespace Refactor_Comments
{
    public partial class CollapseXmlComment : StandardPlugIn
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
       
        private void XmlSummaryCollapseProvider_CheckAvailability(object sender, CheckContentAvailabilityEventArgs ea)
        {
            if (ea.CodeActive.ElementType == LanguageElementType.XmlDocText)
            {               
                if (ea.Element.Name.Contains(Environment.NewLine))
                    ea.Available = true;
            }
            else
                ea.Available = false;
        }

        private void XmlSummaryCollapseProvider_Apply(object sender, ApplyContentEventArgs ea)
        {
            string comment = ea.Element.Name;
            comment = comment.Trim();
            
            if (comment.Length == ea.Element.Name.Length)
            {
                // After removing leading/trailing CR/LF we're still the same length, so now
                // try to collapse any internal CR/LF's in the string
                char[] newLine = Environment.NewLine.ToCharArray();
                string newComment = "";
                string[] lines = comment.Split(newLine, StringSplitOptions.RemoveEmptyEntries);
                for (int i = 0; i < lines.Length; i++)
                {
                    newComment += lines[i].Trim();
                    if (lines.Length > 0)
                        if (i < (lines.Length - 1))
                            newComment += " ";
                }
                comment = newComment;                
            }
            else if (comment.Contains(Environment.NewLine))
            {
                // We've got some CR/LF's so we need to fix them up to add in our XML comment prefix
                // If we don't do this we end up with lines that don't have the XML comment prefix
                // on them. ex.
                // ///<summary>
                // sample</summary>
                if (ea.Element.Parent.Parent.ElementType == LanguageElementType.XmlDocComment)
                {
                    char[] newLine = Environment.NewLine.ToCharArray();
                    string newComment = "";
                    int indent = ea.Element.Parent.Parent.StartOffset;
                    if (indent > 0)
                        indent--;

                    string[] lines = comment.Split(newLine, StringSplitOptions.RemoveEmptyEntries);

                    for (int i = 0; i < lines.Length; i++)
                    {
                        // Start on the second element, since the first has already been collapsed 
                        if (i == 0)
                        {
                            newComment = lines[0].Trim();
                            continue;
                        }

                        newComment += String.Format("{0}{1}{2} {3}", 
                                          Environment.NewLine, 
                                          "".PadRight(indent),
                                          CodeRush.Language.ActiveExtension.XMLDocCommentBegin, 
                                          lines[i].Trim());
                    }

                    comment = newComment;
                }

            }

            ea.Element.ReplaceWith(comment, "Collapse");
        }
    }
}