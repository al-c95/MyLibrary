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
    public class BookCopyService_Tests
    {
        [Test]
        public async Task Create_Test()
        {
            // arrange
            var fakeUowProvider = A.Fake<IUnitOfWorkProvider>();
            var fakeRepoProvider = A.Fake<IBookCopyRepositoryProvider>();
            var fakeRepo = A.Fake<IBookCopyRepository>();
            var fakeUow = A.Fake<IUnitOfWork>();
            A.CallTo(() => fakeUowProvider.Get()).Returns(fakeUow);
            A.CallTo(() => fakeRepoProvider.Get(fakeUow)).Returns(fakeRepo);
            BookCopyService service = new BookCopyService(fakeUowProvider, fakeRepoProvider);
            BookCopy copy = new BookCopy { Id = 1, BookId = 1, Description = "copy1", Notes = "copy1" };

            // act
            await service.Create(copy);

            // assert
            A.CallTo(() => fakeRepo.Create(copy)).MustHaveHappened();
            A.CallTo(() => fakeUow.Dispose()).MustHaveHappened();
        }

        [Test]
        public async Task ReadAll_Test()
        {
            // arrange
            var fakeUowProvider = A.Fake<IUnitOfWorkProvider>();
            var fakeRepoProvider = A.Fake<IBookCopyRepositoryProvider>();
            var fakeRepo = A.Fake<IBookCopyRepository>();
            var fakeUow = A.Fake<IUnitOfWork>();
            A.CallTo(() => fakeUowProvider.Get()).Returns(fakeUow);
            A.CallTo(() => fakeRepoProvider.Get(fakeUow)).Returns(fakeRepo);
            BookCopyService service = new BookCopyService(fakeUowProvider, fakeRepoProvider);
            List<BookCopy> copies = new List<BookCopy>
            {
                new BookCopy{Id=1, BookId=1, Description="copy", Notes="copy"}
            };
            A.CallTo(() => fakeRepo.ReadAll()).Returns(copies);

            // act
            var result = await service.GetAll();

            // assert
            Assert.IsTrue(result.ToList().Count == 1);
            Assert.IsTrue(result.ToList()[0].Id == 1);
            Assert.IsTrue(result.ToList()[0].BookId == 1);
            Assert.IsTrue(result.ToList()[0].Description.Equals("copy"));
            Assert.IsTrue(result.ToList()[0].Notes.Equals("copy"));
            A.CallTo(() => fakeUow.Dispose()).MustHaveHappened();
        }

        [Test]
        public async Task GetByItemId_Test()
        {
            // arrange
            MockService service = new MockService();

            // act
            var result = await service.GetByItemId(2);

            // assert
            Assert.IsTrue(result.ToList().Count == 1);
            Assert.IsTrue(result.ToList()[0].Id == 2);
        }

        [Test]
        public async Task DeleteById_Test()
        {
            // arrange
            var fakeUowProvider = A.Fake<IUnitOfWorkProvider>();
            var fakeRepoProvider = A.Fake<IBookCopyRepositoryProvider>();
            var fakeRepo = A.Fake<IBookCopyRepository>();
            var fakeUow = A.Fake<IUnitOfWork>();
            A.CallTo(() => fakeUowProvider.Get()).Returns(fakeUow);
            A.CallTo(() => fakeRepoProvider.Get(fakeUow)).Returns(fakeRepo);
            BookCopyService service = new BookCopyService(fakeUowProvider, fakeRepoProvider);

            // act
            await service.DeleteById(1);

            // assert
            A.CallTo(() => fakeRepo.DeleteById(1)).MustHaveHappened();
            A.CallTo(() => fakeUow.Dispose()).MustHaveHappened();
        }

        [Test]
        public async Task Update_Test()
        {
            // arrange
            var fakeUowProvider = A.Fake<IUnitOfWorkProvider>();
            var fakeRepoProvider = A.Fake<IBookCopyRepositoryProvider>();
            var fakeRepo = A.Fake<IBookCopyRepository>();
            var fakeUow = A.Fake<IUnitOfWork>();
            A.CallTo(() => fakeUowProvider.Get()).Returns(fakeUow);
            A.CallTo(() => fakeRepoProvider.Get(fakeUow)).Returns(fakeRepo);
            BookCopyService service = new BookCopyService(fakeUowProvider, fakeRepoProvider);
            BookCopy copy = new BookCopy { Id = 1, BookId = 1, Description = "copy", Notes = "copy" };

            // act
            await service.Update(copy);

            // assert
            A.CallTo(() => fakeRepo.Update(copy)).MustHaveHappened();
            A.CallTo(() => fakeUow.Dispose()).MustHaveHappened();
        }

        [TestCase("copy1", true)]
        [TestCase("copy3", false)]
        public async Task ExistsWithDescription_Test(string description, bool expectedResult)
        {
            // arrange
            MockService service = new MockService();

            // act
            bool actualResult = await service.ExistsWithDescription(description);

            // assert
            Assert.AreEqual(expectedResult, actualResult);
        }

        class MockService : BookCopyService
        {
            public async override Task<IEnumerable<BookCopy>> GetAll()
            {
                List<BookCopy> copies = new List<BookCopy>();
                await Task.Run(() =>
                {
                    copies.Add(new BookCopy
                    {
                        Id=1,
                        BookId=1,
                        Description="copy1",
                        Notes="copy1"
                    });

                    copies.Add(new BookCopy
                    {
                        Id=2,
                        BookId=2,
                        Description="copy2",
                        Notes="copy2"
                    });
                });

                return copies;
            }
        }//class
    }//class
}
