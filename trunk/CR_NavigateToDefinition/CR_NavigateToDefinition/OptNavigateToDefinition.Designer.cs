using System;
using System.Windows.Forms;
using DevExpress.CodeRush.Core;

namespace CR_NavigateToDefinition
{
  public partial class OptNavigateToDefinition
  {
#pragma warning disable 
    private System.ComponentModel.Container components;
#pragma warning restore

    private void InitializeComponent()
    {
      this.chkEnabled = new System.Windows.Forms.CheckBox();
      this.chkDropMarker = new System.Windows.Forms.CheckBox();
      this.mainPanel = new System.Windows.Forms.Panel();
      this.chkShowBeacon = new System.Windows.Forms.CheckBox();
      this.chkUseGoToDef = new System.Windows.Forms.CheckBox();
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
      this.chkEnabled.TabIndex = 6;
      this.chkEnabled.Text = "Enabled";
      this.chkEnabled.UseVisualStyleBackColor = true;
      // 
      // chkDropMarker
      // 
      this.chkDropMarker.FlatStyle = System.Windows.Forms.FlatStyle.System;
      this.chkDropMarker.Location = new System.Drawing.Point(3, 4);
      this.chkDropMarker.Name = "chkDropMarker";
      this.chkDropMarker.Size = new System.Drawing.Size(184, 24);
      this.chkDropMarker.TabIndex = 0;
      this.chkDropMarker.Text = "Drop Marker";
      // 
      // mainPanel
      // 
      this.mainPanel.Controls.Add(this.chkUseGoToDef);
      this.mainPanel.Controls.Add(this.chkShowBeacon);
      this.mainPanel.Controls.Add(this.chkDropMarker);
      this.mainPanel.Location = new System.Drawing.Point(13, 27);
      this.mainPanel.Name = "mainPanel";
      this.mainPanel.Size = new System.Drawing.Size(333, 351);
      this.mainPanel.TabIndex = 5;
      // 
      // chkShowBeacon
      // 
      this.chkShowBeacon.FlatStyle = System.Windows.Forms.FlatStyle.System;
      this.chkShowBeacon.Location = new System.Drawing.Point(3, 34);
      this.chkShowBeacon.Name = "chkShowBeacon";
      this.chkShowBeacon.Size = new System.Drawing.Size(184, 24);
      this.chkShowBeacon.TabIndex = 1;
      this.chkShowBeacon.Text = "Show Beacon";
      // 
      // chkUseGoToDef
      // 
      this.chkUseGoToDef.FlatStyle = System.Windows.Forms.FlatStyle.System;
      this.chkUseGoToDef.Location = new System.Drawing.Point(3, 64);
      this.chkUseGoToDef.Name = "chkUseGoToDef";
      this.chkUseGoToDef.Size = new System.Drawing.Size(301, 24);
      this.chkUseGoToDef.TabIndex = 2;
      this.chkUseGoToDef.Text = "When member not found use standard Go To Definition";
      // 
      // OptNavigateToDefinition
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.Controls.Add(this.chkEnabled);
      this.Controls.Add(this.mainPanel);
      this.Name = "OptNavigateToDefinition";
      this.Size = new System.Drawing.Size(661, 617);
      this.Load += new System.EventHandler(this.OptNavigateToDefinition_Load);
      this.PreparePage += new DevExpress.CodeRush.Core.OptionsPage.PreparePageEventHandler(this.OptNavigateToDefinition_PreparePage);
      this.CommitChanges += new DevExpress.CodeRush.Core.OptionsPage.CommitChangesEventHandler(this.OptNavigateToDefinition_CommitChanges);
      this.mainPanel.ResumeLayout(false);
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
      return @"Editor\Navigation";
    }

    public static string GetPageName()
    {
      return @"Navigate to Definition";
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
        return OptNavigateToDefinition.GetCategory();
      }
    }

    public override string PageName
    {
      get
      {
        return OptNavigateToDefinition.GetPageName();
      }
    }

    public new static void Show()
    {
      CodeRush.Command.Execute("Options", FullPath);
    }
    private CheckBox chkEnabled;
    private CheckBox chkDropMarker;
    private Panel mainPanel;
    private CheckBox chkShowBeacon;
    private CheckBox chkUseGoToDef;

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
