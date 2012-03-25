namespace CR_CreatePartialClasses
{
	partial class CPCPlugIn
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		public CPCPlugIn()
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CPCPlugIn));
			this.createPartialClassesCodeProvider = new DevExpress.CodeRush.Core.CodeProvider(this.components);
			((System.ComponentModel.ISupportInitialize)(this.createPartialClassesCodeProvider)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
			// 
			// createPartialClassesCodeProvider
			// 
			this.createPartialClassesCodeProvider.ActionHintText = "";
			this.createPartialClassesCodeProvider.AutoActivate = false;
			this.createPartialClassesCodeProvider.AutoUndo = false;
			this.createPartialClassesCodeProvider.CodeIssueMessage = null;
			this.createPartialClassesCodeProvider.Description = "Will create partial classes using classes found inside active source file";
			this.createPartialClassesCodeProvider.Image = ((System.Drawing.Bitmap)(resources.GetObject("createPartialClassesCodeProvider.Image")));
			this.createPartialClassesCodeProvider.NeedsSelection = false;
			this.createPartialClassesCodeProvider.ProviderName = "Create partial classes";
			this.createPartialClassesCodeProvider.Register = true;
			this.createPartialClassesCodeProvider.RequiresSubMenuChoice = false;
			this.createPartialClassesCodeProvider.SupportsAsyncMode = false;
			this.createPartialClassesCodeProvider.CheckAvailability += new DevExpress.Refactor.Core.CheckAvailabilityEventHandler(this.createPartialClassesCodeProvider_CheckAvailability);
			this.createPartialClassesCodeProvider.LanguageSupported += new DevExpress.CodeRush.Core.LanguageSupportedEventHandler(this.createPartialClassesCodeProvider_LanguageSupported);
			this.createPartialClassesCodeProvider.Apply += new DevExpress.Refactor.Core.ApplyRefactoringEventHandler(this.createPartialClassesCodeProvider_Apply);
			((System.ComponentModel.ISupportInitialize)(this.createPartialClassesCodeProvider)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this)).EndInit();

		}

		#endregion

		private DevExpress.CodeRush.Core.CodeProvider createPartialClassesCodeProvider;
	}
}