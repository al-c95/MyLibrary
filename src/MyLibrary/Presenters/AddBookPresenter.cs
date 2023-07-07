﻿//MIT License

//Copyright (c) 2021-2023

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
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using MyLibrary.Models.BusinessLogic;
using MyLibrary.Models.Entities;
using MyLibrary.Models.Entities.Builders;
using MyLibrary.Views;
using MyLibrary.Utils;
using MyLibrary.Events;
using System.IO;

namespace MyLibrary.Presenters
{
    /// <summary>
    /// Contains most of the logic behind the add new book window.
    /// </summary>
    public class AddBookPresenter
    {
        private IBookService _bookService;
        private ITagService _tagService;
        private IAuthorService _authorService;
        private IPublisherService _publisherService;
        private Book _newBook;

        private IAddBookForm _view;

        private IImageFileReader _imageFileReader;

        protected Dictionary<string, bool> _allTags;
        protected Dictionary<string, bool> _allAuthors;
        protected List<string> _allPublishers;

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
            this._allAuthors = new Dictionary<string, bool>();
            this._allPublishers = new List<string>();

            // subscribe to the view's events
            this._view.SaveButtonClicked += (async (sender, args) =>
            {
                await HandleSaveButtonClicked(sender, args);
            });
            this._view.InputFieldsUpdated += InputFieldsUpdated;
            this._view.FilterTagsFieldUpdated += FilterTags;
            this._view.FilterAuthorsFieldUpdated += FilterAuthors;
            this._view.FilterPublishersFieldUpdated += FilterPublishers;
            this._view.AddNewTagButtonClicked += HandleAddNewTagClicked;
            this._view.AddNewAuthorButtonClicked += HandleAddNewAuthorClicked;
            this._view.AddNewPublisherButtonClicked += HandleAddNewPublisherClicked;
            this._view.TagCheckedChanged += HandleTagCheckedChanged;
            this._view.AuthorCheckedChanged += HandleAuthorCheckedChanged;
        }

        /// <summary>
        /// Given a book object, fill the fields in the form with the available data.
        /// </summary>
        /// <param name="book"></param>
        public void Prefill(Book book)
        {
            this._view.TitleFieldText = "";
            this._view.LongTitleFieldText = "";
            this._view.IsbnFieldText = "";
            this._view.Isbn13FieldText = "";
            this._view.OverviewFieldText = "";
            this._view.MsrpFieldText = "";
            this._view.PagesFieldText = "";
            this._view.SynopsysFieldText = "";
            this._view.ExcerptFieldText = "";
            this._view.EditionFieldText = "";
            this._view.DeweyDecimalFieldText = "";
            this._view.DimensionsFieldText = "";
            this._view.DatePublishedFieldText = "";
            this._view.PlaceOfPublicationFieldText = "";
            this._view.PagesFieldText = "";
            this._view.LanguageFieldText = "";
            this._view.UncheckAllAuthors();
            this._view.UncheckAllTags();

            this._view.TitleFieldText = book.Title;
            this._view.LongTitleFieldText = book.TitleLong;
            this._view.IsbnFieldText = book.Isbn;
            this._view.Isbn13FieldText = book.Isbn13;
            this._view.DatePublishedFieldText = book.DatePublished;
            this._view.PlaceOfPublicationFieldText = book.PlaceOfPublication;
            this._view.PagesFieldText = book.Pages.ToString();
            this._view.LanguageFieldText = book.Language;

            if (!this._allPublishers.Contains(book.Publisher.Name))
            {
                this._allPublishers.Add(book.Publisher.Name);
            }
            FilterPublishers(null, null);
            this._view.SetPublisher(book.Publisher, true);

            foreach (var key in this._allAuthors.Keys.ToList())
            {
                this._allAuthors[key] = false;
            }
            foreach (var author in book.Authors)
            {
                if (!this._allAuthors.ContainsKey(author.GetFullNameLastNameCommaFirstName()))
                {
                    this._allAuthors.Add(author.GetFullNameLastNameCommaFirstName(), true);
                }
                else
                {
                    this._allAuthors[author.GetFullNameLastNameCommaFirstName()] = true;
                }
            }
            FilterAuthors(null, null);
        }//Prefill

