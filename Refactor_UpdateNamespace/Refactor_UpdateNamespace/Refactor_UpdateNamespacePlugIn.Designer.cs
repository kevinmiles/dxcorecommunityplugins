/*
 *
 * Refactor_UpdateNamespace
 * Copyright (C)2009 Stephen A. Bohlen
 * http://dxcorecommunityplugins.googlecode.com
 *
 * This library is free software; you can redistribute it and/or
 * modify it under the terms of the GNU Lesser General Public
 * License as published by the Free Software Foundation; either
 * version 2.1 of the License, or (at your option) any later version.
 *
 * This library is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU
 * Lesser General Public License for more details.
 *
 * You should have received a copy of the GNU Lesser General Public
 * License along with this library; if not, write to the Free Software
 * Foundation, Inc., 59 Temple Place, Suite 330, Boston, MA  02111-1307  USA
 *
 */

namespace Refactor_UpdateNamespace
{
    partial class Refactor_UpdateNamespacePlugIn
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        public Refactor_UpdateNamespacePlugIn()
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Refactor_UpdateNamespacePlugIn));
            this.refactoringUpdateNamespace = new DevExpress.Refactor.Core.RefactoringProvider(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.refactoringUpdateNamespace)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // refactoringUpdateNamespace
            // 
            this.refactoringUpdateNamespace.ActionHintText = "";
            this.refactoringUpdateNamespace.AutoActivate = true;
            this.refactoringUpdateNamespace.AutoUndo = false;
            this.refactoringUpdateNamespace.Description = resources.GetString("refactoringUpdateNamespace.Description");
            this.refactoringUpdateNamespace.Image = ((System.Drawing.Bitmap)(resources.GetObject("refactoringUpdateNamespace.Image")));
            this.refactoringUpdateNamespace.NeedsSelection = false;
            this.refactoringUpdateNamespace.ProviderName = "Update To Default Namespace";
            this.refactoringUpdateNamespace.Register = true;
            this.refactoringUpdateNamespace.SupportsAsyncMode = false;
            this.refactoringUpdateNamespace.LanguageSupported += new DevExpress.CodeRush.Core.LanguageSupportedEventHandler(this.refactoringUpdateNamespace_LanguageSupported);
            this.refactoringUpdateNamespace.Apply += new DevExpress.Refactor.Core.ApplyRefactoringEventHandler(this.refactoringUpdateNamespace_Apply);
            this.refactoringUpdateNamespace.CheckAvailability += new DevExpress.Refactor.Core.CheckAvailabilityEventHandler(this.refactoringUpdateNamespace_CheckAvailability);
            ((System.ComponentModel.ISupportInitialize)(this.refactoringUpdateNamespace)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }

        #endregion

        private DevExpress.Refactor.Core.RefactoringProvider refactoringUpdateNamespace;
    }
}