using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using NUnit;
using NUnit.Framework;
using FakeItEasy;
using MyLibrary.Models.Entities;
using MyLibrary.Models.Csv;

namespace MyLibrary_Test.Models_Tests.Csv_Tests
{
    [TestFixture]
    class PublisherCsvImport_Tests
    {
        [Test]
        public void Constructor_Test_Ok()
        {
            string[] lines = new string[]
            {
                "Publisher",
                "publisher1",
                "publisher2"
            };

            Assert.DoesNotThrow(() => new PublisherCsvImport(lines));
        }
        
        [Test]
        public void Constructor_Test_Invalid()
        {
            string[] lines = new string[]
            {
                "bogus header",
                "publisher1",
                "publisher2"
            };

            Assert.Throws<FormatException>(() => new PublisherCsvImport(lines));
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

            var import = new PublisherCsvImport(lines);
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
