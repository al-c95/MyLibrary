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
    public class TagService_Tests
    {
        [TestCase("tag1", true)]
        [TestCase("bogus", false)]
        public async Task ExistsWithName_Test(string name, bool expectedResult)
        {
            // arrange
            var fakeUowProvider = A.Fake<IUnitOfWorkProvider>();
            var fakeRepoProvider = A.Fake<ITagRepositoryServiceProvider>();
            var fakeUow = A.Fake<IUnitOfWork>();
            var fakeRepo = A.Fake<ITagRepository>();
            A.CallTo(() => fakeRepo.ExistsWithNameAsync("tag1")).Returns(true);
            A.CallTo(() => fakeRepo.ExistsWithNameAsync("bogus")).Returns(false);
            A.CallTo(() => fakeUowProvider.Get()).Returns(fakeUow);
            A.CallTo(() => fakeRepoProvider.Get(fakeUow)).Returns(fakeRepo);
            TagService service = new TagService(fakeUowProvider, fakeRepoProvider);

            // act
            bool actualResult = await service.ExistsWithName(name);

            // assert
            Assert.AreEqual(expectedResult, actualResult);
        }

        [Test]
        public async Task Add_Test()
        {
            // arrange
            var fakeUowProvider = A.Fake<IUnitOfWorkProvider>();
            var fakeRepoProvider = A.Fake<ITagRepositoryServiceProvider>();
            var fakeUow = A.Fake<IUnitOfWork>();
            var fakeRepo = A.Fake<ITagRepository>();
            A.CallTo(() => fakeUowProvider.Get()).Returns(fakeUow);
            A.CallTo(() => fakeRepoProvider.Get(fakeUow)).Returns(fakeRepo);
            Tag tag = new Tag { Id = 1, Name = "tag" };
            TagService service = new TagService(fakeUowProvider, fakeRepoProvider);

            // act
            await service.Add(tag);

            // assert
            A.CallTo(() => fakeRepo.CreateAsync(tag)).MustHaveHappened();
        }

        [Test]
        public async Task AddIfNotExists_Test_Exists()
        {
            // arrange
            var fakeUowProvider = A.Fake<IUnitOfWorkProvider>();
            var fakeRepoProvider = A.Fake<ITagRepositoryServiceProvider>();
            var fakeUow = A.Fake<IUnitOfWork>();
            var fakeRepo = A.Fake<ITagRepository>();
            A.CallTo(() => fakeRepo.ExistsWithNameAsync("tag")).Returns(true);
            A.CallTo(() => fakeUowProvider.Get()).Returns(fakeUow);
            A.CallTo(() => fakeRepoProvider.Get(fakeUow)).Returns(fakeRepo);
            TagService service = new TagService(fakeUowProvider, fakeRepoProvider);

            // act
            bool result = await service.AddIfNotExists(new Tag { Name = "tag" });

            // assert
            Assert.IsFalse(result);
            A.CallTo(() => fakeRepo.CreateAsync(A<Tag>.That.Matches(t => t.Name=="tag"))).MustNotHaveHappened();
        }

        [Test]
        public async Task AddIfNotExists_Test_DoesNotExist()
        {
            // arrange
            var fakeUowProvider = A.Fake<IUnitOfWorkProvider>();
            var fakeRepoProvider = A.Fake<ITagRepositoryServiceProvider>();
            var fakeUow = A.Fake<IUnitOfWork>();
            var fakeRepo = A.Fake<ITagRepository>();
            A.CallTo(() => fakeRepo.ExistsWithNameAsync("tag")).Returns(false);
            A.CallTo(() => fakeUowProvider.Get()).Returns(fakeUow);
            A.CallTo(() => fakeRepoProvider.Get(fakeUow)).Returns(fakeRepo);
            TagService service = new TagService(fakeUowProvider, fakeRepoProvider);

            // act
            bool result = await service.AddIfNotExists(new Tag { Name = "tag" });

            // assert
            Assert.IsTrue(result);
            A.CallTo(() => fakeRepo.CreateAsync(A<Tag>.That.Matches(t => t.Name == "tag"))).MustHaveHappened();
        }

        [Test]
        public async Task DeleteByName()
        {
            // arrange
            var fakeUowProvider = A.Fake<IUnitOfWorkProvider>();
            var fakeRepoProvider = A.Fake<ITagRepositoryServiceProvider>();
            var fakeUow = A.Fake<IUnitOfWork>();
            var fakeRepo = A.Fake<ITagRepository>();
            A.CallTo(() => fakeUowProvider.Get()).Returns(fakeUow);
            A.CallTo(() => fakeRepoProvider.Get(fakeUow)).Returns(fakeRepo);
            TagService service = new TagService(fakeUowProvider, fakeRepoProvider);

            // act
            await service.DeleteByName("tag");

            // assert
            A.CallTo(() => fakeRepo.DeleteByNameAsync("tag")).MustHaveHappened();
        }

        [Test]
        public async Task GetAll_Test()
        {
            // arrange
            var fakeUowProvider = A.Fake<IUnitOfWorkProvider>();
            var fakeRepoProvider = A.Fake<ITagRepositoryServiceProvider>();
            var fakeUow = A.Fake<IUnitOfWork>();
            var fakeRepo = A.Fake<ITagRepository>();
            A.CallTo(() => fakeUowProvider.Get()).Returns(fakeUow);
            A.CallTo(() => fakeRepoProvider.Get(fakeUow)).Returns(fakeRepo);
            List<Tag> tags = new List<Tag>
            {
                new Tag{Id=1, Name="tag1" },
                new Tag{ Id=2, Name="tag2"}
            };
            A.CallTo(() => fakeRepo.ReadAllAsync()).Returns(tags);
            TagService service = new TagService(fakeUowProvider, fakeRepoProvider);

            // act
            var results = await service.GetAll();

            // assert
            Assert.IsTrue(results.ToList().Count == 2);
            Assert.IsTrue(results.ToList().Any(a => a.Id == 1));
            Assert.IsTrue(results.ToList().Any(a => a.Id == 2));
        }
    }//class
}
