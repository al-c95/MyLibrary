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
using MyLibrary.BusinessLogic;
using MyLibrary.Models.Entities;
using MyLibrary.DataAccessLayer;
using MyLibrary.Views;
using MyLibrary.Presenters;

namespace MyLibrary_Test.Presenters_Tests
{
    [TestFixture]
    public class ItemPresenter_Tests
    {
        [Test]
        public void PerformFilter_Test_FiltersApplied()
        {
            // arrange
            // view
            var fakeView = A.Fake<IItemView>();
            fakeView.CategoryDropDownSelectedIndex = 0;
            A.CallTo(() => fakeView.TitleFilterText).Returns("book 1");
            // repos
            var fakeBookRepo = A.Fake<IBookRepository>();
            DataTable allItems = new DataTable();
            allItems.Columns.Add("Id");
            allItems.Columns.Add("Title");
            allItems.Rows.Add(
                    1,
                    "book 1"
                );
            allItems.Rows.Add(
                    2,
                    "book 2"
                );
            var fakeMediaItemRepo = A.Fake<IMediaItemRepository>();
            // presenter
            MockItemPresenter presenter = new MockItemPresenter(fakeBookRepo, fakeMediaItemRepo, fakeView,
                allItems);

            // act
            presenter.PerformFilter();

            // assert
            Assert.IsTrue(fakeView.DisplayedItems.Rows.Count == 1);
            Assert.AreEqual("book 1", fakeView.DisplayedItems.Rows[0].ItemArray[1].ToString());
        }

        [Test]
        public void PerformFilter_Test_NoFilters()
        {
            // arrange
            // view
            var fakeView = A.Fake<IItemView>();
            fakeView.CategoryDropDownSelectedIndex = 0;
            A.CallTo(() => fakeView.TitleFilterText).Returns("");
            // repos
            var fakeBookRepo = A.Fake<IBookRepository>();
            DataTable allItems = new DataTable();
            allItems.Columns.Add("Id");
            allItems.Columns.Add("Title");
            allItems.Rows.Add(
                    1,
                    "book 1"
                );
            allItems.Rows.Add(
                    2,
                    "book 2"
                );
            var fakeMediaItemRepo = A.Fake<IMediaItemRepository>();
            // presenter
            MockItemPresenter presenter = new MockItemPresenter(fakeBookRepo, fakeMediaItemRepo, fakeView,
                allItems);

            // act
            presenter.PerformFilter();

            // assert
            Assert.IsTrue(fakeView.DisplayedItems.Rows.Count == 2);
        }

        [Test]
        public void SelectedItemModified_Test()
        {
            // arrange
            var fakeBookRepo = A.Fake<IBookRepository>();
            var fakeMediaItemRepo = A.Fake<IMediaItemRepository>();
            var fakeView = A.Fake<IItemView>();
            ItemPresenter presenter = new ItemPresenter(fakeBookRepo, fakeMediaItemRepo, fakeView);

            // act
            presenter.SelectedItemModified(null, null);

            // assert
            Assert.IsTrue(fakeView.UpdateSelectedItemButtonEnabled);
            Assert.IsTrue(fakeView.DiscardSelectedItemChangesButtonEnabled);
        }
    }//class

    public class MockItemPresenter : ItemPresenter
    {
        public MockItemPresenter(IBookRepository bookRepository, IMediaItemRepository mediaItemRepository, IItemView view,
            DataTable allItemsDt)
            :base(bookRepository, mediaItemRepository, view)
        {
            this._allItems = allItemsDt;
        }
    }
}
