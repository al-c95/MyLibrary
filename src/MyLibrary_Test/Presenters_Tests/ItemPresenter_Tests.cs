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
    public class ItemPresenter_Tests
    {
        [Test]
        public void PerformFilter_Test_FiltersApplied()
        {
            // arrange
            // view
            var fakeView = A.Fake<IItemView>();
            fakeView.CategoryDropDownSelectedIndex = 0;
            A.CallTo(() => fakeView.TitleFilterText).Returns("book 2");
            A.CallTo(() => fakeView.SelectedFilterTags).Returns(new List<string> { "tag2" });
            // repos
            var fakeBookRepo = A.Fake<BookRepository>();
            DataTable allItems = new DataTable();
            allItems.Columns.Add("Id");
            allItems.Columns.Add("Title");
            allItems.Columns.Add("Tags");
            allItems.Rows.Add(
                    1,
                    "book 1",
                    "tag1"
                );
            allItems.Rows.Add(
                    2,
                    "book 2",
                    "tag2"
                );
            allItems.Rows.Add(
                    3,
                    "third book",
                    "tag2"
                );
            var fakeMediaItemRepo = A.Fake<MediaItemRepository>();
            // presenter
            MockItemPresenter presenter = new MockItemPresenter(fakeBookRepo, fakeMediaItemRepo, fakeView,
                allItems);

            // act
            presenter.PerformFilter();

            // assert
            Assert.IsTrue(fakeView.DisplayedItems.Rows.Count == 1);
            Assert.AreEqual("book 2", fakeView.DisplayedItems.Rows[0].ItemArray[1].ToString());
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
            var fakeBookRepo = A.Fake<BookRepository>();
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
            var fakeMediaItemRepo = A.Fake<MediaItemRepository>();
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
            var fakeBookRepo = A.Fake<BookRepository>();
            var fakeMediaItemRepo = A.Fake<MediaItemRepository>();
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
        public MockItemPresenter(BookRepository bookRepository, MediaItemRepository mediaItemRepository, IItemView view,
            DataTable allItemsDt)
            :base(bookRepository, mediaItemRepository, view)
        {
            this._allItems = allItemsDt;
        }
    }//class
}