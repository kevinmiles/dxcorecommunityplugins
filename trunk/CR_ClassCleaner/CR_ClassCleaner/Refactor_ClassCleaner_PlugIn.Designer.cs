/*
 *
 * Refactor_ClassCleaner_Menu
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


namespace Refactor_ClassCleaner_Menu
{
    partial class Refactor_ClassCleaner_PlugIn
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        public Refactor_ClassCleaner_PlugIn()
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
            this.refactoringProviderWRegions = new DevExpress.Refactor.Core.RefactoringProvider(this.components);
            this.refactoringProviderWORegions = new DevExpress.Refactor.Core.RefactoringProvider(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.refactoringProviderWRegions)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.refactoringProviderWORegions)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // refactoringProviderWRegions
            // 
            this.refactoringProviderWRegions.ActionHintText = "Reformats the current class with regions";
            this.refactoringProviderWRegions.AutoActivate = true;
            this.refactoringProviderWRegions.AutoUndo = false;
            this.refactoringProviderWRegions.Description = "Reformats the current class with regions";
            this.refactoringProviderWRegions.DisplayName = "Clean Class With Regions";
            this.refactoringProviderWRegions.Image = null;
            this.refactoringProviderWRegions.NeedsSelection = false;
            this.refactoringProviderWRegions.ProviderName = "CleanClassWithRegions";
            this.refactoringProviderWRegions.Register = true;
            this.refactoringProviderWRegions.SupportsAsyncMode = false;
            this.refactoringProviderWRegions.Apply += new DevExpress.Refactor.Core.ApplyRefactoringEventHandler(this.refactoringProviderWRegions_Apply);
            this.refactoringProviderWRegions.CheckAvailability += new DevExpress.Refactor.Core.CheckAvailabilityEventHandler(this.refactoringProviderWRegions_CheckAvailability);
            // 
            // refactoringProviderWORegions
            // 
            this.refactoringProviderWORegions.ActionHintText = "Reformats the current class without regions";
            this.refactoringProviderWORegions.AutoActivate = true;
            this.refactoringProviderWORegions.AutoUndo = false;
            this.refactoringProviderWORegions.Description = "Reformats the current class without regions";
            this.refactoringProviderWORegions.DisplayName = "Clean Class Without Regions";
            this.refactoringProviderWORegions.Image = null;
            this.refactoringProviderWORegions.NeedsSelection = false;
            this.refactoringProviderWORegions.ProviderName = "CleanClassWithoutRegions";
            this.refactoringProviderWORegions.Register = true;
            this.refactoringProviderWORegions.SupportsAsyncMode = false;
            this.refactoringProviderWORegions.Apply += new DevExpress.Refactor.Core.ApplyRefactoringEventHandler(this.refactoringProviderWORegions_Apply);
            this.refactoringProviderWORegions.CheckAvailability += new DevExpress.Refactor.Core.CheckAvailabilityEventHandler(this.refactoringProviderWORegions_CheckAvailability);
            ((System.ComponentModel.ISupportInitialize)(this.refactoringProviderWRegions)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.refactoringProviderWORegions)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }

        #endregion

        internal DevExpress.Refactor.Core.RefactoringProvider refactoringProviderWRegions;
        internal DevExpress.Refactor.Core.RefactoringProvider refactoringProviderWORegions;
    }
}