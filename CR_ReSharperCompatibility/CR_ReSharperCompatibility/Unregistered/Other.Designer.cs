namespace CR_ReSharperCompatibility.Unregistered
{
  partial class Other
  {
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    public Other()
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Other));
      this.actReSharperLocateInSolutionExplorer = new DevExpress.CodeRush.Core.Action(this.components);
      this.actReSharperCloseRecentTool = new DevExpress.CodeRush.Core.Action(this.components);
      this.actReSharperActivateRecentTool = new DevExpress.CodeRush.Core.Action(this.components);
      ((System.ComponentModel.ISupportInitialize)(this.actReSharperLocateInSolutionExplorer)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.actReSharperCloseRecentTool)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.actReSharperActivateRecentTool)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
      // 
      // actReSharperLocateInSolutionExplorer
      // 
      this.actReSharperLocateInSolutionExplorer.ActionName = "ReSharper.LocateInSolutionExplorer";
      this.actReSharperLocateInSolutionExplorer.CommonMenu = DevExpress.CodeRush.Menus.VsCommonBar.None;
      this.actReSharperLocateInSolutionExplorer.Description = "Compatible with ReSharper\'s \"Locate in Solution Explorer\" feature.";
      this.actReSharperLocateInSolutionExplorer.Image = ((System.Drawing.Bitmap)(resources.GetObject("actReSharperLocateInSolutionExplorer.Image")));
      this.actReSharperLocateInSolutionExplorer.ImageBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(254)))), ((int)(((byte)(0)))));
      this.actReSharperLocateInSolutionExplorer.RegisterInCR = false;
      // 
      // actReSharperCloseRecentTool
      // 
      this.actReSharperCloseRecentTool.ActionName = "ReSharper.CloseRecentTool";
      this.actReSharperCloseRecentTool.CommonMenu = DevExpress.CodeRush.Menus.VsCommonBar.None;
      this.actReSharperCloseRecentTool.Description = "Compatible with ReSharper\'s \"Close recent tool\" feature.";
      this.actReSharperCloseRecentTool.Image = ((System.Drawing.Bitmap)(resources.GetObject("actReSharperCloseRecentTool.Image")));
      this.actReSharperCloseRecentTool.ImageBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(254)))), ((int)(((byte)(0)))));
      this.actReSharperCloseRecentTool.RegisterInCR = false;
      // 
      // actReSharperActivateRecentTool
      // 
      this.actReSharperActivateRecentTool.ActionName = "ReSharper.ActivateRecentTool";
      this.actReSharperActivateRecentTool.CommonMenu = DevExpress.CodeRush.Menus.VsCommonBar.None;
      this.actReSharperActivateRecentTool.Description = "Compatible with ReSharper\'s \"Activate recent tool\" feature.";
      this.actReSharperActivateRecentTool.Image = ((System.Drawing.Bitmap)(resources.GetObject("actReSharperActivateRecentTool.Image")));
      this.actReSharperActivateRecentTool.ImageBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(254)))), ((int)(((byte)(0)))));
      this.actReSharperActivateRecentTool.RegisterInCR = false;
      ((System.ComponentModel.ISupportInitialize)(this.actReSharperLocateInSolutionExplorer)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.actReSharperCloseRecentTool)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.actReSharperActivateRecentTool)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

    }

    #endregion

    private DevExpress.CodeRush.Core.Action actReSharperLocateInSolutionExplorer;
    private DevExpress.CodeRush.Core.Action actReSharperCloseRecentTool;
    private DevExpress.CodeRush.Core.Action actReSharperActivateRecentTool;
  }
}