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
        [Test]
        public void InputFieldsUpdated_Test_Valid()
        {
            // arrange
            var fakeView = A.Fake<IAddBookForm>();
            A.CallTo(() => fakeView.TitleFieldText).Returns("title");
            A.CallTo(() => fakeView.LongTitleFieldText).Returns("long title");
            A.CallTo(() => fakeView.LanguageFieldText).Returns("English");
            A.CallTo(() => fakeView.PagesFieldText).Returns("60");
            var fakeBookRepo = A.Fake<BookRepository>();
            var fakeTagRepo = A.Fake<TagRepository>();
            MockBookPresenter presenter = new MockBookPresenter(fakeBookRepo, fakeTagRepo, fakeView);

            // act
            presenter.InputFieldsUpdated(null, null);

            // assert
            Assert.IsTrue(fakeView.SaveButtonEnabled);
        }

        [TestCase("title", "", "", "")]
        [TestCase("title", "long title", "", "")]
        [TestCase("title", "long title", "English", "")]
        [TestCase("", "", "", "")]
        [TestCase("", "", "English", "")]
        [TestCase("", "long title", "English", "")]
        [TestCase("", "", "", "60")]
        [TestCase("title", "long title", "English", "test")]
        public void InputFieldsUpdated_Test_Invalid(string titleFieldText, string longTitleFieldText, string languageFieldText, string pagesFieldText)
        {
            // arrange
            var fakeView = A.Fake<IAddBookForm>();
            A.CallTo(() => fakeView.TitleFieldText).Returns(titleFieldText);
            A.CallTo(() => fakeView.LongTitleFieldText).Returns(longTitleFieldText);
            A.CallTo(() => fakeView.LanguageFieldText).Returns(languageFieldText);
            A.CallTo(() => fakeView.PagesFieldText).Returns(pagesFieldText);
            var fakeBookRepo = A.Fake<BookRepository>();
            var fakeTagRepo = A.Fake<TagRepository>();
            MockBookPresenter presenter = new MockBookPresenter(fakeBookRepo, fakeTagRepo, fakeView);

            // act
            presenter.InputFieldsUpdated(null, null);

            // assert
            Assert.IsFalse(fakeView.SaveButtonEnabled);
        }
    }//class

    public class MockBookPresenter : AddBookPresenter
    {
        public MockBookPresenter(BookRepository bookRepo, TagRepository tagRepo, IAddBookForm view)
            :base(bookRepo, tagRepo, view)
        {

        }
    }//class
}
