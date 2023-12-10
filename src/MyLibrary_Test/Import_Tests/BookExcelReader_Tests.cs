//MIT License

//Copyright (c) 2021-2023

//Permission is hereby granted, free of charge, to any person obtaining a copy
//of this software and associated documentation files (the "Software"), to deal
//in the Software without restriction, including without limitation the rights
//to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
//copies of the Software, and to permit persons to whom the Software is
//furnished to do so, subject to the following conditions:

//The above copyright notice and this permission notice shall be included in all
//copies or substantial portions of the Software.

//THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
//IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
//FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
//AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
//LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
//OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
//SOFTWARE

using System;
using System.Linq;
using NUnit.Framework;
using OfficeOpenXml;
using MyLibrary.Import;

namespace MyLibrary_Test.Import_Tests
{
    [TestFixture]
    public class BookExcelReader_Tests
    {
        int parsedCount = 0;
        int skippedCount = 0;

        private void ProgressCallback(int parsed, int skipped)
        {
            parsedCount = parsed;
            skippedCount = skipped;
        }

        private void AddCell(ExcelPackage pck, string address, string value)
        {
            pck.Workbook.Worksheets["Book"].Cells[address].Value = value;
        }

        private void AddValidHeadersAndMetadata(ExcelPackage pck)
        {
            // metadata
            AddCell(pck, "A1", "MyLibrary");
            AddCell(pck, "A2", "Type");
            AddCell(pck, "A3", "App Version:");
            AddCell(pck, "B3", "1.5.0");
            AddCell(pck, "B2", "Books");
            AddCell(pck, "A4", "Extracted At:");
            // headers
            AddCell(pck, "A6", "Id");
            AddCell(pck, "B6", "Title");
            AddCell(pck, "C6", "Long Title");
            AddCell(pck, "D6", "ISBN");
            AddCell(pck, "E6", "ISBN13");
            AddCell(pck, "F6", "Authors");
            AddCell(pck, "G6", "Language");
            AddCell(pck, "H6", "Tags");
            AddCell(pck, "I6", "Dewey Decimal");
            AddCell(pck, "J6", "MSRP");
            AddCell(pck, "K6", "Publisher");
            AddCell(pck, "L6", "Format");
            AddCell(pck, "M6", "Date Published");
            AddCell(pck, "N6", "Place of Publication");
            AddCell(pck, "O6", "Edition");
            AddCell(pck, "P6", "Pages");
            AddCell(pck, "Q6", "Dimensions");
            AddCell(pck, "R6", "Overview");
            AddCell(pck, "S6", "Excerpt");
            AddCell(pck, "T6", "Synopsys");
            AddCell(pck, "U6", "Notes");
        }