        public void FilterTags(object sender, EventArgs args)
        {
            const RegexOptions REGEX_OPTIONS = RegexOptions.IgnoreCase | RegexOptions.ExplicitCapture;
            Regex filterPattern = new Regex(this._view.FilterTagsFieldEntry, REGEX_OPTIONS);

            Dictionary<string, bool> filteredTags = new Dictionary<string, bool>();
            foreach (var kvp in this._allTags)
            {
                if (filterPattern.IsMatch(kvp.Key))
                {
                    filteredTags.Add(kvp.Key, kvp.Value);
                }
            }

            this._view.AddTags(filteredTags);
        }//FilterTags

        public void FilterAuthors(object sender, EventArgs args)
        {
            const RegexOptions REGEX_OPTIONS = RegexOptions.IgnoreCase | RegexOptions.ExplicitCapture;
            Regex filterPattern = new Regex(this._view.FilterAuthorsFieldEntry, REGEX_OPTIONS);

            Dictionary<string, bool> filteredAuthors = new Dictionary<string, bool>();
            foreach (var kvp in this._allAuthors)
            {
                if (filterPattern.IsMatch(kvp.Key))
                {
                    filteredAuthors.Add(kvp.Key, kvp.Value);
                }
            }

            this._view.AddAuthors(filteredAuthors);
        }//FilterAuthors

        public void FilterPublishers(object sender, EventArgs args)
        {
            const RegexOptions REGEX_OPTIONS = RegexOptions.IgnoreCase | RegexOptions.ExplicitCapture;
            Regex filterPattern = new Regex(this._view.FilterPublishersFieldEntry, REGEX_OPTIONS);

            List<string> filteredPublishers = new List<string>();
            foreach (var publisher in this._allPublishers)
            {
                if (filterPattern.IsMatch(publisher))
                {
                    filteredPublishers.Add(publisher);
                }
            }

            this._view.AddPublishers(filteredPublishers);
        }//FilterPublishers

        public async Task PopulateAuthorsList()
        {
            var allAuthors = await this._authorService.GetAll();
            foreach (var author in allAuthors)
            {
                string authorName = author.GetFullNameLastNameCommaFirstName();
                if (!this._allAuthors.ContainsKey(authorName))
                {
                    this._allAuthors.Add(authorName, false);
                }
            }

            FilterAuthors(null, null);
        }//PopulateAuthorsList

        public async Task PopulateTagsList()
        {
            var allTags = await this._tagService.GetAll();
            foreach (var tag in allTags)
            {
                string tagName = tag.Name;
                if (!this._allTags.ContainsKey(tagName))
                {
                    this._allTags.Add(tag.Name, false);
                }
            }

            FilterTags(null, null);
        }//PopulateTagsList

        public async Task PopulatePublishersList()
        {
            var allPublishers = await this._publisherService.GetAll();
            foreach (var publisher in allPublishers)
            {
                string publisherName = publisher.Name;
                if (!this._allPublishers.Contains(publisherName))
                {
                    this._allPublishers.Add(publisher.Name);
                }
            }

            FilterPublishers(null, null);
        }//PopulatePublishersList

