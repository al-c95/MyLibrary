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
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.IO;
using NUnit.Framework;
using FakeItEasy;
using MyLibrary.Models.BusinessLogic;
using MyLibrary.Models.Entities;
using MyLibrary.Views;
using MyLibrary.Presenters;
using MyLibrary.Utils;
using MyLibrary.Models.Entities.Factories;
using MyLibrary;
using MyLibrary.Import;

namespace MyLibrary_Test.Presenters_Tests
{
    [TestFixture]
    class AddBookPresenter_Tests
    {
        [Test]
        public async Task PopulateTagsAsync_Test()
        {
            // arrange
            var fakeView = A.Fake<IAddBookForm>();
            A.CallTo(() => fakeView.FilterTagsFieldEntry).Returns("");
            var fakeBookService = A.Fake<IBookService>();
            var fakeTagService = A.Fake<ITagService>();
            List<Tag> tags = new List<Tag> { new Tag { Name = "tag" } };
            A.CallTo(() => fakeTagService.GetAll()).Returns(tags);
            var fakeImageFileReader = A.Fake<IImageFileReader>();
            AddBookPresenter presenter = new AddBookPresenter(fakeBookService, fakeTagService, null, null, null, fakeView, fakeImageFileReader);

            // act
            await presenter.PopulateTagsAsync();

            // assert
            Assert.AreEqual(1, presenter.AllTags.Count);
            Assert.IsFalse(presenter.AllTags["tag"]);
        }

        [Test]
        public async Task PopulatePublishersAsync_Test()
        {
            // arrange
            var fakeView = A.Fake<IAddBookForm>();
            A.CallTo(() => fakeView.FilterPublishersFieldEntry).Returns("");
            var fakeBookService = A.Fake<IBookService>();
            var fakePublisherService = A.Fake<IPublisherService>();
            List<Publisher> publishers = new List<Publisher> { new Publisher { Name = "publisher" } };
            A.CallTo(() => fakePublisherService.GetAll()).Returns(publishers);
            var fakeImageFileReader = A.Fake<IImageFileReader>();
            AddBookPresenter presenter = new AddBookPresenter(fakeBookService, null, null, fakePublisherService, null, fakeView, fakeImageFileReader);

            // act
            await presenter.PopulatePublishersAsync();

            // assert
            Assert.AreEqual(1, presenter.AllPublishers.Count);
            Assert.IsTrue(presenter.AllPublishers.Any(p => p == "publisher"));
        }

        [Test]
        public async Task PopulateAuthorsAsync_Test()
        {
            // arrange
            var fakeView = A.Fake<IAddBookForm>();
            A.CallTo(() => fakeView.FilterAuthorsFieldEntry).Returns("");
            var fakeBookService = A.Fake<IBookService>();
            var fakeAuthorService = A.Fake<IAuthorService>();
            List<Author> authors = new List<Author> { new Author { FirstName = "John", LastName = "Smith" } };
            A.CallTo(() => fakeAuthorService.GetAll()).Returns(authors);
            var fakeImageFileReader = A.Fake<IImageFileReader>();
            AddBookPresenter presenter = new AddBookPresenter(fakeBookService, null, fakeAuthorService, null, null, fakeView, fakeImageFileReader);

            // act
            await presenter.PopulateAuthorsAsync();

            // assert
            Assert.AreEqual(1, presenter.AllAuthors.Count);
            Assert.IsFalse(presenter.AllAuthors["Smith, John"]);
        }

        [Test]
        public async Task HandleSaveButtonClicked_Test_ErrorAddingItem()
        {
            // arrange
            var fakeView = A.Fake<IAddBookForm>();
            var fakeBookService = A.Fake<IBookService>();
            Book book = new Book
            {
                Title = "Book",
                Pages=100,
                Language="English",
                Publisher = new Publisher
                {
                    Name="publisher"
                }
            };
            A.CallTo(() => fakeBookService.AddIfNotExistsAsync(book)).Throws(new Exception("error"));
            var fakeImageFileReader = A.Fake<IImageFileReader>();
            AddBookPresenter presenter = new AddBookPresenter(fakeBookService, null, null, null, null, fakeView, fakeImageFileReader);
            presenter.NewBook = book;

            // act
            await presenter.HandleSaveButtonClicked(null, null);

            // assert
            Assert.IsTrue(fakeView.SaveButtonEnabled);
            Assert.IsTrue(fakeView.CancelButtonEnabled);
            A.CallTo(() => fakeView.ShowErrorDialog("Error creating item", "error")).MustHaveHappened();
        }

