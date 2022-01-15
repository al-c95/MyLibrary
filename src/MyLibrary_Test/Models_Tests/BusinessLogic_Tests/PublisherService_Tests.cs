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
using MyLibrary.DataAccessLayer;
using MyLibrary.DataAccessLayer.Repositories;

namespace MyLibrary_Test.Models_Tests.BusinessLogic_Tests
{
    [TestFixture]
    public class PublisherService_Tests
    {
        [TestCase("some_publisher", true)]
        [TestCase("bogus", false)]
        public async Task Exists_Test(string name, bool expectedResult)
        {
            // arrange
            MockPublisherService service = new MockPublisherService();

            // act
            bool actualResult = await service.Exists(name);

            // assert
            Assert.AreEqual(expectedResult, actualResult);
        }

        class MockPublisherService : PublisherService
        {
            public async override Task<IEnumerable<Publisher>> GetAll()
            {
                List<Publisher> publishers = new List<Publisher>();
                await Task.Run(() =>
                {
                    publishers.Add(new Publisher
                    {
                        Name = "some_publisher"
                    });

                    publishers.Add(new Publisher
                    {
                        Name = "another_publisher"
                    });
                });

                return publishers;
            }
        }//class
    }//class
}
