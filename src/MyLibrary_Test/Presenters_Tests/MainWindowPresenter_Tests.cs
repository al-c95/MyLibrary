using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Data;
using NUnit.Framework;
using FakeItEasy;
using MyLibrary;
using MyLibrary.Models.Entities;
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
            var fakeBookService = A.Fake<IBookService>();
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
            MockItemPresenter presenter = new MockItemPresenter(fakeBookService, fakeMediaItemService, fakeTagService, fakeAuthorService, fakePublisherService, fakeView,
                allItems,
                null);

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
            var fakeBookService = A.Fake<IBookService>();
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
            MockItemPresenter presenter = new MockItemPresenter(fakeBookService, fakeMediaItemService, fakeTagService, fakeAuthorService, fakePublisherService, fakeView,
                allItems,
                null);

            // act
            presenter.PerformFilter(null,null);

            // assert
            Assert.IsTrue(fakeView.DisplayedItems.Rows.Count == 2);
        }

        [Test]
        public void SelectedItemModified_Test()
        {
            // arrange
            var fakeBookService = A.Fake<IBookService>();
            var fakeMediaItemService = A.Fake<IMediaItemService>();
            var fakeTagService = A.Fake<ITagService>();
            var fakeAuthorService = A.Fake<IAuthorService>();
            var fakePublisherService = A.Fake<IPublisherService>();
            var fakeView = A.Fake<IMainWindow>();
            MainWindowPresenter presenter = new MainWindowPresenter(fakeBookService, fakeMediaItemService, fakeTagService, fakeAuthorService, fakePublisherService, fakeView);

            // act
            presenter.SelectedItemModified(null, null);

            // assert
            Assert.IsTrue(fakeView.UpdateSelectedItemButtonEnabled);
            Assert.IsTrue(fakeView.DiscardSelectedItemChangesButtonEnabled);
        }

        [Test]
        public async Task HandleDeleteButtonClicked_Test_UserSaysNoToConfirmation()
        {
            // arrange
            var fakeBookService = A.Fake<IBookService>();
            var fakeMediaItemService = A.Fake<IMediaItemService>();
            var fakeTagService = A.Fake<ITagService>();
            var fakeAuthorService = A.Fake<IAuthorService>();
            var fakePublisherService = A.Fake<IPublisherService>();
            var fakeView = A.Fake<IMainWindow>();
            A.CallTo(() => fakeView.CategoryDropDownSelectedIndex).Returns(0);
            A.CallTo(() => fakeView.ShowDeleteConfirmationDialog("item")).Returns(false);
            MediaItem item = new MediaItem
            {
                Id = 1,
                Title = "item"
            };
            A.CallTo(() => fakeView.SelectedItem).Returns(item);
            MainWindowPresenter presenter = new MainWindowPresenter(fakeBookService, fakeMediaItemService, fakeTagService, fakeAuthorService, fakePublisherService, fakeView);

            // act
            await presenter.HandleDeleteButtonClicked(null, null);

            // assert
            A.CallTo(() => fakeBookService.DeleteByIdAsync(fakeView.SelectedItemId)).MustNotHaveHappened();
        }

        [Test]
        public async Task HandleDeleteButtonClicked_Test_Book_UserConfirmsDelete()
        {
            // arrange
            var fakeBookService = A.Fake<IBookService>();
            var fakeMediaItemService = A.Fake<IMediaItemService>();
            var fakeTagService = A.Fake<ITagService>();
            var fakeAuthorService = A.Fake<IAuthorService>();
            var fakePublisherService = A.Fake<IPublisherService>();
            var fakeView = A.Fake<IMainWindow>();
            Book book = new Book
            {
                Id = 1,
                Title = "book"
            };
            A.CallTo(() => fakeView.CategoryDropDownSelectedIndex).Returns(0);
            A.CallTo(() => fakeView.ShowDeleteConfirmationDialog("book")).Returns(true);
            A.CallTo(() => fakeView.SelectedItem).Returns(book);
            MainWindowPresenter presenter = new MainWindowPresenter(fakeBookService, fakeMediaItemService, fakeTagService, fakeAuthorService, fakePublisherService, fakeView);

            // act
            await presenter.HandleDeleteButtonClicked(null, null);

            // assert
            A.CallTo(() => fakeBookService.DeleteByIdAsync(fakeView.SelectedItemId)).MustHaveHappened();
        }

        [Test]
        public async Task HandleDeleteButtonClicked_Test_MediaItem_UserConfirmsDelete()
        {
            // arrange
            var fakeBookService = A.Fake<IBookService>();
            var fakeMediaItemService = A.Fake<IMediaItemService>();
            var fakeTagService = A.Fake<ITagService>();
            var fakeAuthorService = A.Fake<IAuthorService>();
            var fakePublisherService = A.Fake<IPublisherService>();
            var fakeView = A.Fake<IMainWindow>();
            MediaItem item = new MediaItem
            {
                Id = 1,
                Title = "item"
            };
            A.CallTo(() => fakeView.SelectedItem).Returns(item);
            A.CallTo(() => fakeView.CategoryDropDownSelectedIndex).Returns(1);
            A.CallTo(() => fakeView.ShowDeleteConfirmationDialog("item")).Returns(true);
            A.CallTo(() => fakeView.SelectedItem).Returns(item);
            MainWindowPresenter presenter = new MainWindowPresenter(fakeBookService, fakeMediaItemService, fakeTagService, fakeAuthorService, fakePublisherService, fakeView);

            // act
            await presenter.HandleDeleteButtonClicked(null, null);

            // assert
            A.CallTo(() => fakeMediaItemService.DeleteByIdAsync(fakeView.SelectedItemId)).MustHaveHappened();
        }

        [Test]
        public async Task HandleDeleteButtonClicked_Test_Book_Error()
        {
            // arrange
            var fakeBookService = A.Fake<IBookService>();
            var fakeMediaItemService = A.Fake<IMediaItemService>();
            var fakeTagService = A.Fake<ITagService>();
            var fakeAuthorService = A.Fake<IAuthorService>();
            var fakePublisherService = A.Fake<IPublisherService>();
            var fakeView = A.Fake<IMainWindow>();
            Book book = new Book
            {
                Id = 1,
                Title = "book"
            };
            A.CallTo(() => fakeView.ShowDeleteConfirmationDialog("book")).Returns(true);
            A.CallTo(() => fakeView.SelectedItem).Returns(book);
            A.CallTo(() => fakeView.CategoryDropDownSelectedIndex).Returns(0);
            A.CallTo(() => fakeBookService.DeleteByIdAsync(fakeView.SelectedItemId)).Throws(new Exception("error"));
            MainWindowPresenter presenter = new MainWindowPresenter(fakeBookService, fakeMediaItemService, fakeTagService, fakeAuthorService, fakePublisherService, fakeView);

            // act
            await presenter.HandleDeleteButtonClicked(null, null);

            // assert
            A.CallTo(() => fakeBookService.DeleteByIdAsync(fakeView.SelectedItemId)).MustHaveHappened();
            A.CallTo(() => fakeView.ShowErrorDialog("Error deleting item.", "error")).MustHaveHappened();
        }

        [Test]
        public async Task HandleDeleteButtonClicked_Test_MediaItem_Error()
        {
            // arrange
            var fakeBookService = A.Fake<IBookService>();
            var fakeMediaItemService = A.Fake<IMediaItemService>();
            var fakeTagService = A.Fake<ITagService>();
            var fakeAuthorService = A.Fake<IAuthorService>();
            var fakePublisherService = A.Fake<IPublisherService>();
            var fakeView = A.Fake<IMainWindow>();
            MediaItem item = new MediaItem
            {
                Id = 1,
                Title = "item"
            };
            A.CallTo(() => fakeView.SelectedItem).Returns(item);
            A.CallTo(() => fakeView.ShowDeleteConfirmationDialog("item")).Returns(true);
            A.CallTo(() => fakeView.CategoryDropDownSelectedIndex).Returns(1);
            A.CallTo(() => fakeMediaItemService.DeleteByIdAsync(fakeView.SelectedItemId)).Throws(new Exception("error"));
            MainWindowPresenter presenter = new MainWindowPresenter(fakeBookService, fakeMediaItemService, fakeTagService, fakeAuthorService, fakePublisherService, fakeView);

            // act
            await presenter.HandleDeleteButtonClicked(null, null);

            // assert
            A.CallTo(() => fakeMediaItemService.DeleteByIdAsync(fakeView.SelectedItemId)).MustHaveHappened();
            A.CallTo(() => fakeView.ShowErrorDialog("Error deleting item.", "error")).MustHaveHappened();
        }

        [Test]
        public async Task HandleItemSelectionChanged_Test_BookSelected()
        {
            // arrange
            var fakeBookService = A.Fake<IBookService>();
            Author author = new Author
            {
                FirstName = "John",
                LastName = "Smith"
            };
            Publisher publisher = new Publisher
            {
                Name = "some_publisher"
            };
            Book book = new Book
            {
                Id = 1,
                Title = "book",
                Publisher = publisher,
                Authors = new List<Author> { author}
            };
            Book book2 = new Book
            {
                Id = 2,
                Title = "book2",
                Publisher = publisher,
                Authors = new List<Author> { author }
            };
            A.CallTo(() => fakeBookService.GetByIdAsync(1)).Returns(book);
            A.CallTo(() => fakeBookService.GetAllAsync()).Returns(new List<Book> { book, book2 });
            DataTable displayedItems = new DataTable();
            displayedItems.Columns.Add(new DataColumn("Id", typeof(int)));
            displayedItems.Columns.Add(new DataColumn("Title", typeof(string)));
            displayedItems.Rows.Add(new object[] { 1, "book"});
            displayedItems.Rows.Add(new object[] { 2, "book2"});
            var fakeMediaItemService = A.Fake<IMediaItemService>();
            var fakeTagService = A.Fake<ITagService>();
            var fakeAuthorService = A.Fake<IAuthorService>();
            var fakePublisherService = A.Fake<IPublisherService>();
            var fakeView = A.Fake<IMainWindow>();
            A.CallTo(() => fakeView.SelectedItemId).Returns(1);
            A.CallTo(() => fakeView.CategoryDropDownSelectedIndex).Returns(0);
            A.CallTo(() => fakeView.NumberOfItemsSelected).Returns(1);
            A.CallTo(() => fakeView.DisplayedItems).Returns(displayedItems);
            MockItemPresenter presenter = new MockItemPresenter(fakeBookService, fakeMediaItemService, fakeTagService, fakeAuthorService, fakePublisherService, fakeView, displayedItems, null);

            // act
            await presenter.HandleItemSelectionChanged(null, null);

            // assert
            // selected item properties/details
            Assert.AreEqual(1, fakeView.SelectedItem.Id);
            Assert.AreEqual("book", fakeView.SelectedItem.Title);
            // buttons
            Assert.IsFalse(fakeView.ItemDetailsSpinner);
            Assert.IsTrue(fakeView.FilterGroupEnabled);
            Assert.IsTrue(fakeView.CategoryGroupEnabled);
            Assert.IsTrue(fakeView.AddItemEnabled);
            Assert.IsTrue(fakeView.SearchBooksButtonEnabled);
            Assert.IsTrue(fakeView.ViewStatisticsEnabled);
            Assert.IsTrue(fakeView.TagsButtonEnabled);
            Assert.IsTrue(fakeView.WishlistButtonEnabled);
            Assert.IsTrue(fakeView.DeleteItemButtonEnabled);
            Assert.IsFalse(fakeView.DiscardSelectedItemChangesButtonEnabled);
            // status bar
            Assert.AreEqual("Ready.", fakeView.StatusText);
            Assert.AreEqual("1 items selected. 2 of 2 items displayed.", fakeView.ItemsDisplayedText);
        }

        [Test]
        public async Task HandleItemSelectionChanged_Test_MediaItemSelected()
        {
            // arrange
            var fakeBookService = A.Fake<IBookService>();
            MediaItem item = new MediaItem
            {
                Id = 1,
                Title = "item"
            };
            MediaItem item2 = new MediaItem
            {
                Id = 2,
                Title = "item2"
            };
            var fakeMediaItemService = A.Fake<IMediaItemService>();
            A.CallTo(() => fakeMediaItemService.GetByIdAsync(1)).Returns(item);
            A.CallTo(() => fakeMediaItemService.GetAllAsync()).Returns(new List<MediaItem> { item, item2 });
            DataTable displayedItems = new DataTable();
            displayedItems.Columns.Add(new DataColumn("Id", typeof(int)));
            displayedItems.Columns.Add(new DataColumn("Title", typeof(string)));
            displayedItems.Rows.Add(new object[] { 1, "item" });
            displayedItems.Rows.Add(new object[] { 2, "item2" });
            var fakeTagService = A.Fake<ITagService>();
            var fakeAuthorService = A.Fake<IAuthorService>();
            var fakePublisherService = A.Fake<IPublisherService>();
            var fakeView = A.Fake<IMainWindow>();
            A.CallTo(() => fakeView.SelectedItemId).Returns(1);
            A.CallTo(() => fakeView.CategoryDropDownSelectedIndex).Returns(1);
            A.CallTo(() => fakeView.NumberOfItemsSelected).Returns(1);
            A.CallTo(() => fakeView.DisplayedItems).Returns(displayedItems);
            MockItemPresenter presenter = new MockItemPresenter(fakeBookService, fakeMediaItemService, fakeTagService, fakeAuthorService, fakePublisherService, fakeView, displayedItems, null);

            // act
            await presenter.HandleItemSelectionChanged(null, null);

            // assert
            // selected item properties/details
            Assert.AreEqual(1, fakeView.SelectedItem.Id);
            Assert.AreEqual("item", fakeView.SelectedItem.Title);
            // buttons
            Assert.IsFalse(fakeView.ItemDetailsSpinner);
            Assert.IsTrue(fakeView.FilterGroupEnabled);
            Assert.IsTrue(fakeView.CategoryGroupEnabled);
            Assert.IsTrue(fakeView.AddItemEnabled);
            Assert.IsTrue(fakeView.SearchBooksButtonEnabled);
            Assert.IsTrue(fakeView.ViewStatisticsEnabled);
            Assert.IsTrue(fakeView.TagsButtonEnabled);
            Assert.IsTrue(fakeView.WishlistButtonEnabled);
            Assert.IsTrue(fakeView.DeleteItemButtonEnabled);
            Assert.IsFalse(fakeView.DiscardSelectedItemChangesButtonEnabled);
            // status bar
            Assert.AreEqual("Ready.", fakeView.StatusText);
            Assert.AreEqual("1 items selected. 2 of 2 items displayed.", fakeView.ItemsDisplayedText);
        }

        [Test]
        public void DiscardSelectedItemChangesButtonClicked_Test()
        {
            // arrange
            MediaItem originalItem = new MediaItem
            {
                Id = 1,
                Title = "item",
                Notes = "not updated"
            };
            MediaItem updatedItem = new MediaItem
            {
                Id = 1,
                Title = "item",
                Notes = "updated"
            };
            var fakeView = A.Fake<IMainWindow>();
            A.CallTo(() => fakeView.SelectedItem).Returns(updatedItem);
            MockItemPresenter presenter = new MockItemPresenter(null, null, null, null, null, fakeView, null, originalItem.GetMemento());

            // act
            presenter.DiscardSelectedItemChangesButtonClicked(null, null);

            // assert
            Assert.IsFalse(fakeView.DiscardSelectedItemChangesButtonEnabled);
            Assert.AreEqual(1, fakeView.SelectedItem.Id);
            Assert.AreEqual("item", fakeView.SelectedItem.Title);
            Assert.AreEqual("not updated", fakeView.SelectedItem.Notes);
        }

        [Test]
        public async Task HandleUpdateSelectedItemButtonClicked_Test_UpdateBookError()
        {
            // arrange
            var fakeBookService = A.Fake<IBookService>();
            var fakeView = A.Fake<IMainWindow>();
            A.CallTo(() => fakeView.CategoryDropDownSelectedIndex).Returns(0);
            Book selected = new Book
            {
                Id = 1,
                Title = "test"
            };
            A.CallTo(() => fakeView.SelectedItem).Returns(selected);
            A.CallTo(() => fakeBookService.UpdateAsync(selected, true)).Throws(new Exception("error"));
            MainWindowPresenter presenter = new MainWindowPresenter(fakeBookService, null, null, null, null, fakeView);

            // act
            await presenter.HandleUpdateSelectedItemButtonClicked();

            // assert
            A.CallTo(() => fakeView.ShowErrorDialog("Error updating item.", "error")).MustHaveHappened();
        }
        
        [Test]
        public async Task HandleUpdateSelectedItemButtonClicked_Test_UpdateMediaItemError()
        {
            // arrange
            var fakeMediaItemService = A.Fake<IMediaItemService>();
            var fakeView = A.Fake<IMainWindow>();
            A.CallTo(() => fakeView.CategoryDropDownSelectedIndex).Returns(1);
            MediaItem selected = new MediaItem
            {
                Id = 1,
                Title = "test"
            };
            A.CallTo(() => fakeView.SelectedItem).Returns(selected);
            A.CallTo(() => fakeMediaItemService.UpdateAsync(selected, true)).Throws(new Exception("error"));
            MainWindowPresenter presenter = new MainWindowPresenter(null, fakeMediaItemService, null, null, null, fakeView);

            // act
            await presenter.HandleUpdateSelectedItemButtonClicked();

            // assert
            A.CallTo(() => fakeView.ShowErrorDialog("Error updating item.", "error")).MustHaveHappened();
        }
    }//class

    public class MockItemPresenter : MainWindowPresenter
    {
        public MockItemPresenter(IBookService bookRepository, IMediaItemService mediaItemService, ITagService tagService, IAuthorService authorService, IPublisherService publisherService,
            IMainWindow view,
            DataTable allItemsDt,
            ItemMemento selectedItemMemento)
            :base(bookRepository, mediaItemService, tagService, authorService, publisherService, view)
        {
            this._allItems = allItemsDt;
            this._selectedItemMemento = selectedItemMemento;
        }
    }//class
}