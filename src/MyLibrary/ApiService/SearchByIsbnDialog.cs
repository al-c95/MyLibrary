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

namespace MyLibrary.ApiService
{
    public partial class SearchByIsbnDialog : Form
    {
        public SearchByIsbnDialog()
        {
            InitializeComponent();

            // register event handlers
            this.cancelButton.Click += ((sender, args) =>
            {
                this.Close();
            });
            this.isbnField.TextChanged += (async (sender, args) =>
            {
                // TODO: factor this out for unit testing
                bool sane = true;
                if (!string.IsNullOrWhiteSpace(this.isbnField.Text))
                {
                    sane = sane && (Regex.IsMatch(this.isbnField.Text, Book.ISBN_10_PATTERN) || (Regex.IsMatch(this.isbnField.Text, Book.ISBN_13_PATTERN)));
                }
                else
                {
                    sane = false;
                }

                this.searchButton.Enabled = sane;

                // start search automatically if scan mode enabled
                if (this.scanModecheckBox.Checked)
                {
                    await Task.Delay(100);
                    this.searchButton.PerformClick();
                }
            });

            this.StartPosition = FormStartPosition.CenterParent;
            this.searchButton.Enabled = false;
        }

        private async void searchButton_Click(object sender, EventArgs e)
        {
            // update status bar and disable buttons
            this.statusLabel.Text = "Searching. Please wait...";
            this.searchButton.Enabled = false;
            this.cancelButton.Enabled = false;

            Book book = null;
            string isbn = this.isbnField.Text;
            try
            {
                using (BookApiService apiService = new BookApiService())
                {
                    book = await apiService.GetBookByIsbnAsync(this.isbnField.Text);
                }//apiService
            }
            catch (BookNotFoundException ex)
            {
                MessageBox.Show("Could not find book with ISBN: " + ex.Isbn, "Search by ISBN", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            finally
            {
                // clear, update status bar and re-enable buttons
                this.searchButton.Enabled = true;
                this.cancelButton.Enabled = true;
                this.statusLabel.Text = "Ready";
                this.isbnField.Clear();
            }

            if (book != null)
            {
                // show add new book dialog and prefill with data
                AddNewBookForm addBookDialog = new AddNewBookForm();
                AddBookPresenter presenter = new AddBookPresenter(new BookRepository(), new TagRepository(), new AuthorRepository(), new PublisherRepository(),
                    addBookDialog);
                await presenter.PopulateTagsList();
                await presenter.PopulateAuthorList();
                await presenter.PopulatePublisherList();
                presenter.Prefill(book);

                addBookDialog.ShowDialog();

                // TODO: update the main list when item is saved
            }
        }//searchButton_Click
    }//class
}
