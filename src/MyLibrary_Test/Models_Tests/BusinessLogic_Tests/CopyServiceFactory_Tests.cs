using System;
using System.Collections.Generic;
using System.Text;
using NUnit;
using NUnit.Framework;
using MyLibrary.Models.BusinessLogic;

namespace MyLibrary_Test.Models_Tests.BusinessLogic_Tests
{
    [TestFixture]
    public class CopyServiceFactory_Tests
    {
        [Test]
        public void GetBookCopyService_Test()
        {
            // arrange
            CopyServiceFactory factory = new CopyServiceFactory();

            // act
            var result = factory.GetBookCopyService();

            // assert
            Assert.AreEqual(typeof(BookCopyService), result.GetType());
        }

        [Test]
        public void GetMediaItemCopyService_Test()
        {
            // arrange
            CopyServiceFactory factory = new CopyServiceFactory();

            // act
            var result = factory.GetMediaItemCopyService();

            // assert
            Assert.AreEqual(typeof(MediaItemCopyService), result.GetType());
        }
    }//class
}
