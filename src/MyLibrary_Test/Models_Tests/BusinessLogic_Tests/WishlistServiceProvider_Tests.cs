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

namespace MyLibrary_Test.Models_Tests.BusinessLogic_Tests
{
    [TestFixture]
    class WishlistServiceProvider_Tests
    {
        [Test]
        public void Get_Test()
        {
            // arrange
            WishlistServiceProvider provider = new WishlistServiceProvider();

            // act
            var result = provider.Get();

            // assert
            Assert.AreEqual(typeof(WishlistService), result.GetType());
        }
    }//class
}
