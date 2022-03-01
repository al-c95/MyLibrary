﻿//MIT License

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
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using MyLibrary.Models.BusinessLogic;
using MyLibrary.Models.Entities;
using MyLibrary.Models.Entities.Builders;
using MyLibrary.Views;
using MyLibrary.Utils;

namespace MyLibrary.Presenters
{
    public class AddBookPresenter
    {
        private IBookService _bookService;
        private ITagService _tagService;
        private IAuthorService _authorService;
        private IPublisherService _publisherService;

        private IAddBookForm _view;

        private IImageFileReader _imageFileReader;

        protected Dictionary<string, bool> _allTags;

        public AddBookPresenter(IBookService bookService, ITagService tagService, IAuthorService authorService, IPublisherService publisherService,
            IAddBookForm view,
            IImageFileReader imageFileReader)
        {
            this._bookService = bookService;
            this._tagService = tagService;
            this._authorService = authorService;
            this._publisherService = publisherService;

            this._view = view;

            this._imageFileReader = imageFileReader;

            this._allTags = new Dictionary<string, bool>();

            // subscribe to the view's events
            this._view.SaveButtonClicked += (async (sender, args) => 
            { 
                await HandleSaveButtonClicked(sender, args); 
            });
            this._view.InputFieldsUpdated += InputFieldsUpdated;
            this._view.NewAuthorFieldsUpdated += NewAuthorFieldsUpdated;
            this._view.NewPublisherFieldUpdated += NewPublisherFieldUpdated;
            this._view.FilterTagsFieldUpdated += FilterTags;
            this._view.AddNewTagButtonClicked += HandleAddNewTagClicked;
            this._view.TagCheckedChanged += HandleTagCheckedChanged;
        }

        public void Prefill(Book book)
        {
            // basic fields
            this._view.TitleFieldText = book.Title;
            this._view.LongTitleFieldText = book.TitleLong;
            this._view.IsbnFieldText = book.Isbn;
            this._view.Isbn13FieldText = book.Isbn13;
            this._view.DatePublishedFieldText = book.DatePublished;
            this._view.PlaceOfPublicationFieldText = book.PlaceOfPublication;
            this._view.PagesFieldText = book.Pages.ToString();
            this._view.LanguageFieldText = book.Language;

            // publisher
            this._view.SetPublisher(book.Publisher, true);

            // authors
            foreach (var author in book.Authors)
            {
                this._view.SetAuthor(author, true);
            }
        }

        public void FilterTags(object sender, EventArgs args)
        {
            // grab the filter
            const RegexOptions REGEX_OPTIONS = RegexOptions.IgnoreCase | RegexOptions.ExplicitCapture;
            Regex filterPattern = new Regex(this._view.FilterTagsFieldEntry, REGEX_OPTIONS);

            // perform filtering
            Dictionary<string, bool> filteredTags = new Dictionary<string, bool>();
            foreach (var kvp in this._allTags)
            {
                if (filterPattern.IsMatch(kvp.Key))
                {
                    filteredTags.Add(kvp.Key, kvp.Value);
                }
            }

            // update the view
            this._view.AddTags(filteredTags);
        }//FilterTags

        public async Task PopulateTagsList()
        {
            // load all tags
            var allTags = await this._tagService.GetAll();
            foreach (var tag in allTags)
            {
                this._allTags.Add(tag.Name, false);
            }

            // perform filtering and update the view
            FilterTags(null, null);
        }//PopulateTagsList

        public async Task PopulateAuthorList()
        {
            var allAuthors = await this._authorService.GetAll();
            List<string> authorNames = new List<string>();
            foreach (var author in allAuthors)
                authorNames.Add(author.GetFullNameLastNameCommaFirstName());

            this._view.PopulateAuthorList(authorNames);
        }

        public async Task PopulatePublisherList()
        {
            var allPublishers = await this._publisherService.GetAll();
            List<string> publisherNames = new List<string>();
            foreach (var publisher in allPublishers)
                publisherNames.Add(publisher.Name);

            this._view.PopulatePublisherList(publisherNames);
        }

