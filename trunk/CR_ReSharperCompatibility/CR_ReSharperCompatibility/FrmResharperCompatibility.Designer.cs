namespace CR_ReSharperCompatibility
{
  partial class FrmResharperCompatibility
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
      this.chkAlwaysPerformThisAction = new System.Windows.Forms.CheckBox();
      this.pnlOptions = new System.Windows.Forms.Panel();
      this.lblOptions = new System.Windows.Forms.Label();
      this.pnlBottom = new System.Windows.Forms.Panel();
      this.btnClose = new System.Windows.Forms.Button();
      this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
      this.lblCompatibilityNote = new System.Windows.Forms.RichTextBox();
      this.pnlTop = new System.Windows.Forms.Panel();
      this.lblTitle = new System.Windows.Forms.Label();
      this.pnlOptions.SuspendLayout();
      this.pnlBottom.SuspendLayout();
      this.pnlTop.SuspendLayout();
      this.SuspendLayout();
      // 
      // chkAlwaysPerformThisAction
      // 
      this.chkAlwaysPerformThisAction.AutoSize = true;
      this.chkAlwaysPerformThisAction.Location = new System.Drawing.Point(4, 8);
      this.chkAlwaysPerformThisAction.Name = "chkAlwaysPerformThisAction";
      this.chkAlwaysPerformThisAction.Size = new System.Drawing.Size(151, 17);
      this.chkAlwaysPerformThisAction.TabIndex = 2;
      this.chkAlwaysPerformThisAction.Text = "Always perform this action.";
      this.chkAlwaysPerformThisAction.UseVisualStyleBackColor = true;
      // 
      // pnlOptions
      // 
      this.pnlOptions.Controls.Add(this.lblOptions);
      this.pnlOptions.Location = new System.Drawing.Point(4, 60);
      this.pnlOptions.Name = "pnlOptions";
      this.pnlOptions.Size = new System.Drawing.Size(416, 241);
      this.pnlOptions.TabIndex = 3;
      // 
      // lblOptions
      // 
      this.lblOptions.AutoSize = true;
      this.lblOptions.Location = new System.Drawing.Point(4, 4);
      this.lblOptions.Name = "lblOptions";
      this.lblOptions.Size = new System.Drawing.Size(46, 13);
      this.lblOptions.TabIndex = 0;
      this.lblOptions.Text = "Options:";
      // 
      // pnlBottom
      // 
      this.pnlBottom.Controls.Add(this.btnClose);
      this.pnlBottom.Controls.Add(this.chkAlwaysPerformThisAction);
      this.pnlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
      this.pnlBottom.Location = new System.Drawing.Point(0, 299);
      this.pnlBottom.Name = "pnlBottom";
      this.pnlBottom.Size = new System.Drawing.Size(488, 28);
      this.pnlBottom.TabIndex = 4;
      // 
      // btnClose
      // 
      this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.btnClose.Location = new System.Drawing.Point(411, 4);
      this.btnClose.Name = "btnClose";
      this.btnClose.Size = new System.Drawing.Size(75, 23);
      this.btnClose.TabIndex = 3;
      this.btnClose.Text = "Close";
      this.btnClose.UseVisualStyleBackColor = true;
      this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
      // 
      // lblCompatibilityNote
      // 
      this.lblCompatibilityNote.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                  | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.lblCompatibilityNote.BackColor = System.Drawing.SystemColors.Control;
      this.lblCompatibilityNote.BorderStyle = System.Windows.Forms.BorderStyle.None;
      this.lblCompatibilityNote.Location = new System.Drawing.Point(7, 30);
      this.lblCompatibilityNote.Name = "lblCompatibilityNote";
      this.lblCompatibilityNote.Size = new System.Drawing.Size(475, 47);
      this.lblCompatibilityNote.TabIndex = 5;
      this.lblCompatibilityNote.Text = "Sample text.";
      this.lblCompatibilityNote.ContentsResized += new System.Windows.Forms.ContentsResizedEventHandler(this.lblCompatibilityNote_ContentsResized);
      // 
      // pnlTop
      // 
      this.pnlTop.Controls.Add(this.lblTitle);
      this.pnlTop.Controls.Add(this.lblCompatibilityNote);
      this.pnlTop.Dock = System.Windows.Forms.DockStyle.Top;
      this.pnlTop.Location = new System.Drawing.Point(0, 0);
      this.pnlTop.Name = "pnlTop";
      this.pnlTop.Size = new System.Drawing.Size(488, 77);
      this.pnlTop.TabIndex = 6;
      // 
      // lblTitle
      // 
      this.lblTitle.AutoSize = true;
      this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lblTitle.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
      this.lblTitle.Location = new System.Drawing.Point(4, 7);
      this.lblTitle.Name = "lblTitle";
      this.lblTitle.Size = new System.Drawing.Size(43, 20);
      this.lblTitle.TabIndex = 6;
      this.lblTitle.Text = "Title";
      // 
      // FrmResharperCompatibility
      // 
      this.AcceptButton = this.btnClose;
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.CancelButton = this.btnClose;
      this.ClientSize = new System.Drawing.Size(488, 327);
      this.Controls.Add(this.pnlOptions);
      this.Controls.Add(this.pnlBottom);
      this.Controls.Add(this.pnlTop);
      this.MinimizeBox = false;
      this.Name = "FrmResharperCompatibility";
      this.ShowIcon = false;
      this.ShowInTaskbar = false;
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
      this.Text = "Compatibility Note";
      this.pnlOptions.ResumeLayout(false);
      this.pnlOptions.PerformLayout();
      this.pnlBottom.ResumeLayout(false);
      this.pnlBottom.PerformLayout();
      this.pnlTop.ResumeLayout(false);
      this.pnlTop.PerformLayout();
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.CheckBox chkAlwaysPerformThisAction;
    private System.Windows.Forms.Panel pnlOptions;
    private System.Windows.Forms.Label lblOptions;
    private System.Windows.Forms.Panel pnlBottom;
    private System.Windows.Forms.Button btnClose;
    private System.Windows.Forms.ToolTip toolTip1;
    private System.Windows.Forms.RichTextBox lblCompatibilityNote;
    private System.Windows.Forms.Panel pnlTop;
    private System.Windows.Forms.Label lblTitle;

  }
}