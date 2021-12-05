using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using NUnit;
using NUnit.Framework;
using FakeItEasy;
using MyLibrary.BusinessLogic;
using MyLibrary.Models.Entities;
using MyLibrary.DataAccessLayer;

namespace MyLibrary_Test.Models_Tests
{
    [TestFixture]
    public class BookRepository_Tests
    {
        private BookRepository _repo;

        public BookRepository_Tests()
        {
            // arrange
            //var fakeDao = A.Fake<IBookDataAccessor>();
            var fakeDao = A.Fake<ItemDataAccessor<Book>>();
            Book book1 = new Book();
            book1.Id = 1;
            book1.Title = "Book 1";
            Book book2 = new Book();
            book2.Id = 2;
            book2.Title = "Book 2";
            List<Book> books = new List<Book>
            {
                book1,
                book2
            };
            A.CallTo(() => fakeDao.ReadAll())
                .Returns(books);
            this._repo = new BookRepository(fakeDao);
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
            Assert.IsTrue(result.Title == "Book 2");
        }

        [Test]
        public async Task GetById_Test_DoesNotExist()
        {
            // act
            var result = await _repo.GetById(3);

            // assert
            Assert.IsNull(result);
        }

        [TestCase(1,true)]
        [TestCase(3,false)]
        public async Task ItemWithIdExists_Test(int id, bool expectedResult)
        {
            // act
            var actualResult = await _repo.ItemWithIdExists(id);

            // assert
            Assert.AreEqual(expectedResult, actualResult);
        }
    }
}
