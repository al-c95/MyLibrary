//MIT License

//Copyright (c) 2022

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
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MyLibrary.Views;

namespace MyLibrary
{
    public partial class ExcelImportDialog : Form, IExcelImportDialog
    {
        public ExcelImportDialog()
        {
            InitializeComponent();

            this.itemsList.View = View.Details;

            // register event handlers
            this.closeButton.Click += CloseButton_Click;
            this.browseButton.Click += ((sender, args) =>
            {
                this.BrowseButtonClicked?.Invoke(sender, args);
            });
            this.startButton.Click += ((sender, args) =>
            {
                this.StartButtonClicked?.Invoke(sender, args);
            });
        }

        public string Label1Text
        {
            get => this.label1.Text;
            set => this.label1.Text = value;
        }

        public string Label2Text 
        {
            get => this.label2.Text;
            set => this.label2.Text = value;
        }

        public bool CloseButtonEnabled 
        {
            get => this.closeButton.Enabled;
            set => this.closeButton.Enabled = value; 
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

        public bool FileFieldEnabled 
        {
            get => this.fileField.Enabled;
            set => this.fileField.Enabled = value;
        }

        public event EventHandler BrowseButtonClicked;
        public event EventHandler StartButtonClicked;

        public void AddError(string message)
        {
            throw new NotImplementedException();
        }

        public void AddSuccess(string message)
        {
            throw new NotImplementedException();
        }

        public void AddWarning(string message)
        {
            throw new NotImplementedException();
        }

        private void AddListItem(string message)
        {
            this.itemsList.Items.Add(message);
        }

        public string ShowFileBrowserDialog()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Excel Files|*.xls;*.xlsx;*.xlsm";
            openFileDialog.Title = "Import Excel";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                return openFileDialog.FileName;
            }

            return null;
        }

        #region UI event handlers
        private void CloseButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion
    }//class
}
