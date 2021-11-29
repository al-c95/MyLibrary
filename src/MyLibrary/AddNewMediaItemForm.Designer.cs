
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
            this.SuspendLayout();
            // 
            // saveButton
            // 
            this.saveButton.Location = new System.Drawing.Point(12, 263);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(150, 27);
            this.saveButton.TabIndex = 10;
            this.saveButton.Text = "Save";
            this.saveButton.UseVisualStyleBackColor = true;
            // 
            // cancelButton
            // 
            this.cancelButton.Location = new System.Drawing.Point(644, 262);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(150, 27);
            this.cancelButton.TabIndex = 11;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // mediaTypesOptions
            // 
            this.mediaTypesOptions.FormattingEnabled = true;
            this.mediaTypesOptions.Location = new System.Drawing.Point(73, 6);
            this.mediaTypesOptions.Name = "mediaTypesOptions";
            this.mediaTypesOptions.Size = new System.Drawing.Size(216, 24);
            this.mediaTypesOptions.TabIndex = 12;
            // 
            // notesField
            // 
            this.notesField.Location = new System.Drawing.Point(12, 131);
            this.notesField.Multiline = true;
            this.notesField.Name = "notesField";
            this.notesField.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.notesField.Size = new System.Drawing.Size(782, 115);
            this.notesField.TabIndex = 13;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 111);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(45, 17);
            this.label5.TabIndex = 14;
            this.label5.Text = "Notes";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(44, 17);
            this.label1.TabIndex = 15;
            this.label1.Text = "Type:";
            // 
            // titleField
            // 
            this.titleField.Location = new System.Drawing.Point(466, 6);
            this.titleField.Name = "titleField";
            this.titleField.Size = new System.Drawing.Size(328, 22);
            this.titleField.TabIndex = 16;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(335, 6);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(39, 17);
            this.label2.TabIndex = 17;
            this.label2.Text = "Title:";
            // 
            // numberField
            // 
            this.numberField.Location = new System.Drawing.Point(466, 34);
            this.numberField.Name = "numberField";
            this.numberField.Size = new System.Drawing.Size(328, 22);
            this.numberField.TabIndex = 18;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(335, 34);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(62, 17);
            this.label3.TabIndex = 19;
            this.label3.Text = "Number:";
            // 
            // runningTimeField
            // 
            this.runningTimeField.Location = new System.Drawing.Point(466, 62);
            this.runningTimeField.Name = "runningTimeField";
            this.runningTimeField.Size = new System.Drawing.Size(328, 22);
            this.runningTimeField.TabIndex = 20;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(335, 65);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(100, 17);
            this.label4.TabIndex = 21;
            this.label4.Text = "Running Time:";
            // 
            // yearField
            // 
            this.yearField.Location = new System.Drawing.Point(466, 90);
            this.yearField.Name = "yearField";
            this.yearField.Size = new System.Drawing.Size(328, 22);
            this.yearField.TabIndex = 22;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(335, 93);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(53, 21);
            this.label6.TabIndex = 23;
            this.label6.Text = "Year:";
            // 
            // AddNewMediaItemForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(806, 301);
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
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AddNewMediaItemForm";
            this.Text = "Add New Media Item";
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
    }
}