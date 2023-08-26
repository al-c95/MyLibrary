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
using System.Collections.Generic;
using System.Linq;
using NUnit;
using NUnit.Framework;
using OfficeOpenXml;
using FakeItEasy;
using MyLibrary.Models.Entities;
using MyLibrary.Models.Entities.Builders;
using MyLibrary.Import;
using System.Security.Cryptography;

namespace MyLibrary_Test.Import_Tests
{
    [TestFixture]
    public class BookExcelReader_Tests
    {
        int importedCount = 0;
        int skippedCount = 0;

        private void ProgressCallback(int imported, int skipped)
        {
            importedCount = imported;
            skippedCount = skipped;
        }

        [Test]
        public void Read_Test_Validated()
        {
            // arrange
            OfficeOpenXml.ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            ExcelPackage pck = new ExcelPackage();
            pck.Workbook.Worksheets.Add("Book");
            // metadata
            pck.Workbook.Worksheets["Book"].Cells["A1"].Value = "MyLibrary";
            pck.Workbook.Worksheets["Book"].Cells["A2"].Value = "Type";
            pck.Workbook.Worksheets["Book"].Cells["A3"].Value = "App Version:";
            pck.Workbook.Worksheets["Book"].Cells["B3"].Value = "1.5.0";
            pck.Workbook.Worksheets["Book"].Cells["B2"].Value = "Books";
            pck.Workbook.Worksheets["Book"].Cells["A4"].Value = "Extracted At:";
            // headers
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
            // item records
            // item 1
            pck.Workbook.Worksheets["Book"].Cells["A7"].Value = "1";
            pck.Workbook.Worksheets["Book"].Cells["B7"].Value = "Pythonic awesomeness";
            pck.Workbook.Worksheets["Book"].Cells["C7"].Value = "Pythonic awesomeness: coding in Python";
            pck.Workbook.Worksheets["Book"].Cells["D7"].Value = "0123456789";
            pck.Workbook.Worksheets["Book"].Cells["E7"].Value = "012345678901X";
            pck.Workbook.Worksheets["Book"].Cells["F7"].Value = "John Smith-Jones; Jane C. Doe";
            pck.Workbook.Worksheets["Book"].Cells["G7"].Value = "English";
            pck.Workbook.Worksheets["Book"].Cells["H7"].Value = "programming, software development";
            pck.Workbook.Worksheets["Book"].Cells["H8"].Value = "200.5";
            pck.Workbook.Worksheets["Book"].Cells["I8"].Value = "20";
            pck.Workbook.Worksheets["Book"].Cells["J8"].Value = "publisher";
            pck.Workbook.Worksheets["Book"].Cells["K8"].Value = "imaginary";
            pck.Workbook.Worksheets["Book"].Cells["L8"].Value = "2018";
            pck.Workbook.Worksheets["Book"].Cells["M8"].Value = "Australia";
            pck.Workbook.Worksheets["Book"].Cells["N8"].Value = "1st";
            pck.Workbook.Worksheets["Book"].Cells["O8"].Value = "100";
            pck.Workbook.Worksheets["Book"].Cells["P8"].Value = "dimensions";
            pck.Workbook.Worksheets["Book"].Cells["Q8"].Value = "overview";
            pck.Workbook.Worksheets["Book"].Cells["R8"].Value = "excerpt";
            pck.Workbook.Worksheets["Book"].Cells["S8"].Value = "synopsys";
            pck.Workbook.Worksheets["Book"].Cells["T8"].Value = "notes";
            // item 2
            /*
            pck.Workbook.Worksheets["Book"].Cells["A7"].Value = "1f";
            pck.Workbook.Worksheets["Book"].Cells["B7"].Value = "";
            pck.Workbook.Worksheets["Book"].Cells["C7"].Value = "";
            pck.Workbook.Worksheets["Book"].Cells["D7"].Value = "bogus isbn";
            pck.Workbook.Worksheets["Book"].Cells["E7"].Value = "bogus isbn13";
            pck.Workbook.Worksheets["Book"].Cells["F7"].Value = "John Smith-Jones; Jane C. Doe";
            pck.Workbook.Worksheets["Book"].Cells["G7"].Value = "";
            pck.Workbook.Worksheets["Book"].Cells["H7"].Value = "programming, software development";
            pck.Workbook.Worksheets["Book"].Cells["H8"].Value = "bogus Dewey decimal";
            pck.Workbook.Worksheets["Book"].Cells["I8"].Value = "";
            pck.Workbook.Worksheets["Book"].Cells["J8"].Value = "publisher";
            pck.Workbook.Worksheets["Book"].Cells["K8"].Value = "imaginary";
            pck.Workbook.Worksheets["Book"].Cells["L8"].Value = "2018";
            pck.Workbook.Worksheets["Book"].Cells["M8"].Value = "Australia";
            pck.Workbook.Worksheets["Book"].Cells["N8"].Value = "1st";
            pck.Workbook.Worksheets["Book"].Cells["O8"].Value = "";
            pck.Workbook.Worksheets["Book"].Cells["P8"].Value = "dimensions";
            pck.Workbook.Worksheets["Book"].Cells["Q8"].Value = "overview";
            pck.Workbook.Worksheets["Book"].Cells["R8"].Value = "excerpt";
            pck.Workbook.Worksheets["Book"].Cells["S8"].Value = "synopsys";
            pck.Workbook.Worksheets["Book"].Cells["T8"].Value = "notes";
            */
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
            Assert.AreEqual(20, results.ToList()[0].Pages);
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
            Assert.AreEqual(2, results.ToList()[0].Tags.Count);
            Assert.IsTrue(results.ToList()[0].Tags.Any(t => t.Name == "programming"));
            Assert.IsTrue(results.ToList()[0].Tags.Any(t => t.Name == "software development"));
            Assert.AreEqual(2, results.ToList()[0].Authors.Count);
            Assert.IsTrue(results.ToList()[0].Authors.Any(a => a.FirstName == "John" && a.LastName == "Smith-Jones"));
            Assert.IsTrue(results.ToList()[0].Authors.Any(a => a.FirstName == "Jane" && a.LastName == "C. Doe"));
            // progress callback values
            Assert.AreEqual(1, importedCount);
            Assert.AreEqual(1, skippedCount);
        }

