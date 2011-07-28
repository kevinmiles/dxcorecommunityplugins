namespace CR_SyncNamespacesToFolder
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
			this.rpSynchronizeToFolderNames = new DevExpress.Refactor.Core.RefactoringProvider(this.components);
			((System.ComponentModel.ISupportInitialize)(this.rpSynchronizeToFolderNames)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
			// 
			// rpSynchronizeToFolderNames
			// 
			this.rpSynchronizeToFolderNames.ActionHintText = "Synchronize to Folder Names";
			this.rpSynchronizeToFolderNames.AutoActivate = true;
			this.rpSynchronizeToFolderNames.AutoUndo = false;
			this.rpSynchronizeToFolderNames.CodeIssueMessage = null;
			this.rpSynchronizeToFolderNames.Description = "Synchronizes all namespaces declared in the active project to the folder name and" +
    " the default project namespace.";
			this.rpSynchronizeToFolderNames.Image = ((System.Drawing.Bitmap)(resources.GetObject("rpSynchronizeToFolderNames.Image")));
			this.rpSynchronizeToFolderNames.NeedsSelection = false;
			this.rpSynchronizeToFolderNames.ProviderName = "Synchronize to Folder Names";
			this.rpSynchronizeToFolderNames.Register = true;
			this.rpSynchronizeToFolderNames.SupportsAsyncMode = false;
			this.rpSynchronizeToFolderNames.CheckAvailability += new DevExpress.Refactor.Core.CheckAvailabilityEventHandler(this.rpSynchronizeToFolderNames_CheckAvailability);
			this.rpSynchronizeToFolderNames.Apply += new DevExpress.Refactor.Core.ApplyRefactoringEventHandler(this.rpSynchronizeToFolderNames_Apply);
			((System.ComponentModel.ISupportInitialize)(this.rpSynchronizeToFolderNames)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this)).EndInit();

		}

		#endregion

		private DevExpress.Refactor.Core.RefactoringProvider rpSynchronizeToFolderNames;
	}
}