        [Test]
        public void Read_Test_Validated()
        {
            // arrange
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            ExcelPackage pck = new ExcelPackage();
            pck.Workbook.Worksheets.Add("Book");
            // metadata and headers
            AddValidHeadersAndMetadata(pck);
            // item records
            // item 1
            AddCell(pck, "A7", "1");
            AddCell(pck, "B7", "Pythonic awesomeness");
            AddCell(pck, "C7", "Pythonic awesomeness: coding in Python");
            AddCell(pck, "D7", "0123456789");
            AddCell(pck, "E7", "012345678901X");
            AddCell(pck, "F7", "John Smith-Jones; Jane C. Doe");
            AddCell(pck, "G7", "English");
            AddCell(pck, "H7", "programming, software development");
            AddCell(pck, "I7", "200.5");
            AddCell(pck, "J7", "20");
            AddCell(pck, "K7", "publisher");
            AddCell(pck, "L7", "imaginary");
            AddCell(pck, "M7", "2018");
            AddCell(pck, "N7", "Australia");
            AddCell(pck, "O7", "1st");
            AddCell(pck, "P7", "100");
            AddCell(pck, "Q7", "dimensions");
            AddCell(pck, "R7", "overview");
            AddCell(pck, "S7", "excerpt");
            AddCell(pck, "T7", "synopsys");
            AddCell(pck, "U7", "notes");
            // reader
            BookExcelReader excelReader = new BookExcelReader(pck, "Book", new MyLibrary.Models.ValueObjects.AppVersion(1, 5, 0));

            // act
            var results = excelReader.Read(ProgressCallback);

            // assert
            // items
            Assert.AreEqual(1, results.Count());
            Assert.AreEqual(1, results.ToList()[0].Id);
            Assert.AreEqual("Pythonic awesomeness", results.ToList()[0].Title);
            Assert.AreEqual("Pythonic awesomeness: coding in Python", results.ToList()[0].TitleLong);
            Assert.AreEqual("0123456789", results.ToList()[0].Isbn);
            Assert.AreEqual("012345678901X", results.ToList()[0].Isbn13);
            Assert.AreEqual("English", results.ToList()[0].Language);
            Assert.AreEqual(200.5, results.ToList()[0].DeweyDecimal);
            Assert.AreEqual("20", results.ToList()[0].Msrp);
            Assert.AreEqual("publisher", results.ToList()[0].Publisher.Name);
            Assert.AreEqual("imaginary", results.ToList()[0].Format);
            Assert.AreEqual("2018", results.ToList()[0].DatePublished);
            Assert.AreEqual("Australia", results.ToList()[0].PlaceOfPublication);
            Assert.AreEqual("1st", results.ToList()[0].Edition);
            Assert.AreEqual(100, results.ToList()[0].Pages);
            Assert.AreEqual("dimensions", results.ToList()[0].Dimensions);
            Assert.AreEqual("overview", results.ToList()[0].Overview);
            Assert.AreEqual("excerpt", results.ToList()[0].Excerpt);
            Assert.AreEqual("synopsys", results.ToList()[0].Synopsys);
            Assert.AreEqual("notes", results.ToList()[0].Notes);
            // progress callback values
            Assert.AreEqual(1, parsedCount);
            Assert.AreEqual(0, skippedCount);
        }

        [Test]
        public void Constructor_Test_ExpectedWorksheetNotFound()
        {
            // arrange
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            ExcelPackage pck = new ExcelPackage();
            pck.Workbook.Worksheets.Add("worksheet");

            // act/assert
            Assert.Throws<FormatException>(() => new BookExcelReader(pck, "Book", new MyLibrary.Models.ValueObjects.AppVersion(1, 5, 0)));
        }

        [Test]
        public void Constructor_Test_InvalidMetadata()
        {
            // arrange
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            ExcelPackage pck = new ExcelPackage();
            pck.Workbook.Worksheets.Add("Book");
            // metadata
            AddCell(pck, "A1", "bogus entry");
            AddCell(pck, "A2", "bogus entry");
            AddCell(pck, "A3", "bogus entry");
            AddCell(pck, "B3", "1.5.0");
            AddCell(pck, "B2", "bogus entry");
            AddCell(pck, "A4", "bogus entry");
            // headers
            AddCell(pck, "A6", "bogus entry");
            AddCell(pck, "B6", "bogus entry");
            AddCell(pck, "C6", "bogus entry");
            AddCell(pck, "D6", "bogus entry");
            AddCell(pck, "E6", "bogus entry");
            AddCell(pck, "F6", "bogus entry");
            AddCell(pck, "G6", "bogus entry");
            AddCell(pck, "H6", "bogus entry");
            AddCell(pck, "I6", "bogus entry");
            AddCell(pck, "J6", "bogus entry");
            AddCell(pck, "K6", "bogus entry");
            AddCell(pck, "L6", "bogus entry");
            AddCell(pck, "M6", "bogus entry");
            AddCell(pck, "N6", "bogus entry");
            AddCell(pck, "O6", "bogus entry");
            AddCell(pck, "P6", "bogus entry");
            AddCell(pck, "Q6", "bogus entry");
            AddCell(pck, "R6", "bogus entry");
            AddCell(pck, "S6", "bogus entry");
            AddCell(pck, "T6", "bogus entry");
            AddCell(pck, "U6", "bogus entry");

            // act/assert
            Assert.Throws<FormatException>(() => new BookExcelReader(pck, "Book", new MyLibrary.Models.ValueObjects.AppVersion(1, 5, 0)));
        }

