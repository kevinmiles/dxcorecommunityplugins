using DevExpress.DXCore.Controls.XtraBars;
namespace CR_NavigateToTest
{
    partial class frmPickTarget
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.trvTargets = new System.Windows.Forms.TreeView();
            this.SuspendLayout();
            // 
            // imageList1
            // 
            this.imageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
            this.imageList1.ImageSize = new System.Drawing.Size(16, 16);
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // trvTargets
            // 
            this.trvTargets.Dock = System.Windows.Forms.DockStyle.Fill;
            this.trvTargets.ImageIndex = 0;
            this.trvTargets.ImageList = this.imageList1;
            this.trvTargets.Location = new System.Drawing.Point(0, 0);
            this.trvTargets.Name = "trvTargets";
            this.trvTargets.Scrollable = false;
            this.trvTargets.SelectedImageIndex = 0;
            this.trvTargets.Size = new System.Drawing.Size(202, 266);
            this.trvTargets.TabIndex = 0;
            this.trvTargets.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.trvTargets_MouseDoubleClick);
            this.trvTargets.AfterCollapse += new System.Windows.Forms.TreeViewEventHandler(this.trvTargets_AfterCollapse);
            this.trvTargets.ClientSizeChanged += new System.EventHandler(this.trvTargets_ClientSizeChanged);
            this.trvTargets.SizeChanged += new System.EventHandler(this.trvTargets_SizeChanged);
            this.trvTargets.AfterExpand += new System.Windows.Forms.TreeViewEventHandler(this.trvTargets_AfterExpand);
            // 
            // frmPickTarget
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(202, 266);
            this.ControlBox = false;
            this.Controls.Add(this.trvTargets);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MinimizeBox = false;
            this.Name = "frmPickTarget";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Shown += new System.EventHandler(this.frmPickTarget_Shown);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.TreeView trvTargets;
    }
}