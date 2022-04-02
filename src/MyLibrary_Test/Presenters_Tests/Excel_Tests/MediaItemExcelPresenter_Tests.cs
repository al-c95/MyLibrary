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
using OfficeOpenXml;
using MyLibrary.Models.BusinessLogic;
using MyLibrary.Models.Entities;
using MyLibrary.DataAccessLayer;
using MyLibrary.Views;
using MyLibrary.Views.Excel;
using MyLibrary.Presenters.Excel;

namespace MyLibrary_Test.Presenters_Tests.Excel_Tests
{
    class MediaItemExcelPresenter_Tests
    {
        [Test]
        public async Task HandleStartButtonClicked_Test_Success()
        {
            // arrange
            var fakeService = A.Fake<IMediaItemService>();
            MediaItem item1 = new MediaItem { Id = 1, Title = "item1" };
            MediaItem item2 = new MediaItem { Id = 2, Title = "item2" };
            List<MediaItem> items = new List<MediaItem>
            {
                item1,
                item2
            };
            A.CallTo(() => fakeService.GetAll()).Returns(items);
            var fakeExcelFile = A.Fake<IExcelFile>();
            var fakeDialog = A.Fake<IExportDialog>();
            MockPresenter presenter = new MockPresenter(fakeService, fakeExcelFile, fakeDialog);

            // act
            await presenter.HandleStartButtonClicked(null, null);

            // assert
            Assert.AreEqual("Task complete.", fakeDialog.Label1);
            //Assert.AreEqual("2 rows exported", fakeDialog.Label2);
        }

        class MockPresenter : MediaItemExcelPresenter
        {
            public MockPresenter(IMediaItemService itemService, IExcelFile file, IExportDialog dialog)
                : base(itemService, file, dialog)
            {

            }
        }//class
    }
}
