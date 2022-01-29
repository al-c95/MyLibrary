using System;
using System.Collections.Generic;
using System.Text;
using NUnit;
using NUnit.Framework;
using MyLibrary.Models.Entities;

namespace MyLibrary_Test.Models_Tests.Entities_Tests
{
    [TestFixture]
    public class Copy_Tests
    {
        [Test]
        public void Description_Test_Valid()
        {
            // arrange
            string description = "description";
            MockCopy copy = new MockCopy();

            // act/assert
            Assert.DoesNotThrow(() => copy.Description = description);
        }

        [Test]
        public void Description_Test_Empty()
        {
            // arrange
            string description = "";
            MockCopy copy = new MockCopy();

            // act/assert
            Assert.Throws<ArgumentNullException>(() => copy.Description = description);
        }

        class MockCopy : Copy
        {

        }
    }//class
}
