using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using System.Data;
using NUnit;
using NUnit.Framework;
using FakeItEasy;
using MyLibrary.Models.BusinessLogic;
using MyLibrary.Models.Entities;
using MyLibrary.DataAccessLayer;
using MyLibrary.Views;
using MyLibrary.Presenters;
using MyLibrary.Utils;

namespace MyLibrary_Test.Presenters_Tests
{
    [TestFixture]
    class AddBookPresenter_Tests
    {
        [TestCase("", "", "", "500.0", ".bmp")]
        [TestCase("", "0123456789", "", "500.0", ".bmp")]
        [TestCase("", "0123456789", "0123456789123", "500.0", ".bmp")]
        [TestCase("", "", "0123456789123", "500.0", ".bmp")]
        [TestCase("", "", "", "", ".bmp")]
        [TestCase("", "0123456789", "", "", ".bmp")]
        [TestCase("", "0123456789", "0123456789123", "", ".bmp")]
        [TestCase("", "", "0123456789123", "", ".bmp")]
        [TestCase("long title", "", "", "500.0", ".bmp")]
        [TestCase("long title", "0123456789", "", "500.0", ".bmp")]
        [TestCase("long title", "0123456789", "0123456789123", "500.0", ".bmp")]
        [TestCase("long title", "", "0123456789123", "500.0", ".bmp")]
        [TestCase("long title", "", "", "", ".bmp")]
        [TestCase("long title", "0123456789", "", "", ".bmp")]
        [TestCase("long title", "0123456789", "0123456789123", "", ".bmp")]
        [TestCase("long title", "", "0123456789123", "", ".bmp")]
        [TestCase("", "", "", "500.0", ".jpg")]
        [TestCase("", "0123456789", "", "500.0", ".jpg")]
        [TestCase("", "0123456789", "0123456789123", "500.0", ".jpg")]
        [TestCase("", "", "0123456789123", "500.0", ".jpg")]
        [TestCase("", "", "", "", ".jpg")]
        [TestCase("", "0123456789", "", "", ".jpg")]
        [TestCase("", "0123456789", "0123456789123", "", ".jpg")]
        [TestCase("", "", "0123456789123", "", ".jpg")]
        [TestCase("long title", "", "", "500.0", ".jpg")]
        [TestCase("long title", "0123456789", "", "500.0", ".jpg")]
        [TestCase("long title", "0123456789", "0123456789123", "500.0", ".jpg")]
        [TestCase("long title", "", "0123456789123", "500.0", ".jpg")]
        [TestCase("long title", "", "", "", ".jpg")]
        [TestCase("long title", "0123456789", "", "", ".jpg")]
        [TestCase("long title", "0123456789", "0123456789123", "", ".jpg")]
        [TestCase("long title", "", "0123456789123", "", ".jpg")]
        [TestCase("", "", "", "500.0", ".jpeg")]
        [TestCase("", "0123456789", "", "500.0", ".jpeg")]
        [TestCase("", "0123456789", "0123456789123", "500.0", ".jpeg")]
        [TestCase("", "", "0123456789123", "500.0", ".jpeg")]
        [TestCase("", "", "", "", ".jpeg")]
        [TestCase("", "0123456789", "", "", ".jpeg")]
        [TestCase("", "0123456789", "0123456789123", "", ".jpeg")]
        [TestCase("", "", "0123456789123", "", ".jpeg")]
        [TestCase("long title", "", "", "500.0", ".jpeg")]
        [TestCase("long title", "0123456789", "", "500.0", ".jpeg")]
        [TestCase("long title", "0123456789", "0123456789123", "500.0", ".jpeg")]
        [TestCase("long title", "", "0123456789123", "500.0", ".jpeg")]
        [TestCase("long title", "", "", "", ".jpeg")]
        [TestCase("long title", "0123456789", "", "", ".jpeg")]
        [TestCase("long title", "0123456789", "0123456789123", "", ".jpeg")]
        [TestCase("long title", "", "0123456789123", "", ".jpeg")]
        [TestCase("", "", "", "500.0", ".png")]
        [TestCase("", "0123456789", "", "500.0", ".png")]
        [TestCase("", "0123456789", "0123456789123", "500.0", ".png")]
        [TestCase("", "", "0123456789123", "500.0", ".png")]
        [TestCase("", "", "", "", ".png")]
        [TestCase("", "0123456789", "", "", ".png")]
        [TestCase("", "0123456789", "0123456789123", "", ".png")]
        [TestCase("", "", "0123456789123", "", ".png")]
        [TestCase("long title", "", "", "500.0", ".png")]
        [TestCase("long title", "0123456789", "", "500.0", ".png")]
        [TestCase("long title", "0123456789", "0123456789123", "500.0", ".png")]
        [TestCase("long title", "", "0123456789123", "500.0", ".png")]
        [TestCase("long title", "", "", "", ".png")]
        [TestCase("long title", "0123456789", "", "", ".png")]
        [TestCase("long title", "0123456789", "0123456789123", "", ".png")]
        [TestCase("long title", "", "0123456789123", "", ".png")]
        public void InputFieldsUpdated_Test_Valid_HasImageFilePath(string longTitle, string isbnFieldText, string isbn13FieldText, string deweyDecimalFieldText,
            string ext)
        {
            // arrange
            var fakeView = A.Fake<IAddBookForm>();
            A.CallTo(() => fakeView.TitleFieldText).Returns("title");
            A.CallTo(() => fakeView.LongTitleFieldText).Returns(longTitle);
            A.CallTo(() => fakeView.LanguageFieldText).Returns("English");
            A.CallTo(() => fakeView.PagesFieldText).Returns("60");
            A.CallTo(() => fakeView.SelectedPublisher).Returns("publisher");
            A.CallTo(() => fakeView.Isbn13FieldText).Returns(isbn13FieldText);
            A.CallTo(() => fakeView.IsbnFieldText).Returns(isbnFieldText);
            A.CallTo(() => fakeView.DeweyDecimalFieldText).Returns(deweyDecimalFieldText);
            A.CallTo(() => fakeView.ImageFilePathFieldText).Returns(@"C:\path\to\file." + ext);
            var fakeBookService = A.Fake<IBookService>();
            var fakeTagService = A.Fake<ITagService>();
            var fakeAuthorService = A.Fake<IAuthorService>();
            var fakePublisherService = A.Fake<IPublisherService>();
            MockBookPresenter presenter = new MockBookPresenter(fakeBookService, fakeTagService, fakeAuthorService, fakePublisherService, 
                fakeView,null);

            // act
            presenter.InputFieldsUpdated(null, null);

            // assert
            Assert.IsTrue(fakeView.SaveButtonEnabled);
        }

