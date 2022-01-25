using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using NUnit;
using NUnit.Framework;
using FakeItEasy;
using MyLibrary.Models.Entities;
using MyLibrary.Models.BusinessLogic;
using MyLibrary.DataAccessLayer;
using MyLibrary.DataAccessLayer.Repositories;
using MyLibrary.DataAccessLayer.ServiceProviders;

namespace MyLibrary_Test.Models_Tests.BusinessLogic_Tests
{
    [TestFixture]
    public class MediaItemService_Tests
    {
        [Test]
        public async Task GetAll_Test()
        {
            // arrange
            var fakeUowProvider = A.Fake<IUnitOfWorkProvider>();
            var fakeRepoProvider = A.Fake<IMediaItemRepositoryProvider>();
            var fakeTagRepoProvider = A.Fake<ITagRepositoryServiceProvider>();
            var fakeUow = A.Fake<IUnitOfWork>();
            var fakeRepo = A.Fake<IMediaItemRepository>();
            var fakeTagRepo = A.Fake<ITagRepository>();
            A.CallTo(() => fakeUowProvider.Get()).Returns(fakeUow);
            A.CallTo(() => fakeRepoProvider.Get(fakeUow)).Returns(fakeRepo);
            A.CallTo(() => fakeTagRepoProvider.Get(fakeUow)).Returns(fakeTagRepo);
            MediaItemService service = new MediaItemService(fakeUowProvider, fakeRepoProvider, fakeTagRepoProvider);
            List<MediaItem> items = new List<MediaItem>
            {
                new MediaItem{Id=1, Title="item1"}
            };
            A.CallTo(() => fakeRepo.ReadAll()).Returns(items);

            // act
            var result = await service.GetAll();

            // assert
            Assert.IsTrue(result.ToList().Count == 1);
            Assert.IsTrue(result.ToList()[0].Id == 1);
            A.CallTo(() => fakeUow.Dispose()).MustHaveHappened();
        }

        [Test]
        public async Task Update_Test()
        {
            // arrange
            var fakeUowProvider = A.Fake<IUnitOfWorkProvider>();
            var fakeRepoProvider = A.Fake<IMediaItemRepositoryProvider>();
            var fakeTagRepoProvider = A.Fake<ITagRepositoryServiceProvider>();
            var fakeUow = A.Fake<IUnitOfWork>();
            var fakeRepo = A.Fake<IMediaItemRepository>();
            var fakeTagRepo = A.Fake<ITagRepository>();
            A.CallTo(() => fakeUowProvider.Get()).Returns(fakeUow);
            A.CallTo(() => fakeRepoProvider.Get(fakeUow)).Returns(fakeRepo);
            A.CallTo(() => fakeTagRepoProvider.Get(fakeUow)).Returns(fakeTagRepo);
            MediaItemService service = new MediaItemService(fakeUowProvider, fakeRepoProvider, fakeTagRepoProvider);
            MediaItem item = new MediaItem { Id = 1, Title = "item" };

            // act
            await service.Add(item);

            // assert
            A.CallTo(() => fakeUow.Begin()).MustHaveHappened();
            A.CallTo(() => fakeRepo.Create(item)).MustHaveHappened();
            A.CallTo(() => fakeUow.Commit()).MustHaveHappened();
        }

        [Test]
        public async Task DeleteById_Test()
        {
            // arrange
            var fakeUowProvider = A.Fake<IUnitOfWorkProvider>();
            var fakeRepoProvider = A.Fake<IMediaItemRepositoryProvider>();
            var fakeTagRepoProvider = A.Fake<ITagRepositoryServiceProvider>();
            var fakeUow = A.Fake<IUnitOfWork>();
            var fakeRepo = A.Fake<IMediaItemRepository>();
            var fakeTagRepo = A.Fake<ITagRepository>();
            A.CallTo(() => fakeUowProvider.Get()).Returns(fakeUow);
            A.CallTo(() => fakeRepoProvider.Get(fakeUow)).Returns(fakeRepo);
            A.CallTo(() => fakeTagRepoProvider.Get(fakeUow)).Returns(fakeTagRepo);
            MediaItemService service = new MediaItemService(fakeUowProvider, fakeRepoProvider, fakeTagRepoProvider);

            // act
            await service.DeleteById(1);

            // assert
            A.CallTo(() => fakeRepo.DeleteById(1)).MustHaveHappened();
            A.CallTo(() => fakeUow.Dispose()).MustHaveHappened();
        }

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

