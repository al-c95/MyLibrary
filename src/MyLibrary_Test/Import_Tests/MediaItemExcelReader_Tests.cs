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

namespace MyLibrary_Test.Import_Tests
{
    [TestFixture]
    public class MediaItemExcelReader_Tests
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
            pck.Workbook.Worksheets.Add("Media item");
            // metadata
            pck.Workbook.Worksheets["Media item"].Cells["A1"].Value = "MyLibrary";
            pck.Workbook.Worksheets["Media item"].Cells["A2"].Value = "Type";
            pck.Workbook.Worksheets["Media item"].Cells["A3"].Value = "App Version:";
            pck.Workbook.Worksheets["Media item"].Cells["B3"].Value = "1.5.0";
            pck.Workbook.Worksheets["Media item"].Cells["B2"].Value = "Media items";
            pck.Workbook.Worksheets["Media item"].Cells["A4"].Value = "Extracted At:";
            // headers
            pck.Workbook.Worksheets["Media item"].Cells["A6"].Value = "Id";
            pck.Workbook.Worksheets["Media item"].Cells["B6"].Value = "Title";
            pck.Workbook.Worksheets["Media item"].Cells["C6"].Value = "Type";
            pck.Workbook.Worksheets["Media item"].Cells["D6"].Value = "Number";
            pck.Workbook.Worksheets["Media item"].Cells["E6"].Value = "Running Time";
            pck.Workbook.Worksheets["Media item"].Cells["F6"].Value = "Release Year";
            pck.Workbook.Worksheets["Media item"].Cells["G6"].Value = "Tags";
            pck.Workbook.Worksheets["Media item"].Cells["H6"].Value = "Notes";
            // item records
            // item 1
            pck.Workbook.Worksheets["Media item"].Cells["A7"].Value = "1";
            pck.Workbook.Worksheets["Media item"].Cells["B7"].Value = "Funny movie";
            pck.Workbook.Worksheets["Media item"].Cells["C7"].Value = "Dvd";
            pck.Workbook.Worksheets["Media item"].Cells["D7"].Value = "234108974";
            pck.Workbook.Worksheets["Media item"].Cells["E7"].Value = "125";
            pck.Workbook.Worksheets["Media item"].Cells["F7"].Value = "2022";
            pck.Workbook.Worksheets["Media item"].Cells["G7"].Value = "comedy, drama";
            pck.Workbook.Worksheets["Media item"].Cells["H7"].Value = "this is a test.";
            // item 2
            pck.Workbook.Worksheets["Media item"].Cells["A8"].Value = "2";
            pck.Workbook.Worksheets["Media item"].Cells["B8"].Value = "Funny movie 2";
            pck.Workbook.Worksheets["Media item"].Cells["C8"].Value = "bogus type";
            pck.Workbook.Worksheets["Media item"].Cells["D8"].Value = "234547890";
            pck.Workbook.Worksheets["Media item"].Cells["E8"].Value = "125";
            pck.Workbook.Worksheets["Media item"].Cells["F8"].Value = "2023";
            pck.Workbook.Worksheets["Media item"].Cells["G8"].Value = "comedy, drama";
            pck.Workbook.Worksheets["Media item"].Cells["H8"].Value = "this is a test.";
            // item 3
            pck.Workbook.Worksheets["Media item"].Cells["A9"].Value = "bogus id";
            pck.Workbook.Worksheets["Media item"].Cells["B9"].Value = "Funny movie 3";
            pck.Workbook.Worksheets["Media item"].Cells["C9"].Value = "BluRay";
            pck.Workbook.Worksheets["Media item"].Cells["D9"].Value = "2342347907890";
            pck.Workbook.Worksheets["Media item"].Cells["E9"].Value = "125";
            pck.Workbook.Worksheets["Media item"].Cells["F9"].Value = "2023";
            pck.Workbook.Worksheets["Media item"].Cells["G9"].Value = "comedy, drama";
            pck.Workbook.Worksheets["Media item"].Cells["H9"].Value = "this is a test.";
            // reader
            MediaItemExcelReader excelReader = new MediaItemExcelReader(pck, "Media item", new MyLibrary.Models.ValueObjects.AppVersion(1,5,0));

            // act
            var results = excelReader.Read(ProgressCallback);

