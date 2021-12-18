
namespace MyLibrary
{
    partial class ManageTagsForItemDialog
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
            this.buttonSave = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.tagsList = new System.Windows.Forms.CheckedListBox();
            this.newTagField = new System.Windows.Forms.TextBox();
            this.addNewTagButton = new System.Windows.Forms.Button();
            this.itemTitleLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // buttonSave
            // 
            this.buttonSave.Location = new System.Drawing.Point(12, 412);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(96, 26);
            this.buttonSave.TabIndex = 0;
            this.buttonSave.Text = "Save";
            this.buttonSave.UseVisualStyleBackColor = true;
            // 
            // buttonCancel
            // 
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(282, 412);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(96, 26);
            this.buttonCancel.TabIndex = 1;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            // 
            // tagsList
            // 
            this.tagsList.FormattingEnabled = true;
            this.tagsList.Location = new System.Drawing.Point(12, 46);
            this.tagsList.Name = "tagsList";
            this.tagsList.Size = new System.Drawing.Size(366, 293);
            this.tagsList.TabIndex = 2;
            // 
            // newTagField
            // 
            this.newTagField.Location = new System.Drawing.Point(12, 345);
            this.newTagField.Name = "newTagField";
            this.newTagField.Size = new System.Drawing.Size(366, 22);
            this.newTagField.TabIndex = 3;
            // 
            // addNewTagButton
            // 
            this.addNewTagButton.Location = new System.Drawing.Point(12, 373);
            this.addNewTagButton.Name = "addNewTagButton";
            this.addNewTagButton.Size = new System.Drawing.Size(96, 26);
            this.addNewTagButton.TabIndex = 4;
            this.addNewTagButton.Text = "Add";
            this.addNewTagButton.UseVisualStyleBackColor = true;
            // 
            // itemTitleLabel
            // 
            this.itemTitleLabel.AutoSize = true;
            this.itemTitleLabel.Location = new System.Drawing.Point(12, 9);
            this.itemTitleLabel.Name = "itemTitleLabel";
            this.itemTitleLabel.Size = new System.Drawing.Size(46, 17);
            this.itemTitleLabel.TabIndex = 5;
            this.itemTitleLabel.Text = "label1";
            // 
            // ManageTagsForItemDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(390, 450);
            this.Controls.Add(this.itemTitleLabel);
            this.Controls.Add(this.addNewTagButton);
            this.Controls.Add(this.newTagField);
            this.Controls.Add(this.tagsList);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonSave);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ManageTagsForItemDialog";
            this.Text = "Manage Tags For Item";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.CheckedListBox tagsList;
        private System.Windows.Forms.TextBox newTagField;
        private System.Windows.Forms.Button addNewTagButton;
        private System.Windows.Forms.Label itemTitleLabel;
    }
}