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
    class AuthorCsvImport_Tests
    {
        [Test]
        public void Constructor_Test_Ok()
        {
            string[] lines = new string[]
            {
                "First Name,Last Name",
                "John,Smith",
                "Jane,Doe"
            };

            Assert.DoesNotThrow(() => new AuthorCsvImport(lines));
        }

        [Test]
        public void Constructor_Test_Invalid()
        {
            string[] lines = new string[]
            {
                "bogus header",
                "John,Smith",
                "Jane,Doe"
            };

            Assert.Throws<FormatException>(() => new AuthorCsvImport(lines));
        }

        [Test]
        public void Enumeration_Test()
        {
            string[] lines = new string[]
            {
                "First Name,Last Name",
                "John,Smith",
                "Jane H.,Doe",
                "bogus author"
            };

            var import = new AuthorCsvImport(lines);
            List<CsvRowResult> results = new List<CsvRowResult>();
            foreach (var result in import)
            {
                results.Add(result);
            }

            Assert.IsTrue(results.Count == 3);
            Assert.IsTrue(results.Any(r => r.Row == 2 && r.RowStatus == CsvRowResult.Status.SUCCESS && ((Author)r.Entity).FirstName.Equals("John") && ((Author)r.Entity).LastName.Equals("Smith")));
            Assert.IsTrue(results.Any(r => r.Row == 3 && r.RowStatus == CsvRowResult.Status.SUCCESS && ((Author)r.Entity).FirstName.Equals("Jane H.") && ((Author)r.Entity).LastName.Equals("Doe")));
            Assert.IsTrue(results.Any(r => r.Row == 4 && r.RowStatus == CsvRowResult.Status.ERROR));
        }
    }
}
