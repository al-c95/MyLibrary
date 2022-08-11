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
        public async Task GetAllAsync_Test()
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
            var result = await service.GetAllAsync();

            // assert
            Assert.IsTrue(result.ToList().Count == 1);
            Assert.IsTrue(result.ToList()[0].Id == 1);
            A.CallTo(() => fakeUow.Dispose()).MustHaveHappened();
        }

        [Test]
        public void Update_Test()
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
            service.Update(item);

            // assert
            A.CallTo(() => fakeRepo.Update(item)).MustHaveHappened();
            A.CallTo(() => fakeUow.Commit()).MustHaveHappened();
        }

        [Test]
        public async Task UpdateAsync_Test()
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
            await service.UpdateAsync(item);

            // assert
            A.CallTo(() => fakeRepo.Update(item)).MustHaveHappened();
            A.CallTo(() => fakeUow.Commit()).MustHaveHappened();
        }

        [Test]
        public async Task DeleteByIdAsync_Test()
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
            await service.DeleteByIdAsync(1);

            // assert
            A.CallTo(() => fakeRepo.DeleteById(1)).MustHaveHappened();
            A.CallTo(() => fakeUow.Commit()).MustHaveHappened();
        }

        [Test]
        public async Task GetByIdAsync_Test()
        {
            // arrange
            var fakeUowProvider = A.Fake<IUnitOfWorkProvider>();
            var fakeRepoProvider = A.Fake<IMediaItemRepositoryProvider>();
            var fakeTagRepoProvider = A.Fake<ITagRepositoryServiceProvider>();
            var fakeUow = A.Fake<IUnitOfWork>();
            var fakeRepo = A.Fake<IMediaItemRepository>();
            MediaItem item = new MediaItem
            {
                Id = 1,
                Title = "item"
            };
            A.CallTo(() => fakeRepo.GetById(1)).Returns(item);
            var fakeTagRepo = A.Fake<ITagRepository>();
            A.CallTo(() => fakeUowProvider.Get()).Returns(fakeUow);
            A.CallTo(() => fakeRepoProvider.Get(fakeUow)).Returns(fakeRepo);
            A.CallTo(() => fakeTagRepoProvider.Get(fakeUow)).Returns(fakeTagRepo);
            MediaItemService service = new MediaItemService(fakeUowProvider, fakeRepoProvider, fakeTagRepoProvider);

            // act
            var result = await service.GetByIdAsync(1);

            // assert
            Assert.AreEqual(1, result.Id);
            Assert.AreEqual("item", result.Title);
        }

        [Test]
        public async Task GetIdByTitleAsync_Test()
        {
            // arrange
            string title = "item2";
            int expectedId = 2;
            MockMediaItemService service = new MockMediaItemService();

            // act
            int actualId = await service.GetIdByTitleAsync(title);

            // assert
            Assert.AreEqual(expectedId, actualId);
        }

        [Test]
        public async Task GetByTypeAsync_Test()
        {
            // arrange
            MockMediaItemService service = new MockMediaItemService();

            // act
            var result = await service.GetByTypeAsync(ItemType.Dvd);

            // assert
            Assert.IsTrue(result.ToList().Count() == 1);
            Assert.IsTrue(result.ToList()[0].Id == 1);
        }

        [TestCase(1,true)]
        [TestCase(3, false)]
        public async Task ExistsWithIdAsync_Test(int id, bool expectedResult)
        {
            // arrange
            MockMediaItemService service = new MockMediaItemService();

            // act
            bool actualResult = await service.ExistsWithIdAsync(id);

            // assert
            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestCase("item1", true)]
        [TestCase("item3", false)]
        public void ExistsWithTitle_Test(string title, bool expectedResult)
        {
            // arrange
            var fakeUowProvider = A.Fake<IUnitOfWorkProvider>();
            var fakeRepoProvider = A.Fake<IMediaItemRepositoryProvider>();
            var fakeTagRepoProvider = A.Fake<ITagRepositoryServiceProvider>();
            var fakeUow = A.Fake<IUnitOfWork>();
            var fakeRepo = A.Fake<IMediaItemRepository>();
            List<string> titles = new List<string>
            {
                "item1",
                "item2"
            };
            A.CallTo(() => fakeRepo.GetTitles()).Returns(titles);
            var fakeTagRepo = A.Fake<ITagRepository>();
            A.CallTo(() => fakeUowProvider.Get()).Returns(fakeUow);
            A.CallTo(() => fakeRepoProvider.Get(fakeUow)).Returns(fakeRepo);
            A.CallTo(() => fakeTagRepoProvider.Get(fakeUow)).Returns(fakeTagRepo);
            MediaItemService service = new MediaItemService(fakeUowProvider, fakeRepoProvider, fakeTagRepoProvider);

            // arrange
            var actualResult = service.ExistsWithTitle(title);

            // act
            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestCase("item1", true)]
        [TestCase("item3", false)]
        public async Task ExistsWithTitleAsync_Test(string title, bool expectedResult)
        {
            // arrange
            var fakeUowProvider = A.Fake<IUnitOfWorkProvider>();
            var fakeRepoProvider = A.Fake<IMediaItemRepositoryProvider>();
            var fakeTagRepoProvider = A.Fake<ITagRepositoryServiceProvider>();
            var fakeUow = A.Fake<IUnitOfWork>();
            var fakeRepo = A.Fake<IMediaItemRepository>();
            List<string> titles = new List<string>
            {
                "item1",
                "item2"
            };
            A.CallTo(() => fakeRepo.GetTitles()).Returns(titles);
            var fakeTagRepo = A.Fake<ITagRepository>();
            A.CallTo(() => fakeUowProvider.Get()).Returns(fakeUow);
            A.CallTo(() => fakeRepoProvider.Get(fakeUow)).Returns(fakeRepo);
            A.CallTo(() => fakeTagRepoProvider.Get(fakeUow)).Returns(fakeTagRepo);
            MediaItemService service = new MediaItemService(fakeUowProvider, fakeRepoProvider, fakeTagRepoProvider);

            // arrange
            var actualResult = await service.ExistsWithTitleAsync(title);

            // act
            Assert.AreEqual(expectedResult, actualResult);
        }

        [Test]
        public void Add_Test()
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
            service.Add(item);

            // assert
            A.CallTo(() => fakeUow.Begin()).MustHaveHappened();
            A.CallTo(() => fakeRepo.Create(item)).MustHaveHappened();
            A.CallTo(() => fakeTagRepo.Create(newTag)).MustHaveHappened();
            A.CallTo(() => fakeTagRepo.LinkMediaItem(1, 1));
            A.CallTo(() => fakeTagRepo.LinkMediaItem(1, 2));
            A.CallTo(() => fakeUow.Commit()).MustHaveHappened();
        }

        [Test]
        public void AddIfNotExists_Test_DoesNotExist()
        {
            // arrange
            var fakeUowProvider = A.Fake<IUnitOfWorkProvider>();
            var fakeRepoProvider = A.Fake<IMediaItemRepositoryProvider>();
            var fakeTagRepoProvider = A.Fake<ITagRepositoryServiceProvider>();
            var fakeUow = A.Fake<IUnitOfWork>();
            var fakeRepo = A.Fake<IMediaItemRepository>();
            List<string> titles = new List<string> { "existing_item" };
            A.CallTo(() => fakeRepo.GetTitles()).Returns(titles);
            var fakeTagRepo = A.Fake<ITagRepository>();
            A.CallTo(() => fakeUowProvider.Get()).Returns(fakeUow);
            A.CallTo(() => fakeRepoProvider.Get(fakeUow)).Returns(fakeRepo);
            A.CallTo(() => fakeTagRepoProvider.Get(fakeUow)).Returns(fakeTagRepo);
            MediaItemService service = new MediaItemService(fakeUowProvider, fakeRepoProvider, fakeTagRepoProvider);
            MediaItem item = new MediaItem
            {
                Title = "new_item"
            };

            // act
            bool result = service.AddIfNotExists(item);

            // assert
            Assert.IsTrue(result);
            A.CallTo(() => fakeUow.Begin()).MustHaveHappened();
            A.CallTo(() => fakeRepo.Create(item)).MustHaveHappened();
            A.CallTo(() => fakeUow.Commit()).MustHaveHappened();
        }

        [Test]
        public void AddIfNotExists_Test_Exists()
        {
            // arrange
            var fakeUowProvider = A.Fake<IUnitOfWorkProvider>();
            var fakeRepoProvider = A.Fake<IMediaItemRepositoryProvider>();
            var fakeTagRepoProvider = A.Fake<ITagRepositoryServiceProvider>();
            var fakeUow = A.Fake<IUnitOfWork>();
            var fakeRepo = A.Fake<IMediaItemRepository>();
            List<string> titles = new List<string> { "existing_item" };
            A.CallTo(() => fakeRepo.GetTitles()).Returns(titles);
            var fakeTagRepo = A.Fake<ITagRepository>();
            A.CallTo(() => fakeUowProvider.Get()).Returns(fakeUow);
            A.CallTo(() => fakeRepoProvider.Get(fakeUow)).Returns(fakeRepo);
            A.CallTo(() => fakeTagRepoProvider.Get(fakeUow)).Returns(fakeTagRepo);
            MediaItemService service = new MediaItemService(fakeUowProvider, fakeRepoProvider, fakeTagRepoProvider);
            MediaItem item = new MediaItem
            {
                Title = "existing_item"
            };

            // act
            bool result = service.AddIfNotExists(item);

            // assert
            Assert.IsFalse(result);
            A.CallTo(() => fakeUow.Begin()).MustNotHaveHappened();
            A.CallTo(() => fakeRepo.Create(item)).MustNotHaveHappened();
            A.CallTo(() => fakeUow.Commit()).MustNotHaveHappened();
        }

        [Test]
        public async Task AddAsync_Test()
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
            await service.AddAsync(item);

            // assert
            A.CallTo(() => fakeUow.Begin()).MustHaveHappened();
            A.CallTo(() => fakeRepo.Create(item)).MustHaveHappened();
            A.CallTo(() => fakeTagRepo.Create(newTag)).MustHaveHappened();
            A.CallTo(() => fakeTagRepo.LinkMediaItem(1, 1));
            A.CallTo(() => fakeTagRepo.LinkMediaItem(1, 2));
            A.CallTo(() => fakeUow.Commit()).MustHaveHappened();
        }

        [Test]
        public void UpdateTags_Test()
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
            service.UpdateTags(itemTags);

            // assert
            A.CallTo(() => fakeUow.Begin()).MustHaveHappened();
            A.CallTo(() => fakeTagRepo.LinkMediaItem(1, 4));
            A.CallTo(() => fakeTagRepo.UnlinkMediaItem(1, 1));
            A.CallTo(() => fakeUow.Commit()).MustHaveHappened();
        }

        [Test]
        public async Task UpdateTagsAsync_Test()
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
            await service.UpdateTagsAsync(itemTags);

            // assert
            A.CallTo(() => fakeUow.Begin()).MustHaveHappened();
            A.CallTo(() => fakeTagRepo.LinkMediaItem(1, 4));
            A.CallTo(() => fakeTagRepo.UnlinkMediaItem(1, 1));
            A.CallTo(() => fakeUow.Commit()).MustHaveHappened();
        }

        class MockMediaItemService : MediaItemService
        {
            public async override Task<IEnumerable<MediaItem>> GetAllAsync()
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
