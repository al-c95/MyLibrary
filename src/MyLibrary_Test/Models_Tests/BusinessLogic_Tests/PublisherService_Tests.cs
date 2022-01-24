﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using NUnit;
using NUnit.Framework;
using FakeItEasy;
using MyLibrary.Models.Entities;
using MyLibrary.Models.BusinessLogic;
using MyLibrary.DataAccessLayer;
using MyLibrary.DataAccessLayer.Repositories;
using MyLibrary.DataAccessLayer.ServiceProviders;

namespace MyLibrary_Test.Models_Tests.BusinessLogic_Tests
{
    [TestFixture]
    public class PublisherService_Tests
    {
        [Test]
        public async Task Create_Test()
        {
            // arrange
            var fakeUowProvider = A.Fake<IUnitOfWorkProvider>();
            var fakeRepoProvider = A.Fake<IPublisherRepositoryProvider>();
            var fakeRepo = A.Fake<IPublisherRepository>();
            var fakeUow = A.Fake<IUnitOfWork>();
            A.CallTo(() => fakeUowProvider.Get()).Returns(fakeUow);
            A.CallTo(() => fakeRepoProvider.Get(fakeUow)).Returns(fakeRepo);
            PublisherService service = new PublisherService(fakeUowProvider, fakeRepoProvider);
            Publisher publisher = new Publisher("some_publisher");

            // act
            await service.Create(publisher);

            // assert
            A.CallTo(() => fakeRepo.Create(publisher)).MustHaveHappened();
            A.CallTo(() => fakeUow.Dispose()).MustHaveHappened();
        }

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

        [Test]
        public async Task GetAll_Test()
        {
            // arrange
            var fakeUowProvider = A.Fake<IUnitOfWorkProvider>();
            var fakeRepoProvider = A.Fake<IPublisherRepositoryProvider>();
            var fakeRepo = A.Fake<IPublisherRepository>();
            var fakeUow = A.Fake<IUnitOfWork>();
            A.CallTo(() => fakeUowProvider.Get()).Returns(fakeUow);
            A.CallTo(() => fakeRepoProvider.Get(fakeUow)).Returns(fakeRepo);
            PublisherService service = new PublisherService(fakeUowProvider, fakeRepoProvider);
            List<Publisher> publishers = new List<Publisher>
            {
                new Publisher("pub1"),
                new Publisher("pub2")
            };
            A.CallTo(() => fakeRepo.ReadAll()).Returns(publishers);

            // act
            var result = await service.GetAll();

            // assert
            Assert.IsTrue(result.ToList().Count == 2);
            Assert.IsTrue(result.ToList()[0].Name.Equals("pub1"));
            Assert.IsTrue(result.ToList()[1].Name.Equals("pub2"));
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
