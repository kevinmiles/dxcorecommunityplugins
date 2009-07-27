using System;
using System.Linq;
using System.Windows.Forms;
using DevExpress.CodeRush.Core;
using System.Collections.Generic;
using DevExpress.CodeRush.PlugInCore;

namespace CR_CCConsole
{
    [Title("CruiseControl.Net Project Status")]
    public partial class CCNetStatusWindow : ToolWindowPlugIn
    {
        // DXCore-generated code...
        #region InitializePlugIn
        public override void InitializePlugIn()
        {
            base.InitializePlugIn();
            tmrUpdate.Interval = CCStatusConfig.UpdateInterval * 60 * 100;
            BindProjectList();
            tmrUpdate.Enabled = true;
            tmrUpdate.Start();
        }
        #endregion
        #region FinalizePlugIn
        public override void FinalizePlugIn()
        {
            tmrUpdate.Enabled = false;
            tmrUpdate.Stop();
            base.FinalizePlugIn();
        }
        #endregion

        private void tmrUpdate_Tick(object sender, EventArgs e)
        {
            BindProjectList();
        }

        internal void BindProjectList()
        {
            if(!bwStatusUpdater.IsBusy)
                bwStatusUpdater.RunWorkerAsync();
        }


        internal void NotifyFailures()
        {
            var failing = CCStatusConfig.FailingProjects;
            foreach (var notify in failing)
            {
                var hint = new BigFeedback() { Text = string.Format("{0} is now FAILING!", notify) };
                if (InvokeRequired)
                    Invoke((System.Action)(() => hint.Show()));
                else
                    hint.Show();
            }
        }

        private void largeIconsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            lvProjectStatus.View = View.LargeIcon;
            CCStatusConfig.CurrentView = View.LargeIcon;
        }

        private void smallIconsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            lvProjectStatus.View = View.SmallIcon;
            CCStatusConfig.CurrentView = View.SmallIcon;
        }

        private void detailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            lvProjectStatus.View = View.Details;
            CCStatusConfig.CurrentView = View.Details;
        }

        private void listToolStripMenuItem_Click(object sender, EventArgs e)
        {
            lvProjectStatus.View = View.List;
            CCStatusConfig.CurrentView = View.List;
        }

        private void refreshToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BindProjectList();
        }

        private void bwStatusUpdater_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            List<CCProject> projects = null;
            try
            {
                projects = CCStatusConfig.GetSelectedProjects();
            }
            catch
            {
                return;
            }

            lvProjectStatus.Invoke((System.Action)(() => lvProjectStatus.Items.Clear()));
            foreach (var project in projects)
            {
                var imageIndex = (int)project.LastBuildStatus;
                if (project.Activity == CCActivity.Building)
                    imageIndex += 2;
                ListViewItem lvi = new ListViewItem(project.Name, imageIndex);
                lvi.SubItems.AddRange(new ListViewItem.ListViewSubItem[] 
                {
                    new ListViewItem.ListViewSubItem(lvi,project.LastBuildStatus.ToString()),
                    new ListViewItem.ListViewSubItem(lvi,project.LastBuildTime.ToString("MM/dd/yyyy hh:mm tt"))
                });
                lvProjectStatus.Invoke((System.Action)(() => lvProjectStatus.Items.Add(lvi)));
            }
            lvProjectStatus.Invoke((System.Action)(() => lvProjectStatus.View = CCStatusConfig.CurrentView));

            if (CCStatusConfig.NotifyOnFailure)
            {
                NotifyFailures();
            }
        }

        private void CCNetStatusWindow_Load(object sender, EventArgs e)
        {
            BindProjectList();
        }
    }
}