        [Test]
        public async Task HandleSaveButtonClicked_Test_ErrorAddingImageFile()
        {
            // arrange
            var fakeView = A.Fake<IAddBookForm>();
            A.CallTo(() => fakeView.ImageFilePathFieldText).Returns(@"C:\path\to\image.jpeg");
            var fakeBookService = A.Fake<IBookService>();
            var fakeImageFileReader = A.Fake<IImageFileReader>();
            A.CallTo(() => fakeImageFileReader.ReadBytes()).Throws(new IOException("error"));
            AddBookPresenter presenter = new AddBookPresenter(fakeBookService, null, null, null, null, fakeView, fakeImageFileReader);

            // act
            await presenter.HandleSaveButtonClicked(null, null);

            // assert
            A.CallTo(() => fakeView.ShowErrorDialog("Image file error", "error")).MustHaveHappened();
            Assert.IsTrue(fakeView.SaveButtonEnabled);
            Assert.IsTrue(fakeView.CancelButtonEnabled);
        }

        [Test]
        public async Task HandleSaveButtonClicked_Test_BookAlreadyExists()
        {
            // arrange
            var fakeView = A.Fake<IAddBookForm>();
            A.CallTo(() => fakeView.TitleFieldText).Returns("Book");
            var fakeBookService = A.Fake<IBookService>();
            Book book = new Book
            {
                Title = "Book",
                Pages = 100,
                Language = "English",
                Publisher = new Publisher
                {
                    Name = "publisher"
                }
            };
            A.CallTo(() => fakeBookService.AddIfNotExistsAsync(book)).Returns(false);
            AddBookPresenter presenter = new AddBookPresenter(fakeBookService, null, null, null, null, fakeView, null);
            presenter.NewBook = book;

            // act
            await presenter.HandleSaveButtonClicked(null, null);

            // assert
            A.CallTo(() => fakeView.ShowItemAlreadyExistsDialog("Book")).MustHaveHappened();
            Assert.IsTrue(fakeView.SaveButtonEnabled);
            Assert.IsTrue(fakeView.CancelButtonEnabled);
        }

        [Test]
        public async Task HandleSaveButtonClicked_Test_BookDoesNotYetExist()
        {
            // arrange
            var fakeView = A.Fake<IAddBookForm>();
            A.CallTo(() => fakeView.TitleFieldText).Returns("Book");
            var fakeBookService = A.Fake<IBookService>();
            Book book = new Book
            {
                Title = "Book",
                Pages = 100,
                Language = "English",
                Publisher = new Publisher
                {
                    Name = "publisher"
                }
            };
            A.CallTo(() => fakeBookService.AddIfNotExistsAsync(book)).Returns(true);
            AddBookPresenter presenter = new AddBookPresenter(fakeBookService, null, null, null, null, fakeView, null);
            presenter.NewBook = book;

            // act
            await presenter.HandleSaveButtonClicked(null, null);

            // assert
            A.CallTo(() => fakeView.ShowItemAlreadyExistsDialog("Book")).MustNotHaveHappened();
        }

        [TestCase(@"C:\path\to\file.docx")]
        [TestCase(@"C:\path\to\<>:|?*file.jpeg")]
        public void InputFieldsUpdated_Test_AllInvalid(string imagePath)
        {
            // arrange
            var fakeView = A.Fake<IAddBookForm>();
            A.CallTo(() => fakeView.ImageFilePathFieldText).Returns(imagePath);
            A.CallTo(() => fakeView.TitleFieldText).Returns("");
            A.CallTo(() => fakeView.LongTitleFieldText).Returns("");
            A.CallTo(() => fakeView.IsbnFieldText).Returns("bogus isbn10");
            A.CallTo(() => fakeView.Isbn13FieldText).Returns("bogus isbn13");
            A.CallTo(() => fakeView.DeweyDecimalFieldText).Returns("bogus Dewey decimal");
            A.CallTo(() => fakeView.LanguageFieldText).Returns("");
            A.CallTo(() => fakeView.SelectedPublisher).Returns("");
            A.CallTo(() => fakeView.PagesFieldText).Returns("");
            A.CallTo(() => fakeView.SelectedTags).Returns(new List<string> { "tag1" });
            A.CallTo(() => fakeView.SelectedAuthors).Returns(new List<string> { "Smith, John" });
            var fakeBookFactory = A.Fake<IBookFactory>();
            A.CallTo(() => fakeBookFactory.Create(new BookFactory.Titles { Title="", LongTitle=""}, new BookFactory.Isbns { Isbn10="bogus isbn10", Isbn13="bogus isbn13"},
                "", "", "", "bogus Dewey decimal", A<List<string>>.That.IsSameSequenceAs(new List<string> { "tag1" }), A<List<string>>.That.IsSameSequenceAs(new List<string> { "Smith, John"}))).Throws(new ArgumentException());
            AddBookPresenter presenter = new AddBookPresenter(null, null, null, null, fakeBookFactory, fakeView, null);

            // act
            presenter.InputFieldsUpdated(null, null);

            // assert
            Assert.IsFalse(fakeView.SaveButtonEnabled);
        }

