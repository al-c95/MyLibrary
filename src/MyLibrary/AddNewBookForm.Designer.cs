﻿
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AddNewBookForm));
            this.titleField = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.longTitleField = new System.Windows.Forms.TextBox();
            this.IsbnField = new System.Windows.Forms.TextBox();
            this.Isbn13Field = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.authorsGroup = new System.Windows.Forms.GroupBox();
            this.clearAuthorFilterButton = new System.Windows.Forms.Button();
            this.applyAuthorFilterButton = new System.Windows.Forms.Button();
            this.filterAuthorsField = new System.Windows.Forms.TextBox();
            this.label21 = new System.Windows.Forms.Label();
            this.addNewAuthorButton = new System.Windows.Forms.Button();
            this.authorsList = new System.Windows.Forms.CheckedListBox();
            this.saveButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.publishersGroup = new System.Windows.Forms.GroupBox();
            this.label20 = new System.Windows.Forms.Label();
            this.addNewPublisherButton = new System.Windows.Forms.Button();
            this.clearPublisherFilterButton = new System.Windows.Forms.Button();
            this.publishersList = new System.Windows.Forms.ListBox();
            this.applyPublisherFilterButton = new System.Windows.Forms.Button();
            this.filterPublishersField = new System.Windows.Forms.TextBox();
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
            this.v = new System.Windows.Forms.Label();
            this.tagsGroup = new System.Windows.Forms.GroupBox();
            this.clearTagFilterButton = new System.Windows.Forms.Button();
            this.addNewTagButton = new System.Windows.Forms.Button();
            this.applyTagFilterButton = new System.Windows.Forms.Button();
            this.tagsList = new System.Windows.Forms.CheckedListBox();
            this.filterTagField = new System.Windows.Forms.TextBox();
            this.label16 = new System.Windows.Forms.Label();
            this.languageField = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.browseImageButton = new System.Windows.Forms.Button();
            this.label18 = new System.Windows.Forms.Label();
            this.imageFilePathField = new System.Windows.Forms.TextBox();
            this.placeOfPublicationField = new System.Windows.Forms.TextBox();
            this.label19 = new System.Windows.Forms.Label();
            this.authorsGroup.SuspendLayout();
            this.publishersGroup.SuspendLayout();
            this.tagsGroup.SuspendLayout();
            this.SuspendLayout();
            // 
            // titleField
            // 
            this.titleField.Location = new System.Drawing.Point(87, 12);
            this.titleField.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.titleField.Name = "titleField";
            this.titleField.Size = new System.Drawing.Size(304, 22);
            this.titleField.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(36, 16);
            this.label1.TabIndex = 1;
            this.label1.Text = "Title:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 43);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(69, 16);
            this.label2.TabIndex = 2;
            this.label2.Text = "Long Title:";
            // 
            // longTitleField
            // 
            this.longTitleField.Location = new System.Drawing.Point(87, 43);
            this.longTitleField.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.longTitleField.Name = "longTitleField";
            this.longTitleField.Size = new System.Drawing.Size(304, 22);
            this.longTitleField.TabIndex = 3;
            // 
            // IsbnField
            // 
            this.IsbnField.Location = new System.Drawing.Point(554, 12);
            this.IsbnField.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.IsbnField.Name = "IsbnField";
            this.IsbnField.Size = new System.Drawing.Size(302, 22);
            this.IsbnField.TabIndex = 4;
            // 
            // Isbn13Field
            // 
            this.Isbn13Field.Location = new System.Drawing.Point(554, 43);
            this.Isbn13Field.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Isbn13Field.Name = "Isbn13Field";
            this.Isbn13Field.Size = new System.Drawing.Size(302, 22);
            this.Isbn13Field.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(413, 15);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 16);
            this.label3.TabIndex = 6;
            this.label3.Text = "ISBN:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(413, 46);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(55, 16);
            this.label4.TabIndex = 7;
            this.label4.Text = "ISBN13:";
            // 
            // authorsGroup
            // 
            this.authorsGroup.Controls.Add(this.clearAuthorFilterButton);
            this.authorsGroup.Controls.Add(this.applyAuthorFilterButton);
            this.authorsGroup.Controls.Add(this.filterAuthorsField);
            this.authorsGroup.Controls.Add(this.label21);
            this.authorsGroup.Controls.Add(this.addNewAuthorButton);
            this.authorsGroup.Controls.Add(this.authorsList);
            this.authorsGroup.Location = new System.Drawing.Point(15, 69);
            this.authorsGroup.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.authorsGroup.Name = "authorsGroup";
            this.authorsGroup.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.authorsGroup.Size = new System.Drawing.Size(380, 217);
            this.authorsGroup.TabIndex = 8;
            this.authorsGroup.TabStop = false;
            this.authorsGroup.Text = "Authors";
            // 
            // clearAuthorFilterButton
            // 
            this.clearAuthorFilterButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.clearAuthorFilterButton.Location = new System.Drawing.Point(299, 148);
            this.clearAuthorFilterButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.clearAuthorFilterButton.Name = "clearAuthorFilterButton";
            this.clearAuthorFilterButton.Size = new System.Drawing.Size(75, 28);
            this.clearAuthorFilterButton.TabIndex = 43;
            this.clearAuthorFilterButton.Text = "Clear";
            this.clearAuthorFilterButton.UseVisualStyleBackColor = true;
            // 
            // applyAuthorFilterButton
            // 
            this.applyAuthorFilterButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.applyAuthorFilterButton.Location = new System.Drawing.Point(218, 148);
            this.applyAuthorFilterButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.applyAuthorFilterButton.Name = "applyAuthorFilterButton";
            this.applyAuthorFilterButton.Size = new System.Drawing.Size(75, 28);
            this.applyAuthorFilterButton.TabIndex = 42;
            this.applyAuthorFilterButton.Text = "Apply";
            this.applyAuthorFilterButton.UseVisualStyleBackColor = true;
            // 
            // filterAuthorsField
            // 
            this.filterAuthorsField.Location = new System.Drawing.Point(57, 151);
            this.filterAuthorsField.Name = "filterAuthorsField";
            this.filterAuthorsField.Size = new System.Drawing.Size(155, 22);
            this.filterAuthorsField.TabIndex = 41;
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(8, 154);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(39, 16);
            this.label21.TabIndex = 41;
            this.label21.Text = "Filter:";
            // 
            // addNewAuthorButton
            // 
            this.addNewAuthorButton.Location = new System.Drawing.Point(278, 186);
            this.addNewAuthorButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.addNewAuthorButton.Name = "addNewAuthorButton";
            this.addNewAuthorButton.Size = new System.Drawing.Size(96, 27);
            this.addNewAuthorButton.TabIndex = 11;
            this.addNewAuthorButton.Text = "Add New";
            this.addNewAuthorButton.UseVisualStyleBackColor = true;
            // 
            // authorsList
            // 
            this.authorsList.FormattingEnabled = true;
            this.authorsList.Location = new System.Drawing.Point(5, 21);
            this.authorsList.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.authorsList.Name = "authorsList";
            this.authorsList.Size = new System.Drawing.Size(369, 123);
            this.authorsList.TabIndex = 0;
            // 
            // saveButton
            // 
            this.saveButton.Image = ((System.Drawing.Image)(resources.GetObject("saveButton.Image")));
            this.saveButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.saveButton.Location = new System.Drawing.Point(12, 645);
            this.saveButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(149, 27);
            this.saveButton.TabIndex = 9;
            this.saveButton.Text = "Save";
            this.saveButton.UseVisualStyleBackColor = true;
            // 
            // cancelButton
            // 
            this.cancelButton.Image = ((System.Drawing.Image)(resources.GetObject("cancelButton.Image")));
            this.cancelButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.cancelButton.Location = new System.Drawing.Point(708, 645);
            this.cancelButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(149, 27);
            this.cancelButton.TabIndex = 10;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // publishersGroup
            // 
            this.publishersGroup.Controls.Add(this.label20);
            this.publishersGroup.Controls.Add(this.addNewPublisherButton);
            this.publishersGroup.Controls.Add(this.clearPublisherFilterButton);
            this.publishersGroup.Controls.Add(this.publishersList);
            this.publishersGroup.Controls.Add(this.applyPublisherFilterButton);
            this.publishersGroup.Controls.Add(this.filterPublishersField);
            this.publishersGroup.Location = new System.Drawing.Point(473, 481);
            this.publishersGroup.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.publishersGroup.Name = "publishersGroup";
            this.publishersGroup.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.publishersGroup.Size = new System.Drawing.Size(384, 150);
            this.publishersGroup.TabIndex = 11;
            this.publishersGroup.TabStop = false;
            this.publishersGroup.Text = "Publisher";
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(8, 124);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(39, 16);
            this.label20.TabIndex = 39;
            this.label20.Text = "Filter:";
            // 
            // addNewPublisherButton
            // 
            this.addNewPublisherButton.Location = new System.Drawing.Point(282, 21);
            this.addNewPublisherButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.addNewPublisherButton.Name = "addNewPublisherButton";
            this.addNewPublisherButton.Size = new System.Drawing.Size(96, 27);
            this.addNewPublisherButton.TabIndex = 12;
            this.addNewPublisherButton.Text = "Add New";
            this.addNewPublisherButton.UseVisualStyleBackColor = true;
            // 
            // clearPublisherFilterButton
            // 
            this.clearPublisherFilterButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.clearPublisherFilterButton.Location = new System.Drawing.Point(303, 118);
            this.clearPublisherFilterButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.clearPublisherFilterButton.Name = "clearPublisherFilterButton";
            this.clearPublisherFilterButton.Size = new System.Drawing.Size(75, 28);
            this.clearPublisherFilterButton.TabIndex = 38;
            this.clearPublisherFilterButton.Text = "Clear";
            this.clearPublisherFilterButton.UseVisualStyleBackColor = true;
            // 
            // publishersList
            // 
            this.publishersList.FormattingEnabled = true;
            this.publishersList.ItemHeight = 16;
            this.publishersList.Location = new System.Drawing.Point(5, 21);
            this.publishersList.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.publishersList.Name = "publishersList";
            this.publishersList.Size = new System.Drawing.Size(271, 84);
            this.publishersList.TabIndex = 0;
            // 
            // applyPublisherFilterButton
            // 
            this.applyPublisherFilterButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.applyPublisherFilterButton.Location = new System.Drawing.Point(222, 118);
            this.applyPublisherFilterButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.applyPublisherFilterButton.Name = "applyPublisherFilterButton";
            this.applyPublisherFilterButton.Size = new System.Drawing.Size(75, 28);
            this.applyPublisherFilterButton.TabIndex = 37;
            this.applyPublisherFilterButton.Text = "Apply";
            this.applyPublisherFilterButton.UseVisualStyleBackColor = true;
            // 
            // filterPublishersField
            // 
            this.filterPublishersField.Location = new System.Drawing.Point(57, 121);
            this.filterPublishersField.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.filterPublishersField.Name = "filterPublishersField";
            this.filterPublishersField.Size = new System.Drawing.Size(159, 22);
            this.filterPublishersField.TabIndex = 36;
            // 
            // notesField
            // 
            this.notesField.Location = new System.Drawing.Point(12, 537);
            this.notesField.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.notesField.Multiline = true;
            this.notesField.Name = "notesField";
            this.notesField.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.notesField.Size = new System.Drawing.Size(437, 94);
            this.notesField.TabIndex = 12;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(17, 518);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(43, 16);
            this.label5.TabIndex = 13;
            this.label5.Text = "Notes";
            // 
            // datePublishedField
            // 
            this.datePublishedField.Location = new System.Drawing.Point(555, 292);
            this.datePublishedField.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.datePublishedField.Name = "datePublishedField";
            this.datePublishedField.Size = new System.Drawing.Size(302, 22);
            this.datePublishedField.TabIndex = 14;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(414, 296);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(102, 16);
            this.label6.TabIndex = 15;
            this.label6.Text = "Date Published:";
            // 
            // editionField
            // 
            this.editionField.Location = new System.Drawing.Point(555, 355);
            this.editionField.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.editionField.Name = "editionField";
            this.editionField.Size = new System.Drawing.Size(302, 22);
            this.editionField.TabIndex = 16;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(414, 358);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(51, 16);
            this.label7.TabIndex = 17;
            this.label7.Text = "Edition:";
            // 
            // deweyDecimalField
            // 
            this.deweyDecimalField.Location = new System.Drawing.Point(555, 385);
            this.deweyDecimalField.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.deweyDecimalField.Name = "deweyDecimalField";
            this.deweyDecimalField.Size = new System.Drawing.Size(302, 22);
            this.deweyDecimalField.TabIndex = 18;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(414, 386);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(105, 16);
            this.label8.TabIndex = 19;
            this.label8.Text = "Dewey Decimal:";
            // 
            // formatField
            // 
            this.formatField.Location = new System.Drawing.Point(555, 414);
            this.formatField.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.formatField.Name = "formatField";
            this.formatField.Size = new System.Drawing.Size(302, 22);
            this.formatField.TabIndex = 20;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(414, 417);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(52, 16);
            this.label9.TabIndex = 21;
            this.label9.Text = "Format:";
            // 
            // dimensionsField
            // 
            this.dimensionsField.Location = new System.Drawing.Point(555, 445);
            this.dimensionsField.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dimensionsField.Name = "dimensionsField";
            this.dimensionsField.Size = new System.Drawing.Size(302, 22);
            this.dimensionsField.TabIndex = 22;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(414, 448);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(81, 16);
            this.label10.TabIndex = 23;
            this.label10.Text = "Dimensions:";
            // 
            // overviewField
            // 
            this.overviewField.Location = new System.Drawing.Point(91, 292);
            this.overviewField.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.overviewField.Name = "overviewField";
            this.overviewField.Size = new System.Drawing.Size(304, 22);
            this.overviewField.TabIndex = 24;
            // 
            // MsrpField
            // 
            this.MsrpField.Location = new System.Drawing.Point(91, 323);
            this.MsrpField.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.MsrpField.Name = "MsrpField";
            this.MsrpField.Size = new System.Drawing.Size(304, 22);
            this.MsrpField.TabIndex = 25;
            // 
            // pagesField
            // 
            this.pagesField.Location = new System.Drawing.Point(91, 355);
            this.pagesField.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.pagesField.Name = "pagesField";
            this.pagesField.Size = new System.Drawing.Size(304, 22);
            this.pagesField.TabIndex = 26;
            // 
            // synopsisField
            // 
            this.synopsisField.Location = new System.Drawing.Point(91, 385);
            this.synopsisField.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.synopsisField.Name = "synopsisField";
            this.synopsisField.Size = new System.Drawing.Size(304, 22);
            this.synopsisField.TabIndex = 27;
            // 
            // excerptField
            // 
            this.excerptField.Location = new System.Drawing.Point(91, 416);
            this.excerptField.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.excerptField.Name = "excerptField";
            this.excerptField.Size = new System.Drawing.Size(304, 22);
            this.excerptField.TabIndex = 28;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(12, 295);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(66, 16);
            this.label11.TabIndex = 29;
            this.label11.Text = "Overview:";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(13, 326);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(49, 16);
            this.label12.TabIndex = 30;
            this.label12.Text = "MSRP:";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(12, 358);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(50, 16);
            this.label13.TabIndex = 31;
            this.label13.Text = "Pages:";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(12, 388);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(66, 16);
            this.label14.TabIndex = 32;
            this.label14.Text = "Synopsis:";
            // 
            // v
            // 
            this.v.AutoSize = true;
            this.v.Location = new System.Drawing.Point(12, 419);
            this.v.Name = "v";
            this.v.Size = new System.Drawing.Size(55, 16);
            this.v.TabIndex = 33;
            this.v.Text = "Excerpt:";
            // 
            // tagsGroup
            // 
            this.tagsGroup.Controls.Add(this.clearTagFilterButton);
            this.tagsGroup.Controls.Add(this.addNewTagButton);
            this.tagsGroup.Controls.Add(this.applyTagFilterButton);
            this.tagsGroup.Controls.Add(this.tagsList);
            this.tagsGroup.Controls.Add(this.filterTagField);
            this.tagsGroup.Controls.Add(this.label16);
            this.tagsGroup.Location = new System.Drawing.Point(403, 69);
            this.tagsGroup.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tagsGroup.Name = "tagsGroup";
            this.tagsGroup.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tagsGroup.Size = new System.Drawing.Size(454, 217);
            this.tagsGroup.TabIndex = 12;
            this.tagsGroup.TabStop = false;
            this.tagsGroup.Text = "Tags";
            // 
            // clearTagFilterButton
            // 
            this.clearTagFilterButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.clearTagFilterButton.Location = new System.Drawing.Point(373, 145);
            this.clearTagFilterButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.clearTagFilterButton.Name = "clearTagFilterButton";
            this.clearTagFilterButton.Size = new System.Drawing.Size(75, 28);
            this.clearTagFilterButton.TabIndex = 47;
            this.clearTagFilterButton.Text = "Clear";
            this.clearTagFilterButton.UseVisualStyleBackColor = true;
            // 
            // addNewTagButton
            // 
            this.addNewTagButton.Location = new System.Drawing.Point(352, 186);
            this.addNewTagButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.addNewTagButton.Name = "addNewTagButton";
            this.addNewTagButton.Size = new System.Drawing.Size(96, 27);
            this.addNewTagButton.TabIndex = 11;
            this.addNewTagButton.Text = "Add New";
            this.addNewTagButton.UseVisualStyleBackColor = true;
            // 
            // applyTagFilterButton
            // 
            this.applyTagFilterButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.applyTagFilterButton.Location = new System.Drawing.Point(292, 145);
            this.applyTagFilterButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.applyTagFilterButton.Name = "applyTagFilterButton";
            this.applyTagFilterButton.Size = new System.Drawing.Size(75, 28);
            this.applyTagFilterButton.TabIndex = 46;
            this.applyTagFilterButton.Text = "Apply";
            this.applyTagFilterButton.UseVisualStyleBackColor = true;
            // 
            // tagsList
            // 
            this.tagsList.FormattingEnabled = true;
            this.tagsList.Location = new System.Drawing.Point(5, 21);
            this.tagsList.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tagsList.Name = "tagsList";
            this.tagsList.Size = new System.Drawing.Size(443, 123);
            this.tagsList.TabIndex = 0;
            // 
            // filterTagField
            // 
            this.filterTagField.Location = new System.Drawing.Point(59, 149);
            this.filterTagField.Name = "filterTagField";
            this.filterTagField.Size = new System.Drawing.Size(227, 22);
            this.filterTagField.TabIndex = 44;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(11, 151);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(39, 16);
            this.label16.TabIndex = 45;
            this.label16.Text = "Filter:";
            // 
            // languageField
            // 
            this.languageField.Location = new System.Drawing.Point(91, 446);
            this.languageField.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.languageField.Name = "languageField";
            this.languageField.Size = new System.Drawing.Size(304, 22);
            this.languageField.TabIndex = 34;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(12, 448);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(71, 16);
            this.label15.TabIndex = 35;
            this.label15.Text = "Language:";
            // 
            // browseImageButton
            // 
            this.browseImageButton.Location = new System.Drawing.Point(354, 481);
            this.browseImageButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.browseImageButton.Name = "browseImageButton";
            this.browseImageButton.Size = new System.Drawing.Size(95, 27);
            this.browseImageButton.TabIndex = 38;
            this.browseImageButton.Text = "Browse";
            this.browseImageButton.UseVisualStyleBackColor = true;
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(13, 486);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(68, 16);
            this.label18.TabIndex = 37;
            this.label18.Text = "Image file:";
            // 
            // imageFilePathField
            // 
            this.imageFilePathField.Location = new System.Drawing.Point(91, 483);
            this.imageFilePathField.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.imageFilePathField.Name = "imageFilePathField";
            this.imageFilePathField.Size = new System.Drawing.Size(257, 22);
            this.imageFilePathField.TabIndex = 36;
            // 
            // placeOfPublicationField
            // 
            this.placeOfPublicationField.Location = new System.Drawing.Point(555, 323);
            this.placeOfPublicationField.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.placeOfPublicationField.Name = "placeOfPublicationField";
            this.placeOfPublicationField.Size = new System.Drawing.Size(302, 22);
            this.placeOfPublicationField.TabIndex = 39;
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(414, 327);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(128, 16);
            this.label19.TabIndex = 40;
            this.label19.Text = "Place of Publication:";
            // 
            // AddNewBookForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(869, 683);
            this.Controls.Add(this.label19);
            this.Controls.Add(this.placeOfPublicationField);
            this.Controls.Add(this.browseImageButton);
            this.Controls.Add(this.label18);
            this.Controls.Add(this.imageFilePathField);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.languageField);
            this.Controls.Add(this.tagsGroup);
            this.Controls.Add(this.v);
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
            this.Controls.Add(this.publishersGroup);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.saveButton);
            this.Controls.Add(this.authorsGroup);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.Isbn13Field);
            this.Controls.Add(this.IsbnField);
            this.Controls.Add(this.longTitleField);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.titleField);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.MaximizeBox = false;
            this.Name = "AddNewBookForm";
            this.Text = "Add New Book";
            this.authorsGroup.ResumeLayout(false);
            this.authorsGroup.PerformLayout();
            this.publishersGroup.ResumeLayout(false);
            this.publishersGroup.PerformLayout();
            this.tagsGroup.ResumeLayout(false);
            this.tagsGroup.PerformLayout();
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
        private System.Windows.Forms.GroupBox authorsGroup;
        private System.Windows.Forms.Button saveButton;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.CheckedListBox authorsList;
        private System.Windows.Forms.Button addNewAuthorButton;
        private System.Windows.Forms.GroupBox publishersGroup;
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
        private System.Windows.Forms.Label v;
        private System.Windows.Forms.GroupBox tagsGroup;
        private System.Windows.Forms.Button addNewTagButton;
        private System.Windows.Forms.CheckedListBox tagsList;
        private System.Windows.Forms.TextBox languageField;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.TextBox filterPublishersField;
        private System.Windows.Forms.Button browseImageButton;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.TextBox imageFilePathField;
        private System.Windows.Forms.TextBox placeOfPublicationField;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Button applyPublisherFilterButton;
        private System.Windows.Forms.Button clearPublisherFilterButton;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Button clearAuthorFilterButton;
        private System.Windows.Forms.Button applyAuthorFilterButton;
        private System.Windows.Forms.TextBox filterAuthorsField;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Button clearTagFilterButton;
        private System.Windows.Forms.Button applyTagFilterButton;
        private System.Windows.Forms.TextBox filterTagField;
        private System.Windows.Forms.Label label16;
    }
}