namespace CR_NavigateToDefinition
{
  partial class NavigateToDefinition
  {
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    public NavigateToDefinition()
    {
      /// <summary>
      /// Required for Windows.Forms Class Composition Designer support
      /// </summary>
      InitializeComponent();
    }

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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NavigateToDefinition));
      this.navigationProvider1 = new DevExpress.CodeRush.Library.NavigationProvider(this.components);
      this.action1 = new DevExpress.CodeRush.Core.Action(this.components);
      this.locatorBeacon1 = new DevExpress.CodeRush.PlugInCore.LocatorBeacon(this.components);
      ((System.ComponentModel.ISupportInitialize)(this.navigationProvider1)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.action1)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.locatorBeacon1)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
      // 
      // navigationProvider1
      // 
      this.navigationProvider1.ActionHintText = "Navigates to definition of member";
      this.navigationProvider1.AutoActivate = true;
      this.navigationProvider1.AutoUndo = false;
      this.navigationProvider1.CodeIssueMessage = null;
      this.navigationProvider1.Description = "";
      this.navigationProvider1.DisplayName = "Navigate To Definition";
      this.navigationProvider1.Image = ((System.Drawing.Bitmap)(resources.GetObject("navigationProvider1.Image")));
      this.navigationProvider1.NeedsSelection = false;
      this.navigationProvider1.ProviderName = "NavigateToDefinitionProvider";
      this.navigationProvider1.Register = true;
      this.navigationProvider1.SupportsAsyncMode = false;
      this.navigationProvider1.Navigate += new DevExpress.CodeRush.Library.NavigationEventHandler(this.navigationProvider1_Navigate);
      this.navigationProvider1.CheckAvailability += new DevExpress.Refactor.Core.CheckAvailabilityEventHandler(this.navigationProvider1_CheckAvailability);
      // 
      // action1
      // 
      this.action1.ActionName = "NavigateToDefinition";
      this.action1.ButtonText = "Navigate To Definition";
      this.action1.CommonMenu = DevExpress.CodeRush.Menus.VsCommonBar.None;
      this.action1.Image = ((System.Drawing.Bitmap)(resources.GetObject("action1.Image")));
      this.action1.ImageBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(254)))), ((int)(((byte)(0)))));
      this.action1.Execute += new DevExpress.CodeRush.Core.CommandExecuteEventHandler(this.action1_Execute);
      // 
      // locatorBeacon1
      // 
      this.locatorBeacon1.Color = System.Drawing.Color.Lime;
      this.locatorBeacon1.Duration = 500;
      // 
      // NavigateToDefinition
      // 
      this.OptionsChanged += new DevExpress.CodeRush.Core.OptionsChangedEventHandler(this.NavigateToDefinition_OptionsChanged);
      ((System.ComponentModel.ISupportInitialize)(this.navigationProvider1)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.action1)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.locatorBeacon1)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

    }

    #endregion

    private DevExpress.CodeRush.Library.NavigationProvider navigationProvider1;
    private DevExpress.CodeRush.Core.Action action1;
    private DevExpress.CodeRush.PlugInCore.LocatorBeacon locatorBeacon1;
  }
}