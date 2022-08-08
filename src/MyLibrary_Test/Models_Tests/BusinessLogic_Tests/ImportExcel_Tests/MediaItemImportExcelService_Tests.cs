using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using NUnit;
using NUnit.Framework;
using FakeItEasy;
using OfficeOpenXml;
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
        private ExcelPackage AddWorksheetHeaders(ExcelPackage pck)
        {
            pck.Workbook.Worksheets["Media item"].Cells["B2"].Value = "Media items";
            pck.Workbook.Worksheets["Media item"].Cells["A6"].Value = "Id";
            pck.Workbook.Worksheets["Media item"].Cells["B6"].Value = "Title";
            pck.Workbook.Worksheets["Media item"].Cells["C6"].Value = "Type";
            pck.Workbook.Worksheets["Media item"].Cells["D6"].Value = "Number";
            pck.Workbook.Worksheets["Media item"].Cells["E6"].Value = "Running Time";
            pck.Workbook.Worksheets["Media item"].Cells["F6"].Value = "Release Year";
            pck.Workbook.Worksheets["Media item"].Cells["G6"].Value = "Tags";
            pck.Workbook.Worksheets["Media item"].Cells["H6"].Value = "Notes";

            return pck;
        }

        private ExcelPackage AddBogusWorksheetHeaders(ExcelPackage pck,
            string B2, string A6, string B6, string C6, string D6, string E6, string F6, string G6, string H6)
        {
            pck.Workbook.Worksheets["Media item"].Cells["B2"].Value = B2;
            pck.Workbook.Worksheets["Media item"].Cells["A6"].Value = A6;
            pck.Workbook.Worksheets["Media item"].Cells["B6"].Value = B6;
            pck.Workbook.Worksheets["Media item"].Cells["C6"].Value = C6;
            pck.Workbook.Worksheets["Media item"].Cells["D6"].Value = D6;
            pck.Workbook.Worksheets["Media item"].Cells["E6"].Value = E6;
            pck.Workbook.Worksheets["Media item"].Cells["F6"].Value = F6;
            pck.Workbook.Worksheets["Media item"].Cells["G6"].Value = G6;
            pck.Workbook.Worksheets["Media item"].Cells["H6"].Value = H6;

            return pck;
        }

        [TestCase("1.3.0")]
        [TestCase("1.4.0")]
        [TestCase("2.0.0")]
        public void Constructor_Test_Ok(string excelVersionEntry)
        {
            // arrange
            var pck = Utils.WorksheetFactory(excelVersionEntry, "Media item");
            pck = AddWorksheetHeaders(pck);
            var fakeUowProvider = A.Fake<IUnitOfWorkProvider>();
            var fakeUow = A.Fake<IUnitOfWork>();
            A.CallTo(() => fakeUowProvider.Get()).Returns(fakeUow);

            // act/assert
            Assert.DoesNotThrow(() => new MediaItemImportExcelService(pck, new MyLibrary.Models.ValueObjects.AppVersion(1, 3, 0), fakeUowProvider));
        }

        [TestCase("1.2.0")]
        [TestCase("2.1.0")]
        public void Constructor_Test_VersionMismatch(string excelVersionEntry)
        {
            // arrange
            var pck = Utils.WorksheetFactory(excelVersionEntry, "Media item");
            pck = AddWorksheetHeaders(pck);
            var fakeUowProvider = A.Fake<IUnitOfWorkProvider>();
            var fakeUow = A.Fake<IUnitOfWork>();
            A.CallTo(() => fakeUowProvider.Get()).Returns(fakeUow);

            // act/assert
            Assert.Throws<FormatException>(() => new MediaItemImportExcelService(pck, new MyLibrary.Models.ValueObjects.AppVersion(1, 3, 0), fakeUowProvider));
        }

        [TestCase("bogus", "Id", "Title", "Type", "Number", "Running Time", "Release Year", "Tags", "Notes")]
        [TestCase("bogus", "bogus", "Title", "Type", "Number", "Running Time", "Release Year", "Tags", "Notes")]
        [TestCase("bogus", "bogus", "bogus", "Type", "Number", "Running Time", "Release Year", "Tags", "Notes")]
        [TestCase("bogus", "bogus", "bogus", "bogus", "Number", "Running Time", "Release Year", "Tags", "Notes")]
        [TestCase("bogus", "bogus", "bogus", "bogus", "bogus", "Running Time", "Release Year", "Tags", "Notes")]
        [TestCase("bogus", "bogus", "bogus", "bogus", "bogus", "bogus", "Release Year", "Tags", "Notes")]
        [TestCase("bogus", "bogus", "bogus", "bogus", "bogus", "bogus", "bogus", "Tags", "Notes")]
        [TestCase("bogus", "bogus", "bogus", "bogus", "bogus", "bogus", "bogus", "bogus", "Notes")]
        [TestCase("bogus", "bogus", "bogus", "bogus", "bogus", "bogus", "bogus", "bogus", "bogus")]
        public void Constructor_Test_InvalidFormat(string B2, string A6, string B6, string C6, string D6, string E6, string F6, string G6, string H6)
        {
            // arrange
            var pck = Utils.WorksheetFactory("1.4.0", "Media item");
            pck = AddBogusWorksheetHeaders(pck,B2,A6,B6,C6,D6,E6,F6,G6,H6);
            var fakeUowProvider = A.Fake<IUnitOfWorkProvider>();
            var fakeUow = A.Fake<IUnitOfWork>();
            A.CallTo(() => fakeUowProvider.Get()).Returns(fakeUow);

            // act/assert
            Assert.Throws<FormatException>(() => new MediaItemImportExcelService(pck, new MyLibrary.Models.ValueObjects.AppVersion(1, 3, 0), fakeUowProvider));
        }
    }//class
}
