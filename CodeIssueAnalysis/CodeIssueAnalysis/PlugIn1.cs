using DevExpress.CodeRush.Core;
using DevExpress.CodeRush.PlugInCore;

namespace CodeIssueAnalysis
{
    public partial class PlugIn1 : StandardPlugIn
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

        private void action1_Execute(ExecuteEventArgs ea)
        {
            //acton needs to be in this bit to work before openign ToolWindow1
            ToolWindow1.ShowWindow();
        }
    }
}