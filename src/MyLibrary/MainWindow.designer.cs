
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.fileMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.exitMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.viewMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.settingsMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.helpMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.addButton = new System.Windows.Forms.Button();
            this.deleteSelectedButton = new System.Windows.Forms.Button();
            this.editButton = new System.Windows.Forms.Button();
            this.viewAllButton = new System.Windows.Forms.Button();
            this.tagsButton = new System.Windows.Forms.Button();
            this.wishListButton = new System.Windows.Forms.Button();
            this.dataGrid = new System.Windows.Forms.DataGridView();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.label1 = new System.Windows.Forms.Label();
            this.filterGroup = new System.Windows.Forms.GroupBox();
            this.saveFilterButton = new System.Windows.Forms.Button();
            this.clearFilterButton = new System.Windows.Forms.Button();
            this.applyFilterButton = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.criteriaField = new System.Windows.Forms.TextBox();
            this.detailsGroup = new System.Windows.Forms.GroupBox();
            this.pictureBox = new System.Windows.Forms.PictureBox();
            this.categoryDropDown = new System.Windows.Forms.ComboBox();
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
            this.menuStrip.Size = new System.Drawing.Size(1029, 28);
            this.menuStrip.TabIndex = 0;
            this.menuStrip.Text = "menuStrip1";
            // 
            // fileMenu
            // 
            this.fileMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exitMenuItem});
            this.fileMenu.Name = "fileMenu";
            this.fileMenu.Size = new System.Drawing.Size(46, 24);
            this.fileMenu.Text = "File";
            // 
            // exitMenuItem
            // 
            this.exitMenuItem.Name = "exitMenuItem";
            this.exitMenuItem.Size = new System.Drawing.Size(116, 26);
            this.exitMenuItem.Text = "Exit";
            // 
            // editMenu
            // 
            this.editMenu.Name = "editMenu";
            this.editMenu.Size = new System.Drawing.Size(49, 24);
            this.editMenu.Text = "Edit";
            // 
            // viewMenu
            // 
            this.viewMenu.Name = "viewMenu";
            this.viewMenu.Size = new System.Drawing.Size(55, 24);
            this.viewMenu.Text = "View";
            // 
            // settingsMenu
            // 
            this.settingsMenu.Name = "settingsMenu";
            this.settingsMenu.Size = new System.Drawing.Size(76, 24);
            this.settingsMenu.Text = "Settings";
            // 
            // helpMenu
            // 
            this.helpMenu.Name = "helpMenu";
            this.helpMenu.Size = new System.Drawing.Size(55, 24);
            this.helpMenu.Text = "Help";
            // 
            // addButton
            // 
            this.addButton.Location = new System.Drawing.Point(12, 31);
            this.addButton.Name = "addButton";
            this.addButton.Size = new System.Drawing.Size(95, 35);
            this.addButton.TabIndex = 1;
            this.addButton.Text = "Add";
            this.addButton.UseVisualStyleBackColor = true;
            // 
            // deleteSelectedButton
            // 
            this.deleteSelectedButton.Enabled = false;
            this.deleteSelectedButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 5.8F);
            this.deleteSelectedButton.Location = new System.Drawing.Point(113, 31);
            this.deleteSelectedButton.Name = "deleteSelectedButton";
            this.deleteSelectedButton.Size = new System.Drawing.Size(95, 35);
            this.deleteSelectedButton.TabIndex = 2;
            this.deleteSelectedButton.Text = "Delete Selected";
            this.deleteSelectedButton.UseVisualStyleBackColor = true;
            // 
            // editButton
            // 
            this.editButton.Enabled = false;
            this.editButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F);
            this.editButton.Location = new System.Drawing.Point(214, 31);
            this.editButton.Name = "editButton";
            this.editButton.Size = new System.Drawing.Size(95, 35);
            this.editButton.TabIndex = 3;
            this.editButton.Text = "Edit";
            this.editButton.UseVisualStyleBackColor = true;
            // 
            // viewAllButton
            // 
            this.viewAllButton.Enabled = false;
            this.viewAllButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F);
            this.viewAllButton.Location = new System.Drawing.Point(315, 31);
            this.viewAllButton.Name = "viewAllButton";
            this.viewAllButton.Size = new System.Drawing.Size(95, 35);
            this.viewAllButton.TabIndex = 4;
            this.viewAllButton.Text = "View All";
            this.viewAllButton.UseVisualStyleBackColor = true;
            // 
            // tagsButton
            // 
            this.tagsButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.tagsButton.Enabled = false;
            this.tagsButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F);
            this.tagsButton.Location = new System.Drawing.Point(821, 31);
            this.tagsButton.Name = "tagsButton";
            this.tagsButton.Size = new System.Drawing.Size(95, 35);
            this.tagsButton.TabIndex = 5;
            this.tagsButton.Text = "Tags";
            this.tagsButton.UseVisualStyleBackColor = true;
            // 
            // wishListButton
            // 
            this.wishListButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.wishListButton.Enabled = false;
            this.wishListButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F);
            this.wishListButton.Location = new System.Drawing.Point(922, 31);
            this.wishListButton.Name = "wishListButton";
            this.wishListButton.Size = new System.Drawing.Size(95, 35);
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
            this.dataGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Arial", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGrid.DefaultCellStyle = dataGridViewCellStyle3;
            this.dataGrid.Location = new System.Drawing.Point(315, 72);
            this.dataGrid.MultiSelect = false;
            this.dataGrid.Name = "dataGrid";
            this.dataGrid.ReadOnly = true;
            this.dataGrid.RowHeadersWidth = 51;
            this.dataGrid.RowTemplate.Height = 24;
            this.dataGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGrid.Size = new System.Drawing.Size(702, 416);
            this.dataGrid.TabIndex = 7;
            // 
            // statusStrip
            // 
            this.statusStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.toolStripStatusLabel2});
            this.statusStrip.Location = new System.Drawing.Point(0, 503);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(1029, 26);
            this.statusStrip.TabIndex = 8;
            this.statusStrip.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(50, 20);
            this.toolStripStatusLabel1.Text = "Ready";
            // 
            // toolStripStatusLabel2
            // 
            this.toolStripStatusLabel2.Name = "toolStripStatusLabel2";
            this.toolStripStatusLabel2.Size = new System.Drawing.Size(269, 20);
            this.toolStripStatusLabel2.Text = "x items selected. y of z items displayed.";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 72);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(69, 17);
            this.label1.TabIndex = 10;
            this.label1.Text = "Category:";
            // 
            // filterGroup
            // 
            this.filterGroup.Controls.Add(this.saveFilterButton);
            this.filterGroup.Controls.Add(this.clearFilterButton);
            this.filterGroup.Controls.Add(this.applyFilterButton);
            this.filterGroup.Controls.Add(this.label2);
            this.filterGroup.Controls.Add(this.criteriaField);
            this.filterGroup.Enabled = false;
            this.filterGroup.Location = new System.Drawing.Point(15, 102);
            this.filterGroup.Name = "filterGroup";
            this.filterGroup.Size = new System.Drawing.Size(294, 125);
            this.filterGroup.TabIndex = 11;
            this.filterGroup.TabStop = false;
            this.filterGroup.Text = "Filter";
            // 
            // saveFilterButton
            // 
            this.saveFilterButton.Location = new System.Drawing.Point(131, 91);
            this.saveFilterButton.Name = "saveFilterButton";
            this.saveFilterButton.Size = new System.Drawing.Size(74, 28);
            this.saveFilterButton.TabIndex = 15;
            this.saveFilterButton.Text = "Save";
            this.saveFilterButton.UseVisualStyleBackColor = true;
            // 
            // clearFilterButton
            // 
            this.clearFilterButton.Location = new System.Drawing.Point(211, 91);
            this.clearFilterButton.Name = "clearFilterButton";
            this.clearFilterButton.Size = new System.Drawing.Size(74, 28);
            this.clearFilterButton.TabIndex = 14;
            this.clearFilterButton.Text = "Clear";
            this.clearFilterButton.UseVisualStyleBackColor = true;
            // 
            // applyFilterButton
            // 
            this.applyFilterButton.Location = new System.Drawing.Point(9, 91);
            this.applyFilterButton.Name = "applyFilterButton";
            this.applyFilterButton.Size = new System.Drawing.Size(74, 28);
            this.applyFilterButton.TabIndex = 13;
            this.applyFilterButton.Text = "Apply";
            this.applyFilterButton.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 24);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(57, 17);
            this.label2.TabIndex = 13;
            this.label2.Text = "Criteria:";
            // 
            // criteriaField
            // 
            this.criteriaField.Location = new System.Drawing.Point(89, 21);
            this.criteriaField.Name = "criteriaField";
            this.criteriaField.Size = new System.Drawing.Size(196, 22);
            this.criteriaField.TabIndex = 13;
            // 
            // detailsGroup
            // 
            this.detailsGroup.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.detailsGroup.Controls.Add(this.pictureBox);
            this.detailsGroup.Enabled = false;
            this.detailsGroup.Location = new System.Drawing.Point(12, 233);
            this.detailsGroup.Name = "detailsGroup";
            this.detailsGroup.Size = new System.Drawing.Size(294, 255);
            this.detailsGroup.TabIndex = 12;
            this.detailsGroup.TabStop = false;
            this.detailsGroup.Text = "Item Details";
            // 
            // pictureBox
            // 
            this.pictureBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.pictureBox.Location = new System.Drawing.Point(6, 21);
            this.pictureBox.Name = "pictureBox";
            this.pictureBox.Size = new System.Drawing.Size(282, 228);
            this.pictureBox.TabIndex = 13;
            this.pictureBox.TabStop = false;
            // 
            // categoryDropDown
            // 
            this.categoryDropDown.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.categoryDropDown.FormattingEnabled = true;
            this.categoryDropDown.Location = new System.Drawing.Point(104, 69);
            this.categoryDropDown.Name = "categoryDropDown";
            this.categoryDropDown.Size = new System.Drawing.Size(196, 24);
            this.categoryDropDown.TabIndex = 15;
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1029, 529);
            this.Controls.Add(this.categoryDropDown);
            this.Controls.Add(this.detailsGroup);
            this.Controls.Add(this.filterGroup);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.dataGrid);
            this.Controls.Add(this.wishListButton);
            this.Controls.Add(this.tagsButton);
            this.Controls.Add(this.viewAllButton);
            this.Controls.Add(this.editButton);
            this.Controls.Add(this.deleteSelectedButton);
            this.Controls.Add(this.addButton);
            this.Controls.Add(this.menuStrip);
            this.MainMenuStrip = this.menuStrip;
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
        private System.Windows.Forms.Button editButton;
        private System.Windows.Forms.Button viewAllButton;
        private System.Windows.Forms.Button tagsButton;
        private System.Windows.Forms.Button wishListButton;
        private System.Windows.Forms.DataGridView dataGrid;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox filterGroup;
        private System.Windows.Forms.GroupBox detailsGroup;
        private System.Windows.Forms.PictureBox pictureBox;
        private System.Windows.Forms.Button saveFilterButton;
        private System.Windows.Forms.Button clearFilterButton;
        private System.Windows.Forms.Button applyFilterButton;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox criteriaField;
        private System.Windows.Forms.ToolStripMenuItem exitMenuItem;
        private System.Windows.Forms.ComboBox categoryDropDown;
    }
}

