using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using NUnit;
using NUnit.Framework;
using MyLibrary.Models.Entities;
using MyLibrary.Models.BusinessLogic;

namespace MyLibrary_Test.Models_Tests.BusinessLogic_Tests
{
    [TestFixture]
    public class AuthorService_Tests
    {
        [TestCase("John", true)]
        [TestCase("Doe", true)]
        [TestCase("bogus", false)]
        public async Task ExistsWithName_Test(string name, bool expectedResult)
        {
            // arrange
            MockAuthorService service = new MockAuthorService();

            // act
            bool actualResult = await service.ExistsWithName(name);

            // assert
            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestCase("John", "Smith", true)]
        [TestCase("John", "Doe", false)]
        [TestCase("Korky", "Buchek", false)]
        public async Task ExistsWithName_FirstNameLastName_Test(string firstName, string lastName, bool expectedResult)
        {
            // arrange
            MockAuthorService service = new MockAuthorService();

            // act
            bool actualResult = await service.ExistsWithName(firstName, lastName);

            // assert
            Assert.AreEqual(expectedResult, actualResult);
        }

        class MockAuthorService : AuthorService
        {
            public async override Task<IEnumerable<Author>> GetAll()
            {
                List<Author> authors = new List<Author>();
                await Task.Run(() =>
                {
                    authors.Add(new Author
                    {
                        FirstName="John",
                        LastName="Smith"
                    });

                    authors.Add(new Author
                    {
                        FirstName = "Jane",
                        LastName = "Doe"
                    });
                });

                return authors;
            }
        }//class
    }//class
}
