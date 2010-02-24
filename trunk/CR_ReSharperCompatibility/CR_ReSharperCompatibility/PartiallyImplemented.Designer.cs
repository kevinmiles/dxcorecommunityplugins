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
        this.actReSharperMoveCodeRight = new DevExpress.CodeRush.Core.Action(this.components);
        this.actReSharperMoveCodeUp = new DevExpress.CodeRush.Core.Action(this.components);
        this.actReSharperMoveCodeDown = new DevExpress.CodeRush.Core.Action(this.components);
        this.actReSharperMoveCodeLeft = new DevExpress.CodeRush.Core.Action(this.components);
        ((System.ComponentModel.ISupportInitialize)(this.actReSharperChangeSignature)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.actReSharperSurroundWithTemplate)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.actReSharperMoveCodeRight)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.actReSharperMoveCodeUp)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.actReSharperMoveCodeDown)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.actReSharperMoveCodeLeft)).BeginInit();
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
        // 
        // actReSharperMoveCodeRight
        // 
        this.actReSharperMoveCodeRight.ActionName = "ReSharper.MoveCodeRight";
        this.actReSharperMoveCodeRight.CommonMenu = DevExpress.CodeRush.Menus.VsCommonBar.None;
        this.actReSharperMoveCodeRight.Description = "Compatible with ReSharper\'s \"Move Code Right\" feature.";
        this.actReSharperMoveCodeRight.Image = ((System.Drawing.Bitmap)(resources.GetObject("actReSharperMoveCodeRight.Image")));
        this.actReSharperMoveCodeRight.ImageBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(254)))), ((int)(((byte)(0)))));
        this.actReSharperMoveCodeRight.Execute += new DevExpress.CodeRush.Core.CommandExecuteEventHandler(this.actReSharperMoveCodeRight_Execute);
        // 
        // actReSharperMoveCodeUp
        // 
        this.actReSharperMoveCodeUp.ActionName = "ReSharper.MoveCodeUp";
        this.actReSharperMoveCodeUp.CommonMenu = DevExpress.CodeRush.Menus.VsCommonBar.None;
        this.actReSharperMoveCodeUp.Description = "Compatible with ReSharper\'s \"Move Code Up\" feature.";
        this.actReSharperMoveCodeUp.Image = ((System.Drawing.Bitmap)(resources.GetObject("actReSharperMoveCodeUp.Image")));
        this.actReSharperMoveCodeUp.ImageBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(254)))), ((int)(((byte)(0)))));
        this.actReSharperMoveCodeUp.Execute += new DevExpress.CodeRush.Core.CommandExecuteEventHandler(this.actReSharperMoveCodeUp_Execute);
        // 
        // actReSharperMoveCodeDown
        // 
        this.actReSharperMoveCodeDown.ActionName = "ReSharper.MoveCodeDown";
        this.actReSharperMoveCodeDown.CommonMenu = DevExpress.CodeRush.Menus.VsCommonBar.None;
        this.actReSharperMoveCodeDown.Description = "Compatible with ReSharper\'s \"Move Code Down\" feature.";
        this.actReSharperMoveCodeDown.Image = ((System.Drawing.Bitmap)(resources.GetObject("actReSharperMoveCodeDown.Image")));
        this.actReSharperMoveCodeDown.ImageBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(254)))), ((int)(((byte)(0)))));
        this.actReSharperMoveCodeDown.Execute += new DevExpress.CodeRush.Core.CommandExecuteEventHandler(this.actReSharperMoveCodeDown_Execute);
        // 
        // actReSharperMoveCodeLeft
        // 
        this.actReSharperMoveCodeLeft.ActionName = "ReSharper.MoveCodeLeft";
        this.actReSharperMoveCodeLeft.CommonMenu = DevExpress.CodeRush.Menus.VsCommonBar.None;
        this.actReSharperMoveCodeLeft.Description = "Compatible with ReSharper\'s \"Move Code Left\" feature.";
        this.actReSharperMoveCodeLeft.Image = ((System.Drawing.Bitmap)(resources.GetObject("actReSharperMoveCodeLeft.Image")));
        this.actReSharperMoveCodeLeft.ImageBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(254)))), ((int)(((byte)(0)))));
        this.actReSharperMoveCodeLeft.Execute += new DevExpress.CodeRush.Core.CommandExecuteEventHandler(this.actReSharperMoveCodeLeft_Execute);
        ((System.ComponentModel.ISupportInitialize)(this.actReSharperChangeSignature)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.actReSharperSurroundWithTemplate)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.actReSharperMoveCodeRight)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.actReSharperMoveCodeUp)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.actReSharperMoveCodeDown)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.actReSharperMoveCodeLeft)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

    }

    #endregion

    private DevExpress.CodeRush.Core.Action actReSharperChangeSignature;
    private DevExpress.CodeRush.Core.Action actReSharperSurroundWithTemplate;
    private DevExpress.CodeRush.Core.Action actReSharperMoveCodeRight;
    private DevExpress.CodeRush.Core.Action actReSharperMoveCodeUp;
    private DevExpress.CodeRush.Core.Action actReSharperMoveCodeDown;
    private DevExpress.CodeRush.Core.Action actReSharperMoveCodeLeft;
  }
}