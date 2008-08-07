using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.CodeRush.Core;
using DevExpress.CodeRush.PlugInCore;
using DevExpress.CodeRush.StructuralParser;

namespace CR_Initials
{
    public partial class CR_Initials : StandardPlugIn
    {
        const string STR_Preferences = "Preferences";
        const string STR_DevInitials = "DevInitials";
        const string STR_DevName = "DevName";
        const string STR_FullNameComment = "Disabled";

        private string m_devInitials;
        private string m_devName;
        private bool m_fullName;

        // DXCore-generated code...
        #region InitializePlugIn
        public override void InitializePlugIn()
        {
            base.InitializePlugIn();

            // PCM - 08/07/2006 - Configure our settings.
            using (DecoupledStorage lStorage = OptInitials.Storage)
            {
                this.m_devInitials = lStorage.ReadString(STR_Preferences, STR_DevInitials, "");
                this.m_devName = lStorage.ReadString(STR_Preferences, STR_DevName, "");
                this.m_fullName = lStorage.ReadBoolean(STR_Preferences, STR_FullNameComment, false);
            }
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
        
        /// <summary>
        /// Add a developer comment.
        /// </summary>
        /// <param name="ea"></param>
        /// <developer>Paul Mrozowski</developer>
        /// <created>08/04/2006</created>
        private void actInitials_Execute(ExecuteEventArgs ea)
        {            
            TextDocument doc = TextDocument.Active;
            SourcePoint sp = CodeRush.Caret.SourcePoint;            
                        
            string line = doc.GetText(sp.Line);             
            string xmlDoc = CodeRush.Language.ActiveExtension.XMLDocCommentBegin;
            bool xmlComment = line.TrimStart().StartsWith(xmlDoc);
            if (xmlDoc == null)
                xmlDoc = "///";
            
            if (xmlComment)
            {                
                doc.QueueInsert(sp, String.Format("<developer>{0}</developer>\r\n{1}{2} <created>{3:MM/dd/yyyy}</created>", 
                                        this.m_devName, 
                                        line.Substring(0, line.IndexOf(xmlDoc)), 
                                        xmlDoc, 
                                        System.DateTime.Now));
            }
            else
            {
                string commentLine = CodeRush.Language.ActiveExtension.SingleLineCommentBegin;
                if (commentLine == null)
                {
                	commentLine = "//";
                }
                
                string comment = "";

                if (this.m_fullName)
                {
                    comment = String.Format("{0} {1} - {2:MM/dd/yyyy} - ", commentLine, this.m_devName, System.DateTime.Now);
                }
                else
                {
                    comment = String.Format("{0} {1} - {2:MM/dd/yyyy} - ", commentLine, this.m_devInitials, System.DateTime.Now);
                }
               
                doc.QueueInsert(sp, comment);
                CodeRush.Caret.MoveToEndOfLine();
                CodeRush.Caret.DeleteLeftWhiteSpace();                
            }
            
            doc.ApplyQueuedEdits("Developer Comment");            
        }
    }
}