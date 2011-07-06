namespace CR_Dispos_o_matic
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
			DevExpress.CodeRush.Core.InsertionPoint insertionPoint1 = new DevExpress.CodeRush.Core.InsertionPoint();
			this.cpImplementIDisposable = new DevExpress.CodeRush.Core.CodeProvider(this.components);
			this.targetPicker1 = new DevExpress.CodeRush.PlugInCore.TargetPicker(this.components);
			this.ipFieldsShouldBeDisposed = new DevExpress.CodeRush.Core.IssueProvider(this.components);
			this.ipClassShouldImplementIDisposable = new DevExpress.CodeRush.Core.IssueProvider(this.components);
			this.cpDisposeFields = new DevExpress.CodeRush.Core.CodeProvider(this.components);
			((System.ComponentModel.ISupportInitialize)(this.cpImplementIDisposable)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.targetPicker1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.ipFieldsShouldBeDisposed)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.ipClassShouldImplementIDisposable)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.cpDisposeFields)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
			// 
			// cpImplementIDisposable
			// 
			this.cpImplementIDisposable.ActionHintText = "Implement IDisposable";
			this.cpImplementIDisposable.AutoActivate = true;
			this.cpImplementIDisposable.AutoUndo = false;
			this.cpImplementIDisposable.CodeIssueMessage = null;
			this.cpImplementIDisposable.Description = "Implements the IDisposable interface in this class.";
			this.cpImplementIDisposable.Image = ((System.Drawing.Bitmap)(resources.GetObject("cpImplementIDisposable.Image")));
			this.cpImplementIDisposable.NeedsSelection = false;
			this.cpImplementIDisposable.ProviderName = "Implement IDisposable";
			this.cpImplementIDisposable.Register = true;
			this.cpImplementIDisposable.SupportsAsyncMode = false;
			this.cpImplementIDisposable.CheckAvailability += new DevExpress.Refactor.Core.CheckAvailabilityEventHandler(this.cpImplementIDisposable_CheckAvailability);
			this.cpImplementIDisposable.Apply += new DevExpress.Refactor.Core.ApplyRefactoringEventHandler(this.cpImplementIDisposable_Apply);
			// 
			// targetPicker1
			// 
			this.targetPicker1.BigHint = null;
			this.targetPicker1.Code = null;
			insertionPoint1.ArrowFillColor = System.Drawing.Color.Red;
			insertionPoint1.ArrowFillOpacity = 30;
			insertionPoint1.ArrowLineColor = System.Drawing.Color.Red;
			insertionPoint1.LineColor = System.Drawing.Color.Red;
			insertionPoint1.LineOpacity = 200;
			this.targetPicker1.InsertionPoint = insertionPoint1;
			this.targetPicker1.IsModalMode = false;
			this.targetPicker1.ShortcutsHint = null;
			this.targetPicker1.TargetSelected += new DevExpress.CodeRush.PlugInCore.TargetSelectedEventHandler(this.targetPicker1_TargetSelected);
			// 
			// ipFieldsShouldBeDisposed
			// 
			this.ipFieldsShouldBeDisposed.DefaultIssueType = DevExpress.CodeRush.Core.CodeIssueType.None;
			this.ipFieldsShouldBeDisposed.Description = "This issue appears when fields implementing IDisposable are inside a class that a" +
    "lso implements IDisposable, but those fields are NOT disposed.";
			this.ipFieldsShouldBeDisposed.ProviderName = "Fields should be disposed";
			this.ipFieldsShouldBeDisposed.Register = true;
			this.ipFieldsShouldBeDisposed.CheckCodeIssues += new DevExpress.CodeRush.Core.CheckCodeIssuesEventHandler(this.ipFieldsShouldBeDisposed_CheckCodeIssues);
			// 
			// ipClassShouldImplementIDisposable
			// 
			this.ipClassShouldImplementIDisposable.DefaultIssueType = DevExpress.CodeRush.Core.CodeIssueType.None;
			this.ipClassShouldImplementIDisposable.Description = "This issue appears when a class contains fields that implement IDisposable, but t" +
    "he class itself does NOT implement IDisposable.";
			this.ipClassShouldImplementIDisposable.ProviderName = "Class should implement IDisposable";
			this.ipClassShouldImplementIDisposable.Register = true;
			this.ipClassShouldImplementIDisposable.CheckCodeIssues += new DevExpress.CodeRush.Core.CheckCodeIssuesEventHandler(this.ipClassShouldImplementIDisposable_CheckCodeIssues);
			// 
			// cpDisposeFields
			// 
			this.cpDisposeFields.ActionHintText = "Dispose Fields";
			this.cpDisposeFields.AutoActivate = true;
			this.cpDisposeFields.AutoUndo = false;
			this.cpDisposeFields.CodeIssueMessage = null;
			this.cpDisposeFields.Description = "Disposes of any fields that implement IDisposable inside this class.";
			this.cpDisposeFields.Image = ((System.Drawing.Bitmap)(resources.GetObject("cpDisposeFields.Image")));
			this.cpDisposeFields.NeedsSelection = false;
			this.cpDisposeFields.ProviderName = "Dispose Fields";
			this.cpDisposeFields.Register = true;
			this.cpDisposeFields.SupportsAsyncMode = false;
			this.cpDisposeFields.CheckAvailability += new DevExpress.Refactor.Core.CheckAvailabilityEventHandler(this.cpDisposeFields_CheckAvailability);
			this.cpDisposeFields.Apply += new DevExpress.Refactor.Core.ApplyRefactoringEventHandler(this.cpDisposeFields_Apply);
			this.cpDisposeFields.PreparePreview += new DevExpress.Refactor.Core.PrepareRefactoringPreviewEventHandler(this.cpDisposeFields_PreparePreview);
			((System.ComponentModel.ISupportInitialize)(this.cpImplementIDisposable)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.targetPicker1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.ipFieldsShouldBeDisposed)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.ipClassShouldImplementIDisposable)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.cpDisposeFields)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this)).EndInit();

		}

		#endregion

		private DevExpress.CodeRush.Core.CodeProvider cpImplementIDisposable;
		private DevExpress.CodeRush.PlugInCore.TargetPicker targetPicker1;
		private DevExpress.CodeRush.Core.IssueProvider ipFieldsShouldBeDisposed;
		private DevExpress.CodeRush.Core.IssueProvider ipClassShouldImplementIDisposable;
		private DevExpress.CodeRush.Core.CodeProvider cpDisposeFields;
	}
}