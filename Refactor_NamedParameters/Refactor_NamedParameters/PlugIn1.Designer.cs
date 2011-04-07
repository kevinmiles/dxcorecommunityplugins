namespace Refactor_NamedParameters
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
      this.rpUseNamedParameters = new DevExpress.Refactor.Core.RefactoringProvider(this.components);
      ((System.ComponentModel.ISupportInitialize)(this.rpUseNamedParameters)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
      // 
      // rpUseNamedParameters
      // 
      this.rpUseNamedParameters.ActionHintText = "";
      this.rpUseNamedParameters.AutoActivate = true;
      this.rpUseNamedParameters.AutoUndo = true;
      this.rpUseNamedParameters.CodeIssueMessage = null;
      this.rpUseNamedParameters.Description = "Converts a method call with positional arguments into a named-argument equivalent" +
    ".";
      this.rpUseNamedParameters.DisplayName = "Use Named Parameters";
      this.rpUseNamedParameters.Image = ((System.Drawing.Bitmap)(resources.GetObject("rpUseNamedParameters.Image")));
      this.rpUseNamedParameters.NeedsSelection = false;
      this.rpUseNamedParameters.ProviderName = "UseNamedParameters";
      this.rpUseNamedParameters.Register = true;
      this.rpUseNamedParameters.SupportsAsyncMode = false;
      this.rpUseNamedParameters.CheckAvailability += new DevExpress.Refactor.Core.CheckAvailabilityEventHandler(this.rpUseNamedParameters_CheckAvailability);
      this.rpUseNamedParameters.VisualStudioSupported += new DevExpress.CodeRush.Core.VisualStudioSupportedEventHandler(this.rpUseNamedParameters_VisualStudioSupported);
      this.rpUseNamedParameters.Apply += new DevExpress.Refactor.Core.ApplyRefactoringEventHandler(this.rpUseNamedParameters_Apply);
      this.rpUseNamedParameters.PreparePreview += new DevExpress.Refactor.Core.PrepareRefactoringPreviewEventHandler(this.rpUseNamedParameters_PreparePreview);
      ((System.ComponentModel.ISupportInitialize)(this.rpUseNamedParameters)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

		}

		#endregion

		private DevExpress.Refactor.Core.RefactoringProvider rpUseNamedParameters;
	}
}