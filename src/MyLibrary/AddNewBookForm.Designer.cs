
namespace MyLibrary
{
    partial class AddNewBookForm
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
            this.titleField = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.longTitleField = new System.Windows.Forms.TextBox();
            this.IsbnField = new System.Windows.Forms.TextBox();
            this.Isbn13Field = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.saveButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.authorsList = new System.Windows.Forms.CheckedListBox();
            this.addNewAuthorButton = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.publishersList = new System.Windows.Forms.ListBox();
            this.addNewPublisherButton = new System.Windows.Forms.Button();
            this.notesField = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.datePublishedField = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.editionField = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.deweyDecimalField = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.formatField = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.dimensionsField = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.overviewField = new System.Windows.Forms.TextBox();
            this.MsrpField = new System.Windows.Forms.TextBox();
            this.pagesField = new System.Windows.Forms.TextBox();
            this.synopsisField = new System.Windows.Forms.TextBox();
            this.excerptField = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.tagsGroup = new System.Windows.Forms.GroupBox();
            this.addNewTagButton = new System.Windows.Forms.Button();
            this.tagsList = new System.Windows.Forms.CheckedListBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.tagsGroup.SuspendLayout();
            this.SuspendLayout();
            // 
            // titleField
            // 
            this.titleField.Location = new System.Drawing.Point(121, 12);
            this.titleField.Name = "titleField";
            this.titleField.Size = new System.Drawing.Size(667, 22);
            this.titleField.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(49, 21);
            this.label1.TabIndex = 1;
            this.label1.Text = "Title:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 43);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(75, 17);
            this.label2.TabIndex = 2;
            this.label2.Text = "Long Title:";
            // 
            // longTitleField
            // 
            this.longTitleField.Location = new System.Drawing.Point(121, 40);
            this.longTitleField.Name = "longTitleField";
            this.longTitleField.Size = new System.Drawing.Size(667, 22);
            this.longTitleField.TabIndex = 3;
            // 
            // IsbnField
            // 
            this.IsbnField.Location = new System.Drawing.Point(121, 81);
            this.IsbnField.Name = "IsbnField";
            this.IsbnField.Size = new System.Drawing.Size(667, 22);
            this.IsbnField.TabIndex = 4;
            // 
            // Isbn13Field
            // 
            this.Isbn13Field.Location = new System.Drawing.Point(121, 109);
            this.Isbn13Field.Name = "Isbn13Field";
            this.Isbn13Field.Size = new System.Drawing.Size(667, 22);
            this.Isbn13Field.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 84);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(54, 21);
            this.label3.TabIndex = 6;
            this.label3.Text = "ISBN:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 112);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(74, 21);
            this.label4.TabIndex = 7;
            this.label4.Text = "ISBN13:";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.addNewAuthorButton);
            this.groupBox1.Controls.Add(this.authorsList);
            this.groupBox1.Location = new System.Drawing.Point(12, 146);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(386, 124);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Authors";
            // 
            // saveButton
            // 
            this.saveButton.Location = new System.Drawing.Point(12, 710);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(150, 27);
            this.saveButton.TabIndex = 9;
            this.saveButton.Text = "Save";
            this.saveButton.UseVisualStyleBackColor = true;
            // 
            // cancelButton
            // 
            this.cancelButton.Location = new System.Drawing.Point(638, 710);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(150, 27);
            this.cancelButton.TabIndex = 10;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // authorsList
            // 
            this.authorsList.FormattingEnabled = true;
            this.authorsList.Location = new System.Drawing.Point(6, 21);
            this.authorsList.Name = "authorsList";
            this.authorsList.Size = new System.Drawing.Size(290, 89);
            this.authorsList.TabIndex = 0;
            // 
            // addNewAuthorButton
            // 
            this.addNewAuthorButton.Location = new System.Drawing.Point(302, 21);
            this.addNewAuthorButton.Name = "addNewAuthorButton";
            this.addNewAuthorButton.Size = new System.Drawing.Size(78, 27);
            this.addNewAuthorButton.TabIndex = 11;
            this.addNewAuthorButton.Text = "Add New";
            this.addNewAuthorButton.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.addNewPublisherButton);
            this.groupBox2.Controls.Add(this.publishersList);
            this.groupBox2.Location = new System.Drawing.Point(404, 146);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(384, 124);
            this.groupBox2.TabIndex = 11;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Publisher";
            // 
            // publishersList
            // 
            this.publishersList.FormattingEnabled = true;
            this.publishersList.ItemHeight = 16;
            this.publishersList.Location = new System.Drawing.Point(6, 21);
            this.publishersList.Name = "publishersList";
            this.publishersList.Size = new System.Drawing.Size(284, 84);
            this.publishersList.TabIndex = 0;
            // 
            // addNewPublisherButton
            // 
            this.addNewPublisherButton.Location = new System.Drawing.Point(296, 21);
            this.addNewPublisherButton.Name = "addNewPublisherButton";
            this.addNewPublisherButton.Size = new System.Drawing.Size(78, 27);
            this.addNewPublisherButton.TabIndex = 12;
            this.addNewPublisherButton.Text = "Add New";
            this.addNewPublisherButton.UseVisualStyleBackColor = true;
            // 
            // notesField
            // 
            this.notesField.Location = new System.Drawing.Point(15, 581);
            this.notesField.Multiline = true;
            this.notesField.Name = "notesField";
            this.notesField.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.notesField.Size = new System.Drawing.Size(773, 115);
            this.notesField.TabIndex = 12;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 561);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(45, 17);
            this.label5.TabIndex = 13;
            this.label5.Text = "Notes";
            // 
            // datePublishedField
            // 
            this.datePublishedField.Location = new System.Drawing.Point(548, 406);
            this.datePublishedField.Name = "datePublishedField";
            this.datePublishedField.Size = new System.Drawing.Size(240, 22);
            this.datePublishedField.TabIndex = 14;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(407, 409);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(108, 17);
            this.label6.TabIndex = 15;
            this.label6.Text = "Date Published:";
            // 
            // editionField
            // 
            this.editionField.Location = new System.Drawing.Point(548, 437);
            this.editionField.Name = "editionField";
            this.editionField.Size = new System.Drawing.Size(240, 22);
            this.editionField.TabIndex = 16;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(408, 440);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(55, 17);
            this.label7.TabIndex = 17;
            this.label7.Text = "Edition:";
            // 
            // deweyDecimalField
            // 
            this.deweyDecimalField.Location = new System.Drawing.Point(548, 468);
            this.deweyDecimalField.Name = "deweyDecimalField";
            this.deweyDecimalField.Size = new System.Drawing.Size(240, 22);
            this.deweyDecimalField.TabIndex = 18;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(407, 471);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(108, 17);
            this.label8.TabIndex = 19;
            this.label8.Text = "Dewey Decimal:";
            // 
            // formatField
            // 
            this.formatField.Location = new System.Drawing.Point(548, 496);
            this.formatField.Name = "formatField";
            this.formatField.Size = new System.Drawing.Size(240, 22);
            this.formatField.TabIndex = 20;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(407, 499);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(56, 17);
            this.label9.TabIndex = 21;
            this.label9.Text = "Format:";
            // 
            // dimensionsField
            // 
            this.dimensionsField.Location = new System.Drawing.Point(548, 524);
            this.dimensionsField.Name = "dimensionsField";
            this.dimensionsField.Size = new System.Drawing.Size(240, 22);
            this.dimensionsField.TabIndex = 22;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(407, 527);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(85, 17);
            this.label10.TabIndex = 23;
            this.label10.Text = "Dimensions:";
            // 
            // overviewField
            // 
            this.overviewField.Location = new System.Drawing.Point(86, 406);
            this.overviewField.Name = "overviewField";
            this.overviewField.Size = new System.Drawing.Size(312, 22);
            this.overviewField.TabIndex = 24;
            // 
            // MsrpField
            // 
            this.MsrpField.Location = new System.Drawing.Point(86, 437);
            this.MsrpField.Name = "MsrpField";
            this.MsrpField.Size = new System.Drawing.Size(312, 22);
            this.MsrpField.TabIndex = 25;
            // 
            // pagesField
            // 
            this.pagesField.Location = new System.Drawing.Point(86, 468);
            this.pagesField.Name = "pagesField";
            this.pagesField.Size = new System.Drawing.Size(312, 22);
            this.pagesField.TabIndex = 26;
            // 
            // synopsisField
            // 
            this.synopsisField.Location = new System.Drawing.Point(86, 496);
            this.synopsisField.Name = "synopsisField";
            this.synopsisField.Size = new System.Drawing.Size(312, 22);
            this.synopsisField.TabIndex = 27;
            // 
            // excerptField
            // 
            this.excerptField.Location = new System.Drawing.Point(86, 524);
            this.excerptField.Name = "excerptField";
            this.excerptField.Size = new System.Drawing.Size(312, 22);
            this.excerptField.TabIndex = 28;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(9, 409);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(70, 17);
            this.label11.TabIndex = 29;
            this.label11.Text = "Overview:";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(12, 440);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(43, 17);
            this.label12.TabIndex = 30;
            this.label12.Text = "Msrp:";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(12, 471);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(52, 17);
            this.label13.TabIndex = 31;
            this.label13.Text = "Pages:";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(10, 499);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(69, 17);
            this.label14.TabIndex = 32;
            this.label14.Text = "Synopsis:";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(12, 527);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(59, 17);
            this.label15.TabIndex = 33;
            this.label15.Text = "Excerpt:";
            // 
            // tagsGroup
            // 
            this.tagsGroup.Controls.Add(this.addNewTagButton);
            this.tagsGroup.Controls.Add(this.tagsList);
            this.tagsGroup.Location = new System.Drawing.Point(208, 276);
            this.tagsGroup.Name = "tagsGroup";
            this.tagsGroup.Size = new System.Drawing.Size(386, 124);
            this.tagsGroup.TabIndex = 12;
            this.tagsGroup.TabStop = false;
            this.tagsGroup.Text = "Tags";
            // 
            // addNewTagButton
            // 
            this.addNewTagButton.Location = new System.Drawing.Point(302, 21);
            this.addNewTagButton.Name = "addNewTagButton";
            this.addNewTagButton.Size = new System.Drawing.Size(78, 27);
            this.addNewTagButton.TabIndex = 11;
            this.addNewTagButton.Text = "Add New";
            this.addNewTagButton.UseVisualStyleBackColor = true;
            // 
            // tagsList
            // 
            this.tagsList.FormattingEnabled = true;
            this.tagsList.Location = new System.Drawing.Point(6, 21);
            this.tagsList.Name = "tagsList";
            this.tagsList.Size = new System.Drawing.Size(290, 89);
            this.tagsList.TabIndex = 0;
            // 
            // AddNewBookForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 749);
            this.Controls.Add(this.tagsGroup);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.excerptField);
            this.Controls.Add(this.synopsisField);
            this.Controls.Add(this.pagesField);
            this.Controls.Add(this.MsrpField);
            this.Controls.Add(this.overviewField);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.dimensionsField);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.formatField);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.deweyDecimalField);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.editionField);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.datePublishedField);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.notesField);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.saveButton);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.Isbn13Field);
            this.Controls.Add(this.IsbnField);
            this.Controls.Add(this.longTitleField);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.titleField);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AddNewBookForm";
            this.Text = "Add New Book";
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.tagsGroup.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox titleField;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox longTitleField;
        private System.Windows.Forms.TextBox IsbnField;
        private System.Windows.Forms.TextBox Isbn13Field;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button saveButton;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.CheckedListBox authorsList;
        private System.Windows.Forms.Button addNewAuthorButton;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button addNewPublisherButton;
        private System.Windows.Forms.ListBox publishersList;
        private System.Windows.Forms.TextBox notesField;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox datePublishedField;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox editionField;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox deweyDecimalField;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox formatField;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox dimensionsField;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox overviewField;
        private System.Windows.Forms.TextBox MsrpField;
        private System.Windows.Forms.TextBox pagesField;
        private System.Windows.Forms.TextBox synopsisField;
        private System.Windows.Forms.TextBox excerptField;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.GroupBox tagsGroup;
        private System.Windows.Forms.Button addNewTagButton;
        private System.Windows.Forms.CheckedListBox tagsList;
    }
}