//MIT License

//Copyright (c) 2021

//Permission is hereby granted, free of charge, to any person obtaining a copy
//of this software and associated documentation files (the "Software"), to deal
//in the Software without restriction, including without limitation the rights
//to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
//copies of the Software, and to permit persons to whom the Software is
//furnished to do so, subject to the following conditions:

//The above copyright notice and this permission notice shall be included in all
//copies or substantial portions of the Software.

//THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
//IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
//FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
//AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
//LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
//OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
//SOFTWARE

using System;
using System.Windows.Forms;
using MyLibrary.Views;

namespace MyLibrary
{
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    public partial class ExportDialog : Form, IExportDialog
    {
        public ExportDialog()
        {
            InitializeComponent();

            this.CenterToParent();

            // register event handlers
            this.browseButton.Click += ((sender, args) =>
            {
                this.BrowseButtonClicked?.Invoke(sender, args);
            });
            this.startButton.Click += ((sender, args) =>
            {
                this.StartButtonClicked?.Invoke(sender, args);
            });
            this.cancelButton.Click += ((sender, args) =>
            {
                this.Cancelled?.Invoke(sender, args);
            });
            this.FormClosed += ((sender, args) =>
            {
                CloseDialog();
            });
        }

        public void CloseDialog()
        {
            this.Close();
        }

        public string Title
        {
            get => this.Text;
            set => this.Text = value;
        }

        public string Label1
        {
            get => this.label1.Text;
            set => this.label1.Text = value;
        }

        public string Label2 
        {
            get => this.label2.Text;
            set => this.label2.Text = value; 
        }

        public string Path 
        {
            get => this.pathField.Text;
            set => this.pathField.Text=value; 
        }

        public bool BrowseButtonEnabled 
        {
            get => this.browseButton.Enabled;
            set => this.browseButton.Enabled = value; 
        }

        public bool StartButtonEnabled 
        {
            get => this.startButton.Enabled;
            set => this.startButton.Enabled = value; 
        }

        public bool CancelButtonEnabled 
        {
            get => this.cancelButton.Enabled;
            set => this.cancelButton.Enabled = value;
        }

        public bool CloseButtonEnabled
        {
            get => this.ControlBox;
            set => this.ControlBox = value;
        }

        public bool PathFieldEnabled
        {
            get => this.pathField.Enabled;
            set => this.pathField.Enabled = value;
        }

        public event EventHandler BrowseButtonClicked;
        public event EventHandler StartButtonClicked;
        public event EventHandler Cancelled;

        public string ShowFolderBrowserDialog(string type)
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                return dialog.SelectedPath + @"\" + type + "s_export.xlsx";
            }
            else
            {
                return null;
            }
        }

        public void ShowErrorDialog(string message)
        {
            MessageBox.Show("Error exporting", message, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }//class
}