        [Test]
        public void Constructor_Test_ExpectedWorksheetNotFound()
        {
            // arrange
            OfficeOpenXml.ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            ExcelPackage pck = new ExcelPackage();
            pck.Workbook.Worksheets.Add("worksheet");

            // act/assert
            Assert.Throws<FormatException>(() => new BookExcelReader(pck, "Book", new MyLibrary.Models.ValueObjects.AppVersion(1, 5, 0)));
        }

        [Test]
        public void Constructor_Test_InvalidMetadata()
        {
            // arrange
            OfficeOpenXml.ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            ExcelPackage pck = new ExcelPackage();
            pck.Workbook.Worksheets.Add("Book");
            // metadata
            pck.Workbook.Worksheets["Book"].Cells["A1"].Value = "bogus entry";
            pck.Workbook.Worksheets["Book"].Cells["A2"].Value = "bogus entry";
            pck.Workbook.Worksheets["Book"].Cells["A3"].Value = "bogus entry";
            pck.Workbook.Worksheets["Book"].Cells["B3"].Value = "1.5.0";
            pck.Workbook.Worksheets["Book"].Cells["B2"].Value = "bogus entry";
            pck.Workbook.Worksheets["Book"].Cells["A4"].Value = "bogus entry";
            // headers
            pck.Workbook.Worksheets["Book"].Cells["A6"].Value = "bogus entry";
            pck.Workbook.Worksheets["Book"].Cells["B6"].Value = "bogus entry";
            pck.Workbook.Worksheets["Book"].Cells["C6"].Value = "bogus entry";
            pck.Workbook.Worksheets["Book"].Cells["D6"].Value = "bogus entry";
            pck.Workbook.Worksheets["Book"].Cells["E6"].Value = "bogus entry";
            pck.Workbook.Worksheets["Book"].Cells["F6"].Value = "bogus entry";
            pck.Workbook.Worksheets["Book"].Cells["G6"].Value = "bogus entry";
            pck.Workbook.Worksheets["Book"].Cells["H6"].Value = "bogus entry";
            pck.Workbook.Worksheets["Book"].Cells["I6"].Value = "bogus entry";
            pck.Workbook.Worksheets["Book"].Cells["J6"].Value = "bogus entry";
            pck.Workbook.Worksheets["Book"].Cells["K6"].Value = "bogus entry";
            pck.Workbook.Worksheets["Book"].Cells["L6"].Value = "bogus entry";
            pck.Workbook.Worksheets["Book"].Cells["M6"].Value = "bogus entry";
            pck.Workbook.Worksheets["Book"].Cells["N6"].Value = "bogus entry";
            pck.Workbook.Worksheets["Book"].Cells["O6"].Value = "bogus entry";
            pck.Workbook.Worksheets["Book"].Cells["P6"].Value = "bogus entry";
            pck.Workbook.Worksheets["Book"].Cells["Q6"].Value = "bogus entry";
            pck.Workbook.Worksheets["Book"].Cells["R6"].Value = "bogus entry";
            pck.Workbook.Worksheets["Book"].Cells["S6"].Value = "bogus entry";
            pck.Workbook.Worksheets["Book"].Cells["T6"].Value = "bogus entry";
            pck.Workbook.Worksheets["Book"].Cells["U6"].Value = "bogus entry";

            // act/assert
            Assert.Throws<FormatException>(() => new BookExcelReader(pck, "Book", new MyLibrary.Models.ValueObjects.AppVersion(1, 5, 0)));
        }

