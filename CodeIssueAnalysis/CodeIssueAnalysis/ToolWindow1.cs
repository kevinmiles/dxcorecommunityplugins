using System;
using System.Diagnostics;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using DevExpress.CodeRush.Core;
using DevExpress.CodeRush.PlugInCore;
using DevExpress.CodeRush.StructuralParser;
using DevExpress.DXCore.Controls.Data;
using DevExpress.DXCore.Controls.XtraGrid.Columns;
using DevExpress.DXCore.Controls.XtraGrid.Views.Grid;

namespace CodeIssueAnalysis
{
    [Title("Code Issue Analysis")]
    public partial class ToolWindow1 : ToolWindowPlugIn
    {
        private bool canRefresh = true;
        int totalCount;

        // DXCore-generated code...
        #region InitializePlugIn
        public override void InitializePlugIn()
        {
            base.InitializePlugIn();            
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

        public void UpdateData(bool wholeSolution)
        {
            if (canRefresh)
            {
                IssueProcessor worker = new IssueProcessor(wholeSolution);
                worker.Results += OnResults;
                worker.Error += OnError;
                Thread workerThread = new Thread(worker.Run);
                this.Cursor = Cursors.WaitCursor;
                workerThread.Start();
            }
        }

        private void OnResults(object sender, IssueProcessor.ResultsArgs e)
        {
            //cross thread - so you don't get the cross theading exception
            if (this.InvokeRequired)
            {
                this.BeginInvoke((MethodInvoker)delegate { OnResults(sender, e); });
                return;
            }
            this.Cursor = Cursors.Default;
            try
            {
                gridControl1.DataSource = e.dt;
                gridControl1.RefreshDataSource();
                gridView1.BestFitMaxRowCount = 50;
                gridView1.BestFitColumns();

                foreach (GridColumn column in gridView1.Columns)
                {
                    column.OptionsFilter.FilterPopupMode = FilterPopupMode.CheckedList;
                }

                gridView1.Columns["Source File"].Visible = false;
                gridView1.Columns["Range"].Visible = false;

                gridControl1.Refresh();
                canRefresh = true;
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message, "Error Updating Grid");
            }
        }

        private void OnError(object sender, IssueProcessor.ErrorArgs e)
        {
            this.Cursor = Cursors.Default;
            MessageBox.Show(e.Error.Message, "Update Error");
            canRefresh = true;
        }

        private void gridView1_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                
                Point pt = gridControl1.PointToClient(MousePosition);
                GridView view = (GridView)sender;
                if (view.CalcHitInfo(pt).InRow)
                {
                    IssueProcessor.GotoCode((SourceFile)gridView1.GetFocusedDataRow()["Source File"], (SourceRange)gridView1.GetFocusedDataRow()["Range"], locatorBeacon1);
                }
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message, "Error Going To Code");
            }
        }

        private void gridView1_CustomSummaryCalculate(object sender, CustomSummaryEventArgs e)
        {
            try
            {
                // Initialization
                if (e.SummaryProcess == CustomSummaryProcess.Start)
                {
                    totalCount = 0;
                }

                // Calculation
                if (e.SummaryProcess == CustomSummaryProcess.Calculate)
                {
                    totalCount++;
                }

                // Finalization
                if (e.SummaryProcess == CustomSummaryProcess.Finalize)
                {
                    e.TotalValue = totalCount;
                }
            }
            catch
            {
                Debug.Assert(false, "Error creating custom summaries");
            }
        }

        private void btnSolutionIssues_Click(object sender, EventArgs e)
        {
            UpdateData(true);
        }

        private void btnProjectIssues_Click(object sender, EventArgs e)
        {
            UpdateData(false);
        }

    
    }
}