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
        this.ConvertPropertyProvider = new DevExpress.CodeRush.Core.CodeProvider(this.components);
        ((System.ComponentModel.ISupportInitialize)(this.ConvertPropertyProvider)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
        // 
        // ConvertPropertyProvider
        // 
        this.ConvertPropertyProvider.ActionHintText = "";
        this.ConvertPropertyProvider.AutoActivate = true;
        this.ConvertPropertyProvider.AutoUndo = true;
        this.ConvertPropertyProvider.Description = "Promotes simple property to PropertyChanged source property.";
        this.ConvertPropertyProvider.DisplayName = "Convert to PropertyChanged Property";
        this.ConvertPropertyProvider.ProviderName = "Convert to PropertyChangedProperty";
        this.ConvertPropertyProvider.Register = true;
        this.ConvertPropertyProvider.Apply += new DevExpress.Refactor.Core.ApplyRefactoringEventHandler(this.Apply);
        this.ConvertPropertyProvider.CheckAvailability += new DevExpress.Refactor.Core.CheckAvailabilityEventHandler(this.CheckAvailability);
        ((System.ComponentModel.ISupportInitialize)(this.ConvertPropertyProvider)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

    }

    #endregion

    private DevExpress.CodeRush.Core.CodeProvider ConvertPropertyProvider;
  }
}