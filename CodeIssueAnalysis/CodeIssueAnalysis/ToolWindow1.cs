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
using System.Data;

namespace CodeIssueAnalysis
{
    [Title("Code Issue Analysis")]
    public partial class ToolWindow1 : ToolWindowPlugIn
    {
        IssueProcessor worker;
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

        private void UpdateData(bool wholeSolution)
        {
            if (canRefresh)
            {
                worker = new IssueProcessor(wholeSolution);
                worker.Results += OnResults;
                worker.Error += OnError;
                worker.ProcessingFile += OnProcessingFile;
                Thread workerThread = new Thread(worker.Run);
                workerThread.Start();
                progressBar.Visible = true;
                btnCancel.Visible = true;
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
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message, "Error Updating Grid");
            }

            EndProcessing();
        }       

        private void OnProcessingFile(object sender, IssueProcessor.ProcessingArgs e)
        {
            //cross thread - so you don't get the cross theading exception
            if (this.InvokeRequired)
            {
                this.BeginInvoke((MethodInvoker)delegate { OnProcessingFile(sender, e); });
                return;
            }            
            progressBar.Maximum = e.FileCount;
            progressBar.Value = e.CurrentFile;
        }

        private void OnError(object sender, IssueProcessor.ErrorArgs e)
        {
            MessageBox.Show(e.Error.Message, "Update Error");
            canRefresh = true;
            progressBar.Value = 0;
            progressBar.Visible = false;
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

        private void btnCancel_Click(object sender, EventArgs e)
        {
            worker.shutdown = true;
            EndProcessing();
        }

        private void EndProcessing()
        {
            canRefresh = true;
            progressBar.Value = 0;
            btnCancel.Visible = false;
            progressBar.Visible = false;
        }
    }
}