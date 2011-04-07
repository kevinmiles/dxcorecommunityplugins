using System;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using DevExpress.CodeRush.Core;
using DevExpress.CodeRush.PlugInCore;
using DevExpress.CodeRush.StructuralParser;

namespace DX_ReviseSelection
{
    public partial class PlugIn1 : StandardPlugIn
    {
        // DXCore-generated code...
        #region InitializePlugIn
        public override void InitializePlugIn()
        {
            base.InitializePlugIn();
            createReviseSelection();
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
        public void createReviseSelection()
        {
            DevExpress.CodeRush.Core.Action ReviseSelection = new DevExpress.CodeRush.Core.Action(components);
            ((System.ComponentModel.ISupportInitialize)(ReviseSelection)).BeginInit();
            ReviseSelection.ActionName = "ReviseSelection";
            ReviseSelection.RegisterInCR = true;
            ReviseSelection.Execute += ReviseSelection_Execute;
            ((System.ComponentModel.ISupportInitialize)(ReviseSelection)).EndInit();
        }
        private void ReviseSelection_Execute(ExecuteEventArgs ea)
        {
            // Check we have a valid Active TextDocument and TextView
            TextDocument activeTextDocument = CodeRush.Documents.ActiveTextDocument;
            if (activeTextDocument == null)
                return;
            TextView activeView = activeTextDocument.ActiveView;
            if (activeView == null)
                return;

            // Extend Selection - This action only works on whole lines.
            activeView.Selection.ExtendToWholeLines();

            // Get Text in selection 
            SourceRange activeSelectionRange = activeView.Selection.Range;
            string textToDuplicate = activeTextDocument.GetText(activeSelectionRange);

            // Remove \r and split on \n - These will be added back later using GetComment
            string strippedText = textToDuplicate.Replace("\r", "");
            string[] commentedLines = strippedText.Split('\n');

            // Build Commented version of code 
            string commentedCode = String.Empty;
            for (int i = 0; i < commentedLines.Length; i++)
                if (commentedLines[i] != String.Empty || i != commentedLines.Length - 1)
                    commentedCode += CodeRush.Language.GetComment(" " + commentedLines[i]);

            // BuildReplacement text - Includes CommentedCode and OriginalText
            string textToInsert = CodeRush.Language.GetComment(" Old code:") + commentedCode
                                + CodeRush.Language.GetComment("-----------") + textToDuplicate;

            // Replace originalText with newly built code.
            activeTextDocument.SetText(activeSelectionRange, textToInsert);

        }
    }
}