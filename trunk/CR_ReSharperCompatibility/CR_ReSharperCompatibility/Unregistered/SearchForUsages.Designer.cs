namespace CR_ReSharperCompatibility
{
  partial class SearchForUsages
  {
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    public SearchForUsages()
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SearchForUsages));
      this.actReSharperFindResults = new DevExpress.CodeRush.Core.Action(this.components);
      this.actReSharperFindUsagesAdvanced = new DevExpress.CodeRush.Core.Action(this.components);
      this.actReSharperGoToTypeDeclaration = new DevExpress.CodeRush.Core.Action(this.components);
      ((System.ComponentModel.ISupportInitialize)(this.actReSharperFindResults)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.actReSharperFindUsagesAdvanced)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.actReSharperGoToTypeDeclaration)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
      // 
      // actReSharperFindResults
      // 
      this.actReSharperFindResults.ActionName = "ReSharper.FindResults";
      this.actReSharperFindResults.CommonMenu = DevExpress.CodeRush.Menus.VsCommonBar.None;
      this.actReSharperFindResults.Description = "Compatible with ReSharper\'s \"Find results\" feature.";
      this.actReSharperFindResults.Image = ((System.Drawing.Bitmap)(resources.GetObject("actReSharperFindResults.Image")));
      this.actReSharperFindResults.ImageBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(254)))), ((int)(((byte)(0)))));
      this.actReSharperFindResults.RegisterInCR = false;
      // 
      // actReSharperFindUsagesAdvanced
      // 
      this.actReSharperFindUsagesAdvanced.ActionName = "ReSharper.FindUsagesAdvanced";
      this.actReSharperFindUsagesAdvanced.CommonMenu = DevExpress.CodeRush.Menus.VsCommonBar.None;
      this.actReSharperFindUsagesAdvanced.Description = "Compatible with ReSharper\'s \"Find usages advanced\" feature.";
      this.actReSharperFindUsagesAdvanced.Image = ((System.Drawing.Bitmap)(resources.GetObject("actReSharperFindUsagesAdvanced.Image")));
      this.actReSharperFindUsagesAdvanced.ImageBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(254)))), ((int)(((byte)(0)))));
      this.actReSharperFindUsagesAdvanced.RegisterInCR = false;
      // 
      // actReSharperGoToTypeDeclaration
      // 
      this.actReSharperGoToTypeDeclaration.ActionName = "ReSharper.GoToTypeDeclaration";
      this.actReSharperGoToTypeDeclaration.CommonMenu = DevExpress.CodeRush.Menus.VsCommonBar.None;
      this.actReSharperGoToTypeDeclaration.Description = "Compatible with ReSharper\'s \"Go to type declaration\" feature.";
      this.actReSharperGoToTypeDeclaration.Image = ((System.Drawing.Bitmap)(resources.GetObject("actReSharperGoToTypeDeclaration.Image")));
      this.actReSharperGoToTypeDeclaration.ImageBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(254)))), ((int)(((byte)(0)))));
      this.actReSharperGoToTypeDeclaration.RegisterInCR = false;
      ((System.ComponentModel.ISupportInitialize)(this.actReSharperFindResults)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.actReSharperFindUsagesAdvanced)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.actReSharperGoToTypeDeclaration)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

    }

    #endregion

    private DevExpress.CodeRush.Core.Action actReSharperFindResults;
    private DevExpress.CodeRush.Core.Action actReSharperFindUsagesAdvanced;
    private DevExpress.CodeRush.Core.Action actReSharperGoToTypeDeclaration;
  }
}