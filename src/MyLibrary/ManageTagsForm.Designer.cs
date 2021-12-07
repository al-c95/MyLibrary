
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
            this.tagsList = new System.Windows.Forms.ListView();
            this.addTagButton = new System.Windows.Forms.Button();
            this.addTagGroup = new System.Windows.Forms.GroupBox();
            this.newTagText = new System.Windows.Forms.TextBox();
            this.deleteSelectedTagButton = new System.Windows.Forms.Button();
            this.addTagGroup.SuspendLayout();
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
            this.addTagButton.Location = new System.Drawing.Point(6, 58);
            this.addTagButton.Name = "addTagButton";
            this.addTagButton.Size = new System.Drawing.Size(87, 28);
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
            this.deleteSelectedTagButton.Location = new System.Drawing.Point(18, 110);
            this.deleteSelectedTagButton.Name = "deleteSelectedTagButton";
            this.deleteSelectedTagButton.Size = new System.Drawing.Size(146, 28);
            this.deleteSelectedTagButton.TabIndex = 4;
            this.deleteSelectedTagButton.Text = "Delete Selected";
            this.deleteSelectedTagButton.UseVisualStyleBackColor = true;
            // 
            // ManageTagsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(390, 450);
            this.Controls.Add(this.deleteSelectedTagButton);
            this.Controls.Add(this.addTagGroup);
            this.Controls.Add(this.tagsList);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ManageTagsForm";
            this.Text = "Manage Tags";
            this.addTagGroup.ResumeLayout(false);
            this.addTagGroup.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView tagsList;
        private System.Windows.Forms.Button addTagButton;
        private System.Windows.Forms.GroupBox addTagGroup;
        private System.Windows.Forms.TextBox newTagText;
        private System.Windows.Forms.Button deleteSelectedTagButton;
    }
}