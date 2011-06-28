namespace CR_AssignmentExpressionToEqualityTest
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
			this.ipAssignmentIntended = new DevExpress.CodeRush.Core.IssueProvider(this.components);
			this.cpAssignmentExpressionToEqualityCheck = new DevExpress.CodeRush.Core.CodeProvider(this.components);
			((System.ComponentModel.ISupportInitialize)(this.ipAssignmentIntended)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.cpAssignmentExpressionToEqualityCheck)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
			// 
			// ipAssignmentIntended
			// 
			this.ipAssignmentIntended.DefaultIssueType = DevExpress.CodeRush.Core.CodeIssueType.None;
			this.ipAssignmentIntended.Description = "Warns if an assignment expression is found where perhaps an equality was intended" +
    ".";
			this.ipAssignmentIntended.ProviderName = "Assignment Intended?";
			this.ipAssignmentIntended.Register = true;
			this.ipAssignmentIntended.CheckCodeIssues += new DevExpress.CodeRush.Core.CheckCodeIssuesEventHandler(this.ipAssignmentIntended_CheckCodeIssues);
			// 
			// cpAssignmentExpressionToEqualityCheck
			// 
			this.cpAssignmentExpressionToEqualityCheck.ActionHintText = "";
			this.cpAssignmentExpressionToEqualityCheck.AutoActivate = true;
			this.cpAssignmentExpressionToEqualityCheck.AutoUndo = false;
			this.cpAssignmentExpressionToEqualityCheck.CodeIssueMessage = "Assignment Intended?";
			this.cpAssignmentExpressionToEqualityCheck.Description = "Converts an assignment expression to an equality check.";
			this.cpAssignmentExpressionToEqualityCheck.Image = ((System.Drawing.Bitmap)(resources.GetObject("cpAssignmentExpressionToEqualityCheck.Image")));
			this.cpAssignmentExpressionToEqualityCheck.NeedsSelection = false;
			this.cpAssignmentExpressionToEqualityCheck.ProviderName = "Assignment Expression to Equality Check";
			this.cpAssignmentExpressionToEqualityCheck.Register = true;
			this.cpAssignmentExpressionToEqualityCheck.SupportsAsyncMode = false;
			this.cpAssignmentExpressionToEqualityCheck.CheckAvailability += new DevExpress.Refactor.Core.CheckAvailabilityEventHandler(this.cpAssignmentExpressionToEqualityCheck_CheckAvailability);
			this.cpAssignmentExpressionToEqualityCheck.Apply += new DevExpress.Refactor.Core.ApplyRefactoringEventHandler(this.cpAssignmentExpressionToEqualityCheck_Apply);
			this.cpAssignmentExpressionToEqualityCheck.PreparePreview += new DevExpress.Refactor.Core.PrepareRefactoringPreviewEventHandler(this.cpAssignmentExpressionToEqualityCheck_PreparePreview);
			((System.ComponentModel.ISupportInitialize)(this.ipAssignmentIntended)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.cpAssignmentExpressionToEqualityCheck)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this)).EndInit();

		}

		#endregion

		private DevExpress.CodeRush.Core.IssueProvider ipAssignmentIntended;
		private DevExpress.CodeRush.Core.CodeProvider cpAssignmentExpressionToEqualityCheck;
	}
}