        [TestCase("", "", "", "500.0")]
        [TestCase("", "0123456789", "", "500.0")]
        [TestCase("", "0123456789", "0123456789123", "500.0")]
        [TestCase("", "", "0123456789123", "500.0")]
        [TestCase("", "", "", "")]
        [TestCase("", "0123456789", "", "")]
        [TestCase("", "0123456789", "0123456789123", "")]
        [TestCase("", "", "0123456789123", "")]
        [TestCase("long title", "", "", "500.0")]
        [TestCase("long title", "0123456789", "", "500.0")]
        [TestCase("long title", "0123456789", "0123456789123", "500.0")]
        [TestCase("long title", "", "0123456789123", "500.0")]
        [TestCase("long title", "", "", "")]
        [TestCase("long title", "0123456789", "", "")]
        [TestCase("long title", "0123456789", "0123456789123", "")]
        [TestCase("long title", "", "0123456789123", "")]
        public void InputFieldsUpdated_Test_Valid_NoImageFilePath(string longTitle, string isbnFieldText, string isbn13FieldText, string deweyDecimalFieldText)
        {
            // arrange
            var fakeView = A.Fake<IAddBookForm>();
            A.CallTo(() => fakeView.TitleFieldText).Returns("title");
            A.CallTo(() => fakeView.LongTitleFieldText).Returns(longTitle);
            A.CallTo(() => fakeView.LanguageFieldText).Returns("English");
            A.CallTo(() => fakeView.PagesFieldText).Returns("60");
            A.CallTo(() => fakeView.SelectedPublisher).Returns("publisher");
            A.CallTo(() => fakeView.Isbn13FieldText).Returns(isbn13FieldText);
            A.CallTo(() => fakeView.IsbnFieldText).Returns(isbnFieldText);
            A.CallTo(() => fakeView.DeweyDecimalFieldText).Returns(deweyDecimalFieldText);
            A.CallTo(() => fakeView.ImageFilePathFieldText).Returns("");
            var fakeBookRepo = A.Fake<IBookService>();
            var fakeTagService = A.Fake<ITagService>();
            var fakeAuthorService = A.Fake<IAuthorService>();
            var fakePublisherService = A.Fake<IPublisherService>();
            MockBookPresenter presenter = new MockBookPresenter(fakeBookRepo, fakeTagService, fakeAuthorService, fakePublisherService,
                fakeView,null);

            // act
            presenter.InputFieldsUpdated(null, null);

            // assert
            Assert.IsTrue(fakeView.SaveButtonEnabled);
        }

        [TestCase("title", "", "", "", "")]
        [TestCase("title", "long title", "", "", "")]
        [TestCase("title", "long title", "English", "", "")]
        [TestCase("", "", "", "", "")]
        [TestCase("", "", "English", "", "")]
        [TestCase("", "long title", "English", "", "")]
        [TestCase("", "", "", "60", "")]
        [TestCase("title", "long title", "English", "test", "")]
        [TestCase("title", "", "", "", "publisher")]
        [TestCase("title", "long title", "", "", "publisher")]
        [TestCase("title", "long title", "English", "", "publisher")]
        [TestCase("", "", "", "", "publisher")]
        [TestCase("", "", "English", "", "publisher")]
        [TestCase("", "long title", "English", "", "publisher")]
        [TestCase("", "", "", "60", "publisher")]
        [TestCase("title", "long title", "English", "test", "publisher")]
        public void InputFieldsUpdated_Test_Invalid_NoImageFilePath(string titleFieldText, string longTitleFieldText, string languageFieldText, string pagesFieldText, string selectedPublisher)
        {
            // arrange
            var fakeView = A.Fake<IAddBookForm>();
            A.CallTo(() => fakeView.TitleFieldText).Returns(titleFieldText);
            A.CallTo(() => fakeView.LongTitleFieldText).Returns(longTitleFieldText);
            A.CallTo(() => fakeView.LanguageFieldText).Returns(languageFieldText);
            A.CallTo(() => fakeView.PagesFieldText).Returns(pagesFieldText);
            A.CallTo(() => fakeView.ImageFilePathFieldText).Returns("");
            var fakeBookRepo = A.Fake<IBookService>();
            var fakeTagService = A.Fake<ITagService>();
            var fakeAuthorService = A.Fake<IAuthorService>();
            var fakePublisherService = A.Fake<IPublisherService>();
            MockBookPresenter presenter = new MockBookPresenter(fakeBookRepo, fakeTagService, fakeAuthorService, fakePublisherService, 
                fakeView,null);

            // act
            presenter.InputFieldsUpdated(null, null);

            // assert
            Assert.IsFalse(fakeView.SaveButtonEnabled);
        }

