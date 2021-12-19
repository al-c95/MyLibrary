using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using MyLibrary.BusinessLogic.Repositories;
using MyLibrary.Models.Entities;
using MyLibrary.Models.Entities.Builders;
using MyLibrary.Views;

namespace MyLibrary.Presenters
{
    public class AddBookPresenter
    {
        private BookRepository _bookRepo;
        private TagRepository _tagRepo;
        private AuthorRepository _authorRepo;
        private PublisherRepository _publisherRepo;

        private IAddBookForm _view;

        public AddBookPresenter(BookRepository bookRepository, TagRepository tagRepository, AuthorRepository authorRepository, PublisherRepository publisherRepository,
            IAddBookForm view)
        {
            this._bookRepo = bookRepository;
            this._tagRepo = tagRepository;
            this._authorRepo = authorRepository;
            this._publisherRepo = publisherRepository;

            this._view = view;

            // subscribe to the view's events
            this._view.SaveButtonClicked += SaveButtonClicked;
            this._view.InputFieldsUpdated += InputFieldsUpdated;
        }

        public async Task PopulateTagsList()
        {
            var allTags = await this._tagRepo.GetAll();
            List<string> tagNames = new List<string>();
            foreach (var tag in allTags)
                tagNames.Add(tag.Name);

            this._view.PopulateTagsList(tagNames);
        }

        public async Task PopulateAuthorList()
        {
            var allAuthors = await this._authorRepo.GetAll();
            List<string> authorNames = new List<string>();
            foreach (var author in allAuthors)
                authorNames.Add(author.GetFullNameLastNameCommaFirstName());

            this._view.PopulateAuthorList(authorNames);
        }

        public async Task PopulatePublisherList()
        {
            var allPublishers = await this._publisherRepo.GetAll();
            List<string> publisherNames = new List<string>();
            foreach (var publisher in allPublishers)
                publisherNames.Add(publisher.Name);

            this._view.PopulatePublisherList(publisherNames);
        }

        #region View event handlers
        public async void SaveButtonClicked(object sender, EventArgs e)
        {
            // check if item with title already exists
            bool exists = false;
            string existingTitle = null;
            try
            {
                if (await this._bookRepo.ExistsWithTitle(this._view.TitleFieldText))
                {
                    exists = true;
                    existingTitle = this._view.TitleFieldText;
                }
                if (await this._bookRepo.ExistsWithLongTitle(this._view.LongTitleFieldText))
                {
                    exists = true;
                    existingTitle = this._view.LongTitleFieldText;
                }
                if (exists)
                {
                    // title already exists
                    // tell the user
                    this._view.ShowItemAlreadyExistsDialog(existingTitle);

                    // nothing more to do
                    return;
                }
            }
            catch (Exception ex)
            {
                // something bad happened
                // notify the user
                this._view.ShowErrorDialog("Error checking if title exists.", ex.Message);
                return;
            }

            // create item
            List<Tag> tags = new List<Tag>();
            foreach (var tagName in this._view.SelectedTags)
                tags.Add(new Tag { Name = tagName });
            List<Author> authors = new List<Author>();
            foreach (var authorName in this._view.SelectedAuthors)
            {
                Author author = new Author();
                author.SetFullNameFromCommaFormat(authorName);
                authors.Add(author);
            }
            Book book = BookBuilder.CreateBook(this._view.TitleFieldText, this._view.LongTitleFieldText, new Publisher { Name = this._view.SelectedPublisher }, this._view.LanguageFieldText, int.Parse(this._view.PagesFieldText))
                .WithTags(tags)
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
            if (!string.IsNullOrWhiteSpace(this._view.ImageFilePathFieldText))
            {
                try
                {
                    byte[] imageBytes = System.IO.File.ReadAllBytes(this._view.ImageFilePathFieldText);
                    book.Image = imageBytes;
                }
                catch (System.IO.IOException ex)
                {
                    // I/O error
                    // alert the user
                    this._view.ShowErrorDialog("Image file error", ex.Message);
                    return;
                }
            }

            // add item
            try
            {
                await this._bookRepo.Create(book);
                this._view.ItemAddedFinished();
            }
            catch (Exception ex)
            {
                // something bad happened
                // notify the user
                this._view.ShowErrorDialog("Error creating book", ex.Message);
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
        #endregion
    }//class
}