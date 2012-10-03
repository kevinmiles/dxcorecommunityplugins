namespace CR_ForceProjectConverter
{
  partial class UnresolvedReferences
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
      this.listBoxControl1 = new DevExpress.DXCore.Controls.XtraEditors.ListBoxControl();
      this.simpleButton1 = new DevExpress.DXCore.Controls.XtraEditors.SimpleButton();
      this.labelControl1 = new DevExpress.DXCore.Controls.XtraEditors.LabelControl();
      ((System.ComponentModel.ISupportInitialize)(this.listBoxControl1)).BeginInit();
      this.SuspendLayout();
      // 
      // listBoxControl1
      // 
      this.listBoxControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.listBoxControl1.Location = new System.Drawing.Point(12, 37);
      this.listBoxControl1.Name = "listBoxControl1";
      this.listBoxControl1.Size = new System.Drawing.Size(500, 278);
      this.listBoxControl1.TabIndex = 0;
      // 
      // simpleButton1
      // 
      this.simpleButton1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.simpleButton1.Location = new System.Drawing.Point(321, 334);
      this.simpleButton1.Name = "simpleButton1";
      this.simpleButton1.Size = new System.Drawing.Size(191, 33);
      this.simpleButton1.TabIndex = 1;
      this.simpleButton1.Text = "Run ProjectConverter";
      this.simpleButton1.Click += new System.EventHandler(this.simpleButton1_Click);
      // 
      // labelControl1
      // 
      this.labelControl1.Location = new System.Drawing.Point(12, 12);
      this.labelControl1.Name = "labelControl1";
      this.labelControl1.Size = new System.Drawing.Size(337, 19);
      this.labelControl1.TabIndex = 2;
      this.labelControl1.Text = "The following references could not be resolved:";
      // 
      // UnresolvedReferences
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(524, 379);
      this.Controls.Add(this.labelControl1);
      this.Controls.Add(this.simpleButton1);
      this.Controls.Add(this.listBoxControl1);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
      this.Name = "UnresolvedReferences";
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
      this.Text = "Unresolved References found";
      ((System.ComponentModel.ISupportInitialize)(this.listBoxControl1)).EndInit();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private DevExpress.DXCore.Controls.XtraEditors.ListBoxControl listBoxControl1;
    private DevExpress.DXCore.Controls.XtraEditors.SimpleButton simpleButton1;
    private DevExpress.DXCore.Controls.XtraEditors.LabelControl labelControl1;
  }
}