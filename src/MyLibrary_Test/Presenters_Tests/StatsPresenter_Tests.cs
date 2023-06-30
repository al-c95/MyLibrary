using System.Collections.Generic;
using System.Threading.Tasks;
using NUnit.Framework;
using FakeItEasy;
using MyLibrary.Views;
using MyLibrary.Presenters;
using MyLibrary.DataAccessLayer;

namespace MyLibrary_Test.Presenters_Tests
{
    [TestFixture]
    class StatsPresenter_Tests
    {
        [Test]
        public async Task LoadDataAsync_Test()
        {
            // arrange
            var fakeDialog = A.Fake<IShowStats>();
            var fakeStatsService = A.Fake<IStatsService>();
            A.CallTo(() => fakeStatsService.GetBooksCountAsync()).Returns(0);
            A.CallTo(() => fakeStatsService.GetPublishersCountAsync()).Returns(1);
            A.CallTo(() => fakeStatsService.GetAuthorsCountAsync()).Returns(2);
            A.CallTo(() => fakeStatsService.GetMediaItemsCountAsync()).Returns(1);
            A.CallTo(() => fakeStatsService.GetTagsCountAsync()).Returns(2);
            StatsPresenter presenter = new StatsPresenter(fakeDialog, fakeStatsService);
            string expectedText = "Books: 0\r\nPublishers: 1\r\nAuthors: 2\r\n\r\nMedia Items: 1\r\n\r\nTags: 2\r\n";

            // act
            await presenter.LoadDataAsync();

            // assert
            Assert.AreEqual(expectedText, fakeDialog.StatsBoxTest);
            Assert.AreEqual("Ready", fakeDialog.StatusLabelText);
        }
    }//class
}
