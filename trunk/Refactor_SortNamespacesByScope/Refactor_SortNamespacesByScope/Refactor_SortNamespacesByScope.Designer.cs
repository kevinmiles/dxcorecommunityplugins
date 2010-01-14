namespace Refactor_SortNamespacesByScope
{
	partial class Refactor_SortNamespacesByScopePlugIn
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		public Refactor_SortNamespacesByScopePlugIn()
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Refactor_SortNamespacesByScopePlugIn));
			this.refactoringProvider = new DevExpress.Refactor.Core.RefactoringProvider(this.components);
			((System.ComponentModel.ISupportInitialize)(this.refactoringProvider)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
			// 
			// refactoringProvider
			// 
			this.refactoringProvider.ActionHintText = "";
			this.refactoringProvider.AutoActivate = true;
			this.refactoringProvider.AutoUndo = false;
			this.refactoringProvider.CodeIssueMessage = null;
			this.refactoringProvider.Description = "";
			this.refactoringProvider.DisplayName = "Sort Namespaces by Scope";
			this.refactoringProvider.Image = ((System.Drawing.Bitmap)(resources.GetObject("refactoringProvider.Image")));
			this.refactoringProvider.NeedsSelection = false;
			this.refactoringProvider.ProviderName = "NamespaceByScopeSorter";
			this.refactoringProvider.Register = true;
			this.refactoringProvider.SupportsAsyncMode = false;
			((System.ComponentModel.ISupportInitialize)(this.refactoringProvider)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this)).EndInit();

		}

		#endregion

		private DevExpress.Refactor.Core.RefactoringProvider refactoringProvider;
	}
}