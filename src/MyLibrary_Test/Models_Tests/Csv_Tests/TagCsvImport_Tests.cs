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
    class TagCsvImport_Tests
    {
        [Test]
        public void Constructor_Test_Ok()
        {
            string[] lines = new string[]
            {
                "Tag",
                "tag1",
                "tag2"
            };

            Assert.DoesNotThrow(() => new TagCsvImport(lines));
        }

        [Test]
        public void Constructor_Test_Invalid()
        {
            string[] lines = new string[]
            {
                "bogus header",
                "tag1",
                "tag2"
            };

            Assert.Throws<FormatException>(() => new TagCsvImport(lines));
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

            var import = new TagCsvImport(lines);
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
    }
}
