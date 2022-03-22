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
using MyLibrary.Utils;
using MyLibrary.Views;
using MyLibrary.Presenters;
using MyLibrary.ApiService;
using MyLibrary.Models.Entities;
using MyLibrary.Models.BusinessLogic;
using MyLibrary.Presenters.ServiceProviders;

namespace MyLibrary_Test.Presenters_Tests
{
    [TestFixture]
    class SearchByIsbnPresenter_Tests
    {
        MockAddBookPresenter _addBookPresenter;

        public SearchByIsbnPresenter_Tests()
        {
            var fakeBookRepo = A.Fake<IBookService>();
            var fakeTagService = A.Fake<ITagService>();
            var fakeAuthorService = A.Fake<IAuthorService>();
            var fakePublisherService = A.Fake<IPublisherService>();
            var fakeDialog = A.Fake<IAddBookForm>();
            this._addBookPresenter = new MockAddBookPresenter(fakeBookRepo, fakeTagService, fakeAuthorService, fakePublisherService, fakeDialog, null);
        }

        [TestCase("0123456789")]
        [TestCase("0123456789012")]
        public void IsbnFieldTextChanged_Test_Valid_ScanModeOn(string isbnFieldText)
        {
            // arrange
            var fakeSearchByIsbnDialog = A.Fake<ISearchByIsbn>();
            A.CallTo(() => fakeSearchByIsbnDialog.IsbnFieldText).Returns(isbnFieldText);
            A.CallTo(() => fakeSearchByIsbnDialog.ScanModeChecked).Returns(true);
            SearchByIsbnPresenter presenter = new SearchByIsbnPresenter(fakeSearchByIsbnDialog,null,null,null,null);
            presenter.AddBookPresenter = this._addBookPresenter;

            // act
            presenter.IsbnFieldTextChanged(null, null);

            // assert
            Assert.IsTrue(fakeSearchByIsbnDialog.SearchButtonEnabled);
            A.CallTo(() => fakeSearchByIsbnDialog.ClickSearchButton()).MustHaveHappened();
        }

        [TestCase("0123456789")]
        [TestCase("0123456789012")]
        public void IsbnFieldTextChanged_Test_Valid_ScanModeOff(string isbnFieldText)
        {
            // arrange
            var fakeSearchByIsbnDialog = A.Fake<ISearchByIsbn>();
            A.CallTo(() => fakeSearchByIsbnDialog.IsbnFieldText).Returns(isbnFieldText);
            A.CallTo(() => fakeSearchByIsbnDialog.ScanModeChecked).Returns(false);
            SearchByIsbnPresenter presenter = new SearchByIsbnPresenter(fakeSearchByIsbnDialog, null, null, null, null);
            presenter.AddBookPresenter = this._addBookPresenter;

            // act
            presenter.IsbnFieldTextChanged(null, null);

            // assert
            Assert.IsTrue(fakeSearchByIsbnDialog.SearchButtonEnabled);
            A.CallTo(() => fakeSearchByIsbnDialog.ClickSearchButton()).MustNotHaveHappened();
        }

        [TestCase("", true)]
        [TestCase(null, true)]
        [TestCase("", false)]
        [TestCase(null, false)]
        public void IsbnFieldTextChanged_Test_Invalid(string isbn, bool scanModeEnabled)
        {
            // arrange
            var fakeSearchByIsbnDialog = A.Fake<ISearchByIsbn>();
            A.CallTo(() => fakeSearchByIsbnDialog.ScanModeChecked).Returns(scanModeEnabled);
            A.CallTo(() => fakeSearchByIsbnDialog.IsbnFieldText).Returns(isbn);
            SearchByIsbnPresenter presenter = new SearchByIsbnPresenter(fakeSearchByIsbnDialog,null,null,null,null);
            presenter.AddBookPresenter = this._addBookPresenter;

            // act
            presenter.IsbnFieldTextChanged(null, null);

            // assert
            Assert.IsFalse(fakeSearchByIsbnDialog.SearchButtonEnabled);
            A.CallTo(() => fakeSearchByIsbnDialog.ClickSearchButton()).MustNotHaveHappened();
        }
        
        [Test]
        public void SearchButtonClicked_Test_AlreadyExists()
        {
            // arrange
            string isbn = "0123456789";
            var fakeSearchByIsbnDialog = A.Fake<ISearchByIsbn>();
            A.CallTo(() => fakeSearchByIsbnDialog.IsbnFieldText).Returns(isbn);
            var fakeRepo = A.Fake<IBookService>();
            A.CallTo(() => fakeRepo.ExistsWithIsbn("0123456789")).Returns(true);
            var presenter = new MockPresenter(fakeSearchByIsbnDialog, null, null, fakeRepo, null);
            presenter.AddBookPresenter = this._addBookPresenter;

            // act
            presenter.SearchButtonClicked(null, null);

            // assert
            A.CallTo(() => fakeSearchByIsbnDialog.ShowAlreadyExistsWithIsbnDialog(isbn)).MustHaveHappened();
        }

