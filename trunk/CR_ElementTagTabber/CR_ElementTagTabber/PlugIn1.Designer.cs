namespace CR_ElementTagTabber
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
			this.ctxInPairedHtmlElementNameTag = new DevExpress.CodeRush.Extensions.ContextProvider(this.components);
			this.spHtmlTagNav = new DevExpress.CodeRush.Core.SearcherProvider();
			((System.ComponentModel.ISupportInitialize)(this.ctxInPairedHtmlElementNameTag)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.spHtmlTagNav)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
			// 
			// ctxInPairedHtmlElementNameTag
			// 
			this.ctxInPairedHtmlElementNameTag.Description = "Satisfied if the caret is in the beginning or ending name tag for an HTML element" +
    " with begin and end tags.";
			this.ctxInPairedHtmlElementNameTag.ProviderName = "Editor\\Code\\InPairedHtmlElementNameTag";
			this.ctxInPairedHtmlElementNameTag.Register = true;
			this.ctxInPairedHtmlElementNameTag.ContextSatisfied += new DevExpress.CodeRush.Core.ContextSatisfiedEventHandler(this.ctxInPairedHtmlElementNameTag_ContextSatisfied);
			// 
			// spHtmlTagNav
			// 
			this.spHtmlTagNav.ActiveFileOnly = false;
			this.spHtmlTagNav.Description = "Supports Tab to Next Reference on HTML Tags";
			this.spHtmlTagNav.ProviderName = "HTML Tag Nav";
			this.spHtmlTagNav.Register = true;
			this.spHtmlTagNav.UseForNavigation = true;
			this.spHtmlTagNav.UseForRenaming = false;
			this.spHtmlTagNav.CheckAvailability += new DevExpress.CodeRush.Core.CheckSearchAvailabilityEventHandler(this.spHtmlTagNav_CheckAvailability);
			this.spHtmlTagNav.SearchReferences += new DevExpress.CodeRush.Core.SearchReferencesEventHandler(this.spHtmlTagNav_SearchReferences);
			((System.ComponentModel.ISupportInitialize)(this.ctxInPairedHtmlElementNameTag)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.spHtmlTagNav)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this)).EndInit();

		}

		#endregion

		private DevExpress.CodeRush.Extensions.ContextProvider ctxInPairedHtmlElementNameTag;
		private DevExpress.CodeRush.Core.SearcherProvider spHtmlTagNav;
	}
}