        [Test]
        public async Task GetByType_Test()
        {
            // arrange
            MockMediaItemService service = new MockMediaItemService();

            // act
            var result = await service.GetByType(ItemType.Dvd);

            // assert
            Assert.IsTrue(result.ToList().Count() == 1);
            Assert.IsTrue(result.ToList()[0].Id == 1);
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

        [Test]
        public async Task Add_Test()
        {
            // arrange
            var fakeUowProvider = A.Fake<IUnitOfWorkProvider>();
            var fakeRepoProvider = A.Fake<IMediaItemRepositoryProvider>();
            var fakeTagRepoProvider = A.Fake<ITagRepositoryServiceProvider>();
            var fakeUow = A.Fake<IUnitOfWork>();
            var fakeRepo = A.Fake<IMediaItemRepository>();
            var fakeTagRepo = A.Fake<ITagRepository>();
            A.CallTo(() => fakeUowProvider.Get()).Returns(fakeUow);
            A.CallTo(() => fakeRepoProvider.Get(fakeUow)).Returns(fakeRepo);
            A.CallTo(() => fakeTagRepoProvider.Get(fakeUow)).Returns(fakeTagRepo);
            A.CallTo(() => fakeTagRepo.ExistsWithName("new_tag")).Returns(false);
            A.CallTo(() => fakeTagRepo.ExistsWithName("existing_tag")).Returns(true);
            MediaItemService service = new MediaItemService(fakeUowProvider, fakeRepoProvider, fakeTagRepoProvider);
            Tag newTag = new Tag { Id = 1, Name = "new_tag" };
            Tag existingTag = new Tag { Id = 2, Name = "existing_tag" };
            MediaItem item = new MediaItem
            {
                Id = 1,
                Title = "new_item",
                Tags = new List<Tag>
                {
                    newTag,
                    existingTag
                }
            };

            // act
            await service.Add(item);

            // assert
            A.CallTo(() => fakeUow.Begin()).MustHaveHappened();
            A.CallTo(() => fakeRepo.Create(item)).MustHaveHappened();
            A.CallTo(() => fakeTagRepo.Create(newTag)).MustHaveHappened();
            A.CallTo(() => fakeTagRepo.LinkMediaItem(1, 1));
            A.CallTo(() => fakeTagRepo.LinkMediaItem(1, 2));
            A.CallTo(() => fakeUow.Commit()).MustHaveHappened();
        }

        [Test]
        public async Task UpdateTags_Test()
        {
            // arrange
            var fakeUowProvider = A.Fake<IUnitOfWorkProvider>();
            var fakeRepoProvider = A.Fake<IMediaItemRepositoryProvider>();
            var fakeTagRepoProvider = A.Fake<ITagRepositoryServiceProvider>();
            var fakeUow = A.Fake<IUnitOfWork>();
            var fakeRepo = A.Fake<IMediaItemRepository>();
            var fakeTagRepo = A.Fake<ITagRepository>();
            A.CallTo(() => fakeUowProvider.Get()).Returns(fakeUow);
            A.CallTo(() => fakeRepoProvider.Get(fakeUow)).Returns(fakeRepo);
            A.CallTo(() => fakeTagRepoProvider.Get(fakeUow)).Returns(fakeTagRepo);
            A.CallTo(() => fakeTagRepo.ExistsWithName("tag1")).Returns(true);
            A.CallTo(() => fakeTagRepo.GetIdByName("tag1")).Returns(1);
            A.CallTo(() => fakeTagRepo.ExistsWithName("tag2")).Returns(true);
            A.CallTo(() => fakeTagRepo.GetIdByName("tag2")).Returns(2);
            A.CallTo(() => fakeTagRepo.ExistsWithName("tag3")).Returns(true);
            A.CallTo(() => fakeTagRepo.GetIdByName("tag3")).Returns(3);
            A.CallTo(() => fakeTagRepo.ExistsWithName("tag4")).Returns(false);
            A.CallTo(() => fakeTagRepo.GetIdByName("tag4")).Returns(4);
            MediaItemService service = new MediaItemService(fakeUowProvider, fakeRepoProvider, fakeTagRepoProvider);
            List<string> originalTags = new List<string> { "tag1", "tag2", "tag3" };
            List<string> selectedTags = new List<string> { "tag2", "tag4" };
            ItemTagsDto itemTags = new ItemTagsDto(1, originalTags, selectedTags);
            Tag newTag = new Tag { Name = "tag4" };

            // act
            await service.UpdateTags(itemTags);

            // assert
            A.CallTo(() => fakeUow.Begin()).MustHaveHappened();
            A.CallTo(() => fakeTagRepo.LinkMediaItem(1, 4));
            A.CallTo(() => fakeTagRepo.UnlinkMediaItem(1, 1));
            A.CallTo(() => fakeUow.Commit()).MustHaveHappened();
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
                        Title = "item1",
                        Type=ItemType.Dvd
                    });

                    items.Add(new MediaItem
                    {
                        Id = 2,
                        Title = "item2",
                        Type=ItemType.BluRay
                    });
                });

                return items;
            }//GetAll
        }//class
    }//class
}
