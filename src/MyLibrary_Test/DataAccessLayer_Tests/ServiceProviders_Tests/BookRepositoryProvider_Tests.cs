using System;
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

namespace MyLibrary_Test.DataAccessLayer_Tests.ServiceProviders_Tests
{
    [TestFixture]
    public class BookRepositoryProvider_Tests
    {
        [Test]
        public void Get_Test()
        {
            // arrange
            var fakeUow = A.Fake<IUnitOfWork>();
            BookRepositoryProvider provider = new BookRepositoryProvider();

            // act
            var result = provider.Get(fakeUow);

            // assert
            Assert.IsTrue(result.GetType() == typeof(BookRepository));
        }
    }
}
