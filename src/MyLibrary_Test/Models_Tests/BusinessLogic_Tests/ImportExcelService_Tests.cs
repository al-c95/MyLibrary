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
using MyLibrary.Models.BusinessLogic;
using MyLibrary.Models.BusinessLogic.ImportExcel;
using MyLibrary.DataAccessLayer;
using MyLibrary.DataAccessLayer.Repositories;
using MyLibrary.DataAccessLayer.ServiceProviders;

namespace MyLibrary_Test.Models_Tests.BusinessLogic_Tests
{
    [TestFixture]
    class ImportExcelService_Tests
    {
        private ExcelPackage WorksheetFactory(string typeEntry, string versionEntry)
        {
            ExcelPackage pck = new ExcelPackage();
            ExcelWorksheet ws = pck.Workbook.Worksheets.Add(typeEntry);
            ws.Cells["A1"].Value = "MyLibrary";
            ws.Cells["A2"].Value = "Type";
            ws.Cells["A3"].Value = "App Version:";
            ws.Cells["A4"].Value = "Extracted At:";
            ws.Cells["B2"].Value = typeEntry + "s";
            ws.Cells["B3"].Value = versionEntry;
            ws.Cells["B4"].Value = "Wednesday, 20 July 2022 23:06:34";

            return pck;
        }

        [TestCase("1.2.0")]
        [TestCase("2.0.1")]
        public void Factory_Test_Version_Mismatch(string excelVersion)
        {
            // arrange
            ExcelPackage pck = WorksheetFactory("Media item", excelVersion);
            MyLibrary.Models.ValueObjects.AppVersion runningAppVersion = new MyLibrary.Models.ValueObjects.AppVersion(1, 2, 1);

            Assert.Throws<FormatException>(() => ImportExcelService.Create(pck, runningAppVersion));
        }

        [Test]
        public void Factory_Test_MediaItems_Ok()
        {
            // arrange
            ExcelPackage pck = WorksheetFactory("Media item", "1.2.1");
            pck.Workbook.Worksheets["Media item"].Cells["A6"].Value = "Id";
            pck.Workbook.Worksheets["Media item"].Cells["B6"].Value = "Title";
            pck.Workbook.Worksheets["Media item"].Cells["C6"].Value = "Type";
            pck.Workbook.Worksheets["Media item"].Cells["D6"].Value = "Number";
            pck.Workbook.Worksheets["Media item"].Cells["E6"].Value = "Running Time";
            pck.Workbook.Worksheets["Media item"].Cells["F6"].Value = "Release Year";
            pck.Workbook.Worksheets["Media item"].Cells["G6"].Value = "Tags";
            pck.Workbook.Worksheets["Media item"].Cells["H6"].Value = "Notes";

            // act
            var result = ImportExcelService.Create(pck, new MyLibrary.Models.ValueObjects.AppVersion(1, 2, 1));

            // assert
            Assert.AreEqual(typeof(MediaItemImportExcelService), result.GetType());
        }

        [Test]
        public void Factory_Test_Books_Ok()
        {
            // arrange
            ExcelPackage pck = WorksheetFactory("Media item", "1.2.1");
            pck.Workbook.Worksheets["Media item"].Cells["A6"].Value = "Id";
            pck.Workbook.Worksheets["Media item"].Cells["B6"].Value = "Title";
            pck.Workbook.Worksheets["Media item"].Cells["C6"].Value = "Long Title";
            pck.Workbook.Worksheets["Media item"].Cells["D6"].Value = "ISBN";
            pck.Workbook.Worksheets["Media item"].Cells["E6"].Value = "ISBN13";
            pck.Workbook.Worksheets["Media item"].Cells["F6"].Value = "Authors";
            pck.Workbook.Worksheets["Media item"].Cells["G6"].Value = "Language";
            pck.Workbook.Worksheets["Media item"].Cells["H6"].Value = "Tags";
            pck.Workbook.Worksheets["Media item"].Cells["I6"].Value = "Dewey Decimal";
            pck.Workbook.Worksheets["Media item"].Cells["J6"].Value = "MSRP";
            pck.Workbook.Worksheets["Media item"].Cells["K6"].Value = "Publisher";
            pck.Workbook.Worksheets["Media item"].Cells["L6"].Value = "Format";
            pck.Workbook.Worksheets["Media item"].Cells["M6"].Value = "Date Published";
            pck.Workbook.Worksheets["Media item"].Cells["N6"].Value = "Place of Publication";
            pck.Workbook.Worksheets["Media item"].Cells["O6"].Value = "Edition";
            pck.Workbook.Worksheets["Media item"].Cells["P6"].Value = "Pages";
            pck.Workbook.Worksheets["Media item"].Cells["Q6"].Value = "Dimensions";
            pck.Workbook.Worksheets["Media item"].Cells["R6"].Value = "Overview";
            pck.Workbook.Worksheets["Media item"].Cells["S6"].Value = "Excerpt";
            pck.Workbook.Worksheets["Media item"].Cells["T6"].Value = "Synopsys";
            pck.Workbook.Worksheets["Media item"].Cells["U6"].Value = "Notes";

            // act
            var result = ImportExcelService.Create(pck, new MyLibrary.Models.ValueObjects.AppVersion(1, 2, 1));

            // assert
            Assert.AreEqual(typeof(BookImportExcelService), result.GetType());
        }

        [Test]
        public void Factory_Test_Invalid_Type()
        {
            // arrange
            ExcelPackage pck = WorksheetFactory("bogus", "1.2.2");
            MyLibrary.Models.ValueObjects.AppVersion runningAppVersion = new MyLibrary.Models.ValueObjects.AppVersion(1, 2, 1);

            Assert.Throws<FormatException>(() => ImportExcelService.Create(pck, runningAppVersion));
        }
    }//class
}
