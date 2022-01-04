﻿
namespace MyLibrary
{
    partial class MainWindow
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainWindow));
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.fileMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.newBookToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newMediaItemToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.exitMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.viewMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.databaseStatisticsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.settingsMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.helpMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addButton = new System.Windows.Forms.Button();
            this.deleteSelectedButton = new System.Windows.Forms.Button();
            this.tagsButton = new System.Windows.Forms.Button();
            this.wishListButton = new System.Windows.Forms.Button();
            this.dataGrid = new System.Windows.Forms.DataGridView();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.statusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.itemsDisplayedLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.label1 = new System.Windows.Forms.Label();
            this.filterGroup = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.tagsList = new System.Windows.Forms.CheckedListBox();
            this.saveFilterButton = new System.Windows.Forms.Button();
            this.clearFilterButton = new System.Windows.Forms.Button();
            this.applyFilterButton = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.titleFilterField = new System.Windows.Forms.TextBox();
            this.detailsGroup = new System.Windows.Forms.GroupBox();
            this.detailsBox = new System.Windows.Forms.RichTextBox();
            this.manageItemTagsButton = new System.Windows.Forms.Button();
            this.removeImageButton = new System.Windows.Forms.Button();
            this.selectImageButton = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.textBoxNotes = new System.Windows.Forms.TextBox();
            this.discardChangesButton = new System.Windows.Forms.Button();
            this.saveChangesButton = new System.Windows.Forms.Button();
            this.pictureBox = new System.Windows.Forms.PictureBox();
            this.categoryDropDown = new System.Windows.Forms.ComboBox();
            this.searchBooksButton = new System.Windows.Forms.Button();
            this.menuStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGrid)).BeginInit();
            this.statusStrip.SuspendLayout();
            this.filterGroup.SuspendLayout();
            this.detailsGroup.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip
            // 
            this.menuStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileMenu,
            this.editMenu,
            this.viewMenu,
            this.settingsMenu,
            this.helpMenu});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Padding = new System.Windows.Forms.Padding(4, 2, 0, 2);
            this.menuStrip.Size = new System.Drawing.Size(772, 28);
            this.menuStrip.TabIndex = 0;
            this.menuStrip.Text = "menuStrip1";
            // 
            // fileMenu
            // 
            this.fileMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newBookToolStripMenuItem,
            this.newMediaItemToolStripMenuItem,
            this.toolStripSeparator1,
            this.exitMenuItem});
            this.fileMenu.Name = "fileMenu";
            this.fileMenu.Size = new System.Drawing.Size(46, 24);
            this.fileMenu.Text = "File";
            // 
            // newBookToolStripMenuItem
            // 
            this.newBookToolStripMenuItem.Name = "newBookToolStripMenuItem";
            this.newBookToolStripMenuItem.Size = new System.Drawing.Size(202, 26);
            this.newBookToolStripMenuItem.Text = "New Book";
            // 
            // newMediaItemToolStripMenuItem
            // 
            this.newMediaItemToolStripMenuItem.Name = "newMediaItemToolStripMenuItem";
            this.newMediaItemToolStripMenuItem.Size = new System.Drawing.Size(202, 26);
            this.newMediaItemToolStripMenuItem.Text = "New Media Item";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(199, 6);
            // 
            // exitMenuItem
            // 
            this.exitMenuItem.Name = "exitMenuItem";
            this.exitMenuItem.Size = new System.Drawing.Size(202, 26);
            this.exitMenuItem.Text = "Exit";
            // 
            // editMenu
            // 
            this.editMenu.Enabled = false;
            this.editMenu.Name = "editMenu";
            this.editMenu.Size = new System.Drawing.Size(49, 24);
            this.editMenu.Text = "Edit";
            // 
            // viewMenu
            // 
            this.viewMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.databaseStatisticsToolStripMenuItem});
            this.viewMenu.Name = "viewMenu";
            this.viewMenu.Size = new System.Drawing.Size(55, 24);
            this.viewMenu.Text = "View";
            // 
            // databaseStatisticsToolStripMenuItem
            // 
            this.databaseStatisticsToolStripMenuItem.Name = "databaseStatisticsToolStripMenuItem";
            this.databaseStatisticsToolStripMenuItem.Size = new System.Drawing.Size(215, 26);
            this.databaseStatisticsToolStripMenuItem.Text = "Database statistics";
            // 
            // settingsMenu
            // 
            this.settingsMenu.Enabled = false;
            this.settingsMenu.Name = "settingsMenu";
            this.settingsMenu.Size = new System.Drawing.Size(76, 24);
            this.settingsMenu.Text = "Settings";
            // 
            // helpMenu
            // 
            this.helpMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem});
            this.helpMenu.Name = "helpMenu";
            this.helpMenu.Size = new System.Drawing.Size(55, 24);
            this.helpMenu.Text = "Help";
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(133, 26);
            this.aboutToolStripMenuItem.Text = "About";
            // 
            // addButton
            // 
            this.addButton.Location = new System.Drawing.Point(9, 25);
            this.addButton.Margin = new System.Windows.Forms.Padding(2);
            this.addButton.Name = "addButton";
            this.addButton.Size = new System.Drawing.Size(88, 28);
            this.addButton.TabIndex = 1;
            this.addButton.Text = "Add";
            this.addButton.UseVisualStyleBackColor = true;
            // 
            // deleteSelectedButton
            // 
            this.deleteSelectedButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F);
            this.deleteSelectedButton.Location = new System.Drawing.Point(195, 25);
            this.deleteSelectedButton.Margin = new System.Windows.Forms.Padding(2);
            this.deleteSelectedButton.Name = "deleteSelectedButton";
            this.deleteSelectedButton.Size = new System.Drawing.Size(89, 28);
            this.deleteSelectedButton.TabIndex = 2;
            this.deleteSelectedButton.Text = "Delete";
            this.deleteSelectedButton.UseVisualStyleBackColor = true;
            // 
            // tagsButton
            // 
            this.tagsButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.tagsButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F);
            this.tagsButton.Location = new System.Drawing.Point(616, 25);
            this.tagsButton.Margin = new System.Windows.Forms.Padding(2);
            this.tagsButton.Name = "tagsButton";
            this.tagsButton.Size = new System.Drawing.Size(71, 28);
            this.tagsButton.TabIndex = 5;
            this.tagsButton.Text = "Tags";
            this.tagsButton.UseVisualStyleBackColor = true;
            // 
            // wishListButton
            // 
            this.wishListButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.wishListButton.Enabled = false;
            this.wishListButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F);
            this.wishListButton.Location = new System.Drawing.Point(692, 25);
            this.wishListButton.Margin = new System.Windows.Forms.Padding(2);
            this.wishListButton.Name = "wishListButton";
            this.wishListButton.Size = new System.Drawing.Size(71, 28);
            this.wishListButton.TabIndex = 6;
            this.wishListButton.Text = "Wishlist";
            this.wishListButton.UseVisualStyleBackColor = true;
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
            this.dataGrid.Location = new System.Drawing.Point(338, 58);
            this.dataGrid.Margin = new System.Windows.Forms.Padding(2);
            this.dataGrid.MultiSelect = false;
            this.dataGrid.Name = "dataGrid";
            this.dataGrid.ReadOnly = true;
            this.dataGrid.RowHeadersWidth = 51;
            this.dataGrid.RowTemplate.Height = 24;
            this.dataGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGrid.Size = new System.Drawing.Size(424, 622);
            this.dataGrid.TabIndex = 7;
            // 
            // statusStrip
            // 
            this.statusStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statusLabel,
            this.itemsDisplayedLabel});
            this.statusStrip.Location = new System.Drawing.Point(0, 686);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Padding = new System.Windows.Forms.Padding(1, 0, 10, 0);
            this.statusStrip.Size = new System.Drawing.Size(772, 26);
            this.statusStrip.TabIndex = 8;
            this.statusStrip.Text = "statusStrip1";
            // 
            // statusLabel
            // 
            this.statusLabel.Name = "statusLabel";
            this.statusLabel.Size = new System.Drawing.Size(53, 20);
            this.statusLabel.Text = "Ready.";
            // 
            // itemsDisplayedLabel
            // 
            this.itemsDisplayedLabel.Name = "itemsDisplayedLabel";
            this.itemsDisplayedLabel.Size = new System.Drawing.Size(269, 20);
            this.itemsDisplayedLabel.Text = "x items selected. y of z items displayed.";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 61);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(58, 15);
            this.label1.TabIndex = 10;
            this.label1.Text = "Category:";
            // 
            // filterGroup
            // 
            this.filterGroup.Controls.Add(this.label4);
            this.filterGroup.Controls.Add(this.tagsList);
            this.filterGroup.Controls.Add(this.saveFilterButton);
            this.filterGroup.Controls.Add(this.clearFilterButton);
            this.filterGroup.Controls.Add(this.applyFilterButton);
            this.filterGroup.Controls.Add(this.label2);
            this.filterGroup.Controls.Add(this.titleFilterField);
            this.filterGroup.Location = new System.Drawing.Point(11, 83);
            this.filterGroup.Margin = new System.Windows.Forms.Padding(2);
            this.filterGroup.Name = "filterGroup";
            this.filterGroup.Padding = new System.Windows.Forms.Padding(2);
            this.filterGroup.Size = new System.Drawing.Size(322, 171);
            this.filterGroup.TabIndex = 11;
            this.filterGroup.TabStop = false;
            this.filterGroup.Text = "Filter";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(9, 50);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(37, 15);
            this.label4.TabIndex = 16;
            this.label4.Text = "Tags:";
            // 
            // tagsList
            // 
            this.tagsList.FormattingEnabled = true;
            this.tagsList.Location = new System.Drawing.Point(54, 50);
            this.tagsList.Margin = new System.Windows.Forms.Padding(2);
            this.tagsList.Name = "tagsList";
            this.tagsList.Size = new System.Drawing.Size(239, 79);
            this.tagsList.TabIndex = 16;
            // 
            // saveFilterButton
            // 
            this.saveFilterButton.Enabled = false;
            this.saveFilterButton.Location = new System.Drawing.Point(176, 144);
            this.saveFilterButton.Margin = new System.Windows.Forms.Padding(2);
            this.saveFilterButton.Name = "saveFilterButton";
            this.saveFilterButton.Size = new System.Drawing.Size(56, 23);
            this.saveFilterButton.TabIndex = 15;
            this.saveFilterButton.Text = "Save";
            this.saveFilterButton.UseVisualStyleBackColor = true;
            // 
            // clearFilterButton
            // 
            this.clearFilterButton.Location = new System.Drawing.Point(236, 144);
            this.clearFilterButton.Margin = new System.Windows.Forms.Padding(2);
            this.clearFilterButton.Name = "clearFilterButton";
            this.clearFilterButton.Size = new System.Drawing.Size(56, 23);
            this.clearFilterButton.TabIndex = 14;
            this.clearFilterButton.Text = "Clear";
            this.clearFilterButton.UseVisualStyleBackColor = true;
            // 
            // applyFilterButton
            // 
            this.applyFilterButton.Location = new System.Drawing.Point(4, 144);
            this.applyFilterButton.Margin = new System.Windows.Forms.Padding(2);
            this.applyFilterButton.Name = "applyFilterButton";
            this.applyFilterButton.Size = new System.Drawing.Size(56, 23);
            this.applyFilterButton.TabIndex = 13;
            this.applyFilterButton.Text = "Apply";
            this.applyFilterButton.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 20);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(33, 15);
            this.label2.TabIndex = 13;
            this.label2.Text = "Title:";
            // 
            // titleFilterField
            // 
            this.titleFilterField.Location = new System.Drawing.Point(54, 17);
            this.titleFilterField.Margin = new System.Windows.Forms.Padding(2);
            this.titleFilterField.Name = "titleFilterField";
            this.titleFilterField.Size = new System.Drawing.Size(239, 20);
            this.titleFilterField.TabIndex = 13;
            // 
            // detailsGroup
            // 
            this.detailsGroup.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.detailsGroup.Controls.Add(this.detailsBox);
            this.detailsGroup.Controls.Add(this.manageItemTagsButton);
            this.detailsGroup.Controls.Add(this.removeImageButton);
            this.detailsGroup.Controls.Add(this.selectImageButton);
            this.detailsGroup.Controls.Add(this.label3);
            this.detailsGroup.Controls.Add(this.textBoxNotes);
            this.detailsGroup.Controls.Add(this.discardChangesButton);
            this.detailsGroup.Controls.Add(this.saveChangesButton);
            this.detailsGroup.Controls.Add(this.pictureBox);
            this.detailsGroup.Location = new System.Drawing.Point(9, 259);
            this.detailsGroup.Margin = new System.Windows.Forms.Padding(2);
            this.detailsGroup.Name = "detailsGroup";
            this.detailsGroup.Padding = new System.Windows.Forms.Padding(2);
            this.detailsGroup.Size = new System.Drawing.Size(325, 422);
            this.detailsGroup.TabIndex = 12;
            this.detailsGroup.TabStop = false;
            this.detailsGroup.Text = "Item Details";
            // 
            // detailsBox
            // 
            this.detailsBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.detailsBox.Location = new System.Drawing.Point(7, 140);
            this.detailsBox.Margin = new System.Windows.Forms.Padding(2);
            this.detailsBox.Name = "detailsBox";
            this.detailsBox.ReadOnly = true;
            this.detailsBox.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.detailsBox.Size = new System.Drawing.Size(314, 159);
            this.detailsBox.TabIndex = 0;
            this.detailsBox.Text = "";
            // 
            // manageItemTagsButton
            // 
            this.manageItemTagsButton.Location = new System.Drawing.Point(106, 11);
            this.manageItemTagsButton.Margin = new System.Windows.Forms.Padding(2);
            this.manageItemTagsButton.Name = "manageItemTagsButton";
            this.manageItemTagsButton.Size = new System.Drawing.Size(116, 23);
            this.manageItemTagsButton.TabIndex = 20;
            this.manageItemTagsButton.Text = "Manage Tags";
            this.manageItemTagsButton.UseVisualStyleBackColor = true;
            // 
            // removeImageButton
            // 
            this.removeImageButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.removeImageButton.Location = new System.Drawing.Point(205, 112);
            this.removeImageButton.Margin = new System.Windows.Forms.Padding(2);
            this.removeImageButton.Name = "removeImageButton";
            this.removeImageButton.Size = new System.Drawing.Size(116, 23);
            this.removeImageButton.TabIndex = 19;
            this.removeImageButton.Text = "Remove Image";
            this.removeImageButton.UseVisualStyleBackColor = true;
            // 
            // selectImageButton
            // 
            this.selectImageButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.selectImageButton.Location = new System.Drawing.Point(7, 112);
            this.selectImageButton.Margin = new System.Windows.Forms.Padding(2);
            this.selectImageButton.Name = "selectImageButton";
            this.selectImageButton.Size = new System.Drawing.Size(116, 23);
            this.selectImageButton.TabIndex = 18;
            this.selectImageButton.Text = "Select Image";
            this.selectImageButton.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(7, 301);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(39, 15);
            this.label3.TabIndex = 17;
            this.label3.Text = "Notes";
            // 
            // textBoxNotes
            // 
            this.textBoxNotes.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.textBoxNotes.Location = new System.Drawing.Point(9, 317);
            this.textBoxNotes.Margin = new System.Windows.Forms.Padding(2);
            this.textBoxNotes.Multiline = true;
            this.textBoxNotes.Name = "textBoxNotes";
            this.textBoxNotes.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxNotes.Size = new System.Drawing.Size(312, 73);
            this.textBoxNotes.TabIndex = 16;
            // 
            // discardChangesButton
            // 
            this.discardChangesButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.discardChangesButton.Enabled = false;
            this.discardChangesButton.Location = new System.Drawing.Point(205, 394);
            this.discardChangesButton.Margin = new System.Windows.Forms.Padding(2);
            this.discardChangesButton.Name = "discardChangesButton";
            this.discardChangesButton.Size = new System.Drawing.Size(116, 23);
            this.discardChangesButton.TabIndex = 15;
            this.discardChangesButton.Text = "Discard Changes";
            this.discardChangesButton.UseVisualStyleBackColor = true;
            // 
            // saveChangesButton
            // 
            this.saveChangesButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.saveChangesButton.Enabled = false;
            this.saveChangesButton.Location = new System.Drawing.Point(9, 394);
            this.saveChangesButton.Margin = new System.Windows.Forms.Padding(2);
            this.saveChangesButton.Name = "saveChangesButton";
            this.saveChangesButton.Size = new System.Drawing.Size(116, 23);
            this.saveChangesButton.TabIndex = 14;
            this.saveChangesButton.Text = "Save Changes";
            this.saveChangesButton.UseVisualStyleBackColor = true;
            // 
            // pictureBox
            // 
            this.pictureBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.pictureBox.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox.Image")));
            this.pictureBox.Location = new System.Drawing.Point(7, 38);
            this.pictureBox.Margin = new System.Windows.Forms.Padding(2);
            this.pictureBox.Name = "pictureBox";
            this.pictureBox.Size = new System.Drawing.Size(314, 69);
            this.pictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox.TabIndex = 13;
            this.pictureBox.TabStop = false;
            // 
            // categoryDropDown
            // 
            this.categoryDropDown.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.categoryDropDown.FormattingEnabled = true;
            this.categoryDropDown.Location = new System.Drawing.Point(65, 58);
            this.categoryDropDown.Margin = new System.Windows.Forms.Padding(2);
            this.categoryDropDown.Name = "categoryDropDown";
            this.categoryDropDown.Size = new System.Drawing.Size(270, 21);
            this.categoryDropDown.TabIndex = 15;
            // 
            // searchBooksButton
            // 
            this.searchBooksButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.8F);
            this.searchBooksButton.Location = new System.Drawing.Point(102, 25);
            this.searchBooksButton.Margin = new System.Windows.Forms.Padding(2);
            this.searchBooksButton.Name = "searchBooksButton";
            this.searchBooksButton.Size = new System.Drawing.Size(88, 28);
            this.searchBooksButton.TabIndex = 16;
            this.searchBooksButton.Text = "Search Books";
            this.searchBooksButton.UseVisualStyleBackColor = true;
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(772, 712);
            this.Controls.Add(this.menuStrip);
            this.Controls.Add(this.searchBooksButton);
            this.Controls.Add(this.categoryDropDown);
            this.Controls.Add(this.detailsGroup);
            this.Controls.Add(this.filterGroup);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.dataGrid);
            this.Controls.Add(this.wishListButton);
            this.Controls.Add(this.tagsButton);
            this.Controls.Add(this.deleteSelectedButton);
            this.Controls.Add(this.addButton);
            this.MainMenuStrip = this.menuStrip;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "MainWindow";
            this.Text = "MyLibrary";
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGrid)).EndInit();
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.filterGroup.ResumeLayout(false);
            this.filterGroup.PerformLayout();
            this.detailsGroup.ResumeLayout(false);
            this.detailsGroup.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStripMenuItem fileMenu;
        private System.Windows.Forms.ToolStripMenuItem editMenu;
        private System.Windows.Forms.ToolStripMenuItem viewMenu;
        private System.Windows.Forms.ToolStripMenuItem settingsMenu;
        private System.Windows.Forms.ToolStripMenuItem helpMenu;
        private System.Windows.Forms.Button addButton;
        private System.Windows.Forms.Button deleteSelectedButton;
        private System.Windows.Forms.Button tagsButton;
        private System.Windows.Forms.Button wishListButton;
        private System.Windows.Forms.DataGridView dataGrid;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel statusLabel;
        private System.Windows.Forms.ToolStripStatusLabel itemsDisplayedLabel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox filterGroup;
        private System.Windows.Forms.GroupBox detailsGroup;
        private System.Windows.Forms.PictureBox pictureBox;
        private System.Windows.Forms.Button saveFilterButton;
        private System.Windows.Forms.Button clearFilterButton;
        private System.Windows.Forms.Button applyFilterButton;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox titleFilterField;
        private System.Windows.Forms.ToolStripMenuItem exitMenuItem;
        private System.Windows.Forms.ComboBox categoryDropDown;
        private System.Windows.Forms.Button discardChangesButton;
        private System.Windows.Forms.Button saveChangesButton;
        private System.Windows.Forms.TextBox textBoxNotes;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button selectImageButton;
        private System.Windows.Forms.Button removeImageButton;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckedListBox tagsList;
        private System.Windows.Forms.Button manageItemTagsButton;
        private System.Windows.Forms.ToolStripMenuItem newBookToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem newMediaItemToolStripMenuItem;
        private System.Windows.Forms.RichTextBox detailsBox;
        private System.Windows.Forms.Button searchBooksButton;
        private System.Windows.Forms.ToolStripMenuItem databaseStatisticsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
    }
}

