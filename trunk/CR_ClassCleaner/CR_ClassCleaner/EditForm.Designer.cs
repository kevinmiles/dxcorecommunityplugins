namespace CR_ClassCleaner
{
    partial class EditForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

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
            System.Windows.Forms.Label displayTextLabel;
            System.Windows.Forms.Label groupRegexLabel;
            System.Windows.Forms.Label commentLabel;
            this.topPanel = new System.Windows.Forms.Panel();
            this.commentTextBox = new System.Windows.Forms.TextBox();
            this.groupBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.displayTextTextBox = new System.Windows.Forms.TextBox();
            this.fillPanel = new System.Windows.Forms.Panel();
            this.elementSplitContainer = new System.Windows.Forms.SplitContainer();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.elementTypesDataGrid = new System.Windows.Forms.DataGridView();
            this.Selected = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.nameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.valueDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.languageElementTypebindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.visibilityDataGrid = new System.Windows.Forms.DataGridView();
            this.dataGridViewCheckBoxColumn1 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.visibilityBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.regexPanel = new System.Windows.Forms.Panel();
            this.groupRegexTextBox = new System.Windows.Forms.TextBox();
            this.regexGroupBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.bottomPanel = new System.Windows.Forms.Panel();
            this.doneButton = new System.Windows.Forms.Button();
            this.elementTypeBindingSource = new System.Windows.Forms.BindingSource(this.components);
            displayTextLabel = new System.Windows.Forms.Label();
            groupRegexLabel = new System.Windows.Forms.Label();
            commentLabel = new System.Windows.Forms.Label();
            this.topPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupBindingSource)).BeginInit();
            this.fillPanel.SuspendLayout();
            this.elementSplitContainer.Panel1.SuspendLayout();
            this.elementSplitContainer.Panel2.SuspendLayout();
            this.elementSplitContainer.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.elementTypesDataGrid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.languageElementTypebindingSource)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.visibilityDataGrid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.visibilityBindingSource)).BeginInit();
            this.regexPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.regexGroupBindingSource)).BeginInit();
            this.bottomPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.elementTypeBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // displayTextLabel
            // 
            displayTextLabel.AutoSize = true;
            displayTextLabel.Location = new System.Drawing.Point(6, 6);
            displayTextLabel.Name = "displayTextLabel";
            displayTextLabel.Size = new System.Drawing.Size(68, 13);
            displayTextLabel.TabIndex = 0;
            displayTextLabel.Text = "Display Text:";
            // 
            // groupRegexLabel
            // 
            groupRegexLabel.AutoSize = true;
            groupRegexLabel.Location = new System.Drawing.Point(3, 11);
            groupRegexLabel.Name = "groupRegexLabel";
            groupRegexLabel.Size = new System.Drawing.Size(73, 13);
            groupRegexLabel.TabIndex = 0;
            groupRegexLabel.Text = "Group Regex:";
            // 
            // commentLabel
            // 
            commentLabel.AutoSize = true;
            commentLabel.Location = new System.Drawing.Point(6, 45);
            commentLabel.Name = "commentLabel";
            commentLabel.Size = new System.Drawing.Size(54, 13);
            commentLabel.TabIndex = 2;
            commentLabel.Text = "Comment:";
            // 
            // topPanel
            // 
            this.topPanel.Controls.Add(commentLabel);
            this.topPanel.Controls.Add(this.commentTextBox);
            this.topPanel.Controls.Add(displayTextLabel);
            this.topPanel.Controls.Add(this.displayTextTextBox);
            this.topPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.topPanel.Location = new System.Drawing.Point(0, 0);
            this.topPanel.Name = "topPanel";
            this.topPanel.Size = new System.Drawing.Size(515, 108);
            this.topPanel.TabIndex = 0;
            // 
            // commentTextBox
            // 
            this.commentTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.groupBindingSource, "Comment", true));
            this.commentTextBox.Location = new System.Drawing.Point(9, 61);
            this.commentTextBox.Multiline = true;
            this.commentTextBox.Name = "commentTextBox";
            this.commentTextBox.Size = new System.Drawing.Size(494, 41);
            this.commentTextBox.TabIndex = 3;
            // 
            // groupBindingSource
            // 
            this.groupBindingSource.DataSource = typeof(CR_ClassCleaner.CodeGroup);
            // 
            // displayTextTextBox
            // 
            this.displayTextTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.groupBindingSource, "DisplayText", true));
            this.displayTextTextBox.Location = new System.Drawing.Point(9, 22);
            this.displayTextTextBox.Name = "displayTextTextBox";
            this.displayTextTextBox.Size = new System.Drawing.Size(494, 20);
            this.displayTextTextBox.TabIndex = 1;
            // 
            // fillPanel
            // 
            this.fillPanel.Controls.Add(this.elementSplitContainer);
            this.fillPanel.Controls.Add(this.regexPanel);
            this.fillPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fillPanel.Location = new System.Drawing.Point(0, 108);
            this.fillPanel.Name = "fillPanel";
            this.fillPanel.Size = new System.Drawing.Size(515, 235);
            this.fillPanel.TabIndex = 1;
            // 
            // elementSplitContainer
            // 
            this.elementSplitContainer.Location = new System.Drawing.Point(3, 95);
            this.elementSplitContainer.Name = "elementSplitContainer";
            // 
            // elementSplitContainer.Panel1
            // 
            this.elementSplitContainer.Panel1.Controls.Add(this.groupBox1);
            // 
            // elementSplitContainer.Panel2
            // 
            this.elementSplitContainer.Panel2.Controls.Add(this.groupBox2);
            this.elementSplitContainer.Size = new System.Drawing.Size(509, 137);
            this.elementSplitContainer.SplitterDistance = 295;
            this.elementSplitContainer.TabIndex = 2;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.elementTypesDataGrid);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(295, 137);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Element Types";
            // 
            // elementTypesDataGrid
            // 
            this.elementTypesDataGrid.AllowUserToAddRows = false;
            this.elementTypesDataGrid.AllowUserToDeleteRows = false;
            this.elementTypesDataGrid.AutoGenerateColumns = false;
            this.elementTypesDataGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.elementTypesDataGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Selected,
            this.nameDataGridViewTextBoxColumn,
            this.valueDataGridViewTextBoxColumn});
            this.elementTypesDataGrid.DataSource = this.languageElementTypebindingSource;
            this.elementTypesDataGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.elementTypesDataGrid.Location = new System.Drawing.Point(3, 16);
            this.elementTypesDataGrid.Name = "elementTypesDataGrid";
            this.elementTypesDataGrid.Size = new System.Drawing.Size(289, 118);
            this.elementTypesDataGrid.TabIndex = 0;
            // 
            // Selected
            // 
            this.Selected.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.Selected.DataPropertyName = "Selected";
            this.Selected.HeaderText = "Selected";
            this.Selected.Name = "Selected";
            this.Selected.Width = 55;
            // 
            // nameDataGridViewTextBoxColumn
            // 
            this.nameDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.nameDataGridViewTextBoxColumn.DataPropertyName = "Name";
            this.nameDataGridViewTextBoxColumn.HeaderText = "Name";
            this.nameDataGridViewTextBoxColumn.Name = "nameDataGridViewTextBoxColumn";
            this.nameDataGridViewTextBoxColumn.ReadOnly = true;
            this.nameDataGridViewTextBoxColumn.Width = 60;
            // 
            // valueDataGridViewTextBoxColumn
            // 
            this.valueDataGridViewTextBoxColumn.DataPropertyName = "Value";
            this.valueDataGridViewTextBoxColumn.HeaderText = "Value";
            this.valueDataGridViewTextBoxColumn.Name = "valueDataGridViewTextBoxColumn";
            this.valueDataGridViewTextBoxColumn.ReadOnly = true;
            this.valueDataGridViewTextBoxColumn.Visible = false;
            // 
            // languageElementTypebindingSource
            // 
            this.languageElementTypebindingSource.DataSource = typeof(CR_ClassCleaner.NameValueSelectected);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.visibilityDataGrid);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(0, 0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(210, 137);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Visibility";
            // 
            // visibilityDataGrid
            // 
            this.visibilityDataGrid.AllowUserToAddRows = false;
            this.visibilityDataGrid.AllowUserToDeleteRows = false;
            this.visibilityDataGrid.AutoGenerateColumns = false;
            this.visibilityDataGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.visibilityDataGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewCheckBoxColumn1,
            this.dataGridViewTextBoxColumn1,
            this.dataGridViewTextBoxColumn2});
            this.visibilityDataGrid.DataSource = this.visibilityBindingSource;
            this.visibilityDataGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.visibilityDataGrid.Location = new System.Drawing.Point(3, 16);
            this.visibilityDataGrid.Name = "visibilityDataGrid";
            this.visibilityDataGrid.Size = new System.Drawing.Size(204, 118);
            this.visibilityDataGrid.TabIndex = 1;
            // 
            // dataGridViewCheckBoxColumn1
            // 
            this.dataGridViewCheckBoxColumn1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.dataGridViewCheckBoxColumn1.DataPropertyName = "Selected";
            this.dataGridViewCheckBoxColumn1.HeaderText = "Selected";
            this.dataGridViewCheckBoxColumn1.Name = "dataGridViewCheckBoxColumn1";
            this.dataGridViewCheckBoxColumn1.Width = 55;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.dataGridViewTextBoxColumn1.DataPropertyName = "Name";
            this.dataGridViewTextBoxColumn1.HeaderText = "Name";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            this.dataGridViewTextBoxColumn1.Width = 60;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.DataPropertyName = "Value";
            this.dataGridViewTextBoxColumn2.HeaderText = "Value";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.ReadOnly = true;
            this.dataGridViewTextBoxColumn2.Visible = false;
            // 
            // visibilityBindingSource
            // 
            this.visibilityBindingSource.DataSource = typeof(CR_ClassCleaner.NameValueSelectected);
            // 
            // regexPanel
            // 
            this.regexPanel.Controls.Add(groupRegexLabel);
            this.regexPanel.Controls.Add(this.groupRegexTextBox);
            this.regexPanel.Location = new System.Drawing.Point(3, 6);
            this.regexPanel.Name = "regexPanel";
            this.regexPanel.Size = new System.Drawing.Size(509, 83);
            this.regexPanel.TabIndex = 0;
            // 
            // groupRegexTextBox
            // 
            this.groupRegexTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.regexGroupBindingSource, "GroupRegex", true));
            this.groupRegexTextBox.Location = new System.Drawing.Point(6, 27);
            this.groupRegexTextBox.Name = "groupRegexTextBox";
            this.groupRegexTextBox.Size = new System.Drawing.Size(494, 20);
            this.groupRegexTextBox.TabIndex = 1;
            // 
            // regexGroupBindingSource
            // 
            this.regexGroupBindingSource.DataSource = typeof(CR_ClassCleaner.RegexCodeGroup);
            // 
            // bottomPanel
            // 
            this.bottomPanel.Controls.Add(this.doneButton);
            this.bottomPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.bottomPanel.Location = new System.Drawing.Point(0, 343);
            this.bottomPanel.Name = "bottomPanel";
            this.bottomPanel.Size = new System.Drawing.Size(515, 35);
            this.bottomPanel.TabIndex = 2;
            // 
            // doneButton
            // 
            this.doneButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.doneButton.Location = new System.Drawing.Point(431, 6);
            this.doneButton.Name = "doneButton";
            this.doneButton.Size = new System.Drawing.Size(75, 23);
            this.doneButton.TabIndex = 0;
            this.doneButton.Text = "Done";
            this.doneButton.UseVisualStyleBackColor = true;
            this.doneButton.Click += new System.EventHandler(this.DoneClicked);
            // 
            // elementTypeBindingSource
            // 
            this.elementTypeBindingSource.DataMember = "ElementTypes";
            this.elementTypeBindingSource.DataSource = typeof(CR_ClassCleaner.ElementTypeCodeGroup);
            // 
            // EditForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(515, 378);
            this.Controls.Add(this.fillPanel);
            this.Controls.Add(this.bottomPanel);
            this.Controls.Add(this.topPanel);
            this.Name = "EditForm";
            this.Text = "EditForm";
            this.topPanel.ResumeLayout(false);
            this.topPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupBindingSource)).EndInit();
            this.fillPanel.ResumeLayout(false);
            this.elementSplitContainer.Panel1.ResumeLayout(false);
            this.elementSplitContainer.Panel2.ResumeLayout(false);
            this.elementSplitContainer.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.elementTypesDataGrid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.languageElementTypebindingSource)).EndInit();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.visibilityDataGrid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.visibilityBindingSource)).EndInit();
            this.regexPanel.ResumeLayout(false);
            this.regexPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.regexGroupBindingSource)).EndInit();
            this.bottomPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.elementTypeBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel topPanel;
        private System.Windows.Forms.BindingSource groupBindingSource;
        private System.Windows.Forms.Panel fillPanel;
        private System.Windows.Forms.BindingSource regexGroupBindingSource;
        private System.Windows.Forms.BindingSource elementTypeBindingSource;
        private System.Windows.Forms.TextBox displayTextTextBox;
        private System.Windows.Forms.Panel bottomPanel;
        private System.Windows.Forms.Button doneButton;
        private System.Windows.Forms.Panel regexPanel;
        private System.Windows.Forms.TextBox groupRegexTextBox;
        private System.Windows.Forms.TextBox commentTextBox;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.DataGridView elementTypesDataGrid;
        private System.Windows.Forms.BindingSource languageElementTypebindingSource;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Selected;
        private System.Windows.Forms.DataGridViewTextBoxColumn nameDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn valueDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridView visibilityDataGrid;
        private System.Windows.Forms.DataGridViewCheckBoxColumn dataGridViewCheckBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.BindingSource visibilityBindingSource;
        private System.Windows.Forms.SplitContainer elementSplitContainer;
    }
}