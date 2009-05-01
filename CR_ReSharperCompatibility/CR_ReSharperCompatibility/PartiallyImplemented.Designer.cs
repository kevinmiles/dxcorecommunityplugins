namespace CR_ReSharperCompatibility
{
  partial class PartiallyImplemented
  {
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    public PartiallyImplemented()
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PartiallyImplemented));
      this.actReSharperChangeSignature = new DevExpress.CodeRush.Core.Action(this.components);
      this.actReSharperSurroundWithTemplate = new DevExpress.CodeRush.Core.Action(this.components);
      ((System.ComponentModel.ISupportInitialize)(this.actReSharperChangeSignature)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.actReSharperSurroundWithTemplate)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
      // 
      // actReSharperChangeSignature
      // 
      this.actReSharperChangeSignature.ActionName = "ReSharper.ChangeSignature";
      this.actReSharperChangeSignature.CommonMenu = DevExpress.CodeRush.Menus.VsCommonBar.None;
      this.actReSharperChangeSignature.Description = "Compatible with ReSharper\'s \"Change signature\" refactoring.";
      this.actReSharperChangeSignature.Image = ((System.Drawing.Bitmap)(resources.GetObject("actReSharperChangeSignature.Image")));
      this.actReSharperChangeSignature.ImageBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(254)))), ((int)(((byte)(0)))));
      this.actReSharperChangeSignature.Execute += new DevExpress.CodeRush.Core.CommandExecuteEventHandler(this.actReSharperChangeSignature_Execute);
      // 
      // actReSharperSurroundWithTemplate
      // 
      this.actReSharperSurroundWithTemplate.ActionName = "ReSharper.SurroundWithTemplate";
      this.actReSharperSurroundWithTemplate.CommonMenu = DevExpress.CodeRush.Menus.VsCommonBar.None;
      this.actReSharperSurroundWithTemplate.Description = "Compatible with ReSharper\'s \"Surround with template\" feature.";
      this.actReSharperSurroundWithTemplate.Image = ((System.Drawing.Bitmap)(resources.GetObject("actReSharperSurroundWithTemplate.Image")));
      this.actReSharperSurroundWithTemplate.ImageBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(254)))), ((int)(((byte)(0)))));
      this.actReSharperSurroundWithTemplate.Execute += new DevExpress.CodeRush.Core.CommandExecuteEventHandler(this.actReSharperSurroundWithTemplate_Execute);
      ((System.ComponentModel.ISupportInitialize)(this.actReSharperChangeSignature)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.actReSharperSurroundWithTemplate)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

    }

    #endregion

    private DevExpress.CodeRush.Core.Action actReSharperChangeSignature;
    private DevExpress.CodeRush.Core.Action actReSharperSurroundWithTemplate;
  }
}