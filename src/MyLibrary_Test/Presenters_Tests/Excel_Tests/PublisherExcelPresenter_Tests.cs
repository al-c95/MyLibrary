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
            Publisher publisher2 = new Publisher { Id = 2, Name="publisher2" };
            List<Publisher> publishers = new List<Publisher>
            {
                publisher1,
                publisher2
            };
            A.CallTo(() => fakeService.GetAll()).Returns(publishers);
            var fakeExcelFile = A.Fake<IExcelFile>();
            var fakeDialog = A.Fake<IExportDialog>();
            var excel = new MyLibrary.Views.Excel.Excel();
            MockPresenter presenter = new MockPresenter(fakeService, fakeExcelFile, fakeDialog, excel);

            // act
            await presenter.HandleStartButtonClicked(null, null);

            // assert
            Assert.AreEqual("Task complete.", fakeDialog.Label1);
            Assert.AreEqual("2 rows exported", fakeDialog.Label2);
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