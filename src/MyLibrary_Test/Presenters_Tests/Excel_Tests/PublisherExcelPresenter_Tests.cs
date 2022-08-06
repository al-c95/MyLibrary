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
    [TestFixture]
    class PublisherExcelPresenter_Tests
    {
        [Test]
        public async Task HandleStartButtonClicked_Test_Success()
        {
            // arrange
            var fakeService = A.Fake<IPublisherService>();
            Publisher publisher1 = new Publisher { Id = 1, Name="publisher1" };
            List<Publisher> publishers = new List<Publisher>
            {
                publisher1
            };
            A.CallTo(() => fakeService.GetAll()).Returns(publishers);
            var fakeExcelFile = A.Fake<IExcelFile>();
            var fakeDialog = A.Fake<IExportDialog>();
            var excel = new MyLibrary.Views.Excel.Excel();
            MockPresenter presenter = new MockPresenter(fakeService, fakeExcelFile, fakeDialog, excel);

            // act
            await presenter.HandleStartButtonClicked(null, null);

            // assert
            // dialog labels
            Assert.AreEqual("Task complete.", fakeDialog.Label1);
            Assert.AreEqual("1 rows exported", fakeDialog.Label2);
            // worksheet
            Assert.AreEqual("MyLibrary", excel.Worksheet.Cells["A1"].GetValue<string>());
            Assert.AreEqual("Type", excel.Worksheet.Cells["A2"].GetValue<string>());
            Assert.AreEqual("Publishers", excel.Worksheet.Cells["B2"].GetValue<string>());
            Assert.AreEqual("App Version:", excel.Worksheet.Cells["A3"].GetValue<string>());
            Assert.AreEqual("Extracted At:", excel.Worksheet.Cells["A4"].GetValue<string>());
            Assert.AreEqual("Id", excel.Worksheet.Cells["A6"].GetValue<string>());
            Assert.AreEqual("Publisher", excel.Worksheet.Cells["B6"].GetValue<string>());
            Assert.AreEqual(1, excel.Worksheet.Cells["A7"].GetValue<int>());
            Assert.AreEqual("publisher1", excel.Worksheet.Cells["B7"].GetValue<string>());
        }

        class MockPresenter : PublisherExcelPresenter
        {
            public MockPresenter(IPublisherService publisherService, IExcelFile file, IExportDialog dialog, MyLibrary.Views.Excel.Excel excel)
                : base(publisherService, file, dialog,excel)
            {

            }
        }//class
    }//class
}