        #region View event handlers
        public async Task HandleSaveButtonClicked(object sender, EventArgs e)
        {
            this._view.SaveButtonEnabled = false;
            this._view.CancelButtonEnabled = false;

            List<Author> authors = new List<Author>();
            foreach (var kvp in this._allAuthors)
            {
                if (this._allAuthors[kvp.Key] == true)
                {
                    Author author = new Author();
                    author.SetFullNameFromCommaFormat(kvp.Key);
                    authors.Add(author);
                }
            }
            this._newBook.Authors = authors;
            this._newBook.Overview = this._view.OverviewFieldText;
            this._newBook.Msrp = this._view.MsrpFieldText;
            this._newBook.Synopsys = this._view.SynopsysFieldText;
            this._newBook.Excerpt = this._view.ExcerptFieldText;
            this._newBook.Edition = this._view.EditionFieldText;
            this._newBook.Format = this._view.FormatFieldText;
            this._newBook.Dimensions = this._view.DimensionsFieldText;
            this._newBook.Notes = this._view.NotesFieldText;
            this._newBook.PlaceOfPublication = this._view.PlaceOfPublicationFieldText;
            foreach (var kvp in this._allTags)
            {
                if (this._allTags[kvp.Key] == true)
                {
                    this._newBook.Tags.Add(new Tag 
                    { 
                        Name = kvp.Key 
                    });
                }
            }

            try
            {
                this._imageFileReader.Path = this._view.ImageFilePathFieldText;
                byte[] imageBytes = this._imageFileReader.ReadBytes();
                this._newBook.Image = imageBytes;
            }
            catch (IOException ex)
            {
                // error reading the image
                // alert the user
                this._view.ShowErrorDialog("Image file error", ex.Message);

                this._view.SaveButtonEnabled = true;
                this._view.CancelButtonEnabled = true;

                return;
            }

            try
            {
                bool added = await this._bookService.AddIfNotExistsAsync(this._newBook);
                if (!added)
                {
                    this._view.ShowItemAlreadyExistsDialog(this._view.TitleFieldText);

                    this._view.SaveButtonEnabled = true;
                    this._view.CancelButtonEnabled = true;

                    return;
                }
                else
                {
                    EventAggregator.GetInstance().Publish(new MediaItemsUpdatedEvent());
                }
            }
            catch (Exception ex)
            {
                // something bad happened
                // notify the user
                this._view.ShowErrorDialog("Error creating item", ex.Message);

                this._view.SaveButtonEnabled = true;
                this._view.CancelButtonEnabled = true;

                return;
            }

            this._view.CloseDialog();
        }//HandleSaveButtonClicked

        public void InputFieldsUpdated(object sender, EventArgs e)
        {
            try
            {
                BookBuilder builder = new BookBuilder();
                this._newBook = builder
                    .WithTitles(this._view.TitleFieldText, this._view.LongTitleFieldText)
                    .InLanguage(this._view.LanguageFieldText)
                    .WithIsbns(this._view.IsbnFieldText, this._view.Isbn13FieldText)
                    .WithPages(this._view.PagesFieldText)
                    .WithDeweyDecimal(this._view.DeweyDecimalFieldText)
                    .PublishedBy(this._view.SelectedPublisher)
                    .Build();

                bool enableSave = true;
                if (!string.IsNullOrWhiteSpace(this._view.ImageFilePathFieldText))
                {
                    enableSave = enableSave && ImageFileReader.ValidateFilePath(this._view.ImageFilePathFieldText);
                }

                this._view.SaveButtonEnabled = enableSave;
            }
            catch (ArgumentException) 
            {
                this._view.SaveButtonEnabled=false;
            }
        }//InputFieldsUpdated

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

        // TODO: unit test
        public void HandleAddNewAuthorClicked(object sender, EventArgs args)
        {
            var result = this._view.ShowNewAuthorDialog();
            if (result is null)
            {
                return;
            }

            dynamic author = new Author($"{result.FirstName} {result.LastName}");
            author = ((Author)author).GetFullNameLastNameCommaFirstName();
            if (!this._allAuthors.ContainsKey(author))
            {
                this._allAuthors.Add(author, true);

                FilterAuthors(null, null);
            }
            else
            {
                this._view.ShowAuthorAlreadyExistsDialog(author);
            }
        }//HandleAddNewAuthorClicked

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
        }//HandleTagCheckedChanged

        public void HandleAuthorCheckedChanged(object sender, EventArgs args)
        {
            foreach (var selectedAuthor in this._view.SelectedAuthors)
            {
                this._allAuthors[selectedAuthor] = true;
            }

            foreach (var unselectedAuthor in this._view.UnselectedAuthors)
            {
                this._allAuthors[unselectedAuthor] = false;
            }
        }//HandleAuthorCheckedChanged

        // TODO: unit test
        public void HandleAddNewPublisherClicked(object sender, EventArgs args)
        {
            string newPublisher = this._view.ShowNewPublisherDialog();
            if (!string.IsNullOrWhiteSpace(newPublisher))
            {
                if (!this._allPublishers.Contains(newPublisher))
                {
                    this._allPublishers.Add(newPublisher);

                    FilterPublishers(null, null);
                }
                else
                {
                    this._view.ShowPublisherAlreadyExistsDialog(newPublisher);
                }
            }
            else
            {
                return;
            }
        }//HandleAddNewPublisherClicked
        #endregion
    }//class
}