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
using MyLibrary.Views;
using MyLibrary.Presenters;
using MyLibrary.ApiService;
using MyLibrary.Models.Entities;

namespace MyLibrary_Test.Presenters_Tests
{
    [TestFixture]
    class SearchByIsbnPresenter_Tests
    {
        [TestCase("0123456789")]
        [TestCase("0123456789012")]
        public void IsbnFieldTextChanged_Test_Valid(string isbnFieldText)
        {
            // arrange
            var fakeSearchByIsbnDialog = A.Fake<ISearchByIsbn>();
            A.CallTo(() => fakeSearchByIsbnDialog.IsbnFieldText).Returns(isbnFieldText);
            BookApiService apiService = new BookApiService();
            SearchByIsbnPresenter presenter = new SearchByIsbnPresenter(fakeSearchByIsbnDialog,null);

            // act
            presenter.IsbnFieldTextChanged(null, null);

            // assert
            Assert.IsTrue(fakeSearchByIsbnDialog.SearchButtonEnabled);
        }

        [Test]
        public void IsbnFieldTextChanged_Test_Invalid()
        {
            // arrange
            var fakeSearchByIsbnDialog = A.Fake<ISearchByIsbn>();
            A.CallTo(() => fakeSearchByIsbnDialog.IsbnFieldText).Returns("");
            BookApiService apiService = new BookApiService();
            SearchByIsbnPresenter presenter = new SearchByIsbnPresenter(fakeSearchByIsbnDialog,null);

            // act
            presenter.IsbnFieldTextChanged(null, null);

            // assert
            Assert.IsFalse(fakeSearchByIsbnDialog.SearchButtonEnabled);
        }
    }//class
}