        [TestCase(@"C:\path\to\file.docx")]
        [TestCase(@"C:\path\to\<>:|?*file.jpeg")]
        public void InputFieldsUpdated_Test_ValidItemDetails_InvalidImagePath(string imagePath)
        {
            // arrange
            var fakeView = A.Fake<IAddBookForm>();
            A.CallTo(() => fakeView.ImageFilePathFieldText).Returns(imagePath);
            A.CallTo(() => fakeView.TitleFieldText).Returns("book");
            A.CallTo(() => fakeView.LongTitleFieldText).Returns("long book");
            A.CallTo(() => fakeView.IsbnFieldText).Returns("0123456789");
            A.CallTo(() => fakeView.Isbn13FieldText).Returns("012345678901X");
            A.CallTo(() => fakeView.DeweyDecimalFieldText).Returns("0");
            A.CallTo(() => fakeView.LanguageFieldText).Returns("English");
            A.CallTo(() => fakeView.SelectedPublisher).Returns("publisher");
            A.CallTo(() => fakeView.PagesFieldText).Returns("100");
            A.CallTo(() => fakeView.SelectedTags).Returns(new List<string> { "tag1" });
            A.CallTo(() => fakeView.SelectedAuthors).Returns(new List<string> { "Smith, John" });
            var fakeBookFactory = A.Fake<IBookFactory>();
            A.CallTo(() => fakeBookFactory.Create(new BookFactory.Titles { Title = "", LongTitle = "" }, new BookFactory.Isbns { Isbn10 = "0123456789", Isbn13 = "012345678901X" },
                "100", "English", "publisher", "0", A<List<string>>.That.IsSameSequenceAs(new List<string> { "tag1" }), A<List<string>>.That.IsSameSequenceAs(new List<string> { "Smith, John" }))).Throws(new ArgumentException());
            AddBookPresenter presenter = new AddBookPresenter(null, null, null, null, fakeBookFactory, fakeView, null);

            // act
            presenter.InputFieldsUpdated(null, null);

            // assert
            Assert.IsFalse(fakeView.SaveButtonEnabled);
        }

        [TestCase(@"C:\path\to\image.png")]
        [TestCase(@"C:\path\to\image.jpg")]
        [TestCase(@"C:\path\to\image.jpeg")]
        [TestCase(@"C:\path\to\image.bmp")]
        public void InputFieldsUpdated_Test_InvalidItemDetails_ValidImagePath(string imagePath)
        {
            // arrange
            var fakeView = A.Fake<IAddBookForm>();
            A.CallTo(() => fakeView.ImageFilePathFieldText).Returns(imagePath);
            A.CallTo(() => fakeView.TitleFieldText).Returns("");
            A.CallTo(() => fakeView.LongTitleFieldText).Returns("");
            A.CallTo(() => fakeView.IsbnFieldText).Returns("bogus isbn10");
            A.CallTo(() => fakeView.Isbn13FieldText).Returns("bogus isbn13");
            A.CallTo(() => fakeView.DeweyDecimalFieldText).Returns("bogus Dewey decimal");
            A.CallTo(() => fakeView.LanguageFieldText).Returns("");
            A.CallTo(() => fakeView.SelectedPublisher).Returns("");
            A.CallTo(() => fakeView.PagesFieldText).Returns("");
            A.CallTo(() => fakeView.SelectedTags).Returns(new List<string> { "tag1" });
            A.CallTo(() => fakeView.SelectedAuthors).Returns(new List<string> { "Smith, John" });
            var fakeBookFactory = A.Fake<IBookFactory>();
            A.CallTo(() => fakeBookFactory.Create(new BookFactory.Titles { Title = "", LongTitle = "" }, new BookFactory.Isbns { Isbn10 = "bogus isbn10", Isbn13 = "bogus isbn13" },
                "", "", "", "bogus Dewey decimal", A<List<string>>.That.IsSameSequenceAs(new List<string> { "tag1" }), A<List<string>>.That.IsSameSequenceAs(new List<string> { "Smith, John" }))).Throws(new ArgumentException());
            AddBookPresenter presenter = new AddBookPresenter(null, null, null, null, fakeBookFactory, fakeView, null);
            Dictionary<string, bool> allTags = new Dictionary<string, bool>();
            allTags.Add("tag1", true);
            allTags.Add("tag2", false);
            presenter.AllTags = allTags;
            Dictionary<string, bool> allAuthors = new Dictionary<string, bool>();
            allAuthors.Add("Smith, John", true);
            presenter.AllAuthors = allAuthors;

            // act
            presenter.InputFieldsUpdated(null, null);

            // assert
            Assert.IsFalse(fakeView.SaveButtonEnabled);
        }

