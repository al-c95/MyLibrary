using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using NUnit.Framework;
using FakeItEasy;
using MyLibrary.Models.Entities;
using MyLibrary;
using MyLibrary.DataAccessLayer;
using MyLibrary.DataAccessLayer.Repositories;
using MyLibrary.DataAccessLayer.ServiceProviders;

namespace MyLibrary_Test
{
    [TestFixture]
    public class WishlistService_Tests
    {
        [Test]
        public async Task Add_Test()
        {
            // arrange
            var fakeUowProvider = A.Fake<IUnitOfWorkProvider>();
            var fakeUow = A.Fake<IUnitOfWork>();
            A.CallTo(() => fakeUowProvider.Get()).Returns(fakeUow);
            var fakeRepoProvider = A.Fake<IWishlistRepositoryProvider>();
            var fakeRepo = A.Fake<IWishlistRepository>();
            A.CallTo(() => fakeRepoProvider.Get(fakeUow)).Returns(fakeRepo);
            WishlistService service = new WishlistService(fakeUowProvider, fakeRepoProvider);
            WishlistItem item = new WishlistItem
            {
                Id = 1,
                Title = "test_item",
                Type = ItemType.Book
            };

            // act
            await service.Add(item);

            // assert
            A.CallTo(() => fakeRepo.CreateAsync(item)).MustHaveHappened();
            A.CallTo(() => fakeUow.Dispose()).MustHaveHappened();
        }

        [Test]
        public async Task GetAll_Test()
        {
            // arrange
            var fakeUowProvider = A.Fake<IUnitOfWorkProvider>();
            var fakeUow = A.Fake<IUnitOfWork>();
            A.CallTo(() => fakeUowProvider.Get()).Returns(fakeUow);
            var fakeRepoProvider = A.Fake<IWishlistRepositoryProvider>();
            var fakeRepo = A.Fake<IWishlistRepository>();
            A.CallTo(() => fakeRepoProvider.Get(fakeUow)).Returns(fakeRepo);
            WishlistService service = new WishlistService(fakeUowProvider, fakeRepoProvider);
            List<WishlistItem> items = new List<WishlistItem>
            {
                new WishlistItem
                {
                    Id = 1,
                    Title = "test_item",
                    Type = ItemType.Book
                }
            };
            A.CallTo(() => fakeRepo.ReadAllAsync()).Returns(items);

            // act
            var result = await service.GetAll();

            // assert
            Assert.IsTrue(result.ToList().Count == 1);
            Assert.IsTrue(result.ToList()[0].Id == 1);
            Assert.IsTrue(result.ToList()[0].Title.Equals("test_item"));
            A.CallTo(() => fakeUow.Dispose()).MustHaveHappened();
        }

        [Test]
        public async Task GetByType_Test()
        {
            // arrange
            var fakeUowProvider = A.Fake<IUnitOfWorkProvider>();
            var fakeUow = A.Fake<IUnitOfWork>();
            A.CallTo(() => fakeUowProvider.Get()).Returns(fakeUow);
            var fakeRepoProvider = A.Fake<IWishlistRepositoryProvider>();
            var fakeRepo = A.Fake<IWishlistRepository>();
            A.CallTo(() => fakeRepoProvider.Get(fakeUow)).Returns(fakeRepo);
            WishlistService service = new WishlistService(fakeUowProvider, fakeRepoProvider);
            List<WishlistItem> items = new List<WishlistItem>
            {
                new WishlistItem
                {
                    Id = 1,
                    Title = "test_item1",
                    Type = ItemType.Book
                },
                new WishlistItem
                {
                    Id = 2,
                    Title = "test_item2",
                    Type = ItemType.Dvd
                }
            };
            A.CallTo(() => fakeRepo.ReadAllAsync()).Returns(items);

            // act
            var result = await service.GetByType(ItemType.Book);

            // assert
            Assert.IsTrue(result.ToList().Count == 1);
            Assert.IsTrue(result.ToList()[0].Id == 1);
            Assert.IsTrue(result.ToList()[0].Title.Equals("test_item1"));
            A.CallTo(() => fakeUow.Dispose()).MustHaveHappened();
        }

