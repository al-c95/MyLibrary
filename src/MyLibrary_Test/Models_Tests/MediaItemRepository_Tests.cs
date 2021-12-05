using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using NUnit;
using NUnit.Framework;
using FakeItEasy;
using MyLibrary.BusinessLogic;
using MyLibrary.Models.Entities;
using MyLibrary.DataAccessLayer;

namespace MyLibrary_Test.Models_Tests
{
    [TestFixture]
    public class MediaItemRepository_Tests
    {
        private MediaItemRepository _repo;

        public MediaItemRepository_Tests()
        {
            // arrange
            var fakeDao = A.Fake<ItemDataAccessor<MediaItem>>();
            MediaItem item1 = new MediaItem();
            item1.Id = 1;
            item1.Title = "Item 1";
            item1.Type = ItemType.Cd;
            MediaItem item2 = new MediaItem();
            item2.Id = 2;
            item2.Title = "Item 2";
            item2.Type = ItemType.Dvd;
            List<MediaItem> items = new List<MediaItem>
            {
                item1,
                item2
            };
            A.CallTo(() => fakeDao.ReadAll())
                .Returns(items);
            this._repo = new MediaItemRepository(fakeDao);
        }

        [Test]
        public async Task GetAll_Test()
        {
            // act
            var result = await _repo.GetAll();

            // assert
            Assert.IsTrue(result.ToList().Count == 2);
            Assert.IsTrue(result.ToList()[0].Id == 1);
            Assert.IsTrue(result.ToList()[1].Id == 2);
        }

        [Test]
        public async Task GetById_Test_Exists()
        {
            // act
            var result = await _repo.GetById(2);

            // assert
            Assert.IsTrue(result.Title == "Item 2");
        }

        [Test]
        public async Task GetById_Test_DoesNotExist()
        {
            // act
            var result = await _repo.GetById(3);

            // assert
            Assert.IsNull(result);
        }

        [TestCase(1, true)]
        [TestCase(3, false)]
        public async Task ItemWithIdExists_Test(int id, bool expectedResult)
        {
            // act
            var actualResult = await _repo.ItemWithIdExists(id);

            // assert
            Assert.AreEqual(expectedResult, actualResult);
        }

        [Test]
        public async Task GetByType_Tests()
        {
            // act
            var result = await _repo.GetByType(ItemType.Dvd);

            // assert
            Assert.AreEqual(ItemType.Dvd, result.ToList()[0].Type);
        }
    }
}
