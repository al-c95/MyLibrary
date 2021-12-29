using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Net;
using System.Net.Http;
using MyLibrary.BusinessLogic.Repositories;
using MyLibrary.Models.Entities;
using MyLibrary.Views;
using MyLibrary.ApiService;

namespace MyLibrary.Presenters
{
    public class SearchByIsbnPresenter
    {
        // injected values
        private ISearchByIsbn _view;
        private IItemView _mainView;

        /// <summary>
        /// Constructor with dependency injection.
        /// </summary>
        /// <param name="view"></param>
        public SearchByIsbnPresenter(ISearchByIsbn view, IItemView mainView)
        {
            this._view = view;
            this._mainView = mainView;

            // subscribe to the view's events
            this._view.IsbnFieldTextChanged += IsbnFieldTextChanged;
            this._view.SearchButtonClicked += SearchButtonClicked;
        }

        #region view event handlers
        public async void IsbnFieldTextChanged(object sender, EventArgs e)
        {
            string enteredIsbn = this._view.IsbnFieldText;

            // validate ISBN
            bool sane = true;
            if (!string.IsNullOrWhiteSpace(enteredIsbn))
            {
                sane = sane && (Regex.IsMatch(enteredIsbn, Book.ISBN_10_PATTERN) || (Regex.IsMatch(enteredIsbn, Book.ISBN_13_PATTERN)));
            }
            else
            {
                sane = false;
            }
            this._view.SearchButtonEnabled = sane;

            // start search automatically if scan mode enabled
            if (this._view.ScanModeChecked)
            {
                await this._view.ClickSearchButton();
            }
        }

        public async void SearchButtonClicked(object sender, EventArgs e)
        {
            // update status bar and disable buttons
            this._view.StatusLabelText = "Searching. Please wait...";
            this._view.SearchButtonEnabled = false;
            this._view.SearchButtonEnabled = false;

            Book book = null;
            string enteredIsbn = this._view.IsbnFieldText;
            try
            {
                using (var apiService = new BookApiService())
                {
                    book = await apiService.GetBookByIsbnAsync(enteredIsbn);
                }//apiService
            }
            catch (BookNotFoundException ex)
            {
                this._view.ShowCouldNotFindBookDialog(ex.Isbn);
            }
            catch (HttpRequestException ex)
            {
                if (ex.InnerException.Message.Equals("The remote name could not be resolved: 'openlibrary.org'"))
                    this._view.ShowConnectionErrorDialog();
            }
            finally
            {
                // clear, update status bar and re-enable buttons
                this._view.SearchButtonEnabled = true;
                this._view.CancelButtonEnabled = true;
                this._view.StatusLabelText = "Ready";
                this._view.IsbnFieldText = "";
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
            }
        }
        #endregion
    }//class
}
