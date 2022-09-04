using System;
using System.Collections;
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

namespace MyLibrary_Test.Models_Tests.BusinessLogic_Tests.ImportExcel_Tests
{
    [TestFixture]
    class MediaItemExcelParser_Tests
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
            var pck = Utils.MediaItemWorksheetFactory(excelVersionEntry, "Media item");
            pck = AddWorksheetHeaders(pck);

            // act/assert
            Assert.DoesNotThrow(() => new MediaItemExcelParser(pck, new MyLibrary.Models.ValueObjects.AppVersion(1, 3, 0)));
        }

        [TestCase("1.2.0")]
        [TestCase("2.1.0")]
        public void Constructor_Test_VersionMismatch(string excelVersionEntry)
        {
            // arrange
            var pck = Utils.MediaItemWorksheetFactory(excelVersionEntry, "Media item");
            pck = AddWorksheetHeaders(pck);

            // act/assert
            Assert.Throws<FormatException>(() => new MediaItemExcelParser(pck, new MyLibrary.Models.ValueObjects.AppVersion(1, 3, 0)));
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
        public void Constructor_Test_InvalidHeaders(string B2, string A6, string B6, string C6, string D6, string E6, string F6, string G6, string H6)
        {
            // arrange
            var pck = Utils.MediaItemWorksheetFactory("1.4.0", "Media item");
            pck = AddBogusWorksheetHeaders(pck,B2,A6,B6,C6,D6,E6,F6,G6,H6);

            // act/assert
            Assert.Throws<FormatException>(() => new MediaItemExcelParser(pck, new MyLibrary.Models.ValueObjects.AppVersion(1, 3, 0)));
        }

        [Test]
        public void Constructor_Test_InvalidMetadata()
        {
            // arrange
            var pck = Utils.MediaItemWorksheetFactory("1.4.0", "Media item");
            pck.Workbook.Worksheets["Media item"].Cells["A1"].Value = "bogus";

            // act/assert
            Assert.Throws<FormatException>(() => new MediaItemExcelParser(pck, new MyLibrary.Models.ValueObjects.AppVersion(1, 3, 0)));
        }

        [Test]
        public void Constructor_Test_ExpectedWorksheetNotFound()
        {
            // arrange
            ExcelPackage pck = new ExcelPackage();
            ExcelWorksheet ws = pck.Workbook.Worksheets.Add("bogus");

            // act/assert
            Assert.Throws<FormatException>(() => new BookExcelParser(pck, new MyLibrary.Models.ValueObjects.AppVersion(1, 3, 0)));
        }

        [Test]
        public void Run_Test()
        {
            // arrange
            // worksheet
            ExcelPackage pck = Utils.MediaItemWorksheetFactory("1.4.0", "Media item");
            // valid row
            AddRow(pck, "A7", null);
            AddRow(pck, "B7", "item1");
            AddRow(pck, "C7", "Dvd");
            AddRow(pck, "D7", 0123456789);
            AddRow(pck, "E7", "80");
            AddRow(pck, "F7", "2012");
            AddRow(pck, "G7", "tag1, tag2");
            AddRow(pck, "H7", "this is a test.");
            // valid row
            AddRow(pck, "A8", "1");
            AddRow(pck, "B8", "item2");
            AddRow(pck, "C8", "Cd");
            AddRow(pck, "D8", "0123456789");
            AddRow(pck, "E8", "");
            AddRow(pck, "F8", "2012");
            AddRow(pck, "G8", "tag1");
            AddRow(pck, "H8", "this is a test.");
            // invalid id
            AddRow(pck, "A9", "bogus_id");
            AddRow(pck, "B9", "");
            AddRow(pck, "C9", "Dvd");
            AddRow(pck, "D9", "0123456789");
            AddRow(pck, "E9", "80");
            AddRow(pck, "F9", "2012");
            AddRow(pck, "G9", "tag1");
            AddRow(pck, "H9", "this is a test.");
            // empty title
            AddRow(pck, "A10", "1");
            AddRow(pck, "B10", "");
            AddRow(pck, "C10", "Dvd");
            AddRow(pck, "D10", "0123456789");
            AddRow(pck, "E10", "80");
            AddRow(pck, "F10", "2012");
            AddRow(pck, "G10", "tag1");
            AddRow(pck, "H10", "this is a test.");
            // unsupported type
            AddRow(pck, "A11", "1");
            AddRow(pck, "B11", "item5");
            AddRow(pck, "C11", "bogus_type");
            AddRow(pck, "D11", "0123456789");
            AddRow(pck, "E11", "80");
            AddRow(pck, "F11", "2012");
            AddRow(pck, "G11", "tag1");
            AddRow(pck, "H11", "this is a test.");
            // unsupported type
            AddRow(pck, "A12", "1");
            AddRow(pck, "B12", "item6");
            AddRow(pck, "C12", "Book");
            AddRow(pck, "D12", "0123456789");
            AddRow(pck, "E12", "80");
            AddRow(pck, "F12", "2012");
            AddRow(pck, "G12", "tag1");
            AddRow(pck, "H12", "this is a test.");
            // invalid number
            AddRow(pck, "A13", "1");
            AddRow(pck, "B13", "item6");
            AddRow(pck, "C13", "Dvd");
            AddRow(pck, "D13", "bogus_number");
            AddRow(pck, "E13", "80");
            AddRow(pck, "F13", "2012");
            AddRow(pck, "G13", "tag1");
            AddRow(pck, "H13", "this is a test.");
            // invalid running time
            AddRow(pck, "A14", "1");
            AddRow(pck, "B14", "item6");
            AddRow(pck, "C14", "Dvd");
            AddRow(pck, "D14", "0123456789");
            AddRow(pck, "E14", "bogus_number");
            AddRow(pck, "F14", "2012");
            AddRow(pck, "G14", "tag1");
            AddRow(pck, "H14", "this is a test.");
            // empty row
            AddRow(pck, "A15", "");
            AddRow(pck, "B15", "");
            AddRow(pck, "C15", "");
            AddRow(pck, "D15", "");
            AddRow(pck, "E15", "");
            AddRow(pck, "F15", "");
            AddRow(pck, "G15", "");
            AddRow(pck, "H15", "");
            // invalid year
            AddRow(pck, "A16", "1");
            AddRow(pck, "B16", "item6");
            AddRow(pck, "C16", "Dvd");
            AddRow(pck, "D16", "0123456789");
            AddRow(pck, "E16", "80");
            AddRow(pck, "F16", "bogus_number");
            AddRow(pck, "G16", "tag1");
            AddRow(pck, "H16", "this is a test.");
            // service
            var importService = new MediaItemExcelParser(pck, new MyLibrary.Models.ValueObjects.AppVersion(1, 3, 0));

            // act
            List<ExcelRowResult> results = importService.Run().ToList();

            // assert
            Assert.IsTrue(results.Count == 9);
            Assert.IsTrue(results.Any(r => r.Status == ExcelRowResultStatus.Success &&
                                            r.Row == 7 &&
                                            r.Item.Title == "item1" &&
                                            r.Item.Type == ItemType.Dvd &&
                                            ((MediaItem)r.Item).Number == 0123456789 &&
                                            ((MediaItem)r.Item).RunningTime == 80 &&
                                            ((MediaItem)r.Item).Tags.Count == 2 &&
                                            ((MediaItem)r.Item).Tags.Any(t => t.Name=="tag1") && 
                                            ((MediaItem)r.Item).Tags.Any(t => t.Name=="tag2") &&
                                            r.Item.Notes == "this is a test."));
            Assert.IsTrue(results.Any(r => r.Status == ExcelRowResultStatus.Success &&
                                            r.Row == 8 &&
                                            r.Item.Id == 1 &&
                                            r.Item.Title == "item2" &&
                                            r.Item.Type == ItemType.Cd &&
                                            ((MediaItem)r.Item).Tags.Count == 1 &&
                                            ((MediaItem)r.Item).Tags.Any(t => t.Name.Equals("tag1")) &&
                                            r.Item.Notes == "this is a test."));
            Assert.IsTrue(results.Any(r => r.Status == ExcelRowResultStatus.Error &&
                                            r.Row == 9 &&
                                            r.Item == null &&
                                            r.Message == "Invalid Id: bogus_id"));
            Assert.IsTrue(results.Any(r => r.Status == ExcelRowResultStatus.Error &&
                                            r.Row == 10 &&
                                            r.Item == null &&
                                            r.Message == "Title cannot be empty"));
            Assert.IsTrue(results.Any(r => r.Status == ExcelRowResultStatus.Error &&
                                            r.Row == 11 &&
                                            r.Item == null &&
                                            r.Message == "Unsupported type: bogus_type"));
            Assert.IsTrue(results.Any(r => r.Status == ExcelRowResultStatus.Error &&
                                            r.Row == 12 &&
                                            r.Item == null &&
                                            r.Message == "Unsupported type: Book"));
            Assert.IsTrue(results.Any(r => r.Status == ExcelRowResultStatus.Error &&
                                            r.Row == 13 &&
                                            r.Item == null &&
                                            r.Message == "Invalid number: bogus_number"));
            Assert.IsTrue(results.Any(r => r.Status == ExcelRowResultStatus.Error &&
                                            r.Row == 14 &&
                                            r.Item == null &&
                                            r.Message == "Invalid running time: bogus_number"));
            Assert.IsTrue(results.Any(r => r.Status == ExcelRowResultStatus.Error &&
                                            r.Row == 16 &&
                                            r.Item == null &&
                                            r.Message == "Invalid release year: bogus_number"));
        }

        private ExcelPackage AddRow(ExcelPackage pck, string address, object value)
        {
            pck.Workbook.Worksheets["Media item"].Cells[address].Value = value;

            return pck;
        }
    }//class
}