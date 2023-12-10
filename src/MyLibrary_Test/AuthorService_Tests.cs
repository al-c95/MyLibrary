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
    public class AuthorService_Tests
    {
        [Test]
        public async Task Add_Test()
        {
            // arrange
            var fakeUowProvider = A.Fake<IUnitOfWorkProvider>();
            var fakeRepoProvider = A.Fake<IAuthorRepositoryProvider>();
            var fakeRepo = A.Fake<IAuthorRepository>();
            var fakeUow = A.Fake<IUnitOfWork>();
            A.CallTo(() => fakeUowProvider.Get()).Returns(fakeUow);
            A.CallTo(() => fakeRepoProvider.Get(fakeUow)).Returns(fakeRepo);
            AuthorService service = new AuthorService(fakeUowProvider, fakeRepoProvider);
            Author author = new Author { FirstName = "John", LastName = "Smith" };

            // act
            await service.Add(author);

            // assert
            A.CallTo(() => fakeRepo.CreateAsync(author)).MustHaveHappened();
            A.CallTo(() => fakeUow.Dispose()).MustHaveHappened();
        }

        [Test]
        public async Task ReadAll_Test()
        {
            // arrange
            var fakeUowProvider = A.Fake<IUnitOfWorkProvider>();
            var fakeRepoProvider = A.Fake<IAuthorRepositoryProvider>();
            var fakeRepo = A.Fake<IAuthorRepository>();
            var fakeUow = A.Fake<IUnitOfWork>();
            A.CallTo(() => fakeUowProvider.Get()).Returns(fakeUow);
            A.CallTo(() => fakeRepoProvider.Get(fakeUow)).Returns(fakeRepo);
            AuthorService service = new AuthorService(fakeUowProvider, fakeRepoProvider);
            List<Author> authors = new List<Author>
            {
                new Author{Id=1, FirstName="John", LastName="Smith" }
            };
            A.CallTo(() => fakeRepo.ReadAllAsync()).Returns(authors);

            // act
            var result = await service.GetAll();

            // assert
            Assert.IsTrue(result.ToList().Count == 1);
            Assert.IsTrue(result.ToList()[0].Id==1);
            Assert.IsTrue(result.ToList()[0].FirstName.Equals("John"));
            Assert.IsTrue(result.ToList()[0].LastName.Equals("Smith"));
            A.CallTo(() => fakeUow.Dispose()).MustHaveHappened();
        }

        [Test]
        public async Task ExistsWithName_Test_Exists()
        {
            // arrange
            var fakeUowProvider = A.Fake<IUnitOfWorkProvider>();
            var fakeRepoProvider = A.Fake<IAuthorRepositoryProvider>();
            var fakeRepo = A.Fake<IAuthorRepository>();
            A.CallTo(() => fakeRepo.AuthorExistsAsync("John", "Smith")).Returns(true);
            var fakeUow = A.Fake<IUnitOfWork>();
            A.CallTo(() => fakeUowProvider.Get()).Returns(fakeUow);
            A.CallTo(() => fakeRepoProvider.Get(fakeUow)).Returns(fakeRepo);
            AuthorService service = new AuthorService(fakeUowProvider, fakeRepoProvider);

            // act
            bool result = await service.ExistsWithName("John", "Smith");

            // assert
            Assert.IsTrue(result);
        }

        [Test]
        public async Task ExistsWithName_Test_DoesNotExist()
        {
            // arrange
            var fakeUowProvider = A.Fake<IUnitOfWorkProvider>();
            var fakeRepoProvider = A.Fake<IAuthorRepositoryProvider>();
            var fakeRepo = A.Fake<IAuthorRepository>();
            A.CallTo(() => fakeRepo.AuthorExistsAsync("John", "Smith")).Returns(false);
            var fakeUow = A.Fake<IUnitOfWork>();
            A.CallTo(() => fakeUowProvider.Get()).Returns(fakeUow);
            A.CallTo(() => fakeRepoProvider.Get(fakeUow)).Returns(fakeRepo);
            AuthorService service = new AuthorService(fakeUowProvider, fakeRepoProvider);

            // act
            bool result = await service.ExistsWithName("John", "Smith");

            // assert
            Assert.IsFalse(result);
        }
    }//class
}