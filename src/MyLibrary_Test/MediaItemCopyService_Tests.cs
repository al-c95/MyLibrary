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
    public class MediaItemCopyService_Tests
    {
        [Test]
        public async Task Create_Test()
        {
            // arrange
            var fakeUowProvider = A.Fake<IUnitOfWorkProvider>();
            var fakeUow = A.Fake<IUnitOfWork>();
            A.CallTo(() => fakeUowProvider.Get()).Returns(fakeUow);
            var fakeRepoProvider = A.Fake<IMediaItemCopyRepositoryProvider>();
            var fakeRepo = A.Fake<IMediaItemCopyRepository>();
            A.CallTo(() => fakeRepoProvider.Get(fakeUow)).Returns(fakeRepo);
            MediaItemCopyService service = new MediaItemCopyService(fakeUowProvider, fakeRepoProvider);
            MediaItemCopy copy = new MediaItemCopy { Id = 1 };

            // act
            await service.Create(copy);

            // assert
            A.CallTo(() => fakeRepo.CreateAsync(copy)).MustHaveHappened();
            A.CallTo(() => fakeUow.Dispose()).MustHaveHappened();
        }

        [Test]
        public async Task Update_Test()
        {
            // arrange
            var fakeUowProvider = A.Fake<IUnitOfWorkProvider>();
            var fakeUow = A.Fake<IUnitOfWork>();
            A.CallTo(() => fakeUowProvider.Get()).Returns(fakeUow);
            var fakeRepoProvider = A.Fake<IMediaItemCopyRepositoryProvider>();
            var fakeRepo = A.Fake<IMediaItemCopyRepository>();
            A.CallTo(() => fakeRepoProvider.Get(fakeUow)).Returns(fakeRepo);
            MediaItemCopyService service = new MediaItemCopyService(fakeUowProvider, fakeRepoProvider);
            MediaItemCopy copy = new MediaItemCopy { Id = 1 };

            // act
            await service.Update(copy);

            // assert
            A.CallTo(() => fakeRepo.UpdateAsync(copy)).MustHaveHappened();
            A.CallTo(() => fakeUow.Dispose()).MustHaveHappened();
        }

        [Test]
        public async Task DeleteById_Test()
        {
            // arrange
            var fakeUowProvider = A.Fake<IUnitOfWorkProvider>();
            var fakeUow = A.Fake<IUnitOfWork>();
            A.CallTo(() => fakeUowProvider.Get()).Returns(fakeUow);
            var fakeRepoProvider = A.Fake<IMediaItemCopyRepositoryProvider>();
            var fakeRepo = A.Fake<IMediaItemCopyRepository>();
            A.CallTo(() => fakeRepoProvider.Get(fakeUow)).Returns(fakeRepo);
            MediaItemCopyService service = new MediaItemCopyService(fakeUowProvider, fakeRepoProvider);

            // act
            await service.DeleteById(1);

            // assert
            A.CallTo(() => fakeRepo.DeleteByIdAsync(1)).MustHaveHappened();
            A.CallTo(() => fakeUow.Dispose()).MustHaveHappened();
        }

        [Test]
        public async Task GetByItemId_Test()
        {
            // arrange
            MockService service = new MockService();

            // act
            var result = await service.GetByItemId(1);

            // assert
            Assert.IsTrue(result.ToList().Count == 1);
            Assert.IsTrue(result.ToList()[0].MediaItemId == 1);
        }

        [Test]
        public async Task GetAll_Test()
        {
            // arrange
            var fakeUowProvider = A.Fake<IUnitOfWorkProvider>();
            var fakeUow = A.Fake<IUnitOfWork>();
            A.CallTo(() => fakeUowProvider.Get()).Returns(fakeUow);
            var fakeRepoProvider = A.Fake<IMediaItemCopyRepositoryProvider>();
            var fakeRepo = A.Fake<IMediaItemCopyRepository>();
            A.CallTo(() => fakeRepoProvider.Get(fakeUow)).Returns(fakeRepo);
            MediaItemCopyService service = new MediaItemCopyService(fakeUowProvider, fakeRepoProvider);
            List<MediaItemCopy> copies = new List<MediaItemCopy>
            {
                new MediaItemCopy{Id=1}
            };
            A.CallTo(() => fakeRepo.ReadAllAsync()).Returns(copies);

            // act
            var result = await service.GetAll();

            // assert
            Assert.IsTrue(result.ToList().Count == 1);
            Assert.IsTrue(result.ToList()[0].Id == 1);
        }

        [TestCase("copy1", true)]
        [TestCase("copy2", false)]
        public async Task ExistsWithDescription_Test(string description, bool expectedResult)
        {
            // arrange
            MockService service = new MockService();

            // act
            bool actualResult = await service.ExistsWithDescription(description);

            // assert
            Assert.AreEqual(expectedResult, actualResult);
        }

        class MockService : MediaItemCopyService
        {
            public async override Task<IEnumerable<MediaItemCopy>> GetAll()
            {
                List<MediaItemCopy> copies = new List<MediaItemCopy>();
                await Task.Run(() =>
                {
                    copies.Add(new MediaItemCopy
                    {
                        MediaItemId = 1,
                        Description = "copy1"
                    });
                });

                return copies;
            }
        }//class
    }//class
}
