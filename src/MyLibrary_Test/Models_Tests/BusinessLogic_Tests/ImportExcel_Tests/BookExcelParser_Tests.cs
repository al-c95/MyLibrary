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
    class BookExcelParser_Tests
    {
        private ExcelPackage AddWorksheetHeaders(ExcelPackage pck)
        {
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

            return pck;
        }

        private ExcelPackage AddBogusWorksheetHeaders(ExcelPackage pck,
            string B2, string A6, string B6, string C6, string D6, string E6, string F6, string G6, string H6,
            string I6, string J6, string K6, string L6, string M6, string N6, string O6, string P6, string Q6,
            string R6, string S6, string T6, string U6)
        {
            pck.Workbook.Worksheets["Book"].Cells["B2"].Value = B2;
            pck.Workbook.Worksheets["Book"].Cells["A6"].Value = A6;
            pck.Workbook.Worksheets["Book"].Cells["B6"].Value = B6;
            pck.Workbook.Worksheets["Book"].Cells["C6"].Value = C6;
            pck.Workbook.Worksheets["Book"].Cells["D6"].Value = D6;
            pck.Workbook.Worksheets["Book"].Cells["E6"].Value = E6;
            pck.Workbook.Worksheets["Book"].Cells["F6"].Value = F6;
            pck.Workbook.Worksheets["Book"].Cells["G6"].Value = G6;
            pck.Workbook.Worksheets["Book"].Cells["H6"].Value = H6;
            pck.Workbook.Worksheets["Book"].Cells["I6"].Value = I6;
            pck.Workbook.Worksheets["Book"].Cells["J6"].Value = J6;
            pck.Workbook.Worksheets["Book"].Cells["K6"].Value = K6;
            pck.Workbook.Worksheets["Book"].Cells["L6"].Value = L6;
            pck.Workbook.Worksheets["Book"].Cells["M6"].Value = M6;
            pck.Workbook.Worksheets["Book"].Cells["N6"].Value = N6;
            pck.Workbook.Worksheets["Book"].Cells["O6"].Value = O6;
            pck.Workbook.Worksheets["Book"].Cells["P6"].Value = P6;
            pck.Workbook.Worksheets["Book"].Cells["Q6"].Value = Q6;
            pck.Workbook.Worksheets["Book"].Cells["R6"].Value = R6;
            pck.Workbook.Worksheets["Book"].Cells["S6"].Value = S6;
            pck.Workbook.Worksheets["Book"].Cells["T6"].Value = T6;
            pck.Workbook.Worksheets["Book"].Cells["U6"].Value = U6;

            return pck;
        }

        [TestCase("1.3.0")]
        [TestCase("1.4.0")]
        [TestCase("2.0.0")]
        public void Constructor_Test_Ok(string excelVersionEntry)
        {
            // arrange
            var pck = Utils.MediaItemWorksheetFactory(excelVersionEntry, "Book");
            pck = AddWorksheetHeaders(pck);

            // act/assert
            Assert.DoesNotThrow(() => new BookExcelParser(pck, new MyLibrary.Models.ValueObjects.AppVersion(1, 3, 0)));
        }

        [TestCase("1.2.0")]
        [TestCase("2.1.0")]
        public void Constructor_Test_VersionMismatch(string excelVersionEntry)
        {
            // arrange
            var pck = Utils.MediaItemWorksheetFactory(excelVersionEntry, "Book");
            pck = AddWorksheetHeaders(pck);

            // act/assert
            Assert.Throws<FormatException>(() => new BookExcelParser(pck, new MyLibrary.Models.ValueObjects.AppVersion(1, 3, 0)));
        }

        [TestCase("bogus", "Id", "Title", "Long Title", "ISBN", "ISBN13", "Authors", "Language", "Tags", "Dewey Decimal", "MSRP", "Publisher", "Format", "Date Published", "Place of Publication", "Edition", "Pages", "Dimensions", "Overview", "Excerpt", "Synopsys", "Notes")]
        [TestCase("bogus", "bogus", "Title", "Long Title", "ISBN", "ISBN13", "Authors", "Language", "Tags", "Dewey Decimal", "MSRP", "Publisher", "Format", "Date Published", "Place of Publication", "Edition", "Pages", "Dimensions", "Overview", "Excerpt", "Synopsys", "Notes")]
        [TestCase("bogus", "bogus", "bogus", "Long Title", "ISBN", "ISBN13", "Authors", "Language", "Tags", "Dewey Decimal", "MSRP", "Publisher", "Format", "Date Published", "Place of Publication", "Edition", "Pages", "Dimensions", "Overview", "Excerpt", "Synopsys", "Notes")]
        [TestCase("bogus", "bogus", "bogus", "bogus", "ISBN", "ISBN13", "Authors", "Language", "Tags", "Dewey Decimal", "MSRP", "Publisher", "Format", "Date Published", "Place of Publication", "Edition", "Pages", "Dimensions", "Overview", "Excerpt", "Synopsys", "Notes")]
        [TestCase("bogus", "bogus", "bogus", "bogus", "bogus", "ISBN13", "Authors", "Language", "Tags", "Dewey Decimal", "MSRP", "Publisher", "Format", "Date Published", "Place of Publication", "Edition", "Pages", "Dimensions", "Overview", "Excerpt", "Synopsys", "Notes")]
        [TestCase("bogus", "bogus", "bogus", "bogus", "bogus", "bogus", "Authors", "Language", "Tags", "Dewey Decimal", "MSRP", "Publisher", "Format", "Date Published", "Place of Publication", "Edition", "Pages", "Dimensions", "Overview", "Excerpt", "Synopsys", "Notes")]
        [TestCase("bogus", "bogus", "bogus", "bogus", "bogus", "bogus", "bogus", "Language", "Tags", "Dewey Decimal", "MSRP", "Publisher", "Format", "Date Published", "Place of Publication", "Edition", "Pages", "Dimensions", "Overview", "Excerpt", "Synopsys", "Notes")]
        [TestCase("bogus", "bogus", "bogus", "bogus", "bogus", "bogus", "bogus", "bogus", "Tags", "Dewey Decimal", "MSRP", "Publisher", "Format", "Date Published", "Place of Publication", "Edition", "Pages", "Dimensions", "Overview", "Excerpt", "Synopsys", "Notes")]
        [TestCase("bogus", "bogus", "bogus", "bogus", "bogus", "bogus", "bogus", "bogus", "bogus", "Dewey Decimal", "MSRP", "Publisher", "Format", "Date Published", "Place of Publication", "Edition", "Pages", "Dimensions", "Overview", "Excerpt", "Synopsys", "Notes")]
        [TestCase("bogus", "bogus", "bogus", "bogus", "bogus", "bogus", "bogus", "bogus", "bogus", "bogus", "MSRP", "Publisher", "Format", "Date Published", "Place of Publication", "Edition", "Pages", "Dimensions", "Overview", "Excerpt", "Synopsys", "Notes")]
        [TestCase("bogus", "bogus", "bogus", "bogus", "bogus", "bogus", "bogus", "bogus", "bogus", "bogus", "bogus", "Publisher", "Format", "Date Published", "Place of Publication", "Edition", "Pages", "Dimensions", "Overview", "Excerpt", "Synopsys", "Notes")]
        [TestCase("bogus", "bogus", "bogus", "bogus", "bogus", "bogus", "bogus", "bogus", "bogus", "bogus", "bogus", "bogus", "Format", "Date Published", "Place of Publication", "Edition", "Pages", "Dimensions", "Overview", "Excerpt", "Synopsys", "Notes")]
        [TestCase("bogus", "bogus", "bogus", "bogus", "bogus", "bogus", "bogus", "bogus", "bogus", "bogus", "bogus", "bogus", "bogus", "Date Published", "Place of Publication", "Edition", "Pages", "Dimensions", "Overview", "Excerpt", "Synopsys", "Notes")]
        [TestCase("bogus", "bogus", "bogus", "bogus", "bogus", "bogus", "bogus", "bogus", "bogus", "bogus", "bogus", "bogus", "bogus", "bogus", "Place of Publication", "Edition", "Pages", "Dimensions", "Overview", "Excerpt", "Synopsys", "Notes")]
        [TestCase("bogus", "bogus", "bogus", "bogus", "bogus", "bogus", "bogus", "bogus", "bogus", "bogus", "bogus", "bogus", "bogus", "bogus", "bogus", "Edition", "Pages", "Dimensions", "Overview", "Excerpt", "Synopsys", "Notes")]
        [TestCase("bogus", "bogus", "bogus", "bogus", "bogus", "bogus", "bogus", "bogus", "bogus", "bogus", "bogus", "bogus", "bogus", "bogus", "bogus", "bogus", "Pages", "Dimensions", "Overview", "Excerpt", "Synopsys", "Notes")]
        [TestCase("bogus", "bogus", "bogus", "bogus", "bogus", "bogus", "bogus", "bogus", "bogus", "bogus", "bogus", "bogus", "bogus", "bogus", "bogus", "bogus", "bogus", "Dimensions", "Overview", "Excerpt", "Synopsys", "Notes")]
        [TestCase("bogus", "bogus", "bogus", "bogus", "bogus", "bogus", "bogus", "bogus", "bogus", "bogus", "bogus", "bogus", "bogus", "bogus", "bogus", "bogus", "bogus", "bogus", "Overview", "Excerpt", "Synopsys", "Notes")]
        [TestCase("bogus", "bogus", "bogus", "bogus", "bogus", "bogus", "bogus", "bogus", "bogus", "bogus", "bogus", "bogus", "bogus", "bogus", "bogus", "bogus", "bogus", "bogus", "bogus", "Excerpt", "Synopsys", "Notes")]
        [TestCase("bogus", "bogus", "bogus", "bogus", "bogus", "bogus", "bogus", "bogus", "bogus", "bogus", "bogus", "bogus", "bogus", "bogus", "bogus", "bogus", "bogus", "bogus", "bogus", "bogus", "Synopsys", "Notes")]
        [TestCase("bogus", "bogus", "bogus", "bogus", "bogus", "bogus", "bogus", "bogus", "bogus", "bogus", "bogus", "bogus", "bogus", "bogus", "bogus", "bogus", "bogus", "bogus", "bogus", "bogus", "bogus", "Notes")]
        [TestCase("bogus", "bogus", "bogus", "bogus", "bogus", "bogus", "bogus", "bogus", "bogus", "bogus", "bogus", "bogus", "bogus", "bogus", "bogus", "bogus", "bogus", "bogus", "bogus", "bogus", "bogus", "bogus")]
        public void Constructor_Test_InvalidFormat(string B2, string A6, string B6, string C6, string D6, string E6, string F6, string G6, string H6,
            string I6, string J6, string K6, string L6, string M6, string N6, string O6, string P6, string Q6,
            string R6, string S6, string T6, string U6)
        {
            // arrange
            var pck = Utils.MediaItemWorksheetFactory("1.4.0", "Book");
            pck = AddBogusWorksheetHeaders(pck,B2,A6,B6,C6,D6,E6,F6,G6,H6,I6,J6,K6,L6,M6,N6,O6,P6,Q6,R6,S6,T6,U6);

            // act/assert
            Assert.Throws<FormatException>(() => new BookExcelParser(pck, new MyLibrary.Models.ValueObjects.AppVersion(1, 3, 0)));
        }
    }//class
}