        [TestCase(@"C:\path\to\image.png")]
        [TestCase(@"C:\path\to\image.jpg")]
        [TestCase(@"C:\path\to\image.jpeg")]
        [TestCase(@"C:\path\to\image.bmp")]
        public void InputFieldsUpdated_Test_AllValid(string imagePath)
        {
            // arrange
            var fakeView = A.Fake<IAddBookForm>();
            A.CallTo(() => fakeView.ImageFilePathFieldText).Returns(imagePath);
            A.CallTo(() => fakeView.TitleFieldText).Returns("book");
            A.CallTo(() => fakeView.LongTitleFieldText).Returns("long book");
            A.CallTo(() => fakeView.IsbnFieldText).Returns("0123456789");
            A.CallTo(() => fakeView.Isbn13FieldText).Returns("012345678901X");
            A.CallTo(() => fakeView.DeweyDecimalFieldText).Returns("0");
            A.CallTo(() => fakeView.LanguageFieldText).Returns("English");
            A.CallTo(() => fakeView.SelectedPublisher).Returns("publisher");
            A.CallTo(() => fakeView.PagesFieldText).Returns("100");
            A.CallTo(() => fakeView.SelectedTags).Returns(new List<string> { "tag1" });
            A.CallTo(() => fakeView.SelectedAuthors).Returns(new List<string> { "Smith, John" });
            var fakeBookFactory = A.Fake<IBookFactory>();
            A.CallTo(() => fakeBookFactory.Create(new BookFactory.Titles { Title = "", LongTitle = "" }, new BookFactory.Isbns { Isbn10 = "bogus isbn10", Isbn13 = "bogus isbn13" },
                "100", "English", "publisher", "0", A<List<string>>.That.IsSameSequenceAs(new List<string> { "tag1" }), A<List<string>>.That.IsSameSequenceAs(new List<string> { "Smith, John" }))).Returns(new Book
                {
                    Title="book",
                    TitleLong="long book",
                    Isbn="0123456789",
                    Isbn13="012345678901X",
                    DeweyDecimal=0,
                    Language="English",
                    Publisher = new Publisher { Name="publisher"},
                    Tags = new List<Tag>
                    {
                        new Tag{Name="tag1"}
                    },
                    Authors = new List<Author> { new Author { FirstName="John", LastName="Smith"} }
                });
            AddBookPresenter presenter = new AddBookPresenter(null, null, null, null, fakeBookFactory, fakeView, null);

            // act
            presenter.InputFieldsUpdated(null, null);

            // assert
            Assert.IsTrue(fakeView.SaveButtonEnabled);
        }

        [Test]
        public void HandleAddNewTagClicked_Test_NoEntry()
        {
            // arrange
            var fakeView = A.Fake<IAddBookForm>();
            var fakeBookService = A.Fake<IBookService>();
            var fakeTagService = A.Fake<ITagService>();
            var fakeAddTagDialog = A.Fake<INewTagOrPublisher>();
            A.CallTo(() => fakeView.ShowNewTagDialog()).Returns("");
            AddBookPresenter presenter = new AddBookPresenter(fakeBookService, fakeTagService, null, null, null, fakeView, null);

            // act
            presenter.HandleAddNewTagClicked(null, null);

            // assert
            A.CallTo(() => fakeView.ShowTagAlreadyExistsDialog("")).MustNotHaveHappened();
            Assert.AreEqual(0, presenter.AllTags.Count);
        }

