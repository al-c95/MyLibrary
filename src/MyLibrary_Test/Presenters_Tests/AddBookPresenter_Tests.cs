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
using MyLibrary.BusinessLogic.Repositories;
using MyLibrary.Models.Entities;
using MyLibrary.DataAccessLayer;
using MyLibrary.Views;
using MyLibrary.Presenters;

namespace MyLibrary_Test.Presenters_Tests
{
    [TestFixture]
    class AddBookPresenter_Tests
    {
        [TestCase("", "", "500.0")]
        [TestCase("0123456789", "", "500.0")]
        [TestCase("0123456789", "0123456789123", "500.0")]
        [TestCase("", "0123456789123", "500.0")]
        [TestCase("", "", "")]
        [TestCase("0123456789", "", "")]
        [TestCase("0123456789", "0123456789123", "")]
        [TestCase("", "0123456789123", "")]
        public void InputFieldsUpdated_Test_Valid(string isbnFieldText, string isbn13FieldText, string deweyDecimalFieldText)
        {
            // arrange
            var fakeView = A.Fake<IAddBookForm>();
            A.CallTo(() => fakeView.TitleFieldText).Returns("title");
            A.CallTo(() => fakeView.LongTitleFieldText).Returns("long title");
            A.CallTo(() => fakeView.LanguageFieldText).Returns("English");
            A.CallTo(() => fakeView.PagesFieldText).Returns("60");
            A.CallTo(() => fakeView.SelectedPublisher).Returns("publisher");
            A.CallTo(() => fakeView.Isbn13FieldText).Returns(isbn13FieldText);
            A.CallTo(() => fakeView.IsbnFieldText).Returns(isbnFieldText);
            A.CallTo(() => fakeView.DeweyDecimalFieldText).Returns(deweyDecimalFieldText);
            var fakeBookRepo = A.Fake<BookRepository>();
            var fakeTagRepo = A.Fake<TagRepository>();
            var fakeAuthorRepo = A.Fake<AuthorRepository>();
            var fakePublisherRepo = A.Fake<PublisherRepository>();
            MockBookPresenter presenter = new MockBookPresenter(fakeBookRepo, fakeTagRepo, fakeAuthorRepo, fakePublisherRepo, 
                fakeView);

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
        public void InputFieldsUpdated_Test_Invalid(string titleFieldText, string longTitleFieldText, string languageFieldText, string pagesFieldText, string selectedPublisher)
        {
            // arrange
            var fakeView = A.Fake<IAddBookForm>();
            A.CallTo(() => fakeView.TitleFieldText).Returns(titleFieldText);
            A.CallTo(() => fakeView.LongTitleFieldText).Returns(longTitleFieldText);
            A.CallTo(() => fakeView.LanguageFieldText).Returns(languageFieldText);
            A.CallTo(() => fakeView.PagesFieldText).Returns(pagesFieldText);
            var fakeBookRepo = A.Fake<BookRepository>();
            var fakeTagRepo = A.Fake<TagRepository>();
            var fakeAuthorRepo = A.Fake<AuthorRepository>();
            var fakePublisherRepo = A.Fake<PublisherRepository>();
            MockBookPresenter presenter = new MockBookPresenter(fakeBookRepo, fakeTagRepo, fakeAuthorRepo, fakePublisherRepo, 
                fakeView);

            // act
            presenter.InputFieldsUpdated(null, null);

            // assert
            Assert.IsFalse(fakeView.SaveButtonEnabled);
        }
    }//class

    public class MockBookPresenter : AddBookPresenter
    {
        public MockBookPresenter(BookRepository bookRepo, TagRepository tagRepo, AuthorRepository authorRepo, PublisherRepository publisherRepo, IAddBookForm view)
            :base(bookRepo, tagRepo, authorRepo, publisherRepo, view)
        {

        }
    }//class
}
