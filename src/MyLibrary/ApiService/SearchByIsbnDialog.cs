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
using MyLibrary.BusinessLogic.Repositories;
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

        public async Task ClickSearchButton()
        {
            await Task.Delay(100);
            this.searchButton.PerformClick();
        }

        private void searchButton_Click(object sender, EventArgs e)
        {
            SearchButtonClicked?.Invoke(sender, e);
        }//searchButton_Click
    }//class
}
