namespace CR_INotifyPropChangeRename
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
			this.spINotifyPropertyChangedRename = new DevExpress.CodeRush.Core.SearcherProvider();
			((System.ComponentModel.ISupportInitialize)(this.spINotifyPropertyChangedRename)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
			// 
			// spINotifyPropertyChangedRename
			// 
			this.spINotifyPropertyChangedRename.ActiveFileOnly = false;
			this.spINotifyPropertyChangedRename.Description = "Supports rename from the string inside a property in a class that implements INot" +
    "ifyPropertyChanged.";
			this.spINotifyPropertyChangedRename.ProviderName = "INotifyPropertyChanged Rename";
			this.spINotifyPropertyChangedRename.Register = true;
			this.spINotifyPropertyChangedRename.UseForNavigation = false;
			this.spINotifyPropertyChangedRename.UseForRenaming = true;
			this.spINotifyPropertyChangedRename.CheckAvailability += new DevExpress.CodeRush.Core.CheckSearchAvailabilityEventHandler(this.spINotifyPropertyChangedRename_CheckAvailability);
			this.spINotifyPropertyChangedRename.SearchReferences += new DevExpress.CodeRush.Core.SearchReferencesEventHandler(this.spINotifyPropertyChangedRename_SearchReferences);
			this.spINotifyPropertyChangedRename.SearchPreviewReferences += new DevExpress.CodeRush.Core.SearchPreviewReferencesEventHandler(this.spINotifyPropertyChangedRename_SearchPreviewReferences);
			((System.ComponentModel.ISupportInitialize)(this.spINotifyPropertyChangedRename)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this)).EndInit();

		}

		#endregion

		private DevExpress.CodeRush.Core.SearcherProvider spINotifyPropertyChangedRename;
	}
}