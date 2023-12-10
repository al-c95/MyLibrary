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
    public class PublisherService_Tests
    {
        [Test]
        public async Task Create_Test()
        {
            // arrange
            var fakeUowProvider = A.Fake<IUnitOfWorkProvider>();
            var fakeRepoProvider = A.Fake<IPublisherRepositoryProvider>();
            var fakeRepo = A.Fake<IPublisherRepository>();
            var fakeUow = A.Fake<IUnitOfWork>();
            A.CallTo(() => fakeUowProvider.Get()).Returns(fakeUow);
            A.CallTo(() => fakeRepoProvider.Get(fakeUow)).Returns(fakeRepo);
            PublisherService service = new PublisherService(fakeUowProvider, fakeRepoProvider);
            Publisher publisher = new Publisher("some_publisher");

            // act
            await service.Add(publisher);

            // assert
            A.CallTo(() => fakeRepo.CreateAsync(publisher)).MustHaveHappened();
            A.CallTo(() => fakeUow.Dispose()).MustHaveHappened();
        }

        [Test]
        public async Task Exists_Test_Exists()
        {
            // arrange
            var fakeUowProvider = A.Fake<IUnitOfWorkProvider>();
            var fakeRepoProvider = A.Fake<IPublisherRepositoryProvider>();
            var fakeRepo = A.Fake<IPublisherRepository>();
            A.CallTo(() => fakeRepo.ExistsWithNameAsync("publisher")).Returns(true);
            var fakeUow = A.Fake<IUnitOfWork>();
            A.CallTo(() => fakeUowProvider.Get()).Returns(fakeUow);
            A.CallTo(() => fakeRepoProvider.Get(fakeUow)).Returns(fakeRepo);
            PublisherService service = new PublisherService(fakeUowProvider, fakeRepoProvider);

            // act
            bool result = await service.ExistsWithName("publisher");

            // assert
            Assert.IsTrue(result);
        }

        [Test]
        public async Task Exists_Test_DoesNotExist()
        {
            // arrange
            var fakeUowProvider = A.Fake<IUnitOfWorkProvider>();
            var fakeRepoProvider = A.Fake<IPublisherRepositoryProvider>();
            var fakeRepo = A.Fake<IPublisherRepository>();
            A.CallTo(() => fakeRepo.ExistsWithNameAsync("publisher")).Returns(false);
            var fakeUow = A.Fake<IUnitOfWork>();
            A.CallTo(() => fakeUowProvider.Get()).Returns(fakeUow);
            A.CallTo(() => fakeRepoProvider.Get(fakeUow)).Returns(fakeRepo);
            PublisherService service = new PublisherService(fakeUowProvider, fakeRepoProvider);

            // act
            bool result = await service.ExistsWithName("publisher");

            // assert
            Assert.IsFalse(result);
        }

        [Test]
        public async Task AddIfNotExists_Test_Exists()
        {
            // arrange
            var fakeUowProvider = A.Fake<IUnitOfWorkProvider>();
            var fakeRepoProvider = A.Fake<IPublisherRepositoryProvider>();
            var fakeRepo = A.Fake<IPublisherRepository>();
            A.CallTo(() => fakeRepo.ExistsWithNameAsync("publisher")).Returns(true);
            var fakeUow = A.Fake<IUnitOfWork>();
            A.CallTo(() => fakeUowProvider.Get()).Returns(fakeUow);
            A.CallTo(() => fakeRepoProvider.Get(fakeUow)).Returns(fakeRepo);
            PublisherService service = new PublisherService(fakeUowProvider, fakeRepoProvider);

            // act
            bool result = await service.AddIfNotExists(new Publisher { Name = "publisher" });

            // assert
            Assert.IsFalse(result);
            A.CallTo(() => fakeRepo.CreateAsync(A<Publisher>.That.Matches(p => p.Name=="publisher"))).MustNotHaveHappened();
        }

        [Test]
        public async Task AddIfNotExists_Test_DoesNotExist()
        {
            // arrange
            var fakeUowProvider = A.Fake<IUnitOfWorkProvider>();
            var fakeRepoProvider = A.Fake<IPublisherRepositoryProvider>();
            var fakeRepo = A.Fake<IPublisherRepository>();
            A.CallTo(() => fakeRepo.ExistsWithNameAsync("publisher")).Returns(false);
            var fakeUow = A.Fake<IUnitOfWork>();
            A.CallTo(() => fakeUowProvider.Get()).Returns(fakeUow);
            A.CallTo(() => fakeRepoProvider.Get(fakeUow)).Returns(fakeRepo);
            PublisherService service = new PublisherService(fakeUowProvider, fakeRepoProvider);

            // act
            bool result = await service.AddIfNotExists(new Publisher { Name = "publisher" });

            // assert
            Assert.IsTrue(result);
            A.CallTo(() => fakeRepo.CreateAsync(A<Publisher>.That.Matches(p => p.Name == "publisher"))).MustHaveHappened();
        }

        [Test]
        public async Task GetAll_Test()
        {
            // arrange
            var fakeUowProvider = A.Fake<IUnitOfWorkProvider>();
            var fakeRepoProvider = A.Fake<IPublisherRepositoryProvider>();
            var fakeRepo = A.Fake<IPublisherRepository>();
            var fakeUow = A.Fake<IUnitOfWork>();
            A.CallTo(() => fakeUowProvider.Get()).Returns(fakeUow);
            A.CallTo(() => fakeRepoProvider.Get(fakeUow)).Returns(fakeRepo);
            PublisherService service = new PublisherService(fakeUowProvider, fakeRepoProvider);
            List<Publisher> publishers = new List<Publisher>
            {
                new Publisher("pub1"),
                new Publisher("pub2")
            };
            A.CallTo(() => fakeRepo.ReadAllAsync()).Returns(publishers);

            // act
            var result = await service.GetAll();

            // assert
            Assert.IsTrue(result.ToList().Count == 2);
            Assert.IsTrue(result.ToList()[0].Name.Equals("pub1"));
            Assert.IsTrue(result.ToList()[1].Name.Equals("pub2"));
        }
    }//class
}
