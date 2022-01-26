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

        [Test]
        public async Task Update_Test()
        {
            // arrange
            var fakeUowProvider = A.Fake<IUnitOfWorkProvider>();
            var fakeUow = A.Fake<IUnitOfWork>();
            A.CallTo(() => fakeUowProvider.Get()).Returns(fakeUow);
            var fakeRepoProvider = A.Fake<IBookRepositoryProvider>();
            var fakeRepo = A.Fake<IBookRepository>();
            A.CallTo(() => fakeRepoProvider.Get(fakeUow)).Returns(fakeRepo);
            var fakeAuthorRepoProvider = A.Fake<IAuthorRepositoryProvider>();
            var fakeTagRepoProvider = A.Fake<ITagRepositoryServiceProvider>();
            var fakePublisherRepoProvider = A.Fake<IPublisherRepositoryProvider>();
            BookService service = new BookService(fakeUowProvider, fakeRepoProvider, fakePublisherRepoProvider, fakeAuthorRepoProvider, fakeTagRepoProvider);
            Book book = new Book { Id = 1, Title = "test_book" };

            // act
            await service.Update(book);

            // assert
            A.CallTo(() => fakeRepo.Update(book)).MustHaveHappened();
            A.CallTo(() => fakeUow.Dispose()).MustHaveHappened();
        }

        [Test]
        public async Task UpdateTags_Test()
        {
            // arrange
            var fakeUowProvider = A.Fake<IUnitOfWorkProvider>();
            var fakeRepoProvider = A.Fake<IBookRepositoryProvider>();
            var fakeTagRepoProvider = A.Fake<ITagRepositoryServiceProvider>();
            var fakePublisherRepoProvider = A.Fake<IPublisherRepositoryProvider>();
            var fakeAuthorRepoProvider = A.Fake<IAuthorRepositoryProvider>();
            var fakeUow = A.Fake<IUnitOfWork>();
            var fakeRepo = A.Fake<IBookRepository>();
            var fakeTagRepo = A.Fake<ITagRepository>();
            A.CallTo(() => fakeUowProvider.Get()).Returns(fakeUow);
            A.CallTo(() => fakeRepoProvider.Get(fakeUow)).Returns(fakeRepo);
            A.CallTo(() => fakeTagRepoProvider.Get(fakeUow)).Returns(fakeTagRepo);
            A.CallTo(() => fakeTagRepo.ExistsWithName("tag1")).Returns(true);
            A.CallTo(() => fakeTagRepo.GetIdByName("tag1")).Returns(1);
            A.CallTo(() => fakeTagRepo.ExistsWithName("tag2")).Returns(true);
            A.CallTo(() => fakeTagRepo.GetIdByName("tag2")).Returns(2);
            A.CallTo(() => fakeTagRepo.ExistsWithName("tag3")).Returns(true);
            A.CallTo(() => fakeTagRepo.GetIdByName("tag3")).Returns(3);
            A.CallTo(() => fakeTagRepo.ExistsWithName("tag4")).Returns(false);
            A.CallTo(() => fakeTagRepo.GetIdByName("tag4")).Returns(4);
            BookService service = new BookService(fakeUowProvider, fakeRepoProvider, fakePublisherRepoProvider, fakeAuthorRepoProvider, fakeTagRepoProvider);
            List<string> originalTags = new List<string> { "tag1", "tag2", "tag3" };
            List<string> selectedTags = new List<string> { "tag2", "tag4" };
            ItemTagsDto itemTags = new ItemTagsDto(1, originalTags, selectedTags);
            Tag newTag = new Tag { Name = "tag4" };

            // act
            await service.UpdateTags(itemTags);

            // assert
            A.CallTo(() => fakeUow.Begin()).MustHaveHappened();
            A.CallTo(() => fakeTagRepo.LinkMediaItem(1, 4));
            A.CallTo(() => fakeTagRepo.UnlinkMediaItem(1, 1));
            A.CallTo(() => fakeUow.Commit()).MustHaveHappened();
        }

        [Test]
        public async Task Add_Test_PublisherAlreadyExists()
        {
            // arrange
            var fakeUowProvider = A.Fake<IUnitOfWorkProvider>();
            var fakeUow = A.Fake<IUnitOfWork>();
            A.CallTo(() => fakeUowProvider.Get()).Returns(fakeUow);
            var fakeRepoProvider = A.Fake<IBookRepositoryProvider>();
            var fakeRepo = A.Fake<IBookRepository>();
            A.CallTo(() => fakeRepo.GetIdByTitle("new_book")).Returns(1);
            A.CallTo(() => fakeRepoProvider.Get(fakeUow)).Returns(fakeRepo);
            var fakeTagRepoProvider = A.Fake<ITagRepositoryServiceProvider>();
            var fakeTagRepo = A.Fake<ITagRepository>();
            A.CallTo(() => fakeTagRepo.ExistsWithName("new_tag")).Returns(false);
            A.CallTo(() => fakeTagRepo.ExistsWithName("existing_tag")).Returns(true);
            A.CallTo(() => fakeTagRepoProvider.Get(fakeUow)).Returns(fakeTagRepo);
            var fakePublisherRepoProvider = A.Fake<IPublisherRepositoryProvider>();
            var fakePublisherRepo = A.Fake<IPublisherRepository>();
            A.CallTo(() => fakePublisherRepo.ExistsWithName("some_publisher")).Returns(true);
            A.CallTo(() => fakePublisherRepo.GetIdByName("some_publisher")).Returns(1);
            A.CallTo(() => fakePublisherRepoProvider.Get(fakeUow)).Returns(fakePublisherRepo);
            var fakeAuthorRepoProvider = A.Fake<IAuthorRepositoryProvider>();
            var fakeAuthorRepo = A.Fake<IAuthorRepository>();
            A.CallTo(() => fakeAuthorRepo.AuthorExists("John", "Smith")).Returns(false);
            A.CallTo(() => fakeAuthorRepo.GetIdByName("John", "Smith")).Returns(2);
            A.CallTo(() => fakeAuthorRepo.AuthorExists("Jane", "Doe")).Returns(true);
            A.CallTo(() => fakeAuthorRepo.GetIdByName("Jane", "Doe")).Returns(1);
            A.CallTo(() => fakeAuthorRepoProvider.Get(fakeUow)).Returns(fakeAuthorRepo);
            Publisher publisher = new Publisher { Name = "some_publisher" };
            BookService service = new BookService(fakeUowProvider, fakeRepoProvider, fakePublisherRepoProvider, fakeAuthorRepoProvider, fakeTagRepoProvider);
            Tag newTag = new Tag { Id = 1, Name = "new_tag" };
            Tag existingTag = new Tag { Id = 2, Name = "existing_tag" };
            Author newAuthor = new Author { Id=2, FirstName = "John", LastName = "Smith" };
            Author existingAuthor = new Author { Id=1, FirstName = "Jane", LastName = "Doe" };
            Book book = new Book
            {
                Id = 1,
                Title = "new_book",
                Publisher = publisher,
                Tags = new List<Tag>
                {
                    newTag,
                    existingTag
                },
                Authors = new List<Author>
                {
                    newAuthor,
                    existingAuthor
                }
            };

            // act
            await service.Add(book);

            // assert
            A.CallTo(() => fakeUow.Begin()).MustHaveHappened();
            Assert.AreEqual(1, book.Publisher.Id);
            A.CallTo(() => fakePublisherRepo.Create(book.Publisher)).MustNotHaveHappened();
            A.CallTo(() => fakeRepo.Create(book)).MustHaveHappened();
            A.CallTo(() => fakeTagRepo.Create(newTag)).MustHaveHappened();
            A.CallTo(() => fakeTagRepo.LinkMediaItem(1, 1));
            A.CallTo(() => fakeTagRepo.LinkMediaItem(1, 2));
            A.CallTo(() => fakeAuthorRepo.Create(newAuthor)).MustHaveHappened();
            A.CallTo(() => fakeAuthorRepo.LinkBook(1, 1)).MustHaveHappened();
            A.CallTo(() => fakeAuthorRepo.LinkBook(1, 2)).MustHaveHappened();
            A.CallTo(() => fakeUow.Commit()).MustHaveHappened();
        }

        [Test]
        public async Task Add_Test_PublisherDoesNotYetExist()
        {
            // arrange
            var fakeUowProvider = A.Fake<IUnitOfWorkProvider>();
            var fakeUow = A.Fake<IUnitOfWork>();
            A.CallTo(() => fakeUowProvider.Get()).Returns(fakeUow);
            var fakeRepoProvider = A.Fake<IBookRepositoryProvider>();
            var fakeRepo = A.Fake<IBookRepository>();
            A.CallTo(() => fakeRepo.GetIdByTitle("new_book")).Returns(1);
            A.CallTo(() => fakeRepoProvider.Get(fakeUow)).Returns(fakeRepo);
            var fakeTagRepoProvider = A.Fake<ITagRepositoryServiceProvider>();
            var fakeTagRepo = A.Fake<ITagRepository>();
            A.CallTo(() => fakeTagRepo.ExistsWithName("new_tag")).Returns(false);
            A.CallTo(() => fakeTagRepo.ExistsWithName("existing_tag")).Returns(true);
            A.CallTo(() => fakeTagRepoProvider.Get(fakeUow)).Returns(fakeTagRepo);
            var fakePublisherRepoProvider = A.Fake<IPublisherRepositoryProvider>();
            var fakePublisherRepo = A.Fake<IPublisherRepository>();
            A.CallTo(() => fakePublisherRepo.ExistsWithName("some_publisher")).Returns(false);
            A.CallTo(() => fakePublisherRepo.GetIdByName("some_publisher")).Returns(1);
            A.CallTo(() => fakePublisherRepoProvider.Get(fakeUow)).Returns(fakePublisherRepo);
            var fakeAuthorRepoProvider = A.Fake<IAuthorRepositoryProvider>();
            var fakeAuthorRepo = A.Fake<IAuthorRepository>();
            A.CallTo(() => fakeAuthorRepo.AuthorExists("John", "Smith")).Returns(false);
            A.CallTo(() => fakeAuthorRepo.GetIdByName("John", "Smith")).Returns(2);
            A.CallTo(() => fakeAuthorRepo.AuthorExists("Jane", "Doe")).Returns(true);
            A.CallTo(() => fakeAuthorRepo.GetIdByName("Jane", "Doe")).Returns(1);
            A.CallTo(() => fakeAuthorRepoProvider.Get(fakeUow)).Returns(fakeAuthorRepo);
            Publisher publisher = new Publisher { Name = "some_publisher" };
            BookService service = new BookService(fakeUowProvider, fakeRepoProvider, fakePublisherRepoProvider, fakeAuthorRepoProvider, fakeTagRepoProvider);
            Tag newTag = new Tag { Id = 1, Name = "new_tag" };
            Tag existingTag = new Tag { Id = 2, Name = "existing_tag" };
            Author newAuthor = new Author { Id = 2, FirstName = "John", LastName = "Smith" };
            Author existingAuthor = new Author { Id = 1, FirstName = "Jane", LastName = "Doe" };
            Book book = new Book
            {
                Id = 1,
                Title = "new_book",
                Publisher = publisher,
                Tags = new List<Tag>
                {
                    newTag,
                    existingTag
                },
                Authors = new List<Author>
                {
                    newAuthor,
                    existingAuthor
                }
            };

            // act
            await service.Add(book);

            // assert
            A.CallTo(() => fakeUow.Begin()).MustHaveHappened();
            Assert.AreEqual(1, book.Publisher.Id);
            A.CallTo(() => fakePublisherRepo.Create(book.Publisher)).MustHaveHappened();
            A.CallTo(() => fakeRepo.Create(book)).MustHaveHappened();
            A.CallTo(() => fakeTagRepo.Create(newTag)).MustHaveHappened();
            A.CallTo(() => fakeTagRepo.LinkMediaItem(1, 1));
            A.CallTo(() => fakeTagRepo.LinkMediaItem(1, 2));
            A.CallTo(() => fakeAuthorRepo.Create(newAuthor)).MustHaveHappened();
            A.CallTo(() => fakeAuthorRepo.LinkBook(1, 1)).MustHaveHappened();
            A.CallTo(() => fakeAuthorRepo.LinkBook(1, 2)).MustHaveHappened();
            A.CallTo(() => fakeUow.Commit()).MustHaveHappened();
        }

        [Test]
        public async Task DeleteById_Test()
        {
            // arrange
            var fakeUowProvider = A.Fake<IUnitOfWorkProvider>();
            var fakeUow = A.Fake<IUnitOfWork>();
            A.CallTo(() => fakeUowProvider.Get()).Returns(fakeUow);
            var fakeRepoProvider = A.Fake<IBookRepositoryProvider>();
            var fakeRepo = A.Fake<IBookRepository>();
            A.CallTo(() => fakeRepoProvider.Get(fakeUow)).Returns(fakeRepo);
            var fakeAuthorRepoProvider = A.Fake<IAuthorRepositoryProvider>();
            var fakeTagRepoProvider = A.Fake<ITagRepositoryServiceProvider>();
            var fakePublisherRepoProvider = A.Fake<IPublisherRepositoryProvider>();
            BookService service = new BookService(fakeUowProvider, fakeRepoProvider, fakePublisherRepoProvider, fakeAuthorRepoProvider, fakeTagRepoProvider);

            // act
            await service.DeleteById(1);

            // assert
            A.CallTo(() => fakeRepo.DeleteById(1)).MustHaveHappened();
            A.CallTo(() => fakeUow.Dispose()).MustHaveHappened();
        }

        [Test]
        public async Task GetAll_Test()
        {
            // arrange
            var fakeUowProvider = A.Fake<IUnitOfWorkProvider>();
            var fakeUow = A.Fake<IUnitOfWork>();
            A.CallTo(() => fakeUowProvider.Get()).Returns(fakeUow);
            var fakeRepoProvider = A.Fake<IBookRepositoryProvider>();
            var fakeRepo = A.Fake<IBookRepository>();
            A.CallTo(() => fakeRepoProvider.Get(fakeUow)).Returns(fakeRepo);
            var fakeAuthorRepoProvider = A.Fake<IAuthorRepositoryProvider>();
            var fakeTagRepoProvider = A.Fake<ITagRepositoryServiceProvider>();
            var fakePublisherRepoProvider = A.Fake<IPublisherRepositoryProvider>();
            BookService service = new BookService(fakeUowProvider, fakeRepoProvider, fakePublisherRepoProvider, fakeAuthorRepoProvider, fakeTagRepoProvider);
            List<Book> books = new List<Book>
            {
                new Book{Id=1, Title="test_book"}
            };
            A.CallTo(() => fakeRepo.ReadAll()).Returns(books);

            // act
            var result = await service.GetAll();

            // assert
            Assert.IsTrue(result.ToList().Count == 1);
            Assert.IsTrue(result.ToList()[0].Id == 1);
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
