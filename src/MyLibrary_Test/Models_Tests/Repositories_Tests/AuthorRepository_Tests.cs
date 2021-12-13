using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using NUnit;
using NUnit.Framework;
using FakeItEasy;
using MyLibrary.BusinessLogic.Repositories;
using MyLibrary.Models.Entities;
using MyLibrary.DataAccessLayer;

namespace MyLibrary_Test.Models_Tests.Repositories_Tests
{
    [TestFixture]
    class AuthorRepository_Tests
    {
        private AuthorRepository _repo;

        public AuthorRepository_Tests()
        {
            // arrange
            var fakeDao = A.Fake<IAuthorDataAccessor>();
            List<Author> authors = new List<Author>
            {
                new Author
                {
                    FirstName="John",
                    LastName="Smith"
                },
                new Author
                {
                    FirstName="Jane",
                    LastName="Doe"
                }
            };
            A.CallTo(() => fakeDao.ReadAll())
                .Returns(authors);
            this._repo = new AuthorRepository(fakeDao);
        }

        [Test]
        public async Task GetAll_Test()
        {
            // act
            var result = await _repo.GetAll();

            // assert
            Assert.IsTrue(result.ToList().Count == 2);
        }

        [TestCase("John", "Smith", true)]
        [TestCase("Jane", "Smith", false)]
        [TestCase("Jerry", "Jones", false)]
        public async Task ExistsWithName_Test_FirstNameLastName(string firstName, string lastName, bool expectedResult)
        {
            // act
            var actualResult = await _repo.ExistsWithName(firstName, lastName);

            // assert
            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestCase("John", true)]
        [TestCase("Smith", true)]
        [TestCase("Jones", false)]
        public async Task ExistsWithName_Test_EitherFirstNameOrLastName(string name, bool expectedResult)
        {
            // act
            var actualResult = await _repo.ExistsWithName(name);

            // assert
            Assert.AreEqual(expectedResult, actualResult);
        }
    }//class
}
