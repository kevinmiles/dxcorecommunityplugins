using DevExpress.CodeRush.Core;
using System.Net;
namespace CR_ShouldBeDisposable
{
    partial class ShouldBeDisposablePlugin
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        private CodeIssueProvider cipClassContainingDisposablesShouldBeDisposable;
        public ShouldBeDisposablePlugin()
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
            this.cipClassContainingDisposablesShouldBeDisposable = new DevExpress.CodeRush.Core.CodeIssueProvider(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.cipClassContainingDisposablesShouldBeDisposable)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // cipClassContainingDisposablesShouldBeDisposable
            // 
            this.cipClassContainingDisposablesShouldBeDisposable.Description = "Checks the fields and properties of a non-disposable class to determine if any types are IDisposable";
            this.cipClassContainingDisposablesShouldBeDisposable.ProviderName = "A class containing an IDisposable should implement IDisposable";
            this.cipClassContainingDisposablesShouldBeDisposable.Register = true;
            this.cipClassContainingDisposablesShouldBeDisposable.CheckCodeIssues += new DevExpress.CodeRush.Core.CheckCodeIssuesEventHandler(this.cipClassContainingDisposablesShouldBeDisposable_CheckCodeIssues);
            ((System.ComponentModel.ISupportInitialize)(this.cipClassContainingDisposablesShouldBeDisposable)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }

        #endregion
    }
}