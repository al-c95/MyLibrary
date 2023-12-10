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
using MyLibrary.Models.Entities;
using MyLibrary.Import;

namespace MyLibrary_Test.Import_Tests
{
    [TestFixture]
    public class MediaItemExcelReader_Tests
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
            pck.Workbook.Worksheets["Media item"].Cells[address].Value = value;
        }

        [Test]
        public void Read_Test_Validated()
        {
            // arrange
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            ExcelPackage pck = new ExcelPackage();         
            pck.Workbook.Worksheets.Add("Media item");
            // metadata
            AddCell(pck, "A1", "MyLibrary");
            AddCell(pck, "A2", "Type");
            AddCell(pck, "A3", "App Version:");
            AddCell(pck, "B3", "1.5.0");
            AddCell(pck, "B2", "Media items");
            AddCell(pck, "A4", "Extracted At:");
            // headers
            AddCell(pck, "A6", "Id");
            AddCell(pck, "B6", "Title");
            AddCell(pck, "C6", "Type");
            AddCell(pck, "D6", "Number");
            AddCell(pck, "E6", "Running Time");
            AddCell(pck, "F6", "Release Year");
            AddCell(pck, "G6", "Tags");
            AddCell(pck, "H6", "Notes");
            // item records
            // item 1
            AddCell(pck, "A7", "1");
            AddCell(pck, "B7", "Funny movie");
            AddCell(pck, "C7", "Dvd");
            AddCell(pck, "D7", "234108974");
            AddCell(pck, "E7", "125");
            AddCell(pck, "F7", "2022");
            AddCell(pck, "G7", "comedy, drama");
            AddCell(pck, "H7", "this is a test.");
            // item 2
            AddCell(pck, "A8", "2");
            AddCell(pck, "B8", "Funny movie 2");
            AddCell(pck, "C8", "bogus type");
            AddCell(pck, "D8", "234547890");
            AddCell(pck, "E8", "125");
            AddCell(pck, "F8", "2023");
            AddCell(pck, "G8", "comedy, drama");
            AddCell(pck, "H8", "this is a test");
            // item 3
            AddCell(pck, "A9", "bogus id");
            AddCell(pck, "B9", "Funny movie 3");
            AddCell(pck, "C9", "BluRay");
            AddCell(pck, "D9", "2342347907890");
            AddCell(pck, "E9", "125");
            AddCell(pck, "F9", "2023");
            AddCell(pck, "G9", "comedy, drama");
            AddCell(pck, "H9", "this is a test.");
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
            Assert.AreEqual(1, parsedCount);
            Assert.AreEqual(2, skippedCount);
        }

        [Test]
        public void Constructor_Test_ExpectedWorksheetNotFound()
        {
            // arrange
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            ExcelPackage pck = new ExcelPackage();
            pck.Workbook.Worksheets.Add("worksheet");

            // act/assert
            Assert.Throws<FormatException>(() => new MediaItemExcelReader(pck, "Media item", new MyLibrary.Models.ValueObjects.AppVersion(1, 5, 0)));
        }

        [Test]
        public void Constructor_Test_InvalidMetadata()
        {
            // arrange
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            ExcelPackage pck = new ExcelPackage();
            pck.Workbook.Worksheets.Add("Media item");
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

            // act/assert
            Assert.Throws<FormatException>(() => new MediaItemExcelReader(pck, "Media item", new MyLibrary.Models.ValueObjects.AppVersion(1, 5, 0)));
        }

        [Test]
        public void Constructor_Test_InvalidHeaders()
        {
            // arrange
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            ExcelPackage pck = new ExcelPackage();
            pck.Workbook.Worksheets.Add("Media item");
            // metadata
            AddCell(pck, "A1", "MyLibrary");
            AddCell(pck, "A2", "Type");
            AddCell(pck, "A3", "App Version:");
            AddCell(pck, "B3", "1.5.0");
            AddCell(pck, "B2", "Media items");
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

            // act/assert
            Assert.Throws<FormatException>(() => new MediaItemExcelReader(pck, "Media item", new MyLibrary.Models.ValueObjects.AppVersion(1, 5, 0)));
        }

        [TestCase(3,0,0)]
        [TestCase(0,1,0)]
        public void Constructor_Test_VersionMismatch(int major, int minor, int revision)
        {
            // arrange
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            ExcelPackage pck = new ExcelPackage();
            pck.Workbook.Worksheets.Add("Media item");
            // metadata
            AddCell(pck, "A1", "MyLibrary");
            AddCell(pck, "A2", "Type");
            AddCell(pck, "A3", "App Version:");
            AddCell(pck, "B3", major + "." + minor + "." + revision);
            AddCell(pck, "B2", "Media items");
            AddCell(pck, "A4", "Extracted At:");
            // headers
            AddCell(pck, "A6", "Id");
            AddCell(pck, "B6", "Title");
            AddCell(pck, "C6", "Type");
            AddCell(pck, "D6", "Number");
            AddCell(pck, "E6", "Running Time");
            AddCell(pck, "F6", "Release Year");
            AddCell(pck, "G6", "Tags");
            AddCell(pck, "H6", "Notes");

            // act/assert
            Assert.Throws<FormatException>(() => new MediaItemExcelReader(pck, "Media item", new MyLibrary.Models.ValueObjects.AppVersion(1, 5, 0)));
        }
    }//class
}