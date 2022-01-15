using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using NUnit;
using NUnit.Framework;
using MyLibrary.Models.Entities;
using MyLibrary.Models.BusinessLogic;

namespace MyLibrary_Test.Models_Tests.Entities_Tests.BusinessLogic_Tests
{
    [TestFixture]
    public class BookService_Tests
    {
        [Test]
        public async Task GetById_Test()
        {
            // arrange
            string expectedTitle = "Test book";
            int id = 1;
            MockBookService service = new MockBookService();

            // act
            Book result = await service.GetById(id);
            string actualTitle = result.Title;

            // assert
            Assert.AreEqual(expectedTitle, actualTitle);
        }

        [Test]
        public async Task GetIdByTitle_Test()
        {
            // arrange
            string title = "Test book";
            int expectedId = 1;
            MockBookService service = new MockBookService();

            // act
            int actualId = await service.GetIdByTitle(title);

            // assert
            Assert.AreEqual(expectedId, actualId);
        }

        [TestCase(1,true)]
        [TestCase(3,false)]
        public async Task ExistsWithId_Test(int id, bool expectedResult)
        {
            // arrange
            MockBookService service = new MockBookService();

            // act
            bool actualResult = await service.ExistsWithId(id);

            // assert
            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestCase("Test book", true)]
        [TestCase("bogus book", false)]
        public async Task ExistsWithTitle_Test(string title, bool expectedResult)
        {
            // arrange
            MockBookService service = new MockBookService();

            // act
            bool actualResult = await service.ExistsWithTitle(title);

            // assert
            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestCase("Test book: this book is a test", true)]
        [TestCase("bogus book", false)]
        public async Task ExistsWithLongTitle_Test(string title, bool expectedResult)
        {
            // arrange
            MockBookService service = new MockBookService();

            // act
            bool actualResult = await service.ExistsWithLongTitle(title);

            // assert
            Assert.AreEqual(expectedResult, actualResult);
        }

        [Test]
        public async Task ExistsWithIsbn_Test_Isbn10Exists()
        {
            // arrange
            MockBookService service = new MockBookService();
            string isbn = "0123456789";

            // act
            bool exists = await service.ExistsWithIsbn(isbn);

            // assert
            Assert.IsTrue(exists);
        }

        [Test]
        public async Task ExistsWithIsbn_Test_Isbn13Exists()
        {
            // arrange
            MockBookService service = new MockBookService();
            string isbn = "2109876543210";

            // act
            bool exists = await service.ExistsWithIsbn(isbn);

            // assert
            Assert.IsTrue(exists);
        }

        [TestCase("0000000000")]
        [TestCase("0000000000000")]
        public async Task ExistsWithIsbn_Test_DoesNotExist(string isbn)
        {
            // arrange
            MockBookService service = new MockBookService();

            // act
            bool exists = await service.ExistsWithIsbn(isbn);

            // assert
            Assert.IsFalse(exists);
        }

        class MockBookService : BookService
        {
            public async override Task<IEnumerable<Book>> GetAll()
            {
                List<Book> books = new List<Book>();
                await Task.Run(() =>
                {
                    books.Add(new Book
                    {
                        Id = 1,
                        Title = "Test book",
                        TitleLong = "Test book: this book is a test",
                        Isbn = "0123456789",
                        Isbn13 = "0123456789012"
                    });

                    books.Add(new Book
                    {
                        Id = 2,
                        Title = "Test book 2",
                        TitleLong = "Test book 2: this book is a test",
                        Isbn = "9876543210",
                        Isbn13 = "2109876543210"
                    });
                });

                return books;
            }
        }//class
    }//class
}
