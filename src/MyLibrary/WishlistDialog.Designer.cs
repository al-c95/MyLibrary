
namespace MyLibrary
{
    partial class WishlistDialog
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.toolStripContainer = new System.Windows.Forms.ToolStripContainer();
            this.deleteButton = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.newCopyNotesBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.saveNewItemChangesButton = new System.Windows.Forms.Button();
            this.newCopyDescriptionField = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.discardChangesButton = new System.Windows.Forms.Button();
            this.selectedCopyNotesBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.saveChangesButton = new System.Windows.Forms.Button();
            this.selectedCopyDescriptionField = new System.Windows.Forms.TextBox();
            this.notesLabel = new System.Windows.Forms.Label();
            this.dataGrid = new System.Windows.Forms.DataGridView();
            this.toolStripContainer.ContentPanel.SuspendLayout();
            this.toolStripContainer.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // toolStripContainer
            // 
            // 
            // toolStripContainer.ContentPanel
            // 
            this.toolStripContainer.ContentPanel.Controls.Add(this.deleteButton);
            this.toolStripContainer.ContentPanel.Controls.Add(this.groupBox2);
            this.toolStripContainer.ContentPanel.Controls.Add(this.groupBox1);
            this.toolStripContainer.ContentPanel.Controls.Add(this.dataGrid);
            this.toolStripContainer.ContentPanel.Size = new System.Drawing.Size(884, 385);
            this.toolStripContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toolStripContainer.Location = new System.Drawing.Point(0, 0);
            this.toolStripContainer.Name = "toolStripContainer";
            this.toolStripContainer.Size = new System.Drawing.Size(884, 410);
            this.toolStripContainer.TabIndex = 0;
            this.toolStripContainer.Text = "toolStripContainer1";
            // 
            // deleteButton
            // 
            this.deleteButton.Enabled = false;
            this.deleteButton.Location = new System.Drawing.Point(235, 181);
            this.deleteButton.Margin = new System.Windows.Forms.Padding(2);
            this.deleteButton.Name = "deleteButton";
            this.deleteButton.Size = new System.Drawing.Size(116, 23);
            this.deleteButton.TabIndex = 28;
            this.deleteButton.Text = "Delete";
            this.deleteButton.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBox2.Controls.Add(this.newCopyNotesBox);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.saveNewItemChangesButton);
            this.groupBox2.Controls.Add(this.newCopyDescriptionField);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Location = new System.Drawing.Point(11, 213);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox2.Size = new System.Drawing.Size(345, 166);
            this.groupBox2.TabIndex = 29;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "New Item";
            // 
            // newCopyNotesBox
            // 
            this.newCopyNotesBox.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.newCopyNotesBox.Location = new System.Drawing.Point(5, 62);
            this.newCopyNotesBox.Margin = new System.Windows.Forms.Padding(2);
            this.newCopyNotesBox.Multiline = true;
            this.newCopyNotesBox.Name = "newCopyNotesBox";
            this.newCopyNotesBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.newCopyNotesBox.Size = new System.Drawing.Size(335, 73);
            this.newCopyNotesBox.TabIndex = 20;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(2, 20);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(72, 15);
            this.label2.TabIndex = 23;
            this.label2.Text = "Description:";
            // 
            // saveNewItemChangesButton
            // 
            this.saveNewItemChangesButton.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.saveNewItemChangesButton.Enabled = false;
            this.saveNewItemChangesButton.Location = new System.Drawing.Point(5, 139);
            this.saveNewItemChangesButton.Margin = new System.Windows.Forms.Padding(2);
            this.saveNewItemChangesButton.Name = "saveNewItemChangesButton";
            this.saveNewItemChangesButton.Size = new System.Drawing.Size(116, 23);
            this.saveNewItemChangesButton.TabIndex = 18;
            this.saveNewItemChangesButton.Text = "Save Changes";
            this.saveNewItemChangesButton.UseVisualStyleBackColor = true;
            // 
            // newCopyDescriptionField
            // 
            this.newCopyDescriptionField.Location = new System.Drawing.Point(69, 18);
            this.newCopyDescriptionField.Margin = new System.Windows.Forms.Padding(2);
            this.newCopyDescriptionField.Name = "newCopyDescriptionField";
            this.newCopyDescriptionField.Size = new System.Drawing.Size(271, 20);
            this.newCopyDescriptionField.TabIndex = 22;
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(4, 46);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(39, 15);
            this.label3.TabIndex = 21;
            this.label3.Text = "Notes";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.discardChangesButton);
            this.groupBox1.Controls.Add(this.selectedCopyNotesBox);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.saveChangesButton);
            this.groupBox1.Controls.Add(this.selectedCopyDescriptionField);
            this.groupBox1.Controls.Add(this.notesLabel);
            this.groupBox1.Location = new System.Drawing.Point(11, 11);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox1.Size = new System.Drawing.Size(345, 166);
            this.groupBox1.TabIndex = 27;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Selected Item Details";
            // 
            // discardChangesButton
            // 
            this.discardChangesButton.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.discardChangesButton.Enabled = false;
            this.discardChangesButton.Location = new System.Drawing.Point(223, 137);
            this.discardChangesButton.Margin = new System.Windows.Forms.Padding(2);
            this.discardChangesButton.Name = "discardChangesButton";
            this.discardChangesButton.Size = new System.Drawing.Size(116, 23);
            this.discardChangesButton.TabIndex = 24;
            this.discardChangesButton.Text = "Discard Changes";
            this.discardChangesButton.UseVisualStyleBackColor = true;
            // 
            // selectedCopyNotesBox
            // 
            this.selectedCopyNotesBox.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.selectedCopyNotesBox.Location = new System.Drawing.Point(5, 62);
            this.selectedCopyNotesBox.Margin = new System.Windows.Forms.Padding(2);
            this.selectedCopyNotesBox.Multiline = true;
            this.selectedCopyNotesBox.Name = "selectedCopyNotesBox";
            this.selectedCopyNotesBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.selectedCopyNotesBox.Size = new System.Drawing.Size(335, 73);
            this.selectedCopyNotesBox.TabIndex = 20;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(2, 20);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(72, 15);
            this.label1.TabIndex = 23;
            this.label1.Text = "Description:";
            // 
            // saveChangesButton
            // 
            this.saveChangesButton.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.saveChangesButton.Enabled = false;
            this.saveChangesButton.Location = new System.Drawing.Point(5, 139);
            this.saveChangesButton.Margin = new System.Windows.Forms.Padding(2);
            this.saveChangesButton.Name = "saveChangesButton";
            this.saveChangesButton.Size = new System.Drawing.Size(116, 23);
            this.saveChangesButton.TabIndex = 18;
            this.saveChangesButton.Text = "Save Changes";
            this.saveChangesButton.UseVisualStyleBackColor = true;
            // 
            // selectedCopyDescriptionField
            // 
            this.selectedCopyDescriptionField.Location = new System.Drawing.Point(69, 18);
            this.selectedCopyDescriptionField.Margin = new System.Windows.Forms.Padding(2);
            this.selectedCopyDescriptionField.Name = "selectedCopyDescriptionField";
            this.selectedCopyDescriptionField.Size = new System.Drawing.Size(271, 20);
            this.selectedCopyDescriptionField.TabIndex = 22;
            // 
            // notesLabel
            // 
            this.notesLabel.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.notesLabel.AutoSize = true;
            this.notesLabel.Location = new System.Drawing.Point(4, 46);
            this.notesLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.notesLabel.Name = "notesLabel";
            this.notesLabel.Size = new System.Drawing.Size(39, 15);
            this.notesLabel.TabIndex = 21;
            this.notesLabel.Text = "Notes";
            // 
            // dataGrid
            // 
            this.dataGrid.AllowUserToAddRows = false;
            this.dataGrid.AllowUserToDeleteRows = false;
            this.dataGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGrid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGrid.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dataGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Arial", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGrid.DefaultCellStyle = dataGridViewCellStyle1;
            this.dataGrid.Location = new System.Drawing.Point(360, 11);
            this.dataGrid.Margin = new System.Windows.Forms.Padding(2);
            this.dataGrid.MultiSelect = false;
            this.dataGrid.Name = "dataGrid";
            this.dataGrid.ReadOnly = true;
            this.dataGrid.RowHeadersWidth = 51;
            this.dataGrid.RowTemplate.Height = 24;
            this.dataGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGrid.Size = new System.Drawing.Size(513, 368);
            this.dataGrid.TabIndex = 26;
            // 
            // WishlistDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(884, 410);
            this.Controls.Add(this.toolStripContainer);
            this.Name = "WishlistDialog";
            this.Text = "Wishlist";
            this.toolStripContainer.ContentPanel.ResumeLayout(false);
            this.toolStripContainer.ResumeLayout(false);
            this.toolStripContainer.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGrid)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ToolStripContainer toolStripContainer;
        private System.Windows.Forms.Button deleteButton;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox newCopyNotesBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button saveNewItemChangesButton;
        private System.Windows.Forms.TextBox newCopyDescriptionField;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button discardChangesButton;
        private System.Windows.Forms.TextBox selectedCopyNotesBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button saveChangesButton;
        private System.Windows.Forms.TextBox selectedCopyDescriptionField;
        private System.Windows.Forms.Label notesLabel;
        private System.Windows.Forms.DataGridView dataGrid;
    }
}