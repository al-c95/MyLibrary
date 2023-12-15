using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using MyLibrary.Import;
using FakeItEasy;

namespace MyLibrary_Test.Import_Tests
{
    [TestFixture]
    public class AuthorCsvImportCollection_Tests
    {
        int _parsedCount;
        int _skippedCount;
        string filePath = @"C:\path\to\my\file.csv";

        void ProgressCallback(int parsedCount, int skippedCount)
        {
            this._parsedCount = parsedCount;
            this._skippedCount = skippedCount;
        }

        [Test]
        public void LoadFromFile_Test_Ok()
        {
            // arrange
            var fakeParser = A.Fake<ICsvParser>();
            var data = new List<string[]>
            {
                new string[]{ "First Name", "Last Name" },
                new string[]{ "John", "Smith" },
                new string[]{ "Jane H.", "Doe" },
                new string[]{ "bogus", "author1" }
            };
            A.CallTo(() => fakeParser.GetEnumerator()).Returns(data.GetEnumerator());
            var fakeParserService = A.Fake<ICsvParserService>();
            A.CallTo(() => fakeParserService.Get(filePath)).Returns(fakeParser);
            var collection = new AuthorCsvImportCollection(fakeParserService);

            // act
            collection.LoadFromFile(filePath, ProgressCallback);

            // assert
            Assert.AreEqual(2, _parsedCount);
            Assert.AreEqual(2, collection.ParsedCount);
            Assert.AreEqual(1, _skippedCount);
            Assert.AreEqual(1, collection.SkippedCount);
            Assert.AreEqual(2, collection.GetAll().Count());
        }

        [Test]
        public void LoadFromFile_Test_InvalidHeader()
        {
            // arrange
            var fakeParser = A.Fake<ICsvParser>();
            var data = new List<string[]>
            {
                new string[]{ "bogus header" }
            };
            A.CallTo(() => fakeParser.GetEnumerator()).Returns(data.GetEnumerator());
            var fakeParserService = A.Fake<ICsvParserService>();
            A.CallTo(() => fakeParserService.Get(filePath)).Returns(fakeParser);
            var collection = new AuthorCsvImportCollection(fakeParserService);

            // act/assert
            Assert.Throws<FormatException>(() => collection.LoadFromFile(filePath, ProgressCallback));
        }

        [Test]
        public void LoadFromFile_Test_InvalidData()
        {
            // arrange
            var fakeParser = A.Fake<ICsvParser>();
            var data = new List<string[]>
            {
                new string[]{ "First Name", "Last Name" },
                new string[]{ "bogus" }
            };
            A.CallTo(() => fakeParser.GetEnumerator()).Returns(data.GetEnumerator());
            var fakeParserService = A.Fake<ICsvParserService>();
            A.CallTo(() => fakeParserService.Get(filePath)).Returns(fakeParser);
            var collection = new AuthorCsvImportCollection(fakeParserService);

            // act/assert
            Assert.Throws<FormatException>(() => collection.LoadFromFile(filePath, ProgressCallback));
        }
    }//class
}