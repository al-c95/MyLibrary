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
    class BookImportExcelService_Tests
    {
        [TestCase("1.3.0")]
        [TestCase("1.4.0")]
        [TestCase("2.0.0")]
        public void Constructor_Test_Ok(string excelVersionEntry)
        {
            // arrange
            var pck = Utils.WorksheetFactory(excelVersionEntry, "Book");
            pck.Workbook.Worksheets["Book"].Cells["B2"].Value = "Books";
            pck.Workbook.Worksheets["Book"].Cells["A6"].Value = "Id";
            pck.Workbook.Worksheets["Book"].Cells["B6"].Value = "Title";
            pck.Workbook.Worksheets["Book"].Cells["C6"].Value = "Long Title";
            pck.Workbook.Worksheets["Book"].Cells["D6"].Value = "ISBN";
            pck.Workbook.Worksheets["Book"].Cells["E6"].Value = "ISBN13";
            pck.Workbook.Worksheets["Book"].Cells["F6"].Value = "Authors";
            pck.Workbook.Worksheets["Book"].Cells["G6"].Value = "Language";
            pck.Workbook.Worksheets["Book"].Cells["H6"].Value = "Tags";
            pck.Workbook.Worksheets["Book"].Cells["I6"].Value = "Dewey Decimal";
            pck.Workbook.Worksheets["Book"].Cells["J6"].Value = "MSRP";
            pck.Workbook.Worksheets["Book"].Cells["K6"].Value = "Publisher";
            pck.Workbook.Worksheets["Book"].Cells["L6"].Value = "Format";
            pck.Workbook.Worksheets["Book"].Cells["M6"].Value = "Date Published";
            pck.Workbook.Worksheets["Book"].Cells["N6"].Value = "Place of Publication";
            pck.Workbook.Worksheets["Book"].Cells["O6"].Value = "Edition";
            pck.Workbook.Worksheets["Book"].Cells["P6"].Value = "Pages";
            pck.Workbook.Worksheets["Book"].Cells["Q6"].Value = "Dimensions";
            pck.Workbook.Worksheets["Book"].Cells["R6"].Value = "Overview";
            pck.Workbook.Worksheets["Book"].Cells["S6"].Value = "Excerpt";
            pck.Workbook.Worksheets["Book"].Cells["T6"].Value = "Synopsys";
            pck.Workbook.Worksheets["Book"].Cells["U6"].Value = "Notes";
            var fakeUowProvider = A.Fake<IUnitOfWorkProvider>();
            var fakeUow = A.Fake<IUnitOfWork>();
            A.CallTo(() => fakeUowProvider.Get()).Returns(fakeUow);

            // act/assert
            Assert.DoesNotThrow(() => new BookImportExcelService(pck, new MyLibrary.Models.ValueObjects.AppVersion(1, 3, 0), fakeUowProvider));
        }
    }//class
}
