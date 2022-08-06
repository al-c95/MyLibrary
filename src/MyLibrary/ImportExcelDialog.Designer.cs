
namespace MyLibrary
{
    partial class ImportExcelDialog
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
            System.Windows.Forms.ListViewItem listViewItem1 = new System.Windows.Forms.ListViewItem(new string[] {
            "",
            "test"}, -1);
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.closeButton = new System.Windows.Forms.Button();
            this.itemsList = new System.Windows.Forms.ListView();
            this.iconColumn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.itemColumn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(46, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "label1";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 26);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(46, 17);
            this.label2.TabIndex = 1;
            this.label2.Text = "label2";
            // 
            // closeButton
            // 
            this.closeButton.Location = new System.Drawing.Point(676, 403);
            this.closeButton.Name = "closeButton";
            this.closeButton.Size = new System.Drawing.Size(112, 35);
            this.closeButton.TabIndex = 3;
            this.closeButton.Text = "Close";
            this.closeButton.UseVisualStyleBackColor = true;
            // 
            // itemsList
            // 
            this.itemsList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.iconColumn,
            this.itemColumn});
            this.itemsList.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.itemsList.HideSelection = false;
            this.itemsList.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem1});
            this.itemsList.Location = new System.Drawing.Point(15, 66);
            this.itemsList.Name = "itemsList";
            this.itemsList.ShowGroups = false;
            this.itemsList.Size = new System.Drawing.Size(773, 331);
            this.itemsList.TabIndex = 4;
            this.itemsList.UseCompatibleStateImageBehavior = false;
            // 
            // iconColumn
            // 
            this.iconColumn.Text = "";
            this.iconColumn.Width = 20;
            // 
            // itemColumn
            // 
            this.itemColumn.Text = "Item";
            // 
            // ImportExcelDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.itemsList);
            this.Controls.Add(this.closeButton);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "ImportExcelDialog";
            this.Text = "ImportExcelDialog";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button closeButton;
        private System.Windows.Forms.ListView itemsList;
        private System.Windows.Forms.ColumnHeader iconColumn;
        private System.Windows.Forms.ColumnHeader itemColumn;
    }
}