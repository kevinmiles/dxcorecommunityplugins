namespace CR_ReSharperCompatibility
{
  partial class CodeGeneration
  {
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    public CodeGeneration()
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CodeGeneration));
      this.actReSharperInsertLiveTemplate = new DevExpress.CodeRush.Core.Action(this.components);
      this.actReSharperCreateFileFromTemplate = new DevExpress.CodeRush.Core.Action(this.components);
      ((System.ComponentModel.ISupportInitialize)(this.actReSharperInsertLiveTemplate)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.actReSharperCreateFileFromTemplate)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
      // 
      // actReSharperInsertLiveTemplate
      // 
      this.actReSharperInsertLiveTemplate.ActionName = "ReSharper.InsertLiveTemplate";
      this.actReSharperInsertLiveTemplate.CommonMenu = DevExpress.CodeRush.Menus.VsCommonBar.None;
      this.actReSharperInsertLiveTemplate.Description = "Compatible with ReSharper\'s \"Insert live template\" feature.";
      this.actReSharperInsertLiveTemplate.Image = ((System.Drawing.Bitmap)(resources.GetObject("actReSharperInsertLiveTemplate.Image")));
      this.actReSharperInsertLiveTemplate.ImageBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(254)))), ((int)(((byte)(0)))));
      this.actReSharperInsertLiveTemplate.RegisterInCR = false;
      // 
      // actReSharperCreateFileFromTemplate
      // 
      this.actReSharperCreateFileFromTemplate.ActionName = "ReSharper.CreateFileFromTemplate";
      this.actReSharperCreateFileFromTemplate.CommonMenu = DevExpress.CodeRush.Menus.VsCommonBar.None;
      this.actReSharperCreateFileFromTemplate.Description = "Compatible with ReSharper\'s \"Create file from template\" feature.";
      this.actReSharperCreateFileFromTemplate.Image = ((System.Drawing.Bitmap)(resources.GetObject("actReSharperCreateFileFromTemplate.Image")));
      this.actReSharperCreateFileFromTemplate.ImageBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(254)))), ((int)(((byte)(0)))));
      this.actReSharperCreateFileFromTemplate.RegisterInCR = false;
      ((System.ComponentModel.ISupportInitialize)(this.actReSharperInsertLiveTemplate)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.actReSharperCreateFileFromTemplate)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

    }

    #endregion

    private DevExpress.CodeRush.Core.Action actReSharperInsertLiveTemplate;
    private DevExpress.CodeRush.Core.Action actReSharperCreateFileFromTemplate;
  }
}