        [TestCase("title", "", "", "", "", ".txt")]
        [TestCase("title", "long title", "", "", "", ".txt")]
        [TestCase("title", "long title", "English", "", "", ".txt")]
        [TestCase("", "", "", "", "", ".txt")]
        [TestCase("", "", "English", "", "", ".txt")]
        [TestCase("", "long title", "English", "", "", ".txt")]
        [TestCase("", "", "", "60", "", ".txt")]
        [TestCase("title", "long title", "English", "test", "", ".txt")]
        [TestCase("title", "", "", "", "publisher", ".txt")]
        [TestCase("title", "long title", "", "", "publisher", ".txt")]
        [TestCase("title", "long title", "English", "", "publisher", ".txt")]
        [TestCase("", "", "", "", "publisher", ".txt")]
        [TestCase("", "", "English", "", "publisher", ".txt")]
        [TestCase("", "long title", "English", "", "publisher", ".txt")]
        [TestCase("", "", "", "60", "publisher", ".txt")]
        [TestCase("title", "long title", "English", "test", "publisher", ".txt")]
        [TestCase("title", "", "", "", "", "bogus file")]
        [TestCase("title", "long title", "", "", "", "bogus file")]
        [TestCase("title", "long title", "English", "", "", "bogus file")]
        [TestCase("", "", "", "", "", "bogus file")]
        [TestCase("", "", "English", "", "", "bogus file")]
        [TestCase("", "long title", "English", "", "", "bogus file")]
        [TestCase("", "", "", "60", "", ".txt")]
        [TestCase("title", "long title", "English", "test", "", "bogus file")]
        [TestCase("title", "", "", "", "publisher", "bogus file")]
        [TestCase("title", "long title", "", "", "publisher", "bogus file")]
        [TestCase("title", "long title", "English", "", "publisher", "bogus file")]
        [TestCase("", "", "", "", "publisher", "bogus file")]
        [TestCase("", "", "English", "", "publisher", "bogus file")]
        [TestCase("", "long title", "English", "", "publisher", "bogus file")]
        [TestCase("", "", "", "60", "publisher", "bogus file")]
        [TestCase("title", "long title", "English", "test", "publisher", "bogus file")]
        public void InputFieldsUpdated_Test_Invalid_HasImageFilePath(string titleFieldText, string longTitleFieldText, string languageFieldText, string pagesFieldText, string selectedPublisher,
            string ext)
        {
            // arrange
            var fakeView = A.Fake<IAddBookForm>();
            A.CallTo(() => fakeView.TitleFieldText).Returns(titleFieldText);
            A.CallTo(() => fakeView.LongTitleFieldText).Returns(longTitleFieldText);
            A.CallTo(() => fakeView.LanguageFieldText).Returns(languageFieldText);
            A.CallTo(() => fakeView.PagesFieldText).Returns(pagesFieldText);
            A.CallTo(() => fakeView.ImageFilePathFieldText).Returns(@"C:\path\to\file." + ext);
            var fakeBookRepo = A.Fake<IBookService>();
            var fakeTagService = A.Fake<ITagService>();
            var fakeAuthorService = A.Fake<IAuthorService>();
            var fakePublisherService = A.Fake<IPublisherService>();
            MockBookPresenter presenter = new MockBookPresenter(fakeBookRepo, fakeTagService, fakeAuthorService, fakePublisherService,
                fakeView,null);

            // act
            presenter.InputFieldsUpdated(null, null);

            // assert
            Assert.IsFalse(fakeView.SaveButtonEnabled);
        }

        [Test]
        public void Prefill_Test()
        {
            // arrange
            Author author1 = new Author { FirstName = "John", LastName = "Smith" };
            Author author2 = new Author { FirstName = "Jane", LastName = "Doe" };
            Publisher publisher = new Publisher("some_publisher");
            Book book = new Book
            {
                Title = "test book",
                TitleLong = "test book: this book is a test",
                Isbn = "0123456789",
                Isbn13 = "0123456789012",
                DatePublished = "2020",
                PlaceOfPublication = "AU",
                Pages = 100,
                Language = "English",

                Publisher = publisher,

                Authors = new List<Author> 
                {
                    author1,
                    author2
                }
            };
            var fakeView = A.Fake<IAddBookForm>();
            var fakeBookRepo = A.Fake<IBookService>();
            var fakeTagService = A.Fake<ITagService>();
            var fakeAuthorService = A.Fake<IAuthorService>();
            var fakePublisherService = A.Fake<IPublisherService>();
            MockBookPresenter presenter = new MockBookPresenter(fakeBookRepo, fakeTagService, fakeAuthorService, fakePublisherService,
                fakeView,null);

            // act
            presenter.Prefill(book);

            // assert
            Assert.AreEqual("test book", fakeView.TitleFieldText);
            Assert.AreEqual("test book: this book is a test", fakeView.LongTitleFieldText);
            Assert.AreEqual("0123456789", fakeView.IsbnFieldText);
            Assert.AreEqual("0123456789012", fakeView.Isbn13FieldText);
            Assert.AreEqual("2020", fakeView.DatePublishedFieldText);
            Assert.AreEqual("AU", fakeView.PlaceOfPublicationFieldText);
            Assert.AreEqual("100", fakeView.PagesFieldText);
            Assert.AreEqual("English", fakeView.LanguageFieldText);
            A.CallTo(() => fakeView.SetPublisher(publisher, true)).MustHaveHappened();
            A.CallTo(() => fakeView.SetAuthor(author1, true)).MustHaveHappened();
            A.CallTo(() => fakeView.SetAuthor(author2, true)).MustHaveHappened();
        }

        [Test]
        public async Task PopulateTagsList_Test()
        {
            // arrange
            var fakeView = A.Fake<IAddBookForm>();
            List<Tag> tags = new List<Tag> { new Tag { Name = "tag" } };
            var fakeTagService = A.Fake<ITagService>();
            A.CallTo(() => fakeTagService.GetAll()).Returns(tags);
            var fakeImageFileReader = A.Fake<IImageFileReader>();
            var fakeBookRepo = A.Fake<IBookService>();
            var fakeAuthorService = A.Fake<IAuthorService>();
            var fakePublisherService = A.Fake<IPublisherService>();
            MockBookPresenter presenter = new MockBookPresenter(fakeBookRepo, fakeTagService, fakeAuthorService, fakePublisherService,
                fakeView, null);

            // act
            await presenter.PopulateTagsList();

            // assert
            Assert.IsFalse(presenter.GetAllTagsValueByKey("tag"));
        }

