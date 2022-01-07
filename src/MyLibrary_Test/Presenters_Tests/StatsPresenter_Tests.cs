using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using System.Data;
using NUnit;
using NUnit.Framework;
using FakeItEasy;
using MyLibrary.Views;
using MyLibrary.Presenters;
using MyLibrary.Models.Entities;
using MyLibrary.BusinessLogic.Repositories;

namespace MyLibrary_Test.Presenters_Tests
{
    [TestFixture]
    class StatsPresenter_Tests
    {
        [Test]
        public async Task ShowStats_Test()
        {
            // arrange
            var fakeDialog = A.Fake<IShowStats>();
            var fakeBookRepo = A.Fake<BookRepository>();
            A.CallTo(() => fakeBookRepo.GetAll()).Returns(new List<Book> { });
            var fakeMediaItemRepo = A.Fake<MediaItemRepository>();
            A.CallTo(() => fakeMediaItemRepo.GetAll()).Returns(new List<MediaItem> { new MediaItem { Title = "mediaItem" } });
            var fakeTagRepo = A.Fake<TagRepository>();
            A.CallTo(() => fakeTagRepo.GetAll()).Returns(new List<Tag> { new Tag { Name="tag1" }, new Tag { Name = "tag2" } });
            var fakePublisherRepo = A.Fake<PublisherRepository>();
            A.CallTo(() => fakePublisherRepo.GetAll()).Returns(new List<Publisher> { new Publisher { Name = "somePublisher" } });
            var fakeAuthorRepo = A.Fake<AuthorRepository>();
            A.CallTo(() => fakeAuthorRepo.GetAll()).Returns(new List<Author> { new Author { FirstName="John", LastName="Smith" }, new Author { FirstName="Jane", LastName="Doe" } });
            StatsPresenter presenter = new StatsPresenter(fakeDialog,
                fakeBookRepo, fakeMediaItemRepo, fakeTagRepo, fakePublisherRepo, fakeAuthorRepo);
            string expectedText = "Books: 0\r\nPublishers: 1\r\nAuthors: 2\r\n\r\nMedia Items: 1\r\n\r\nTags: 2\r\n";

            // act
            await presenter.ShowStats();

            // assert
            Assert.AreEqual(expectedText, fakeDialog.StatsBoxTest);
            Assert.AreEqual("Ready", fakeDialog.StatusLabelText);
        }
    }//class
}
