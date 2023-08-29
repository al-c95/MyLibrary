using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using NUnit.Framework;
using FakeItEasy;
using MyLibrary.Models.Entities;
using MyLibrary.Models.BusinessLogic.ImportCsv;
using MyLibrary;

namespace MyLibrary_Test.Models_Tests.BusinessLogic_Tests.ImportCsv_Tests
{
    [TestFixture]
    class PublisherCsvImport_Tests
    {
        [Test]
        public void Constructor_Test_Ok()
        {
            // arrange
            string[] lines = new string[]
            {
                "Publisher",
                "publisher1",
                "publisher2"
            };
            var fakeService = A.Fake<IPublisherService>();

            // act/assert
            Assert.DoesNotThrow(() => new PublisherCsvImport(lines, fakeService));
        }

        [Test]
        public void Constructor_Test_Invalid()
        {
            // arrange
            string[] lines = new string[]
            {
                "bogus header",
                "publisher1",
                "publisher2"
            };
            var fakeService = A.Fake<IPublisherService>();

            // act/assert
            Assert.Throws<FormatException>(() => new PublisherCsvImport(lines, fakeService));
        }

        [Test]
        public void GetTypeName_Test()
        {
            // arrange
            string expectedResult = "Publisher";
            var fakeService = A.Fake<IPublisherService>();
            var import = new PublisherCsvImport(new string[] { "Publisher" }, fakeService);

            // act
            string actualResult = import.GetTypeName;

            // assert
            Assert.AreEqual(expectedResult, actualResult);
        }

        [Test]
        public async Task AddIfNotExists_Test_Exists()
        {
            // arrange
            Publisher publisher = new Publisher("some_publisher");
            CsvRowResult row = new CsvRowResult(1, CsvRowResult.Status.SUCCESS, publisher, "Publisher");
            var fakeService = A.Fake<IPublisherService>();
            A.CallTo(() => fakeService.ExistsWithName("some_publisher")).Returns(true);
            PublisherCsvImport import = new PublisherCsvImport(new string[] { "Publisher" }, fakeService);

            // act
            bool result = await import.AddIfNotExists(row);

            // assert
            Assert.IsFalse(result);
            A.CallTo(() => fakeService.Add(publisher)).MustNotHaveHappened();
        }

        [Test]
        public async Task AddIfNotExists_Test_DoesNotExist()
        {
            // arrange
            Publisher publisher = new Publisher("some_publisher");
            CsvRowResult row = new CsvRowResult(1, CsvRowResult.Status.SUCCESS, publisher, "Publisher");
            var fakeService = A.Fake<IPublisherService>();
            A.CallTo(() => fakeService.ExistsWithName("some_publisher")).Returns(false);
            PublisherCsvImport import = new PublisherCsvImport(new string[] { "Publisher" }, fakeService);

            // act
            bool result = await import.AddIfNotExists(row);

            // assert
            Assert.IsTrue(result);
            A.CallTo(() => fakeService.Add(publisher)).MustHaveHappened();
        }

        [Test]
        public void Enumeration_Test()
        {
            string[] lines = new string[]
            {
                "Publisher",
                "publisher1",
                "",
                "publisher2"
            };
            var fakeService = A.Fake<IPublisherService>();
            var import = new PublisherCsvImport(lines, fakeService);
            List<CsvRowResult> results = new List<CsvRowResult>();
            foreach (var result in import)
            {
                results.Add(result);
            }

            Assert.IsTrue(results.Count == 3);
            Assert.IsTrue(results.Any(r => r.Row == 2 && r.RowStatus == CsvRowResult.Status.SUCCESS && ((Publisher)r.Entity).Name.Equals("publisher1")));
            Assert.IsTrue(results.Any(r => r.Row == 3 && r.RowStatus == CsvRowResult.Status.ERROR));
            Assert.IsTrue(results.Any(r => r.Row == 4 && r.RowStatus == CsvRowResult.Status.SUCCESS && ((Publisher)r.Entity).Name.Equals("publisher2")));
        }
    }//class
}
