using System.Collections.Generic;
using System.Threading.Tasks;
using System.Data;
using NUnit.Framework;
using FakeItEasy;
using MyLibrary.Models.BusinessLogic;
using MyLibrary.Models.Entities;
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
            fakeView.BookFilterSelected = true;
            fakeView.DvdFilterSelected = true;
            fakeView.CdFilterSelected = true;
            fakeView.BlurayFilterSelected = true;
            fakeView.VinylFilterSelected = true;
            fakeView.VhsFilterSelected = true;
            fakeView.FlashDriveFilterSelected = true;
            fakeView.FloppyDiskFilterSelected = true;
            fakeView.OtherFilterSelected = true;
            var fakeServiceFactory = A.Fake<IWishlistServiceProvider>();
            var fakeService = A.Fake<IWishlistService>();
            A.CallTo(() => fakeServiceFactory.Get()).Returns(fakeService);
            List<WishlistItem> items = GetCollectionOfAllTypesOfWishlistItems();
            A.CallTo(() => fakeService.GetAll()).Returns(items);
            WishlistPresenter presenter = new WishlistPresenter(fakeView, fakeServiceFactory);

            // act
            await presenter.LoadData();
            var displayedTable = fakeView.DisplayedItems;

            // assert
            Assert.AreEqual(9, displayedTable.Rows.Count);
            Assert.IsTrue(displayedTable.Rows[0].Field<string>("Id").Equals("1"));
            Assert.IsTrue(displayedTable.Rows[0].Field<string>("Title").Equals("Test Book"));
            Assert.IsTrue(displayedTable.Rows[0].Field<string>("Notes").Equals("test"));
            Assert.IsTrue(displayedTable.Rows[0].Field<string>("Type").Equals("Book"));
            Assert.IsTrue(displayedTable.Rows[1].Field<string>("Id").Equals("2"));
            Assert.IsTrue(displayedTable.Rows[1].Field<string>("Title").Equals("Test Dvd"));
            Assert.IsTrue(displayedTable.Rows[1].Field<string>("Notes").Equals("test"));
            Assert.IsTrue(displayedTable.Rows[1].Field<string>("Type").Equals("Dvd"));
            Assert.IsTrue(displayedTable.Rows[2].Field<string>("Id").Equals("3"));
            Assert.IsTrue(displayedTable.Rows[2].Field<string>("Title").Equals("Test Cd"));
            Assert.IsTrue(displayedTable.Rows[2].Field<string>("Notes").Equals("test"));
            Assert.IsTrue(displayedTable.Rows[2].Field<string>("Type").Equals("Cd"));
            Assert.IsTrue(displayedTable.Rows[3].Field<string>("Id").Equals("4"));
            Assert.IsTrue(displayedTable.Rows[3].Field<string>("Title").Equals("Test BluRay"));
            Assert.IsTrue(displayedTable.Rows[3].Field<string>("Notes").Equals("test"));
            Assert.IsTrue(displayedTable.Rows[3].Field<string>("Type").Equals("BluRay"));
            Assert.IsTrue(displayedTable.Rows[4].Field<string>("Id").Equals("5"));
            Assert.IsTrue(displayedTable.Rows[4].Field<string>("Title").Equals("Test Vhs"));
            Assert.IsTrue(displayedTable.Rows[4].Field<string>("Notes").Equals("test"));
            Assert.IsTrue(displayedTable.Rows[4].Field<string>("Type").Equals("Vhs"));
            Assert.IsTrue(displayedTable.Rows[5].Field<string>("Id").Equals("6"));
            Assert.IsTrue(displayedTable.Rows[5].Field<string>("Title").Equals("Test Vinyl"));
            Assert.IsTrue(displayedTable.Rows[5].Field<string>("Notes").Equals("test"));
            Assert.IsTrue(displayedTable.Rows[5].Field<string>("Type").Equals("Vinyl"));
            Assert.IsTrue(displayedTable.Rows[6].Field<string>("Id").Equals("7"));
            Assert.IsTrue(displayedTable.Rows[6].Field<string>("Title").Equals("Test Flash Drive"));
            Assert.IsTrue(displayedTable.Rows[6].Field<string>("Notes").Equals("test"));
            Assert.IsTrue(displayedTable.Rows[6].Field<string>("Type").Equals("Flash Drive"));
            Assert.IsTrue(displayedTable.Rows[7].Field<string>("Id").Equals("8"));
            Assert.IsTrue(displayedTable.Rows[7].Field<string>("Title").Equals("Test Floppy Disk"));
            Assert.IsTrue(displayedTable.Rows[7].Field<string>("Notes").Equals("test"));
            Assert.IsTrue(displayedTable.Rows[7].Field<string>("Type").Equals("Floppy Disk"));
            Assert.IsTrue(displayedTable.Rows[8].Field<string>("Id").Equals("9"));
            Assert.IsTrue(displayedTable.Rows[8].Field<string>("Title").Equals("Test Other"));
            Assert.IsTrue(displayedTable.Rows[8].Field<string>("Notes").Equals("test"));
            Assert.IsTrue(displayedTable.Rows[8].Field<string>("Type").Equals("Other"));
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
            Assert.IsTrue(fakeView.AddToLibraryButtonEnabled);
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
            Assert.IsFalse(fakeView.AddToLibraryButtonEnabled);
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
        [TestCase("test", true)]
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
            A.CallTo(() => fakeService.Update(modifiedItem, false)).MustHaveHappened();
            Assert.AreEqual("Ready.", fakeView.StatusText);
        }

        [Test]
        public async Task SaveNewClicked_Test_DoesNotYetExist()
        {
            // arrange
            var fakeView = A.Fake<IWishlistForm>();
            fakeView.BookFilterSelected = true;
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
            var displayedTable = fakeView.DisplayedItems;

            // assert
            A.CallTo(() => fakeService.Add(newItem)).MustHaveHappened();
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
            Assert.AreEqual("Ready.", fakeView.StatusText);
        }

        [Test]
        public async Task ApplyFilters_Test_TitleFilter()
        {
            // arrange
            var fakeView = A.Fake<IWishlistForm>();
            fakeView.BookFilterSelected = true;
            fakeView.DvdFilterSelected = true;
            fakeView.TitleFilterText = "Dvd";
            var fakeServiceFactory = A.Fake<IWishlistServiceProvider>();
            var fakeService = A.Fake<IWishlistService>();
            List<WishlistItem> items = new List<WishlistItem>
            {
                new WishlistItem
                {
                    Id=1,
                    Title="Test Book",
                    Notes="test",
                    Type=ItemType.Book
                },
                new WishlistItem
                {
                    Id=2,
                    Title="Test Dvd",
                    Notes="test",
                    Type=ItemType.Dvd
                }
            };
            A.CallTo(() => fakeService.GetAll()).Returns(items);
            A.CallTo(() => fakeServiceFactory.Get()).Returns(fakeService);
            WishlistPresenter presenter = new WishlistPresenter(fakeView, fakeServiceFactory);

            // act
            await presenter.LoadData();
            presenter.ApplyFilters(null, null);
            var displayedTable = fakeView.DisplayedItems;

            // assert
            Assert.AreEqual(1, displayedTable.Rows.Count);
            Assert.IsTrue(displayedTable.Rows[0].Field<string>("Id").Equals("2"));
            Assert.IsTrue(displayedTable.Rows[0].Field<string>("Title").Equals("Test Dvd"));
            Assert.IsTrue(displayedTable.Rows[0].Field<string>("Type").Equals("Dvd"));
            Assert.IsTrue(displayedTable.Rows[0].Field<string>("Notes").Equals("test"));
            Assert.AreEqual("Ready.", fakeView.StatusText);
        }

        [Test]
        public async Task ApplyFilters_Test_NoTitleFilter_BookFilter()
        {
            // arrange
            var fakeView = A.Fake<IWishlistForm>();
            fakeView.BookFilterSelected = true;
            var fakeServiceFactory = A.Fake<IWishlistServiceProvider>();
            var fakeService = A.Fake<IWishlistService>();
            List<WishlistItem> items = GetCollectionOfAllTypesOfWishlistItems();
            A.CallTo(() => fakeService.GetAll()).Returns(items);
            A.CallTo(() => fakeServiceFactory.Get()).Returns(fakeService);
            WishlistPresenter presenter = new WishlistPresenter(fakeView, fakeServiceFactory);

            // act
            await presenter.LoadData();
            presenter.ApplyFilters(null, null);
            var displayedTable = fakeView.DisplayedItems;

            // assert
            Assert.AreEqual(1, displayedTable.Rows.Count);
            Assert.IsTrue(displayedTable.Rows[0].Field<string>("Id").Equals("1"));
            Assert.IsTrue(displayedTable.Rows[0].Field<string>("Title").Equals("Test Book"));
            Assert.IsTrue(displayedTable.Rows[0].Field<string>("Type").Equals("Book"));
            Assert.IsTrue(displayedTable.Rows[0].Field<string>("Notes").Equals("test"));
            Assert.AreEqual("Ready.", fakeView.StatusText);
        }

        [Test]
        public async Task ApplyFilters_Test_NoTitleFilter_DvdFilter()
        {
            // arrange
            var fakeView = A.Fake<IWishlistForm>();
            fakeView.DvdFilterSelected = true;
            var fakeServiceFactory = A.Fake<IWishlistServiceProvider>();
            var fakeService = A.Fake<IWishlistService>();
            List<WishlistItem> items = GetCollectionOfAllTypesOfWishlistItems();
            A.CallTo(() => fakeService.GetAll()).Returns(items);
            A.CallTo(() => fakeServiceFactory.Get()).Returns(fakeService);
            WishlistPresenter presenter = new WishlistPresenter(fakeView, fakeServiceFactory);

            // act
            await presenter.LoadData();
            presenter.ApplyFilters(null, null);
            var displayedTable = fakeView.DisplayedItems;

            // assert
            Assert.AreEqual(1, displayedTable.Rows.Count);
            Assert.IsTrue(displayedTable.Rows[0].Field<string>("Id").Equals("2"));
            Assert.IsTrue(displayedTable.Rows[0].Field<string>("Title").Equals("Test Dvd"));
            Assert.IsTrue(displayedTable.Rows[0].Field<string>("Type").Equals("Dvd"));
            Assert.IsTrue(displayedTable.Rows[0].Field<string>("Notes").Equals("test"));
            Assert.AreEqual("Ready.", fakeView.StatusText);
        }

        [Test]
        public async Task ApplyFilters_Test_NoTitleFilter_CdFilter()
        {
            // arrange
            var fakeView = A.Fake<IWishlistForm>();
            fakeView.CdFilterSelected = true;
            var fakeServiceFactory = A.Fake<IWishlistServiceProvider>();
            var fakeService = A.Fake<IWishlistService>();
            List<WishlistItem> items = GetCollectionOfAllTypesOfWishlistItems();
            A.CallTo(() => fakeService.GetAll()).Returns(items);
            A.CallTo(() => fakeServiceFactory.Get()).Returns(fakeService);
            WishlistPresenter presenter = new WishlistPresenter(fakeView, fakeServiceFactory);

            // act
            await presenter.LoadData();
            presenter.ApplyFilters(null, null);
            var displayedTable = fakeView.DisplayedItems;

            // assert
            Assert.AreEqual(1, displayedTable.Rows.Count);
            Assert.IsTrue(displayedTable.Rows[0].Field<string>("Id").Equals("3"));
            Assert.IsTrue(displayedTable.Rows[0].Field<string>("Title").Equals("Test Cd"));
            Assert.IsTrue(displayedTable.Rows[0].Field<string>("Type").Equals("Cd"));
            Assert.IsTrue(displayedTable.Rows[0].Field<string>("Notes").Equals("test"));
            Assert.AreEqual("Ready.", fakeView.StatusText);
        }

        [Test]
        public async Task ApplyFilters_Test_NoTitleFilter_BluRayFilter()
        {
            // arrange
            var fakeView = A.Fake<IWishlistForm>();
            fakeView.BlurayFilterSelected = true;
            var fakeServiceFactory = A.Fake<IWishlistServiceProvider>();
            var fakeService = A.Fake<IWishlistService>();
            List<WishlistItem> items = GetCollectionOfAllTypesOfWishlistItems();
            A.CallTo(() => fakeService.GetAll()).Returns(items);
            A.CallTo(() => fakeServiceFactory.Get()).Returns(fakeService);
            WishlistPresenter presenter = new WishlistPresenter(fakeView, fakeServiceFactory);

            // act
            await presenter.LoadData();
            presenter.ApplyFilters(null, null);
            var displayedTable = fakeView.DisplayedItems;

            // assert
            Assert.AreEqual(1, displayedTable.Rows.Count);
            Assert.IsTrue(displayedTable.Rows[0].Field<string>("Id").Equals("4"));
            Assert.IsTrue(displayedTable.Rows[0].Field<string>("Title").Equals("Test BluRay"));
            Assert.IsTrue(displayedTable.Rows[0].Field<string>("Type").Equals("BluRay"));
            Assert.IsTrue(displayedTable.Rows[0].Field<string>("Notes").Equals("test"));
            Assert.AreEqual("Ready.", fakeView.StatusText);
        }

        [Test]
        public async Task ApplyFilters_Test_NoTitleFilter_VhsFilter()
        {
            // arrange
            var fakeView = A.Fake<IWishlistForm>();
            fakeView.VhsFilterSelected = true;
            var fakeServiceFactory = A.Fake<IWishlistServiceProvider>();
            var fakeService = A.Fake<IWishlistService>();
            List<WishlistItem> items = GetCollectionOfAllTypesOfWishlistItems();
            A.CallTo(() => fakeService.GetAll()).Returns(items);
            A.CallTo(() => fakeServiceFactory.Get()).Returns(fakeService);
            WishlistPresenter presenter = new WishlistPresenter(fakeView, fakeServiceFactory);

            // act
            await presenter.LoadData();
            presenter.ApplyFilters(null, null);
            var displayedTable = fakeView.DisplayedItems;

            // assert
            Assert.AreEqual(1, displayedTable.Rows.Count);
            Assert.IsTrue(displayedTable.Rows[0].Field<string>("Id").Equals("5"));
            Assert.IsTrue(displayedTable.Rows[0].Field<string>("Title").Equals("Test Vhs"));
            Assert.IsTrue(displayedTable.Rows[0].Field<string>("Type").Equals("Vhs"));
            Assert.IsTrue(displayedTable.Rows[0].Field<string>("Notes").Equals("test"));
            Assert.AreEqual("Ready.", fakeView.StatusText);
        }

        [Test]
        public async Task ApplyFilters_Test_NoTitleFilter_VinylFilter()
        {
            // arrange
            var fakeView = A.Fake<IWishlistForm>();
            fakeView.VinylFilterSelected = true;
            var fakeServiceFactory = A.Fake<IWishlistServiceProvider>();
            var fakeService = A.Fake<IWishlistService>();
            List<WishlistItem> items = GetCollectionOfAllTypesOfWishlistItems();
            A.CallTo(() => fakeService.GetAll()).Returns(items);
            A.CallTo(() => fakeServiceFactory.Get()).Returns(fakeService);
            WishlistPresenter presenter = new WishlistPresenter(fakeView, fakeServiceFactory);

            // act
            await presenter.LoadData();
            presenter.ApplyFilters(null, null);
            var displayedTable = fakeView.DisplayedItems;

            // assert
            Assert.AreEqual(1, displayedTable.Rows.Count);
            Assert.IsTrue(displayedTable.Rows[0].Field<string>("Id").Equals("6"));
            Assert.IsTrue(displayedTable.Rows[0].Field<string>("Title").Equals("Test Vinyl"));
            Assert.IsTrue(displayedTable.Rows[0].Field<string>("Type").Equals("Vinyl"));
            Assert.IsTrue(displayedTable.Rows[0].Field<string>("Notes").Equals("test"));
            Assert.AreEqual("Ready.", fakeView.StatusText);
        }

        [Test]
        public async Task ApplyFilters_Test_NoTitleFilter_FlashDriveFilter()
        {
            // arrange
            var fakeView = A.Fake<IWishlistForm>();
            fakeView.FlashDriveFilterSelected = true;
            var fakeServiceFactory = A.Fake<IWishlistServiceProvider>();
            var fakeService = A.Fake<IWishlistService>();
            List<WishlistItem> items = GetCollectionOfAllTypesOfWishlistItems();
            A.CallTo(() => fakeService.GetAll()).Returns(items);
            A.CallTo(() => fakeServiceFactory.Get()).Returns(fakeService);
            WishlistPresenter presenter = new WishlistPresenter(fakeView, fakeServiceFactory);

            // act
            await presenter.LoadData();
            presenter.ApplyFilters(null, null);
            var displayedTable = fakeView.DisplayedItems;

            // assert
            Assert.AreEqual(1, displayedTable.Rows.Count);
            Assert.IsTrue(displayedTable.Rows[0].Field<string>("Id").Equals("7"));
            Assert.IsTrue(displayedTable.Rows[0].Field<string>("Title").Equals("Test Flash Drive"));
            Assert.IsTrue(displayedTable.Rows[0].Field<string>("Type").Equals("Flash Drive"));
            Assert.IsTrue(displayedTable.Rows[0].Field<string>("Notes").Equals("test"));
            Assert.AreEqual("Ready.", fakeView.StatusText);
        }

        [Test]
        public async Task ApplyFilters_Test_NoTitleFilter_FloppyDiskFilter()
        {
            // arrange
            var fakeView = A.Fake<IWishlistForm>();
            fakeView.FloppyDiskFilterSelected = true;
            var fakeServiceFactory = A.Fake<IWishlistServiceProvider>();
            var fakeService = A.Fake<IWishlistService>();
            List<WishlistItem> items = GetCollectionOfAllTypesOfWishlistItems();
            A.CallTo(() => fakeService.GetAll()).Returns(items);
            A.CallTo(() => fakeServiceFactory.Get()).Returns(fakeService);
            WishlistPresenter presenter = new WishlistPresenter(fakeView, fakeServiceFactory);

            // act
            await presenter.LoadData();
            presenter.ApplyFilters(null, null);
            var displayedTable = fakeView.DisplayedItems;

            // assert
            Assert.AreEqual(1, displayedTable.Rows.Count);
            Assert.IsTrue(displayedTable.Rows[0].Field<string>("Id").Equals("8"));
            Assert.IsTrue(displayedTable.Rows[0].Field<string>("Title").Equals("Test Floppy Disk"));
            Assert.IsTrue(displayedTable.Rows[0].Field<string>("Type").Equals("Floppy Disk"));
            Assert.IsTrue(displayedTable.Rows[0].Field<string>("Notes").Equals("test"));
            Assert.AreEqual("Ready.", fakeView.StatusText);
        }

        [Test]
        public async Task ApplyFilters_Test_NoTitleFilter_OtherFilter()
        {
            // arrange
            var fakeView = A.Fake<IWishlistForm>();
            fakeView.OtherFilterSelected = true;
            var fakeServiceFactory = A.Fake<IWishlistServiceProvider>();
            var fakeService = A.Fake<IWishlistService>();
            List<WishlistItem> items = GetCollectionOfAllTypesOfWishlistItems();
            A.CallTo(() => fakeService.GetAll()).Returns(items);
            A.CallTo(() => fakeServiceFactory.Get()).Returns(fakeService);
            WishlistPresenter presenter = new WishlistPresenter(fakeView, fakeServiceFactory);

            // act
            await presenter.LoadData();
            presenter.ApplyFilters(null, null);
            var displayedTable = fakeView.DisplayedItems;

            // assert
            Assert.AreEqual(1, displayedTable.Rows.Count);
            Assert.IsTrue(displayedTable.Rows[0].Field<string>("Id").Equals("9"));
            Assert.IsTrue(displayedTable.Rows[0].Field<string>("Title").Equals("Test Other"));
            Assert.IsTrue(displayedTable.Rows[0].Field<string>("Type").Equals("Other"));
            Assert.IsTrue(displayedTable.Rows[0].Field<string>("Notes").Equals("test"));
            Assert.AreEqual("Ready.", fakeView.StatusText);
        }

        private List<WishlistItem> GetCollectionOfAllTypesOfWishlistItems()
        {
            return new List<WishlistItem>
            {
                new WishlistItem
                {
                    Id=1,
                    Title="Test Book",
                    Notes="test",
                    Type=ItemType.Book
                },
                new WishlistItem
                {
                    Id=2,
                    Title="Test Dvd",
                    Notes="test",
                    Type=ItemType.Dvd
                },
                new WishlistItem
                {
                    Id=3,
                    Title="Test Cd",
                    Notes="test",
                    Type=ItemType.Cd
                },
                new WishlistItem
                {
                    Id=4,
                    Title="Test BluRay",
                    Notes="test",
                    Type=ItemType.BluRay
                },
                new WishlistItem
                {
                    Id=5,
                    Title="Test Vhs",
                    Notes="test",
                    Type=ItemType.Vhs
                },
                new WishlistItem
                {
                    Id=6,
                    Title="Test Vinyl",
                    Notes="test",
                    Type=ItemType.Vinyl
                },
                new WishlistItem
                {
                    Id=7,
                    Title="Test Flash Drive",
                    Notes="test",
                    Type=ItemType.FlashDrive
                },
                new WishlistItem
                {
                    Id=8,
                    Title="Test Floppy Disk",
                    Notes="test",
                    Type=ItemType.FloppyDisk
                },
                new WishlistItem
                {
                    Id=9,
                    Title="Test Other",
                    Notes="test",
                    Type=ItemType.Other
                }
            };
        }
    }//class
}