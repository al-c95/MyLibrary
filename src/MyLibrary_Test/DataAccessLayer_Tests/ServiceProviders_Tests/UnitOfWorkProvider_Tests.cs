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
    class UnitOfWorkProvider_Tests
    {
        [Test]
        public void Get_Test()
        {
            // arrange
            UnitOfWorkProvider uowProvider = new UnitOfWorkProvider();

            // act
            var result = uowProvider.Get();

            // assert
            Assert.IsTrue(result.GetType() == typeof(UnitOfWork));
        }
    }//class
}
