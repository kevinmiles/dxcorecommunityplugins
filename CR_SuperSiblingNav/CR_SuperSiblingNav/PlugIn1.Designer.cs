namespace CR_SuperSiblingNav
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
			this.navMemberNext = new DevExpress.CodeRush.Library.NavigationProvider(this.components);
			this.navMemberPrevious = new DevExpress.CodeRush.Library.NavigationProvider(this.components);
			((System.ComponentModel.ISupportInitialize)(this.navMemberNext)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.navMemberPrevious)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
			// 
			// navMemberNext
			// 
			this.navMemberNext.ActionHintText = "Member Next";
			this.navMemberNext.AutoActivate = true;
			this.navMemberNext.AutoUndo = false;
			this.navMemberNext.CodeIssueMessage = null;
			this.navMemberNext.Description = "Navigates to the next method or property, attempting to restore a similar caret p" +
    "osition or selection based on the current caret position or selection.";
			this.navMemberNext.Image = ((System.Drawing.Bitmap)(resources.GetObject("navMemberNext.Image")));
			this.navMemberNext.NeedsSelection = false;
			this.navMemberNext.ProviderName = "Member Next";
			this.navMemberNext.Register = true;
			this.navMemberNext.SupportsAsyncMode = false;
			this.navMemberNext.CheckAvailability += new DevExpress.Refactor.Core.CheckAvailabilityEventHandler(this.SiblingNavAvailable);
			this.navMemberNext.Apply += new DevExpress.Refactor.Core.ApplyRefactoringEventHandler(this.navMemberNext_Apply);
			// 
			// navMemberPrevious
			// 
			this.navMemberPrevious.ActionHintText = "Member Previous";
			this.navMemberPrevious.AutoActivate = true;
			this.navMemberPrevious.AutoUndo = false;
			this.navMemberPrevious.CodeIssueMessage = null;
			this.navMemberPrevious.Description = "Navigates to the previous method or property, attempting to restore a similar car" +
    "et position or selection based on the current caret position or selection.";
			this.navMemberPrevious.Image = ((System.Drawing.Bitmap)(resources.GetObject("navMemberPrevious.Image")));
			this.navMemberPrevious.NeedsSelection = false;
			this.navMemberPrevious.ProviderName = "Member Previous";
			this.navMemberPrevious.Register = true;
			this.navMemberPrevious.SupportsAsyncMode = false;
			this.navMemberPrevious.CheckAvailability += new DevExpress.Refactor.Core.CheckAvailabilityEventHandler(this.SiblingNavAvailable);
			this.navMemberPrevious.Apply += new DevExpress.Refactor.Core.ApplyRefactoringEventHandler(this.navMemberPrevious_Apply);
			// 
			// PlugIn1
			// 
			this.CaretMoved += new DevExpress.CodeRush.Core.CaretMovedEventHandler(this.PlugIn1_CaretMoved);
			this.SelectionChanged += new DevExpress.CodeRush.Core.SelectionChangedEventHandler(this.PlugIn1_SelectionChanged);
			((System.ComponentModel.ISupportInitialize)(this.navMemberNext)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.navMemberPrevious)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this)).EndInit();

		}

		#endregion

		private DevExpress.CodeRush.Library.NavigationProvider navMemberNext;
		private DevExpress.CodeRush.Library.NavigationProvider navMemberPrevious;
	}
}