using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using FakeItEasy;
using MyLibrary.Import;

namespace MyLibrary_Test.Import_Tests
{
    [TestFixture]
    public class TagCsvImportCollection_Tests
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
                new string[]{ "Tag" },
                new string[]{ "tag1" },
                new string[]{ "bogus, tag" },
                new string[]{ "tag2" }
            };
            A.CallTo(() => fakeParser.GetEnumerator()).Returns(data.GetEnumerator());
            var fakeParserService = A.Fake<ICsvParserService>();
            A.CallTo(() => fakeParserService.Get(filePath)).Returns(fakeParser);
            var collection = new TagCsvImportCollection(fakeParserService);

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
            var collection = new TagCsvImportCollection(fakeParserService);

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
                new string[]{ "Tag" },
                new string[]{ "tag1", "tag2"}
            };
            A.CallTo(() => fakeParser.GetEnumerator()).Returns(data.GetEnumerator());
            var fakeParserService = A.Fake<ICsvParserService>();
            A.CallTo(() => fakeParserService.Get(filePath)).Returns(fakeParser);
            var collection = new TagCsvImportCollection(fakeParserService);

            // act/assert
            Assert.Throws<FormatException>(() => collection.LoadFromFile(filePath, ProgressCallback));
        }
    }//class
}