        [Test]
        public async Task PopulateAuthorList_Test()
        {
            // arrange
            var fakeView = A.Fake<IAddBookForm>();
            var fakeBookRepo = A.Fake<IBookService>();
            var fakeTagService = A.Fake<ITagService>();
            var fakeAuthorService = A.Fake<IAuthorService>();
            Author author1 = new Author { FirstName = "John", LastName = "Smith" };
            A.CallTo(() => fakeAuthorService.GetAll()).Returns(new List<Author> { author1 });
            var fakePublisherService = A.Fake<IPublisherService>();
            MockBookPresenter presenter = new MockBookPresenter(fakeBookRepo, fakeTagService, fakeAuthorService, fakePublisherService,
                fakeView,null);

            // act
            await presenter.PopulateAuthorsList();

            // assert
            A.CallTo(() => fakeView.PopulateAuthorList(A<List<string>>.That.Matches(l => l.Count() == 1 && l.Contains("Smith, John"))));
        }

        [Test]
        public async Task PopulatePublisherList_Test()
        {
            // arrange
            var fakeView = A.Fake<IAddBookForm>();
            var fakeBookRepo = A.Fake<IBookService>();
            var fakeTagService = A.Fake<ITagService>();
            var fakeAuthorService = A.Fake<IAuthorService>();
            var fakePublisherService = A.Fake<IPublisherService>();
            Publisher publisher1 = new Publisher("some_publisher");
            A.CallTo(() => fakePublisherService.GetAll()).Returns(new List<Publisher> { publisher1 });
            MockBookPresenter presenter = new MockBookPresenter(fakeBookRepo, fakeTagService, fakeAuthorService, fakePublisherService,
                fakeView,null);

            // act
            await presenter.PopulatePublishersList();

            // assert
            A.CallTo(() => fakeView.PopulatePublisherList(A<List<string>>.That.Matches(l => l.Count() == 1 && l.Contains("some_publisher"))));
        }

        [Test]
        public async Task SaveButtonClicked_Test_ItemWithTitleAlreadyExists()
        {
            // arrange
            var fakeView = A.Fake<IAddBookForm>();
            A.CallTo(() => fakeView.TitleFieldText).Returns("test book");
            var fakeBookRepo = A.Fake<IBookService>();
            A.CallTo(() => fakeBookRepo.ExistsWithTitle("test book")).Returns(true);
            var fakeTagService = A.Fake<ITagService>();
            var fakeAuthorService = A.Fake<IAuthorService>();
            var fakePublisherService = A.Fake<IPublisherService>();
            MockBookPresenter presenter = new MockBookPresenter(fakeBookRepo, fakeTagService, fakeAuthorService, fakePublisherService,
                fakeView,null);

            // act
            await presenter.HandleSaveButtonClicked(null, null);

            // assert
            A.CallTo(() => fakeView.ShowItemAlreadyExistsDialog("test book")).MustHaveHappened();
            Assert.IsTrue(fakeView.SaveButtonEnabled);
            Assert.IsTrue(fakeView.CancelButtonEnabled);
        }

        [Test]
        public async Task SaveButtonClicked_Test_ItemWithLongTitleAlreadyExists()
        {
            // arrange
            var fakeView = A.Fake<IAddBookForm>();
            A.CallTo(() => fakeView.LongTitleFieldText).Returns("test book");
            var fakeBookRepo = A.Fake<IBookService>();
            A.CallTo(() => fakeBookRepo.ExistsWithLongTitle("test book")).Returns(true);
            var fakeTagService = A.Fake<ITagService>();
            var fakeAuthorService = A.Fake<IAuthorService>();
            var fakePublisherService = A.Fake<IPublisherService>();
            MockBookPresenter presenter = new MockBookPresenter(fakeBookRepo, fakeTagService, fakeAuthorService, fakePublisherService,
                fakeView,null);

            // act
            await presenter.HandleSaveButtonClicked(null, null);

            // assert
            A.CallTo(() => fakeView.ShowItemAlreadyExistsDialog("test book")).MustHaveHappened();
            Assert.IsTrue(fakeView.SaveButtonEnabled);
            Assert.IsTrue(fakeView.CancelButtonEnabled);
        }

        [Test]
        public async Task SaveButtonClicked_Test_ItemWithIsbn10AlreadyExists()
        {
            // arrange
            var fakeView = A.Fake<IAddBookForm>();
            A.CallTo(() => fakeView.TitleFieldText).Returns("test book");
            A.CallTo(() => fakeView.IsbnFieldText).Returns("0123456789");
            var fakeBookRepo = A.Fake<IBookService>();
            A.CallTo(() => fakeBookRepo.ExistsWithIsbn("0123456789")).Returns(true);
            var fakeTagService = A.Fake<ITagService>();
            var fakeAuthorService = A.Fake<IAuthorService>();
            var fakePublisherService = A.Fake<IPublisherService>();
            MockBookPresenter presenter = new MockBookPresenter(fakeBookRepo, fakeTagService, fakeAuthorService, fakePublisherService,
                fakeView,null);

            // act
            await presenter.HandleSaveButtonClicked(null, null);

            // assert
            A.CallTo(() => fakeView.ShowIsbnAlreadyExistsDialog("0123456789")).MustHaveHappened();
            Assert.IsTrue(fakeView.SaveButtonEnabled);
            Assert.IsTrue(fakeView.CancelButtonEnabled);
        }

        [Test]
        public async Task SaveButtonClicked_Test_ItemWithIsbn13AlreadyExists()
        {
            // arrange
            var fakeView = A.Fake<IAddBookForm>();
            A.CallTo(() => fakeView.TitleFieldText).Returns("test book");
            A.CallTo(() => fakeView.Isbn13FieldText).Returns("0123456789012");
            var fakeBookRepo = A.Fake<IBookService>();
            A.CallTo(() => fakeBookRepo.ExistsWithIsbn("0123456789012")).Returns(true);
            var fakeTagService = A.Fake<ITagService>();
            var fakeAuthorService = A.Fake<IAuthorService>();
            var fakePublisherService = A.Fake<IPublisherService>();
            MockBookPresenter presenter = new MockBookPresenter(fakeBookRepo, fakeTagService, fakeAuthorService, fakePublisherService,
                fakeView,null);

            // act
            await presenter.HandleSaveButtonClicked(null, null);

            // assert
            A.CallTo(() => fakeView.ShowIsbnAlreadyExistsDialog("0123456789012")).MustHaveHappened();
            Assert.IsTrue(fakeView.SaveButtonEnabled);
            Assert.IsTrue(fakeView.CancelButtonEnabled);
        }

