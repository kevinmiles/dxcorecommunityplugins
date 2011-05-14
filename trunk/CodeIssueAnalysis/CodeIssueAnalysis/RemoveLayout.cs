using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;

namespace CodeIssueAnalysis
{
    public partial class RemoveLayout : Form
    {
        public RemoveLayout()
        {
            InitializeComponent();
            CodeIssueOptions.UpdateLayoutsList(cmbLayouts, false);
            try
            {
                cmbLayouts.SelectedIndex = 0;
            }
            catch 
            {
                Debug.Assert(false, "No Layouts");
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            try
            {
                File.Delete(CodeIssueOptions.GetLayoutPath() + Path.DirectorySeparatorChar + cmbLayouts.SelectedItem.ToString());
                this.Close();
            }
            catch (Exception err)
            {
                MessageBox.Show("Failed to Remove Layout", "Removing Layout Failed");
                this.Close();
            }
        }
    }
}