        [TestCase(true)]
        [TestCase(false)]
        public void HandleAddNewTagClicked_Test_TagAlreadyExists(bool existingTagSelected)
        {
            // arrange
            var fakeView = A.Fake<IAddBookForm>();
            var fakeBookService = A.Fake<IBookService>();
            var fakeTagService = A.Fake<ITagService>();
            var fakeAddTagDialog = A.Fake<INewTagOrPublisher>();
            A.CallTo(() => fakeView.ShowNewTagDialog()).Returns("tag");
            AddBookPresenter presenter = new AddBookPresenter(fakeBookService, fakeTagService, null, null, null, fakeView, null);
            presenter.AllTags.Add("tag", existingTagSelected);

            // act
            presenter.HandleAddNewTagClicked(null, null);

            // assert
            A.CallTo(() => fakeView.ShowTagAlreadyExistsDialog("tag")).MustHaveHappened();
            Assert.AreEqual(1, presenter.AllTags.Count);
        }

        [Test]
        public void HandleAddNewTagClicked_Test_TagDoesNotYetExist()
        {
            // arrange
            var fakeView = A.Fake<IAddBookForm>();
            var fakeBookService = A.Fake<IBookService>();
            var fakeTagService = A.Fake<ITagService>();
            var fakeAddTagDialog = A.Fake<INewTagOrPublisher>();
            A.CallTo(() => fakeView.ShowNewTagDialog()).Returns("tag");
            AddBookPresenter presenter = new AddBookPresenter(fakeBookService, fakeTagService, null, null, null, fakeView, null);

            // act
            presenter.HandleAddNewTagClicked(null, null);

            // assert
            A.CallTo(() => fakeView.ShowTagAlreadyExistsDialog("")).MustNotHaveHappened();
            Assert.AreEqual(1, presenter.AllTags.Count);
        }

        [Test]
        public void HandleTagCheckedChanged_Test()
        {
            // arrange
            var fakeView = A.Fake<IAddBookForm>();
            A.CallTo(() => fakeView.SelectedTags).Returns(new List<string> { "tag1", "tag3"});
            A.CallTo(() => fakeView.UnselectedTags).Returns(new List<string> { "tag2" });
            AddBookPresenter presenter = new AddBookPresenter(null, null, null, null, null, fakeView, null);
            Dictionary<string, bool> allTags = new Dictionary<string, bool>();
            allTags.Add("tag1", true);
            allTags.Add("tag2", true);
            allTags.Add("tag3", false);
            presenter.AllTags = allTags;

            // act
            presenter.HandleTagCheckedChanged(null, null);

            // assert
            Assert.IsTrue(presenter.AllTags["tag1"]);
            Assert.IsFalse(presenter.AllTags["tag2"]);
            Assert.IsTrue(presenter.AllTags["tag3"]);
        }

        [Test]
        public void HandleAuthorCheckedChanged_Test()
        {
            // arrange
            var fakeView = A.Fake<IAddBookForm>();
            A.CallTo(() => fakeView.SelectedAuthors).Returns(new List<string> { "Smith, John", "Jones, Simon" });
            A.CallTo(() => fakeView.UnselectedAuthors).Returns(new List<string> { "Doe, Jane" });
            AddBookPresenter presenter = new AddBookPresenter(null, null, null, null, null, fakeView, null);
            Dictionary<string, bool> allAuthors = new Dictionary<string, bool>();
            allAuthors.Add("Smith, John", true);
            allAuthors.Add("Doe, Jane", true);
            allAuthors.Add("Jones, Simon", false);
            presenter.AllAuthors = allAuthors;

            // act
            presenter.HandleAuthorCheckedChanged(null, null);

            // assert
            Assert.IsTrue(presenter.AllAuthors["Smith, John"]);
            Assert.IsFalse(presenter.AllAuthors["Doe, Jane"]);
            Assert.IsTrue(presenter.AllAuthors["Jones, Simon"]);
        }

        [Test]
        public void HandleAddNewPublisherClicked_Test_Empty()
        {
            // arrange
            var fakeView = A.Fake<IAddBookForm>();
            A.CallTo(() => fakeView.ShowNewPublisherDialog()).Returns("");
            AddBookPresenter presenter = new AddBookPresenter(null, null, null, null, null, fakeView, null);
            List<string> allPublishers = new List<string>();
            allPublishers.Add("publisher");
            presenter.AllPublishers = allPublishers;

            // act
            presenter.HandleAddNewPublisherClicked(null, null);

            // assert
            A.CallTo(() => fakeView.ShowPublisherAlreadyExistsDialog("")).MustNotHaveHappened();
            Assert.AreEqual(1, presenter.AllPublishers.Count);
        }

