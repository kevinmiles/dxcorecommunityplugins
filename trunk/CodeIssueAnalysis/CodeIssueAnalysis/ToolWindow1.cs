using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Reflection;
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
            helper = new RefreshHelper(gridView1, "Hash");
        }


        private void SetupWorker()
        {
            worker = new IssueProcessor();
            worker.Results += OnResults;
            worker.Error += OnError;
            worker.ProcessingFile += OnProcessingFile;
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
            if (InvokeRequired)
            {
                BeginInvoke((MethodInvoker)delegate { OnResults(sender, e); });
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
            }
            finally
            {
                EndProcessing();
            }
        }

        private void OnProcessingFile(object sender, IssueProcessor.ProcessingArgs e)
        {
            //cross thread
            if (InvokeRequired)
            {
                BeginInvoke((MethodInvoker)delegate { OnProcessingFile(sender, e); });
                return;
            }

            progressBar.Maximum = e.totalFiles;
            progressBar.Value = e.processedFiles;           
        }

        private void OnError(object sender, IssueProcessor.ErrorArgs e)
        {
            //cross thread
            if (InvokeRequired)
            {
                BeginInvoke((MethodInvoker)delegate { OnError(sender, e); });
                return;
            }

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
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error Going To Code");
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
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Error Creating Custom Summaries");
            }
        }

        private void btnSolutionIssues_Click(object sender, EventArgs e)
        {
            SetupWorker();
            new Thread(worker.AddAllSolutionIssues).Start();
            progressBar.Visible = true;
            btnCancel.Visible = true;
        }

        private void btnProjectIssues_Click(object sender, EventArgs e)
        {
            SetupWorker();
            new Thread(worker.AddAllProjectIssues).Start();
            progressBar.Visible = true;
            btnCancel.Visible = true;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (worker != null)
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

            foreach (CodeIssueFile issue in worker.CodeIssues)
            {
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
            SetupWorker();
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
            return Assembly.GetExecutingAssembly().GetManifestResourceStream(String.Format("{0}.{1}", Assembly.GetExecutingAssembly().GetName().Name, fileName));
        }

        private void btnExportHTMLTable_Click(object sender, EventArgs e)
        {
            DialogResult expand = MessageBox.Show(
                            "The export will only show what is visible in the grid. Do you wish to expand all groups to receive all the content?",
                            "Expand Groups",
                            MessageBoxButtons.YesNo);

            if (expand == DialogResult.Yes)
                gridView1.ExpandAllGroups();

            using (SaveFileDialog dlg = new SaveFileDialog 
            { 
                Filter = "HTML (*.html)|*.html", CheckFileExists = false, 
                InitialDirectory = CodeIssueOptions.GetLayoutPath() })
            {

                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        File.WriteAllText(dlg.FileName, new HTMLTableBuilder(gridView1).BuildHTMLTable("#E3EEFB"));
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Error Exporting File");
                    }
                }
            }
        }


    }
}