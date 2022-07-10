
namespace MyLibrary
{
    partial class ManageTagsForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ManageTagsForm));
            this.tagsList = new System.Windows.Forms.ListView();
            this.addTagButton = new System.Windows.Forms.Button();
            this.addTagGroup = new System.Windows.Forms.GroupBox();
            this.newTagText = new System.Windows.Forms.TextBox();
            this.deleteSelectedTagButton = new System.Windows.Forms.Button();
            this.applyFilterButton = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.filterTagField = new System.Windows.Forms.TextBox();
            this.clearFilterButton = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.addTagGroup.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tagsList
            // 
            this.tagsList.GridLines = true;
            this.tagsList.HideSelection = false;
            this.tagsList.Location = new System.Drawing.Point(12, 144);
            this.tagsList.Name = "tagsList";
            this.tagsList.Size = new System.Drawing.Size(366, 294);
            this.tagsList.TabIndex = 0;
            this.tagsList.UseCompatibleStateImageBehavior = false;
            // 
            // addTagButton
            // 
            this.addTagButton.Image = ((System.Drawing.Image)(resources.GetObject("addTagButton.Image")));
            this.addTagButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.addTagButton.Location = new System.Drawing.Point(6, 58);
            this.addTagButton.Name = "addTagButton";
            this.addTagButton.Size = new System.Drawing.Size(159, 28);
            this.addTagButton.TabIndex = 1;
            this.addTagButton.Text = "Add";
            this.addTagButton.UseVisualStyleBackColor = true;
            // 
            // addTagGroup
            // 
            this.addTagGroup.Controls.Add(this.newTagText);
            this.addTagGroup.Controls.Add(this.addTagButton);
            this.addTagGroup.Location = new System.Drawing.Point(12, 12);
            this.addTagGroup.Name = "addTagGroup";
            this.addTagGroup.Size = new System.Drawing.Size(366, 92);
            this.addTagGroup.TabIndex = 2;
            this.addTagGroup.TabStop = false;
            this.addTagGroup.Text = "Add new tag";
            // 
            // newTagText
            // 
            this.newTagText.Location = new System.Drawing.Point(6, 21);
            this.newTagText.Name = "newTagText";
            this.newTagText.Size = new System.Drawing.Size(354, 22);
            this.newTagText.TabIndex = 3;
            // 
            // deleteSelectedTagButton
            // 
            this.deleteSelectedTagButton.Image = ((System.Drawing.Image)(resources.GetObject("deleteSelectedTagButton.Image")));
            this.deleteSelectedTagButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.deleteSelectedTagButton.Location = new System.Drawing.Point(18, 110);
            this.deleteSelectedTagButton.Name = "deleteSelectedTagButton";
            this.deleteSelectedTagButton.Size = new System.Drawing.Size(159, 28);
            this.deleteSelectedTagButton.TabIndex = 4;
            this.deleteSelectedTagButton.Text = "Delete Selected";
            this.deleteSelectedTagButton.UseVisualStyleBackColor = true;
            // 
            // applyFilterButton
            // 
            this.applyFilterButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.applyFilterButton.Location = new System.Drawing.Point(90, 59);
            this.applyFilterButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.applyFilterButton.Name = "applyFilterButton";
            this.applyFilterButton.Size = new System.Drawing.Size(75, 28);
            this.applyFilterButton.TabIndex = 29;
            this.applyFilterButton.Text = "Apply";
            this.applyFilterButton.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.clearFilterButton);
            this.groupBox1.Controls.Add(this.filterTagField);
            this.groupBox1.Controls.Add(this.applyFilterButton);
            this.groupBox1.Location = new System.Drawing.Point(12, 444);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(366, 102);
            this.groupBox1.TabIndex = 30;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Filter";
            // 
            // filterTagField
            // 
            this.filterTagField.Location = new System.Drawing.Point(90, 20);
            this.filterTagField.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.filterTagField.Name = "filterTagField";
            this.filterTagField.Size = new System.Drawing.Size(270, 22);
            this.filterTagField.TabIndex = 30;
            // 
            // clearFilterButton
            // 
            this.clearFilterButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.clearFilterButton.Location = new System.Drawing.Point(285, 59);
            this.clearFilterButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.clearFilterButton.Name = "clearFilterButton";
            this.clearFilterButton.Size = new System.Drawing.Size(75, 28);
            this.clearFilterButton.TabIndex = 31;
            this.clearFilterButton.Text = "Clear";
            this.clearFilterButton.UseVisualStyleBackColor = true;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(6, 23);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(78, 21);
            this.label8.TabIndex = 32;
            this.label8.Text = "Filter by:";
            // 
            // ManageTagsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(390, 558);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.deleteSelectedTagButton);
            this.Controls.Add(this.addTagGroup);
            this.Controls.Add(this.tagsList);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ManageTagsForm";
            this.Text = "Manage Tags";
            this.addTagGroup.ResumeLayout(false);
            this.addTagGroup.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView tagsList;
        private System.Windows.Forms.Button addTagButton;
        private System.Windows.Forms.GroupBox addTagGroup;
        private System.Windows.Forms.TextBox newTagText;
        private System.Windows.Forms.Button deleteSelectedTagButton;
        private System.Windows.Forms.Button applyFilterButton;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox filterTagField;
        private System.Windows.Forms.Button clearFilterButton;
        private System.Windows.Forms.Label label8;
    }
}