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

        [Test]
        public async Task ExistsWithTitle_Test_ExistsTitleOnly()
        {
            // arrange
            string title = "test";
            Book item3 = new Book();
            item3.Id = 3;
            item3.Title = "test";
            item3.TitleLong = ".";
            List<Book> items = new List<Book>
            {
                item3
            };
            var fakeDao = A.Fake<ItemDataAccessor<Book>>();
            A.CallTo(() => fakeDao.ReadAll())
                .Returns(items);
            this._repo = new BookRepository(fakeDao);

            // act
            var result = await this._repo.ExistsWithTitle("test");

            // assert
            Assert.IsTrue(result);
        }

        [Test]
        public async Task ExistsWithTitle_Test_ExistsTitleAndLongTitle()
        {
            // arrange
            string title = "test";
            Book item3 = new Book();
            item3.Id = 3;
            item3.Title = "test";
            item3.TitleLong = "test";
            List<Book> items = new List<Book>
            {
                item3
            };
            var fakeDao = A.Fake<ItemDataAccessor<Book>>();
            A.CallTo(() => fakeDao.ReadAll())
                .Returns(items);
            this._repo = new BookRepository(fakeDao);

            // act
            var result = await this._repo.ExistsWithTitle("test");

            // assert
            Assert.IsTrue(result);
        }

        [Test]
        public async Task ExistsWithTitle_Test_ExistsLongTitleOnly()
        {
            // arrange
            string title = "test";
            Book item3 = new Book();
            item3.Id = 3;
            item3.Title = ".";
            item3.TitleLong = "test";
            List<Book> items = new List<Book>
            {
                item3
            };
            var fakeDao = A.Fake<ItemDataAccessor<Book>>();
            A.CallTo(() => fakeDao.ReadAll())
                .Returns(items);
            this._repo = new BookRepository(fakeDao);

            // act
            var result = await this._repo.ExistsWithTitle("test");

            // assert
            Assert.IsTrue(result);
        }

        [Test]
        public async Task ExistsWithTitle_Test_DoesNotExist()
        {
            // arrange
            string title = "test";
            Book item3 = new Book();
            item3.Id = 3;
            item3.Title = ".";
            item3.TitleLong = ".";
            List<Book> items = new List<Book>
            {
                item3
            };
            var fakeDao = A.Fake<ItemDataAccessor<Book>>();
            A.CallTo(() => fakeDao.ReadAll())
                .Returns(items);
            this._repo = new BookRepository(fakeDao);

            // act
            var result = await this._repo.ExistsWithTitle("test");

            // assert
            Assert.IsFalse(result);
        }
    }//class
}
