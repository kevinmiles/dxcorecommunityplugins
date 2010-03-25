using System;
using System.Windows.Forms;
using DevExpress.CodeRush.Core;

namespace CR_ExtractHqlNamedQuery
{
  public partial class OptExtractHqlNamedQuery
  {
#pragma warning disable 
    private System.ComponentModel.Container components;
#pragma warning restore

    private void InitializeComponent()
    {
      this.chkEnabled = new System.Windows.Forms.CheckBox();
      this.mainPanel = new System.Windows.Forms.Panel();
      this.labelFindHqlFileStrategy = new System.Windows.Forms.Label();
      this.labelHqlNamedQueryFileName = new System.Windows.Forms.Label();
      this.comboFindHqlFileStrategy = new System.Windows.Forms.ComboBox();
      this.textHqlNamedQueryFileName = new System.Windows.Forms.TextBox();
      this.mainPanel.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
      this.SuspendLayout();
      // 
      // chkEnabled
      // 
      this.chkEnabled.AutoSize = true;
      this.chkEnabled.Checked = true;
      this.chkEnabled.CheckState = System.Windows.Forms.CheckState.Checked;
      this.chkEnabled.Location = new System.Drawing.Point(4, 4);
      this.chkEnabled.Name = "chkEnabled";
      this.chkEnabled.Size = new System.Drawing.Size(65, 17);
      this.chkEnabled.TabIndex = 0;
      this.chkEnabled.Text = "Enabled";
      this.chkEnabled.UseVisualStyleBackColor = true;
      // 
      // mainPanel
      // 
      this.mainPanel.Controls.Add(this.labelFindHqlFileStrategy);
      this.mainPanel.Controls.Add(this.labelHqlNamedQueryFileName);
      this.mainPanel.Controls.Add(this.comboFindHqlFileStrategy);
      this.mainPanel.Controls.Add(this.textHqlNamedQueryFileName);
      this.mainPanel.Location = new System.Drawing.Point(13, 27);
      this.mainPanel.Name = "mainPanel";
      this.mainPanel.Size = new System.Drawing.Size(333, 351);
      this.mainPanel.TabIndex = 5;
      // 
      // labelFindHqlFileStrategy
      // 
      this.labelFindHqlFileStrategy.AutoSize = true;
      this.labelFindHqlFileStrategy.Location = new System.Drawing.Point(3, 64);
      this.labelFindHqlFileStrategy.Name = "labelFindHqlFileStrategy";
      this.labelFindHqlFileStrategy.Size = new System.Drawing.Size(170, 13);
      this.labelFindHqlFileStrategy.TabIndex = 6;
      this.labelFindHqlFileStrategy.Text = "Search for Hql Named Queries File";
      // 
      // labelHqlNamedQueryFileName
      // 
      this.labelHqlNamedQueryFileName.AutoSize = true;
      this.labelHqlNamedQueryFileName.Location = new System.Drawing.Point(3, 10);
      this.labelHqlNamedQueryFileName.Name = "labelHqlNamedQueryFileName";
      this.labelHqlNamedQueryFileName.Size = new System.Drawing.Size(149, 13);
      this.labelHqlNamedQueryFileName.TabIndex = 5;
      this.labelHqlNamedQueryFileName.Text = "Hql Named Queries File Name";
      // 
      // comboFindHqlFileStrategy
      // 
      this.comboFindHqlFileStrategy.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.comboFindHqlFileStrategy.FormattingEnabled = true;
      this.comboFindHqlFileStrategy.Items.AddRange(new object[] {
            "In current project only",
            "First in current project, then in solution"});
      this.comboFindHqlFileStrategy.Location = new System.Drawing.Point(6, 80);
      this.comboFindHqlFileStrategy.Name = "comboFindHqlFileStrategy";
      this.comboFindHqlFileStrategy.Size = new System.Drawing.Size(298, 21);
      this.comboFindHqlFileStrategy.TabIndex = 1;
      // 
      // textHqlNamedQueryFileName
      // 
      this.textHqlNamedQueryFileName.Location = new System.Drawing.Point(6, 26);
      this.textHqlNamedQueryFileName.Name = "textHqlNamedQueryFileName";
      this.textHqlNamedQueryFileName.Size = new System.Drawing.Size(298, 20);
      this.textHqlNamedQueryFileName.TabIndex = 0;
      this.textHqlNamedQueryFileName.Text = "NamedQueries.hbm.xml";
      // 
      // OptExtractHqlNamedQuery
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.Controls.Add(this.chkEnabled);
      this.Controls.Add(this.mainPanel);
      this.Name = "OptExtractHqlNamedQuery";
      this.Size = new System.Drawing.Size(661, 617);
      this.Load += new System.EventHandler(this.OptExtractHqlNamedQuery_Load);
      this.PreparePage += new DevExpress.CodeRush.Core.OptionsPage.PreparePageEventHandler(this.OptExtractHqlNamedQuery_PreparePage);
      this.CommitChanges += new DevExpress.CodeRush.Core.OptionsPage.CommitChangesEventHandler(this.OptExtractHqlNamedQuery_CommitChanges);
      this.mainPanel.ResumeLayout(false);
      this.mainPanel.PerformLayout();
      ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    protected override void Dispose(bool disposing)
    {
      if (disposing)
      {
        if (components != null)
          components.Dispose();
      }
      base.Dispose(disposing);
    }

    #region CodeRush generated code
    protected override void Initialize()
    {
      base.Initialize();

      //
      // TODO: Add your initialization code here.
      //
    }

    public static string GetCategory()
    {
      return @"Editor\Refactoring";
    }

    public static string GetPageName()
    {
      return @"Extract Hql Named Query";
    }

    public static DecoupledStorage Storage
    {
      get
      {
        return CodeRush.Options.GetStorage(GetCategory(), GetPageName());
      }
    }

    public override string Category
    {
      get
      {
        return OptExtractHqlNamedQuery.GetCategory();
      }
    }

    public override string PageName
    {
      get
      {
        return OptExtractHqlNamedQuery.GetPageName();
      }
    }

    public new static void Show()
    {
      CodeRush.Command.Execute("Options", FullPath);
    }
    private CheckBox chkEnabled;
    private Panel mainPanel;
    private Label labelHqlNamedQueryFileName;
    private ComboBox comboFindHqlFileStrategy;
    private TextBox textHqlNamedQueryFileName;
    private Label labelFindHqlFileStrategy;

    public static string FullPath
    {
      get
      {
        return GetCategory() + "\\" + GetPageName();
      }
    }
    #endregion

  }
}
