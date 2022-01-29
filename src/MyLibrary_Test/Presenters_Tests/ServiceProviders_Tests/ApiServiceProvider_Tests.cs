using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using NUnit;
using NUnit.Framework;
using FakeItEasy;
using MyLibrary.Presenters.ServiceProviders;
using MyLibrary.ApiService;

namespace MyLibrary_Test.Presenters_Tests.ServiceProviders_Tests
{
    [TestFixture]
    public class ApiServiceProvider_Tests
    {
        [Test]
        public void Get_Test()
        {
            // arrange
            ApiServiceProvider provider = new ApiServiceProvider();

            // act
            var result = provider.Get();

            // assert
            Assert.AreEqual(typeof(BookApiService), result.GetType());
        }
    }//class
}