        [Test]
        public async Task SaveButtonClicked_Test_ErrorWhileCheckingIfTitleAlreadyExists()
        {
            // arrange
            var fakeView = A.Fake<IAddBookForm>();
            A.CallTo(() => fakeView.LongTitleFieldText).Returns("test book");
            var fakeBookRepo = A.Fake<IBookService>();
            Exception ex = new Exception("error");
            A.CallTo(() => fakeBookRepo.ExistsWithLongTitle("test book")).Throws(ex);
            var fakeTagService = A.Fake<ITagService>();
            var fakeAuthorService = A.Fake<IAuthorService>();
            var fakePublisherService = A.Fake<IPublisherService>();
            MockBookPresenter presenter = new MockBookPresenter(fakeBookRepo, fakeTagService, fakeAuthorService, fakePublisherService,
                fakeView,null);

            // act
            await presenter.HandleSaveButtonClicked(null, null);

            // assert
            A.CallTo(() => fakeView.ShowErrorDialog("Error checking if title or ISBN exists.", "error"));
            Assert.IsTrue(fakeView.SaveButtonEnabled);
            Assert.IsTrue(fakeView.CancelButtonEnabled);
        }

        [Test]
        public async Task SaveButtonClicked_Test_ErrorWhileCheckingIfIsbnAlreadyExists()
        {
            // arrange
            var fakeView = A.Fake<IAddBookForm>();
            A.CallTo(() => fakeView.LongTitleFieldText).Returns("test book");
            A.CallTo(() => fakeView.IsbnFieldText).Returns("0123456789");
            var fakeBookRepo = A.Fake<IBookService>();
            Exception ex = new Exception("error");
            A.CallTo(() => fakeBookRepo.ExistsWithIsbn("0123456789")).Throws(ex);
            var fakeTagService = A.Fake<ITagService>();
            var fakeAuthorService = A.Fake<IAuthorService>();
            var fakePublisherService = A.Fake<IPublisherService>();
            MockBookPresenter presenter = new MockBookPresenter(fakeBookRepo, fakeTagService, fakeAuthorService, fakePublisherService,
                fakeView,null);

            // act
            await presenter.HandleSaveButtonClicked(null, null);

            // assert
            A.CallTo(() => fakeView.ShowErrorDialog("Error checking if title or ISBN exists.", "error"));
            Assert.IsTrue(fakeView.SaveButtonEnabled);
            Assert.IsTrue(fakeView.CancelButtonEnabled);
        }

        [Test]
        public async Task SaveButtonClicked_Test_ErrorReadingImageFile()
        {
            // arrange
            var fakeView = A.Fake<IAddBookForm>();
            A.CallTo(() => fakeView.TitleFieldText).Returns("test_book");
            A.CallTo(() => fakeView.LongTitleFieldText).Returns("test_book");
            A.CallTo(() => fakeView.IsbnFieldText).Returns("0123456789");
            A.CallTo(() => fakeView.ImageFilePathFieldText).Returns(@"C:\path\to\image.jpeg");
            A.CallTo(() => fakeView.SelectedPublisher).Returns("some_publisher");
            A.CallTo(() => fakeView.PagesFieldText).Returns("100");
            A.CallTo(() => fakeView.LanguageFieldText).Returns("English");
            var fakeBookService = A.Fake<IBookService>();
            var fakeTagService = A.Fake<ITagService>();
            var fakeAuthorService = A.Fake<IAuthorService>();
            var fakePublisherService = A.Fake<IPublisherService>();
            var fakeImageFileReader = A.Fake<IImageFileReader>();
            A.CallTo(() => fakeImageFileReader.ReadBytes()).Throws(new System.IO.IOException("error"));
            MockBookPresenter presenter = new MockBookPresenter(fakeBookService, fakeTagService, fakeAuthorService, fakePublisherService,
                fakeView, fakeImageFileReader);

            // act
            await presenter.HandleSaveButtonClicked(null, null);

            // assert
            A.CallTo(() => fakeView.ShowErrorDialog("Image file error", "error")).MustHaveHappened();
            Assert.IsTrue(fakeView.SaveButtonEnabled);
            Assert.IsTrue(fakeView.CancelButtonEnabled);
        }