        [Test]
        public void HandleAddNewPublisherClicked_Test_AlreadyExists()
        {
            // arrange
            var fakeView = A.Fake<IAddBookForm>();
            A.CallTo(() => fakeView.ShowNewPublisherDialog()).Returns("new publisher");
            AddBookPresenter presenter = new AddBookPresenter(null, null, null, null, null, fakeView, null);
            List<string> allPublishers = new List<string>();
            allPublishers.Add("new publisher");
            presenter.AllPublishers = allPublishers;

            // act
            presenter.HandleAddNewPublisherClicked(null, null);

            // assert
            A.CallTo(() => fakeView.ShowPublisherAlreadyExistsDialog("new publisher")).MustHaveHappened();
            Assert.AreEqual(1, presenter.AllPublishers.Count);
        }

        [Test]
        public void HandleAddNewPublisherClicked_Test_DoesNotYetExist()
        {
            // arrange
            var fakeView = A.Fake<IAddBookForm>();
            A.CallTo(() => fakeView.FilterPublishersFieldEntry).Returns("new");
            A.CallTo(() => fakeView.ShowNewPublisherDialog()).Returns("new publisher");
            AddBookPresenter presenter = new AddBookPresenter(null, null, null, null, null, fakeView, null);
            List<string> allPublishers = new List<string>();
            allPublishers.Add("publisher 2");
            presenter.AllPublishers = allPublishers;

            // act
            presenter.HandleAddNewPublisherClicked(null, null);

            // assert
            A.CallTo(() => fakeView.ShowPublisherAlreadyExistsDialog("new publisher")).MustNotHaveHappened();
            Assert.AreEqual(2, presenter.AllPublishers.Count);
            A.CallTo(() => fakeView.AddPublishers(A<List<string>>.That.IsSameSequenceAs(new List<string> { "new publisher" }))).MustHaveHappened();
        }

        [Test]
        public void HandleAddNewAuthorClicked_Test_Empty()
        {
            // arrange
            var fakeView = A.Fake<IAddBookForm>();
            A.CallTo(() => fakeView.ShowNewAuthorDialog()).Returns(null);
            AddBookPresenter presenter = new AddBookPresenter(null, null, null, null, null, fakeView, null);
            Dictionary<string, bool> allAuthors = new Dictionary<string, bool>();
            allAuthors.Add("Smith, John", true);
            presenter.AllAuthors = allAuthors;

            // act
            presenter.HandleAddNewAuthorClicked(null, null);

            // assert
            A.CallTo(() => fakeView.ShowAuthorAlreadyExistsDialog("")).MustNotHaveHappened();
            Assert.AreEqual(1, presenter.AllAuthors.Count);
        }

        [Test]
        public void HandleAddNewAuthorClicked_Test_DoesNotYetExist()
        {
            // arrange
            var fakeView = A.Fake<IAddBookForm>();
            A.CallTo(() => fakeView.ShowNewAuthorDialog()).Returns(new AuthorName { FirstName="John", LastName="Smith"});
            AddBookPresenter presenter = new AddBookPresenter(null, null, null, null, null, fakeView, null);
            Dictionary<string, bool> allAuthors = new Dictionary<string, bool>();
            presenter.AllAuthors = allAuthors;

            // act
            presenter.HandleAddNewAuthorClicked(null, null);

            // assert
            A.CallTo(() => fakeView.ShowAuthorAlreadyExistsDialog("Smith, John")).MustNotHaveHappened();
            Assert.AreEqual(1, presenter.AllAuthors.Count);
        }

        [Test]
        public void HandleAddNewAuthorClicked_Test_AlreadyExists()
        {
            // arrange
            var fakeView = A.Fake<IAddBookForm>();
            A.CallTo(() => fakeView.ShowNewAuthorDialog()).Returns(new AuthorName { FirstName = "John", LastName = "Smith" });
            AddBookPresenter presenter = new AddBookPresenter(null, null, null, null, null, fakeView, null);
            Dictionary<string, bool> allAuthors = new Dictionary<string, bool>();
            allAuthors.Add("Smith, John", true);
            presenter.AllAuthors = allAuthors;

            // act
            presenter.HandleAddNewAuthorClicked(null, null);

            A.CallTo(() => fakeView.ShowAuthorAlreadyExistsDialog("Smith, John")).MustHaveHappened();
            Assert.AreEqual(1, presenter.AllAuthors.Count);
        }
    }
}