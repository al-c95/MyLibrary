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
            var fakeUowProvider = A.Fake<IUnitOfWorkProvider>();
            var fakeUow = A.Fake<IUnitOfWork>();
            A.CallTo(() => fakeUowProvider.Get()).Returns(fakeUow);
            var fakeRepoProvider = A.Fake<IBookRepositoryProvider>();
            var fakeRepo = A.Fake<IBookRepository>();
            Book book = new Book { Id = 1, Title = "test_book" };
            A.CallTo(() => fakeRepo.GetById(1)).Returns(book);
            A.CallTo(() => fakeRepoProvider.Get(fakeUow)).Returns(fakeRepo);
            var fakeAuthorRepoProvider = A.Fake<IAuthorRepositoryProvider>();
            var fakeTagRepoProvider = A.Fake<ITagRepositoryServiceProvider>();
            var fakePublisherRepoProvider = A.Fake<IPublisherRepositoryProvider>();
            BookService service = new BookService(fakeUowProvider, fakeRepoProvider, fakePublisherRepoProvider, fakeAuthorRepoProvider, fakeTagRepoProvider);

            // act
            var result = await service.GetByIdAsync(1);

            // assert
            Assert.AreEqual(1, result.Id);
            Assert.AreEqual("test_book", result.Title);
        }

        [Test]
        public async Task GetIdByTitle_Test()
        {
            // arrange
            var fakeUowProvider = A.Fake<IUnitOfWorkProvider>();
            var fakeUow = A.Fake<IUnitOfWork>();
            A.CallTo(() => fakeUowProvider.Get()).Returns(fakeUow);
            var fakeRepoProvider = A.Fake<IBookRepositoryProvider>();
            var fakeRepo = A.Fake<IBookRepository>();
            A.CallTo(() => fakeRepo.GetIdByTitle("test")).Returns(1);
            A.CallTo(() => fakeRepoProvider.Get(fakeUow)).Returns(fakeRepo);
            var fakeAuthorRepoProvider = A.Fake<IAuthorRepositoryProvider>();
            var fakeTagRepoProvider = A.Fake<ITagRepositoryServiceProvider>();
            var fakePublisherRepoProvider = A.Fake<IPublisherRepositoryProvider>();
            BookService service = new BookService(fakeUowProvider, fakeRepoProvider, fakePublisherRepoProvider, fakeAuthorRepoProvider, fakeTagRepoProvider);

            // act
            var result = await service.GetIdByTitleAsync("test");

            // assert
            Assert.AreEqual(1, result);
        }

        [TestCase(1,true)]
        [TestCase(3,false)]
        public async Task ExistsWithId_Test(int id, bool expectedResult)
        {
            // arrange
            MockBookService service = new MockBookService();

            // act
            bool actualResult = await service.ExistsWithIdAsync(id);

            // assert
            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestCase("Test book", true)]
        [TestCase("bogus book", false)]
        public async Task ExistsWithTitle_Test(string title, bool expectedResult)
        {
            // arrange
            var fakeUowProvider = A.Fake<IUnitOfWorkProvider>();
            var fakeUow = A.Fake<IUnitOfWork>();
            A.CallTo(() => fakeUowProvider.Get()).Returns(fakeUow);
            var fakeRepoProvider = A.Fake<IBookRepositoryProvider>();
            var fakeRepo = A.Fake<IBookRepository>();
            List<string> titles = new List<string>
            {
                "Test book",
                "Test book 2"
            };
            A.CallTo(() => fakeRepo.GetTitles()).Returns(titles);
            A.CallTo(() => fakeRepoProvider.Get(fakeUow)).Returns(fakeRepo);
            var fakeAuthorRepoProvider = A.Fake<IAuthorRepositoryProvider>();
            var fakeTagRepoProvider = A.Fake<ITagRepositoryServiceProvider>();
            var fakePublisherRepoProvider = A.Fake<IPublisherRepositoryProvider>();
            BookService service = new BookService(fakeUowProvider, fakeRepoProvider, fakePublisherRepoProvider, fakeAuthorRepoProvider, fakeTagRepoProvider);

            // act
            bool actualResult = await service.ExistsWithTitleAsync(title);

            // assert
            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestCase("Test book: this book is a test", true)]
        [TestCase("bogus book", false)]
        public async Task ExistsWithLongTitle_Test(string title, bool expectedResult)
        {
            // arrange
            var fakeUowProvider = A.Fake<IUnitOfWorkProvider>();
            var fakeUow = A.Fake<IUnitOfWork>();
            A.CallTo(() => fakeUowProvider.Get()).Returns(fakeUow);
            var fakeRepoProvider = A.Fake<IBookRepositoryProvider>();
            var fakeRepo = A.Fake<IBookRepository>();
            List<string> titles = new List<string>
            {
                "Test book: this book is a test",
                "Test book 2"
            };
            A.CallTo(() => fakeRepo.GetLongTitles()).Returns(titles);
            A.CallTo(() => fakeRepoProvider.Get(fakeUow)).Returns(fakeRepo);
            var fakeAuthorRepoProvider = A.Fake<IAuthorRepositoryProvider>();
            var fakeTagRepoProvider = A.Fake<ITagRepositoryServiceProvider>();
            var fakePublisherRepoProvider = A.Fake<IPublisherRepositoryProvider>();
            BookService service = new BookService(fakeUowProvider, fakeRepoProvider, fakePublisherRepoProvider, fakeAuthorRepoProvider, fakeTagRepoProvider);

            // act
            bool actualResult = await service.ExistsWithLongTitleAsync(title);

            // assert
            Assert.AreEqual(expectedResult, actualResult);
        }

        [Test]
        public async Task ExistsWithIsbn_Test_Isbn10Exists()
        {
            // arrange
            var fakeUowProvider = A.Fake<IUnitOfWorkProvider>();
            var fakeUow = A.Fake<IUnitOfWork>();
            A.CallTo(() => fakeUowProvider.Get()).Returns(fakeUow);
            var fakeRepoProvider = A.Fake<IBookRepositoryProvider>();
            var fakeRepo = A.Fake<IBookRepository>();
            List<string> isbns = new List<string>
            {
                "0123456789"
            };
            A.CallTo(() => fakeRepo.GetIsbns()).Returns(isbns);
            A.CallTo(() => fakeRepoProvider.Get(fakeUow)).Returns(fakeRepo);
            var fakeAuthorRepoProvider = A.Fake<IAuthorRepositoryProvider>();
            var fakeTagRepoProvider = A.Fake<ITagRepositoryServiceProvider>();
            var fakePublisherRepoProvider = A.Fake<IPublisherRepositoryProvider>();
            BookService service = new BookService(fakeUowProvider, fakeRepoProvider, fakePublisherRepoProvider, fakeAuthorRepoProvider, fakeTagRepoProvider);

            // act
            bool exists = await service.ExistsWithIsbnAsync("0123456789");

            // assert
            Assert.IsTrue(exists);
        }

        [Test]
        public async Task ExistsWithIsbn_Test_Isbn13Exists()
        {
            // arrange
            var fakeUowProvider = A.Fake<IUnitOfWorkProvider>();
            var fakeUow = A.Fake<IUnitOfWork>();
            A.CallTo(() => fakeUowProvider.Get()).Returns(fakeUow);
            var fakeRepoProvider = A.Fake<IBookRepositoryProvider>();
            var fakeRepo = A.Fake<IBookRepository>();
            List<string> isbns = new List<string>
            {
                "0123456789012"
            };
            A.CallTo(() => fakeRepo.GetIsbn13s()).Returns(isbns);
            A.CallTo(() => fakeRepoProvider.Get(fakeUow)).Returns(fakeRepo);
            var fakeAuthorRepoProvider = A.Fake<IAuthorRepositoryProvider>();
            var fakeTagRepoProvider = A.Fake<ITagRepositoryServiceProvider>();
            var fakePublisherRepoProvider = A.Fake<IPublisherRepositoryProvider>();
            BookService service = new BookService(fakeUowProvider, fakeRepoProvider, fakePublisherRepoProvider, fakeAuthorRepoProvider, fakeTagRepoProvider);

            // act
            bool exists = await service.ExistsWithIsbnAsync("0123456789012");

            // assert
            Assert.IsTrue(exists);
        }

        [TestCase("0000000000")]
        [TestCase("0000000000000")]
        public async Task ExistsWithIsbn_Test_DoesNotExist(string isbn)
        {
            // arrange
            var fakeUowProvider = A.Fake<IUnitOfWorkProvider>();
            var fakeUow = A.Fake<IUnitOfWork>();
            A.CallTo(() => fakeUowProvider.Get()).Returns(fakeUow);
            var fakeRepoProvider = A.Fake<IBookRepositoryProvider>();
            var fakeRepo = A.Fake<IBookRepository>();
            List<string> isbns = new List<string>
            {
                "0123456789"
            };
            List<string> isbn13s = new List<string>
            {
                "0123456789012"
            };
            A.CallTo(() => fakeRepo.GetIsbns()).Returns(isbns);
            A.CallTo(() => fakeRepo.GetIsbn13s()).Returns(isbn13s);
            A.CallTo(() => fakeRepoProvider.Get(fakeUow)).Returns(fakeRepo);
            var fakeAuthorRepoProvider = A.Fake<IAuthorRepositoryProvider>();
            var fakeTagRepoProvider = A.Fake<ITagRepositoryServiceProvider>();
            var fakePublisherRepoProvider = A.Fake<IPublisherRepositoryProvider>();
            BookService service = new BookService(fakeUowProvider, fakeRepoProvider, fakePublisherRepoProvider, fakeAuthorRepoProvider, fakeTagRepoProvider);

            // act
            bool exists = await service.ExistsWithIsbnAsync("0123456789012");

            // assert
            Assert.IsTrue(exists);
        }

        [TestCase(true)]
        [TestCase(false)]
        public async Task Update_Test(bool updateImage)
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
            await service.UpdateAsync(book, updateImage);

            // assert
            A.CallTo(() => fakeRepo.Update(book, updateImage)).MustHaveHappened();
            A.CallTo(() => fakeUow.Commit()).MustHaveHappened();
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
            await service.UpdateTagsAsync(itemTags);

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
            await service.AddAsync(book);

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
            await service.AddAsync(book);

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
        public async Task AddIfNotExistsAsync_Test_DoesNotExist()
        {
            // arrange
            var fakeUowProvider = A.Fake<IUnitOfWorkProvider>();
            var fakeRepoProvider = A.Fake<IBookRepositoryProvider>();
            var fakeTagRepoProvider = A.Fake<ITagRepositoryServiceProvider>();
            var fakeAuthorRepoProvider = A.Fake<IAuthorRepositoryProvider>();
            var fakePublisherRepoProvider = A.Fake<IPublisherRepositoryProvider>();
            var fakeUow = A.Fake<IUnitOfWork>();
            var fakeRepo = A.Fake<IBookRepository>();
            List<string> titles = new List<string> { "existing_item" };
            A.CallTo(() => fakeRepo.GetTitles()).Returns(titles);
            var fakeTagRepo = A.Fake<ITagRepository>();
            var fakeAuthorRepo = A.Fake<IAuthorRepository>();
            var fakePublisherRepo = A.Fake<IPublisherRepository>();
            A.CallTo(() => fakeAuthorRepoProvider.Get(fakeUow)).Returns(fakeAuthorRepo);
            A.CallTo(() => fakePublisherRepoProvider.Get(fakeUow)).Returns(fakePublisherRepo);
            A.CallTo(() => fakeUowProvider.Get()).Returns(fakeUow);
            A.CallTo(() => fakeRepoProvider.Get(fakeUow)).Returns(fakeRepo);
            A.CallTo(() => fakeTagRepoProvider.Get(fakeUow)).Returns(fakeTagRepo);
            BookService service = new BookService(fakeUowProvider, fakeRepoProvider, fakePublisherRepoProvider, fakeAuthorRepoProvider, fakeTagRepoProvider);
            Book item = new Book
            {
                Title = "new_item",
                TitleLong = "",
                Publisher = new Publisher { Name="some_publisher"},
                Authors = new List<Author> { new Author { FirstName = "John", LastName = "Smith" } }
            };

            // act
            bool result = await service.AddIfNotExistsAsync(item);

            // assert
            Assert.IsTrue(result);
            A.CallTo(() => fakeUow.Begin()).MustHaveHappened();
            A.CallTo(() => fakeRepo.Create(item)).MustHaveHappened();
            A.CallTo(() => fakeUow.Commit()).MustHaveHappened();
        }

        [Test]
        public async Task AddIfNotExistsAsync_Test_Exists()
        {
            // arrange
            var fakeUowProvider = A.Fake<IUnitOfWorkProvider>();
            var fakeRepoProvider = A.Fake<IBookRepositoryProvider>();
            var fakeTagRepoProvider = A.Fake<ITagRepositoryServiceProvider>();
            var fakeAuthorRepoProvider = A.Fake<IAuthorRepositoryProvider>();
            var fakePublisherRepoProvider = A.Fake<IPublisherRepositoryProvider>();
            var fakeUow = A.Fake<IUnitOfWork>();
            var fakeRepo = A.Fake<IBookRepository>();
            List<string> titles = new List<string> { "existing_item" };
            A.CallTo(() => fakeRepo.GetTitles()).Returns(titles);
            var fakeTagRepo = A.Fake<ITagRepository>();
            var fakeAuthorRepo = A.Fake<IAuthorRepository>();
            var fakePublisherRepo = A.Fake<IPublisherRepository>();
            A.CallTo(() => fakeAuthorRepoProvider.Get(fakeUow)).Returns(fakeAuthorRepo);
            A.CallTo(() => fakePublisherRepoProvider.Get(fakeUow)).Returns(fakePublisherRepo);
            A.CallTo(() => fakeUowProvider.Get()).Returns(fakeUow);
            A.CallTo(() => fakeRepoProvider.Get(fakeUow)).Returns(fakeRepo);
            A.CallTo(() => fakeTagRepoProvider.Get(fakeUow)).Returns(fakeTagRepo);
            BookService service = new BookService(fakeUowProvider, fakeRepoProvider, fakePublisherRepoProvider, fakeAuthorRepoProvider, fakeTagRepoProvider);
            Book item = new Book
            {
                Title = "existing_item",
                TitleLong = ""
            };

            // act
            bool result = await service.AddIfNotExistsAsync(item);

            // assert
            Assert.IsFalse(result);
            A.CallTo(() => fakeUow.Begin()).MustNotHaveHappened();
            A.CallTo(() => fakeRepo.Create(item)).MustNotHaveHappened();
            A.CallTo(() => fakeUow.Commit()).MustNotHaveHappened();
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
            await service.DeleteByIdAsync(1);

            // assert
            A.CallTo(() => fakeRepo.DeleteById(1)).MustHaveHappened();
            A.CallTo(() => fakeUow.Commit()).MustHaveHappened();
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
            var result = await service.GetAllAsync();

            // assert
            Assert.IsTrue(result.ToList().Count == 1);
            Assert.IsTrue(result.ToList()[0].Id == 1);
        }

        class MockBookService : BookService
        {
            public async override Task<IEnumerable<Book>> GetAllAsync()
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