        [Test]
        public void Constructor_Test_InvalidHeaders()
        {
            // arrange
            OfficeOpenXml.ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            ExcelPackage pck = new ExcelPackage();
            pck.Workbook.Worksheets.Add("Book");
            // metadata
            pck.Workbook.Worksheets["Book"].Cells["A1"].Value = "MyLibrary";
            pck.Workbook.Worksheets["Book"].Cells["A2"].Value = "Type";
            pck.Workbook.Worksheets["Book"].Cells["A3"].Value = "App Version:";
            pck.Workbook.Worksheets["Book"].Cells["B3"].Value = "1.5.0";
            pck.Workbook.Worksheets["Book"].Cells["B2"].Value = "Books";
            pck.Workbook.Worksheets["Book"].Cells["A4"].Value = "Extracted At:";
            // headers
            pck.Workbook.Worksheets["Book"].Cells["A6"].Value = "bogus entry";
            pck.Workbook.Worksheets["Book"].Cells["B6"].Value = "bogus entry";
            pck.Workbook.Worksheets["Book"].Cells["C6"].Value = "bogus entry";
            pck.Workbook.Worksheets["Book"].Cells["D6"].Value = "bogus entry";
            pck.Workbook.Worksheets["Book"].Cells["E6"].Value = "bogus entry";
            pck.Workbook.Worksheets["Book"].Cells["F6"].Value = "bogus entry";
            pck.Workbook.Worksheets["Book"].Cells["G6"].Value = "bogus entry";
            pck.Workbook.Worksheets["Book"].Cells["H6"].Value = "bogus entry";
            pck.Workbook.Worksheets["Book"].Cells["I6"].Value = "bogus entry";
            pck.Workbook.Worksheets["Book"].Cells["J6"].Value = "bogus entry";
            pck.Workbook.Worksheets["Book"].Cells["K6"].Value = "bogus entry";
            pck.Workbook.Worksheets["Book"].Cells["L6"].Value = "bogus entry";
            pck.Workbook.Worksheets["Book"].Cells["M6"].Value = "bogus entry";
            pck.Workbook.Worksheets["Book"].Cells["N6"].Value = "bogus entry";
            pck.Workbook.Worksheets["Book"].Cells["O6"].Value = "bogus entry";
            pck.Workbook.Worksheets["Book"].Cells["P6"].Value = "bogus entry";
            pck.Workbook.Worksheets["Book"].Cells["Q6"].Value = "bogus entry";
            pck.Workbook.Worksheets["Book"].Cells["R6"].Value = "bogus entry";
            pck.Workbook.Worksheets["Book"].Cells["S6"].Value = "bogus entry";
            pck.Workbook.Worksheets["Book"].Cells["T6"].Value = "bogus entry";
            pck.Workbook.Worksheets["Book"].Cells["U6"].Value = "bogus entry";

            // act/assert
            Assert.Throws<FormatException>(() => new BookExcelReader(pck, "Book", new MyLibrary.Models.ValueObjects.AppVersion(1, 5, 0)));
        }

        [TestCase(3, 0, 0)]
        [TestCase(0, 1, 0)]
        public void Constructor_Test_VersionMismatch(int major, int minor, int revision)
        {
            // arrange
            OfficeOpenXml.ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            ExcelPackage pck = new ExcelPackage();
            pck.Workbook.Worksheets.Add("Book");
            // metadata
            pck.Workbook.Worksheets["Book"].Cells["A1"].Value = "MyLibrary";
            pck.Workbook.Worksheets["Book"].Cells["A2"].Value = "Type";
            pck.Workbook.Worksheets["Book"].Cells["A3"].Value = "App Version:";
            pck.Workbook.Worksheets["Book"].Cells["B3"].Value = major + "." + minor + "." + revision;
            pck.Workbook.Worksheets["Book"].Cells["B2"].Value = "Books";
            pck.Workbook.Worksheets["Book"].Cells["A4"].Value = "Extracted At:";
            // headers
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

            // act/assert
            Assert.Throws<FormatException>(() => new BookExcelReader(pck, "Book", new MyLibrary.Models.ValueObjects.AppVersion(1, 5, 0)));
        }
    }//class
}