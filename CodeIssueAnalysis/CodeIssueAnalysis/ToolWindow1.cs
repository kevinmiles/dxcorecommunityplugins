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
using System.Collections.Generic;
using System.IO;
using System.Collections;
using System.Reflection;

namespace CodeIssueAnalysis
{
    [Title("Code Issue Analysis")]
    public partial class ToolWindow1 : ToolWindowPlugIn
    {       
        Stopwatch watch = new Stopwatch();
        IssueProcessor worker;
        int totalCount;
        RefreshHelper helper;

        internal enum UpdateType
        {
            Add,
            Remove
        }

        // DXCore-generated code...
        #region InitializePlugIn
        public override void InitializePlugIn()
        {
            base.InitializePlugIn();           
            gridView1.BestFitMaxRowCount = 50;
            CodeIssueOptions.SetupSettingsLists();
            CodeIssueOptions.UpdateLayoutsList(cmbLayouts, true);
            worker = new IssueProcessor();
            worker.Results += OnResults;
            worker.Error += OnError;
            worker.ProcessingFile += OnProcessingFile;
            helper = new RefreshHelper(gridView1, "Hash");
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

        private void OnResults(object sender, EventArgs e)
        {
            //cross thread - so you don't get the cross theading exception
            if (this.InvokeRequired)
            {
                this.BeginInvoke((MethodInvoker)delegate { OnResults(sender, e); });
                return;
            }

            try
            {
                helper.SaveViewInfo();
                gridControl1.DataSource = GetCodeIssuesDataTable();
                helper.LoadViewInfo();
                gridView1.Columns["Type"].OptionsFilter.FilterPopupMode = FilterPopupMode.CheckedList;
                gridView1.Columns["Solution"].OptionsFilter.FilterPopupMode = FilterPopupMode.CheckedList;
                gridView1.Columns["Project"].OptionsFilter.FilterPopupMode = FilterPopupMode.CheckedList;
                gridView1.Columns["Message"].OptionsFilter.FilterPopupMode = FilterPopupMode.CheckedList;                
                gridView1.Columns["Source File"].Visible = false;
                gridView1.Columns["Range"].Visible = false;
                gridView1.Columns["Hash"].Visible = false;

                gridControl1.RefreshDataSource();
                gridView1.BestFitColumns();
                gridControl1.Refresh();

                watch.Stop();
                //MessageBox.Show("Ticks = " + watch.ElapsedTicks.ToString());
                watch.Reset();
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message, "Error Updating Grid");
            }
            finally
            {
                EndProcessing();
            }
        }       

        private void OnProcessingFile(object sender, IssueProcessor.ProcessingArgs e)
        {
            //cross thread - so you don't get the cross theading exception
            if (this.InvokeRequired)
            {
                this.BeginInvoke((MethodInvoker)delegate { OnProcessingFile(sender, e); });
                return;
            }

            try
            {
                progressBar.Maximum = e.totalFiles;
                progressBar.Value = e.processedFiles;
            }
            catch (Exception err)
            {
                Debug.Assert(false, "Error Setting Progress Bar");
            }
        }

        private void OnError(object sender, IssueProcessor.ErrorArgs e)
        {
            MessageBox.Show(e.Error.Message, "Update Error");
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
            new Thread(worker.AddAllSolutionIssues).Start();
            progressBar.Visible = true;
            btnCancel.Visible = true;  
        }

        private void btnProjectIssues_Click(object sender, EventArgs e)
        {
            new Thread(worker.AddAllProjectIssues).Start();
            progressBar.Visible = true;
            btnCancel.Visible = true;  
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            worker.shutdown = true;
            EndProcessing();
        }

        private void EndProcessing()
        {
            progressBar.Value = 0;
            btnCancel.Visible = false;
            progressBar.Visible = false;
        }

        private DataTable GetCodeIssuesDataTable()
        {
            DataTable dt = new DataTable(typeof(CodeIssue).Name);
            dt.Columns.Add("Type", typeof(String));
            dt.Columns.Add("Message", typeof(String));
            dt.Columns.Add("Line", typeof(Int32));
            dt.Columns.Add("Solution", typeof(String));
            dt.Columns.Add("Project", typeof(String));
            dt.Columns.Add("File", typeof(String));
            dt.Columns.Add("Source File", typeof(SourceFile));
            dt.Columns.Add("Range", typeof(SourceRange));
            dt.Columns.Add("Text", typeof(String));
            dt.Columns.Add("Hash", typeof(int));

            foreach (CodeIssueFile issue in worker.codeIssues)
            {
                int tmp = issue.GetHashCode();
                var values = new object[10];
                values[0] = issue.codeIssue.Type.ToString();
                values[1] = issue.codeIssue.Message;
                values[2] = issue.codeIssue.Range.Start.Line;
                values[3] = Path.GetFileName(issue.file.Solution.Name);
                values[4] = issue.file.Project.Name;
                values[5] = Path.GetFileName(issue.file.Name);
                values[6] = issue.file;
                values[7] = issue.codeIssue.Range;
                values[8] = issue.message;
                values[9] = issue.GetHashCode();
                dt.Rows.Add(values);
            }

            return dt;
        }

        private void btnFileIssues_Click(object sender, EventArgs e)
        {
            worker.RescanFileIssues(CodeRush.Source.ActiveSourceFile);
        }

        private void cmbLayouts_SelectedValueChanged(object sender, EventArgs e)
        {
            
            switch (cmbLayouts.SelectedItem.ToString())
            {
                case CodeIssueOptions.AllString:
                    gridView1.RestoreLayoutFromStream(GetEmbeddedFile("All.xml"));
                    break;
                case CodeIssueOptions.SaveString:
                    using (SaveLayout saveLayout = new SaveLayout(gridView1))
                    {
                        saveLayout.ShowDialog();

                        if (saveLayout.DialogResult == DialogResult.OK)
                            CodeIssueOptions.UpdateLayoutsList(cmbLayouts, true, saveLayout.saveName);
                    }                    
                    break;
                case CodeIssueOptions.RemoveString:
                    using (RemoveLayout removeLayout = new RemoveLayout())
                    {
                        removeLayout.ShowDialog();

                        if (removeLayout.DialogResult == DialogResult.OK)
                            CodeIssueOptions.UpdateLayoutsList(cmbLayouts, true);
                    }                    
                    break;
                case CodeIssueOptions.Separator:
                    break;
                default:
                    CodeIssueOptions.LoadLayout(gridView1, cmbLayouts.SelectedItem.ToString());
                    break;
            }
        }

        public static Stream GetEmbeddedFile(string fileName)
        {
            return Assembly.GetExecutingAssembly().GetManifestResourceStream(Assembly.GetExecutingAssembly().GetName().Name + "." + fileName);
        }

        
    }
}