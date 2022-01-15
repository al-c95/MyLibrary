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
            var fakeBookRepo = A.Fake<IBookService>();
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
            var fakeMediaItemService = A.Fake<IMediaItemService>();
            // presenter
            MockItemPresenter presenter = new MockItemPresenter(fakeBookRepo, fakeMediaItemService, fakeView,
                allItems);

            // act
            presenter.PerformFilter(null,null);

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
            var fakeBookRepo = A.Fake<IBookService>();
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
            var fakeMediaItemService = A.Fake<IMediaItemService>();
            // presenter
            MockItemPresenter presenter = new MockItemPresenter(fakeBookRepo, fakeMediaItemService, fakeView,
                allItems);

            // act
            presenter.PerformFilter(null,null);

            // assert
            Assert.IsTrue(fakeView.DisplayedItems.Rows.Count == 2);
        }

        [Test]
        public void SelectedItemModified_Test()
        {
            // arrange
            var fakeBookRepo = A.Fake<IBookService>();
            var fakeMediaItemService = A.Fake<IMediaItemService>();
            var fakeView = A.Fake<IItemView>();
            ItemPresenter presenter = new ItemPresenter(fakeBookRepo, fakeMediaItemService, fakeView);

            // act
            presenter.SelectedItemModified(null, null);

            // assert
            Assert.IsTrue(fakeView.UpdateSelectedItemButtonEnabled);
            Assert.IsTrue(fakeView.DiscardSelectedItemChangesButtonEnabled);
        }

        [Test]
        public void DeleteButtonClicked_Test_Book()
        {
            // arrange
            var fakeBookRepo = A.Fake<IBookService>();
            var fakeMediaItemService = A.Fake<IMediaItemService>();
            var fakeView = A.Fake<IItemView>();
            A.CallTo(() => fakeView.CategoryDropDownSelectedIndex).Returns(0);
            ItemPresenter presenter = new ItemPresenter(fakeBookRepo, fakeMediaItemService, fakeView);

            // act
            presenter.DeleteButtonClicked(null, null);

            // assert
            A.CallTo(() => fakeBookRepo.DeleteById(fakeView.SelectedItemId)).MustHaveHappened();
        }

        [Test]
        public void DeleteButtonClicked_Test_MediaItem()
        {
            // arrange
            var fakeBookRepo = A.Fake<IBookService>();
            var fakeMediaItemService = A.Fake<IMediaItemService>();
            var fakeView = A.Fake<IItemView>();
            A.CallTo(() => fakeView.CategoryDropDownSelectedIndex).Returns(1);
            ItemPresenter presenter = new ItemPresenter(fakeBookRepo, fakeMediaItemService, fakeView);

            // act
            presenter.DeleteButtonClicked(null, null);

            // assert
            A.CallTo(() => fakeMediaItemService.DeleteById(fakeView.SelectedItemId)).MustHaveHappened();
        }

        [Test]
        public void DeleteButtonClicked_Test_Book_Error()
        {
            // arrange
            var fakeBookRepo = A.Fake<IBookService>();
            var fakeMediaItemService = A.Fake<IMediaItemService>();
            var fakeView = A.Fake<IItemView>();
            A.CallTo(() => fakeView.CategoryDropDownSelectedIndex).Returns(0);
            A.CallTo(() => fakeBookRepo.DeleteById(fakeView.SelectedItemId)).Throws(new Exception("error"));
            ItemPresenter presenter = new ItemPresenter(fakeBookRepo, fakeMediaItemService, fakeView);

            // act
            presenter.DeleteButtonClicked(null, null);

            // assert
            A.CallTo(() => fakeBookRepo.DeleteById(fakeView.SelectedItemId)).MustHaveHappened();
            A.CallTo(() => fakeView.ShowErrorDialog("Error deleting item.", "error")).MustHaveHappened();
        }

        [Test]
        public void DeleteButtonClicked_Test_MediaItem_Error()
        {
            // arrange
            var fakeBookRepo = A.Fake<IBookService>();
            var fakeMediaItemService = A.Fake<IMediaItemService>();
            var fakeView = A.Fake<IItemView>();
            A.CallTo(() => fakeView.CategoryDropDownSelectedIndex).Returns(1);
            A.CallTo(() => fakeMediaItemService.DeleteById(fakeView.SelectedItemId)).Throws(new Exception("error"));
            ItemPresenter presenter = new ItemPresenter(fakeBookRepo, fakeMediaItemService, fakeView);

            // act
            presenter.DeleteButtonClicked(null, null);

            // assert
            A.CallTo(() => fakeMediaItemService.DeleteById(fakeView.SelectedItemId)).MustHaveHappened();
            A.CallTo(() => fakeView.ShowErrorDialog("Error deleting item.", "error")).MustHaveHappened();
        }
    }//class

    public class MockItemPresenter : ItemPresenter
    {
        public MockItemPresenter(IBookService bookRepository, IMediaItemService mediaItemService, IItemView view,
            DataTable allItemsDt)
            :base(bookRepository, mediaItemService, view)
        {
            this._allItems = allItemsDt;
        }
    }//class
}