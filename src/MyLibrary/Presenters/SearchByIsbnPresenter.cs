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
using System.Text.RegularExpressions;
using System.Net.Http;
using MyLibrary.Models.BusinessLogic;
using MyLibrary.Models.Entities;
using MyLibrary.Views;
using MyLibrary.ApiService;
using MyLibrary.Presenters.ServiceProviders;

namespace MyLibrary.Presenters
{
    public class SearchByIsbnPresenter
    {
        // injected values
        private ISearchByIsbn _view;
        private IAddBookForm _addBookView;
        private IBookService _bookService;
        private IApiServiceProvider _apiServiceProvider;

        public AddBookPresenter AddBookPresenter;

        /// <summary>
        /// Constructor with dependency injection.
        /// </summary>
        /// <param name="view"></param>
        public SearchByIsbnPresenter(ISearchByIsbn view, 
            IAddBookForm addBookView, 
            IBookService bookService, 
            IApiServiceProvider apiServiceProvider)
        {
            this._view = view;
            this._addBookView = addBookView;
            this._bookService = bookService;
            this._apiServiceProvider = apiServiceProvider;

            // subscribe to the view's events
            this._view.IsbnFieldTextChanged += IsbnFieldTextChanged;
            this._view.SearchButtonClicked += SearchButtonClicked;

            // enable scan mode by default
            this._view.ScanModeChecked = true;
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
            if (!sane)
            {
                return;
            }

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

            string enteredIsbn = this._view.IsbnFieldText;

            Book book = null;
            try
            {
                this._view.IsbnFieldEnabled = false;

                using (var apiService = this._apiServiceProvider.Get())
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
                {
                    this._view.ShowConnectionErrorDialog();
                }
            }
            catch (Exception ex)
            {
                // some other error
                this._view.ShowErrorDialog(ex.Message);
            }
            finally
            {
                this._view.IsbnFieldEnabled = true;
                Reset();
            }

            if (book != null)
            {
                // show add new book dialog and prefill with data
                await this.AddBookPresenter.PopulateTagsAsync();
                await this.AddBookPresenter.PopulateAuthorsAsync();
                await this.AddBookPresenter.PopulatePublishersAsync();
                this.AddBookPresenter.Prefill(book);
                this._addBookView.ShowAsDialog();
            }
        }//SearchButtonClicked
        #endregion

        private void Reset()
        {
            // clear, update status bar and re-enable buttons
            this._view.SearchButtonEnabled = false;
            this._view.CancelButtonEnabled = true;
            this._view.StatusLabelText = "Ready";
            this._view.IsbnFieldText = "";
        }
    }//class
}