        [Test]
        public void Constructor_Test_InvalidHeaders()
        {
            // arrange
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            ExcelPackage pck = new ExcelPackage();
            pck.Workbook.Worksheets.Add("Book");
            // metadata
            AddCell(pck, "A1", "MyLibrary");
            AddCell(pck, "A2", "Type");
            AddCell(pck, "A3", "App Version:");
            AddCell(pck, "B3", "1.5.0");
            AddCell(pck, "B2", "Books");
            AddCell(pck, "A4", "Extracted At:");
            // headers
            AddCell(pck, "A6", "bogus entry");
            AddCell(pck, "B6", "bogus entry");
            AddCell(pck, "C6", "bogus entry");
            AddCell(pck, "D6", "bogus entry");
            AddCell(pck, "E6", "bogus entry");
            AddCell(pck, "F6", "bogus entry");
            AddCell(pck, "G6", "bogus entry");
            AddCell(pck, "H6", "bogus entry");
            AddCell(pck, "I6", "bogus entry");
            AddCell(pck, "J6", "bogus entry");
            AddCell(pck, "K6", "bogus entry");
            AddCell(pck, "L6", "bogus entry");
            AddCell(pck, "M6", "bogus entry");
            AddCell(pck, "N6", "bogus entry");
            AddCell(pck, "O6", "bogus entry");
            AddCell(pck, "P6", "bogus entry");
            AddCell(pck, "Q6", "bogus entry");
            AddCell(pck, "R6", "bogus entry");
            AddCell(pck, "S6", "bogus entry");
            AddCell(pck, "T6", "bogus entry");
            AddCell(pck, "U6", "bogus entry");

            // act/assert
            Assert.Throws<FormatException>(() => new BookExcelReader(pck, "Book", new MyLibrary.Models.ValueObjects.AppVersion(1, 5, 0)));
        }

        [TestCase(3, 0, 0)]
        [TestCase(0, 1, 0)]
        public void Constructor_Test_VersionMismatch(int major, int minor, int revision)
        {
            // arrange
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            ExcelPackage pck = new ExcelPackage();
            pck.Workbook.Worksheets.Add("Book");
            // metadata
            AddCell(pck, "A1", "MyLibrary");
            AddCell(pck, "A2", "Type");
            AddCell(pck, "A3", "App Version:");
            AddCell(pck, "B3", major + "." + minor + "." + revision);
            AddCell(pck, "B2", "Books");
            AddCell(pck, "A4", "Extracted At:");
            // headers
            AddCell(pck, "A6", "Id");
            AddCell(pck, "B6", "Title");
            AddCell(pck, "C6", "Long Title");
            AddCell(pck, "D6", "ISBN");
            AddCell(pck, "E6", "ISBN13");
            AddCell(pck, "F6", "Authors");
            AddCell(pck, "G6", "Language");
            AddCell(pck, "H6", "Tags");
            AddCell(pck, "I6", "Dewey Decimal");
            AddCell(pck, "J6", "MSRP");
            AddCell(pck, "K6", "Publisher");
            AddCell(pck, "L6", "Format");
            AddCell(pck, "M6", "Date Published");
            AddCell(pck, "N6", "Place of Publication");
            AddCell(pck, "O6", "Edition");
            AddCell(pck, "P6", "Pages");
            AddCell(pck, "Q6", "Dimensions");
            AddCell(pck, "R6", "Overview");
            AddCell(pck, "S6", "Excerpt");
            AddCell(pck, "T6", "Synopsys");
            AddCell(pck, "U6", "Notes");

            // act/assert
            Assert.Throws<FormatException>(() => new BookExcelReader(pck, "Book", new MyLibrary.Models.ValueObjects.AppVersion(1, 5, 0)));
        }
    }//class
}