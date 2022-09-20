
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AddNewMediaItemForm));
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
            this.clearFilterButton = new System.Windows.Forms.Button();
            this.applyFilterButton = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.filterTagField = new System.Windows.Forms.TextBox();
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
            this.saveButton.Image = ((System.Drawing.Image)(resources.GetObject("saveButton.Image")));
            this.saveButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.saveButton.Location = new System.Drawing.Point(12, 585);
            this.saveButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(149, 27);
            this.saveButton.TabIndex = 10;
            this.saveButton.Text = "Save";
            this.saveButton.UseVisualStyleBackColor = true;
            // 
            // cancelButton
            // 
            this.cancelButton.Image = ((System.Drawing.Image)(resources.GetObject("cancelButton.Image")));
            this.cancelButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.cancelButton.Location = new System.Drawing.Point(325, 585);
            this.cancelButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(149, 27);
            this.cancelButton.TabIndex = 11;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // mediaTypesOptions
            // 
            this.mediaTypesOptions.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.mediaTypesOptions.FormattingEnabled = true;
            this.mediaTypesOptions.Location = new System.Drawing.Point(146, 6);
            this.mediaTypesOptions.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.mediaTypesOptions.Name = "mediaTypesOptions";
            this.mediaTypesOptions.Size = new System.Drawing.Size(328, 24);
            this.mediaTypesOptions.TabIndex = 12;
            // 
            // notesField
            // 
            this.notesField.Location = new System.Drawing.Point(12, 478);
            this.notesField.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.notesField.Multiline = true;
            this.notesField.Name = "notesField";
            this.notesField.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.notesField.Size = new System.Drawing.Size(457, 94);
            this.notesField.TabIndex = 13;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(20, 459);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(43, 16);
            this.label5.TabIndex = 14;
            this.label5.Text = "Notes";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(14, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(42, 16);
            this.label1.TabIndex = 15;
            this.label1.Text = "Type:";
            // 
            // titleField
            // 
            this.titleField.Location = new System.Drawing.Point(146, 46);
            this.titleField.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.titleField.Name = "titleField";
            this.titleField.Size = new System.Drawing.Size(328, 22);
            this.titleField.TabIndex = 16;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(14, 46);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(36, 16);
            this.label2.TabIndex = 17;
            this.label2.Text = "Title:";
            // 
            // numberField
            // 
            this.numberField.Location = new System.Drawing.Point(146, 74);
            this.numberField.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.numberField.Name = "numberField";
            this.numberField.Size = new System.Drawing.Size(328, 22);
            this.numberField.TabIndex = 18;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(14, 74);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(58, 16);
            this.label3.TabIndex = 19;
            this.label3.Text = "Number:";
            // 
            // runningTimeField
            // 
            this.runningTimeField.Location = new System.Drawing.Point(146, 102);
            this.runningTimeField.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.runningTimeField.Name = "runningTimeField";
            this.runningTimeField.Size = new System.Drawing.Size(328, 22);
            this.runningTimeField.TabIndex = 20;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(14, 105);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(93, 16);
            this.label4.TabIndex = 21;
            this.label4.Text = "Running Time:";
            // 
            // yearField
            // 
            this.yearField.Location = new System.Drawing.Point(146, 130);
            this.yearField.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.yearField.Name = "yearField";
            this.yearField.Size = new System.Drawing.Size(328, 22);
            this.yearField.TabIndex = 22;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(14, 134);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(39, 16);
            this.label6.TabIndex = 23;
            this.label6.Text = "Year:";
            // 
            // tagsGroup
            // 
            this.tagsGroup.Controls.Add(this.clearFilterButton);
            this.tagsGroup.Controls.Add(this.applyFilterButton);
            this.tagsGroup.Controls.Add(this.label8);
            this.tagsGroup.Controls.Add(this.filterTagField);
            this.tagsGroup.Controls.Add(this.addNewTagButton);
            this.tagsGroup.Controls.Add(this.tagsList);
            this.tagsGroup.Location = new System.Drawing.Point(17, 169);
            this.tagsGroup.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tagsGroup.Name = "tagsGroup";
            this.tagsGroup.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tagsGroup.Size = new System.Drawing.Size(457, 240);
            this.tagsGroup.TabIndex = 24;
            this.tagsGroup.TabStop = false;
            this.tagsGroup.Text = "Tags";
            // 
            // clearFilterButton
            // 
            this.clearFilterButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.clearFilterButton.Location = new System.Drawing.Point(376, 162);
            this.clearFilterButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.clearFilterButton.Name = "clearFilterButton";
            this.clearFilterButton.Size = new System.Drawing.Size(75, 28);
            this.clearFilterButton.TabIndex = 29;
            this.clearFilterButton.Text = "Clear";
            this.clearFilterButton.UseVisualStyleBackColor = true;
            // 
            // applyFilterButton
            // 
            this.applyFilterButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.applyFilterButton.Location = new System.Drawing.Point(295, 162);
            this.applyFilterButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.applyFilterButton.Name = "applyFilterButton";
            this.applyFilterButton.Size = new System.Drawing.Size(75, 28);
            this.applyFilterButton.TabIndex = 28;
            this.applyFilterButton.Text = "Apply";
            this.applyFilterButton.UseVisualStyleBackColor = true;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(6, 168);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(39, 16);
            this.label8.TabIndex = 27;
            this.label8.Text = "Filter:";
            // 
            // filterTagField
            // 
            this.filterTagField.Location = new System.Drawing.Point(55, 165);
            this.filterTagField.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.filterTagField.Name = "filterTagField";
            this.filterTagField.Size = new System.Drawing.Size(234, 22);
            this.filterTagField.TabIndex = 25;
            // 
            // addNewTagButton
            // 
            this.addNewTagButton.Location = new System.Drawing.Point(356, 205);
            this.addNewTagButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.addNewTagButton.Name = "addNewTagButton";
            this.addNewTagButton.Size = new System.Drawing.Size(95, 27);
            this.addNewTagButton.TabIndex = 11;
            this.addNewTagButton.Text = "Add New";
            this.addNewTagButton.UseVisualStyleBackColor = true;
            // 
            // tagsList
            // 
            this.tagsList.FormattingEnabled = true;
            this.tagsList.Location = new System.Drawing.Point(6, 21);
            this.tagsList.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tagsList.Name = "tagsList";
            this.tagsList.Size = new System.Drawing.Size(445, 140);
            this.tagsList.TabIndex = 0;
            // 
            // imageFilePathField
            // 
            this.imageFilePathField.Location = new System.Drawing.Point(98, 425);
            this.imageFilePathField.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.imageFilePathField.Name = "imageFilePathField";
            this.imageFilePathField.Size = new System.Drawing.Size(269, 22);
            this.imageFilePathField.TabIndex = 25;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(20, 425);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(68, 16);
            this.label7.TabIndex = 26;
            this.label7.Text = "Image file:";
            // 
            // browseImageButton
            // 
            this.browseImageButton.Location = new System.Drawing.Point(373, 423);
            this.browseImageButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.browseImageButton.Name = "browseImageButton";
            this.browseImageButton.Size = new System.Drawing.Size(95, 27);
            this.browseImageButton.TabIndex = 26;
            this.browseImageButton.Text = "Browse";
            this.browseImageButton.UseVisualStyleBackColor = true;
            // 
            // AddNewMediaItemForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(496, 624);
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
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.MaximizeBox = false;
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
        private System.Windows.Forms.TextBox filterTagField;
        private System.Windows.Forms.TextBox imageFilePathField;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button browseImageButton;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button applyFilterButton;
        private System.Windows.Forms.Button clearFilterButton;
    }
}