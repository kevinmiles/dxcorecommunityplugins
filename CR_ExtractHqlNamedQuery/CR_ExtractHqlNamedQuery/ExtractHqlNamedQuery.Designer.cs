namespace CR_ExtractHqlNamedQuery
{
  partial class ExtractHqlNamedQuery
  {
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    public ExtractHqlNamedQuery()
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ExtractHqlNamedQuery));
      this.refactoringProvider1 = new DevExpress.Refactor.Core.RefactoringProvider(this.components);
      this.action1 = new DevExpress.CodeRush.Core.Action(this.components);
      ((System.ComponentModel.ISupportInitialize)(this.refactoringProvider1)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.action1)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
      // 
      // refactoringProvider1
      // 
      this.refactoringProvider1.ActionHintText = "Extract Hql Named Query";
      this.refactoringProvider1.AutoActivate = true;
      this.refactoringProvider1.AutoUndo = false;
      this.refactoringProvider1.CodeIssueMessage = null;
      this.refactoringProvider1.Description = "Extract Hql Named Query";
      this.refactoringProvider1.DisplayName = "Extract Hql Named Query";
      this.refactoringProvider1.Image = ((System.Drawing.Bitmap)(resources.GetObject("refactoringProvider1.Image")));
      this.refactoringProvider1.NeedsSelection = false;
      this.refactoringProvider1.ProviderName = "ExtractHqlNamedQuery";
      this.refactoringProvider1.Register = true;
      this.refactoringProvider1.SupportsAsyncMode = false;
      this.refactoringProvider1.Apply += new DevExpress.Refactor.Core.ApplyRefactoringEventHandler(this.refactoringProvider1_Apply);
      this.refactoringProvider1.CheckAvailability += new DevExpress.Refactor.Core.CheckAvailabilityEventHandler(this.refactoringProvider1_CheckAvailability);
      // 
      // action1
      // 
      this.action1.ActionName = "ExtractHqlNamedQuery";
      this.action1.ButtonText = "Extract Hql Named Query";
      this.action1.CommonMenu = DevExpress.CodeRush.Menus.VsCommonBar.None;
      this.action1.Image = ((System.Drawing.Bitmap)(resources.GetObject("action1.Image")));
      this.action1.ImageBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(254)))), ((int)(((byte)(0)))));
      this.action1.Execute += new DevExpress.CodeRush.Core.CommandExecuteEventHandler(this.action1_Execute);
      // 
      // ExtractHqlNamedQuery
      // 
      this.OptionsChanged += new DevExpress.CodeRush.Core.OptionsChangedEventHandler(this.ExtractHqlNamedQuery_OptionsChanged);
      ((System.ComponentModel.ISupportInitialize)(this.refactoringProvider1)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.action1)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

    }

    #endregion

    private DevExpress.Refactor.Core.RefactoringProvider refactoringProvider1;
    private DevExpress.CodeRush.Core.Action action1;
  }
}