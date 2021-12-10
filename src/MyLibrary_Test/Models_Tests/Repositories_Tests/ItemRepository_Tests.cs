using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using NUnit;
using NUnit.Framework;
using FakeItEasy;
using MyLibrary.BusinessLogic.Repositories;
using MyLibrary.Models.Entities;
using MyLibrary.DataAccessLayer;

namespace MyLibrary_Test.Models_Tests.Repositories_Tests
{
    [TestFixture]
    class ItemRepository_Tests
    {
        // class under test
        private ItemRepository<MockItem> _repo;

        // ctor
        public ItemRepository_Tests()
        {
            // arrange
            var fakeDao = A.Fake<ItemDataAccessor<MockItem>>();
            A.CallTo(() => fakeDao.ReadAll()).Returns(new List<MockItem>
            {
                new MockItem
                {
                    Id = 1,
                    Title = "item1"
                },
                new MockItem
                {
                    Id = 2,
                    Title = "item2"
                }
            });
            this._repo = new MockItemRepository(fakeDao);
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
            Assert.IsTrue(result.Title == "item2");
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
        public async Task ExistsWithId_Test(int id, bool expectedResult)
        {
            // act
            var actualResult = await _repo.ExistsWithId(id);

            // assert
            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestCase("item1", true)]
        [TestCase("item3", false)]
        public async Task ExistsWithTitle_Test(string title, bool expectedResult)
        {
            // act
            var actualResult = await _repo.ExistsWithTitle(title);

            // assert
            Assert.AreEqual(expectedResult, actualResult);
        }
    }//class

    public class MockItem : Item
    {

    }//class

    public class MockItemRepository : ItemRepository<MockItem>
    {
        public MockItemRepository(ItemDataAccessor<MockItem> dao)
            :base(dao)
        {

        }
    }//class
}
