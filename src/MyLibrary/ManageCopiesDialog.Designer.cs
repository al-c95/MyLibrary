﻿
namespace MyLibrary
{
    partial class ManageCopiesDialog
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.toolStripContainer = new System.Windows.Forms.ToolStripContainer();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.deleteButton = new System.Windows.Forms.Button();
            this.itemTitleLabel = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.newCopyNotesBox = new System.Windows.Forms.TextBox();
            this.saveNewCopyChangesButton = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.discardChangesButton = new System.Windows.Forms.Button();
            this.selectedCopyNotesBox = new System.Windows.Forms.TextBox();
            this.saveChangesButton = new System.Windows.Forms.Button();
            this.notesLabel = new System.Windows.Forms.Label();
            this.dataGrid = new System.Windows.Forms.DataGridView();
            this.label2 = new System.Windows.Forms.Label();
            this.newCopyDescriptionField = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.selectedCopyDescriptionField = new System.Windows.Forms.TextBox();
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
            this.toolStripContainer.ContentPanel.Controls.Add(this.deleteButton);
            this.toolStripContainer.ContentPanel.Controls.Add(this.itemTitleLabel);
            this.toolStripContainer.ContentPanel.Controls.Add(this.groupBox2);
            this.toolStripContainer.ContentPanel.Controls.Add(this.groupBox1);
            this.toolStripContainer.ContentPanel.Controls.Add(this.dataGrid);
            this.toolStripContainer.ContentPanel.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.toolStripContainer.ContentPanel.Size = new System.Drawing.Size(760, 415);
            this.toolStripContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toolStripContainer.Location = new System.Drawing.Point(0, 0);
            this.toolStripContainer.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.toolStripContainer.Name = "toolStripContainer";
            this.toolStripContainer.Size = new System.Drawing.Size(760, 461);
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
            this.statusStrip1.Size = new System.Drawing.Size(760, 26);
            this.statusStrip1.TabIndex = 0;
            // 
            // toolStripStatusLabel
            // 
            this.toolStripStatusLabel.Name = "toolStripStatusLabel";
            this.toolStripStatusLabel.Size = new System.Drawing.Size(53, 20);
            this.toolStripStatusLabel.Text = "Ready.";
            // 
            // deleteButton
            // 
            this.deleteButton.Enabled = false;
            this.deleteButton.Location = new System.Drawing.Point(232, 213);
            this.deleteButton.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.deleteButton.Name = "deleteButton";
            this.deleteButton.Size = new System.Drawing.Size(116, 23);
            this.deleteButton.TabIndex = 25;
            this.deleteButton.Text = "Delete";
            this.deleteButton.UseVisualStyleBackColor = true;
            // 
            // itemTitleLabel
            // 
            this.itemTitleLabel.AutoSize = true;
            this.itemTitleLabel.Location = new System.Drawing.Point(9, 7);
            this.itemTitleLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.itemTitleLabel.Name = "itemTitleLabel";
            this.itemTitleLabel.Size = new System.Drawing.Size(41, 15);
            this.itemTitleLabel.TabIndex = 26;
            this.itemTitleLabel.Text = "label4";
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBox2.Controls.Add(this.newCopyNotesBox);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.saveNewCopyChangesButton);
            this.groupBox2.Controls.Add(this.newCopyDescriptionField);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Location = new System.Drawing.Point(9, 241);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.groupBox2.Size = new System.Drawing.Size(345, 166);
            this.groupBox2.TabIndex = 25;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "New Copy";
            // 
            // newCopyNotesBox
            // 
            this.newCopyNotesBox.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.newCopyNotesBox.Location = new System.Drawing.Point(6, 64);
            this.newCopyNotesBox.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.newCopyNotesBox.Multiline = true;
            this.newCopyNotesBox.Name = "newCopyNotesBox";
            this.newCopyNotesBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.newCopyNotesBox.Size = new System.Drawing.Size(333, 71);
            this.newCopyNotesBox.TabIndex = 20;
            // 
            // saveNewCopyChangesButton
            // 
            this.saveNewCopyChangesButton.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.saveNewCopyChangesButton.Enabled = false;
            this.saveNewCopyChangesButton.Location = new System.Drawing.Point(5, 139);
            this.saveNewCopyChangesButton.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.saveNewCopyChangesButton.Name = "saveNewCopyChangesButton";
            this.saveNewCopyChangesButton.Size = new System.Drawing.Size(116, 23);
            this.saveNewCopyChangesButton.TabIndex = 18;
            this.saveNewCopyChangesButton.Text = "Save Changes";
            this.saveNewCopyChangesButton.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(5, 47);
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
            this.groupBox1.Location = new System.Drawing.Point(9, 43);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.groupBox1.Size = new System.Drawing.Size(345, 166);
            this.groupBox1.TabIndex = 24;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Selected Copy Details";
            // 
            // discardChangesButton
            // 
            this.discardChangesButton.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.discardChangesButton.Enabled = false;
            this.discardChangesButton.Location = new System.Drawing.Point(223, 137);
            this.discardChangesButton.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.discardChangesButton.Name = "discardChangesButton";
            this.discardChangesButton.Size = new System.Drawing.Size(116, 23);
            this.discardChangesButton.TabIndex = 24;
            this.discardChangesButton.Text = "Discard Changes";
            this.discardChangesButton.UseVisualStyleBackColor = true;
            // 
            // selectedCopyNotesBox
            // 
            this.selectedCopyNotesBox.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.selectedCopyNotesBox.Location = new System.Drawing.Point(5, 61);
            this.selectedCopyNotesBox.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.selectedCopyNotesBox.Multiline = true;
            this.selectedCopyNotesBox.Name = "selectedCopyNotesBox";
            this.selectedCopyNotesBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.selectedCopyNotesBox.Size = new System.Drawing.Size(334, 74);
            this.selectedCopyNotesBox.TabIndex = 20;
            // 
            // saveChangesButton
            // 
            this.saveChangesButton.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.saveChangesButton.Enabled = false;
            this.saveChangesButton.Location = new System.Drawing.Point(5, 139);
            this.saveChangesButton.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.saveChangesButton.Name = "saveChangesButton";
            this.saveChangesButton.Size = new System.Drawing.Size(116, 23);
            this.saveChangesButton.TabIndex = 18;
            this.saveChangesButton.Text = "Save Changes";
            this.saveChangesButton.UseVisualStyleBackColor = true;
            // 
            // notesLabel
            // 
            this.notesLabel.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.notesLabel.AutoSize = true;
            this.notesLabel.Location = new System.Drawing.Point(5, 44);
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
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Arial", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGrid.DefaultCellStyle = dataGridViewCellStyle2;
            this.dataGrid.Location = new System.Drawing.Point(361, 46);
            this.dataGrid.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.dataGrid.MultiSelect = false;
            this.dataGrid.Name = "dataGrid";
            this.dataGrid.ReadOnly = true;
            this.dataGrid.RowHeadersWidth = 51;
            this.dataGrid.RowTemplate.Height = 24;
            this.dataGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGrid.Size = new System.Drawing.Size(392, 361);
            this.dataGrid.TabIndex = 8;
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
            // newCopyDescriptionField
            // 
            this.newCopyDescriptionField.Location = new System.Drawing.Point(69, 18);
            this.newCopyDescriptionField.Margin = new System.Windows.Forms.Padding(2);
            this.newCopyDescriptionField.Name = "newCopyDescriptionField";
            this.newCopyDescriptionField.Size = new System.Drawing.Size(271, 20);
            this.newCopyDescriptionField.TabIndex = 22;
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
            // selectedCopyDescriptionField
            // 
            this.selectedCopyDescriptionField.Location = new System.Drawing.Point(69, 18);
            this.selectedCopyDescriptionField.Margin = new System.Windows.Forms.Padding(2);
            this.selectedCopyDescriptionField.Name = "selectedCopyDescriptionField";
            this.selectedCopyDescriptionField.Size = new System.Drawing.Size(271, 20);
            this.selectedCopyDescriptionField.TabIndex = 22;
            // 
            // ManageCopiesDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(760, 461);
            this.Controls.Add(this.toolStripContainer);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "ManageCopiesDialog";
            this.Text = "Manage Copies For Item";
            this.toolStripContainer.BottomToolStripPanel.ResumeLayout(false);
            this.toolStripContainer.BottomToolStripPanel.PerformLayout();
            this.toolStripContainer.ContentPanel.ResumeLayout(false);
            this.toolStripContainer.ContentPanel.PerformLayout();
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
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel;
        private System.Windows.Forms.DataGridView dataGrid;
        private System.Windows.Forms.Label notesLabel;
        private System.Windows.Forms.TextBox selectedCopyNotesBox;
        private System.Windows.Forms.Button saveChangesButton;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox newCopyNotesBox;
        private System.Windows.Forms.Button saveNewCopyChangesButton;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label itemTitleLabel;
        private System.Windows.Forms.Button discardChangesButton;
        private System.Windows.Forms.Button deleteButton;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox newCopyDescriptionField;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox selectedCopyDescriptionField;
    }
}