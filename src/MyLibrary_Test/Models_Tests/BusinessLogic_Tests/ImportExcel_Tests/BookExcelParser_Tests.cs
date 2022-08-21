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

        [Test]
        public void Run_Test()
        {
            // arrange
            // worksheet
            ExcelPackage pck = Utils.BookWorksheetFactory("1.4.0");
            // valid row
            AddRow(pck, "A7", null);
            AddRow(pck, "B7", "item1");
            AddRow(pck, "C7", "item1: this is a test");
            AddRow(pck, "D7", "0123456789");
            AddRow(pck, "E7", "0123456789012");
            AddRow(pck, "F7", "John Smith-Jones; Jane C. Doe");
            AddRow(pck, "G7", "English");
            AddRow(pck, "H7", "tag1, tag2");
            AddRow(pck, "I7", "1.20");
            AddRow(pck, "J7", "msrp");
            AddRow(pck, "K7", "some_publisher");
            AddRow(pck, "L7", "format");
            AddRow(pck, "M7", "2010");
            AddRow(pck, "N7", "Australia");
            AddRow(pck, "O7", "First Edition");
            AddRow(pck, "P7", "100");
            AddRow(pck, "Q7", "dimensions");
            AddRow(pck, "R7", "overview");
            AddRow(pck, "S7", "excerpt");
            AddRow(pck, "T7", "synopsys");
            AddRow(pck, "U7", "this is a test.");
            // valid row
            AddRow(pck, "A8", 1);
            AddRow(pck, "B8", "item1");
            AddRow(pck, "C8", "");
            AddRow(pck, "D8", "0123456789");
            AddRow(pck, "E8", "");
            AddRow(pck, "F8", "John de Coder");
            AddRow(pck, "G8", "English");
            AddRow(pck, "H8", "tag");
            AddRow(pck, "I8", "");
            AddRow(pck, "J8", "");
            AddRow(pck, "K8", "some_publisher");
            AddRow(pck, "L8", "");
            AddRow(pck, "M8", "2010");
            AddRow(pck, "N8", "");
            AddRow(pck, "O8", "");
            AddRow(pck, "P8", "100");
            AddRow(pck, "Q8", "");
            AddRow(pck, "R8", "");
            AddRow(pck, "S8", "");
            AddRow(pck, "T8", "");
            AddRow(pck, "U8", "");
            // invalid id
            AddRow(pck, "A9", "bogus_id");
            AddRow(pck, "B9", "item1");
            AddRow(pck, "C9", "");
            AddRow(pck, "D9", "0123456789");
            AddRow(pck, "E9", "");
            AddRow(pck, "F9", "John Smith");
            AddRow(pck, "G9", "English");
            AddRow(pck, "H9", "tag");
            AddRow(pck, "I9", "");
            AddRow(pck, "J9", "");
            AddRow(pck, "K9", "some_publisher");
            AddRow(pck, "L9", "");
            AddRow(pck, "M9", "2010");
            AddRow(pck, "N9", "");
            AddRow(pck, "O9", "");
            AddRow(pck, "P9", "100");
            AddRow(pck, "Q9", "");
            AddRow(pck, "R9", "");
            AddRow(pck, "S9", "");
            AddRow(pck, "T9", "");
            AddRow(pck, "U9", "");
            // empty title
            AddRow(pck, "A10", 1);
            AddRow(pck, "B10", "");
            AddRow(pck, "C10", "");
            AddRow(pck, "D10", "0123456789");
            AddRow(pck, "E10", "");
            AddRow(pck, "F10", "John Smith");
            AddRow(pck, "G10", "English");
            AddRow(pck, "H10", "tag");
            AddRow(pck, "I10", "");
            AddRow(pck, "J10", "");
            AddRow(pck, "K10", "some_publisher");
            AddRow(pck, "L10", "");
            AddRow(pck, "M10", "2010");
            AddRow(pck, "N10", "");
            AddRow(pck, "O10", "");
            AddRow(pck, "P10", "100");
            AddRow(pck, "Q10", "");
            AddRow(pck, "R10", "");
            AddRow(pck, "S10", "");
            AddRow(pck, "T10", "");
            AddRow(pck, "U10", "");
            // invalid isbn
            AddRow(pck, "A11", 1);
            AddRow(pck, "B11", "item1");
            AddRow(pck, "C11", "");
            AddRow(pck, "D11", "bogus_isbn");
            AddRow(pck, "E11", "");
            AddRow(pck, "F11", "John Smith");
            AddRow(pck, "G11", "English");
            AddRow(pck, "H11", "tag");
            AddRow(pck, "I11", "");
            AddRow(pck, "J11", "");
            AddRow(pck, "K11", "some_publisher");
            AddRow(pck, "L11", "");
            AddRow(pck, "M11", "2010");
            AddRow(pck, "N11", "");
            AddRow(pck, "O11", "");
            AddRow(pck, "P11", "100");
            AddRow(pck, "Q11", "");
            AddRow(pck, "R11", "");
            AddRow(pck, "S11", "");
            AddRow(pck, "T11", "");
            AddRow(pck, "U11", "");
            // invalid isbn13
            AddRow(pck, "A12", 1);
            AddRow(pck, "B12", "item1");
            AddRow(pck, "E12", "bogus_isbn");
            AddRow(pck, "F12", "John Smith");
            AddRow(pck, "G12", "English");
            AddRow(pck, "H12", "tag");
            AddRow(pck, "I12", "");
            AddRow(pck, "J12", "");
            AddRow(pck, "K12", "some_publisher");
            AddRow(pck, "L12", "");
            AddRow(pck, "M12", "2010");
            AddRow(pck, "N12", "");
            AddRow(pck, "O12", "");
            AddRow(pck, "P12", "100");
            AddRow(pck, "Q12", "");
            AddRow(pck, "R12", "");
            AddRow(pck, "S12", "");
            AddRow(pck, "T12", "");
            AddRow(pck, "U12", "");
            // invalid authors
            AddRow(pck, "A13", 1);
            AddRow(pck, "B13", "item1");
            AddRow(pck, "F13", "John Smith;;");
            AddRow(pck, "G13", "English");
            AddRow(pck, "H13", "tag");
            AddRow(pck, "I13", "");
            AddRow(pck, "J13", "");
            AddRow(pck, "K13", "some_publisher");
            AddRow(pck, "L13", "");
            AddRow(pck, "M13", "2010");
            AddRow(pck, "N13", "");
            AddRow(pck, "O13", "");
            AddRow(pck, "P13", "100");
            AddRow(pck, "Q13", "");
            AddRow(pck, "R13", "");
            AddRow(pck, "S13", "");
            AddRow(pck, "T13", "");
            AddRow(pck, "U13", "");
            // empty language
            AddRow(pck, "A14", 1);
            AddRow(pck, "B14", "item1");
            AddRow(pck, "F14", "John Smith");
            AddRow(pck, "G14", "");
            AddRow(pck, "H14", "tag");
            AddRow(pck, "I14", "");
            AddRow(pck, "J14", "");
            AddRow(pck, "K14", "some_publisher");
            AddRow(pck, "L14", "");
            AddRow(pck, "M14", "2010");
            AddRow(pck, "N14", "");
            AddRow(pck, "O14", "");
            AddRow(pck, "P14", "100");
            AddRow(pck, "Q14", "");
            AddRow(pck, "R14", "");
            AddRow(pck, "S14", "");
            AddRow(pck, "T14", "");
            AddRow(pck, "U14", "");
            // invalid Dewey decimal
            AddRow(pck, "A15", 1);
            AddRow(pck, "B15", "item1");
            AddRow(pck, "F15", "John Smith");
            AddRow(pck, "G15", "English");
            AddRow(pck, "I15", "bogus");
            AddRow(pck, "J15", "");
            AddRow(pck, "K15", "some_publisher");
            AddRow(pck, "L15", "");
            AddRow(pck, "M15", "2010");
            AddRow(pck, "N15", "");
            AddRow(pck, "O15", "");
            AddRow(pck, "P15", "100");
            AddRow(pck, "Q15", "");
            AddRow(pck, "R15", "");
            AddRow(pck, "S15", "");
            AddRow(pck, "T15", "");
            AddRow(pck, "U15", "");
            // empty publisher
            AddRow(pck, "A16", 1);
            AddRow(pck, "B16", "item1");
            AddRow(pck, "F16", "John Smith");
            AddRow(pck, "G16", "English");
            AddRow(pck, "K16", "");
            AddRow(pck, "L16", "");
            AddRow(pck, "M16", "2010");
            AddRow(pck, "N16", "");
            AddRow(pck, "O16", "");
            AddRow(pck, "P16", "100");
            AddRow(pck, "Q16", "");
            AddRow(pck, "R16", "");
            AddRow(pck, "S16", "");
            AddRow(pck, "T16", "");
            AddRow(pck, "U16", "");
            // empty row
            AddRow(pck, "A17", "");
            AddRow(pck, "B17", "");
            AddRow(pck, "C17", "");
            AddRow(pck, "D17", "");
            AddRow(pck, "E17", "");
            AddRow(pck, "F17", "");
            AddRow(pck, "G17", "");
            AddRow(pck, "H17", "");
            AddRow(pck, "I17", "");
            AddRow(pck, "J17", "");
            AddRow(pck, "K17", "");
            AddRow(pck, "L17", "");
            AddRow(pck, "M17", "");
            AddRow(pck, "N17", "");
            AddRow(pck, "O17", "");
            AddRow(pck, "P17", "");
            AddRow(pck, "Q17", "");
            AddRow(pck, "R17", "");
            AddRow(pck, "S17", "");
            AddRow(pck, "T17", "");
            AddRow(pck, "U17", "");
            // invalid pages
            AddRow(pck, "A18", null);
            AddRow(pck, "B18", "item1");
            AddRow(pck, "C18", "item1: this is a test");
            AddRow(pck, "D18", "0123456789");
            AddRow(pck, "E18", "0123456789012");
            AddRow(pck, "F18", "John Smith; Jane Doe");
            AddRow(pck, "G18", "English");
            AddRow(pck, "H18", "tag1, tag2");
            AddRow(pck, "I18", "1.20");
            AddRow(pck, "J18", "msrp");
            AddRow(pck, "K18", "some_publisher");
            AddRow(pck, "L18", "format");
            AddRow(pck, "M18", "2010");
            AddRow(pck, "N18", "Australia");
            AddRow(pck, "O18", "First Edition");
            AddRow(pck, "P18", "bogus");
            AddRow(pck, "Q18", "dimensions");
            AddRow(pck, "R18", "overview");
            AddRow(pck, "S18", "excerpt");
            AddRow(pck, "T18", "synopsys");
            AddRow(pck, "U18", "this is a test.");
            // service
            var importService = new BookExcelParser(pck, new MyLibrary.Models.ValueObjects.AppVersion(1, 3, 0));

            // act
            List<ExcelRowResult> results = importService.Run().ToList();

            // assert
            Assert.IsTrue(results.Count == 11);
            Assert.IsTrue(results.Any(r => r.Status == ExcelRowResultStatus.Success &&
                                            r.Row == 7 &&
                                            r.Item.Title == "item1" &&
                                            ((Book)r.Item).TitleLong == "item1: this is a test" &&
                                            ((Book)r.Item).Isbn == "0123456789" &&
                                            ((Book)r.Item).Isbn13 == "0123456789012" &&
                                            ((Book)r.Item).Authors.Count == 2 &&
                                            ((Book)r.Item).Authors.Any(a => a.FirstName.Equals("John") && a.LastName.Equals("Smith-Jones")) &&
                                            ((Book)r.Item).Authors.Any(a => a.FirstName.Equals("Jane C.") && a.LastName.Equals("Doe")) &&
                                            ((Book)r.Item).Language == "English" &&
                                            ((Book)r.Item).Tags.Count == 2 &&
                                            ((Book)r.Item).Tags.Any(t => t.Name.Equals("tag1")) &&
                                            ((Book)r.Item).Tags.Any(t => t.Name.Equals("tag2")) &&
                                            ((Book)r.Item).DeweyDecimal.Equals(1.20M) &&
                                            ((Book)r.Item).Msrp.Equals("msrp") &&
                                            ((Book)r.Item).Publisher.Name.Equals("some_publisher") &&
                                            ((Book)r.Item).Format == "format" &&
                                            ((Book)r.Item).DatePublished == "2010" &&
                                            ((Book)r.Item).Pages == 100 &&
                                            ((Book)r.Item).PlaceOfPublication == "Australia" &&
                                            ((Book)r.Item).Edition == "First Edition" &&
                                            ((Book)r.Item).Dimensions == "dimensions" &&
                                            ((Book)r.Item).Overview == "overview" && 
                                            ((Book)r.Item).Excerpt == "excerpt" && 
                                            ((Book)r.Item).Synopsys == "synopsys" &&
                                            ((Book)r.Item).Notes == "this is a test." ));
            Assert.IsTrue(results.Any(r => r.Status == ExcelRowResultStatus.Success &&
                                            r.Row == 8 && 
                                            r.Item.Title == "item1" &&
                                            ((Book)r.Item).Isbn == "0123456789" && 
                                            ((Book)r.Item).Authors.Count == 1 &&
                                            ((Book)r.Item).Authors.Any(a => a.FirstName == "John" && a.LastName == "de Coder") &&
                                            ((Book)r.Item).Language == "English" &&
                                            ((Book)r.Item).Tags.Count == 1 &&
                                            ((Book)r.Item).Tags.Any(t => t.Name.Equals("tag")) &&
                                            ((Book)r.Item).Publisher.Name.Equals("some_publisher") &&
                                            ((Book)r.Item).DatePublished == "2010" &&
                                            ((Book)r.Item).Pages == 100));
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
                                            r.Message == "Invalid ISBN: bogus_isbn"));
            Assert.IsTrue(results.Any(r => r.Status == ExcelRowResultStatus.Error &&
                                            r.Row == 12 &&
                                            r.Item == null &&
                                            r.Message == "Invalid ISBN13: bogus_isbn"));
            Assert.IsTrue(results.Any(r => r.Status == ExcelRowResultStatus.Error &&
                                            r.Row == 13 &&
                                            r.Item == null &&
                                            r.Message == "Invalid Authors entry: John Smith;;"));
            Assert.IsTrue(results.Any(r => r.Status == ExcelRowResultStatus.Error &&
                                            r.Row == 14 &&
                                            r.Item == null &&
                                            r.Message == "Language cannot be empty"));
            Assert.IsTrue(results.Any(r => r.Status == ExcelRowResultStatus.Error &&
                                            r.Row == 15 &&
                                            r.Item == null &&
                                            r.Message == "Invalid Dewey Decimal: bogus"));
            Assert.IsTrue(results.Any(r => r.Status == ExcelRowResultStatus.Error &&
                                            r.Row == 16 &&
                                            r.Item == null &&
                                            r.Message == "Publisher cannot be empty"));
            Assert.IsTrue(results.Any(r => r.Status == ExcelRowResultStatus.Error &&
                                            r.Row == 18 &&
                                            r.Item == null &&
                                            r.Message == "Invalid Pages: bogus"));
        }

        private ExcelPackage AddRow(ExcelPackage pck, string address, object value)
        {
            pck.Workbook.Worksheets["Book"].Cells[address].Value = value;

            return pck;
        }
    }//class
}
