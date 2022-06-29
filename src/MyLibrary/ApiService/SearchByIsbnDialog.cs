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
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using MyLibrary.Models.Entities;
using MyLibrary.Presenters;
using MyLibrary.ApiService;
using MyLibrary.Views;

namespace MyLibrary.ApiService
{
    public partial class SearchByIsbnDialog : Form, ISearchByIsbn
    {
        public SearchByIsbnDialog()
        {
            InitializeComponent();

            // register event handlers
            this.cancelButton.Click += ((sender, args) =>
            {
                this.Close();
            });
            this.isbnField.TextChanged += ((sender, args) =>
            {
                IsbnFieldTextChanged?.Invoke(sender, args);
            });
            this.scanModecheckBox.CheckedChanged += ((sender, args) =>
            {
                this.ActiveControl = this.isbnField;
            });

            this.StartPosition = FormStartPosition.CenterParent;
            this.searchButton.Enabled = false;
        }

        public string IsbnFieldText
        {
            get => this.isbnField.Text;
            set => this.isbnField.Text = value;
        }

        public bool IsbnFieldEnabled
        {
            get => this.isbnField.Enabled;
            set => this.isbnField.Enabled = value;
        }

        public bool ScanModeChecked 
        {
            get => this.scanModecheckBox.Checked;
            set => this.scanModecheckBox.Checked = value;
        }

        public bool SearchButtonEnabled
        {
            get => this.searchButton.Enabled;
            set => this.searchButton.Enabled = value;
        }

        public bool CancelButtonEnabled
        {
            get => this.cancelButton.Enabled;
            set => this.cancelButton.Enabled = value;
        }

        public string StatusLabelText
        {
            get => this.statusLabel.Text;
            set => this.statusLabel.Text = value;
        }

        public event EventHandler SearchButtonClicked;
        public event EventHandler IsbnFieldTextChanged;

        public void ShowCouldNotFindBookDialog(string isbn)
        {
            MessageBox.Show("Could not find book with ISBN: " + isbn, "Search by ISBN", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        public void ShowConnectionErrorDialog()
        {
            MessageBox.Show("Could not connect to openlibrary.org. Check internet connection.", "Search by ISBN", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public void ShowAlreadyExistsWithIsbnDialog(string isbn)
        {
            MessageBox.Show("Book with ISBN10 or ISBN13: " + isbn + " already exists in database.", "Search by ISBN", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        public async Task ClickSearchButton()
        {
            await Task.Delay(100);
            this.searchButton.PerformClick();
        }

        private void searchButton_Click(object sender, EventArgs e)
        {
            SearchButtonClicked?.Invoke(sender, e);
        }//searchButton_Click

        public void ShowErrorDialog(string message)
        {
            MessageBox.Show(message, "Error getting book", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }//class
}
