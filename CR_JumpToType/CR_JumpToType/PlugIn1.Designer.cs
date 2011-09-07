namespace CR_JumpToType
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
			this.navJumpToType = new DevExpress.CodeRush.Library.NavigationProvider(this.components);
			((System.ComponentModel.ISupportInitialize)(this.navJumpToType)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
			// 
			// navJumpToType
			// 
			this.navJumpToType.ActionHintText = "";
			this.navJumpToType.AutoActivate = true;
			this.navJumpToType.AutoUndo = false;
			this.navJumpToType.CodeIssueMessage = null;
			this.navJumpToType.Description = "Jumps to the declaration of the type of the reference at the caret.";
			this.navJumpToType.Image = ((System.Drawing.Bitmap)(resources.GetObject("navJumpToType.Image")));
			this.navJumpToType.NeedsSelection = false;
			this.navJumpToType.ProviderName = "Type Declaration";
			this.navJumpToType.Register = true;
			this.navJumpToType.SupportsAsyncMode = false;
			this.navJumpToType.CheckAvailability += new DevExpress.Refactor.Core.CheckAvailabilityEventHandler(this.navJumpToType_CheckAvailability);
			this.navJumpToType.Apply += new DevExpress.Refactor.Core.ApplyRefactoringEventHandler(this.navJumpToType_Apply);
			((System.ComponentModel.ISupportInitialize)(this.navJumpToType)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this)).EndInit();

		}

		#endregion

		private DevExpress.CodeRush.Library.NavigationProvider navJumpToType;
	}
}