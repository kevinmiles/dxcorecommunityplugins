using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace CR_SortLines
{
	/// <summary>
	/// Displays the options for line sorting.
	/// </summary>
	public class SortDialog : System.Windows.Forms.Form
	{
		
		#region SortDialog Variables
  
		#region Constants

		/// <summary>
		/// Max number of combo box items.
		/// </summary>
		private const int MAX_COMBO_ITEMS = 10;

		#endregion

		#region Instance
 
		private System.Windows.Forms.GroupBox grpSortOrder;
		private System.Windows.Forms.RadioButton rbSortAsc;
		private System.Windows.Forms.RadioButton rbSortDesc;
		private System.Windows.Forms.Button btnSort;
		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.GroupBox grpCaseSensitivity;
		private System.Windows.Forms.RadioButton rbCaseSensitive;
		private System.Windows.Forms.RadioButton rbCaseInsensitive;
		private System.Windows.Forms.Label lblMatchExpression;
		private System.Windows.Forms.ComboBox cmbMatchExpression;
		private System.Windows.Forms.CheckBox chkUseSortExpression;
		private System.Windows.Forms.Label lblSortExpression;
		private System.Windows.Forms.ComboBox cmbSortExpression;
		private System.Windows.Forms.Panel pnlSortExpressions;
		private System.Windows.Forms.CheckBox chkDeleteDuplicateLines;

		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		#endregion
  
		#endregion

  
		#region SortDialog Properties

		/// <summary>
		/// Gets a value indicating if the sort should be ascending.
		/// </summary>
		/// <value>
		/// <see langword="true" /> for ascending sort; <see langword="false" /> for
		/// descending sort.
		/// </value>
		public bool SortAscending {
			get {
				return this.rbSortAsc.Checked;
			}
		}

		/// <summary>
		/// Gets a value indicating if the sort should be case sensitive.
		/// </summary>
		/// <value>
		/// <see langword="true" /> for case sensitive sort; <see langword="false" /> for
		/// case insensitive sort.
		/// </value>
		public bool SortCaseSensitive{
			get {
				return this.rbCaseSensitive.Checked;
			}
		}

		/// <summary>
		/// Gets a value indicating if the sort should use a sort expression.
		/// </summary>
		/// <value>
		/// <see langword="true" /> to use sort expressions; <see langword="false" /> to
		/// do a standard sort.
		/// </value>
		public bool UseSortExpression{
			get{
				return this.chkUseSortExpression.Checked;
			}
		}

		/// <summary>
		/// Gets the sort expression to use.
		/// </summary>
		/// <value>
		/// A <see cref="System.String"/> with the line replacement expression that will be
		/// used to generate the sortable line data.
		/// </value>
		public string SortExpression{
			get {
				return this.cmbSortExpression.Text;
			}
		}

		/// <summary>
		/// Gets the match expression to use.
		/// </summary>
		/// <value>
		/// A <see cref="System.String"/> with the line match expression that will be used
		/// to set up the sort expression for line sorting data.
		/// </value>
		public string MatchExpression{
			get {
				return this.cmbMatchExpression.Text;
			}
		}

		/// <summary>
		/// Gets a value indicating if the sort should delete duplicate lines.
		/// </summary>
		/// <value>
		/// <see langword="true" /> to use delete duplicate lines; <see langword="false" /> to
		/// leave all lines alone.
		/// </value>
		public bool DeleteDuplicates{
			get {
				return this.chkDeleteDuplicateLines.Checked;
			}
		}

		#endregion

  
		#region SortDialog Implementation
  
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="CR_SortLines.SortDialog" /> class.
		/// </summary>
		public SortDialog() {
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

		}
 
		#endregion
  
		#region Overrides
  
		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing ) {
			if( disposing ) {
				if(components != null) {
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#endregion
  
		#region Event Handlers
 
		/// <summary>
		/// Handles the "Use Sort Expression" checkbox changed event.  Shows/hides the
		/// sort expression form fields.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">An <see cref="System.EventArgs" /> that contains the event data.</param>
		private void chkUseSortExpression_CheckedChanged(object sender, System.EventArgs e) {
			UpdateFormForSortExpressionPanel();
		}

		/// <summary>
		/// Handles the form activation event.  Initializes the visibility of the sort
		/// expression form fields.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">An <see cref="System.EventArgs" /> that contains the event data.</param>
		private void SortDialog_Activated(object sender, System.EventArgs e) {
			UpdateFormForSortExpressionPanel();
		}
 
		/// <summary>
		/// Handles the form closing event.  Ensures the form contents are valid before returning
		/// the dialog result.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">An <see cref="System.ComponentModel.CancelEventArgs" /> that contains the event data.</param>
		private void SortDialog_Closing(object sender, System.ComponentModel.CancelEventArgs e) {
			if(this.DialogResult == DialogResult.OK){
				if(!this.IsFormValid()){
					e.Cancel = true;
				}
				else{
					this.UpdateMRUList(this.cmbMatchExpression);
					this.UpdateMRUList(this.cmbSortExpression);
				}
			}
		}

		#endregion
  
		#region Methods
  
		#region Instance

		/// <summary>
		/// Updates the most-recently-used list in the expression combo boxes.
		/// </summary>
		/// <param name="toUpdate">
		/// The <see cref="System.Windows.Forms.ComboBox"/> to update the MRU list on.
		/// </param>
		private void UpdateMRUList(ComboBox toUpdate){
			string itemToAdd = toUpdate.Text;
			int index = toUpdate.Items.IndexOf(itemToAdd);
			if(index < 0){
				// Add the item
				if(toUpdate.Items.Count >= MAX_COMBO_ITEMS){
					toUpdate.Items.RemoveAt(toUpdate.Items.Count - 1);
				}
			}
			else{
				// Move the item to the top
				toUpdate.Items.RemoveAt(index);
			}
			toUpdate.Items.Insert(0, itemToAdd);
			toUpdate.SelectedItem = itemToAdd;
		}

		/// <summary>
		/// Checks the form validity.
		/// </summary>
		/// <returns><see langword="true" /> if the input is valid; <see langword="false" /> otherwise.</returns>
		private bool IsFormValid(){
			if(this.chkUseSortExpression.Checked){
				if(this.cmbMatchExpression.Text.Trim() == ""){
					MessageBox.Show("If using a sort expression, the match expression must contain data.", "Enter Sort Expression", MessageBoxButtons.OK, MessageBoxIcon.Error);
					this.cmbMatchExpression.Focus();
					return false;
				}
				else if(this.cmbSortExpression.Text.Trim() == ""){
					MessageBox.Show("If using a sort expression, the sort expression must contain data.", "Enter Sort Expression", MessageBoxButtons.OK, MessageBoxIcon.Error);
					this.cmbSortExpression.Focus();
					return false;
				}
			}
			return true;
		}

		/// <summary>
		/// Updates the form to show/hide the sort expression panel.
		/// </summary>
		private void UpdateFormForSortExpressionPanel(){
			if(this.chkUseSortExpression.Checked){
				if(!this.pnlSortExpressions.Visible){
					this.pnlSortExpressions.Visible = true;
					this.Height += this.pnlSortExpressions.Height;
				}
			}
			else{
				if(this.pnlSortExpressions.Visible){
					this.pnlSortExpressions.Visible = false;
					this.Height -= this.pnlSortExpressions.Height;
				}
			}
		}
  
		#endregion

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(SortDialog));
			this.grpSortOrder = new System.Windows.Forms.GroupBox();
			this.rbSortDesc = new System.Windows.Forms.RadioButton();
			this.rbSortAsc = new System.Windows.Forms.RadioButton();
			this.btnSort = new System.Windows.Forms.Button();
			this.btnCancel = new System.Windows.Forms.Button();
			this.grpCaseSensitivity = new System.Windows.Forms.GroupBox();
			this.rbCaseInsensitive = new System.Windows.Forms.RadioButton();
			this.rbCaseSensitive = new System.Windows.Forms.RadioButton();
			this.lblMatchExpression = new System.Windows.Forms.Label();
			this.lblSortExpression = new System.Windows.Forms.Label();
			this.cmbMatchExpression = new System.Windows.Forms.ComboBox();
			this.cmbSortExpression = new System.Windows.Forms.ComboBox();
			this.pnlSortExpressions = new System.Windows.Forms.Panel();
			this.chkUseSortExpression = new System.Windows.Forms.CheckBox();
			this.chkDeleteDuplicateLines = new System.Windows.Forms.CheckBox();
			this.grpSortOrder.SuspendLayout();
			this.grpCaseSensitivity.SuspendLayout();
			this.pnlSortExpressions.SuspendLayout();
			this.SuspendLayout();
			// 
			// grpSortOrder
			// 
			this.grpSortOrder.AccessibleDescription = resources.GetString("grpSortOrder.AccessibleDescription");
			this.grpSortOrder.AccessibleName = resources.GetString("grpSortOrder.AccessibleName");
			this.grpSortOrder.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("grpSortOrder.Anchor")));
			this.grpSortOrder.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("grpSortOrder.BackgroundImage")));
			this.grpSortOrder.Controls.Add(this.rbSortDesc);
			this.grpSortOrder.Controls.Add(this.rbSortAsc);
			this.grpSortOrder.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("grpSortOrder.Dock")));
			this.grpSortOrder.Enabled = ((bool)(resources.GetObject("grpSortOrder.Enabled")));
			this.grpSortOrder.Font = ((System.Drawing.Font)(resources.GetObject("grpSortOrder.Font")));
			this.grpSortOrder.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("grpSortOrder.ImeMode")));
			this.grpSortOrder.Location = ((System.Drawing.Point)(resources.GetObject("grpSortOrder.Location")));
			this.grpSortOrder.Name = "grpSortOrder";
			this.grpSortOrder.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("grpSortOrder.RightToLeft")));
			this.grpSortOrder.Size = ((System.Drawing.Size)(resources.GetObject("grpSortOrder.Size")));
			this.grpSortOrder.TabIndex = ((int)(resources.GetObject("grpSortOrder.TabIndex")));
			this.grpSortOrder.TabStop = false;
			this.grpSortOrder.Text = resources.GetString("grpSortOrder.Text");
			this.grpSortOrder.Visible = ((bool)(resources.GetObject("grpSortOrder.Visible")));
			// 
			// rbSortDesc
			// 
			this.rbSortDesc.AccessibleDescription = resources.GetString("rbSortDesc.AccessibleDescription");
			this.rbSortDesc.AccessibleName = resources.GetString("rbSortDesc.AccessibleName");
			this.rbSortDesc.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("rbSortDesc.Anchor")));
			this.rbSortDesc.Appearance = ((System.Windows.Forms.Appearance)(resources.GetObject("rbSortDesc.Appearance")));
			this.rbSortDesc.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("rbSortDesc.BackgroundImage")));
			this.rbSortDesc.CheckAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("rbSortDesc.CheckAlign")));
			this.rbSortDesc.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("rbSortDesc.Dock")));
			this.rbSortDesc.Enabled = ((bool)(resources.GetObject("rbSortDesc.Enabled")));
			this.rbSortDesc.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("rbSortDesc.FlatStyle")));
			this.rbSortDesc.Font = ((System.Drawing.Font)(resources.GetObject("rbSortDesc.Font")));
			this.rbSortDesc.Image = ((System.Drawing.Image)(resources.GetObject("rbSortDesc.Image")));
			this.rbSortDesc.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("rbSortDesc.ImageAlign")));
			this.rbSortDesc.ImageIndex = ((int)(resources.GetObject("rbSortDesc.ImageIndex")));
			this.rbSortDesc.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("rbSortDesc.ImeMode")));
			this.rbSortDesc.Location = ((System.Drawing.Point)(resources.GetObject("rbSortDesc.Location")));
			this.rbSortDesc.Name = "rbSortDesc";
			this.rbSortDesc.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("rbSortDesc.RightToLeft")));
			this.rbSortDesc.Size = ((System.Drawing.Size)(resources.GetObject("rbSortDesc.Size")));
			this.rbSortDesc.TabIndex = ((int)(resources.GetObject("rbSortDesc.TabIndex")));
			this.rbSortDesc.Text = resources.GetString("rbSortDesc.Text");
			this.rbSortDesc.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("rbSortDesc.TextAlign")));
			this.rbSortDesc.Visible = ((bool)(resources.GetObject("rbSortDesc.Visible")));
			// 
			// rbSortAsc
			// 
			this.rbSortAsc.AccessibleDescription = resources.GetString("rbSortAsc.AccessibleDescription");
			this.rbSortAsc.AccessibleName = resources.GetString("rbSortAsc.AccessibleName");
			this.rbSortAsc.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("rbSortAsc.Anchor")));
			this.rbSortAsc.Appearance = ((System.Windows.Forms.Appearance)(resources.GetObject("rbSortAsc.Appearance")));
			this.rbSortAsc.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("rbSortAsc.BackgroundImage")));
			this.rbSortAsc.CheckAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("rbSortAsc.CheckAlign")));
			this.rbSortAsc.Checked = true;
			this.rbSortAsc.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("rbSortAsc.Dock")));
			this.rbSortAsc.Enabled = ((bool)(resources.GetObject("rbSortAsc.Enabled")));
			this.rbSortAsc.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("rbSortAsc.FlatStyle")));
			this.rbSortAsc.Font = ((System.Drawing.Font)(resources.GetObject("rbSortAsc.Font")));
			this.rbSortAsc.Image = ((System.Drawing.Image)(resources.GetObject("rbSortAsc.Image")));
			this.rbSortAsc.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("rbSortAsc.ImageAlign")));
			this.rbSortAsc.ImageIndex = ((int)(resources.GetObject("rbSortAsc.ImageIndex")));
			this.rbSortAsc.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("rbSortAsc.ImeMode")));
			this.rbSortAsc.Location = ((System.Drawing.Point)(resources.GetObject("rbSortAsc.Location")));
			this.rbSortAsc.Name = "rbSortAsc";
			this.rbSortAsc.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("rbSortAsc.RightToLeft")));
			this.rbSortAsc.Size = ((System.Drawing.Size)(resources.GetObject("rbSortAsc.Size")));
			this.rbSortAsc.TabIndex = ((int)(resources.GetObject("rbSortAsc.TabIndex")));
			this.rbSortAsc.TabStop = true;
			this.rbSortAsc.Text = resources.GetString("rbSortAsc.Text");
			this.rbSortAsc.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("rbSortAsc.TextAlign")));
			this.rbSortAsc.Visible = ((bool)(resources.GetObject("rbSortAsc.Visible")));
			// 
			// btnSort
			// 
			this.btnSort.AccessibleDescription = resources.GetString("btnSort.AccessibleDescription");
			this.btnSort.AccessibleName = resources.GetString("btnSort.AccessibleName");
			this.btnSort.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("btnSort.Anchor")));
			this.btnSort.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnSort.BackgroundImage")));
			this.btnSort.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.btnSort.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("btnSort.Dock")));
			this.btnSort.Enabled = ((bool)(resources.GetObject("btnSort.Enabled")));
			this.btnSort.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("btnSort.FlatStyle")));
			this.btnSort.Font = ((System.Drawing.Font)(resources.GetObject("btnSort.Font")));
			this.btnSort.Image = ((System.Drawing.Image)(resources.GetObject("btnSort.Image")));
			this.btnSort.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnSort.ImageAlign")));
			this.btnSort.ImageIndex = ((int)(resources.GetObject("btnSort.ImageIndex")));
			this.btnSort.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("btnSort.ImeMode")));
			this.btnSort.Location = ((System.Drawing.Point)(resources.GetObject("btnSort.Location")));
			this.btnSort.Name = "btnSort";
			this.btnSort.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("btnSort.RightToLeft")));
			this.btnSort.Size = ((System.Drawing.Size)(resources.GetObject("btnSort.Size")));
			this.btnSort.TabIndex = ((int)(resources.GetObject("btnSort.TabIndex")));
			this.btnSort.Text = resources.GetString("btnSort.Text");
			this.btnSort.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnSort.TextAlign")));
			this.btnSort.Visible = ((bool)(resources.GetObject("btnSort.Visible")));
			// 
			// btnCancel
			// 
			this.btnCancel.AccessibleDescription = resources.GetString("btnCancel.AccessibleDescription");
			this.btnCancel.AccessibleName = resources.GetString("btnCancel.AccessibleName");
			this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("btnCancel.Anchor")));
			this.btnCancel.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnCancel.BackgroundImage")));
			this.btnCancel.CausesValidation = false;
			this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnCancel.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("btnCancel.Dock")));
			this.btnCancel.Enabled = ((bool)(resources.GetObject("btnCancel.Enabled")));
			this.btnCancel.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("btnCancel.FlatStyle")));
			this.btnCancel.Font = ((System.Drawing.Font)(resources.GetObject("btnCancel.Font")));
			this.btnCancel.Image = ((System.Drawing.Image)(resources.GetObject("btnCancel.Image")));
			this.btnCancel.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnCancel.ImageAlign")));
			this.btnCancel.ImageIndex = ((int)(resources.GetObject("btnCancel.ImageIndex")));
			this.btnCancel.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("btnCancel.ImeMode")));
			this.btnCancel.Location = ((System.Drawing.Point)(resources.GetObject("btnCancel.Location")));
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("btnCancel.RightToLeft")));
			this.btnCancel.Size = ((System.Drawing.Size)(resources.GetObject("btnCancel.Size")));
			this.btnCancel.TabIndex = ((int)(resources.GetObject("btnCancel.TabIndex")));
			this.btnCancel.Text = resources.GetString("btnCancel.Text");
			this.btnCancel.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnCancel.TextAlign")));
			this.btnCancel.Visible = ((bool)(resources.GetObject("btnCancel.Visible")));
			// 
			// grpCaseSensitivity
			// 
			this.grpCaseSensitivity.AccessibleDescription = resources.GetString("grpCaseSensitivity.AccessibleDescription");
			this.grpCaseSensitivity.AccessibleName = resources.GetString("grpCaseSensitivity.AccessibleName");
			this.grpCaseSensitivity.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("grpCaseSensitivity.Anchor")));
			this.grpCaseSensitivity.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("grpCaseSensitivity.BackgroundImage")));
			this.grpCaseSensitivity.Controls.Add(this.rbCaseInsensitive);
			this.grpCaseSensitivity.Controls.Add(this.rbCaseSensitive);
			this.grpCaseSensitivity.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("grpCaseSensitivity.Dock")));
			this.grpCaseSensitivity.Enabled = ((bool)(resources.GetObject("grpCaseSensitivity.Enabled")));
			this.grpCaseSensitivity.Font = ((System.Drawing.Font)(resources.GetObject("grpCaseSensitivity.Font")));
			this.grpCaseSensitivity.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("grpCaseSensitivity.ImeMode")));
			this.grpCaseSensitivity.Location = ((System.Drawing.Point)(resources.GetObject("grpCaseSensitivity.Location")));
			this.grpCaseSensitivity.Name = "grpCaseSensitivity";
			this.grpCaseSensitivity.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("grpCaseSensitivity.RightToLeft")));
			this.grpCaseSensitivity.Size = ((System.Drawing.Size)(resources.GetObject("grpCaseSensitivity.Size")));
			this.grpCaseSensitivity.TabIndex = ((int)(resources.GetObject("grpCaseSensitivity.TabIndex")));
			this.grpCaseSensitivity.TabStop = false;
			this.grpCaseSensitivity.Text = resources.GetString("grpCaseSensitivity.Text");
			this.grpCaseSensitivity.Visible = ((bool)(resources.GetObject("grpCaseSensitivity.Visible")));
			// 
			// rbCaseInsensitive
			// 
			this.rbCaseInsensitive.AccessibleDescription = resources.GetString("rbCaseInsensitive.AccessibleDescription");
			this.rbCaseInsensitive.AccessibleName = resources.GetString("rbCaseInsensitive.AccessibleName");
			this.rbCaseInsensitive.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("rbCaseInsensitive.Anchor")));
			this.rbCaseInsensitive.Appearance = ((System.Windows.Forms.Appearance)(resources.GetObject("rbCaseInsensitive.Appearance")));
			this.rbCaseInsensitive.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("rbCaseInsensitive.BackgroundImage")));
			this.rbCaseInsensitive.CheckAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("rbCaseInsensitive.CheckAlign")));
			this.rbCaseInsensitive.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("rbCaseInsensitive.Dock")));
			this.rbCaseInsensitive.Enabled = ((bool)(resources.GetObject("rbCaseInsensitive.Enabled")));
			this.rbCaseInsensitive.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("rbCaseInsensitive.FlatStyle")));
			this.rbCaseInsensitive.Font = ((System.Drawing.Font)(resources.GetObject("rbCaseInsensitive.Font")));
			this.rbCaseInsensitive.Image = ((System.Drawing.Image)(resources.GetObject("rbCaseInsensitive.Image")));
			this.rbCaseInsensitive.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("rbCaseInsensitive.ImageAlign")));
			this.rbCaseInsensitive.ImageIndex = ((int)(resources.GetObject("rbCaseInsensitive.ImageIndex")));
			this.rbCaseInsensitive.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("rbCaseInsensitive.ImeMode")));
			this.rbCaseInsensitive.Location = ((System.Drawing.Point)(resources.GetObject("rbCaseInsensitive.Location")));
			this.rbCaseInsensitive.Name = "rbCaseInsensitive";
			this.rbCaseInsensitive.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("rbCaseInsensitive.RightToLeft")));
			this.rbCaseInsensitive.Size = ((System.Drawing.Size)(resources.GetObject("rbCaseInsensitive.Size")));
			this.rbCaseInsensitive.TabIndex = ((int)(resources.GetObject("rbCaseInsensitive.TabIndex")));
			this.rbCaseInsensitive.Text = resources.GetString("rbCaseInsensitive.Text");
			this.rbCaseInsensitive.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("rbCaseInsensitive.TextAlign")));
			this.rbCaseInsensitive.Visible = ((bool)(resources.GetObject("rbCaseInsensitive.Visible")));
			// 
			// rbCaseSensitive
			// 
			this.rbCaseSensitive.AccessibleDescription = resources.GetString("rbCaseSensitive.AccessibleDescription");
			this.rbCaseSensitive.AccessibleName = resources.GetString("rbCaseSensitive.AccessibleName");
			this.rbCaseSensitive.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("rbCaseSensitive.Anchor")));
			this.rbCaseSensitive.Appearance = ((System.Windows.Forms.Appearance)(resources.GetObject("rbCaseSensitive.Appearance")));
			this.rbCaseSensitive.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("rbCaseSensitive.BackgroundImage")));
			this.rbCaseSensitive.CheckAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("rbCaseSensitive.CheckAlign")));
			this.rbCaseSensitive.Checked = true;
			this.rbCaseSensitive.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("rbCaseSensitive.Dock")));
			this.rbCaseSensitive.Enabled = ((bool)(resources.GetObject("rbCaseSensitive.Enabled")));
			this.rbCaseSensitive.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("rbCaseSensitive.FlatStyle")));
			this.rbCaseSensitive.Font = ((System.Drawing.Font)(resources.GetObject("rbCaseSensitive.Font")));
			this.rbCaseSensitive.Image = ((System.Drawing.Image)(resources.GetObject("rbCaseSensitive.Image")));
			this.rbCaseSensitive.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("rbCaseSensitive.ImageAlign")));
			this.rbCaseSensitive.ImageIndex = ((int)(resources.GetObject("rbCaseSensitive.ImageIndex")));
			this.rbCaseSensitive.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("rbCaseSensitive.ImeMode")));
			this.rbCaseSensitive.Location = ((System.Drawing.Point)(resources.GetObject("rbCaseSensitive.Location")));
			this.rbCaseSensitive.Name = "rbCaseSensitive";
			this.rbCaseSensitive.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("rbCaseSensitive.RightToLeft")));
			this.rbCaseSensitive.Size = ((System.Drawing.Size)(resources.GetObject("rbCaseSensitive.Size")));
			this.rbCaseSensitive.TabIndex = ((int)(resources.GetObject("rbCaseSensitive.TabIndex")));
			this.rbCaseSensitive.TabStop = true;
			this.rbCaseSensitive.Text = resources.GetString("rbCaseSensitive.Text");
			this.rbCaseSensitive.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("rbCaseSensitive.TextAlign")));
			this.rbCaseSensitive.Visible = ((bool)(resources.GetObject("rbCaseSensitive.Visible")));
			// 
			// lblMatchExpression
			// 
			this.lblMatchExpression.AccessibleDescription = resources.GetString("lblMatchExpression.AccessibleDescription");
			this.lblMatchExpression.AccessibleName = resources.GetString("lblMatchExpression.AccessibleName");
			this.lblMatchExpression.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("lblMatchExpression.Anchor")));
			this.lblMatchExpression.AutoSize = ((bool)(resources.GetObject("lblMatchExpression.AutoSize")));
			this.lblMatchExpression.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("lblMatchExpression.Dock")));
			this.lblMatchExpression.Enabled = ((bool)(resources.GetObject("lblMatchExpression.Enabled")));
			this.lblMatchExpression.Font = ((System.Drawing.Font)(resources.GetObject("lblMatchExpression.Font")));
			this.lblMatchExpression.Image = ((System.Drawing.Image)(resources.GetObject("lblMatchExpression.Image")));
			this.lblMatchExpression.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblMatchExpression.ImageAlign")));
			this.lblMatchExpression.ImageIndex = ((int)(resources.GetObject("lblMatchExpression.ImageIndex")));
			this.lblMatchExpression.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("lblMatchExpression.ImeMode")));
			this.lblMatchExpression.Location = ((System.Drawing.Point)(resources.GetObject("lblMatchExpression.Location")));
			this.lblMatchExpression.Name = "lblMatchExpression";
			this.lblMatchExpression.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("lblMatchExpression.RightToLeft")));
			this.lblMatchExpression.Size = ((System.Drawing.Size)(resources.GetObject("lblMatchExpression.Size")));
			this.lblMatchExpression.TabIndex = ((int)(resources.GetObject("lblMatchExpression.TabIndex")));
			this.lblMatchExpression.Text = resources.GetString("lblMatchExpression.Text");
			this.lblMatchExpression.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblMatchExpression.TextAlign")));
			this.lblMatchExpression.Visible = ((bool)(resources.GetObject("lblMatchExpression.Visible")));
			// 
			// lblSortExpression
			// 
			this.lblSortExpression.AccessibleDescription = resources.GetString("lblSortExpression.AccessibleDescription");
			this.lblSortExpression.AccessibleName = resources.GetString("lblSortExpression.AccessibleName");
			this.lblSortExpression.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("lblSortExpression.Anchor")));
			this.lblSortExpression.AutoSize = ((bool)(resources.GetObject("lblSortExpression.AutoSize")));
			this.lblSortExpression.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("lblSortExpression.Dock")));
			this.lblSortExpression.Enabled = ((bool)(resources.GetObject("lblSortExpression.Enabled")));
			this.lblSortExpression.Font = ((System.Drawing.Font)(resources.GetObject("lblSortExpression.Font")));
			this.lblSortExpression.Image = ((System.Drawing.Image)(resources.GetObject("lblSortExpression.Image")));
			this.lblSortExpression.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblSortExpression.ImageAlign")));
			this.lblSortExpression.ImageIndex = ((int)(resources.GetObject("lblSortExpression.ImageIndex")));
			this.lblSortExpression.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("lblSortExpression.ImeMode")));
			this.lblSortExpression.Location = ((System.Drawing.Point)(resources.GetObject("lblSortExpression.Location")));
			this.lblSortExpression.Name = "lblSortExpression";
			this.lblSortExpression.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("lblSortExpression.RightToLeft")));
			this.lblSortExpression.Size = ((System.Drawing.Size)(resources.GetObject("lblSortExpression.Size")));
			this.lblSortExpression.TabIndex = ((int)(resources.GetObject("lblSortExpression.TabIndex")));
			this.lblSortExpression.Text = resources.GetString("lblSortExpression.Text");
			this.lblSortExpression.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lblSortExpression.TextAlign")));
			this.lblSortExpression.Visible = ((bool)(resources.GetObject("lblSortExpression.Visible")));
			// 
			// cmbMatchExpression
			// 
			this.cmbMatchExpression.AccessibleDescription = resources.GetString("cmbMatchExpression.AccessibleDescription");
			this.cmbMatchExpression.AccessibleName = resources.GetString("cmbMatchExpression.AccessibleName");
			this.cmbMatchExpression.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("cmbMatchExpression.Anchor")));
			this.cmbMatchExpression.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("cmbMatchExpression.BackgroundImage")));
			this.cmbMatchExpression.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("cmbMatchExpression.Dock")));
			this.cmbMatchExpression.Enabled = ((bool)(resources.GetObject("cmbMatchExpression.Enabled")));
			this.cmbMatchExpression.Font = ((System.Drawing.Font)(resources.GetObject("cmbMatchExpression.Font")));
			this.cmbMatchExpression.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("cmbMatchExpression.ImeMode")));
			this.cmbMatchExpression.IntegralHeight = ((bool)(resources.GetObject("cmbMatchExpression.IntegralHeight")));
			this.cmbMatchExpression.ItemHeight = ((int)(resources.GetObject("cmbMatchExpression.ItemHeight")));
			this.cmbMatchExpression.Location = ((System.Drawing.Point)(resources.GetObject("cmbMatchExpression.Location")));
			this.cmbMatchExpression.MaxDropDownItems = ((int)(resources.GetObject("cmbMatchExpression.MaxDropDownItems")));
			this.cmbMatchExpression.MaxLength = ((int)(resources.GetObject("cmbMatchExpression.MaxLength")));
			this.cmbMatchExpression.Name = "cmbMatchExpression";
			this.cmbMatchExpression.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("cmbMatchExpression.RightToLeft")));
			this.cmbMatchExpression.Size = ((System.Drawing.Size)(resources.GetObject("cmbMatchExpression.Size")));
			this.cmbMatchExpression.TabIndex = ((int)(resources.GetObject("cmbMatchExpression.TabIndex")));
			this.cmbMatchExpression.Text = resources.GetString("cmbMatchExpression.Text");
			this.cmbMatchExpression.Visible = ((bool)(resources.GetObject("cmbMatchExpression.Visible")));
			// 
			// cmbSortExpression
			// 
			this.cmbSortExpression.AccessibleDescription = resources.GetString("cmbSortExpression.AccessibleDescription");
			this.cmbSortExpression.AccessibleName = resources.GetString("cmbSortExpression.AccessibleName");
			this.cmbSortExpression.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("cmbSortExpression.Anchor")));
			this.cmbSortExpression.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("cmbSortExpression.BackgroundImage")));
			this.cmbSortExpression.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("cmbSortExpression.Dock")));
			this.cmbSortExpression.Enabled = ((bool)(resources.GetObject("cmbSortExpression.Enabled")));
			this.cmbSortExpression.Font = ((System.Drawing.Font)(resources.GetObject("cmbSortExpression.Font")));
			this.cmbSortExpression.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("cmbSortExpression.ImeMode")));
			this.cmbSortExpression.IntegralHeight = ((bool)(resources.GetObject("cmbSortExpression.IntegralHeight")));
			this.cmbSortExpression.ItemHeight = ((int)(resources.GetObject("cmbSortExpression.ItemHeight")));
			this.cmbSortExpression.Location = ((System.Drawing.Point)(resources.GetObject("cmbSortExpression.Location")));
			this.cmbSortExpression.MaxDropDownItems = ((int)(resources.GetObject("cmbSortExpression.MaxDropDownItems")));
			this.cmbSortExpression.MaxLength = ((int)(resources.GetObject("cmbSortExpression.MaxLength")));
			this.cmbSortExpression.Name = "cmbSortExpression";
			this.cmbSortExpression.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("cmbSortExpression.RightToLeft")));
			this.cmbSortExpression.Size = ((System.Drawing.Size)(resources.GetObject("cmbSortExpression.Size")));
			this.cmbSortExpression.TabIndex = ((int)(resources.GetObject("cmbSortExpression.TabIndex")));
			this.cmbSortExpression.Text = resources.GetString("cmbSortExpression.Text");
			this.cmbSortExpression.Visible = ((bool)(resources.GetObject("cmbSortExpression.Visible")));
			// 
			// pnlSortExpressions
			// 
			this.pnlSortExpressions.AccessibleDescription = resources.GetString("pnlSortExpressions.AccessibleDescription");
			this.pnlSortExpressions.AccessibleName = resources.GetString("pnlSortExpressions.AccessibleName");
			this.pnlSortExpressions.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("pnlSortExpressions.Anchor")));
			this.pnlSortExpressions.AutoScroll = ((bool)(resources.GetObject("pnlSortExpressions.AutoScroll")));
			this.pnlSortExpressions.AutoScrollMargin = ((System.Drawing.Size)(resources.GetObject("pnlSortExpressions.AutoScrollMargin")));
			this.pnlSortExpressions.AutoScrollMinSize = ((System.Drawing.Size)(resources.GetObject("pnlSortExpressions.AutoScrollMinSize")));
			this.pnlSortExpressions.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pnlSortExpressions.BackgroundImage")));
			this.pnlSortExpressions.Controls.Add(this.cmbMatchExpression);
			this.pnlSortExpressions.Controls.Add(this.lblSortExpression);
			this.pnlSortExpressions.Controls.Add(this.cmbSortExpression);
			this.pnlSortExpressions.Controls.Add(this.lblMatchExpression);
			this.pnlSortExpressions.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("pnlSortExpressions.Dock")));
			this.pnlSortExpressions.Enabled = ((bool)(resources.GetObject("pnlSortExpressions.Enabled")));
			this.pnlSortExpressions.Font = ((System.Drawing.Font)(resources.GetObject("pnlSortExpressions.Font")));
			this.pnlSortExpressions.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("pnlSortExpressions.ImeMode")));
			this.pnlSortExpressions.Location = ((System.Drawing.Point)(resources.GetObject("pnlSortExpressions.Location")));
			this.pnlSortExpressions.Name = "pnlSortExpressions";
			this.pnlSortExpressions.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("pnlSortExpressions.RightToLeft")));
			this.pnlSortExpressions.Size = ((System.Drawing.Size)(resources.GetObject("pnlSortExpressions.Size")));
			this.pnlSortExpressions.TabIndex = ((int)(resources.GetObject("pnlSortExpressions.TabIndex")));
			this.pnlSortExpressions.Text = resources.GetString("pnlSortExpressions.Text");
			this.pnlSortExpressions.Visible = ((bool)(resources.GetObject("pnlSortExpressions.Visible")));
			// 
			// chkUseSortExpression
			// 
			this.chkUseSortExpression.AccessibleDescription = resources.GetString("chkUseSortExpression.AccessibleDescription");
			this.chkUseSortExpression.AccessibleName = resources.GetString("chkUseSortExpression.AccessibleName");
			this.chkUseSortExpression.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("chkUseSortExpression.Anchor")));
			this.chkUseSortExpression.Appearance = ((System.Windows.Forms.Appearance)(resources.GetObject("chkUseSortExpression.Appearance")));
			this.chkUseSortExpression.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("chkUseSortExpression.BackgroundImage")));
			this.chkUseSortExpression.CheckAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("chkUseSortExpression.CheckAlign")));
			this.chkUseSortExpression.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("chkUseSortExpression.Dock")));
			this.chkUseSortExpression.Enabled = ((bool)(resources.GetObject("chkUseSortExpression.Enabled")));
			this.chkUseSortExpression.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("chkUseSortExpression.FlatStyle")));
			this.chkUseSortExpression.Font = ((System.Drawing.Font)(resources.GetObject("chkUseSortExpression.Font")));
			this.chkUseSortExpression.Image = ((System.Drawing.Image)(resources.GetObject("chkUseSortExpression.Image")));
			this.chkUseSortExpression.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("chkUseSortExpression.ImageAlign")));
			this.chkUseSortExpression.ImageIndex = ((int)(resources.GetObject("chkUseSortExpression.ImageIndex")));
			this.chkUseSortExpression.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("chkUseSortExpression.ImeMode")));
			this.chkUseSortExpression.Location = ((System.Drawing.Point)(resources.GetObject("chkUseSortExpression.Location")));
			this.chkUseSortExpression.Name = "chkUseSortExpression";
			this.chkUseSortExpression.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("chkUseSortExpression.RightToLeft")));
			this.chkUseSortExpression.Size = ((System.Drawing.Size)(resources.GetObject("chkUseSortExpression.Size")));
			this.chkUseSortExpression.TabIndex = ((int)(resources.GetObject("chkUseSortExpression.TabIndex")));
			this.chkUseSortExpression.Text = resources.GetString("chkUseSortExpression.Text");
			this.chkUseSortExpression.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("chkUseSortExpression.TextAlign")));
			this.chkUseSortExpression.Visible = ((bool)(resources.GetObject("chkUseSortExpression.Visible")));
			this.chkUseSortExpression.CheckedChanged += new System.EventHandler(this.chkUseSortExpression_CheckedChanged);
			// 
			// chkDeleteDuplicateLines
			// 
			this.chkDeleteDuplicateLines.AccessibleDescription = resources.GetString("chkDeleteDuplicateLines.AccessibleDescription");
			this.chkDeleteDuplicateLines.AccessibleName = resources.GetString("chkDeleteDuplicateLines.AccessibleName");
			this.chkDeleteDuplicateLines.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("chkDeleteDuplicateLines.Anchor")));
			this.chkDeleteDuplicateLines.Appearance = ((System.Windows.Forms.Appearance)(resources.GetObject("chkDeleteDuplicateLines.Appearance")));
			this.chkDeleteDuplicateLines.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("chkDeleteDuplicateLines.BackgroundImage")));
			this.chkDeleteDuplicateLines.CheckAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("chkDeleteDuplicateLines.CheckAlign")));
			this.chkDeleteDuplicateLines.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("chkDeleteDuplicateLines.Dock")));
			this.chkDeleteDuplicateLines.Enabled = ((bool)(resources.GetObject("chkDeleteDuplicateLines.Enabled")));
			this.chkDeleteDuplicateLines.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("chkDeleteDuplicateLines.FlatStyle")));
			this.chkDeleteDuplicateLines.Font = ((System.Drawing.Font)(resources.GetObject("chkDeleteDuplicateLines.Font")));
			this.chkDeleteDuplicateLines.Image = ((System.Drawing.Image)(resources.GetObject("chkDeleteDuplicateLines.Image")));
			this.chkDeleteDuplicateLines.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("chkDeleteDuplicateLines.ImageAlign")));
			this.chkDeleteDuplicateLines.ImageIndex = ((int)(resources.GetObject("chkDeleteDuplicateLines.ImageIndex")));
			this.chkDeleteDuplicateLines.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("chkDeleteDuplicateLines.ImeMode")));
			this.chkDeleteDuplicateLines.Location = ((System.Drawing.Point)(resources.GetObject("chkDeleteDuplicateLines.Location")));
			this.chkDeleteDuplicateLines.Name = "chkDeleteDuplicateLines";
			this.chkDeleteDuplicateLines.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("chkDeleteDuplicateLines.RightToLeft")));
			this.chkDeleteDuplicateLines.Size = ((System.Drawing.Size)(resources.GetObject("chkDeleteDuplicateLines.Size")));
			this.chkDeleteDuplicateLines.TabIndex = ((int)(resources.GetObject("chkDeleteDuplicateLines.TabIndex")));
			this.chkDeleteDuplicateLines.Text = resources.GetString("chkDeleteDuplicateLines.Text");
			this.chkDeleteDuplicateLines.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("chkDeleteDuplicateLines.TextAlign")));
			this.chkDeleteDuplicateLines.Visible = ((bool)(resources.GetObject("chkDeleteDuplicateLines.Visible")));
			// 
			// SortDialog
			// 
			this.AcceptButton = this.btnSort;
			this.AccessibleDescription = resources.GetString("$this.AccessibleDescription");
			this.AccessibleName = resources.GetString("$this.AccessibleName");
			this.AutoScaleBaseSize = ((System.Drawing.Size)(resources.GetObject("$this.AutoScaleBaseSize")));
			this.AutoScroll = ((bool)(resources.GetObject("$this.AutoScroll")));
			this.AutoScrollMargin = ((System.Drawing.Size)(resources.GetObject("$this.AutoScrollMargin")));
			this.AutoScrollMinSize = ((System.Drawing.Size)(resources.GetObject("$this.AutoScrollMinSize")));
			this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
			this.CancelButton = this.btnCancel;
			this.ClientSize = ((System.Drawing.Size)(resources.GetObject("$this.ClientSize")));
			this.ControlBox = false;
			this.Controls.Add(this.chkDeleteDuplicateLines);
			this.Controls.Add(this.chkUseSortExpression);
			this.Controls.Add(this.pnlSortExpressions);
			this.Controls.Add(this.grpCaseSensitivity);
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.btnSort);
			this.Controls.Add(this.grpSortOrder);
			this.Enabled = ((bool)(resources.GetObject("$this.Enabled")));
			this.Font = ((System.Drawing.Font)(resources.GetObject("$this.Font")));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("$this.ImeMode")));
			this.Location = ((System.Drawing.Point)(resources.GetObject("$this.Location")));
			this.MaximizeBox = false;
			this.MaximumSize = ((System.Drawing.Size)(resources.GetObject("$this.MaximumSize")));
			this.MinimizeBox = false;
			this.MinimumSize = ((System.Drawing.Size)(resources.GetObject("$this.MinimumSize")));
			this.Name = "SortDialog";
			this.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("$this.RightToLeft")));
			this.StartPosition = ((System.Windows.Forms.FormStartPosition)(resources.GetObject("$this.StartPosition")));
			this.Text = resources.GetString("$this.Text");
			this.Closing += new System.ComponentModel.CancelEventHandler(this.SortDialog_Closing);
			this.Activated += new System.EventHandler(this.SortDialog_Activated);
			this.grpSortOrder.ResumeLayout(false);
			this.grpCaseSensitivity.ResumeLayout(false);
			this.pnlSortExpressions.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		#endregion
  
		#endregion
  
	}
}
