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
    public class TagService_Tests
    {
        [TestCase("tag1", true)]
        [TestCase("bogus", false)]
        public async Task ExistsWithName_Test(string name, bool expectedResult)
        {
            // arrange
            MockTagService service = new MockTagService();

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
            A.CallTo(() => fakeRepo.Create(tag)).MustHaveHappened();
            A.CallTo(() => fakeUow.Dispose()).MustHaveHappened();
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
            A.CallTo(() => fakeRepo.DeleteByName("tag")).MustHaveHappened();
            A.CallTo(() => fakeUow.Dispose()).MustHaveHappened();
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
            A.CallTo(() => fakeRepo.ReadAll()).Returns(tags);
            TagService service = new TagService(fakeUowProvider, fakeRepoProvider);

            // act
            var results = await service.GetAll();

            // assert
            Assert.IsTrue(results.ToList().Count == 2);
            Assert.IsTrue(results.ToList().Any(a => a.Id == 1));
            Assert.IsTrue(results.ToList().Any(a => a.Id == 2));
        }

        class MockTagService : TagService
        {
            public override async Task<IEnumerable<Tag>> GetAll()
            {
                List<Tag> tags = new List<Tag>();
                await Task.Run(() =>
                {
                    tags.Add(new Tag
                    {
                        Name="tag1"
                    });

                    tags.Add(new Tag 
                    { 
                        Name="tag2"
                    });
                });

                return tags;
            }//GetAll
        }//class
    }//class
}
