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
using MyLibrary.Models.BusinessLogic;

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
            var fakeBookRepo = A.Fake<IBookService>();
            A.CallTo(() => fakeBookRepo.GetAll()).Returns(new List<Book> { });
            var fakeMediaItemService = A.Fake<IMediaItemService>();
            A.CallTo(() => fakeMediaItemService.GetAllAsync()).Returns(new List<MediaItem> { new MediaItem { Title = "mediaItem" } });
            var fakeTagService = A.Fake<ITagService>();
            A.CallTo(() => fakeTagService.GetAll()).Returns(new List<Tag> { new Tag { Name="tag1" }, new Tag { Name = "tag2" } });
            var fakePublisherService = A.Fake<IPublisherService>();
            A.CallTo(() => fakePublisherService.GetAll()).Returns(new List<Publisher> { new Publisher { Name = "somePublisher" } });
            var fakeAuthorService = A.Fake<IAuthorService>();
            A.CallTo(() => fakeAuthorService.GetAll()).Returns(new List<Author> { new Author { FirstName="John", LastName="Smith" }, new Author { FirstName="Jane", LastName="Doe" } });
            StatsPresenter presenter = new StatsPresenter(fakeDialog,
                fakeBookRepo, fakeMediaItemService, fakeTagService, fakePublisherService, fakeAuthorService);
            string expectedText = "Books: 0\r\nPublishers: 1\r\nAuthors: 2\r\n\r\nMedia Items: 1\r\n\r\nTags: 2\r\n";

            // act
            await presenter.ShowStats();

            // assert
            Assert.AreEqual(expectedText, fakeDialog.StatsBoxTest);
            Assert.AreEqual("Ready", fakeDialog.StatusLabelText);
        }
    }//class
}
