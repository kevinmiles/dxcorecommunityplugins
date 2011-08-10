namespace CR_RemoveOuterBlock
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
			this.cpRemoveOuterBlock = new DevExpress.CodeRush.Core.CodeProvider(this.components);
			((System.ComponentModel.ISupportInitialize)(this.cpRemoveOuterBlock)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
			// 
			// cpRemoveOuterBlock
			// 
			this.cpRemoveOuterBlock.ActionHintText = "Remove Outer Block";
			this.cpRemoveOuterBlock.AutoActivate = true;
			this.cpRemoveOuterBlock.AutoUndo = false;
			this.cpRemoveOuterBlock.CodeIssueMessage = null;
			this.cpRemoveOuterBlock.Description = "Removes the outer code block while preserving the children (and promoting them up" +
    " one level).";
			this.cpRemoveOuterBlock.Image = ((System.Drawing.Bitmap)(resources.GetObject("cpRemoveOuterBlock.Image")));
			this.cpRemoveOuterBlock.NeedsSelection = false;
			this.cpRemoveOuterBlock.ProviderName = "Remove Outer Block";
			this.cpRemoveOuterBlock.Register = true;
			this.cpRemoveOuterBlock.SupportsAsyncMode = false;
			this.cpRemoveOuterBlock.CheckAvailability += new DevExpress.Refactor.Core.CheckAvailabilityEventHandler(this.cpRemoveOuterBlock_CheckAvailability);
			this.cpRemoveOuterBlock.Apply += new DevExpress.Refactor.Core.ApplyRefactoringEventHandler(this.cpRemoveOuterBlock_Apply);
			this.cpRemoveOuterBlock.PreparePreview += new DevExpress.Refactor.Core.PrepareRefactoringPreviewEventHandler(this.cpRemoveOuterBlock_PreparePreview);
			((System.ComponentModel.ISupportInitialize)(this.cpRemoveOuterBlock)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this)).EndInit();

		}

		#endregion

		private DevExpress.CodeRush.Core.CodeProvider cpRemoveOuterBlock;
	}
}