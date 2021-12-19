using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using NUnit;
using NUnit.Framework;
using FakeItEasy;
using MyLibrary.BusinessLogic.Repositories;
using MyLibrary.Models.Entities;
using MyLibrary.DataAccessLayer;

namespace MyLibrary_Test.Models_Tests.Repositories_Tests
{
    [TestFixture]
    class BookRepository_Tests
    {
        private BookRepository _repo;

        public BookRepository_Tests()
        {
            
        }

        [TestCase("book1: book number one", true)]
        [TestCase("book2: book number two", false)]
        public async Task ExistsWithLongTitle_Test(string longTitle, bool expectedResult)
        {
            // arrange
            Book book1 = new Book
            {
                Title = "book1",
                TitleLong = "book1: book number one"
            };
            List<Book> books = new List<Book>
            {
                book1
            };
            var fakeDao = A.Fake<ItemDataAccessor<Book>>();
            A.CallTo(() => fakeDao.ReadAll())
                .Returns(books);
            this._repo = new BookRepository(fakeDao);

            // act
            bool actualResult = await this._repo.ExistsWithLongTitle(longTitle);

            // assert
            Assert.AreEqual(expectedResult, actualResult);
        }
    }//class
}