        [TestCase("", "", "", "500.0", ".bmp")]
        [TestCase("", "0123456789", "", "500.0", ".bmp")]
        [TestCase("", "0123456789", "0123456789123", "500.0", ".bmp")]
        [TestCase("", "", "0123456789123", "500.0", ".bmp")]
        [TestCase("", "", "", "", ".bmp")]
        [TestCase("", "0123456789", "", "", ".bmp")]
        [TestCase("", "0123456789", "0123456789123", "", ".bmp")]
        [TestCase("", "", "0123456789123", "", ".bmp")]
        [TestCase("long title", "", "", "500.0", ".bmp")]
        [TestCase("long title", "0123456789", "", "500.0", ".bmp")]
        [TestCase("long title", "0123456789", "0123456789123", "500.0", ".bmp")]
        [TestCase("long title", "", "0123456789123", "500.0", ".bmp")]
        [TestCase("long title", "", "", "", ".bmp")]
        [TestCase("long title", "0123456789", "", "", ".bmp")]
        [TestCase("long title", "0123456789", "0123456789123", "", ".bmp")]
        [TestCase("long title", "", "0123456789123", "", ".bmp")]
        [TestCase("", "", "", "500.0", ".jpg")]
        [TestCase("", "0123456789", "", "500.0", ".jpg")]
        [TestCase("", "0123456789", "0123456789123", "500.0", ".jpg")]
        [TestCase("", "", "0123456789123", "500.0", ".jpg")]
        [TestCase("", "", "", "", ".jpg")]
        [TestCase("", "0123456789", "", "", ".jpg")]
        [TestCase("", "0123456789", "0123456789123", "", ".jpg")]
        [TestCase("", "", "0123456789123", "", ".jpg")]
        [TestCase("long title", "", "", "500.0", ".jpg")]
        [TestCase("long title", "0123456789", "", "500.0", ".jpg")]
        [TestCase("long title", "0123456789", "0123456789123", "500.0", ".jpg")]
        [TestCase("long title", "", "0123456789123", "500.0", ".jpg")]
        [TestCase("long title", "", "", "", ".jpg")]
        [TestCase("long title", "0123456789", "", "", ".jpg")]
        [TestCase("long title", "0123456789", "0123456789123", "", ".jpg")]
        [TestCase("long title", "", "0123456789123", "", ".jpg")]
        [TestCase("", "", "", "500.0", ".jpeg")]
        [TestCase("", "0123456789", "", "500.0", ".jpeg")]
        [TestCase("", "0123456789", "0123456789123", "500.0", ".jpeg")]
        [TestCase("", "", "0123456789123", "500.0", ".jpeg")]
        [TestCase("", "", "", "", ".jpeg")]
        [TestCase("", "0123456789", "", "", ".jpeg")]
        [TestCase("", "0123456789", "0123456789123", "", ".jpeg")]
        [TestCase("", "", "0123456789123", "", ".jpeg")]
        [TestCase("long title", "", "", "500.0", ".jpeg")]
        [TestCase("long title", "0123456789", "", "500.0", ".jpeg")]
        [TestCase("long title", "0123456789", "0123456789123", "500.0", ".jpeg")]
        [TestCase("long title", "", "0123456789123", "500.0", ".jpeg")]
        [TestCase("long title", "", "", "", ".jpeg")]
        [TestCase("long title", "0123456789", "", "", ".jpeg")]
        [TestCase("long title", "0123456789", "0123456789123", "", ".jpeg")]
        [TestCase("long title", "", "0123456789123", "", ".jpeg")]
        [TestCase("", "", "", "500.0", ".png")]
        [TestCase("", "0123456789", "", "500.0", ".png")]
        [TestCase("", "0123456789", "0123456789123", "500.0", ".png")]
        [TestCase("", "", "0123456789123", "500.0", ".png")]
        [TestCase("", "", "", "", ".png")]
        [TestCase("", "0123456789", "", "", ".png")]
        [TestCase("", "0123456789", "0123456789123", "", ".png")]
        [TestCase("", "", "0123456789123", "", ".png")]
        [TestCase("long title", "", "", "500.0", ".png")]
        [TestCase("long title", "0123456789", "", "500.0", ".png")]
        [TestCase("long title", "0123456789", "0123456789123", "500.0", ".png")]
        [TestCase("long title", "", "0123456789123", "500.0", ".png")]
        [TestCase("long title", "", "", "", ".png")]
        [TestCase("long title", "0123456789", "", "", ".png")]
        [TestCase("long title", "0123456789", "0123456789123", "", ".png")]
        [TestCase("long title", "", "0123456789123", "", ".png")]
        public async Task SaveButtonClicked_Test_Success(string longTitle, string isbnFieldText, string isbn13FieldText, string deweyDecimalFieldText,
            string ext)
        {
            // arrange
            var fakeView = A.Fake<IAddBookForm>();
            A.CallTo(() => fakeView.TitleFieldText).Returns("title");
            A.CallTo(() => fakeView.LongTitleFieldText).Returns(longTitle);
            A.CallTo(() => fakeView.LanguageFieldText).Returns("English");
            A.CallTo(() => fakeView.PagesFieldText).Returns("60");
            A.CallTo(() => fakeView.SelectedPublisher).Returns("publisher");
            A.CallTo(() => fakeView.Isbn13FieldText).Returns(isbn13FieldText);
            A.CallTo(() => fakeView.IsbnFieldText).Returns(isbnFieldText);
            A.CallTo(() => fakeView.DeweyDecimalFieldText).Returns(deweyDecimalFieldText);
            A.CallTo(() => fakeView.ImageFilePathFieldText).Returns(@"C:\path\to\file." + ext);
            var fakeBookService = A.Fake<IBookService>();
            var fakeTagService = A.Fake<ITagService>();
            var fakeAuthorService = A.Fake<IAuthorService>();
            var fakePublisherService = A.Fake<IPublisherService>();
            var fakeImageFileReader = A.Fake<IImageFileReader>();
            MockBookPresenter presenter = new MockBookPresenter(fakeBookService, fakeTagService, fakeAuthorService, fakePublisherService,
                fakeView, fakeImageFileReader);

            // act
            await presenter.HandleSaveButtonClicked(null, null);

            // assert
            decimal? deweyDecimal;
            if (System.Text.RegularExpressions.Regex.IsMatch(deweyDecimalFieldText, Book.DEWEY_DECIMAL_PATTERN))
            {
                deweyDecimal = decimal.Parse(deweyDecimalFieldText);
            }
            else
            {
                deweyDecimal = null;
            }
            A.CallTo(() => fakeBookService.Add(A<Book>.That.Matches(b =>
            b.Title == "title" &&
            b.TitleLong == longTitle &&
            b.Language == "English" &&
            b.Pages == 60 &&
            b.Isbn == isbnFieldText &&
            b.Isbn13 == isbn13FieldText &&
            b.DeweyDecimal == deweyDecimal))).MustHaveHappened();
            A.CallTo(() => fakeView.CloseDialog()).MustHaveHappened();
        }

