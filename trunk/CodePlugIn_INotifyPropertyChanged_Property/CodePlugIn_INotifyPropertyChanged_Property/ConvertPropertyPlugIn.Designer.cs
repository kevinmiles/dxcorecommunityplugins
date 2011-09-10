namespace Refactor_ConvertProperty
{
  partial class ConvertPropertyPlugIn
  {
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    public ConvertPropertyPlugIn()
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ConvertPropertyPlugIn));
            this.ConvertPropertyProviderBasic = new DevExpress.CodeRush.Core.CodeProvider(this.components);
            this.ConvertPropertyINPCBaseClass = new DevExpress.CodeRush.Core.CodeProvider(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.ConvertPropertyProviderBasic)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ConvertPropertyINPCBaseClass)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // ConvertPropertyProviderBasic
            // 
            this.ConvertPropertyProviderBasic.ActionHintText = "";
            this.ConvertPropertyProviderBasic.AutoActivate = true;
            this.ConvertPropertyProviderBasic.AutoUndo = true;
            this.ConvertPropertyProviderBasic.CodeIssueMessage = null;
            this.ConvertPropertyProviderBasic.Description = "Promotes simple property to PropertyChanged source property.";
            this.ConvertPropertyProviderBasic.Image = ((System.Drawing.Bitmap)(resources.GetObject("ConvertPropertyProviderBasic.Image")));
            this.ConvertPropertyProviderBasic.NeedsSelection = false;
            this.ConvertPropertyProviderBasic.ProviderName = "Convert to INPC Property";
            this.ConvertPropertyProviderBasic.Register = true;
            this.ConvertPropertyProviderBasic.SupportsAsyncMode = false;
            this.ConvertPropertyProviderBasic.CheckAvailability += new DevExpress.Refactor.Core.CheckAvailabilityEventHandler(this.CheckAvailability);
            this.ConvertPropertyProviderBasic.Apply += new DevExpress.Refactor.Core.ApplyRefactoringEventHandler(this.ApplyINPCBasic);
            // 
            // ConvertPropertyINPCBaseClass
            // 
            this.ConvertPropertyINPCBaseClass.ActionHintText = "";
            this.ConvertPropertyINPCBaseClass.AutoActivate = true;
            this.ConvertPropertyINPCBaseClass.AutoUndo = true;
            this.ConvertPropertyINPCBaseClass.CodeIssueMessage = null;
            this.ConvertPropertyINPCBaseClass.Description = "Promotes simple property to PropertyChanged source property with base class metho" +
    "d call.";
            this.ConvertPropertyINPCBaseClass.Image = ((System.Drawing.Bitmap)(resources.GetObject("ConvertPropertyINPCBaseClass.Image")));
            this.ConvertPropertyINPCBaseClass.NeedsSelection = false;
            this.ConvertPropertyINPCBaseClass.ProviderName = "Convert to INPC Property Base Class Call";
            this.ConvertPropertyINPCBaseClass.Register = true;
            this.ConvertPropertyINPCBaseClass.SupportsAsyncMode = false;
            this.ConvertPropertyINPCBaseClass.CheckAvailability += new DevExpress.Refactor.Core.CheckAvailabilityEventHandler(this.CheckAvailability);
            this.ConvertPropertyINPCBaseClass.Apply += new DevExpress.Refactor.Core.ApplyRefactoringEventHandler(this.ApplyINPCBaseClassOnPropertyChanged);
            ((System.ComponentModel.ISupportInitialize)(this.ConvertPropertyProviderBasic)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ConvertPropertyINPCBaseClass)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

    }

    #endregion

    private DevExpress.CodeRush.Core.CodeProvider ConvertPropertyProviderBasic;
    private DevExpress.CodeRush.Core.CodeProvider ConvertPropertyINPCBaseClass;
  }
}