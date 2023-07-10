//MIT License

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
using MyLibrary.Models.Entities.Factories;
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
        private IBookFactory _bookFactory;
        public Book NewBook
        {
            get; set;
        }

        private IAddBookForm _view;

        private IImageFileReader _imageFileReader;

        public Dictionary<string, bool> AllTags;
        public Dictionary<string, bool> AllAuthors;
        public List<string> AllPublishers;

        public AddBookPresenter(IBookService bookService, ITagService tagService, IAuthorService authorService, IPublisherService publisherService,
            IBookFactory bookFactory,
            IAddBookForm view,
            IImageFileReader imageFileReader)
        {
            this._bookService = bookService;
            this._tagService = tagService;
            this._authorService = authorService;
            this._publisherService = publisherService;
            this._bookFactory = bookFactory;
            this.NewBook = new Book();

            this._view = view;

            this._imageFileReader = imageFileReader;

            this.AllTags = new Dictionary<string, bool>();
            this.AllAuthors = new Dictionary<string, bool>();
            this.AllPublishers = new List<string>();

            // subscribe to the view's events
            this._view.SaveButtonClicked += (async (sender, args) =>
            {
                await HandleSaveButtonClicked(sender, args);
            });
            this._view.InputFieldsUpdated += InputFieldsUpdated;
            this._view.FilterTagsFieldUpdated += FilterTags;
            this._view.FilterAuthorsFieldUpdated += FilterAuthors;
            this._view.FilterPublishersFieldUpdated += FilterPublishers;
            this._view.AddNewTagButtonClicked += ((sender, args) => 
            {
                HandleAddNewTagClicked(sender, args);
                InputFieldsUpdated(sender, args);
            });

            this._view.AddNewAuthorButtonClicked += ((sender, args) =>
            {
                HandleAddNewAuthorClicked(sender, args);
                InputFieldsUpdated(sender, args);
            }); 
            this._view.AddNewPublisherButtonClicked += HandleAddNewPublisherClicked;
            this._view.TagCheckedChanged += ((sender, args) =>
            {
                HandleTagCheckedChanged(sender, args);
                InputFieldsUpdated(sender, args);
            });
            this._view.AuthorCheckedChanged += ((sender, args) =>
            {
                HandleAuthorCheckedChanged(sender, args);
                InputFieldsUpdated(sender, args);
            });
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

            if (!this.AllPublishers.Contains(book.Publisher.Name))
            {
                this.AllPublishers.Add(book.Publisher.Name);
            }
            FilterPublishers(null, null);
            this._view.SetPublisher(book.Publisher, true);

            foreach (var key in this.AllAuthors.Keys.ToList())
            {
                this.AllAuthors[key] = false;
            }
            foreach (var author in book.Authors)
            {
                if (!this.AllAuthors.ContainsKey(author.GetFullNameLastNameCommaFirstName()))
                {
                    this.AllAuthors.Add(author.GetFullNameLastNameCommaFirstName(), true);
                }
                else
                {
                    this.AllAuthors[author.GetFullNameLastNameCommaFirstName()] = true;
                }
            }
            FilterAuthors(null, null);
        }//Prefill

        public void FilterTags(object sender, EventArgs args)
        {
            const RegexOptions REGEX_OPTIONS = RegexOptions.IgnoreCase | RegexOptions.ExplicitCapture;
            Regex filterPattern = new Regex(this._view.FilterTagsFieldEntry, REGEX_OPTIONS);

            Dictionary<string, bool> filteredTags = new Dictionary<string, bool>();
            foreach (var kvp in this.AllTags)
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
            foreach (var kvp in this.AllAuthors)
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
            foreach (var publisher in this.AllPublishers)
            {
                if (filterPattern.IsMatch(publisher))
                {
                    filteredPublishers.Add(publisher);
                }
            }

            this._view.AddPublishers(filteredPublishers);
        }//FilterPublishers

        public async Task PopulateAuthorsAsync()
        {
            var allAuthors = await this._authorService.GetAll();
            foreach (var author in allAuthors)
            {
                string authorName = author.GetFullNameLastNameCommaFirstName();
                if (!this.AllAuthors.ContainsKey(authorName))
                {
                    this.AllAuthors.Add(authorName, false);
                }
            }

            FilterAuthors(null, null);
        }//PopulateAuthorsList

        public async Task PopulateTagsAsync()
        {
            var allTags = await this._tagService.GetAll();
            foreach (var tag in allTags)
            {
                string tagName = tag.Name;
                if (!this.AllTags.ContainsKey(tagName))
                {
                    this.AllTags.Add(tag.Name, false);
                }
            }

            FilterTags(null, null);
        }//PopulateTagsList

        public async Task PopulatePublishersAsync()
        {
            var allPublishers = await this._publisherService.GetAll();
            foreach (var publisher in allPublishers)
            {
                string publisherName = publisher.Name;
                if (!this.AllPublishers.Contains(publisherName))
                {
                    this.AllPublishers.Add(publisher.Name);
                }
            }

            FilterPublishers(null, null);
        }//PopulatePublishersList

        private IEnumerable<string> GetSelectedTags()
        {
            foreach (var kvp in this.AllTags)
            {
                if (this.AllTags[kvp.Key])
                {
                    yield return kvp.Key;
                }
            }
        }

        private IEnumerable<string> GetSelectedAuthors()
        {
            foreach (var kvp in this.AllAuthors)
            {
                if (this.AllAuthors[kvp.Key])
                {
                    yield return kvp.Key;
                }
            }
        }

        private void EnableButtons(bool save, bool cancel)
        {
            this._view.SaveButtonEnabled = save;
            this._view.CancelButtonEnabled = cancel;
        }

        private void HandleImageFile()
        {
            try
            {
                this._imageFileReader.Path = this._view.ImageFilePathFieldText;
                byte[] imageBytes = this._imageFileReader.ReadBytes();
                this.NewBook.Image = imageBytes;
            }
            catch (IOException ex)
            {
                this._view.ShowErrorDialog("Image file error", ex.Message);
                EnableButtons(true, true);

                return;
            }
        }

        #region View event handlers
        public async Task HandleSaveButtonClicked(object sender, EventArgs e)
        {
            EnableButtons(false, false);

            if (!string.IsNullOrWhiteSpace(this._view.ImageFilePathFieldText))
            {
                HandleImageFile();
            }

            try
            {
                bool added = await this._bookService.AddIfNotExistsAsync(this.NewBook);
                if (!added)
                {
                    this._view.ShowItemAlreadyExistsDialog(this._view.TitleFieldText);
                    EnableButtons(true, true);

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
                EnableButtons(true, true);

                return;
            }

            this._view.CloseDialog();
        }//HandleSaveButtonClicked

        public void InputFieldsUpdated(object sender, EventArgs e)
        {
            try
            {
                var selectedTags = GetSelectedTags().ToList();
                var selectedAuthors = GetSelectedAuthors().ToList();
                this.NewBook = this._bookFactory.Create(new BookFactory.Titles { Title=this._view.TitleFieldText, LongTitle=this._view.LongTitleFieldText}, 
                    new BookFactory.Isbns { Isbn10=this._view.IsbnFieldText, Isbn13=this._view.Isbn13FieldText},
                    this._view.PagesFieldText, this._view.LanguageFieldText, this._view.SelectedPublisher, this._view.DeweyDecimalFieldText, selectedTags, selectedAuthors);
                this.NewBook.Notes = this._view.NotesFieldText;
                this.NewBook.Msrp = this._view.MsrpFieldText;
                this.NewBook.Synopsys = this._view.SynopsysFieldText;
                this.NewBook.Excerpt = this._view.ExcerptFieldText;
                this.NewBook.DatePublished = this._view.DatePublishedFieldText;
                this.NewBook.PlaceOfPublication = this._view.PlaceOfPublicationFieldText;
                this.NewBook.Edition = this._view.EditionFieldText;
                this.NewBook.Format = this._view.FormatFieldText;
                this.NewBook.Dimensions = this._view.DimensionsFieldText;
                this.NewBook.Overview = this._view.OverviewFieldText;

                this._view.SaveButtonEnabled = true;

                if (!string.IsNullOrWhiteSpace(this._view.ImageFilePathFieldText))
                {
                    this._view.SaveButtonEnabled = ImageFileReader.ValidateFilePath(this._view.ImageFilePathFieldText);
                }
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
                if (!this.AllTags.ContainsKey(newTag))
                {
                    this.AllTags.Add(newTag, true);

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

        public void HandleAddNewAuthorClicked(object sender, EventArgs args)
        {
            var result = this._view.ShowNewAuthorDialog();
            if (result is null)
            {
                return;
            }

            dynamic author = new Author($"{result.FirstName} {result.LastName}");
            author = ((Author)author).GetFullNameLastNameCommaFirstName();
            if (!this.AllAuthors.ContainsKey(author))
            {
                this.AllAuthors.Add(author, true);

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
                this.AllTags[selectedTag] = true;
            }

            foreach (var unselectedTag in this._view.UnselectedTags)
            {
                this.AllTags[unselectedTag] = false;
            }
        }//HandleTagCheckedChanged

        public void HandleAuthorCheckedChanged(object sender, EventArgs args)
        {
            foreach (var selectedAuthor in this._view.SelectedAuthors)
            {
                this.AllAuthors[selectedAuthor] = true;
            }

            foreach (var unselectedAuthor in this._view.UnselectedAuthors)
            {
                this.AllAuthors[unselectedAuthor] = false;
            }
        }//HandleAuthorCheckedChanged

        public void HandleAddNewPublisherClicked(object sender, EventArgs args)
        {
            string newPublisher = this._view.ShowNewPublisherDialog();
            if (!string.IsNullOrWhiteSpace(newPublisher))
            {
                if (!this.AllPublishers.Contains(newPublisher))
                {
                    this.AllPublishers.Add(newPublisher);

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