        [TestCase("", "", "", "500.0", ".bmp")]
        [TestCase("", "0123456789", "", "500.0", ".bmp")]
        [TestCase("", "0123456789", "0123456789123", "500.0", ".bmp")]
        [TestCase("", "", "0123456789123", "500.0", ".bmp")]
        [TestCase("", "", "", "", ".bmp")]
        [TestCase("", "0123456789", "", "", ".bmp")]
        [TestCase("", "0123456789", "0123456789123", "", ".bmp")]
        [TestCase("", "", "0123456789123", "", ".bmp")]
        [TestCase("long title", "", "", "500.0", ".bmp")]
        [TestCase("long title", "0123456789", "", "500.0", ".bmp")]
        [TestCase("long title", "0123456789", "0123456789123", "500.0", ".bmp")]
        [TestCase("long title", "", "0123456789123", "500.0", ".bmp")]
        [TestCase("long title", "", "", "", ".bmp")]
        [TestCase("long title", "0123456789", "", "", ".bmp")]
        [TestCase("long title", "0123456789", "0123456789123", "", ".bmp")]
        [TestCase("long title", "", "0123456789123", "", ".bmp")]
        [TestCase("", "", "", "500.0", ".jpg")]
        [TestCase("", "0123456789", "", "500.0", ".jpg")]
        [TestCase("", "0123456789", "0123456789123", "500.0", ".jpg")]
        [TestCase("", "", "0123456789123", "500.0", ".jpg")]
        [TestCase("", "", "", "", ".jpg")]
        [TestCase("", "0123456789", "", "", ".jpg")]
        [TestCase("", "0123456789", "0123456789123", "", ".jpg")]
        [TestCase("", "", "0123456789123", "", ".jpg")]
        [TestCase("long title", "", "", "500.0", ".jpg")]
        [TestCase("long title", "0123456789", "", "500.0", ".jpg")]
        [TestCase("long title", "0123456789", "0123456789123", "500.0", ".jpg")]
        [TestCase("long title", "", "0123456789123", "500.0", ".jpg")]
        [TestCase("long title", "", "", "", ".jpg")]
        [TestCase("long title", "0123456789", "", "", ".jpg")]
        [TestCase("long title", "0123456789", "0123456789123", "", ".jpg")]
        [TestCase("long title", "", "0123456789123", "", ".jpg")]
        [TestCase("", "", "", "500.0", ".jpeg")]
        [TestCase("", "0123456789", "", "500.0", ".jpeg")]
        [TestCase("", "0123456789", "0123456789123", "500.0", ".jpeg")]
        [TestCase("", "", "0123456789123", "500.0", ".jpeg")]
        [TestCase("", "", "", "", ".jpeg")]
        [TestCase("", "0123456789", "", "", ".jpeg")]
        [TestCase("", "0123456789", "0123456789123", "", ".jpeg")]
        [TestCase("", "", "0123456789123", "", ".jpeg")]
        [TestCase("long title", "", "", "500.0", ".jpeg")]
        [TestCase("long title", "0123456789", "", "500.0", ".jpeg")]
        [TestCase("long title", "0123456789", "0123456789123", "500.0", ".jpeg")]
        [TestCase("long title", "", "0123456789123", "500.0", ".jpeg")]
        [TestCase("long title", "", "", "", ".jpeg")]
        [TestCase("long title", "0123456789", "", "", ".jpeg")]
        [TestCase("long title", "0123456789", "0123456789123", "", ".jpeg")]
        [TestCase("long title", "", "0123456789123", "", ".jpeg")]
        [TestCase("", "", "", "500.0", ".png")]
        [TestCase("", "0123456789", "", "500.0", ".png")]
        [TestCase("", "0123456789", "0123456789123", "500.0", ".png")]
        [TestCase("", "", "0123456789123", "500.0", ".png")]
        [TestCase("", "", "", "", ".png")]
        [TestCase("", "0123456789", "", "", ".png")]
        [TestCase("", "0123456789", "0123456789123", "", ".png")]
        [TestCase("", "", "0123456789123", "", ".png")]
        [TestCase("long title", "", "", "500.0", ".png")]
        [TestCase("long title", "0123456789", "", "500.0", ".png")]
        [TestCase("long title", "0123456789", "0123456789123", "500.0", ".png")]
        [TestCase("long title", "", "0123456789123", "500.0", ".png")]
        [TestCase("long title", "", "", "", ".png")]
        [TestCase("long title", "0123456789", "", "", ".png")]
        [TestCase("long title", "0123456789", "0123456789123", "", ".png")]
        [TestCase("long title", "", "0123456789123", "", ".png")]
        public async Task SaveButtonClicked_Test_ErrorSavingBook(string longTitle, string isbnFieldText, string isbn13FieldText, string deweyDecimalFieldText,
            string ext)
        {
            // arrange
            var fakeView = A.Fake<IAddBookForm>();
            A.CallTo(() => fakeView.TitleFieldText).Returns("title");
            A.CallTo(() => fakeView.LongTitleFieldText).Returns(longTitle);
            A.CallTo(() => fakeView.LanguageFieldText).Returns("English");
            A.CallTo(() => fakeView.PagesFieldText).Returns("60");
            A.CallTo(() => fakeView.SelectedPublisher).Returns("publisher");
            A.CallTo(() => fakeView.Isbn13FieldText).Returns(isbn13FieldText);
            A.CallTo(() => fakeView.IsbnFieldText).Returns(isbnFieldText);
            A.CallTo(() => fakeView.DeweyDecimalFieldText).Returns(deweyDecimalFieldText);
            A.CallTo(() => fakeView.ImageFilePathFieldText).Returns(@"C:\path\to\file." + ext);
            var fakeBookService = A.Fake<IBookService>();
            A.CallTo(() => fakeBookService.Add(A<Book>.That.Matches(b => b.Title == "title"))).Throws(new Exception("error"));
            var fakeTagService = A.Fake<ITagService>();
            var fakeAuthorService = A.Fake<IAuthorService>();
            var fakePublisherService = A.Fake<IPublisherService>();
            var fakeImageFileReader = A.Fake<IImageFileReader>();
            MockBookPresenter presenter = new MockBookPresenter(fakeBookService, fakeTagService, fakeAuthorService, fakePublisherService,
                fakeView, fakeImageFileReader);

            // act
            await presenter.HandleSaveButtonClicked(null, null);

            // assert
            decimal? deweyDecimal;
            if (System.Text.RegularExpressions.Regex.IsMatch(deweyDecimalFieldText, Book.DEWEY_DECIMAL_PATTERN))
            {
                deweyDecimal = decimal.Parse(deweyDecimalFieldText);
            }
            else
            {
                deweyDecimal = null;
            }
            A.CallTo(() => fakeBookService.Add(A<Book>.That.Matches(b => 
            b.Title == "title" &&
            b.TitleLong == longTitle &&
            b.Language == "English" &&
            b.Pages == 60 &&
            b.Isbn == isbnFieldText &&
            b.Isbn13 == isbn13FieldText &&
            b.DeweyDecimal == deweyDecimal))).MustHaveHappened();
            A.CallTo(() => fakeView.ShowErrorDialog("Error creating book", "error")).MustHaveHappened(); 
            Assert.IsTrue(fakeView.SaveButtonEnabled);
            Assert.IsTrue(fakeView.CancelButtonEnabled);
        }

