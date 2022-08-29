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
            MediaItem item1 = new MediaItem 
            { 
                Id = 1, 
                Title = "funny movie on a flash drive",
                Type = ItemType.FlashDrive,
                Number = 0123456789,
                RunningTime = 100,
                ReleaseYear = 1995,
                Notes = "this is a test"
            };
            item1.Tags.Add(new Tag { Name = "tag1" });
            item1.Tags.Add(new Tag { Name = "tag2" });
            List<MediaItem> items = new List<MediaItem>
            {
                item1
            };
            A.CallTo(() => fakeService.GetAllAsync()).Returns(items);
            var fakeExcelFile = A.Fake<IExcelFile>();
            var fakeDialog = A.Fake<IExportDialog>();
            var excel = new MyLibrary.Views.Excel.Excel();
            MockPresenter presenter = new MockPresenter(fakeService, fakeExcelFile, fakeDialog, excel);

            // act
            await presenter.HandleStartButtonClicked(null, null);

            // assert
            Assert.AreEqual("Task complete.", fakeDialog.Label1);
            Assert.AreEqual("1 rows exported", fakeDialog.Label2);
            Assert.AreEqual("MyLibrary", excel.Worksheet.Cells["A1"].GetValue<string>());
            Assert.AreEqual("Type", excel.Worksheet.Cells["A2"].GetValue<string>());
            Assert.AreEqual("Media items", excel.Worksheet.Cells["B2"].GetValue<string>());
            Assert.AreEqual("App Version:", excel.Worksheet.Cells["A3"].GetValue<string>());
            Assert.AreEqual("Extracted At:", excel.Worksheet.Cells["A4"].GetValue<string>());
            Assert.AreEqual("Id", excel.Worksheet.Cells["A6"].GetValue<string>());
            Assert.AreEqual("Title", excel.Worksheet.Cells["B6"].GetValue<string>());
            Assert.AreEqual("Type", excel.Worksheet.Cells["C6"].GetValue<string>());
            Assert.AreEqual("Number", excel.Worksheet.Cells["D6"].GetValue<string>());
            Assert.AreEqual("Running Time", excel.Worksheet.Cells["E6"].GetValue<string>());
            Assert.AreEqual("Release Year", excel.Worksheet.Cells["F6"].GetValue<string>());
            Assert.AreEqual("Tags", excel.Worksheet.Cells["G6"].GetValue<string>());
            Assert.AreEqual("Notes", excel.Worksheet.Cells["H6"].GetValue<string>());
            Assert.AreEqual(1, excel.Worksheet.Cells["A7"].GetValue<int>());
            Assert.AreEqual("funny movie on a flash drive", excel.Worksheet.Cells["B7"].GetValue<string>());
            Assert.AreEqual("Flash Drive", excel.Worksheet.Cells["C7"].GetValue<string>());
            Assert.AreEqual(0123456789, excel.Worksheet.Cells["D7"].GetValue<int>());
            Assert.AreEqual(100, excel.Worksheet.Cells["E7"].GetValue<int>());
            Assert.AreEqual(1995, excel.Worksheet.Cells["F7"].GetValue<int>());
            Assert.AreEqual("tag1, tag2", excel.Worksheet.Cells["G7"].GetValue<string>());
            Assert.AreEqual("this is a test", excel.Worksheet.Cells["H7"].GetValue<string>());
        }

        class MockPresenter : MediaItemExcelPresenter
        {
            public MockPresenter(IMediaItemService itemService, IExcelFile file, IExportDialog dialog, MyLibrary.Views.Excel.Excel excel)
                : base(itemService, file, dialog, excel)
            {

            }
        }//class
    }
}