        [TestCase("test_item1", true)]
        [TestCase("bogus", false)]
        public async Task ExistsWithTitle_Test(string title, bool expectedResult)
        {
            // arrange
            var fakeUowProvider = A.Fake<IUnitOfWorkProvider>();
            var fakeUow = A.Fake<IUnitOfWork>();
            A.CallTo(() => fakeUowProvider.Get()).Returns(fakeUow);
            var fakeRepoProvider = A.Fake<IWishlistRepositoryProvider>();
            var fakeRepo = A.Fake<IWishlistRepository>();
            A.CallTo(() => fakeRepoProvider.Get(fakeUow)).Returns(fakeRepo);
            WishlistService service = new WishlistService(fakeUowProvider, fakeRepoProvider);
            A.CallTo(() => fakeRepo.ExistsWithTitleAsync("test_item1")).Returns(true);
            A.CallTo(() => fakeRepo.ExistsWithTitleAsync("bogus")).Returns(false);

            // act
            var actualResult = await service.ExistsWithTitle(title);

            // assert
            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestCase(1, true)]
        [TestCase(3, false)]
        public async Task ExistsWithId_Test(int id, bool expectedResult)
        {
            // arrange
            var fakeUowProvider = A.Fake<IUnitOfWorkProvider>();
            var fakeUow = A.Fake<IUnitOfWork>();
            A.CallTo(() => fakeUowProvider.Get()).Returns(fakeUow);
            var fakeRepoProvider = A.Fake<IWishlistRepositoryProvider>();
            var fakeRepo = A.Fake<IWishlistRepository>();
            A.CallTo(() => fakeRepoProvider.Get(fakeUow)).Returns(fakeRepo);
            WishlistService service = new WishlistService(fakeUowProvider, fakeRepoProvider);
            List<WishlistItem> items = new List<WishlistItem>
            {
                new WishlistItem
                {
                    Id = 1,
                    Title = "test_item1",
                    Type = ItemType.Book
                },
                new WishlistItem
                {
                    Id = 2,
                    Title = "test_item2",
                    Type = ItemType.Dvd
                }
            };
            A.CallTo(() => fakeRepo.ReadAllAsync()).Returns(items);

            // act
            var actualResult = await service.ExistsWithId(id);

            // assert
            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestCase(true)]
        [TestCase(false)]
        public async Task Update_Test(bool updateImage)
        {
            // arrange
            var fakeUowProvider = A.Fake<IUnitOfWorkProvider>();
            var fakeUow = A.Fake<IUnitOfWork>();
            A.CallTo(() => fakeUowProvider.Get()).Returns(fakeUow);
            var fakeRepoProvider = A.Fake<IWishlistRepositoryProvider>();
            var fakeRepo = A.Fake<IWishlistRepository>();
            A.CallTo(() => fakeRepoProvider.Get(fakeUow)).Returns(fakeRepo);
            WishlistService service = new WishlistService(fakeUowProvider, fakeRepoProvider);
            WishlistItem item = new WishlistItem
            {
                Id = 1,
                Title = "test_item",
                Type = ItemType.Book
            };

            // act
            await service.Update(item, updateImage);

            // assert
            A.CallTo(() => fakeRepo.UpdateAsync(item, updateImage)).MustHaveHappened();
            A.CallTo(() => fakeUow.Dispose()).MustHaveHappened();
        }

        [Test]
        public async Task DeleteById_Test()
        {
            // arrange
            var fakeUowProvider = A.Fake<IUnitOfWorkProvider>();
            var fakeUow = A.Fake<IUnitOfWork>();
            A.CallTo(() => fakeUowProvider.Get()).Returns(fakeUow);
            var fakeRepoProvider = A.Fake<IWishlistRepositoryProvider>();
            var fakeRepo = A.Fake<IWishlistRepository>();
            A.CallTo(() => fakeRepoProvider.Get(fakeUow)).Returns(fakeRepo);
            WishlistService service = new WishlistService(fakeUowProvider, fakeRepoProvider);

            // act
            await service.DeleteById(1);

            // assert
            A.CallTo(() => fakeRepo.DeleteByIdAsync(1)).MustHaveHappened();
            A.CallTo(() => fakeUow.Dispose()).MustHaveHappened();
        }
    }//class
}
