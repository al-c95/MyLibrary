﻿using System;
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
        public void InputFieldsUpdated_Test_Invalid_NoImageFilePath(string titleFieldText, string longTitleFieldText, string languageFieldText, string pagesFieldText, string selectedPublisher)
        {
            // arrange
            var fakeView = A.Fake<IAddBookForm>();
            A.CallTo(() => fakeView.TitleFieldText).Returns(titleFieldText);
            A.CallTo(() => fakeView.LongTitleFieldText).Returns(longTitleFieldText);
            A.CallTo(() => fakeView.LanguageFieldText).Returns(languageFieldText);
            A.CallTo(() => fakeView.PagesFieldText).Returns(pagesFieldText);
            A.CallTo(() => fakeView.ImageFilePathFieldText).Returns("");
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
