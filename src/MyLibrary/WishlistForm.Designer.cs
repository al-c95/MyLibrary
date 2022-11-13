
namespace MyLibrary
{
    partial class WishlistForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WishlistForm));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.toolStripContainer = new System.Windows.Forms.ToolStripContainer();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.typesDropDown = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.newItemTitleField = new System.Windows.Forms.TextBox();
            this.newItemNotesBox = new System.Windows.Forms.TextBox();
            this.saveNewItemChangesButton = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.deleteButton = new System.Windows.Forms.Button();
            this.discardChangesButton = new System.Windows.Forms.Button();
            this.selectedItemNotesBox = new System.Windows.Forms.TextBox();
            this.saveChangesButton = new System.Windows.Forms.Button();
            this.notesLabel = new System.Windows.Forms.Label();
            this.dataGrid = new System.Windows.Forms.DataGridView();
            this.addToLibraryButton = new System.Windows.Forms.Button();
            this.toolStripContainer.BottomToolStripPanel.SuspendLayout();
            this.toolStripContainer.ContentPanel.SuspendLayout();
            this.toolStripContainer.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // toolStripContainer
            // 
            // 
            // toolStripContainer.BottomToolStripPanel
            // 
            this.toolStripContainer.BottomToolStripPanel.Controls.Add(this.statusStrip1);
            // 
            // toolStripContainer.ContentPanel
            // 
            this.toolStripContainer.ContentPanel.Controls.Add(this.groupBox2);
            this.toolStripContainer.ContentPanel.Controls.Add(this.groupBox1);
            this.toolStripContainer.ContentPanel.Controls.Add(this.dataGrid);
            this.toolStripContainer.ContentPanel.Margin = new System.Windows.Forms.Padding(4);
            this.toolStripContainer.ContentPanel.Size = new System.Drawing.Size(1137, 597);
            this.toolStripContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toolStripContainer.Location = new System.Drawing.Point(0, 0);
            this.toolStripContainer.Margin = new System.Windows.Forms.Padding(4);
            this.toolStripContainer.Name = "toolStripContainer";
            this.toolStripContainer.Size = new System.Drawing.Size(1137, 648);
            this.toolStripContainer.TabIndex = 0;
            this.toolStripContainer.Text = "toolStripContainer1";
            // 
            // statusStrip1
            // 
            this.statusStrip1.Dock = System.Windows.Forms.DockStyle.None;
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel});
            this.statusStrip1.Location = new System.Drawing.Point(0, 0);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1137, 26);
            this.statusStrip1.TabIndex = 0;
            // 
            // toolStripStatusLabel
            // 
            this.toolStripStatusLabel.Name = "toolStripStatusLabel";
            this.toolStripStatusLabel.Size = new System.Drawing.Size(53, 20);
            this.toolStripStatusLabel.Text = "Ready.";
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.typesDropDown);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.newItemTitleField);
            this.groupBox2.Controls.Add(this.newItemNotesBox);
            this.groupBox2.Controls.Add(this.saveNewItemChangesButton);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Location = new System.Drawing.Point(15, 317);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox2.Size = new System.Drawing.Size(460, 272);
            this.groupBox2.TabIndex = 29;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "New Item";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 210);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(42, 16);
            this.label2.TabIndex = 30;
            this.label2.Text = "Type:";
            // 
            // typesDropDown
            // 
            this.typesDropDown.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.typesDropDown.FormattingEnabled = true;
            this.typesDropDown.Location = new System.Drawing.Point(75, 207);
            this.typesDropDown.Margin = new System.Windows.Forms.Padding(4);
            this.typesDropDown.Name = "typesDropDown";
            this.typesDropDown.Size = new System.Drawing.Size(377, 24);
            this.typesDropDown.TabIndex = 30;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(5, 23);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(36, 16);
            this.label1.TabIndex = 30;
            this.label1.Text = "Title:";
            // 
            // newItemTitleField
            // 
            this.newItemTitleField.Location = new System.Drawing.Point(57, 22);
            this.newItemTitleField.Margin = new System.Windows.Forms.Padding(4);
            this.newItemTitleField.Name = "newItemTitleField";
            this.newItemTitleField.Size = new System.Drawing.Size(393, 22);
            this.newItemTitleField.TabIndex = 30;
            // 
            // newItemNotesBox
            // 
            this.newItemNotesBox.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.newItemNotesBox.Location = new System.Drawing.Point(7, 74);
            this.newItemNotesBox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.newItemNotesBox.Multiline = true;
            this.newItemNotesBox.Name = "newItemNotesBox";
            this.newItemNotesBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.newItemNotesBox.Size = new System.Drawing.Size(445, 126);
            this.newItemNotesBox.TabIndex = 20;
            // 
            // saveNewItemChangesButton
            // 
            this.saveNewItemChangesButton.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.saveNewItemChangesButton.Enabled = false;
            this.saveNewItemChangesButton.Image = ((System.Drawing.Image)(resources.GetObject("saveNewItemChangesButton.Image")));
            this.saveNewItemChangesButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.saveNewItemChangesButton.Location = new System.Drawing.Point(7, 239);
            this.saveNewItemChangesButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.saveNewItemChangesButton.Name = "saveNewItemChangesButton";
            this.saveNewItemChangesButton.Size = new System.Drawing.Size(155, 28);
            this.saveNewItemChangesButton.TabIndex = 18;
            this.saveNewItemChangesButton.Text = "Save Changes";
            this.saveNewItemChangesButton.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(5, 53);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(43, 16);
            this.label3.TabIndex = 21;
            this.label3.Text = "Notes";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.addToLibraryButton);
            this.groupBox1.Controls.Add(this.deleteButton);
            this.groupBox1.Controls.Add(this.discardChangesButton);
            this.groupBox1.Controls.Add(this.selectedItemNotesBox);
            this.groupBox1.Controls.Add(this.saveChangesButton);
            this.groupBox1.Controls.Add(this.notesLabel);
            this.groupBox1.Location = new System.Drawing.Point(15, 14);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox1.Size = new System.Drawing.Size(460, 299);
            this.groupBox1.TabIndex = 27;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Selected Item";
            // 
            // deleteButton
            // 
            this.deleteButton.Enabled = false;
            this.deleteButton.Image = ((System.Drawing.Image)(resources.GetObject("deleteButton.Image")));
            this.deleteButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.deleteButton.Location = new System.Drawing.Point(10, 227);
            this.deleteButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.deleteButton.Name = "deleteButton";
            this.deleteButton.Size = new System.Drawing.Size(155, 28);
            this.deleteButton.TabIndex = 28;
            this.deleteButton.Text = "Delete";
            this.deleteButton.UseVisualStyleBackColor = true;
            // 
            // discardChangesButton
            // 
            this.discardChangesButton.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.discardChangesButton.Enabled = false;
            this.discardChangesButton.Image = ((System.Drawing.Image)(resources.GetObject("discardChangesButton.Image")));
            this.discardChangesButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.discardChangesButton.Location = new System.Drawing.Point(298, 180);
            this.discardChangesButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.discardChangesButton.Name = "discardChangesButton";
            this.discardChangesButton.Size = new System.Drawing.Size(155, 28);
            this.discardChangesButton.TabIndex = 24;
            this.discardChangesButton.Text = "Discard Changes";
            this.discardChangesButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.discardChangesButton.UseVisualStyleBackColor = true;
            // 
            // selectedItemNotesBox
            // 
            this.selectedItemNotesBox.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.selectedItemNotesBox.Location = new System.Drawing.Point(8, 50);
            this.selectedItemNotesBox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.selectedItemNotesBox.Multiline = true;
            this.selectedItemNotesBox.Name = "selectedItemNotesBox";
            this.selectedItemNotesBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.selectedItemNotesBox.Size = new System.Drawing.Size(445, 126);
            this.selectedItemNotesBox.TabIndex = 20;
            // 
            // saveChangesButton
            // 
            this.saveChangesButton.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.saveChangesButton.Enabled = false;
            this.saveChangesButton.Image = ((System.Drawing.Image)(resources.GetObject("saveChangesButton.Image")));
            this.saveChangesButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.saveChangesButton.Location = new System.Drawing.Point(8, 180);
            this.saveChangesButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.saveChangesButton.Name = "saveChangesButton";
            this.saveChangesButton.Size = new System.Drawing.Size(155, 28);
            this.saveChangesButton.TabIndex = 18;
            this.saveChangesButton.Text = "Save Changes";
            this.saveChangesButton.UseVisualStyleBackColor = true;
            // 
            // notesLabel
            // 
            this.notesLabel.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.notesLabel.AutoSize = true;
            this.notesLabel.Location = new System.Drawing.Point(7, 32);
            this.notesLabel.Name = "notesLabel";
            this.notesLabel.Size = new System.Drawing.Size(43, 16);
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
            this.dataGrid.Location = new System.Drawing.Point(480, 14);
            this.dataGrid.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dataGrid.MultiSelect = false;
            this.dataGrid.Name = "dataGrid";
            this.dataGrid.ReadOnly = true;
            this.dataGrid.RowHeadersWidth = 51;
            this.dataGrid.RowTemplate.Height = 24;
            this.dataGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGrid.Size = new System.Drawing.Size(642, 576);
            this.dataGrid.TabIndex = 26;
            // 
            // addToLibraryButton
            // 
            this.addToLibraryButton.Enabled = false;
            this.addToLibraryButton.Image = ((System.Drawing.Image)(resources.GetObject("addToLibraryButton.Image")));
            this.addToLibraryButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.addToLibraryButton.Location = new System.Drawing.Point(10, 259);
            this.addToLibraryButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.addToLibraryButton.Name = "addToLibraryButton";
            this.addToLibraryButton.Size = new System.Drawing.Size(155, 28);
            this.addToLibraryButton.TabIndex = 29;
            this.addToLibraryButton.Text = "Add to Library";
            this.addToLibraryButton.UseVisualStyleBackColor = true;
            // 
            // WishlistDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1137, 648);
            this.Controls.Add(this.toolStripContainer);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "WishlistDialog";
            this.Text = "Wishlist";
            this.toolStripContainer.BottomToolStripPanel.ResumeLayout(false);
            this.toolStripContainer.BottomToolStripPanel.PerformLayout();
            this.toolStripContainer.ContentPanel.ResumeLayout(false);
            this.toolStripContainer.ResumeLayout(false);
            this.toolStripContainer.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
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
        private System.Windows.Forms.TextBox newItemNotesBox;
        private System.Windows.Forms.Button saveNewItemChangesButton;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button discardChangesButton;
        private System.Windows.Forms.TextBox selectedItemNotesBox;
        private System.Windows.Forms.Button saveChangesButton;
        private System.Windows.Forms.Label notesLabel;
        private System.Windows.Forms.DataGridView dataGrid;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox newItemTitleField;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox typesDropDown;
        private System.Windows.Forms.Button addToLibraryButton;
    }
}