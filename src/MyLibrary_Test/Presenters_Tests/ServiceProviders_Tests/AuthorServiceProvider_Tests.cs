using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using System.Data;
using NUnit;
using NUnit.Framework;
using FakeItEasy;
using MyLibrary.Views;
using MyLibrary.Presenters;
using MyLibrary.ApiService;
using MyLibrary.Models.Entities;
using MyLibrary.Models.BusinessLogic;
using MyLibrary.Presenters.ServiceProviders;

namespace MyLibrary_Test.Presenters_Tests.ServiceProviders_Tests
{
    [TestFixture]
    public class AuthorServiceProvider_Tests
    {
        [Test]
        public void Get_Test()
        {
            // arrange
            AuthorServiceProvider provider = new AuthorServiceProvider();

            // act
            var result = provider.Get();

            // assert
            Assert.IsTrue(result.GetType() == typeof(AuthorService));
        }
    }//class
}
