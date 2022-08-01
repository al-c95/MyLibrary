using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using NUnit;
using NUnit.Framework;
using FakeItEasy;
using MyLibrary.Models.Entities;
using MyLibrary.Models.Csv;
using MyLibrary.Models.BusinessLogic;

namespace MyLibrary_Test.Models_Tests.Csv_Tests
{
    [TestFixture]
    class TagCsvImport_Tests
    {
        [Test]
        public void Constructor_Test_Ok()
        {
            // arrange
            string[] lines = new string[]
            {
                "Tag",
                "tag1",
                "tag2"
            };
            var fakeService = A.Fake<ITagService>();

            // act/assert
            Assert.DoesNotThrow(() => new TagCsvImport(lines, fakeService));
        }

        [Test]
        public void Constructor_Test_Invalid()
        {
            // arrange
            string[] lines = new string[]
            {
                "bogus header",
                "tag1",
                "tag2"
            };
            var fakeService = A.Fake<ITagService>();

            // act/assert
            Assert.Throws<FormatException>(() => new TagCsvImport(lines, fakeService));
        }

        [Test]
        public void Enumeration_Test()
        {
            string[] lines = new string[]
            {
                "Tag",
                "tag1",
                "tag2",
                "tag, tag"
            };
            var fakeService = A.Fake<ITagService>();
            var import = new TagCsvImport(lines, fakeService);
            List<CsvRowResult> results = new List<CsvRowResult>();
            foreach (var result in import)
            {
                results.Add(result);
            }

            Assert.IsTrue(results.Count == 3);
            Assert.IsTrue(results.Any(r => r.Row == 2 && r.RowStatus == CsvRowResult.Status.SUCCESS && ((Tag)r.Entity).Name.Equals("tag1") ));
            Assert.IsTrue(results.Any(r => r.Row == 3 && r.RowStatus == CsvRowResult.Status.SUCCESS && ((Tag)r.Entity).Name.Equals("tag2")));
            Assert.IsTrue(results.Any(r => r.Row == 4 && r.RowStatus == CsvRowResult.Status.ERROR && r.Entity is null));
        }

        [Test]
        public void GetTypeName_Test()
        {
            // arrange
            string expectedResult = "Tag";
            var import = new TagCsvImport(new string[] { "Tag" }, null);

            // act
            string actualResult = import.GetTypeName;

            // assert
            Assert.AreEqual(expectedResult, actualResult);
        }

        [Test]
        public async Task AddIfNotExists_Test_Exists()
        {
            // arrange
            Tag tag = new Tag();
            tag.Name = "tag";
            CsvRowResult row = new CsvRowResult(1, CsvRowResult.Status.SUCCESS, tag, "Tag");
            var fakeService = A.Fake<ITagService>();
            A.CallTo(() => fakeService.ExistsWithName("tag")).Returns(true);
            TagCsvImport import = new TagCsvImport(new string[] { "Tag" }, fakeService);

            // act
            bool result = await import.AddIfNotExists(row);

            // assert
            Assert.IsFalse(result);
            A.CallTo(() => fakeService.Add(tag)).MustNotHaveHappened();
        }

        [Test]
        public async Task AddIfNotExists_Test_DoesNotExist()
        {
            // arrange
            Tag tag = new Tag();
            tag.Name = "tag";
            CsvRowResult row = new CsvRowResult(1, CsvRowResult.Status.SUCCESS, tag, "Tag");
            var fakeService = A.Fake<ITagService>();
            A.CallTo(() => fakeService.ExistsWithName("tag")).Returns(false);
            TagCsvImport import = new TagCsvImport(new string[] { "Tag" }, fakeService);

            // act
            bool result = await import.AddIfNotExists(row);

            // assert
            Assert.IsTrue(result);
            A.CallTo(() => fakeService.Add(tag)).MustHaveHappened();
        }
    }//class
}