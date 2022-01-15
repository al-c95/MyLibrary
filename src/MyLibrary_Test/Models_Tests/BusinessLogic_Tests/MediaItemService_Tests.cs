using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using NUnit;
using NUnit.Framework;
using MyLibrary.Models.Entities;
using MyLibrary.Models.BusinessLogic;
using MyLibrary.DataAccessLayer;
using MyLibrary.DataAccessLayer.Repositories;

namespace MyLibrary_Test.Models_Tests.BusinessLogic_Tests
{
    [TestFixture]
    public class MediaItemService_Tests
    {
        [Test]
        public async Task GetById_Test()
        {
            // arrange
            string expectedTitle = "item1";
            int id = 1;
            MockMediaItemService service = new MockMediaItemService();

            // act
            MediaItem result = await service.GetById(id);
            string actualTitle = result.Title;

            // assert
            Assert.AreEqual(expectedTitle, actualTitle);
        }

        [Test]
        public async Task GetIdByTitle_Test()
        {
            // arrange
            string title = "item2";
            int expectedId = 2;
            MockMediaItemService service = new MockMediaItemService();

            // act
            int actualId = await service.GetIdByTitle(title);

            // assert
            Assert.AreEqual(expectedId, actualId);
        }

        [TestCase(1,true)]
        [TestCase(3, false)]
        public async Task ExistsWithId_Test(int id, bool expectedResult)
        {
            // arrange
            MockMediaItemService service = new MockMediaItemService();

            // act
            bool actualResult = await service.ExistsWithId(id);

            // assert
            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestCase("item1", true)]
        [TestCase("item3", false)]
        public async Task ExistsWithTitle_Test(string title, bool expectedResult)
        {
            // arrange
            MockMediaItemService service = new MockMediaItemService();

            // act
            bool actualResult = await service.ExistsWithTitle(title);

            // assert
            Assert.AreEqual(expectedResult, actualResult);
        }

        class MockMediaItemService : MediaItemService
        {
            public async override Task<IEnumerable<MediaItem>> GetAll()
            {
                List<MediaItem> items = new List<MediaItem>();
                await Task.Run(() =>
                {
                    items.Add(new MediaItem
                    {
                        Id=1,
                        Title = "item1"
                    });

                    items.Add(new MediaItem
                    {
                        Id = 2,
                        Title = "item2"
                    });
                });

                return items;
            }//GetAll
        }//class
    }//class
}
