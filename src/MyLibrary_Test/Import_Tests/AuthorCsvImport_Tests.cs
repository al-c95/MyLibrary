using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using NUnit;
using NUnit.Framework;
using FakeItEasy;
using MyLibrary.Models.Entities;
using MyLibrary.Import;
using MyLibrary;

namespace MyLibrary_Test.Models_Tests.BusinessLogic_Tests.ImportCsv_Tests
{
    /*
    [TestFixture]
    class AuthorCsvImport_Tests
    {
        [Test]
        public void GetTypeName_Test()
        {
            // arrange
            string expectedResult = "Author";
            var import = new AuthorCsvImport(new string[] { "First Name,Last Name" }, null);

            // act
            string actualResult = import.GetTypeName;

            // assert
            Assert.AreEqual(expectedResult, actualResult);
        }

        [Test]
        public void Constructor_Test_Ok()
        {
            // arrange
            string[] lines = new string[]
            {
                "First Name,Last Name",
                "John,Smith",
                "Jane,Doe"
            };
            var fakeService = A.Fake<IAuthorService>();

            // act/assert
            Assert.DoesNotThrow(() => new AuthorCsvImport(lines, fakeService));
        }

        [Test]
        public void Constructor_Test_Invalid()
        {
            // arrange
            string[] lines = new string[]
            {
                "bogus header",
                "John,Smith",
                "Jane,Doe"
            };
            var fakeService = A.Fake<IAuthorService>();

            // act/assert
            Assert.Throws<FormatException>(() => new AuthorCsvImport(lines, fakeService));
        }

        [Test]
        public async Task AddIfNotExists_Test_Exists()
        {
            // arrange
            Author author = new Author { FirstName = "John", LastName = "Smith" };
            CsvRowResult row = new CsvRowResult(1, CsvRowResult.Status.SUCCESS, author, "Author");
            var fakeService = A.Fake<IAuthorService>();
            A.CallTo(() => fakeService.ExistsWithName("John", "Smith")).Returns(true);
            AuthorCsvImport import = new AuthorCsvImport(new string[] { "First Name,Last Name" }, fakeService);

            // act
            bool result = await import.AddIfNotExists(row);

            // assert
            Assert.IsFalse(result);
            A.CallTo(() => fakeService.Add(author)).MustNotHaveHappened();
        }

        [Test]
        public async Task AddIfNotExists_Test_DoesNotExist()
        {
            // arrange
            Author author = new Author { FirstName = "John", LastName = "Smith" };
            CsvRowResult row = new CsvRowResult(1, CsvRowResult.Status.SUCCESS, author, "Author");
            var fakeService = A.Fake<IAuthorService>();
            A.CallTo(() => fakeService.ExistsWithName("John", "Smith")).Returns(false);
            AuthorCsvImport import = new AuthorCsvImport(new string[] { "First Name,Last Name" }, fakeService);

            // act
            bool result = await import.AddIfNotExists(row);

            // assert
            Assert.IsTrue(result);
            A.CallTo(() => fakeService.Add(author)).MustHaveHappened();
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
            var fakeService = A.Fake<IAuthorService>();
            var import = new AuthorCsvImport(lines, fakeService);
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
    }//class
    */
}
