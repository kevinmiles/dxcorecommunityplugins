namespace Refactor_MoveTypesToFiles
{
	partial class MTTFPlugIn
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		public MTTFPlugIn()
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MTTFPlugIn));
			this.moveTypesToFilesRefactoring = new DevExpress.Refactor.Core.RefactoringProvider(this.components);
			((System.ComponentModel.ISupportInitialize)(this.moveTypesToFilesRefactoring)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
			// 
			// moveTypesToFilesRefactoring
			// 
			this.moveTypesToFilesRefactoring.ActionHintText = "";
			this.moveTypesToFilesRefactoring.AutoActivate = false;
			this.moveTypesToFilesRefactoring.AutoUndo = false;
			this.moveTypesToFilesRefactoring.CodeIssueMessage = null;
			this.moveTypesToFilesRefactoring.Description = "Moves all classes from active source file to their own file";
			this.moveTypesToFilesRefactoring.ExclusiveAvailabilityBehavior = DevExpress.CodeRush.Core.ExclusiveAvailabilityBehavior.ShowMenu;
			this.moveTypesToFilesRefactoring.Image = ((System.Drawing.Bitmap)(resources.GetObject("moveTypesToFilesRefactoring.Image")));
			this.moveTypesToFilesRefactoring.NeedsSelection = false;
			this.moveTypesToFilesRefactoring.ProviderName = "Move types to files";
			this.moveTypesToFilesRefactoring.Register = true;
			this.moveTypesToFilesRefactoring.RequiresSubMenuChoice = false;
			this.moveTypesToFilesRefactoring.SupportsAsyncMode = false;
			this.moveTypesToFilesRefactoring.CheckAvailability += new DevExpress.Refactor.Core.CheckAvailabilityEventHandler(this.moveTypesToFilesRefactoring_CheckAvailability);
			this.moveTypesToFilesRefactoring.LanguageSupported += new DevExpress.CodeRush.Core.LanguageSupportedEventHandler(this.moveTypesToFilesRefactoring_LanguageSupported);
			this.moveTypesToFilesRefactoring.Apply += new DevExpress.Refactor.Core.ApplyRefactoringEventHandler(this.moveTypesToFilesRefactoring_Apply);
			((System.ComponentModel.ISupportInitialize)(this.moveTypesToFilesRefactoring)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this)).EndInit();

		}

		#endregion

		private DevExpress.Refactor.Core.RefactoringProvider moveTypesToFilesRefactoring;
	}
}