        #region View event handlers
        public async Task HandleSaveButtonClicked(object sender, EventArgs e)
        {
            // disable buttons
            this._view.SaveButtonEnabled = false;
            this._view.CancelButtonEnabled = false;

            // check if item with title or ISBN already exists
            bool titleExists = false;
            string existingTitle = null;
            bool isbnExists = false;
            string existingIsbn = null;
            try
            {
                if (await this._bookService.ExistsWithTitle(this._view.TitleFieldText))
                {
                    titleExists = true;
                    existingTitle = this._view.TitleFieldText;
                }
                if (!string.IsNullOrWhiteSpace(this._view.LongTitleFieldText))
                {
                    if (await this._bookService.ExistsWithLongTitle(this._view.LongTitleFieldText))
                    {
                        titleExists = true;
                        existingTitle = this._view.LongTitleFieldText;
                    }          
                }

                string isbnEntry = this._view.IsbnFieldText;
                if (await this._bookService.ExistsWithIsbn(isbnEntry))
                {
                    if (!string.IsNullOrWhiteSpace(isbnEntry))
                    {
                        isbnExists = true;
                        existingIsbn = this._view.IsbnFieldText;
                    }
                }
                string isbn13Entry = this._view.Isbn13FieldText;
                if (await this._bookService.ExistsWithIsbn(isbn13Entry))
                {
                    if (!string.IsNullOrWhiteSpace(isbn13Entry))
                    {
                        isbnExists = true;
                        existingIsbn = this._view.Isbn13FieldText;
                    }
                }

                if (titleExists)
                {
                    // title already exists
                    // tell the user
                    this._view.ShowItemAlreadyExistsDialog(existingTitle);

                    // re-enable buttons
                    this._view.SaveButtonEnabled = true;
                    this._view.CancelButtonEnabled = true;

                    // nothing more to do
                    return;
                }

                if (isbnExists)
                {
                    // isbn already exists
                    // tell the user
                    this._view.ShowIsbnAlreadyExistsDialog(existingIsbn);

                    // re-enable buttons
                    this._view.SaveButtonEnabled = true;
                    this._view.CancelButtonEnabled = true;

                    // nothing more to do
                    return;
                }
            }
            catch (Exception ex)
            {
                // something bad happened
                // notify the user
                this._view.ShowErrorDialog("Error checking if title or ISBN exists.", ex.Message);

                // re-enable buttons
                this._view.SaveButtonEnabled = true;
                this._view.CancelButtonEnabled = true;

                return;
            }

            // create item
            // authors
            List<Author> authors = new List<Author>();
            foreach (var authorName in this._view.SelectedAuthors)
            {
                Author author = new Author();
                author.SetFullNameFromCommaFormat(authorName);
                authors.Add(author);
            }
            Book book = BookBuilder.CreateBook(this._view.TitleFieldText, this._view.LongTitleFieldText, 
                new Publisher { Name = this._view.SelectedPublisher }, this._view.LanguageFieldText, int.Parse(this._view.PagesFieldText))
                //.WithTags(tags)
                .WithAuthors(authors)
                .WithIsbn(this._view.IsbnFieldText)
                .WithIsbn13(this._view.Isbn13FieldText)
                .WithOverview(this._view.OverviewFieldText)
                .WithMsrp(this._view.MsrpFieldText)
                .WithSynopsys(this._view.SynopsysFieldText)
                .WithExcerpt(this._view.ExcerptFieldText)
                .Edition(this._view.EditionFieldText)
                .WithDeweyDecimal(this._view.DeweyDecimalFieldText)
                .PublishedIn(this._view.DatePublishedFieldText)
                .InFormat(this._view.FormatFieldText)
                .Sized(this._view.DimensionsFieldText)
                    .Get();
            book.Notes = this._view.NotesFieldText;
            book.PlaceOfPublication = this._view.PlaceOfPublicationFieldText;
            // tags
            foreach (var kvp in this._allTags)
            {
                if (this._allTags[kvp.Key] == true)
                {
                    book.Tags.Add(new Tag { Name = kvp.Key });
                }
            }
            // image
            if (!string.IsNullOrWhiteSpace(this._view.ImageFilePathFieldText))
            {
                try
                {
                    this._imageFileReader.Path = this._view.ImageFilePathFieldText;
                    byte[] imageBytes = this._imageFileReader.ReadBytes();
                    book.Image = imageBytes;
                }
                catch (System.IO.IOException ex)
                {
                    // I/O error
                    // alert the user
                    this._view.ShowErrorDialog("Image file error", ex.Message);

                    // re-enable buttons
                    this._view.SaveButtonEnabled = true;
                    this._view.CancelButtonEnabled = true;

                    return;
                }
            }

            // add item
            try
            {
                await this._bookService.Add(book);
                this._view.ItemAddedFinished();
            }
            catch (Exception ex)
            {
                // something bad happened
                // notify the user
                this._view.ShowErrorDialog("Error creating book", ex.Message);

                // re-enable buttons
                this._view.SaveButtonEnabled = true;
                this._view.CancelButtonEnabled = true;

                return;
            }
            
            this._view.CloseDialog();
        }