        [Test]
        public void SearchButtonClicked_Test_BookNotFound()
        {
            // arrange
            string isbn = "0123456789";
            var fakeSearchByIsbnDialog = A.Fake<ISearchByIsbn>();
            A.CallTo(() => fakeSearchByIsbnDialog.IsbnFieldText).Returns(isbn);
            var fakeRepo = A.Fake<IBookService>();
            A.CallTo(() => fakeRepo.ExistsWithIsbn("0123456789")).Returns(false);
            var fakeApiServiceProvider = A.Fake<IApiServiceProvider>();
            var fakeApiService = A.Fake<IBookApiService>();
            A.CallTo(() => fakeApiServiceProvider.Get()).Returns(fakeApiService);
            A.CallTo(() => fakeApiService.GetBookByIsbnAsync("0123456789")).Throws(new BookNotFoundException("0123456789"));
            var presenter = new MockPresenter(fakeSearchByIsbnDialog, null, null, fakeRepo, fakeApiServiceProvider);
            presenter.AddBookPresenter = this._addBookPresenter;

            // act
            presenter.SearchButtonClicked(null, null);

            // assert
            A.CallTo(() => fakeSearchByIsbnDialog.ShowCouldNotFindBookDialog("0123456789")).MustHaveHappened();
        }

        [Test]
        public void SearchButtonClicked_Test_OtherError()
        {
            // arrange
            string isbn = "0123456789";
            var fakeSearchByIsbnDialog = A.Fake<ISearchByIsbn>();
            A.CallTo(() => fakeSearchByIsbnDialog.IsbnFieldText).Returns(isbn);
            var fakeRepo = A.Fake<IBookService>();
            A.CallTo(() => fakeRepo.ExistsWithIsbn("0123456789")).Returns(false);
            var fakeApiServiceProvider = A.Fake<IApiServiceProvider>();
            var fakeApiService = A.Fake<IBookApiService>();
            A.CallTo(() => fakeApiServiceProvider.Get()).Returns(fakeApiService);
            A.CallTo(() => fakeApiService.GetBookByIsbnAsync("0123456789")).Throws(new Exception("error"));
            var presenter = new MockPresenter(fakeSearchByIsbnDialog, null, null, fakeRepo, fakeApiServiceProvider);
            presenter.AddBookPresenter = this._addBookPresenter;

            // act
            presenter.SearchButtonClicked(null, null);

            // assert
            A.CallTo(() => fakeSearchByIsbnDialog.ShowErrorDialog("error")).MustHaveHappened();
        }

        [Test]
        public void SearchButtonClicked_Test_Success()
        {
            // arrange
            string isbn = "0123456789";
            var fakeSearchByIsbnDialog = A.Fake<ISearchByIsbn>();
            A.CallTo(() => fakeSearchByIsbnDialog.IsbnFieldText).Returns(isbn);
            var fakeRepo = A.Fake<IBookService>();
            A.CallTo(() => fakeRepo.ExistsWithIsbn("0123456789")).Returns(false);
            var fakeApiServiceProvider = A.Fake<IApiServiceProvider>();
            var fakeApiService = A.Fake<IBookApiService>();
            A.CallTo(() => fakeApiServiceProvider.Get()).Returns(fakeApiService);
            A.CallTo(() => fakeApiService.GetBookByIsbnAsync("0123456789")).Returns(new Book { Title = "book", Publisher = new Publisher { Name = "publisher" } });
            var fakeAddBookDialog = A.Fake<IAddBookForm>();
            var presenter = new MockPresenter(fakeSearchByIsbnDialog, null, fakeAddBookDialog, fakeRepo, fakeApiServiceProvider);
            presenter.AddBookPresenter = this._addBookPresenter;

            // act
            presenter.SearchButtonClicked(null, null);

            // assert
            A.CallTo(() => fakeAddBookDialog.ShowAsDialog()).MustHaveHappened();
        }
        
        class MockPresenter : SearchByIsbnPresenter
        {
            public MockPresenter(ISearchByIsbn view, IItemView mainView, IAddBookForm addBookView,
            IBookService bookRepo,
            IApiServiceProvider apiServiceProvider)
                :base(view, mainView, addBookView, bookRepo, apiServiceProvider)
            {

            }
        }

        class MockAddBookPresenter : AddBookPresenter
        {
            public MockAddBookPresenter(IBookService bookRepository, ITagService tagService, IAuthorService authorService, IPublisherService publisherService,
            IAddBookForm view, IImageFileReader imageFileReader)
                :base(bookRepository, tagService, authorService, publisherService, view, imageFileReader)
            {

            }
        }
    }//class
}
