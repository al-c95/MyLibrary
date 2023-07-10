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
using NUnit;
using NUnit.Framework;
using FakeItEasy;
using MyLibrary.Views;
using MyLibrary.Presenters;
using MyLibrary.ApiService;
using MyLibrary.Models.Entities;
using MyLibrary.Models.Entities.Factories;
using MyLibrary.Models.BusinessLogic;
using MyLibrary.Presenters.ServiceProviders;

namespace MyLibrary_Test.Presenters_Tests
{
    [TestFixture]
    class SearchByIsbnPresenter_Tests
    {
        AddBookPresenter _addBookPresenter;

        public SearchByIsbnPresenter_Tests()
        {
            var fakeBookRepo = A.Fake<IBookService>();
            var fakeTagService = A.Fake<ITagService>();
            var fakeAuthorService = A.Fake<IAuthorService>();
            var fakePublisherService = A.Fake<IPublisherService>();
            var fakeDialog = A.Fake<IAddBookForm>();
            this._addBookPresenter = new AddBookPresenter(fakeBookRepo, fakeTagService, fakeAuthorService, fakePublisherService, null, fakeDialog, null);
        }

        [TestCase("0123456789")]
        [TestCase("0123456789012")]
        public void IsbnFieldTextChanged_Test_Valid_ScanModeOn(string isbnFieldText)
        {
            // arrange
            var fakeSearchByIsbnDialog = A.Fake<ISearchByIsbn>();
            A.CallTo(() => fakeSearchByIsbnDialog.IsbnFieldText).Returns(isbnFieldText);
            A.CallTo(() => fakeSearchByIsbnDialog.ScanModeChecked).Returns(true);
            SearchByIsbnPresenter presenter = new SearchByIsbnPresenter(fakeSearchByIsbnDialog,null,null,null);
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
            SearchByIsbnPresenter presenter = new SearchByIsbnPresenter(fakeSearchByIsbnDialog, null, null, null);
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
        [TestCase("bogus_isbn", true)]
        [TestCase("bogus_isbn", false)]
        public void IsbnFieldTextChanged_Test_Invalid(string isbn, bool scanModeEnabled)
        {
            // arrange
            var fakeSearchByIsbnDialog = A.Fake<ISearchByIsbn>();
            A.CallTo(() => fakeSearchByIsbnDialog.ScanModeChecked).Returns(scanModeEnabled);
            A.CallTo(() => fakeSearchByIsbnDialog.IsbnFieldText).Returns(isbn);
            SearchByIsbnPresenter presenter = new SearchByIsbnPresenter(fakeSearchByIsbnDialog,null,null,null);
            presenter.AddBookPresenter = this._addBookPresenter;

            // act
            presenter.IsbnFieldTextChanged(null, null);

            // assert
            Assert.IsFalse(fakeSearchByIsbnDialog.SearchButtonEnabled);
            A.CallTo(() => fakeSearchByIsbnDialog.ClickSearchButton()).MustNotHaveHappened();
        }

        [Test]
        public void SearchButtonClicked_Test_BookNotFound()
        {
            // arrange
            string isbn = "0123456789";
            var fakeSearchByIsbnDialog = A.Fake<ISearchByIsbn>();
            A.CallTo(() => fakeSearchByIsbnDialog.IsbnFieldText).Returns(isbn);
            var fakeBookService = A.Fake<IBookService>();
            A.CallTo(() => fakeBookService.ExistsWithIsbnAsync("0123456789")).Returns(false);
            var fakeApiServiceProvider = A.Fake<IApiServiceProvider>();
            var fakeApiService = A.Fake<IBookApiService>();
            A.CallTo(() => fakeApiServiceProvider.Get()).Returns(fakeApiService);
            A.CallTo(() => fakeApiService.GetBookByIsbnAsync("0123456789")).Throws(new BookNotFoundException("0123456789"));
            var presenter = new SearchByIsbnPresenter(fakeSearchByIsbnDialog, null, fakeBookService, fakeApiServiceProvider);
            presenter.AddBookPresenter = this._addBookPresenter;

            // act
            presenter.SearchButtonClicked(null, null);

            // assert
            A.CallTo(() => fakeSearchByIsbnDialog.ShowCouldNotFindBookDialog("0123456789")).MustHaveHappened();
        }

        [Test]
        public void SearchButtonClicked_Test_HttpRequestException()
        {
            // arrange
            string isbn = "0123456789";
            var fakeSearchByIsbnDialog = A.Fake<ISearchByIsbn>();
            A.CallTo(() => fakeSearchByIsbnDialog.IsbnFieldText).Returns(isbn);
            var fakeBookService = A.Fake<IBookService>();
            A.CallTo(() => fakeBookService.ExistsWithIsbnAsync("0123456789")).Returns(false);
            var fakeApiServiceProvider = A.Fake<IApiServiceProvider>();
            var fakeApiService = A.Fake<IBookApiService>();
            A.CallTo(() => fakeApiServiceProvider.Get()).Returns(fakeApiService);
            Exception innerException = new Exception("The remote name could not be resolved: 'openlibrary.org'");
            System.Net.Http.HttpRequestException httpRequestException = new System.Net.Http.HttpRequestException("", innerException);
            A.CallTo(() => fakeApiService.GetBookByIsbnAsync("0123456789")).Throws(httpRequestException);
            var presenter = new SearchByIsbnPresenter(fakeSearchByIsbnDialog, null, fakeBookService, fakeApiServiceProvider);
            presenter.AddBookPresenter = this._addBookPresenter;

            // act
            presenter.SearchButtonClicked(null, null);

            // assert
            A.CallTo(() => fakeSearchByIsbnDialog.ShowConnectionErrorDialog()).MustHaveHappened();
        }

        [Test]
        public void SearchButtonClicked_Test_OtherError()
        {
            // arrange
            string isbn = "0123456789";
            var fakeSearchByIsbnDialog = A.Fake<ISearchByIsbn>();
            A.CallTo(() => fakeSearchByIsbnDialog.IsbnFieldText).Returns(isbn);
            var fakeBookService = A.Fake<IBookService>();
            A.CallTo(() => fakeBookService.ExistsWithIsbnAsync("0123456789")).Returns(false);
            var fakeApiServiceProvider = A.Fake<IApiServiceProvider>();
            var fakeApiService = A.Fake<IBookApiService>();
            A.CallTo(() => fakeApiServiceProvider.Get()).Returns(fakeApiService);
            A.CallTo(() => fakeApiService.GetBookByIsbnAsync("0123456789")).Throws(new Exception("error"));
            var presenter = new SearchByIsbnPresenter(fakeSearchByIsbnDialog, null, fakeBookService, fakeApiServiceProvider);
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
            A.CallTo(() => fakeRepo.ExistsWithIsbnAsync("0123456789")).Returns(false);
            var fakeApiServiceProvider = A.Fake<IApiServiceProvider>();
            var fakeApiService = A.Fake<IBookApiService>();
            A.CallTo(() => fakeApiServiceProvider.Get()).Returns(fakeApiService);
            A.CallTo(() => fakeApiService.GetBookByIsbnAsync("0123456789")).Returns(new Book { Title = "book", Publisher = new Publisher { Name = "publisher" } });
            var fakeAddBookDialog = A.Fake<IAddBookForm>();
            var presenter = new SearchByIsbnPresenter(fakeSearchByIsbnDialog, fakeAddBookDialog, fakeRepo, fakeApiServiceProvider);
            presenter.AddBookPresenter = this._addBookPresenter;

            // act
            presenter.SearchButtonClicked(null, null);

            // assert
            A.CallTo(() => fakeAddBookDialog.ShowAsDialog()).MustHaveHappened();
        }
    }//class
}
