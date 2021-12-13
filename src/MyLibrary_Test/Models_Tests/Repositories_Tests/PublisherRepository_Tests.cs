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
    class PublisherRepository_Tests
    {
        private PublisherRepository _repo;

        public PublisherRepository_Tests()
        {
            // arrange
            var fakeDao = A.Fake<IPublisherDataAccessor>();
            List<Publisher> publishers = new List<Publisher>
            {
                new Publisher
                {
                    Name="J.S. Publishing"
                },
                new Publisher
                {
                    Name="J.D. Publishing"
                }
            };
            A.CallTo(() => fakeDao.ReadAll())
                .Returns(publishers);
            this._repo = new PublisherRepository(fakeDao);
        }

        [Test]
        public async Task GetAll_Test()
        {
            // act
            var result = await _repo.GetAll();

            // assert
            Assert.IsTrue(result.ToList().Count == 2);
        }

        [TestCase("J.S. Publishing", true)]
        [TestCase("test", false)]
        public async Task Exists_Test(string name, bool expectedResult)
        {
            // act
            var actualResult = await _repo.Exists(name);

            // assert
            Assert.AreEqual(expectedResult, actualResult);
        }
    }//class
}
