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
    class WishlistExcelPresenter_Tests
    {
        [Test]
        public async Task HandleStartButtonClicked_Test_Success()
        {
            // arrange
            var fakeService = A.Fake<IWishlistService>();
            WishlistItem item1 = new WishlistItem { Id = 1 };
            WishlistItem item2 = new WishlistItem { Id = 2 };
            List<WishlistItem> items = new List<WishlistItem>
            {
                item1,
                item2
            };
            A.CallTo(() => fakeService.GetAll()).Returns(items);
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

        class MockPresenter : WishlistExcelPresenter
        {
            public MockPresenter(IWishlistService wishlistService, IExcelFile file, IExportDialog dialog, MyLibrary.Views.Excel.Excel excel)
                : base(wishlistService, file, dialog, excel)
            {

            }
        }//class
    }//class
}