        public void InputFieldsUpdated(object sender, EventArgs e)
        {
            bool sane = true;
            // title field mandatory
            sane = sane && !string.IsNullOrWhiteSpace(this._view.TitleFieldText);
            // language field mandatory
            sane = sane && !string.IsNullOrWhiteSpace(this._view.LanguageFieldText);
            // number of pages field mandatory, must be an integer
            sane = sane && !string.IsNullOrWhiteSpace(this._view.PagesFieldText);
            int pages;
            sane = sane && (int.TryParse(this._view.PagesFieldText, out pages));
            // publisher selection mandatory
            sane = sane && this._view.SelectedPublisher != null;
            // ISBN fields must be either blank or have the appropriate pattern of digits
            sane = sane && (Regex.IsMatch(this._view.IsbnFieldText, Book.ISBN_10_PATTERN) || string.IsNullOrWhiteSpace(this._view.IsbnFieldText));
            sane = sane && (Regex.IsMatch(this._view.Isbn13FieldText, Book.ISBN_13_PATTERN) || string.IsNullOrWhiteSpace(this._view.Isbn13FieldText));
            // dewey decimal field must be either blank or have the appropriate pattern
            sane = sane && (Regex.IsMatch(this._view.DeweyDecimalFieldText, Book.DEWEY_DECIMAL_PATTERN) || string.IsNullOrWhiteSpace(this._view.DeweyDecimalFieldText));
            // image file path field must be either blank or a valid file path with supported extension
            string imageFilePath = this._view.ImageFilePathFieldText;
            sane = sane && (Item.IsValidImageFileType(imageFilePath) || string.IsNullOrWhiteSpace(imageFilePath));
            // don't care about the other fields

            this._view.SaveButtonEnabled = sane;
        }

        public void NewAuthorFieldsUpdated(object sender, EventArgs args)
        {
            string firstNameFieldEntry = this._view.NewAuthorFirstNameFieldText;
            string lastNameFieldEntry = this._view.NewAuthorLastNameFieldText;

            bool sane = true;
            sane = sane && Regex.IsMatch(firstNameFieldEntry, Author.NAME_PATTERN) || Regex.IsMatch(firstNameFieldEntry, Author.WITH_MIDDLE_NAME_PATTERN);
            sane = sane && Regex.IsMatch(lastNameFieldEntry, Author.NAME_PATTERN);

            this._view.AddNewAuthorButtonEnabled = sane;
        }

        public void NewPublisherFieldUpdated(object sender, EventArgs args)
        {
            this._view.AddNewPublisherButtonEnabled = !string.IsNullOrWhiteSpace(this._view.NewPublisherFieldText);
        }

        public void NewTagFieldUpdated(object sender, EventArgs args)
        {
            this._view.AddNewTagButtonEnabled = !string.IsNullOrWhiteSpace(this._view.NewTagFieldText);
        }

        public void HandleAddNewTagClicked(object sender, EventArgs args)
        {
            string newTag = this._view.ShowNewTagDialog();
            if (!string.IsNullOrWhiteSpace(newTag))
            {
                if (!this._allTags.ContainsKey(newTag))
                {
                    this._allTags.Add(newTag, true);

                    FilterTags(null, null);
                }
                else
                {
                    this._view.ShowTagAlreadyExistsDialog(newTag);
                }
            }
            else
            {
                return;
            }
        }//HandleAddNewTagClicked

        public void HandleTagCheckedChanged(object sender, EventArgs args)
        {
            foreach (var selectedTag in this._view.SelectedTags)
            {
                this._allTags[selectedTag] = true;
            }

            foreach (var unselectedTag in this._view.UnselectedTags)
            {
                this._allTags[unselectedTag] = false;
            }
        }
        #endregion
    }//class
}