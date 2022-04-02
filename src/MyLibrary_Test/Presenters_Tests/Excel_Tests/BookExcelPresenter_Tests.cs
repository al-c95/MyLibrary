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
    class BookExcelPresenter_Tests
    {
        [Test]
        public async Task HandleStartButtonClicked_Test_Success()
        {
            // arrange
            var fakeService = A.Fake<IBookService>();
            Book item1 = new Book { Id = 1, Title="book1" };
            Book item2 = new Book { Id = 2, Title="book2" };
            List<Book> items = new List<Book>
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

        class MockPresenter : BookExcelPresenter
        {
            public MockPresenter(IBookService bookService, IExcelFile file, IExportDialog dialog)
                : base(bookService, file, dialog)
            {

            }
        }//class
    }//class
}
