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
    public class MainWindowPresenter_Tests
    {
        [Test]
        public void PerformFilter_Test_FiltersApplied()
        {
            // arrange
            // view
            var fakeView = A.Fake<IMainWindow>();
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
            var fakeTagService = A.Fake<ITagService>();
            var fakeAuthorService = A.Fake<IAuthorService>();
            var fakePublisherService = A.Fake<IPublisherService>();
            MockItemPresenter presenter = new MockItemPresenter(fakeBookRepo, fakeMediaItemService, fakeTagService, fakeAuthorService, fakePublisherService, fakeView,
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
            var fakeView = A.Fake<IMainWindow>();
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
            var fakeTagService = A.Fake<ITagService>();
            var fakeAuthorService = A.Fake<IAuthorService>();
            var fakePublisherService = A.Fake<IPublisherService>();
            MockItemPresenter presenter = new MockItemPresenter(fakeBookRepo, fakeMediaItemService, fakeTagService, fakeAuthorService, fakePublisherService, fakeView,
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
            var fakeTagService = A.Fake<ITagService>();
            var fakeAuthorService = A.Fake<IAuthorService>();
            var fakePublisherService = A.Fake<IPublisherService>();
            var fakeView = A.Fake<IMainWindow>();
            MainWindowPresenter presenter = new MainWindowPresenter(fakeBookRepo, fakeMediaItemService, fakeTagService, fakeAuthorService, fakePublisherService, fakeView);

            // act
            presenter.SelectedItemModified(null, null);

            // assert
            Assert.IsTrue(fakeView.UpdateSelectedItemButtonEnabled);
            Assert.IsTrue(fakeView.DiscardSelectedItemChangesButtonEnabled);
        }

        [Test]
        public async Task DeleteButtonClicked_Test_Book()
        {
            // arrange
            var fakeBookRepo = A.Fake<IBookService>();
            var fakeMediaItemService = A.Fake<IMediaItemService>();
            var fakeTagService = A.Fake<ITagService>();
            var fakeAuthorService = A.Fake<IAuthorService>();
            var fakePublisherService = A.Fake<IPublisherService>();
            var fakeView = A.Fake<IMainWindow>();
            A.CallTo(() => fakeView.CategoryDropDownSelectedIndex).Returns(0);
            MainWindowPresenter presenter = new MainWindowPresenter(fakeBookRepo, fakeMediaItemService, fakeTagService, fakeAuthorService, fakePublisherService, fakeView);

            // act
            await presenter.HandleDeleteButtonClicked(null, null);

            // assert
            A.CallTo(() => fakeBookRepo.DeleteById(fakeView.SelectedItemId)).MustHaveHappened();
        }

        [Test]
        public async Task DeleteButtonClicked_Test_MediaItem()
        {
            // arrange
            var fakeBookRepo = A.Fake<IBookService>();
            var fakeMediaItemService = A.Fake<IMediaItemService>();
            var fakeTagService = A.Fake<ITagService>();
            var fakeAuthorService = A.Fake<IAuthorService>();
            var fakePublisherService = A.Fake<IPublisherService>();
            var fakeView = A.Fake<IMainWindow>();
            A.CallTo(() => fakeView.CategoryDropDownSelectedIndex).Returns(1);
            MainWindowPresenter presenter = new MainWindowPresenter(fakeBookRepo, fakeMediaItemService, fakeTagService, fakeAuthorService, fakePublisherService, fakeView);

            // act
            await presenter.HandleDeleteButtonClicked(null, null);

            // assert
            A.CallTo(() => fakeMediaItemService.DeleteByIdAsync(fakeView.SelectedItemId)).MustHaveHappened();
        }

        [Test]
        public async Task DeleteButtonClicked_Test_Book_Error()
        {
            // arrange
            var fakeBookRepo = A.Fake<IBookService>();
            var fakeMediaItemService = A.Fake<IMediaItemService>();
            var fakeTagService = A.Fake<ITagService>();
            var fakeAuthorService = A.Fake<IAuthorService>();
            var fakePublisherService = A.Fake<IPublisherService>();
            var fakeView = A.Fake<IMainWindow>();
            A.CallTo(() => fakeView.CategoryDropDownSelectedIndex).Returns(0);
            A.CallTo(() => fakeBookRepo.DeleteById(fakeView.SelectedItemId)).Throws(new Exception("error"));
            MainWindowPresenter presenter = new MainWindowPresenter(fakeBookRepo, fakeMediaItemService, fakeTagService, fakeAuthorService, fakePublisherService, fakeView);

            // act
            await presenter.HandleDeleteButtonClicked(null, null);

            // assert
            A.CallTo(() => fakeBookRepo.DeleteById(fakeView.SelectedItemId)).MustHaveHappened();
            A.CallTo(() => fakeView.ShowErrorDialog("Error deleting item.", "error")).MustHaveHappened();
        }

        [Test]
        public async Task DeleteButtonClicked_Test_MediaItem_Error()
        {
            // arrange
            var fakeBookRepo = A.Fake<IBookService>();
            var fakeMediaItemService = A.Fake<IMediaItemService>();
            var fakeTagService = A.Fake<ITagService>();
            var fakeAuthorService = A.Fake<IAuthorService>();
            var fakePublisherService = A.Fake<IPublisherService>();
            var fakeView = A.Fake<IMainWindow>();
            A.CallTo(() => fakeView.CategoryDropDownSelectedIndex).Returns(1);
            A.CallTo(() => fakeMediaItemService.DeleteByIdAsync(fakeView.SelectedItemId)).Throws(new Exception("error"));
            MainWindowPresenter presenter = new MainWindowPresenter(fakeBookRepo, fakeMediaItemService, fakeTagService, fakeAuthorService, fakePublisherService, fakeView);

            // act
            await presenter.HandleDeleteButtonClicked(null, null);

            // assert
            A.CallTo(() => fakeMediaItemService.DeleteByIdAsync(fakeView.SelectedItemId)).MustHaveHappened();
            A.CallTo(() => fakeView.ShowErrorDialog("Error deleting item.", "error")).MustHaveHappened();
        }
    }//class

    public class MockItemPresenter : MainWindowPresenter
    {
        public MockItemPresenter(IBookService bookRepository, IMediaItemService mediaItemService, ITagService tagService, IAuthorService authorService, IPublisherService publisherService,
            IMainWindow view,
            DataTable allItemsDt)
            :base(bookRepository, mediaItemService, tagService, authorService, publisherService, view)
        {
            this._allItems = allItemsDt;
        }
    }//class
}