namespace CR_ObjectInitializerToConstructor
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
			this.cdeCreateConstructorCall = new DevExpress.CodeRush.Core.CodeProvider(this.components);
			this.refConvertToConstructorCall = new DevExpress.Refactor.Core.RefactoringProvider(this.components);
			this.targetPicker1 = new DevExpress.CodeRush.PlugInCore.TargetPicker(this.components);
			((System.ComponentModel.ISupportInitialize)(this.cdeCreateConstructorCall)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.refConvertToConstructorCall)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.targetPicker1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
			// 
			// cdeCreateConstructorCall
			// 
			this.cdeCreateConstructorCall.ActionHintText = "";
			this.cdeCreateConstructorCall.AutoActivate = true;
			this.cdeCreateConstructorCall.AutoUndo = false;
			this.cdeCreateConstructorCall.CodeIssueMessage = null;
			this.cdeCreateConstructorCall.Description = "Creates a constructor call matching the object initializer arguments and calls it." +
    "";
			this.cdeCreateConstructorCall.Image = ((System.Drawing.Bitmap)(resources.GetObject("cdeCreateConstructorCall.Image")));
			this.cdeCreateConstructorCall.NeedsSelection = false;
			this.cdeCreateConstructorCall.ProviderName = "Create (and convert initializer to) Constructor Call";
			this.cdeCreateConstructorCall.Register = true;
			this.cdeCreateConstructorCall.SupportsAsyncMode = false;
			this.cdeCreateConstructorCall.CheckAvailability += new DevExpress.Refactor.Core.CheckAvailabilityEventHandler(this.cdeCreateConstructorCall_CheckAvailability);
			this.cdeCreateConstructorCall.Apply += new DevExpress.Refactor.Core.ApplyRefactoringEventHandler(this.cdeCreateConstructorCall_Apply);
			this.cdeCreateConstructorCall.PreparePreview += new DevExpress.Refactor.Core.PrepareRefactoringPreviewEventHandler(this.cdeCreateConstructorCall_PreparePreview);
			// 
			// refConvertToConstructorCall
			// 
			this.refConvertToConstructorCall.ActionHintText = "Call Constructor";
			this.refConvertToConstructorCall.AutoActivate = true;
			this.refConvertToConstructorCall.AutoUndo = false;
			this.refConvertToConstructorCall.CodeIssueMessage = null;
			this.refConvertToConstructorCall.Description = "Converts object initializer construction code to a simple constructor call.";
			this.refConvertToConstructorCall.Image = ((System.Drawing.Bitmap)(resources.GetObject("refConvertToConstructorCall.Image")));
			this.refConvertToConstructorCall.NeedsSelection = false;
			this.refConvertToConstructorCall.ProviderName = "Convert Initializer to Constructor Call";
			this.refConvertToConstructorCall.Register = true;
			this.refConvertToConstructorCall.SupportsAsyncMode = false;
			this.refConvertToConstructorCall.CheckAvailability += new DevExpress.Refactor.Core.CheckAvailabilityEventHandler(this.refConvertToConstructorCall_CheckAvailability);
			this.refConvertToConstructorCall.Apply += new DevExpress.Refactor.Core.ApplyRefactoringEventHandler(this.refConvertToConstructorCall_Apply);
			this.refConvertToConstructorCall.PreparePreview += new DevExpress.Refactor.Core.PrepareRefactoringPreviewEventHandler(this.refConvertToConstructorCall_PreparePreview);
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
			
			this.TextViewActivated += new DevExpress.CodeRush.Core.TextViewHandler(this.PlugIn1_TextViewActivated);
			((System.ComponentModel.ISupportInitialize)(this.cdeCreateConstructorCall)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.refConvertToConstructorCall)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.targetPicker1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this)).EndInit();

		}

		#endregion

		private DevExpress.CodeRush.Core.CodeProvider cdeCreateConstructorCall;
		private DevExpress.Refactor.Core.RefactoringProvider refConvertToConstructorCall;
		private DevExpress.CodeRush.PlugInCore.TargetPicker targetPicker1;
	}
}