            // assert
            // items
            Assert.AreEqual(1, results.Count());
            Assert.AreEqual(1, results.ToList()[0].Id);
            Assert.AreEqual("Funny movie", results.ToList()[0].Title);
            Assert.AreEqual(ItemType.Dvd, results.ToList()[0].Type);
            Assert.AreEqual(234108974, results.ToList()[0].Number);
            Assert.AreEqual(125, results.ToList()[0].RunningTime);
            Assert.AreEqual(2022, results.ToList()[0].ReleaseYear);
            Assert.AreEqual("this is a test.", results.ToList()[0].Notes);
            Assert.AreEqual(2, results.ToList()[0].Tags.Count);
            Assert.IsTrue(results.ToList()[0].Tags.Any(t => t.Name == "comedy"));
            Assert.IsTrue(results.ToList()[0].Tags.Any(t => t.Name == "drama"));
            // progress callback values
            Assert.AreEqual(1, importedCount);
            Assert.AreEqual(2, skippedCount);
        }

        [Test]
        public void Constructor_Test_ExpectedWorksheetNotFound()
        {
            // arrange
            OfficeOpenXml.ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            ExcelPackage pck = new ExcelPackage();
            pck.Workbook.Worksheets.Add("worksheet");

            // act/assert
            Assert.Throws<FormatException>(() => new MediaItemExcelReader(pck, "Media item", new MyLibrary.Models.ValueObjects.AppVersion(1, 5, 0)));
        }

        [Test]
        public void Constructor_Test_InvalidMetadata()
        {
            // arrange
            OfficeOpenXml.ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            ExcelPackage pck = new ExcelPackage();
            pck.Workbook.Worksheets.Add("Media item");
            // metadata
            pck.Workbook.Worksheets["Media item"].Cells["A1"].Value = "bogus entry";
            pck.Workbook.Worksheets["Media item"].Cells["A2"].Value = "bogus entry";
            pck.Workbook.Worksheets["Media item"].Cells["A3"].Value = "bogus entry";
            pck.Workbook.Worksheets["Media item"].Cells["B3"].Value = "1.5.0";
            pck.Workbook.Worksheets["Media item"].Cells["B2"].Value = "bogus entry";
            pck.Workbook.Worksheets["Media item"].Cells["A4"].Value = "bogus entry";
            // headers
            pck.Workbook.Worksheets["Media item"].Cells["A6"].Value = "bogus entry";
            pck.Workbook.Worksheets["Media item"].Cells["B6"].Value = "bogus entry";
            pck.Workbook.Worksheets["Media item"].Cells["C6"].Value = "bogus entry";
            pck.Workbook.Worksheets["Media item"].Cells["D6"].Value = "bogus entry";
            pck.Workbook.Worksheets["Media item"].Cells["E6"].Value = "bogus entry";
            pck.Workbook.Worksheets["Media item"].Cells["F6"].Value = "bogus entry";
            pck.Workbook.Worksheets["Media item"].Cells["G6"].Value = "bogus entry";
            pck.Workbook.Worksheets["Media item"].Cells["H6"].Value = "bogus entry";

            // act/assert
            Assert.Throws<FormatException>(() => new MediaItemExcelReader(pck, "Media item", new MyLibrary.Models.ValueObjects.AppVersion(1, 5, 0)));
        }

        [Test]
        public void Constructor_Test_InvalidHeaders()
        {
            // arrange
            OfficeOpenXml.ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            ExcelPackage pck = new ExcelPackage();
            pck.Workbook.Worksheets.Add("Media item");
            // metadata
            pck.Workbook.Worksheets["Media item"].Cells["A1"].Value = "MyLibrary";
            pck.Workbook.Worksheets["Media item"].Cells["A2"].Value = "Type";
            pck.Workbook.Worksheets["Media item"].Cells["A3"].Value = "App Version:";
            pck.Workbook.Worksheets["Media item"].Cells["B3"].Value = "1.5.0";
            pck.Workbook.Worksheets["Media item"].Cells["B2"].Value = "Media items";
            pck.Workbook.Worksheets["Media item"].Cells["A4"].Value = "Extracted At:";
            // headers
            pck.Workbook.Worksheets["Media item"].Cells["A6"].Value = "bogus entry";
            pck.Workbook.Worksheets["Media item"].Cells["B6"].Value = "bogus entry";
            pck.Workbook.Worksheets["Media item"].Cells["C6"].Value = "bogus entry";
            pck.Workbook.Worksheets["Media item"].Cells["D6"].Value = "bogus entry";
            pck.Workbook.Worksheets["Media item"].Cells["E6"].Value = "bogus entry";
            pck.Workbook.Worksheets["Media item"].Cells["F6"].Value = "bogus entry";
            pck.Workbook.Worksheets["Media item"].Cells["G6"].Value = "bogus entry";
            pck.Workbook.Worksheets["Media item"].Cells["H6"].Value = "bogus entry";

            // act/assert
            Assert.Throws<FormatException>(() => new MediaItemExcelReader(pck, "Media item", new MyLibrary.Models.ValueObjects.AppVersion(1, 5, 0)));
        }

        [TestCase(3,0,0)]
        [TestCase(0,1,0)]
        public void Constructor_Test_VersionMismatch(int major, int minor, int revision)
        {
            // arrange
            OfficeOpenXml.ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            ExcelPackage pck = new ExcelPackage();
            pck.Workbook.Worksheets.Add("Media item");
            // metadata
            pck.Workbook.Worksheets["Media item"].Cells["A1"].Value = "MyLibrary";
            pck.Workbook.Worksheets["Media item"].Cells["A2"].Value = "Type";
            pck.Workbook.Worksheets["Media item"].Cells["A3"].Value = "App Version:";
            pck.Workbook.Worksheets["Media item"].Cells["B3"].Value = major + "." + minor + "." + revision;
            pck.Workbook.Worksheets["Media item"].Cells["B2"].Value = "Media items";
            pck.Workbook.Worksheets["Media item"].Cells["A4"].Value = "Extracted At:";
            // headers
            pck.Workbook.Worksheets["Media item"].Cells["A6"].Value = "Id";
            pck.Workbook.Worksheets["Media item"].Cells["B6"].Value = "Title";
            pck.Workbook.Worksheets["Media item"].Cells["C6"].Value = "Type";
            pck.Workbook.Worksheets["Media item"].Cells["D6"].Value = "Number";
            pck.Workbook.Worksheets["Media item"].Cells["E6"].Value = "Running Time";
            pck.Workbook.Worksheets["Media item"].Cells["F6"].Value = "Release Year";
            pck.Workbook.Worksheets["Media item"].Cells["G6"].Value = "Tags";
            pck.Workbook.Worksheets["Media item"].Cells["H6"].Value = "Notes";

            // act/assert
            Assert.Throws<FormatException>(() => new MediaItemExcelReader(pck, "Media item", new MyLibrary.Models.ValueObjects.AppVersion(1, 5, 0)));
        }
    }//class
}