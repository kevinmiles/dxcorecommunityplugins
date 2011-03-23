namespace CR_ExtensionMethodsHelper
{
  partial class PlugIn1
  {
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    public PlugIn1()
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PlugIn1));
      this.cpAddReference = new DevExpress.CodeRush.Core.CodeProvider(this.components);
      ((System.ComponentModel.ISupportInitialize)(this.cpAddReference)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
      // 
      // cpAddReference
      // 
      this.cpAddReference.ActionHintText = "";
      this.cpAddReference.AutoActivate = true;
      this.cpAddReference.AutoUndo = true;
      this.cpAddReference.CodeIssueMessage = null;
      this.cpAddReference.Description = "Add a namespace reference for the target extension method.";
      this.cpAddReference.Image = ((System.Drawing.Bitmap)(resources.GetObject("cpAddReference.Image")));
      this.cpAddReference.NeedsSelection = false;
      this.cpAddReference.ProviderName = "Add Namespace Reference";
      this.cpAddReference.Register = true;
      this.cpAddReference.SupportsAsyncMode = true;
      this.cpAddReference.CheckAvailability += new DevExpress.Refactor.Core.CheckAvailabilityEventHandler(this.cpAddReference_CheckAvailability);
      this.cpAddReference.Apply += new DevExpress.Refactor.Core.ApplyRefactoringEventHandler(this.cpAddReference_Apply);
      ((System.ComponentModel.ISupportInitialize)(this.cpAddReference)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

    }

    #endregion

    private DevExpress.CodeRush.Core.CodeProvider cpAddReference;
  }
}