using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.DXCore.Controls.XtraGrid.Views.Grid;
using System.IO;
using System.Diagnostics;

namespace CodeIssueAnalysis
{
    public partial class SaveLayout : Form
    {
        GridView view;
        internal string saveName;

        public SaveLayout()
        {
            InitializeComponent();
        }

        public SaveLayout(GridView view)
        {
            this.view = view;
            InitializeComponent();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtName.Text.Length > 0)
                {
                    view.SaveLayoutToXml(CodeIssueOptions.GetLayoutPath() + Path.DirectorySeparatorChar + txtName.Text + ".xml");
                    saveName = txtName.Text + ".xml";
                    this.Close();
                }
                else
                {
                    MessageBox.Show("You must enter a layout name.", "Enter Layout Name");
                }
            }
            catch (Exception err)
            {
                MessageBox.Show("Failed to Save", "Saving Failed Possibly Access Privileges");
            }
        }
    }
}
