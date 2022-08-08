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
using MyLibrary.Models.BusinessLogic.ImportExcel;
using MyLibrary.DataAccessLayer;
using MyLibrary.DataAccessLayer.Repositories;
using MyLibrary.DataAccessLayer.ServiceProviders;

namespace MyLibrary_Test.Models_Tests.BusinessLogic_Tests.ImportExcel_Tests
{
    [TestFixture]
    class MediaItemImportExcelService_Tests
    {
        [TestCase("1.3.0")]
        [TestCase("1.4.0")]
        [TestCase("2.0.0")]
        public void Constructor_Test_Ok(string excelVersionEntry)
        {
            // arrange
            var pck = Utils.WorksheetFactory(excelVersionEntry, "Media item");
            pck.Workbook.Worksheets["Media item"].Cells["B2"].Value = "Media items";
            pck.Workbook.Worksheets["Media item"].Cells["A6"].Value = "Id";
            pck.Workbook.Worksheets["Media item"].Cells["B6"].Value = "Title";
            pck.Workbook.Worksheets["Media item"].Cells["C6"].Value = "Type";
            pck.Workbook.Worksheets["Media item"].Cells["D6"].Value = "Number";
            pck.Workbook.Worksheets["Media item"].Cells["E6"].Value = "Running Time";
            pck.Workbook.Worksheets["Media item"].Cells["F6"].Value = "Release Year";
            pck.Workbook.Worksheets["Media item"].Cells["G6"].Value = "Tags";
            pck.Workbook.Worksheets["Media item"].Cells["H6"].Value = "Notes";
            var fakeUowProvider = A.Fake<IUnitOfWorkProvider>();
            var fakeUow = A.Fake<IUnitOfWork>();
            A.CallTo(() => fakeUowProvider.Get()).Returns(fakeUow);

            // act/assert
            Assert.DoesNotThrow(() => new MediaItemImportExcelService(pck, new MyLibrary.Models.ValueObjects.AppVersion(1, 3, 0), fakeUowProvider));
        }
    }
}
