namespace RenameXamlNamespacePrefix
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
			this.rpRenameXamlNamespacePrefix = new DevExpress.Refactor.Core.RefactoringProvider(this.components);
			((System.ComponentModel.ISupportInitialize)(this.rpRenameXamlNamespacePrefix)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
			// 
			// rpRenameXamlNamespacePrefix
			// 
			this.rpRenameXamlNamespacePrefix.ActionHintText = "";
			this.rpRenameXamlNamespacePrefix.AutoActivate = true;
			this.rpRenameXamlNamespacePrefix.AutoUndo = false;
			this.rpRenameXamlNamespacePrefix.CodeIssueMessage = null;
			this.rpRenameXamlNamespacePrefix.Description = "Renames the XAML namespace prefix at the caret.";
			this.rpRenameXamlNamespacePrefix.DisplayName = "Rename XAML Namespace Prefix";
			this.rpRenameXamlNamespacePrefix.Image = ((System.Drawing.Bitmap)(resources.GetObject("rpRenameXamlNamespacePrefix.Image")));
			this.rpRenameXamlNamespacePrefix.NeedsSelection = false;
			this.rpRenameXamlNamespacePrefix.ProviderName = "RenameXamlNamespacePrefix";
			this.rpRenameXamlNamespacePrefix.Register = true;
			this.rpRenameXamlNamespacePrefix.SupportsAsyncMode = false;
			this.rpRenameXamlNamespacePrefix.CheckAvailability += new DevExpress.Refactor.Core.CheckAvailabilityEventHandler(this.rpRenameXamlNamespacePrefix_CheckAvailability);
			this.rpRenameXamlNamespacePrefix.LanguageSupported += new DevExpress.CodeRush.Core.LanguageSupportedEventHandler(this.rpRenameXamlNamespacePrefix_LanguageSupported);
			this.rpRenameXamlNamespacePrefix.Apply += new DevExpress.Refactor.Core.ApplyRefactoringEventHandler(this.rpRenameXamlNamespacePrefix_Apply);
			this.rpRenameXamlNamespacePrefix.PreparePreview += new DevExpress.Refactor.Core.PrepareRefactoringPreviewEventHandler(this.rpRenameXamlNamespacePrefix_PreparePreview);
			((System.ComponentModel.ISupportInitialize)(this.rpRenameXamlNamespacePrefix)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this)).EndInit();

    }

    #endregion

    private DevExpress.Refactor.Core.RefactoringProvider rpRenameXamlNamespacePrefix;
  }
}