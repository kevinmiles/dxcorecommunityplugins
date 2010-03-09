using System;
using DevExpress.CodeRush.Core;

namespace CR_ClassCleaner
{
    partial class ClassCleanerOptions
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        public ClassCleanerOptions()
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
            this.classCleanerGroupBox = new System.Windows.Forms.GroupBox();
            this.sortOrderGroupBox = new System.Windows.Forms.GroupBox();
            this.codeGroupGrid = new System.Windows.Forms.DataGridView();
            this.displayTextDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CodeGroupType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Comment = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.editButtonColumn = new System.Windows.Forms.DataGridViewButtonColumn();
            this.groupBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.orderPanel = new System.Windows.Forms.Panel();
            this.downButton = new System.Windows.Forms.Button();
            this.upButton = new System.Windows.Forms.Button();
            this.buttonPanel = new System.Windows.Forms.Panel();
            this.resetButton = new System.Windows.Forms.Button();
            this.addButton = new System.Windows.Forms.Button();
            this.deleteButton = new System.Windows.Forms.Button();
            this.saveButton = new System.Windows.Forms.Button();
            this.importButton = new System.Windows.Forms.Button();
            this.exportButton = new System.Windows.Forms.Button();
            this.classCleanerGroupBox.SuspendLayout();
            this.sortOrderGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.codeGroupGrid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupBindingSource)).BeginInit();
            this.orderPanel.SuspendLayout();
            this.buttonPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // classCleanerGroupBox
            // 
            this.classCleanerGroupBox.Controls.Add(this.sortOrderGroupBox);
            this.classCleanerGroupBox.Controls.Add(this.buttonPanel);
            this.classCleanerGroupBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.classCleanerGroupBox.Location = new System.Drawing.Point(0, 0);
            this.classCleanerGroupBox.Name = "classCleanerGroupBox";
            this.classCleanerGroupBox.Size = new System.Drawing.Size(530, 480);
            this.classCleanerGroupBox.TabIndex = 0;
            this.classCleanerGroupBox.TabStop = false;
            this.classCleanerGroupBox.Text = "Class Cleaner Options";
            // 
            // sortOrderGroupBox
            // 
            this.sortOrderGroupBox.Controls.Add(this.codeGroupGrid);
            this.sortOrderGroupBox.Controls.Add(this.orderPanel);
            this.sortOrderGroupBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.sortOrderGroupBox.Location = new System.Drawing.Point(3, 16);
            this.sortOrderGroupBox.Name = "sortOrderGroupBox";
            this.sortOrderGroupBox.Size = new System.Drawing.Size(524, 422);
            this.sortOrderGroupBox.TabIndex = 1;
            this.sortOrderGroupBox.TabStop = false;
            this.sortOrderGroupBox.Text = "Sort Order";
            // 
            // codeGroupGrid
            // 
            this.codeGroupGrid.AllowUserToAddRows = false;
            this.codeGroupGrid.AutoGenerateColumns = false;
            this.codeGroupGrid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.codeGroupGrid.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.codeGroupGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.codeGroupGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.displayTextDataGridViewTextBoxColumn,
            this.CodeGroupType,
            this.Comment,
            this.editButtonColumn});
            this.codeGroupGrid.DataSource = this.groupBindingSource;
            this.codeGroupGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.codeGroupGrid.Location = new System.Drawing.Point(3, 16);
            this.codeGroupGrid.MultiSelect = false;
            this.codeGroupGrid.Name = "codeGroupGrid";
            this.codeGroupGrid.Size = new System.Drawing.Size(468, 403);
            this.codeGroupGrid.TabIndex = 2;
            this.codeGroupGrid.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.CellClicked);
            // 
            // displayTextDataGridViewTextBoxColumn
            // 
            this.displayTextDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.displayTextDataGridViewTextBoxColumn.DataPropertyName = "DisplayText";
            this.displayTextDataGridViewTextBoxColumn.HeaderText = "DisplayText";
            this.displayTextDataGridViewTextBoxColumn.Name = "displayTextDataGridViewTextBoxColumn";
            this.displayTextDataGridViewTextBoxColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // CodeGroupType
            // 
            this.CodeGroupType.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.CodeGroupType.DataPropertyName = "CodeGroupType";
            this.CodeGroupType.HeaderText = "Type";
            this.CodeGroupType.Name = "CodeGroupType";
            this.CodeGroupType.ReadOnly = true;
            // 
            // Comment
            // 
            this.Comment.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Comment.DataPropertyName = "Comment";
            this.Comment.HeaderText = "Comment";
            this.Comment.Name = "Comment";
            // 
            // editButtonColumn
            // 
            this.editButtonColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.editButtonColumn.DataPropertyName = "EditButtonText";
            this.editButtonColumn.HeaderText = "Edit";
            this.editButtonColumn.Name = "editButtonColumn";
            this.editButtonColumn.ReadOnly = true;
            this.editButtonColumn.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.editButtonColumn.Text = "Edit";
            this.editButtonColumn.ToolTipText = "Edit this code group";
            this.editButtonColumn.UseColumnTextForButtonValue = true;
            // 
            // groupBindingSource
            // 
            this.groupBindingSource.DataSource = typeof(CR_ClassCleaner.CodeGroup);
            // 
            // orderPanel
            // 
            this.orderPanel.Controls.Add(this.downButton);
            this.orderPanel.Controls.Add(this.upButton);
            this.orderPanel.Dock = System.Windows.Forms.DockStyle.Right;
            this.orderPanel.Location = new System.Drawing.Point(471, 16);
            this.orderPanel.Name = "orderPanel";
            this.orderPanel.Size = new System.Drawing.Size(50, 403);
            this.orderPanel.TabIndex = 1;
            // 
            // downButton
            // 
            this.downButton.Location = new System.Drawing.Point(3, 190);
            this.downButton.Name = "downButton";
            this.downButton.Size = new System.Drawing.Size(44, 23);
            this.downButton.TabIndex = 1;
            this.downButton.Text = "Down";
            this.downButton.UseVisualStyleBackColor = true;
            this.downButton.Click += new System.EventHandler(this.DownButtonClick);
            // 
            // upButton
            // 
            this.upButton.Location = new System.Drawing.Point(3, 161);
            this.upButton.Name = "upButton";
            this.upButton.Size = new System.Drawing.Size(44, 23);
            this.upButton.TabIndex = 0;
            this.upButton.Text = "Up";
            this.upButton.UseVisualStyleBackColor = true;
            this.upButton.Click += new System.EventHandler(this.UpButtonClick);
            // 
            // buttonPanel
            // 
            this.buttonPanel.Controls.Add(this.resetButton);
            this.buttonPanel.Controls.Add(this.addButton);
            this.buttonPanel.Controls.Add(this.deleteButton);
            this.buttonPanel.Controls.Add(this.saveButton);
            this.buttonPanel.Controls.Add(this.importButton);
            this.buttonPanel.Controls.Add(this.exportButton);
            this.buttonPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.buttonPanel.Location = new System.Drawing.Point(3, 438);
            this.buttonPanel.Name = "buttonPanel";
            this.buttonPanel.Size = new System.Drawing.Size(524, 39);
            this.buttonPanel.TabIndex = 2;
            // 
            // resetButton
            // 
            this.resetButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.resetButton.Location = new System.Drawing.Point(169, 8);
            this.resetButton.Name = "resetButton";
            this.resetButton.Size = new System.Drawing.Size(75, 23);
            this.resetButton.TabIndex = 5;
            this.resetButton.Text = "Reset";
            this.resetButton.UseVisualStyleBackColor = true;
            this.resetButton.Click += new System.EventHandler(this.ResetButtonClick);
            // 
            // addButton
            // 
            this.addButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.addButton.Location = new System.Drawing.Point(272, 8);
            this.addButton.Name = "addButton";
            this.addButton.Size = new System.Drawing.Size(75, 23);
            this.addButton.TabIndex = 4;
            this.addButton.Text = "Add";
            this.addButton.UseVisualStyleBackColor = true;
            this.addButton.Click += new System.EventHandler(this.AddButtonClick);
            // 
            // deleteButton
            // 
            this.deleteButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.deleteButton.Location = new System.Drawing.Point(353, 8);
            this.deleteButton.Name = "deleteButton";
            this.deleteButton.Size = new System.Drawing.Size(75, 23);
            this.deleteButton.TabIndex = 3;
            this.deleteButton.Text = "Delete";
            this.deleteButton.UseVisualStyleBackColor = true;
            this.deleteButton.Click += new System.EventHandler(this.DeleteButtonClick);
            // 
            // saveButton
            // 
            this.saveButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.saveButton.Location = new System.Drawing.Point(434, 8);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(75, 23);
            this.saveButton.TabIndex = 2;
            this.saveButton.Text = "Save";
            this.saveButton.UseVisualStyleBackColor = true;
            this.saveButton.Click += new System.EventHandler(this.SaveButtonClick);
            // 
            // importButton
            // 
            this.importButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.importButton.Location = new System.Drawing.Point(7, 8);
            this.importButton.Name = "importButton";
            this.importButton.Size = new System.Drawing.Size(75, 23);
            this.importButton.TabIndex = 1;
            this.importButton.Text = "Import";
            this.importButton.UseVisualStyleBackColor = true;
            this.importButton.Click += new System.EventHandler(this.ImportButtonClick);
            // 
            // exportButton
            // 
            this.exportButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.exportButton.Location = new System.Drawing.Point(88, 8);
            this.exportButton.Name = "exportButton";
            this.exportButton.Size = new System.Drawing.Size(75, 23);
            this.exportButton.TabIndex = 0;
            this.exportButton.Text = "Export";
            this.exportButton.UseVisualStyleBackColor = true;
            this.exportButton.Click += new System.EventHandler(this.ExportButtonClick);
            // 
            // ClassCleanerOptions
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.Controls.Add(this.classCleanerGroupBox);
            this.Name = "ClassCleanerOptions";
            this.classCleanerGroupBox.ResumeLayout(false);
            this.sortOrderGroupBox.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.codeGroupGrid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupBindingSource)).EndInit();
            this.orderPanel.ResumeLayout(false);
            this.buttonPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion


        private System.Windows.Forms.GroupBox classCleanerGroupBox;
        private System.Windows.Forms.GroupBox sortOrderGroupBox;
        private System.Windows.Forms.Panel orderPanel;
        private System.Windows.Forms.Panel buttonPanel;
        private System.Windows.Forms.DataGridView codeGroupGrid;
        private System.Windows.Forms.BindingSource groupBindingSource;
        private System.Windows.Forms.Button exportButton;
        private System.Windows.Forms.Button importButton;
        private System.Windows.Forms.Button saveButton;
        private System.Windows.Forms.Button downButton;
        private System.Windows.Forms.Button upButton;
        private System.Windows.Forms.Button resetButton;
        private System.Windows.Forms.Button addButton;
        private System.Windows.Forms.Button deleteButton;
        private System.Windows.Forms.DataGridViewTextBoxColumn displayTextDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn CodeGroupType;
        private System.Windows.Forms.DataGridViewTextBoxColumn Comment;
        private System.Windows.Forms.DataGridViewButtonColumn editButtonColumn;
    }
}