
namespace MyLibrary
{
    partial class AddNewMediaItemForm
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
            this.saveButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.mediaTypesOptions = new System.Windows.Forms.ComboBox();
            this.notesField = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.titleField = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.numberField = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.runningTimeField = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.yearField = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.tagsGroup = new System.Windows.Forms.GroupBox();
            this.newTagField = new System.Windows.Forms.TextBox();
            this.addNewTagButton = new System.Windows.Forms.Button();
            this.tagsList = new System.Windows.Forms.CheckedListBox();
            this.imageFilePathField = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.browseImageButton = new System.Windows.Forms.Button();
            this.tagsGroup.SuspendLayout();
            this.SuspendLayout();
            // 
            // saveButton
            // 
            this.saveButton.Enabled = false;
            this.saveButton.Location = new System.Drawing.Point(9, 346);
            this.saveButton.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(112, 22);
            this.saveButton.TabIndex = 10;
            this.saveButton.Text = "Save";
            this.saveButton.UseVisualStyleBackColor = true;
            // 
            // cancelButton
            // 
            this.cancelButton.Location = new System.Drawing.Point(483, 345);
            this.cancelButton.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(112, 22);
            this.cancelButton.TabIndex = 11;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // mediaTypesOptions
            // 
            this.mediaTypesOptions.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.mediaTypesOptions.FormattingEnabled = true;
            this.mediaTypesOptions.Location = new System.Drawing.Point(55, 5);
            this.mediaTypesOptions.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.mediaTypesOptions.Name = "mediaTypesOptions";
            this.mediaTypesOptions.Size = new System.Drawing.Size(163, 21);
            this.mediaTypesOptions.TabIndex = 12;
            // 
            // notesField
            // 
            this.notesField.Location = new System.Drawing.Point(9, 239);
            this.notesField.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.notesField.Multiline = true;
            this.notesField.Name = "notesField";
            this.notesField.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.notesField.Size = new System.Drawing.Size(588, 94);
            this.notesField.TabIndex = 13;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(9, 223);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(39, 15);
            this.label5.TabIndex = 14;
            this.label5.Text = "Notes";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 7);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(36, 15);
            this.label1.TabIndex = 15;
            this.label1.Text = "Type:";
            // 
            // titleField
            // 
            this.titleField.Location = new System.Drawing.Point(350, 5);
            this.titleField.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.titleField.Name = "titleField";
            this.titleField.Size = new System.Drawing.Size(247, 20);
            this.titleField.TabIndex = 16;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(251, 5);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(33, 15);
            this.label2.TabIndex = 17;
            this.label2.Text = "Title:";
            // 
            // numberField
            // 
            this.numberField.Location = new System.Drawing.Point(350, 28);
            this.numberField.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.numberField.Name = "numberField";
            this.numberField.Size = new System.Drawing.Size(247, 20);
            this.numberField.TabIndex = 18;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(251, 28);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(55, 15);
            this.label3.TabIndex = 19;
            this.label3.Text = "Number:";
            // 
            // runningTimeField
            // 
            this.runningTimeField.Location = new System.Drawing.Point(350, 50);
            this.runningTimeField.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.runningTimeField.Name = "runningTimeField";
            this.runningTimeField.Size = new System.Drawing.Size(247, 20);
            this.runningTimeField.TabIndex = 20;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(251, 53);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(88, 15);
            this.label4.TabIndex = 21;
            this.label4.Text = "Running Time:";
            // 
            // yearField
            // 
            this.yearField.Location = new System.Drawing.Point(350, 73);
            this.yearField.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.yearField.Name = "yearField";
            this.yearField.Size = new System.Drawing.Size(247, 20);
            this.yearField.TabIndex = 22;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(251, 76);
            this.label6.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(35, 15);
            this.label6.TabIndex = 23;
            this.label6.Text = "Year:";
            // 
            // tagsGroup
            // 
            this.tagsGroup.Controls.Add(this.newTagField);
            this.tagsGroup.Controls.Add(this.addNewTagButton);
            this.tagsGroup.Controls.Add(this.tagsList);
            this.tagsGroup.Location = new System.Drawing.Point(159, 96);
            this.tagsGroup.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tagsGroup.Name = "tagsGroup";
            this.tagsGroup.Padding = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tagsGroup.Size = new System.Drawing.Size(290, 101);
            this.tagsGroup.TabIndex = 24;
            this.tagsGroup.TabStop = false;
            this.tagsGroup.Text = "Tags";
            // 
            // newTagField
            // 
            this.newTagField.Location = new System.Drawing.Point(160, 17);
            this.newTagField.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.newTagField.Name = "newTagField";
            this.newTagField.Size = new System.Drawing.Size(126, 20);
            this.newTagField.TabIndex = 25;
            // 
            // addNewTagButton
            // 
            this.addNewTagButton.Location = new System.Drawing.Point(214, 40);
            this.addNewTagButton.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.addNewTagButton.Name = "addNewTagButton";
            this.addNewTagButton.Size = new System.Drawing.Size(71, 22);
            this.addNewTagButton.TabIndex = 11;
            this.addNewTagButton.Text = "Add New";
            this.addNewTagButton.UseVisualStyleBackColor = true;
            // 
            // tagsList
            // 
            this.tagsList.FormattingEnabled = true;
            this.tagsList.Location = new System.Drawing.Point(4, 17);
            this.tagsList.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tagsList.Name = "tagsList";
            this.tagsList.Size = new System.Drawing.Size(152, 79);
            this.tagsList.TabIndex = 0;
            // 
            // imageFilePathField
            // 
            this.imageFilePathField.Location = new System.Drawing.Point(159, 209);
            this.imageFilePathField.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.imageFilePathField.Name = "imageFilePathField";
            this.imageFilePathField.Size = new System.Drawing.Size(362, 20);
            this.imageFilePathField.TabIndex = 25;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(100, 211);
            this.label7.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(64, 15);
            this.label7.TabIndex = 26;
            this.label7.Text = "Image file:";
            // 
            // browseImageButton
            // 
            this.browseImageButton.Location = new System.Drawing.Point(524, 207);
            this.browseImageButton.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.browseImageButton.Name = "browseImageButton";
            this.browseImageButton.Size = new System.Drawing.Size(71, 22);
            this.browseImageButton.TabIndex = 26;
            this.browseImageButton.Text = "Browse";
            this.browseImageButton.UseVisualStyleBackColor = true;
            // 
            // AddNewMediaItemForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(604, 375);
            this.Controls.Add(this.browseImageButton);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.imageFilePathField);
            this.Controls.Add(this.tagsGroup);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.yearField);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.runningTimeField);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.numberField);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.titleField);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.notesField);
            this.Controls.Add(this.mediaTypesOptions);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.saveButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AddNewMediaItemForm";
            this.Text = "Add New Media Item";
            this.tagsGroup.ResumeLayout(false);
            this.tagsGroup.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button saveButton;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.ComboBox mediaTypesOptions;
        private System.Windows.Forms.TextBox notesField;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox titleField;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox numberField;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox runningTimeField;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox yearField;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.GroupBox tagsGroup;
        private System.Windows.Forms.Button addNewTagButton;
        private System.Windows.Forms.CheckedListBox tagsList;
        private System.Windows.Forms.TextBox newTagField;
        private System.Windows.Forms.TextBox imageFilePathField;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button browseImageButton;
    }
}