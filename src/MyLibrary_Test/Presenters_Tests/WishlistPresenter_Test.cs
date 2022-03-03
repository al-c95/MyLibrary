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
    public class WishlistPresenter_Test
    {
        [Test]
        public async Task LoadData_Test()
        {
            // arrange
            var fakeView = A.Fake<IWishlistForm>();
            WishlistItem item = new WishlistItem { Id = 1 };
            var fakeServiceFactory = A.Fake<IWishlistServiceProvider>();
            var fakeService = A.Fake<IWishlistService>();
            A.CallTo(() => fakeServiceFactory.Get()).Returns(fakeService);
            List<WishlistItem> items = new List<WishlistItem>
            {
                new WishlistItem{Id=1}
            };
            A.CallTo(() => fakeService.GetAll()).Returns(items);
            WishlistPresenter presenter = new WishlistPresenter(fakeView, fakeServiceFactory);

            // act
            await presenter.LoadData();

            // assert
            A.CallTo(() => fakeView.DisplayItems(items)).MustHaveHappened();
            Assert.AreEqual("Ready.", fakeView.StatusText);
        }

        [Test]
        public void ItemSelected_Test_Selected()
        {
            // arrange
            var fakeView = A.Fake<IWishlistForm>();
            WishlistItem selectedItem = new WishlistItem
            {
                Id = 1,
                Title = "item",
                Notes = "test",
                Type = ItemType.Book
            };
            A.CallTo(() => fakeView.SelectedItem).Returns(selectedItem);
            var fakeServiceFactory = A.Fake<IWishlistServiceProvider>();
            var fakeService = A.Fake<IWishlistService>();
            A.CallTo(() => fakeServiceFactory.Get()).Returns(fakeService);
            WishlistPresenter presenter = new WishlistPresenter(fakeView, fakeServiceFactory);

            // act
            presenter.ItemSelected(null, null);

            // assert
            Assert.AreEqual("test", fakeView.SelectedNotes);
            Assert.IsTrue(fakeView.SaveSelectedButtonEnabled);
            Assert.IsTrue(fakeView.DiscardChangesButtonEnabled);
            Assert.IsTrue(fakeView.DeleteSelectedButtonEnabled);
        }

        [Test]
        public void ItemSelected_Test_NoSelection()
        {
            // arrange
            var fakeView = A.Fake<IWishlistForm>();
            A.CallTo(() => fakeView.SelectedItem).Returns(null);
            var fakeServiceFactory = A.Fake<IWishlistServiceProvider>();
            var fakeService = A.Fake<IWishlistService>();
            A.CallTo(() => fakeServiceFactory.Get()).Returns(fakeService);
            WishlistPresenter presenter = new WishlistPresenter(fakeView, fakeServiceFactory);

            // act
            presenter.ItemSelected(null, null);

            // assert
            Assert.AreEqual("", fakeView.SelectedNotes);
            Assert.IsFalse(fakeView.SaveSelectedButtonEnabled);
            Assert.IsFalse(fakeView.DiscardChangesButtonEnabled);
            Assert.IsFalse(fakeView.DeleteSelectedButtonEnabled);
        }

        [Test]
        public void DiscardChangesClicked_Test()
        {
            // arrange
            var fakeView = A.Fake<IWishlistForm>();
            fakeView.SelectedNotes = "test test";
            WishlistItem selectedItem = new WishlistItem
            {
                Id = 1,
                Title = "item",
                Notes = "test",
                Type = ItemType.Book
            };
            A.CallTo(() => fakeView.SelectedItem).Returns(selectedItem);
            WishlistPresenter presenter = new WishlistPresenter(fakeView, null);

            // act
            presenter.DiscardChangesClicked(null, null);

            // assert
            Assert.AreEqual("test", fakeView.SelectedNotes);
        }

        [TestCase("", false)]
        [TestCase("test",true)]
        public void NewItemFieldsUpdated_Test(string title, bool expectedSaveNewButtonEnabled)
        {
            // arrange
            var fakeView = A.Fake<IWishlistForm>();
            A.CallTo(() => fakeView.NewItemTitle).Returns(title);
            WishlistPresenter presenter = new WishlistPresenter(fakeView, null);

            // act
            presenter.NewItemFieldsUpdated(null, null);
            bool actualSaveNewButtonEnabled = fakeView.SaveNewButtonEnabled;

            // assert
            Assert.AreEqual(expectedSaveNewButtonEnabled, actualSaveNewButtonEnabled);
        }

        [Test]
        public async Task SaveSelectedClicked_Test()
        {
            // arrange
            var fakeView = A.Fake<IWishlistForm>();
            WishlistItem modifiedItem = new WishlistItem
            {
                Id = 1,
                Title = "item",
                Notes = "test",
                Type = ItemType.Book
            };
            A.CallTo(() => fakeView.ModifiedItem).Returns(modifiedItem);
            var fakeServiceProvider = A.Fake<IWishlistServiceProvider>();
            var fakeService = A.Fake<IWishlistService>();
            List<WishlistItem> items = new List<WishlistItem>();
            A.CallTo(() => fakeService.GetAll()).Returns(items);
            A.CallTo(() => fakeServiceProvider.Get()).Returns(fakeService);
            WishlistPresenter presenter = new WishlistPresenter(fakeView, fakeServiceProvider);

            // act
            await presenter.SaveSelectedClicked(null, null);

            // assert
            A.CallTo(() => fakeService.Update(modifiedItem)).MustHaveHappened();
            A.CallTo(() => fakeView.DisplayItems(items)).MustHaveHappened();
            Assert.AreEqual("Ready.", fakeView.StatusText);
        }

        [Test]
        public async Task SaveNewClicked_Test_DoesNotYetExist()
        {
            // arrange
            var fakeView = A.Fake<IWishlistForm>();
            WishlistItem newItem = new WishlistItem
            {
                Id = 1,
                Title = "item",
                Notes = "test",
                Type = ItemType.Book
            };
            A.CallTo(() => fakeView.NewItem).Returns(newItem);
            var fakeServiceProvider = A.Fake<IWishlistServiceProvider>();
            var fakeService = A.Fake<IWishlistService>();
            List<WishlistItem> items = new List<WishlistItem>();
            A.CallTo(() => fakeService.GetAll()).Returns(items);
            A.CallTo(() => fakeServiceProvider.Get()).Returns(fakeService);
            WishlistPresenter presenter = new WishlistPresenter(fakeView, fakeServiceProvider);

            // act
            await presenter.SaveNewClicked(null, null);

            // assert
            A.CallTo(() => fakeService.Add(newItem)).MustHaveHappened();
            A.CallTo(() => fakeView.DisplayItems(items)).MustHaveHappened();
            Assert.AreEqual("Ready.", fakeView.StatusText);
            Assert.AreEqual("", fakeView.NewNotes);
            Assert.AreEqual("", fakeView.NewItemTitle);
        }

        [Test]
        public async Task SaveNewClicked_Test_AlreadyExists()
        {
            // arrange
            var fakeView = A.Fake<IWishlistForm>();
            WishlistItem newItem = new WishlistItem
            {
                Id = 1,
                Title = "item",
                Notes = "test",
                Type = ItemType.Book
            };
            A.CallTo(() => fakeView.NewItem).Returns(newItem);
            A.CallTo(() => fakeView.NewItemTitle).Returns("item");
            var fakeServiceProvider = A.Fake<IWishlistServiceProvider>();
            var fakeService = A.Fake<IWishlistService>();
            A.CallTo(() => fakeService.ExistsWithTitle("item")).Returns(true);
            List<WishlistItem> items = new List<WishlistItem>();
            A.CallTo(() => fakeService.GetAll()).Returns(items);
            A.CallTo(() => fakeServiceProvider.Get()).Returns(fakeService);
            WishlistPresenter presenter = new WishlistPresenter(fakeView, fakeServiceProvider);

            // act
            await presenter.SaveNewClicked(null, null);

            // assert
            A.CallTo(() => fakeView.ShowItemAlreadyExistsDialog("item")).MustHaveHappened();
            Assert.AreEqual("Ready.", fakeView.StatusText);
        }

        [Test]
        public async Task DeleteClicked_Test() 
        {
            // arrange
            var fakeView = A.Fake<IWishlistForm>();
            WishlistItem selectedItem = new WishlistItem
            {
                Id = 1,
                Title = "item",
                Notes = "test",
                Type = ItemType.Book
            };
            A.CallTo(() => fakeView.SelectedItem).Returns(selectedItem);
            var fakeServiceProvider = A.Fake<IWishlistServiceProvider>();
            var fakeService = A.Fake<IWishlistService>();
            List<WishlistItem> items = new List<WishlistItem>();
            A.CallTo(() => fakeService.GetAll()).Returns(items);
            A.CallTo(() => fakeServiceProvider.Get()).Returns(fakeService);
            WishlistPresenter presenter = new WishlistPresenter(fakeView, fakeServiceProvider);

            // act
            await presenter.DeleteClicked(null, null);

            // assert
            A.CallTo(() => fakeService.DeleteById(1)).MustHaveHappened();
            A.CallTo(() => fakeView.DisplayItems(items)).MustHaveHappened();
            Assert.AreEqual("Ready.", fakeView.StatusText);
        }
    }//class
}