        [Test]
        public void HandleAddNewTagClicked_Test_NoEntry()
        {
            // arrange
            var fakeView = A.Fake<IAddBookForm>();
            A.CallTo(() => fakeView.ShowNewTagDialog()).Returns("");
            var fakeBookService = A.Fake<IBookService>();
            var fakeTagService = A.Fake<ITagService>();
            var fakeImageFileReader = A.Fake<IImageFileReader>();
            var fakeAuthorService = A.Fake<IAuthorService>();
            var fakePublisherService = A.Fake<IPublisherService>();
            MockBookPresenter presenter = new MockBookPresenter(fakeBookService, fakeTagService, fakeAuthorService, fakePublisherService, fakeView, fakeImageFileReader);

            // act
            presenter.HandleAddNewTagClicked(null, null);

            // assert
            A.CallTo(() => fakeView.ShowTagAlreadyExistsDialog("")).MustNotHaveHappened();
        }

        [Test]
        public void HandleAddNewTagClicked_Test_TagAlreadyExists()
        {
            // arrange
            var fakeView = A.Fake<IAddBookForm>();
            A.CallTo(() => fakeView.ShowNewTagDialog()).Returns("tag1");
            var fakeBookService = A.Fake<IBookService>();
            var fakeTagService = A.Fake<ITagService>();
            var fakeImageFileReader = A.Fake<IImageFileReader>();
            var fakeAuthorService = A.Fake<IAuthorService>();
            var fakePublisherService = A.Fake<IPublisherService>();
            MockBookPresenter presenter = new MockBookPresenter(fakeBookService, fakeTagService, fakeAuthorService, fakePublisherService, fakeView, fakeImageFileReader);
            Dictionary<string, bool> allTags = new Dictionary<string, bool>();
            allTags.Add("tag1", true);
            presenter.SetAllTags(allTags);

            // act
            presenter.HandleAddNewTagClicked(null, null);

            // assert
            Assert.IsTrue(presenter.GetAllTagsValueByKey("tag1"));
        }

        [Test]
        public void HandleAddNewTagClicked_Test_TagDoesNotYetExist()
        {
            // arrange
            var fakeView = A.Fake<IAddBookForm>();
            A.CallTo(() => fakeView.ShowNewTagDialog()).Returns("tag2");
            var fakeBookService = A.Fake<IBookService>();
            var fakeTagService = A.Fake<ITagService>();
            var fakeImageFileReader = A.Fake<IImageFileReader>();
            var fakeAuthorService = A.Fake<IAuthorService>();
            var fakePublisherService = A.Fake<IPublisherService>();
            MockBookPresenter presenter = new MockBookPresenter(fakeBookService, fakeTagService, fakeAuthorService, fakePublisherService, fakeView, fakeImageFileReader);
            Dictionary<string, bool> allTags = new Dictionary<string, bool>();
            allTags.Add("tag1", true);
            presenter.SetAllTags(allTags);

            // act
            presenter.HandleAddNewTagClicked(null, null);

            // assert
            Assert.IsTrue(presenter.GetAllTagsValueByKey("tag2"));
        }

        [Test]
        public void HandleTagCheckedChanged_Test()
        {
            // arrange
            var fakeView = A.Fake<IAddBookForm>();
            List<string> selectedTags = new List<string> { "tag1" };
            List<string> unselectedTags = new List<string> { "tag2" };
            A.CallTo(() => fakeView.SelectedTags).Returns(selectedTags);
            A.CallTo(() => fakeView.UnselectedTags).Returns(unselectedTags);
            var fakeBookService = A.Fake<IBookService>();
            var fakeTagService = A.Fake<ITagService>();
            var fakeImageFileReader = A.Fake<IImageFileReader>();
            var fakeAuthorService = A.Fake<IAuthorService>();
            var fakePublisherService = A.Fake<IPublisherService>();
            MockBookPresenter presenter = new MockBookPresenter(fakeBookService, fakeTagService, fakeAuthorService, fakePublisherService, fakeView, fakeImageFileReader);
            Dictionary<string, bool> allTags = new Dictionary<string, bool>();
            allTags.Add("tag1", false);
            allTags.Add("tag2", true);
            presenter.SetAllTags(allTags);

            // act
            presenter.HandleTagCheckedChanged(null, null);

            // assert
            Assert.IsTrue(presenter.GetAllTagsValueByKey("tag1"));
            Assert.IsFalse(presenter.GetAllTagsValueByKey("tag2"));
        }
    }//class

    public class MockBookPresenter : AddBookPresenter
    {
        public MockBookPresenter(IBookService bookRepo, ITagService tagService, IAuthorService authorService, IPublisherService publisherService, IAddBookForm view,
            IImageFileReader imageFileReader)
            :base(bookRepo, tagService, authorService, publisherService, view, imageFileReader)
        {

        }

        public void SetAllTags(Dictionary<string, bool> allTags)
        {
            this._allTags = allTags;
        }

        public bool GetAllTagsValueByKey(string key)
        {
            return this._allTags[key];
        }
    }//class
}
