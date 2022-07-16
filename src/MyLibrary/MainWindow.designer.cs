
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainWindow));
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.fileMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.newBookToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newMediaItemToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.importToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cSVToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.importTagsCsvToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.importAuthorsCsvToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.importPublishersCsvToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tagsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.publishersToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.authorsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.booksToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.mediaItemsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportWishlistMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.exitMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.databaseStatisticsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.booksToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mediaItemsAllCategoriesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cdsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dvdsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.bluRaysToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.vhssToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.vinylsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.otherToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dataGrid = new System.Windows.Forms.DataGridView();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.statusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.itemsDisplayedLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.categoryLabel = new System.Windows.Forms.Label();
            this.filterGroup = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.tagsList = new System.Windows.Forms.CheckedListBox();
            this.clearFilterButton = new System.Windows.Forms.Button();
            this.applyFilterButton = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.titleFilterField = new System.Windows.Forms.TextBox();
            this.detailsGroup = new System.Windows.Forms.GroupBox();
            this.itemDetailsSpinner = new CircularProgressBar.CircularProgressBar();
            this.manageItemCopiesButton = new System.Windows.Forms.Button();
            this.detailsBox = new System.Windows.Forms.RichTextBox();
            this.manageItemTagsButton = new System.Windows.Forms.Button();
            this.removeImageButton = new System.Windows.Forms.Button();
            this.selectImageButton = new System.Windows.Forms.Button();
            this.notesLabel = new System.Windows.Forms.Label();
            this.textBoxNotes = new System.Windows.Forms.TextBox();
            this.discardChangesButton = new System.Windows.Forms.Button();
            this.saveChangesButton = new System.Windows.Forms.Button();
            this.pictureBox = new System.Windows.Forms.PictureBox();
            this.categoryDropDown = new System.Windows.Forms.ComboBox();
            this.toolStripContainer1 = new System.Windows.Forms.ToolStripContainer();
            this.toolStrip = new System.Windows.Forms.ToolStrip();
            this.addButton = new System.Windows.Forms.ToolStripButton();
            this.searchBooksButton = new System.Windows.Forms.ToolStripButton();
            this.deleteSelectedButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.tagsButton = new System.Windows.Forms.ToolStripButton();
            this.wishlistButton = new System.Windows.Forms.ToolStripButton();
            this.menuStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGrid)).BeginInit();
            this.statusStrip.SuspendLayout();
            this.filterGroup.SuspendLayout();
            this.detailsGroup.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).BeginInit();
            this.toolStripContainer1.BottomToolStripPanel.SuspendLayout();
            this.toolStripContainer1.ContentPanel.SuspendLayout();
            this.toolStripContainer1.TopToolStripPanel.SuspendLayout();
            this.toolStripContainer1.SuspendLayout();
            this.toolStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip
            // 
            this.menuStrip.Dock = System.Windows.Forms.DockStyle.None;
            this.menuStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileMenu,
            this.viewMenu,
            this.helpMenu});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Padding = new System.Windows.Forms.Padding(4, 2, 0, 2);
            this.menuStrip.Size = new System.Drawing.Size(1349, 28);
            this.menuStrip.TabIndex = 0;
            this.menuStrip.Text = "menuStrip1";
            // 
            // fileMenu
            // 
            this.fileMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newBookToolStripMenuItem,
            this.newMediaItemToolStripMenuItem,
            this.toolStripSeparator1,
            this.importToolStripMenuItem,
            this.exportToolStripMenuItem,
            this.toolStripSeparator3,
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
            // importToolStripMenuItem
            // 
            this.importToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cSVToolStripMenuItem});
            this.importToolStripMenuItem.Name = "importToolStripMenuItem";
            this.importToolStripMenuItem.Size = new System.Drawing.Size(202, 26);
            this.importToolStripMenuItem.Text = "Import";
            // 
            // cSVToolStripMenuItem
            // 
            this.cSVToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.importTagsCsvToolStripMenuItem,
            this.importAuthorsCsvToolStripMenuItem,
            this.importPublishersCsvToolStripMenuItem});
            this.cSVToolStripMenuItem.Name = "cSVToolStripMenuItem";
            this.cSVToolStripMenuItem.Size = new System.Drawing.Size(118, 26);
            this.cSVToolStripMenuItem.Text = "CSV";
            // 
            // importTagsCsvToolStripMenuItem
            // 
            this.importTagsCsvToolStripMenuItem.Name = "importTagsCsvToolStripMenuItem";
            this.importTagsCsvToolStripMenuItem.Size = new System.Drawing.Size(158, 26);
            this.importTagsCsvToolStripMenuItem.Text = "Tags";
            // 
            // importAuthorsCsvToolStripMenuItem
            // 
            this.importAuthorsCsvToolStripMenuItem.Name = "importAuthorsCsvToolStripMenuItem";
            this.importAuthorsCsvToolStripMenuItem.Size = new System.Drawing.Size(158, 26);
            this.importAuthorsCsvToolStripMenuItem.Text = "Authors";
            // 
            // importPublishersCsvToolStripMenuItem
            // 
            this.importPublishersCsvToolStripMenuItem.Name = "importPublishersCsvToolStripMenuItem";
            this.importPublishersCsvToolStripMenuItem.Size = new System.Drawing.Size(158, 26);
            this.importPublishersCsvToolStripMenuItem.Text = "Publishers";
            // 
            // exportToolStripMenuItem
            // 
            this.exportToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tagsToolStripMenuItem,
            this.publishersToolStripMenuItem,
            this.authorsToolStripMenuItem,
            this.booksToolStripMenuItem1,
            this.mediaItemsToolStripMenuItem,
            this.exportWishlistMenuItem});
            this.exportToolStripMenuItem.Name = "exportToolStripMenuItem";
            this.exportToolStripMenuItem.Size = new System.Drawing.Size(202, 26);
            this.exportToolStripMenuItem.Text = "Export";
            // 
            // tagsToolStripMenuItem
            // 
            this.tagsToolStripMenuItem.Name = "tagsToolStripMenuItem";
            this.tagsToolStripMenuItem.Size = new System.Drawing.Size(174, 26);
            this.tagsToolStripMenuItem.Text = "Tags";
            // 
            // publishersToolStripMenuItem
            // 
            this.publishersToolStripMenuItem.Name = "publishersToolStripMenuItem";
            this.publishersToolStripMenuItem.Size = new System.Drawing.Size(174, 26);
            this.publishersToolStripMenuItem.Text = "Publishers";
            // 
            // authorsToolStripMenuItem
            // 
            this.authorsToolStripMenuItem.Name = "authorsToolStripMenuItem";
            this.authorsToolStripMenuItem.Size = new System.Drawing.Size(174, 26);
            this.authorsToolStripMenuItem.Text = "Authors";
            // 
            // booksToolStripMenuItem1
            // 
            this.booksToolStripMenuItem1.Name = "booksToolStripMenuItem1";
            this.booksToolStripMenuItem1.Size = new System.Drawing.Size(174, 26);
            this.booksToolStripMenuItem1.Text = "Books";
            // 
            // mediaItemsToolStripMenuItem
            // 
            this.mediaItemsToolStripMenuItem.Name = "mediaItemsToolStripMenuItem";
            this.mediaItemsToolStripMenuItem.Size = new System.Drawing.Size(174, 26);
            this.mediaItemsToolStripMenuItem.Text = "Media items";
            // 
            // exportWishlistMenuItem
            // 
            this.exportWishlistMenuItem.Name = "exportWishlistMenuItem";
            this.exportWishlistMenuItem.Size = new System.Drawing.Size(174, 26);
            this.exportWishlistMenuItem.Text = "Wishlist";
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(199, 6);
            // 
            // exitMenuItem
            // 
            this.exitMenuItem.Name = "exitMenuItem";
            this.exitMenuItem.Size = new System.Drawing.Size(202, 26);
            this.exitMenuItem.Text = "Exit";
            // 
            // viewMenu
            // 
            this.viewMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.databaseStatisticsToolStripMenuItem,
            this.toolStripSeparator2,
            this.booksToolStripMenuItem,
            this.mediaItemsAllCategoriesToolStripMenuItem,
            this.cdsToolStripMenuItem,
            this.dvdsToolStripMenuItem,
            this.bluRaysToolStripMenuItem,
            this.vhssToolStripMenuItem,
            this.vinylsToolStripMenuItem,
            this.otherToolStripMenuItem});
            this.viewMenu.Name = "viewMenu";
            this.viewMenu.Size = new System.Drawing.Size(55, 24);
            this.viewMenu.Text = "View";
            // 
            // databaseStatisticsToolStripMenuItem
            // 
            this.databaseStatisticsToolStripMenuItem.Name = "databaseStatisticsToolStripMenuItem";
            this.databaseStatisticsToolStripMenuItem.Size = new System.Drawing.Size(279, 26);
            this.databaseStatisticsToolStripMenuItem.Text = "Database statistics";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(276, 6);
            // 
            // booksToolStripMenuItem
            // 
            this.booksToolStripMenuItem.Name = "booksToolStripMenuItem";
            this.booksToolStripMenuItem.Size = new System.Drawing.Size(279, 26);
            this.booksToolStripMenuItem.Text = "Books";
            // 
            // mediaItemsAllCategoriesToolStripMenuItem
            // 
            this.mediaItemsAllCategoriesToolStripMenuItem.Name = "mediaItemsAllCategoriesToolStripMenuItem";
            this.mediaItemsAllCategoriesToolStripMenuItem.Size = new System.Drawing.Size(279, 26);
            this.mediaItemsAllCategoriesToolStripMenuItem.Text = "Media Items (All categories)";
            // 
            // cdsToolStripMenuItem
            // 
            this.cdsToolStripMenuItem.Name = "cdsToolStripMenuItem";
            this.cdsToolStripMenuItem.Size = new System.Drawing.Size(279, 26);
            this.cdsToolStripMenuItem.Text = "Cds";
            // 
            // dvdsToolStripMenuItem
            // 
            this.dvdsToolStripMenuItem.Name = "dvdsToolStripMenuItem";
            this.dvdsToolStripMenuItem.Size = new System.Drawing.Size(279, 26);
            this.dvdsToolStripMenuItem.Text = "Dvds";
            // 
            // bluRaysToolStripMenuItem
            // 
            this.bluRaysToolStripMenuItem.Name = "bluRaysToolStripMenuItem";
            this.bluRaysToolStripMenuItem.Size = new System.Drawing.Size(279, 26);
            this.bluRaysToolStripMenuItem.Text = "BluRays";
            // 
            // vhssToolStripMenuItem
            // 
            this.vhssToolStripMenuItem.Name = "vhssToolStripMenuItem";
            this.vhssToolStripMenuItem.Size = new System.Drawing.Size(279, 26);
            this.vhssToolStripMenuItem.Text = "Vhss";
            // 
            // vinylsToolStripMenuItem
            // 
            this.vinylsToolStripMenuItem.Name = "vinylsToolStripMenuItem";
            this.vinylsToolStripMenuItem.Size = new System.Drawing.Size(279, 26);
            this.vinylsToolStripMenuItem.Text = "Vinyls";
            // 
            // otherToolStripMenuItem
            // 
            this.otherToolStripMenuItem.Name = "otherToolStripMenuItem";
            this.otherToolStripMenuItem.Size = new System.Drawing.Size(279, 26);
            this.otherToolStripMenuItem.Text = "Other";
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
            this.dataGrid.Location = new System.Drawing.Point(485, 37);
            this.dataGrid.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dataGrid.MultiSelect = false;
            this.dataGrid.Name = "dataGrid";
            this.dataGrid.ReadOnly = true;
            this.dataGrid.RowHeadersWidth = 51;
            this.dataGrid.RowTemplate.Height = 24;
            this.dataGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGrid.Size = new System.Drawing.Size(849, 743);
            this.dataGrid.TabIndex = 7;
            // 
            // statusStrip
            // 
            this.statusStrip.Dock = System.Windows.Forms.DockStyle.None;
            this.statusStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statusLabel,
            this.itemsDisplayedLabel});
            this.statusStrip.Location = new System.Drawing.Point(0, 0);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Padding = new System.Windows.Forms.Padding(1, 0, 10, 0);
            this.statusStrip.Size = new System.Drawing.Size(1349, 26);
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
            // categoryLabel
            // 
            this.categoryLabel.AutoSize = true;
            this.categoryLabel.Location = new System.Drawing.Point(205, 8);
            this.categoryLabel.Name = "categoryLabel";
            this.categoryLabel.Size = new System.Drawing.Size(69, 17);
            this.categoryLabel.TabIndex = 10;
            this.categoryLabel.Text = "Category:";
            // 
            // filterGroup
            // 
            this.filterGroup.Controls.Add(this.label4);
            this.filterGroup.Controls.Add(this.tagsList);
            this.filterGroup.Controls.Add(this.clearFilterButton);
            this.filterGroup.Controls.Add(this.applyFilterButton);
            this.filterGroup.Controls.Add(this.label2);
            this.filterGroup.Controls.Add(this.titleFilterField);
            this.filterGroup.Location = new System.Drawing.Point(7, 37);
            this.filterGroup.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.filterGroup.Name = "filterGroup";
            this.filterGroup.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.filterGroup.Size = new System.Drawing.Size(468, 221);
            this.filterGroup.TabIndex = 11;
            this.filterGroup.TabStop = false;
            this.filterGroup.Text = "Filter";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 62);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(44, 17);
            this.label4.TabIndex = 16;
            this.label4.Text = "Tags:";
            // 
            // tagsList
            // 
            this.tagsList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tagsList.FormattingEnabled = true;
            this.tagsList.Location = new System.Drawing.Point(72, 62);
            this.tagsList.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tagsList.Name = "tagsList";
            this.tagsList.Size = new System.Drawing.Size(389, 106);
            this.tagsList.TabIndex = 16;
            // 
            // clearFilterButton
            // 
            this.clearFilterButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.clearFilterButton.Location = new System.Drawing.Point(388, 185);
            this.clearFilterButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.clearFilterButton.Name = "clearFilterButton";
            this.clearFilterButton.Size = new System.Drawing.Size(75, 28);
            this.clearFilterButton.TabIndex = 14;
            this.clearFilterButton.Text = "Clear";
            this.clearFilterButton.UseVisualStyleBackColor = true;
            // 
            // applyFilterButton
            // 
            this.applyFilterButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.applyFilterButton.Location = new System.Drawing.Point(5, 188);
            this.applyFilterButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.applyFilterButton.Name = "applyFilterButton";
            this.applyFilterButton.Size = new System.Drawing.Size(75, 28);
            this.applyFilterButton.TabIndex = 13;
            this.applyFilterButton.Text = "Apply";
            this.applyFilterButton.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 25);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(39, 17);
            this.label2.TabIndex = 13;
            this.label2.Text = "Title:";
            // 
            // titleFilterField
            // 
            this.titleFilterField.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.titleFilterField.Location = new System.Drawing.Point(72, 21);
            this.titleFilterField.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.titleFilterField.Name = "titleFilterField";
            this.titleFilterField.Size = new System.Drawing.Size(389, 22);
            this.titleFilterField.TabIndex = 13;
            // 
            // detailsGroup
            // 
            this.detailsGroup.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.detailsGroup.Controls.Add(this.itemDetailsSpinner);
            this.detailsGroup.Controls.Add(this.manageItemCopiesButton);
            this.detailsGroup.Controls.Add(this.detailsBox);
            this.detailsGroup.Controls.Add(this.manageItemTagsButton);
            this.detailsGroup.Controls.Add(this.removeImageButton);
            this.detailsGroup.Controls.Add(this.selectImageButton);
            this.detailsGroup.Controls.Add(this.notesLabel);
            this.detailsGroup.Controls.Add(this.textBoxNotes);
            this.detailsGroup.Controls.Add(this.discardChangesButton);
            this.detailsGroup.Controls.Add(this.saveChangesButton);
            this.detailsGroup.Controls.Add(this.pictureBox);
            this.detailsGroup.Location = new System.Drawing.Point(7, 262);
            this.detailsGroup.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.detailsGroup.Name = "detailsGroup";
            this.detailsGroup.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.detailsGroup.Size = new System.Drawing.Size(468, 518);
            this.detailsGroup.TabIndex = 12;
            this.detailsGroup.TabStop = false;
            this.detailsGroup.Text = "Item Details";
            // 
            // itemDetailsSpinner
            // 
            this.itemDetailsSpinner.AnimationFunction = WinFormAnimation.KnownAnimationFunctions.Liner;
            this.itemDetailsSpinner.AnimationSpeed = 500;
            this.itemDetailsSpinner.BackColor = System.Drawing.Color.Transparent;
            this.itemDetailsSpinner.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.itemDetailsSpinner.ForeColor = System.Drawing.Color.DimGray;
            this.itemDetailsSpinner.InnerColor = System.Drawing.SystemColors.Control;
            this.itemDetailsSpinner.InnerMargin = 2;
            this.itemDetailsSpinner.InnerWidth = 0;
            this.itemDetailsSpinner.Location = new System.Drawing.Point(143, 143);
            this.itemDetailsSpinner.MarqueeAnimationSpeed = 1000;
            this.itemDetailsSpinner.Name = "itemDetailsSpinner";
            this.itemDetailsSpinner.OuterColor = System.Drawing.Color.Transparent;
            this.itemDetailsSpinner.OuterMargin = 0;
            this.itemDetailsSpinner.OuterWidth = 6;
            this.itemDetailsSpinner.ProgressColor = System.Drawing.Color.Lime;
            this.itemDetailsSpinner.ProgressWidth = 10;
            this.itemDetailsSpinner.SecondaryFont = new System.Drawing.Font("Microsoft Sans Serif", 36F);
            this.itemDetailsSpinner.Size = new System.Drawing.Size(181, 181);
            this.itemDetailsSpinner.StartAngle = 270;
            this.itemDetailsSpinner.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.itemDetailsSpinner.SubscriptColor = System.Drawing.Color.FromArgb(((int)(((byte)(166)))), ((int)(((byte)(166)))), ((int)(((byte)(166)))));
            this.itemDetailsSpinner.SubscriptMargin = new System.Windows.Forms.Padding(10, -35, 0, 0);
            this.itemDetailsSpinner.SubscriptText = "";
            this.itemDetailsSpinner.SuperscriptColor = System.Drawing.Color.FromArgb(((int)(((byte)(166)))), ((int)(((byte)(166)))), ((int)(((byte)(166)))));
            this.itemDetailsSpinner.SuperscriptMargin = new System.Windows.Forms.Padding(10, 35, 0, 0);
            this.itemDetailsSpinner.SuperscriptText = "";
            this.itemDetailsSpinner.TabIndex = 18;
            this.itemDetailsSpinner.Text = "Loading...";
            this.itemDetailsSpinner.TextMargin = new System.Windows.Forms.Padding(8, 8, 0, 0);
            this.itemDetailsSpinner.Value = 68;
            // 
            // manageItemCopiesButton
            // 
            this.manageItemCopiesButton.Image = ((System.Drawing.Image)(resources.GetObject("manageItemCopiesButton.Image")));
            this.manageItemCopiesButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.manageItemCopiesButton.Location = new System.Drawing.Point(307, 18);
            this.manageItemCopiesButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.manageItemCopiesButton.Name = "manageItemCopiesButton";
            this.manageItemCopiesButton.Size = new System.Drawing.Size(155, 28);
            this.manageItemCopiesButton.TabIndex = 21;
            this.manageItemCopiesButton.Text = "Manage Copies";
            this.manageItemCopiesButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.manageItemCopiesButton.UseVisualStyleBackColor = true;
            // 
            // detailsBox
            // 
            this.detailsBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.detailsBox.Location = new System.Drawing.Point(9, 169);
            this.detailsBox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.detailsBox.Name = "detailsBox";
            this.detailsBox.ReadOnly = true;
            this.detailsBox.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.detailsBox.Size = new System.Drawing.Size(452, 195);
            this.detailsBox.TabIndex = 0;
            this.detailsBox.Text = "";
            // 
            // manageItemTagsButton
            // 
            this.manageItemTagsButton.Image = ((System.Drawing.Image)(resources.GetObject("manageItemTagsButton.Image")));
            this.manageItemTagsButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.manageItemTagsButton.Location = new System.Drawing.Point(8, 18);
            this.manageItemTagsButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.manageItemTagsButton.Name = "manageItemTagsButton";
            this.manageItemTagsButton.Size = new System.Drawing.Size(155, 28);
            this.manageItemTagsButton.TabIndex = 20;
            this.manageItemTagsButton.Text = "Manage Tags";
            this.manageItemTagsButton.UseVisualStyleBackColor = true;
            // 
            // removeImageButton
            // 
            this.removeImageButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.removeImageButton.Location = new System.Drawing.Point(308, 136);
            this.removeImageButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.removeImageButton.Name = "removeImageButton";
            this.removeImageButton.Size = new System.Drawing.Size(155, 28);
            this.removeImageButton.TabIndex = 19;
            this.removeImageButton.Text = "Remove Image";
            this.removeImageButton.UseVisualStyleBackColor = true;
            // 
            // selectImageButton
            // 
            this.selectImageButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.selectImageButton.Location = new System.Drawing.Point(9, 136);
            this.selectImageButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.selectImageButton.Name = "selectImageButton";
            this.selectImageButton.Size = new System.Drawing.Size(155, 28);
            this.selectImageButton.TabIndex = 18;
            this.selectImageButton.Text = "Select Image";
            this.selectImageButton.UseVisualStyleBackColor = true;
            // 
            // notesLabel
            // 
            this.notesLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.notesLabel.AutoSize = true;
            this.notesLabel.Location = new System.Drawing.Point(12, 369);
            this.notesLabel.Name = "notesLabel";
            this.notesLabel.Size = new System.Drawing.Size(45, 17);
            this.notesLabel.TabIndex = 17;
            this.notesLabel.Text = "Notes";
            // 
            // textBoxNotes
            // 
            this.textBoxNotes.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxNotes.Location = new System.Drawing.Point(12, 387);
            this.textBoxNotes.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBoxNotes.Multiline = true;
            this.textBoxNotes.Name = "textBoxNotes";
            this.textBoxNotes.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxNotes.Size = new System.Drawing.Size(449, 89);
            this.textBoxNotes.TabIndex = 16;
            // 
            // discardChangesButton
            // 
            this.discardChangesButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.discardChangesButton.Enabled = false;
            this.discardChangesButton.Image = ((System.Drawing.Image)(resources.GetObject("discardChangesButton.Image")));
            this.discardChangesButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.discardChangesButton.Location = new System.Drawing.Point(308, 482);
            this.discardChangesButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.discardChangesButton.Name = "discardChangesButton";
            this.discardChangesButton.Size = new System.Drawing.Size(155, 28);
            this.discardChangesButton.TabIndex = 15;
            this.discardChangesButton.Text = "Discard Changes";
            this.discardChangesButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.discardChangesButton.UseVisualStyleBackColor = true;
            // 
            // saveChangesButton
            // 
            this.saveChangesButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.saveChangesButton.Enabled = false;
            this.saveChangesButton.Image = ((System.Drawing.Image)(resources.GetObject("saveChangesButton.Image")));
            this.saveChangesButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.saveChangesButton.Location = new System.Drawing.Point(12, 482);
            this.saveChangesButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.saveChangesButton.Name = "saveChangesButton";
            this.saveChangesButton.Size = new System.Drawing.Size(151, 28);
            this.saveChangesButton.TabIndex = 14;
            this.saveChangesButton.Text = "Save Changes";
            this.saveChangesButton.UseVisualStyleBackColor = true;
            // 
            // pictureBox
            // 
            this.pictureBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox.Image")));
            this.pictureBox.Location = new System.Drawing.Point(9, 50);
            this.pictureBox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.pictureBox.Name = "pictureBox";
            this.pictureBox.Size = new System.Drawing.Size(453, 78);
            this.pictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox.TabIndex = 13;
            this.pictureBox.TabStop = false;
            // 
            // categoryDropDown
            // 
            this.categoryDropDown.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.categoryDropDown.FormattingEnabled = true;
            this.categoryDropDown.Location = new System.Drawing.Point(280, 5);
            this.categoryDropDown.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.categoryDropDown.Name = "categoryDropDown";
            this.categoryDropDown.Size = new System.Drawing.Size(195, 24);
            this.categoryDropDown.TabIndex = 15;
            // 
            // toolStripContainer1
            // 
            // 
            // toolStripContainer1.BottomToolStripPanel
            // 
            this.toolStripContainer1.BottomToolStripPanel.Controls.Add(this.statusStrip);
            // 
            // toolStripContainer1.ContentPanel
            // 
            this.toolStripContainer1.ContentPanel.Controls.Add(this.toolStrip);
            this.toolStripContainer1.ContentPanel.Controls.Add(this.detailsGroup);
            this.toolStripContainer1.ContentPanel.Controls.Add(this.categoryDropDown);
            this.toolStripContainer1.ContentPanel.Controls.Add(this.filterGroup);
            this.toolStripContainer1.ContentPanel.Controls.Add(this.categoryLabel);
            this.toolStripContainer1.ContentPanel.Controls.Add(this.dataGrid);
            this.toolStripContainer1.ContentPanel.Margin = new System.Windows.Forms.Padding(4);
            this.toolStripContainer1.ContentPanel.Size = new System.Drawing.Size(1349, 787);
            this.toolStripContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toolStripContainer1.Location = new System.Drawing.Point(0, 0);
            this.toolStripContainer1.Margin = new System.Windows.Forms.Padding(4);
            this.toolStripContainer1.Name = "toolStripContainer1";
            this.toolStripContainer1.Size = new System.Drawing.Size(1349, 841);
            this.toolStripContainer1.TabIndex = 17;
            this.toolStripContainer1.Text = "toolStripContainer1";
            // 
            // toolStripContainer1.TopToolStripPanel
            // 
            this.toolStripContainer1.TopToolStripPanel.Controls.Add(this.menuStrip);
            // 
            // toolStrip
            // 
            this.toolStrip.Dock = System.Windows.Forms.DockStyle.None;
            this.toolStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addButton,
            this.searchBooksButton,
            this.deleteSelectedButton,
            this.toolStripSeparator4,
            this.tagsButton,
            this.wishlistButton});
            this.toolStrip.Location = new System.Drawing.Point(7, 2);
            this.toolStrip.Name = "toolStrip";
            this.toolStrip.Size = new System.Drawing.Size(164, 27);
            this.toolStrip.TabIndex = 18;
            this.toolStrip.Text = "toolStrip1";
            // 
            // addButton
            // 
            this.addButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.addButton.Image = ((System.Drawing.Image)(resources.GetObject("addButton.Image")));
            this.addButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.addButton.Name = "addButton";
            this.addButton.Size = new System.Drawing.Size(29, 24);
            this.addButton.Text = "toolStripButton1";
            this.addButton.ToolTipText = "Add New Item";
            // 
            // searchBooksButton
            // 
            this.searchBooksButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.searchBooksButton.Image = ((System.Drawing.Image)(resources.GetObject("searchBooksButton.Image")));
            this.searchBooksButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.searchBooksButton.Name = "searchBooksButton";
            this.searchBooksButton.Size = new System.Drawing.Size(29, 24);
            this.searchBooksButton.Text = "toolStripButton2";
            this.searchBooksButton.ToolTipText = "Search Books Online by ISBN";
            // 
            // deleteSelectedButton
            // 
            this.deleteSelectedButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.deleteSelectedButton.Image = ((System.Drawing.Image)(resources.GetObject("deleteSelectedButton.Image")));
            this.deleteSelectedButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.deleteSelectedButton.Name = "deleteSelectedButton";
            this.deleteSelectedButton.Size = new System.Drawing.Size(29, 24);
            this.deleteSelectedButton.Text = "toolStripButton3";
            this.deleteSelectedButton.ToolTipText = "Delete Selected Item";
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 27);
            // 
            // tagsButton
            // 
            this.tagsButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tagsButton.Image = ((System.Drawing.Image)(resources.GetObject("tagsButton.Image")));
            this.tagsButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tagsButton.Name = "tagsButton";
            this.tagsButton.Size = new System.Drawing.Size(29, 24);
            this.tagsButton.Text = "toolStripButton4";
            this.tagsButton.ToolTipText = "Tags";
            // 
            // wishlistButton
            // 
            this.wishlistButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.wishlistButton.Image = ((System.Drawing.Image)(resources.GetObject("wishlistButton.Image")));
            this.wishlistButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.wishlistButton.Name = "wishlistButton";
            this.wishlistButton.Size = new System.Drawing.Size(29, 24);
            this.wishlistButton.Text = "toolStripButton5";
            this.wishlistButton.ToolTipText = "Wishlist";
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1349, 841);
            this.Controls.Add(this.toolStripContainer1);
            this.MainMenuStrip = this.menuStrip;
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "MainWindow";
            this.Text = "MyLibrary";
            this.Resize += new System.EventHandler(this.MainWindow_Resize);
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
            this.toolStripContainer1.BottomToolStripPanel.ResumeLayout(false);
            this.toolStripContainer1.BottomToolStripPanel.PerformLayout();
            this.toolStripContainer1.ContentPanel.ResumeLayout(false);
            this.toolStripContainer1.ContentPanel.PerformLayout();
            this.toolStripContainer1.TopToolStripPanel.ResumeLayout(false);
            this.toolStripContainer1.TopToolStripPanel.PerformLayout();
            this.toolStripContainer1.ResumeLayout(false);
            this.toolStripContainer1.PerformLayout();
            this.toolStrip.ResumeLayout(false);
            this.toolStrip.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStripMenuItem fileMenu;
        private System.Windows.Forms.ToolStripMenuItem viewMenu;
        private System.Windows.Forms.ToolStripMenuItem helpMenu;
        private System.Windows.Forms.DataGridView dataGrid;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel statusLabel;
        private System.Windows.Forms.ToolStripStatusLabel itemsDisplayedLabel;
        private System.Windows.Forms.Label categoryLabel;
        private System.Windows.Forms.GroupBox filterGroup;
        private System.Windows.Forms.GroupBox detailsGroup;
        private System.Windows.Forms.PictureBox pictureBox;
        private System.Windows.Forms.Button clearFilterButton;
        private System.Windows.Forms.Button applyFilterButton;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox titleFilterField;
        private System.Windows.Forms.ToolStripMenuItem exitMenuItem;
        private System.Windows.Forms.ComboBox categoryDropDown;
        private System.Windows.Forms.Button discardChangesButton;
        private System.Windows.Forms.Button saveChangesButton;
        private System.Windows.Forms.TextBox textBoxNotes;
        private System.Windows.Forms.Label notesLabel;
        private System.Windows.Forms.Button selectImageButton;
        private System.Windows.Forms.Button removeImageButton;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckedListBox tagsList;
        private System.Windows.Forms.Button manageItemTagsButton;
        private System.Windows.Forms.ToolStripMenuItem newBookToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem newMediaItemToolStripMenuItem;
        private System.Windows.Forms.RichTextBox detailsBox;
        private System.Windows.Forms.ToolStripMenuItem databaseStatisticsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripContainer toolStripContainer1;
        private System.Windows.Forms.Button manageItemCopiesButton;
        private CircularProgressBar.CircularProgressBar itemDetailsSpinner;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem booksToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mediaItemsAllCategoriesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cdsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem dvdsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem bluRaysToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem vhssToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem vinylsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem otherToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem importToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cSVToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem importTagsCsvToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem importAuthorsCsvToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem importPublishersCsvToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exportToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tagsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem publishersToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem authorsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem booksToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem mediaItemsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exportWishlistMenuItem;
        private System.Windows.Forms.ToolStrip toolStrip;
        private System.Windows.Forms.ToolStripButton addButton;
        private System.Windows.Forms.ToolStripButton searchBooksButton;
        private System.Windows.Forms.ToolStripButton deleteSelectedButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripButton tagsButton;
        private System.Windows.Forms.ToolStripButton wishlistButton;
    }
}

