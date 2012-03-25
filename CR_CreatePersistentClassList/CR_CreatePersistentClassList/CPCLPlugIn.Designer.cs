namespace CR_CreatePersistentClassList
{
	partial class CPCLPlugIn
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		public CPCLPlugIn()
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CPCLPlugIn));
			this.createPersistentClassCodeProvider = new DevExpress.CodeRush.Core.CodeProvider(this.components);
			((System.ComponentModel.ISupportInitialize)(this.createPersistentClassCodeProvider)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
			// 
			// createPersistentClassCodeProvider
			// 
			this.createPersistentClassCodeProvider.ActionHintText = "";
			this.createPersistentClassCodeProvider.AutoActivate = true;
			this.createPersistentClassCodeProvider.AutoUndo = false;
			this.createPersistentClassCodeProvider.CodeIssueMessage = null;
			this.createPersistentClassCodeProvider.Description = "Will create type array for all persistent classes found inside active source file" +
    "";
			this.createPersistentClassCodeProvider.Image = ((System.Drawing.Bitmap)(resources.GetObject("createPersistentClassCodeProvider.Image")));
			this.createPersistentClassCodeProvider.NeedsSelection = false;
			this.createPersistentClassCodeProvider.ProviderName = "Create persistent class list";
			this.createPersistentClassCodeProvider.Register = true;
			this.createPersistentClassCodeProvider.RequiresSubMenuChoice = false;
			this.createPersistentClassCodeProvider.SupportsAsyncMode = false;
			this.createPersistentClassCodeProvider.CheckAvailability += new DevExpress.Refactor.Core.CheckAvailabilityEventHandler(this.createPersistentClassCodeProvider_CheckAvailability);
			this.createPersistentClassCodeProvider.LanguageSupported += new DevExpress.CodeRush.Core.LanguageSupportedEventHandler(this.createPersistentClassCodeProvider_LanguageSupported);
			this.createPersistentClassCodeProvider.Apply += new DevExpress.Refactor.Core.ApplyRefactoringEventHandler(this.createPersistentClassCodeProvider_Apply);
			((System.ComponentModel.ISupportInitialize)(this.createPersistentClassCodeProvider)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this)).EndInit();

		}

		#endregion

		private DevExpress.CodeRush.Core.CodeProvider createPersistentClassCodeProvider;
	}
}